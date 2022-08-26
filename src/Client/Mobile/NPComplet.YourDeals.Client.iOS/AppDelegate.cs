// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AppDelegate.cs" company="NPComplet">
//   FR-SAS
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NPComplet.YourDeals.iOS
{
    #region

    using Foundation;

    using NPComplet.YourDeals.Client.Shared;

    using UIKit;

    using Xamarin;
    using Xamarin.Forms;
    using Xamarin.Forms.Platform.iOS;

    #endregion

    // The UIApplicationDelegate for the application. This class is responsible for launching the
    // User Interface of the application, as well as listening (and optionally responding) to
    // application events from iOS.
    /// <summary>
    /// The app delegate.
    /// </summary>
    [Register("AppDelegate")]
    public class AppDelegate : FormsApplicationDelegate
    {
        // This method is invoked when the application has loaded and is ready to run. In this
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        /// <summary>
        /// The finished launching.
        /// </summary>
        /// <param name="uiApplication">
        /// The ui application.
        /// </param>
        /// <param name="launchOptions">
        /// The launch options.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public override bool FinishedLaunching(UIApplication uiApplication, NSDictionary launchOptions)
        {
            Forms.Init();

            // For iOS, wrap inside a navigation page, otherwise the header looks wrong
            var formsApp = new NPCompletApp();
            formsApp.MainPage = new NavigationPage(formsApp.MainPage);
            FormsMaps.Init();
            this.LoadApplication(formsApp);

            return base.FinishedLaunching(uiApplication, launchOptions);
        }
    }
}