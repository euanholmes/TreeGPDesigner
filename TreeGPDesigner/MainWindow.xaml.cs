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
            //Initialise window and load home view.
            InitializeComponent();
            AppInfoSingleton.Instance.CurrentViewModel = new HomeViewModel();

            //Brush Settings
            if (Properties.Settings.Default.SettingsRadioButton == 1)
            {
                ChangeBrushes(AppInfoSingleton.BrushSet1);
            }
            else if (Properties.Settings.Default.SettingsRadioButton == 2)
            {
                ChangeBrushes(AppInfoSingleton.BrushSet2);
            }
            else if (Properties.Settings.Default.SettingsRadioButton == 3)
            {
                ChangeBrushes(AppInfoSingleton.BrushSet3);
            }
            else if (Properties.Settings.Default.SettingsRadioButton == 4)
            {
                ChangeBrushes(AppInfoSingleton.BrushSet4);
            }
            else if (Properties.Settings.Default.SettingsRadioButton == 5)
            {
                ChangeBrushes(AppInfoSingleton.BrushSet5);
            }
            else if (Properties.Settings.Default.SettingsRadioButton == 6)
            {
                ChangeBrushes(AppInfoSingleton.BrushSet6);
            }

            //Dark or Light Mode Settings
            if (Properties.Settings.Default.SettingsMode)
            {
                AppInfoSingleton.Instance.CurrentModeToggleButton = new BitmapImage(new Uri("pack://application:,,,/Images/light-mode-toggle-icon.png"));
                AppInfoSingleton.Instance.CurrentZoomIcon = new BitmapImage(new Uri("pack://application:,,,/Images/zoom-pan-icon.png"));
                AppInfoSingleton.Instance.CurrentBackground = AppInfoSingleton.LightBackground;
                AppInfoSingleton.Instance.CurrentText = Brushes.Black;
                AppInfoSingleton.Instance.CurrentPanel1Color = AppInfoSingleton.LightPanel1;
                AppInfoSingleton.Instance.CurrentPanel2Color = AppInfoSingleton.LightPanel2;
                AppInfoSingleton.Instance.CurrentNormalButtonColor = AppInfoSingleton.LightNormalButton;
                AppInfoSingleton.Instance.CurrentNavButtonColor = AppInfoSingleton.LightNavButton;
            }
            else
            {
                AppInfoSingleton.Instance.CurrentModeToggleButton = new BitmapImage(new Uri("pack://application:,,,/Images/dark-mode-toggle-icon-inverted.png"));
                AppInfoSingleton.Instance.CurrentZoomIcon = new BitmapImage(new Uri("pack://application:,,,/Images/zoom-pan-icon-inverted.png"));
                AppInfoSingleton.Instance.CurrentBackground = AppInfoSingleton.DarkBackground;
                AppInfoSingleton.Instance.CurrentText = Brushes.White;
                AppInfoSingleton.Instance.CurrentPanel1Color = AppInfoSingleton.DarkPanel1;
                AppInfoSingleton.Instance.CurrentPanel2Color = AppInfoSingleton.DarkPanel2;
                AppInfoSingleton.Instance.CurrentNormalButtonColor = AppInfoSingleton.DarkNormalButton;
                AppInfoSingleton.Instance.CurrentNavButtonColor = AppInfoSingleton.DarkNavButton;
            }

            //Data Context
            DataContext = new MainWindowViewModel();
        }

        public void ChangeBrushes(Brush[] brushSet)
        {
            AppInfoSingleton.Instance.CurrentFunctionNodeOutlineBrush = brushSet[0];
            AppInfoSingleton.Instance.CurrentFunctionNodeBackgroundBrush = brushSet[1];
            AppInfoSingleton.Instance.CurrentTerminalNodeOutlineBrush = brushSet[2];
            AppInfoSingleton.Instance.CurrentTerminalNodeBackgroundBrush = brushSet[3];
        }
    }
}
