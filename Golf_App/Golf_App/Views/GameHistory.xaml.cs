using Golf_App.Classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
            StatsManager.FindGameHistory();     // Scans for any saved games.
            lv_View_Game_History.ItemsSource = StatsManager.GameHistory;    // Sets listview.
            lv_View_Game_History.SelectedItem = ((ObservableCollection<Course>)lv_View_Game_History.ItemsSource).FirstOrDefault();      // Selects first index data set in the listview
            Display_Game_History();     // Hides / Shows information based on save games.
            base.OnAppearing();
        }

        // Loads in save games to be viewable.
        private void lv_View_Game_History_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (StatsManager.GameHistory.Count != 0)
            {
                StatsManager.ViewGameHistory = (Course)(sender as ListView).SelectedItem;
                lv_Game_History.ItemsSource = StatsManager.ViewGameHistory.CourseHoles;
                lv_Game_History.SelectedItem = ((Hole[])lv_Game_History.ItemsSource).FirstOrDefault();
                StatsManager.GameFile = StatsManager.ViewGameHistory.FileName;

                tb_History_TotalScore.Text = StatsManager.ViewGameHistory.TotalScore.ToString();
                tb_History_Handicap.Text = StatsManager.ViewGameHistory.UserHandicap.ToString();
                tb_History_Handicap_Score.Text = (StatsManager.ViewGameHistory.TotalScore - StatsManager.ViewGameHistory.UserHandicap).ToString();
            }
            else
            {
            }
        }

        // Removes the saved game file. 
        private void bt_Delete_Game_File_Clicked(object sender, EventArgs e)
        {
            //DisplayAlert("File", "Deleting " + StatsManager.GameFile + StatsManager.ViewGameHistory.FileName + StatsManager.ViewGameHistory.GameTime, "Ok");

            if (File.Exists(EnviromentManager.SavedGamesPath + StatsManager.GameFile))
            {
                File.Delete(EnviromentManager.SavedGamesPath + StatsManager.GameFile);
            }
            StatsManager.ClearGameHistory();

            lv_View_Game_History.SelectedItem = ((ObservableCollection<Course>)lv_View_Game_History.ItemsSource).FirstOrDefault();
            Display_Game_History();
        }

        private void Display_Game_History()
        {
            if (StatsManager.GameHistory.Count == 0)
            {
                Grid_History.IsVisible = false;
            }
            else
            {
                Grid_History.IsVisible = true;
            }
        }
    }
}