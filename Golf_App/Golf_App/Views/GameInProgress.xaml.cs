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

        // Prompts the user to select a course then return back to course select page.
        private async void NoSelectedCourse()
        {
            await DisplayAlert("Select a Course", "Please select a Course to Play!", "Ok");
            Shell.Current.CurrentItem = Shell.Current.CurrentItem.Items[2];
        }

        protected override void OnAppearing()
        {          
            if (GameManager.CurrentGame.IsEnabled == false)
            {
                Display_Game_Content(false);    // Hides page content (shows nothing behind message box)
                NoSelectedCourse();             // Prompts the user to select a course. 
            }
            else
            {
                Display_Game_Content(true);     // Displays the page contents
                loadCourseData();               // Loads in select course data.
            }            
            base.OnAppearing();
        }

        // Hides / shows content (gives it a cleaner look)
        private void Display_Game_Content(bool display_content)
        {
            image_coursehole.IsVisible = display_content;
            Title_Game_in_Progress.IsVisible = display_content;
            bt_NextHole.IsVisible = display_content;
            bt_PreviousHole.IsVisible = display_content;
            bt_ScrollHoleNumber.IsVisible = display_content;
            tb_ParValue_Text.IsVisible = display_content;
            tb_ParValue.IsVisible = display_content;
            tb_Hole_Distance_Text.IsVisible = display_content;
            tb_Hole_Distance.IsVisible = display_content;
            tb_Hole_Stroke_Index_Text.IsVisible = display_content;
            tb_Hole_Stroke_Index.IsVisible = display_content;
            tb_ParScore_Text.IsVisible = display_content;
            tb_ParScore.IsVisible = display_content;
            bt_ScrollScore.IsVisible = display_content;
            tb_CurrentScore_Text.IsVisible = display_content;
            bt_FinishGame.IsVisible = display_content;
        }

        // Loads in select course data and selects first hole.
        private void loadCourseData()
        {
            Title_Game_in_Progress.Text = GameManager.CurrentGame.CourseName;
            cb_ScrollHoleNumber.ItemsSource = GameManager.CurrentGame.CourseHoles;
            cb_ScrollScore.ItemsSource = GameManager.UserScoreValues;
            cb_ScrollHoleNumber.SelectedIndex = 0;
        }

        // Loads relevant data for the selected hole.
        private void cb_ScrollHoleNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            var hole = (sender as Picker).SelectedItem as Hole;
            if (hole != null)
            {
                if (hole.HoleNumber == 1)               // hides Previous Hole button then hole number is 1.
                {                                       // Display Next Hole button.
                    bt_PreviousHole.IsVisible = false;
                    bt_NextHole.IsVisible = true;
                }
                else if (hole.HoleNumber == GameManager.CurrentGame.CourseHoles.Length) // hides Next Hole button then hole number is at the last hole.
                {                                                                       // Display Previous Hole button.
                    bt_PreviousHole.IsVisible = true;
                    bt_NextHole.IsVisible = false;
                }
                else
                {
                    bt_PreviousHole.IsVisible = true;   // Display Previous Hole button.
                    bt_NextHole.IsVisible = true;       // Display Next Hole button.
                }

                string holeimage = "Golf_App.Resources.Courses." + Regex.Replace(GameManager.CurrentGame.CourseName, @"\s+", "")  + ".Hole" + hole.HoleNumber + ".JPG";
                image_coursehole.Source = ImageSource.FromResource(holeimage);      // Loads relevant hole image

                GameManager.ParNumber = hole.HolePar;
                GameManager.HoleNumber = hole.HoleNumber;
                GameManager.CurrentHoleNumber = cb_ScrollHoleNumber.SelectedIndex;
                cb_ScrollScore.SelectedIndex = hole.HoleScore;
                tb_Hole_Distance.Text = hole.HoleDistance.ToString() + " Yards";
                tb_Hole_Stroke_Index.Text = hole.HoleStrokeIndex.ToString();
                tb_ParValue.Text = hole.HolePar.ToString();
                bt_ScrollHoleNumber.Text = (GameManager.CurrentHoleNumber + 1).ToString();

                GameSummary();      // Active on hole 9 or 18
            }
            else
            {
            }
        }

        // Takes the user's inputted score and returns a score term aswell as updates the placeholder file for current game.
        private void cb_ScrollScore_SelectedIndexChanged(object sender, EventArgs e)
        {
            GameManager.ScoreNumber = cb_ScrollScore.SelectedIndex;
            GameManager.ScoreTermGame();
            GameManager.UpdateGame();
            tb_ParScore.Text = GameManager.CurrentGame.CourseHoles[GameManager.CurrentHoleNumber].HoleParScore;
            bt_ScrollScore.Text = GameManager.CurrentGame.CourseHoles[GameManager.CurrentHoleNumber].HoleScore.ToString();

            GameSummary();      // Active on hole 9 or 18
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

        // Saves a file of the current game to game history folder and clears the current game data then returns to home page. 
        private void bt_FinishGame_Clicked(object sender, EventArgs e)
        {
            GameManager.SaveGame();
            GameManager.ClearCurrentGame();
            GameManager.CurrentGame.IsEnabled = false;
            Shell.Current.CurrentItem = Shell.Current.CurrentItem.Items[1];     // Returns to home page
        }

        // Loads a scroll bar for the hole number.
        private void bt_ScrollHoleNumber_Clicked(object sender, EventArgs e)
        {
            cb_ScrollHoleNumber.Focus();
        }

        // Loads a scroll bar for the score of the hole number.
        private void bt_ScrollScore_Clicked(object sender, EventArgs e)
        {
            cb_ScrollScore.Focus();
        }

        // Displays a game summary after 9 holes or a total summary on the last hole (9 or 18) once user has entered a score for that hole.
        private void GameSummary()
        {
            if (GameManager.HoleNumber == GameManager.CurrentGame.CourseHoles.Last().HoleNumber && GameManager.ScoreNumber > 0)
            {
                int first;
                int last = GameManager.CurrentGame.CourseHoles.Last().HoleNumber;
                if (GameManager.CurrentGame.CourseHoles.Last().HoleNumber == 9)
                {
                    first = 0;
                }
                else
                {
                    first = 9;
                }
                GameManager.CurrentGameScoreTotal();
                DisplayAlert("Game Summary", "Total Score " + GameManager.ScoreTotal.ToString() + Environment.NewLine + GameManager.CurrentGameSummary(GameManager.ScoreParTotal,
                    GameManager.ScoreTotal) + Environment.NewLine + "Handicap " + GameManager.CurrentGame.UserHandicap.ToString() + Environment.NewLine + GameManager.GameSummary(first, last), "Ok");
            }
            else if (GameManager.HoleNumber == 9 && GameManager.ScoreNumber > 0)
            {
                GameManager.CurrentGameScoreFirstNine();
                DisplayAlert("Game Summary", "Current Score " + GameManager.ScoreFirstNine.ToString() + Environment.NewLine + GameManager.CurrentGameSummary(GameManager.ScoreParFirstNine, GameManager.ScoreFirstNine) + Environment.NewLine + GameManager.GameSummary(0, 9), "Ok");
            }
        }
    }
}