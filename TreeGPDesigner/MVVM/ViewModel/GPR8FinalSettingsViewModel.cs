using CommunityToolkit.Mvvm.Input;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TreeGPDesigner.MVVM.View;

namespace TreeGPDesigner.MVVM.ViewModel
{
    //Viewmodel class for Final Settings View
    public partial class GPR8FinalSettingsViewModel : ViewModelBase
    {
        //Commands
        public ICommand NavStartRunCommand { get; }
        public ICommand NavBackCommand { get; }

        //Constructor
        public GPR8FinalSettingsViewModel()
        {
            NavStartRunCommand = new RelayCommand(NavStartRun);
            NavBackCommand = new RelayCommand(NavBack);
        }

        //Navigation Functions
        public async void NavStartRun()
        {
            if (AppInfoSingleton.Instance.CurrentTemplate.CurrentPopulationCount < 10)
            {
                AppInfoSingleton.Instance.CurrentTemplate.CurrentPopulationCount = 10;
            }

            if (AppInfoSingleton.Instance.CurrentTemplate.CurrentMaxDepth < AppInfoSingleton.Instance.CurrentTemplate.CurrentMinDepth)
            {
                AppInfoSingleton.Instance.CurrentTemplate.CurrentMaxDepth = AppInfoSingleton.Instance.CurrentTemplate.CurrentMinDepth;
            }

            /*Application.Current.MainWindow.Hide();

            Thread newWindowThread = new Thread(new ThreadStart(() => LoadFunction()));
            newWindowThread.SetApartmentState(ApartmentState.STA);
            newWindowThread.IsBackground = true;
            newWindowThread.Start();*/



            /*Action<CancellationToken> action = (CancellationToken cancellationToken) =>
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    Trace.WriteLine("hello");
                }

                AppInfoSingleton.Instance.CurrentViewModel = new GPR9MainScreenViewModel();
            };*/

            /*AppInfoSingleton.LoadCancellationToken = AppInfoSingleton.LoadCancellationTokenSource.Token;

            Task task = LoadWindow(AppInfoSingleton.LoadCancellationToken);

            AppInfoSingleton.LoadTask = task;*/

            

            /*try
            {
                await task;
            }
            catch
            {
                Application.Current.MainWindow.Close();
            }*/
            
            AppInfoSingleton.Instance.CurrentViewModel = new GPR9MainScreenViewModel();
        }

       /* public static async Task LoadWindow(CancellationToken cancelToken)
        {
            if (cancelToken.IsCancellationRequested)
            {
                throw new TaskCanceledException();
            }

            AppInfoSingleton.Instance.CurrentViewModel = new GPR9MainScreenViewModel();
        }

        double windowWidth = Application.Current.MainWindow.Width;
        double windowHeight = Application.Current.MainWindow.Height;
        double windowXPositon = Application.Current.MainWindow.Left;
        double windowYPositon = Application.Current.MainWindow.Top;
        WindowState windowState = Application.Current.MainWindow.WindowState;

        public void LoadFunction()
        {
            LoadingWindow tempWindow = new();
            tempWindow.Width = windowWidth;
            tempWindow.Height = windowHeight;
            tempWindow.Left = windowXPositon;
            tempWindow.Top = windowYPositon;
            tempWindow.WindowState = windowState;
            AppInfoSingleton.LoadingWindow = tempWindow;
            tempWindow.Show();
            System.Windows.Threading.Dispatcher.Run();
        }*/

        public void NavBack()
        {
            AppInfoSingleton.Instance.CurrentViewModel = new GPR7SelectSelectionMethodViewModel();
        }
    }
}
