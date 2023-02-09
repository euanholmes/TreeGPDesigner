using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using TreeGPDesigner.MVVM.ViewModel;

namespace TreeGPDesigner.MVVM.View
{
    /// <summary>
    /// Interaction logic for LoadingWindow.xaml
    /// </summary>
    public partial class LoadingWindow : Window
    {
        public LoadingWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //give this priority
            /*Application.Current.Dispatcher.Invoke( () =>
            {
                Application.Current.MainWindow.Hide();
            });*/

            //Application.Current.Dispatcher.Invoke(DispatcherPriority.Send, CancelLoading);

            //Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Send, new Action(() => CancelLoading()));
            //AppInfoSingleton.LoadCancellationTokenSource.Cancel();
            //System.Windows.Application.Current.Shutdown();
            //this.Close();
        }

        private void CancelLoading()
        {
            //Application.Current.MainWindow.Close();
            //AppInfoSingleton.Instance.CurrentViewModel = new GPR8FinalSettingsViewModel();
            //MainWindow mainWindow = new();
            //mainWindow.Show();
            //Application.Current.MainWindow.Close();
            //LoadingWindow loadwindow = new();
            //loadwindow.Show();
            //AppInfoSingleton.LoadCancellationTokenSource.Cancel();
        }
    }
}
