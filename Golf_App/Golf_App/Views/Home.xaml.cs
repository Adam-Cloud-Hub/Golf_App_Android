using Golf_App.Classes;
using System;
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


            Routing.RegisterRoute("Home", typeof(Home));
            Routing.RegisterRoute("SelectGame", typeof(SelectGame));

        }


        private async void bt_New_Game_Click(object sender, EventArgs e)
        {

            await Shell.Current.GoToAsync("SelectGame");

            //tab_load_game.Focus();
        }

        private void bt_Continue_Game_Click(object sender, EventArgs e)
        {
            //tab_game_in_progress.Visibility = Visibility.Visible;
            //GameManager.LoadPastGame();
            //loadCourseData();
            //tab_game_in_progress.Focus();
        }
    }
}