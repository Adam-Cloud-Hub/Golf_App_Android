using Golf_App.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Golf_App.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GameHistory : ContentPage
    {
        public GameHistory()
        {
            InitializeComponent();



        }

        protected override void OnAppearing()
        {
            //Write the code of your page here



            StatsManager.FindGameHistory();
            StatsManager.GetGameHistory();
            lv_View_Game_History.ItemsSource = StatsManager.GameHistory;


            base.OnAppearing();
        }

        private void lv_View_Game_History_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {



            //StatsManager.SetViewGameHistory();

            if (StatsManager.ViewGameHistory != null)
            {
                //lv_Game_History.ItemsSource = StatsManager.ViewGameHistory.CourseHoles;
                //lv_Game_History.SelectedIndex = 0;


                //bt_Delete_Game_File.Visibility = Visibility.Visible;
            }
            else
            {
                //bt_Delete_Game_File.Visibility = Visibility.Hidden;
            }

        }
    }
}