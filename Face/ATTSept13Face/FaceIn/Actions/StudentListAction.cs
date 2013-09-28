using System;
using FaceIn.Common;
using FaceIn.ViewModels;
using FaceIn.Model;

namespace FaceIn.Actions
{

    internal class StudentListAction : WorkflowAction, IDependsOn<StudentViewModel>
    {

        private StudentViewModel model;
         

        public void Inject(StudentViewModel modelParam)
        {
            this.model = modelParam;
              
        }

        public void Process()
        {
            this.MainViewModel.CurrentView = this.model;
        }

        
    }
}
