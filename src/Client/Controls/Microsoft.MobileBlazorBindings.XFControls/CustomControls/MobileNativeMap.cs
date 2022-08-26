// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MobileNativeMap.cs" company="NPComplet">
//   Org
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Microsoft.MobileBlazorBindings.XFControls.CustomControls
{
    #region

    using System.Collections.Generic;

    using NPComplet.YourDeals.Domain.Shared.Search;

    using Xamarin.Forms.Maps;

    #endregion

    /// <summary>
    /// The mobile native map.
    /// </summary>
    public class MobileNativeMap : Map
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MobileNativeMap"/> class.
        /// </summary>
        public MobileNativeMap()
        {
            this.NPCompletPins = new List<NPCompletPin>();
        }

        /// <summary>
        /// Gets or sets the initial view.
        /// </summary>
        public MapSpan InitialView { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is search.
        /// </summary>
        public bool isSearch { get; set; }

        /// <summary>
        /// Gets or sets the np complet pins.
        /// </summary>
        public List<NPCompletPin> NPCompletPins { get; set; }
    }

    /// <summary>
    /// The np complet pin.
    /// </summary>
    public class NPCompletPin : Pin
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NPCompletPin"/> class.
        /// </summary>
        public NPCompletPin()
        {
            this.Tags = new List<Tag>();
        }

        /// <summary>
        /// Gets or sets the deal type.
        /// </summary>
        public DealType DealType { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the tags.
        /// </summary>
        public List<Tag> Tags { get; set; }

        /// <summary>
        /// Gets or sets the url.
        /// </summary>
        public string Url { get; set; }
    }

    /// <summary>
    /// The deal type.
    /// </summary>
    public enum DealType
    {
        /// <summary>
        /// The offer.
        /// </summary>
        Offer,

        /// <summary>
        /// The request.
        /// </summary>
        Request
    }
}