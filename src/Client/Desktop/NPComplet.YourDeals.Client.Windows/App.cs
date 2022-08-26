using System;
using Microsoft.MobileBlazorBindings.WebView.Windows;
using NPComplet.YourDeals.Client.Shared;
using Xamarin.Forms;
using Xamarin.Forms.Platform.WPF;
using Application = System.Windows.Application;

namespace NPComplet.YourDeals.Client.Windows
{
    public class MainWindow : FormsApplicationPage
    {
        public MainWindow(string[] args)
        {
            Forms.Init();
            BlazorHybridWindows.Init();
            LoadApplication(new NPCompletApp());
        }

        [STAThread]
        public static void Main(string[] args)
        {
            var app = new Application();
            app.Run(new MainWindow(args));
        }
    }
}