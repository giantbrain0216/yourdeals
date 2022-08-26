// --------------------------------------------------------------------------------------------------------------------
// <copyright file="XUnitLoggerProvider.cs" company="NPComplet">
//   FR-SAS
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NPComplet.YourDeals.Tests.TestsHelper
{
    #region

    using System;

    using Microsoft.Extensions.Logging;

    #endregion

    /// <summary>
    /// The x unit logger provider.
    /// </summary>
    public class XUnitLoggerProvider : ILoggerProvider
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="XUnitLoggerProvider"/> class.
        /// </summary>
        /// <param name="writer"> The writer. </param>
        public XUnitLoggerProvider(IWriter writer)
        {
            Writer = writer;
        }

        /// <summary>
        /// Gets the writer.
        /// </summary>
        public IWriter Writer { get; }

        /// <summary>
        /// The create logger.
        /// </summary>
        /// <param name="categoryName"> The category name. </param>
        /// <returns>
        /// The <see cref="ILogger"/>.
        /// </returns>
        public ILogger CreateLogger(string categoryName)
        {
            return new XUnitLogger(Writer);
        }

        /// <summary>
        /// The dispose.
        /// </summary>
        public void Dispose()
        {
            GC.Collect();
            GC.SuppressFinalize(this);
        }
    }

    /// <summary>
    /// The x unit logger.
    /// </summary>
    public class XUnitLogger : ILogger
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="XUnitLogger"/> class.
        /// </summary>
        /// <param name="writer">
        /// The writer.
        /// </param>
        public XUnitLogger(IWriter writer)
        {
            Writer = writer;
            Name = nameof(XUnitLogger);
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets the writer.
        /// </summary>
        public IWriter Writer { get; }

        /// <summary>
        /// The begin scope.
        /// </summary>
        /// <param name="state">
        /// The state.
        /// </param>
        /// <typeparam name="TState">
        /// </typeparam>
        /// <returns>
        /// The <see cref="IDisposable"/>.
        /// </returns>
        public IDisposable BeginScope<TState>(TState state)
        {
            return new XUnitScope();
        }

        /// <summary>
        /// The is enabled.
        /// </summary>
        /// <param name="logLevel"> The log level. </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        /// <summary>
        /// The log.
        /// </summary>
        /// <param name="logLevel"> The log level. </param>
        /// <param name="eventId"> The event id. </param>
        /// <param name="state"> The state. </param>
        /// <param name="exception"> The exception. </param>
        /// <param name="formatter"> The formatter. </param>
        /// <typeparam name="TState"> </typeparam>
        /// <exception cref="ArgumentNullException">
        /// </exception>
        public void Log<TState>(
            LogLevel logLevel,
            EventId eventId,
            TState state,
            Exception exception,
            Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
                return;

            if (formatter == null)
                throw new ArgumentNullException(nameof(formatter));

            var message = formatter(state, exception);
            if (string.IsNullOrEmpty(message) && exception == null)
                return;

            var line = $"{logLevel}: {Name}: {message}";

            Writer.WriteLine(line);

            if (exception != null) Writer.WriteLine(exception.ToString());
        }
    }

    /// <summary>
    /// The x unit scope.
    /// </summary>
    public class XUnitScope : IDisposable
    {
        /// <summary>
        /// The dispose.
        /// </summary>
        public void Dispose()
        {
            GC.Collect();
            GC.SuppressFinalize(this);
        }
    }
}