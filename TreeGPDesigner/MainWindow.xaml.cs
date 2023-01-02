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
            AppInfoSingleton.Instance.CurrentBackground = Brushes.White;


            //Trying out gradient brushes
            LinearGradientBrush LightBackground = new();
            LightBackground.StartPoint = new Point(0, 0);
            LightBackground.EndPoint = new Point(1, 1);
            LightBackground.GradientStops.Add(new GradientStop(Colors.White, 0.0));
            //LightBackground.GradientStops.Add(new GradientStop(Colors.GhostWhite, 0.5));
            LightBackground.GradientStops.Add(new GradientStop(Colors.WhiteSmoke, 1.0));

            //AppInfoSingleton.Instance.CurrentBackground = Brushes.White;
            AppInfoSingleton.Instance.CurrentBackground = LightBackground;

            AppInfoSingleton.Instance.CurrentText = Brushes.Black;
            AppInfoSingleton.Instance.CurrentRadioButtonCheck = 1;
            DataContext = new MainWindowViewModel();
        }
    }
}
