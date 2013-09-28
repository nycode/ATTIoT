using System;
using FaceIn.Common;
using FaceIn.ViewModels;
using FaceIn.Model;

namespace FaceIn.Actions
{ 
    internal class StudentCreateAction : WorkflowAction, IDependsOn<StudentCreateViewModel>
    {
 
        private StudentCreateViewModel viewModel;
     
        public void Inject(StudentCreateViewModel model)
        {
            this.viewModel = model;
            
        }
         
        public void Process(StudentModel model)
        {
            this.viewModel.Model = model;
            this.MainViewModel.CurrentView = this.viewModel;
        }

        

    }
}
