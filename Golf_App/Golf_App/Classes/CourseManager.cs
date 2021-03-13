using System;
using System.IO;
using System.Xml.Serialization;

namespace Golf_App.Classes
{
    public static class CoursesManager
    {
        public static Courses Courses = new Courses();
        public static Course SelectedCourse;



        public static void ImportCourses()
        {
            var assembly = typeof(App).Assembly;
            Stream xmlInputStream = assembly.GetManifestResourceStream("Golf_App.Resources.Courses.xml");
            XmlSerializer deserializer1 = new XmlSerializer(typeof(Courses));
            Courses = (Courses)deserializer1.Deserialize(xmlInputStream);
            xmlInputStream.Close();

            //CourseImage();
        }

        private static void CourseImage()
        {
            foreach (var course in Courses.Course)
            {
                //var assembly = typeof(App).Assembly;
                //Stream Stream = assembly.GetManifestResourceStream("Golf_App.Resources.Courses." + course.CourseName + "." + course.CourseImage);
                //string holeimage = Stream.ToString();

                //string holeimage = EnviromentManager.CousredataPath + course.CourseName + "/" + course.CourseImage;

                course.CourseImage = "Golf_App.Resources.Courses." + course.CourseName + "." + course.CourseImage;

                //if (File.Exists(holeimage))
                //{
                //    course.CourseImage = holeimage;
                //}
            }
        }

        public static void LaunchGame()
        {
            //LoadSelectedCourse();
            GameManager.NewGame();
            GameManager.SaveCurrentGame();
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

        [XmlElement("HoleScore")]
        public int HoleScore { get; set; }

        [XmlElement("HoleParScore")]
        public string HoleParScore { get; set; }
    }
}