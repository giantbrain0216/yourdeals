// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExceptionMiddleware.cs" company="NPComplet">
//   Org
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NPComplet.YourDeals.Server.Api.Blazor.Extensions
{
    #region

    using System;
    using System.Net;
    using System.Text.Json;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;

    using NPComplet.YourDeals.Domain.Shared.DataTransferObjects;
    using NPComplet.YourDeals.Domain.Shared.DataTransferObjects.Exceptions;

    #endregion

    /// <summary>
    ///     Exception Middleware
    /// </summary>
    public class ExceptionMiddleware
    {
        private readonly ILogger<ExceptionMiddleware> _logger;

        private readonly RequestDelegate _next;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionMiddleware"/> class.
        /// </summary>
        /// <param name="next">
        /// The next. 
        /// </param>
        /// <param name="logger">
        /// The logger. 
        /// </param>
        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            this._logger = logger;
            this._next = next;
        }

        /// <summary>
        /// InvokeAsync
        /// </summary>
        /// <param name="httpContext">
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await this._next(httpContext);
            }
            catch (Exception ex)
            {
                if (ex is ApplicationCoreException)
                    this._logger.LogError(ex, $"Functional Exception in the Application  {ex.Message} , trace {ex.StackTrace}");
                else
                    this._logger.LogError(ex,$"Exception in the Application  {ex.Message} , trace {ex.StackTrace}");
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            if (exception is ApplicationCoreException) context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

            await context.Response.WriteAsync(
                JsonSerializer.Serialize(
                    new ErrorDetails
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = exception.Message,
                            StackTrace = exception.StackTrace
                        }));
        }
    }

    internal class ErrorDetails
    {
        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        public string? Message { get; set; }

        /// <summary>
        /// Gets or sets the stack trace.
        /// </summary>
        public string? StackTrace { get; set; }

        /// <summary>
        /// Gets the status code.
        /// </summary>
        public int StatusCode { get; internal set; }
    }
}