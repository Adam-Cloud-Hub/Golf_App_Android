using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace Golf_App.Classes
{
    public static class CoursesManager
    {
        public static Courses Courses = new Courses();      // Holds data for all courses.
        public static Course SelectedCourse;                // Holds data for the selected course.

        public static void ImportCourses()
        {
            var assembly = typeof(App).Assembly;
            Stream xmlInputStream = assembly.GetManifestResourceStream("Golf_App.Resources.Courses.xml");
            XmlSerializer deserializer1 = new XmlSerializer(typeof(Courses));
            Courses = (Courses)deserializer1.Deserialize(xmlInputStream);
            xmlInputStream.Close();

            CourseImage();
        }

        private static void CourseImage()
        {
            foreach (var course in Courses.Course)
            {
                course.CourseImage = Regex.Replace(course.CourseName, @"\s+", "") + "_" + course.CourseImage;
            }
        }

        public static void LaunchGame()
        {
            GameManager.NewGame();
            GameManager.SaveCurrentGame();
        }

        public static void SwitchSelectionAll(bool active)
        {
            foreach (var course in Courses.Course)
            {
                course.IsEnabled = active;
            }
        }
    }

    [XmlRoot("Courses")]
    public class Courses
    {
        [XmlElement("Course")]
        public Course[] Course { get; set; }
    }

    public class Course
    {
        [XmlElement("CourseName")]
        public string CourseName { get; set; }

        [XmlElement("CourseImage")]
        public string CourseImage { get; set; }

        [XmlElement("FileName")]
        public string FileName { get; set; }

        [XmlElement("IsEnabled")]
        public string BackingIsEnabled
        {
            set
            {
                IsEnabled = bool.Parse(value.ToLower());
            }
            get
            {
                return IsEnabled.ToString();
            }
        }
        [XmlIgnore]
        public bool IsEnabled { get; set; }

        [XmlElement("GPS_Longitude")]
        public string GPS_Longitude { get; set; }

        [XmlElement("GPS_Latitude")]
        public string GPS_Latitude { get; set; }

        [XmlElement("UserHandicap")]
        public int UserHandicap { get; set; }

        [XmlElement("TotalScore")]
        public int TotalScore { get; set; }

        [XmlElement("GameDate")]
        public string GameDate { get; set; }

        [XmlElement("GameTime")]
        public string GameTime { get; set; }

        [XmlArray("CourseHoles"), XmlArrayItem("Hole")]
        public Hole[] CourseHoles { get; set; }
    }

    [XmlRoot("Hole")]
    public class Hole
    {
        [XmlElement("HoleNumber")]
        public int HoleNumber { get; set; }

        [XmlElement("HolePar")]
        public int HolePar { get; set; }

        [XmlElement("HoleDistance")]
        public int HoleDistance { get; set; }

        [XmlElement("HoleStrokeIndex")]
        public int HoleStrokeIndex { get; set; }
              
        [XmlElement("HoleScore")]
        public int HoleScore { get; set; }

        [XmlElement("HoleParScore")]
        public string HoleParScore { get; set; }
    }
}