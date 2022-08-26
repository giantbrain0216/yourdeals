﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Main.cs" company="NPComplet">
//   FR-SAS
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NPComplet.YourDeals.iOS
{
    #region

    using Microsoft.MobileBlazorBindings.WebView.iOS;

    using UIKit;

    #endregion

    /// <summary>
    /// The application.
    /// </summary>
    public class Application
    {
        // This is the main entry point of the application.
        private static void Main(string[] args)
        {
            BlazorHybridIOS.Init();

            // if you want to use a different Application Delegate class from "AppDelegate"
            // you can specify it here.
            UIApplication.Main(args, null, "AppDelegate");
        }
    }
}