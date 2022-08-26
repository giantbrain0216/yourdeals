// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CustomMKAnnotationView.cs" company="NPComplet">
//   FR-SAS
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NPComplet.YourDeals.iOS.Renderers
{
    #region

    using MapKit;
    using Microsoft.MobileBlazorBindings.XFControls.CustomControls;

    #endregion

    /// <summary>
    /// The custom mk annotation view.
    /// </summary>
    public class CustomMKAnnotationView : MKAnnotationView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomMKAnnotationView"/> class.
        /// </summary>
        /// <param name="annotation">
        /// The annotation.
        /// </param>
        /// <param name="id">
        /// The id.
        /// </param>
        public CustomMKAnnotationView(IMKAnnotation annotation, string id)
            : base(annotation, id)
        {
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the url.
        /// </summary>
        public string Url { get; set; }

        public DealType DealType { get; set; }
    }
}