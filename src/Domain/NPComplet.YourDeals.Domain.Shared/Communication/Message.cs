// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Message.cs" company="NPComplet">
//   Org
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NPComplet.YourDeals.Domain.Shared.Communication
{
    #region

    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    using NPComplet.YourDeals.Domain.Shared.Base;
    using NPComplet.YourDeals.Domain.Shared.Shared;
    using NPComplet.YourDeals.Domain.Shared.Users;

    #endregion

    /// <summary>
    ///     the Message entity
    /// </summary>
    [Table("MESSAGES", Schema = "COMMUNICATION")]
    public class Message : BaseEntityDomain
    {
        /// <summary>
        /// </summary>
        public bool IsRevivedUserSeen { get; set; }

        /// <summary>
        ///     the message core of the message
        /// </summary>
        public Post MessageCore { get; set; }

        /// <summary>
        ///     the sending time in Utc
        /// </summary>
        public DateTime SendingTimeUtc { get; set; }

        /// <summary>
        ///     Owner
        /// </summary>
        public User ToUser { get; set; }

        /// <summary>
        ///     the deal Owner Id
        /// </summary>
        public Guid ToUserId { get; set; }

        /// <summary>
        /// The proposition messages post identifier.
        /// </summary>
        public Guid? PropositionMessagesPostId { get; set; }


        /// <summary>
        /// The deal messages post identifier.
        /// </summary>
        public Guid? DealMessagesPostId { get; set; }

        /// <summary>
        /// Get or set value that indicate the message between user and owner is open.
        /// </summary>
        public bool IsOpen { get; set; }

        /// <summary>
        /// Update when isOpen is false.
        /// </summary>
        public DateTime? CloseDate { get; set; }
    }
}