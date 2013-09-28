using Emgu.CV;
using Emgu.CV.Structure;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;


namespace FaceIn.Model
{ 
     
    public class StudentListModel : List<StudentModel>
    {  
        public StudentListModel()
        { 
        }  
    }

    public static class StudentLoader
    {
        private static string startupPath = Environment.CurrentDirectory;


        static StudentListModel _cstudents;
        public static StudentListModel StudentsCheckedIn
        {
            get
            {
                if (_cstudents == null)
                {
                    _cstudents = new StudentListModel();
                }
                return _cstudents;
            }
        }

        static StudentListModel _students;
        public static StudentListModel Students
        {
            get
            {
                if(_students == null)
                {
                    _students = new StudentListModel();
                }
                return _students;
            }
        }

        public static void Save()
        {
            var content = JsonConvert.SerializeObject(Students);
            File.WriteAllText(startupPath + @"\data.txt", content);
        }

        public static void Load()
        {
            if (File.Exists(startupPath + @"\data.txt"))
            {
                var data = File.ReadAllText(startupPath + @"\data.txt");
                var content = JsonConvert.DeserializeObject<StudentListModel>(data);
                _students = content;
            }
            
        }

       static List<Image<Gray, byte>> trainingImages = new List<Image<Gray, byte>>();

        public static void LoadImages()
        {

            foreach (var itemLoop in StudentLoader.Students)
            {
                var LoadFaces = "sface" + itemLoop.Id + ".bmp";
                trainingImages.Add(new Image<Gray, byte>(startupPath + "/TrainedFaces/" + LoadFaces));
            }

        }

        public static List<Image<Gray, byte>> Images
        {
            get
            {
               
                return trainingImages;
            }
        }

    }
}
