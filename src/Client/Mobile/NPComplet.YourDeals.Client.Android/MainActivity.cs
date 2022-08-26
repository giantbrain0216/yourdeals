// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MainActivity.cs" company="NPComplet">
//   FR-SAS
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NPComplet.YourDeals.Client.Android
{
    using Acr.UserDialogs;
    #region

    using global::Android.App;
    using global::Android.Content;
    using global::Android.Content.PM;
    using global::Android.OS;
    using global::Android.Runtime;

    using Microsoft.MobileBlazorBindings.WebView.Android;

    using Shared;
    using System;
    using Xamarin;
    using Xamarin.Forms;
    using Xamarin.Forms.Platform.Android;

    using Platform = Xamarin.Essentials.Platform;

    #endregion

    /// <summary>
    /// The main activity.
    /// </summary>
    [Activity(
        Label = "YourDeals",
        Icon = "@mipmap/icon",
        Theme = "@style/MainTheme",
        MainLauncher = true,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : FormsAppCompatActivity
    {
        /// <summary>
        /// The on request permissions result.
        /// </summary>
        /// <param name="requestCode"> The request code. </param>
        /// <param name="permissions"> The permissions. </param>
        /// <param name="grantResults"> The grant results. </param>
        public override void OnRequestPermissionsResult(
            int requestCode,
            string[] permissions,
            [GeneratedEnum] Permission[] grantResults)
        {
            Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        /// <summary>
        /// The on create.
        /// </summary>
        /// <param name="savedInstanceState"> The saved instance state. </param>
        protected override void OnCreate(Bundle savedInstanceState)
        {
            BlazorHybridAndroid.Init();

            var fileProvider = new AssetFileProvider(Assets, "wwwroot");
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Platform.Init(this, savedInstanceState);
            Forms.Init(this, savedInstanceState);
            UserDialogs.Init(this);
            FormsMaps.Init(this, savedInstanceState);
            LoadApplication(new NPCompletApp(fileProvider));
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            var assembly = typeof(BlazorHybridAndroid).Assembly;
            Type type = assembly.GetType("Microsoft.MobileBlazorBindings.WebView.Android.ActivityResultCallbackRegistryHelper");
            var method = type.GetMethod("InvokeCallback");
            method?.Invoke(null, new object[] { requestCode, resultCode, data });
            base.OnActivityResult(requestCode, resultCode, data);
        }
    }
}