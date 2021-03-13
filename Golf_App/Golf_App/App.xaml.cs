using Android.Content.Res;
using System;
using Xamarin.Forms;

namespace Golf_App
{
    public partial class App : Application
    {
        public AssetManager Assets { get; private set; }

        public App()
        {
            InitializeComponent();
            MainPage = new AppShell();

        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
