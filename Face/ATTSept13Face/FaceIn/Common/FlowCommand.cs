using System;
using System.Windows.Input;

namespace FaceIn.Common
{
    
    public class FlowCommand : ICommand, IFlowCommand
    {
      
        private readonly Predicate<object> canExecute;

       
        public FlowCommand()
        {
        }

        
        public FlowCommand(Predicate<object> canExecute)
        {
            this.canExecute = canExecute;
        }

      
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

      
        public event Action ExecuteAction = delegate { };
          
      
        public void Execute(object parameter)
        {
            this.ExecuteAction();
        }

       
        public bool CanExecute(object parameter)
        {
            return this.canExecute == null ? true : this.canExecute(parameter);
        }
    }
}
