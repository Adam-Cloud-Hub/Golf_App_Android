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


            //Write the code of your page here

            if (File.Exists(EnviromentManager.SaveCurrentGame))
            {
                //DisplayAlert("File", "File Found!", "Ok");

                bt_Continue_Game.IsVisible = true;
            }
            else
            {
                //DisplayAlert("File", "File Not Found!", "Ok");

                bt_Continue_Game.IsVisible = false;
            }

            base.OnAppearing();
        }

        private void bt_New_Game_Click(object sender, EventArgs e)
        {
            //await Shell.Current.GoToAsync("SelectGame");

            //await Navigation.PushAsync(new SelectGame());

            Shell.Current.CurrentItem = Shell.Current.CurrentItem.Items[2];



            //tab_load_game.Focus();
        }

        private void bt_Continue_Game_Click(object sender, EventArgs e)
        {

            GameManager.LoadPastGame();
            Shell.Current.CurrentItem = Shell.Current.CurrentItem.Items[3];

        }
    }
}