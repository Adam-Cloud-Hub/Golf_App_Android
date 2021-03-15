using Golf_App.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Golf_App.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GameInProgress : ContentPage
    {
        public GameInProgress()
        {
            InitializeComponent();
        }

        private async void NoSelectedCourse()
        {
            await DisplayAlert("Select a Course", "Please select a Course to Play!", "Ok");
            Shell.Current.CurrentItem = Shell.Current.CurrentItem.Items[2];
        }

        protected override void OnAppearing()
        {
            if (GameManager.CurrentGame.CourseName == null)
            {
                NoSelectedCourse();
            }

            loadCourseData();
            base.OnAppearing();
        }

        private void loadCourseData()
        {
            Title_Game_in_Progress.Text = GameManager.CurrentGame.CourseName;
            cb_ScrollHoleNumber.ItemsSource = GameManager.CurrentGame.CourseHoles;
            cb_ScrollScore.ItemsSource = GameManager.UserScoreValues;
            cb_ScrollHoleNumber.SelectedIndex = 0;
        }

        private void cb_ScrollHoleNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            var hole = (sender as Picker).SelectedItem as Hole;
            if (hole != null)
            {
                if (hole.HoleNumber == 1)
                {
                    bt_PreviousHole.IsVisible = false;
                    bt_NextHole.IsVisible = true;
                }
                else if (hole.HoleNumber == GameManager.CurrentGame.CourseHoles.Length)
                {
                    bt_PreviousHole.IsVisible = true;
                    bt_NextHole.IsVisible = false;
                }
                else
                {
                    bt_PreviousHole.IsVisible = true;
                    bt_NextHole.IsVisible = true;
                }

                string holeimage = "Golf_App.Resources.Courses." + Regex.Replace(GameManager.CurrentGame.CourseName, @"\s+", "")  + ".Hole" + hole.HoleNumber + ".JPG";
                image_coursehole.Source = ImageSource.FromResource(holeimage);

                GameManager.ParNumber = hole.HolePar;
                GameManager.HoleNumber = hole.HoleNumber;
                GameManager.CurrentHoleNumber = cb_ScrollHoleNumber.SelectedIndex;                  
                cb_ScrollScore.SelectedIndex = hole.HoleScore;               
                tb_Hole_Distance.Text = hole.HoleDistance.ToString() + " Yards";
                tb_ParValue.Text = hole.HolePar.ToString();
                bt_ScrollHoleNumber.Text = (GameManager.CurrentHoleNumber + 1).ToString();
            }
            else
            {
            }
        }

        private void cb_ScrollScore_SelectedIndexChanged(object sender, EventArgs e)
        {
            GameManager.ScoreNumber = cb_ScrollScore.SelectedIndex;
            GameManager.ScoreTermGame();
            GameManager.UpdateGame();
            tb_ParScore.Text = GameManager.CurrentGame.CourseHoles[GameManager.CurrentHoleNumber].HoleParScore;
            bt_ScrollScore.Text = GameManager.CurrentGame.CourseHoles[GameManager.CurrentHoleNumber].HoleScore.ToString();  
        }

        private void bt_PreviousHole_Clicked(object sender, EventArgs e)
        {
            int holenumber = GameManager.CurrentHoleNumber + 2;

            if (holenumber == GameManager.CurrentGame.CourseHoles.Length)
            {
                GameManager.CurrentHoleNumber--;
                cb_ScrollHoleNumber.SelectedIndex = GameManager.CurrentHoleNumber;
            }
            else
            {
                GameManager.CurrentHoleNumber--;
                cb_ScrollHoleNumber.SelectedIndex = GameManager.CurrentHoleNumber;
            }
        }

        private void bt_NextHole_Clicked(object sender, EventArgs e)
        {
            int holenumber = GameManager.CurrentHoleNumber + 2;

            if (holenumber == GameManager.CurrentGame.CourseHoles.Length)
            {
                GameManager.CurrentHoleNumber++;
                cb_ScrollHoleNumber.SelectedIndex = GameManager.CurrentHoleNumber;
            }
            else
            {
                GameManager.CurrentHoleNumber++;
                cb_ScrollHoleNumber.SelectedIndex = GameManager.CurrentHoleNumber;
            }
        }

        private void bt_FinishGame_Clicked(object sender, EventArgs e)
        {
            GameManager.SaveGame();
            Shell.Current.CurrentItem = Shell.Current.CurrentItem.Items[1];
        }

        private void bt_ScrollHoleNumber_Clicked(object sender, EventArgs e)
        {
            cb_ScrollHoleNumber.Focus();
        }

        private void bt_ScrollScore_Clicked(object sender, EventArgs e)
        {
            cb_ScrollScore.Focus();
        }
    }
}