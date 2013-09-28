using FaceIn.Common;
using FaceIn.ViewModels;

namespace FaceIn.Actions
{
    
    internal abstract class WorkflowAction : IDependsOn<MainViewModel>
    {
       
        protected MainViewModel MainViewModel { get; private set; }

         
        public void Inject(MainViewModel mainViewModel)
        {
            this.MainViewModel = mainViewModel;
        }
    }
}
