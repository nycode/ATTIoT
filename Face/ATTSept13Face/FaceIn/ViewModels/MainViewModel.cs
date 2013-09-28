
using FaceIn.Common;

namespace FaceIn.ViewModels
{
     
    public class MainViewModel : ViewModelBase
    {
        
 
        private ViewModelBase currentView;

      
        public ViewModelBase CurrentView
        {
            get
            {
                return this.currentView;
            }

            set
            {
                this.currentView = value;
                this.OnPropertyChanged("CurrentView");
            }
        }

       
    }
}
