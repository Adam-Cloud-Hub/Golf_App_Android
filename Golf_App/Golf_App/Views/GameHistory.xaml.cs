using Golf_App.Classes;
using System;
using System.Collections.Generic;
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
            StatsManager.FindGameHistory();
            StatsManager.GetGameHistory();
            lv_View_Game_History.ItemsSource = StatsManager.GameHistory;
            lv_View_Game_History.SelectedItem = ((List<Course>)lv_View_Game_History.ItemsSource).FirstOrDefault();

            if (StatsManager.GameHistory.Count == 0)
            {
                grid_History.IsVisible = false;
                lv_Game_History.IsVisible = false;
                bt_Delete_Game_File.IsVisible = false;
            }
            else
            {
                grid_History.IsVisible = true;
                lv_Game_History.IsVisible = true;
                bt_Delete_Game_File.IsVisible = true;
            }
            base.OnAppearing();
        }

        private void lv_View_Game_History_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (StatsManager.GameHistory.Count != 0)
            {
                StatsManager.ViewGameHistory = (Course)(sender as ListView).SelectedItem;
                StatsManager.SetViewGameHistory();

                lv_Game_History.ItemsSource = StatsManager.ViewGameHistory.CourseHoles;
                lv_Game_History.SelectedItem = ((Hole[])lv_Game_History.ItemsSource).FirstOrDefault();
            }
            else
            {
            }

        }

        private void lv_Game_History_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var hole = (sender as ListView).SelectedItem as Hole;
            if (hole != null)
            {
                tb_History_HoleNumber.Text = hole.HoleNumber.ToString();
                tb_History_HoleParNumber.Text = hole.HolePar.ToString();
                tb_History_HoleScore.Text = hole.HoleScore.ToString();
                tb_History_ParScore.Text = hole.HoleParScore;
            }
            else
            {
            }
        }

        // Removes the saved game file. 
        private void bt_Delete_Game_File_Clicked(object sender, EventArgs e)
        {
            if (File.Exists(EnviromentManager.SavedGamesPath + StatsManager.GameFile)) File.Delete(EnviromentManager.SavedGamesPath + StatsManager.GameFile);
            StatsManager.ClearGameHistory();

            Shell.Current.CurrentItem = Shell.Current.CurrentItem.Items[1];
        }
    }
}