using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Xml.Serialization;

namespace Golf_App.Classes
{
    public static class StatsManager
    {
        //SaveGameHistory();             // Rework the way games are saved??

        public static List<string> GameFiles = new List<string>();
        public static ObservableCollection<Course> GameHistory = new ObservableCollection<Course>();
        public static Course ViewGameHistory;
        public static string GameFile;

        public static void ClearGameHistory()
        {
            GameHistory.Remove(ViewGameHistory);


            //GameFiles.Clear();
            //GameHistory.Clear();
            //ViewGameHistory = null;
            //GameFile = null;
        }

        public static void FindGameHistory()
        {
            GameHistory.Clear();

            //GameFiles.Clear();            
            var files = Directory.GetFiles(EnviromentManager.SavedGamesPath);
            foreach (string file in files)
            {
                ImportGameHistory(Path.GetFileName(file));


                //GameFiles.Add(Path.GetFileName(file));
            }         
        }

        public static void GetGameHistory()
        {
            GameHistory.Clear();
            foreach (var file in GameFiles)
            {
                ImportGameHistory(file);
            }
        }

        private static void ImportGameHistory(string filename)
        {
            try
            {
                if (File.Exists(EnviromentManager.SavedGamesPath + filename))
                {
                    Stream xmlInputStream = File.OpenRead(EnviromentManager.SavedGamesPath + filename);
                    XmlSerializer deserializer = new XmlSerializer(typeof(Course));
                    GameHistory.Add((Course)deserializer.Deserialize(xmlInputStream));
                    xmlInputStream.Close();
                }
            }
            catch (Exception)
            {
                //MessageBox.Show("Could not load Course data.\n" + e.Message);
            }
        }
    }
}