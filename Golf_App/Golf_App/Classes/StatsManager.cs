using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Xml.Serialization;

namespace Golf_App.Classes
{
    public static class StatsManager
    {
        //SaveGameHistory();             // Rework the way games are saved??

        public static List<string> GameFiles = new List<string>();
        public static List<Course> GameHistory = new List<Course>();
        public static Course ViewGameHistory;
        public static string GameFile;

        public static void ClearGameHistory()
        {
            GameFiles.Clear();
            GameHistory.Clear();
            ViewGameHistory = null;
            GameFile = null;
        }

        public static void SetViewGameHistory()
        {
            ViewGameHistory.IsEnabled = true;
            
            for (int i = 0; i < GameHistory.Count; i++)
            {
                if (GameHistory[i].IsEnabled == true)
                {
                    GameFile = GameFiles[i];
                }
            }
        }

        public static void FindGameHistory()
        {
            GameFiles.Clear();
            
            var files = Directory.GetFiles(EnviromentManager.SavedGamesPath);
            foreach (string file in files)
            {
                GameFiles.Add(Path.GetFileName(file));
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

        public static void SwitchSelectionAll(bool active)
        {
            foreach (var course in GameHistory)
            {
                course.IsEnabled = active;
            }
        }
    }
}