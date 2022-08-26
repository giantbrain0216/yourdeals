// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ApplicationCoreException.cs" company="NPComplet">
//   Org
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NPComplet.YourDeals.Domain.Shared.DataTransferObjects.Exceptions
{
    #region

    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Runtime.Serialization;

    #endregion

    /// <summary>
    ///     The custom application core exception.
    /// </summary>
    [ExcludeFromCodeCoverage]
    [Serializable]
    public class ApplicationCoreException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationCoreException"/> class. 
        ///     Initialize the class <see cref="ApplicationCoreException"/>
        /// </summary>
        public ApplicationCoreException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationCoreException"/> class. 
        /// Initialize the class <see cref="ApplicationCoreException"/>
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        public ApplicationCoreException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationCoreException"/> class. 
        /// Initializes a new instance of the <see cref="ApplicationCoreException"/>.
        /// </summary>
        /// <param name="message">
        /// The error message.
        /// </param>
        public ApplicationCoreException(ErrorMesage message)
            : base(Helper.ToEnumString(message))
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationCoreException"/> class. 
        /// Initializes a new instance of the <see cref="ApplicationCoreException"/>.
        /// </summary>
        /// <param name="message">
        /// The error message.
        /// </param>
        /// <param name="innerException">
        /// The inner Exception.
        /// </param>
        public ApplicationCoreException(ErrorMesage message, Exception innerException)
            : base(Helper.ToEnumString(message), innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationCoreException"/> class. 
        /// Initializes a new instance of the <see cref="ApplicationCoreException"/>.
        /// </summary>
        /// <param name="message">
        /// The error message.
        /// </param>
        /// <param name="innerMessage">
        /// The inner message.
        /// </param>
        public ApplicationCoreException(ErrorMesage message, string innerMessage)
            : base(Helper.ToEnumString(message), new Exception(innerMessage))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationCoreException"/> class. 
        /// Initializes a new instance of the <see cref="ApplicationCoreException"/>.
        /// </summary>
        /// <param name="message">
        /// The error message.
        /// </param>
        /// <param name="innerException">
        /// The inner Exception.
        /// </param>
        public ApplicationCoreException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationCoreException"/> class. 
        /// Initializes a new instance of the <see cref="ApplicationCoreException"/>.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="args">
        /// Args
        /// </param>
        public ApplicationCoreException(string message, params object[] args)
            : base(string.Format(CultureInfo.CurrentCulture, message, args))
        {
        }

        private string oldStackTrace = null ;

       

       

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationCoreException"/> class. 
        /// Initialize the class <see cref="ApplicationCoreException"/>
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        public ApplicationCoreException(string message, string stackTrace)
            : base(message)
        {
            this.oldStackTrace = stackTrace;
        }

        public override string StackTrace
        {
            get
            {
                return this.oldStackTrace ?? base.StackTrace;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationCoreException"/> class. 
        /// Initialize the class <see cref="ApplicationCoreException"/>.
        /// </summary>
        /// <param name="info">
        /// The serialization info
        /// </param>
        /// <param name="context">
        /// The context
        /// </param>
        protected ApplicationCoreException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        /// <inheritdoc />
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
    }
}