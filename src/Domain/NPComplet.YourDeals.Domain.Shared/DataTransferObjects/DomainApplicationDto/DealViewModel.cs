// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DealViewModel.cs" company="NPComplet">
//   Org
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using NPComplet.YourDeals.Domain.Shared.Enumerable;
using NPComplet.YourDeals.Domain.Shared.Users;
using NPComplet.YourDeals.Translations.Resources;

namespace NPComplet.YourDeals.Domain.Shared.DataTransferObjects.DomainApplicationDto
{
    #region

    #endregion

    /// <summary>
    /// </summary>
    public class DealViewModel
    {
        /// <summary>
        /// The payment manors
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(Translation), ErrorMessageResourceName = "Required_field")]
        public IEnumerable<PaymentManor> PaymentManors { get; set; }

        /// <summary>
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(Translation), ErrorMessageResourceName = "Required_field")]
        public string Description { get; set; }

        /// <summary>
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// </summary>
        public List<string> ImagePath { get; set; }

        /// <summary>
        /// </summary>
        public float Lat { get; set; }

        /// <summary>
        /// </summary>
        public float Lon { get; set; }

        /// <summary>
        /// </summary>
        public User Owner { get; set; }

        /// <summary>
        /// </summary>
        public string OwnerId { get; set; }

        /// <summary>
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        ///     Gets or set the tags value.
        /// </summary>
        public string Tags { get; set; }

        /// <summary>
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(Translation), ErrorMessageResourceName = "Required_field")]
        public string Title { get; set; }
    }
}