using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Xml.Serialization;

namespace Golf_App.Classes
{
    public static class GameManager
    {
        public static Course CurrentGame = new Course();
        public static List<Hole> UserScoreValues = new List<Hole>();

        public static int CurrentHoleNumber = 0;
        public static int HoleNumber = 0;
        public static int ParNumber = 0;
        public static int ScoreNumber = 0;
        public static int UserHandicap = 0;
        public static int ScoreFirstNine = 0;
        public static int ScoreParFirstNine = 0;
        public static int ScoreTotal = 0;
        public static int ScoreParTotal = 0;

        public static void ClearCurrentGame()
        {
            CoursesManager.SwitchSelectionAll(false);

            CurrentHoleNumber = 0;
            HoleNumber = 0;
            ParNumber = 0;
            ScoreNumber = 0;
        }

        public static void NewGame()
        {
            CurrentGame = CoursesManager.SelectedCourse;
        }

        public static void CurrentGameScoreFirstNine()
        {
            ScoreFirstNine = 0;
            ScoreParFirstNine = 0;
            for (int i = 0; i < 9; i++)
            {
                ScoreFirstNine += CurrentGame.CourseHoles[i].HoleScore;
                ScoreParFirstNine += CurrentGame.CourseHoles[i].HolePar;
            }
        }

        public static void CurrentGameScoreTotal()
        {
            ScoreTotal = 0;
            ScoreParTotal = 0;
            for (int i = 0; i < CurrentGame.CourseHoles.Length; i++)
            {
                ScoreTotal += CurrentGame.CourseHoles[i].HoleScore;
                ScoreParTotal += CurrentGame.CourseHoles[i].HolePar;
            }
            CurrentGame.TotalScore = ScoreTotal;
            CurrentGame.ParofCourse = ScoreParTotal;
        }

        public static string CurrentGameSummary(int ScorePar, int Score)
        {
            string parshots;
            if (Score < ScorePar)
            {
                parshots = "You Were " + (ScorePar - Score).ToString() + " Under Par.";
            }
            else if (Score == ScorePar)
            {
                parshots = "You Matched The Course Par.";
            }
            else
            {
                parshots = "You Were " + (Score - ScorePar).ToString() + " Over Par.";
            }
            return parshots;
        }

        public static string GameSummary(int start, int hole)
        {
            string Summary = "";
            for (int i = start; i < hole; i++)
            {
                Summary += Environment.NewLine + "Hole " + (i + 1).ToString() + "  Par " + GameManager.CurrentGame.CourseHoles[i].HolePar + "  Score " + GameManager.CurrentGame.CourseHoles[i].HoleScore + "   " + GameManager.CurrentGame.CourseHoles[i].HoleParScore;
            }
            return Summary;
        }

        public static void UpdateGame()
        {
            for (int i = 0; i < CurrentGame.CourseHoles.Length; i++)
            {
                if (CurrentGame.CourseHoles[i].HoleNumber == HoleNumber)
                {
                    CurrentGame.CourseHoles[i].HoleScore = ScoreNumber;
                }
            }
            SaveCurrentGame();
        }

        // Saves current game, allows user to come back if they havn't finished.
        public static void SaveCurrentGame()
        {
            if (File.Exists(EnviromentManager.SaveCurrentGame)) File.Delete(EnviromentManager.SaveCurrentGame);     // Removes past Current game saved data. 

            CurrentGame.GameDate = DateTime.Now.Date.ToString("dd/MM/yyyy");
            CurrentGame.GameTime = DateTime.Now.ToString("HH:mm tt");
            CurrentGameScoreTotal();

            XmlSerializer writer = new XmlSerializer(CurrentGame.GetType());
            FileStream file = File.Create(EnviromentManager.SaveCurrentGame);
            writer.Serialize(file, CurrentGame);
            file.Close();
        }

        public static void SaveGame()
        {
            int filenumber = 1;
            string filename;
            bool filesaved = false;

            while (filesaved == false)
            {
                filename = "Game" + filenumber.ToString() + ".xml";

                if (!File.Exists(EnviromentManager.SavedGamesPath + filename))
                {
                    CurrentGame.FileName = filename;
                    XmlSerializer writer = new XmlSerializer(CurrentGame.GetType());
                    FileStream file = File.Create(EnviromentManager.SavedGamesPath + filename);
                    writer.Serialize(file, CurrentGame);
                    file.Close();
                    filesaved = true;
                }
                else
                {
                    filenumber++;
                    filesaved = false;
                }
            }
            File.Delete(EnviromentManager.SaveCurrentGame);     // Removes past game 
        }

        // Picks up were user left off or loads in past game.
        public static void LoadPastGame()
        {
            ImportGame();
            CoursesManager.SelectedCourse = CurrentGame;
            CurrentGame.IsEnabled = true;
        }

        public static void ImportGame()
        {
            try
            {
                if (File.Exists(EnviromentManager.SaveCurrentGame))
                {
                    Stream xmlInputStream = File.OpenRead(EnviromentManager.SaveCurrentGame);
                    XmlSerializer deserializer = new XmlSerializer(typeof(Course));
                    CurrentGame = (Course)deserializer.Deserialize(xmlInputStream);
                    xmlInputStream.Close();
                }
            }
            catch (Exception)
            {              
            }
        }

        public static void ScoreTermGame()
        {
            for (int i = 0; i < CurrentGame.CourseHoles.Length; i++)
            {
                if (CurrentGame.CourseHoles[i].HoleNumber == HoleNumber)
                {
                    int FindPar = (ScoreNumber - ParNumber);

                    if (ScoreNumber == 1)
                    {
                        CurrentGame.CourseHoles[i].HoleParScore = "Ace";
                        break;
                    }
                    else if (FindPar >= 4)
                    {
                        CurrentGame.CourseHoles[i].HoleParScore = "Max";
                        break;
                    }
                    else if (FindPar <= -4 || ScoreNumber == 0)
                    {
                        CurrentGame.CourseHoles[i].HoleParScore = "-";
                        break;
                    }
                    switch (FindPar)
                    {
                        case -3:
                            CurrentGame.CourseHoles[i].HoleParScore = "Albatross";
                            break;
                        case -2:
                            CurrentGame.CourseHoles[i].HoleParScore = "Eagle";
                            break;
                        case -1:
                            CurrentGame.CourseHoles[i].HoleParScore = "Birde";
                            break;
                        case 0:
                            CurrentGame.CourseHoles[i].HoleParScore = "Par";
                            break;
                        case 1:
                            CurrentGame.CourseHoles[i].HoleParScore = "Bogey";
                            break;
                        case 2:
                            CurrentGame.CourseHoles[i].HoleParScore = "Double Bogey";
                            break;
                        case 3:
                            CurrentGame.CourseHoles[i].HoleParScore = "Triple Bogey";
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }
}