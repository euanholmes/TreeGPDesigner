using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using TreeGPDesigner.MVVM.Model;
using TreeGPDesigner.MVVM.ViewModel;

namespace TreeGPDesigner
{
    //Code behind for Main Window
    public partial class MainWindow : Window
    {
        //Constructor
        public MainWindow()
        {
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
                AppInfoSingleton.Instance.CurrentBrushSet1 = AppInfoSingleton.BrushSet1;
                AppInfoSingleton.Instance.CurrentBrushSet2 = AppInfoSingleton.BrushSet2;
                AppInfoSingleton.Instance.CurrentBrushSet3 = AppInfoSingleton.BrushSet3;
                AppInfoSingleton.Instance.CurrentBrushSet4 = AppInfoSingleton.BrushSet4;
                AppInfoSingleton.Instance.CurrentBrushSet5 = AppInfoSingleton.BrushSet5;
                AppInfoSingleton.Instance.CurrentBrushSet6 = AppInfoSingleton.BrushSet6;
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
                AppInfoSingleton.Instance.CurrentBrushSet1 = AppInfoSingleton.BrushSet1Dark;
                AppInfoSingleton.Instance.CurrentBrushSet2 = AppInfoSingleton.BrushSet2Dark;
                AppInfoSingleton.Instance.CurrentBrushSet3 = AppInfoSingleton.BrushSet3Dark;
                AppInfoSingleton.Instance.CurrentBrushSet4 = AppInfoSingleton.BrushSet4Dark;
                AppInfoSingleton.Instance.CurrentBrushSet5 = AppInfoSingleton.BrushSet5Dark;
                AppInfoSingleton.Instance.CurrentBrushSet6 = AppInfoSingleton.BrushSet6Dark;
            }

            //Brush Settings
            if (Properties.Settings.Default.SettingsRadioButton == 1)
            {
                AppInfoSingleton.Instance.CurrentBrushSet = AppInfoSingleton.Instance.CurrentBrushSet1;
            }
            else if (Properties.Settings.Default.SettingsRadioButton == 2)
            {
                AppInfoSingleton.Instance.CurrentBrushSet = AppInfoSingleton.Instance.CurrentBrushSet2;
            }
            else if (Properties.Settings.Default.SettingsRadioButton == 3)
            {
                AppInfoSingleton.Instance.CurrentBrushSet = AppInfoSingleton.Instance.CurrentBrushSet3;
            }
            else if (Properties.Settings.Default.SettingsRadioButton == 4)
            {
                AppInfoSingleton.Instance.CurrentBrushSet = AppInfoSingleton.Instance.CurrentBrushSet4;
            }
            else if (Properties.Settings.Default.SettingsRadioButton == 5)
            {
                AppInfoSingleton.Instance.CurrentBrushSet = AppInfoSingleton.Instance.CurrentBrushSet5;
            }
            else if (Properties.Settings.Default.SettingsRadioButton == 6)
            {
                AppInfoSingleton.Instance.CurrentBrushSet = AppInfoSingleton.Instance.CurrentBrushSet6;
            }

            //Code to load asynchronous functions early. Increases initial load time but removes load time of adding first custom node.
            BinPackingTemplate bpTemplate = new BinPackingTemplate();
            bpTemplate.AddCustomFunctionNode("X", 2, "Load", "a => a[0] + a[1]");

            InitializeComponent();
            DataContext = new MainWindowViewModel();
            AppInfoSingleton.Instance.CurrentViewModel = new HomeViewModel();
        }
    }
}
