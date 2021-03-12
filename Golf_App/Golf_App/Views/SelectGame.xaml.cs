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


            lv_courses.ItemsSource = CoursesManager.Courses.Course;
        }

        private void lv_Courses_SelectionChanged(object sender, EventArgs e)
        {
            CoursesManager.SwitchSelectionAll(false);           // Clears selected course.
                        
            var accs = (sender as ListView).SelectedItem;




            //foreach (Course acc in accs)
            //{
            //    acc.IsEnabled = true;
            //}
        }

    }
}