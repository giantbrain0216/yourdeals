// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Log.cs" company="NPComplet">
//   Org
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NPComplet.YourDeals.Domain.Shared.Monitoring
{
    #region

    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    using NPComplet.YourDeals.Domain.Shared.Base;

    #endregion

    /// <summary>
    ///     The log class.
    /// </summary>
    [Table("LOG", Schema = "MONITORING")]
    public class Log : BaseEntityDomain
    {
        /// <summary>
        ///     Gets or sets the date.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        ///     Gets or sets the exception.
        /// </summary>
        public string Exception { get; set; }

        /// <summary>
        ///     Gets or sets the level.
        /// </summary>
        public string Level { get; set; }

        /// <summary>
        ///     Gets or sets the logger.
        /// </summary>
        public string Logger { get; set; }

        /// <summary>
        ///     Gets or sets the message.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        ///     Thread
        /// </summary>
        public string Thread { get; set; }
    }
}