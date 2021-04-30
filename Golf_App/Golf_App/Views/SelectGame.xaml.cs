using Android.Views;
using Golf_App.Classes;
using System;
using System.IO;
using System.Xml.Serialization;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Golf_App.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SelectGame : ContentPage
    {
        public SelectGame()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            lv_courses.ItemsSource = CoursesManager.Courses.Course;
            cb_ScrollHoleNumber.ItemsSource = GameManager.UserScoreValues;
            cb_ScrollHoleNumber.SelectedIndex = 0;
            base.OnAppearing();
        }

        private void lv_courses_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            CoursesManager.SelectedCourse = (Course)(sender as ListView).SelectedItem;
        }

        private void bt_load_game_Click(object sender, EventArgs e)
        {
            if (lv_courses.SelectedItem == null)
            {
                DisplayAlert("Select a Course", "Please select a Course to Play!", "Ok");
            }
            else
            {
                CoursesManager.LaunchGame();
                GameManager.CurrentGame.IsEnabled = true;
                GameManager.CurrentGame.UserHandicap = GameManager.UserHandicap;

                foreach (var hole in GameManager.CurrentGame.CourseHoles)
                {
                    hole.HoleScore = 0;
                }

                lv_courses.SelectedItem = null;
                GameManager.UserHandicap = 0;
                Shell.Current.CurrentItem = Shell.Current.CurrentItem.Items[3];
            }
        }

        private void bt_ScrollHoleNumber_Clicked(object sender, EventArgs e)
        {
            cb_ScrollHoleNumber.Focus();
        }

        private void cb_ScrollHoleNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            bt_ScrollHoleNumber.Text = cb_ScrollHoleNumber.SelectedIndex.ToString();
            GameManager.UserHandicap = cb_ScrollHoleNumber.SelectedIndex;
        }
    }
}