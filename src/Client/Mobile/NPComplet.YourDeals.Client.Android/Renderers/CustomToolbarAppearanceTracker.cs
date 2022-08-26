using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace NPComplet.YourDeals.Client.Android.Renderers
{
    /// <summary>
    /// Toolbar appearance
    /// </summary>
    public class CustomToolbarAppearanceTracker : ShellToolbarAppearanceTracker
    {
        public CustomToolbarAppearanceTracker(IShellContext context) : base(context)
        {
        }

        public override void ResetAppearance(AndroidX.AppCompat.Widget.Toolbar toolbar, IShellToolbarTracker toolbarTracker)
        {
            toolbar.SetBackgroundColor(Color.Transparent.ToAndroid());
        }

        public override void SetAppearance(AndroidX.AppCompat.Widget.Toolbar toolbar, IShellToolbarTracker toolbarTracker, ShellAppearance appearance)
        {
            toolbar.SetBackgroundColor(Color.Transparent.ToAndroid());
        }
    }
}