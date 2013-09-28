using FaceIn.Common;

using FaceIn.Model;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace FaceIn.ViewModels
{
    public class StudentCreateViewModel : ViewModelBase
    {
         
        public StudentCreateViewModel()
        {
            
        }

        private StudentModel _model;
        public StudentModel Model
        {
            get
            {
                return _model;
            }
            set
            {
                _model = value;
                OnPropertyChanged("Model");
            }
        }


        
    }

   
}
