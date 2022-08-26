// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UninitializedMapException.cs" company="NPComplet">
//   Org
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NPComplet.YourDeals.Controls.OpenStreetMaps.Exceptions
{
    #region

    using System;

    #endregion

    /// <summary>
    /// The uninitialized map exception.
    /// </summary>
    //NOSONAR lightweight logging
    public class UninitializedMapException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UninitializedMapException"/> class.
        /// </summary>
        public UninitializedMapException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UninitializedMapException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        public UninitializedMapException(string message)
            : base(message)
        {
        }
    }
}