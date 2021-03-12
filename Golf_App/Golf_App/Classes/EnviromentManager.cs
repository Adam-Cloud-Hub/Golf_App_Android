using System;
using System.IO;
using Xamarin.Forms;

namespace Golf_App.Classes
{
    public static class EnviromentManager
    {
        public static string CurrentGamePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Golf App\Saved Games\Temp\";
        public static string SavedGamesPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Golf App\Saved Games\";


        //public static string CurrentGamePath = Directory.GetCurrentDirectory() + @"..\..\..\Saved Games\Temp\";
        //public static string SavedGamesPath = Directory.GetCurrentDirectory() + @"..\..\..\Saved Games\";



        public static string SaveCurrentGame = CurrentGamePath + "CurrentGame.xml";

        //public static string AccPath = AppdataPath + "Courses.xml";

        public static string CousredataPath = Directory.GetCurrentDirectory() + @"..\..\..\Resources\Courses\";

        public static string AppdataPath = Directory.GetCurrentDirectory() + @"..\..\..\Data\";
        public static string Courses = AppdataPath + "Courses.xml";




        public static void DirectorySetup()
        {

            if (!Directory.Exists(CurrentGamePath)) DependencyService.Get<IDirectory>().CreateDirectory(CurrentGamePath);


            //if (!Directory.Exists(CurrentGamePath)) Directory.CreateDirectory(CurrentGamePath);
        }


        public interface IDirectory
        {
            string CreateDirectory(string directoryName);
            void RemoveDirectory();
            string RenameDirectory(string oldDirectoryName, string newDirectoryName);
        }
    }
}