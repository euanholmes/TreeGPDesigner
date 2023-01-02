using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TreeGPDesigner.MVVM.ViewModel;

namespace TreeGPDesigner
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            AppInfoSingleton.Instance.CurrentViewModel = new HomeViewModel();
            AppInfoSingleton.Instance.CurrentFunctionNodeOutlineBrush = Brushes.Red;
            AppInfoSingleton.Instance.CurrentFunctionNodeBackgroundBrush = Brushes.Pink;
            AppInfoSingleton.Instance.CurrentTerminalNodeOutlineBrush = Brushes.Blue;
            AppInfoSingleton.Instance.CurrentTerminalNodeBackgroundBrush = Brushes.LightBlue;
            AppInfoSingleton.Instance.CurrentModeToggleButton = new BitmapImage(new Uri("pack://application:,,,/Images/light-mode-toggle-icon.png"));
            AppInfoSingleton.Instance.CurrentBackground = AppInfoSingleton.LightBackground;
            AppInfoSingleton.Instance.CurrentText = Brushes.Black;
            AppInfoSingleton.Instance.CurrentRadioButtonCheck = 1;
            AppInfoSingleton.Instance.CurrentPanel1Color = AppInfoSingleton.LightPanel1;
            AppInfoSingleton.Instance.CurrentNormalButtonColor = AppInfoSingleton.LightNormalButton;
            AppInfoSingleton.Instance.CurrentNavButtonColor = AppInfoSingleton.LightNavButton;
            DataContext = new MainWindowViewModel();
        }
    }
}
