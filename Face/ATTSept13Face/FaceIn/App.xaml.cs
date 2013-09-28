using FaceIn.Actions;
using FaceIn.Model;
using FaceIn.ViewModels;
using System;
using System.Threading;
using System.Windows;

namespace FaceIn
{
    
    public partial class App : Application
    {

        private StudentViewModel model;

        protected override void OnStartup(StartupEventArgs e)
        {
            
            Thread.CurrentThread.Name = "MainThread";

            base.OnStartup(e);
 
            this.DispatcherUnhandledException += new System.Windows.Threading.DispatcherUnhandledExceptionEventHandler(this.App_DispatcherUnhandledException);

            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(this.CurrentDomain_UnhandledException);

          
            var mainViewModel = new MainViewModel();
            var mainWindow = new MainWindow();
 
            //mainWindow.Inject(mainViewModel);
            //this.MainWindow = mainWindow;

            var createModel = new StudentCreateViewModel();
            model = new StudentViewModel();
            var listAction = new StudentListAction();
            var createAction = new StudentCreateAction();
            listAction.Inject(mainViewModel);
            listAction.Inject(model);
            createAction.Inject(mainViewModel);
            createAction.Inject(createModel);
            
            listAction.Process();
             

            this.MainWindow.Show();


        }

        private static void HandleException(Exception ex)
        {
           
        }
 
        private void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            HandleException(e.Exception);
            e.Handled = true;
        }

         
        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            HandleException((Exception)e.ExceptionObject);
        }
    }
}
