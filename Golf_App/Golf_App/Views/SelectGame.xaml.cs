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
            //Write the code of your page here
            lv_courses.ItemsSource = CoursesManager.Courses.Course;
            base.OnAppearing();
        }


        private void lv_courses_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
           // CoursesManager.SwitchSelectionAll(false);           // Clears selected course.

            
            CoursesManager.SelectedCourse = (Course)(sender as ListView).SelectedItem;

        }

        private void bt_load_game_Click(object sender, EventArgs e)
        {
            if (lv_courses.SelectedItem == null)
            {
                //tab_game_in_progress.Visibility = Visibility.Hidden;

                DisplayAlert("Select a Course", "Please select a Course to Play!","Ok");
            }
            else
            {
                //tab_game_in_progress.Visibility = Visibility.Visible;
                //GameManager.CurrentHoleNumber = 0;

                CoursesManager.LaunchGame();

                //await Shell.Current.GoToAsync("GameInProgress");

                //Shell.Current.CurrentItem = Shell.Current.CurrentItem.Items[3];

                Shell.Current.CurrentItem = Shell.Current.CurrentItem.Items[3];

                //tab_game_in_progress.Focus();
                //tb_ParScore.Visibility = Visibility.Hidden;
                //tb_ParScore_Text.Visibility = Visibility.Hidden;
            }
        }



    }
}