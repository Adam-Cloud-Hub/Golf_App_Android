using Golf_App.Classes;
using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Golf_App.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Home : ContentPage
    {
        public Home()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            //for (int i = 0; i < 10; i++)
            //{
            //    if (File.Exists(EnviromentManager.SavedGamesPath + "Game" + i + ".xml"))
            //    {
            //        DisplayAlert("File", EnviromentManager.SavedGamesPath + "Game" + i + ".xml", "Ok");
            //    }
            //}

            //if (Directory.Exists(EnviromentManager.SavedGamesPath))
            //{
            //    DisplayAlert("File", "folder Exists", "Ok");
            //}            
            if (File.Exists(EnviromentManager.SaveCurrentGame))
            {
                bt_Continue_Game.IsVisible = true;
            }
            else
            {
                bt_Continue_Game.IsVisible = false;
            }
            base.OnAppearing();
        }

        private void bt_New_Game_Click(object sender, EventArgs e)
        {
            Shell.Current.CurrentItem = Shell.Current.CurrentItem.Items[2];
        }

        private void bt_Continue_Game_Click(object sender, EventArgs e)
        {
            GameManager.LoadPastGame();
            Shell.Current.CurrentItem = Shell.Current.CurrentItem.Items[3];
        }
    }
}