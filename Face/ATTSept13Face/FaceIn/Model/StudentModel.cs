using FaceIn.Common;
using FaceIn.ViewModels;
using System;


namespace FaceIn.Model
{ 
     
    public class StudentModel : ModelBase
    { 
        public string Name { get; set; }
        public string Email { get; set; }
        public string Id { get; set; }
       
        public StudentModel()
        { 
        } 

    }
}
