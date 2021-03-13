using Golf_App.Classes;
using System.IO;
using System.Xml.Serialization;

namespace Golf_App
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Startup();

            CurrentItem.CurrentItem = CurrentItem.Items[1];

            //CurrentItem = new Views.Home();
        }

        private void Startup()
        {
            EnviromentManager.DirectorySetup();
            CoursesManager.ImportCourses();

            for (int i = 0; i < 13; i++)
            {
                GameManager.UserScoreValues.Add(new Hole() { HoleNumber = i });
            }

            //Mainwin_LoadSetup();
        }

    }
}
