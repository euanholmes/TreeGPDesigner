using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using TreeGPDesigner.MVVM.Model;

namespace TreeGPDesigner.MVVM.ViewModel
{
    public partial class HomeViewModel : ObservableObject, INotifyPropertyChanged
    {
        //Variables in every viewmodel.
        public new event PropertyChangedEventHandler? PropertyChanged;

        public Brush? TextColour => AppInfoSingleton.Instance.CurrentText;
        private void OnCurrentTextChanged()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TextColour)));
        }

        public Brush? Panel1Colour => AppInfoSingleton.Instance.CurrentPanel1Color;
        private void OnCurrentPanel1ColourChanged()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Panel1Colour)));
        }

        public Brush? Panel2Colour => AppInfoSingleton.Instance.CurrentPanel2Color;
        private void OnCurrentPanel2ColourChanged()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Panel2Colour)));
        }

        public Brush? NormalButtonColour => AppInfoSingleton.Instance.CurrentNormalButtonColor;
        private void OnCurrentNormalButtonColourChanged()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(NormalButtonColour)));
        }

        public Brush? NavButtonColour => AppInfoSingleton.Instance.CurrentNavButtonColor;
        private void OnCurrentNavButtonColourChanged()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(NavButtonColour)));
        }


        //Tree Drawing Variables.
        public Brush? FunctionNodeOutlineBrush => AppInfoSingleton.Instance.CurrentFunctionNodeOutlineBrush;
        private void OnCurrentFunctionNodeOutlineBrushChanged()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FunctionNodeOutlineBrush)));
        }

        public Brush? FunctionNodeBackgroundBrush => AppInfoSingleton.Instance.CurrentFunctionNodeBackgroundBrush;
        private void OnCurrentFunctionNodeBackgroundBrushChanged()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FunctionNodeBackgroundBrush)));
        }

        public Brush? TerminalNodeOutlineBrush => AppInfoSingleton.Instance.CurrentTerminalNodeOutlineBrush;
        private void OnCurrentTerminalNodeOutlineBrushChanged()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TerminalNodeOutlineBrush)));
        }

        public Brush? TerminalNodeBackgroundBrush => AppInfoSingleton.Instance.CurrentTerminalNodeBackgroundBrush;
        private void OnCurrentTerminalNodeBackgroundBrushChanged()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TerminalNodeBackgroundBrush)));
        }

        public ImageSource? ZoomIconSource => AppInfoSingleton.Instance.CurrentZoomIcon;
        private void OnCurrentZoomIconChanged()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ZoomIconSource)));
        }

        [ObservableProperty]
        public ObservableCollection<NodePlot> displayTreePlot = new();

        [ObservableProperty]
        private float? canvasHeight;

        [ObservableProperty]
        private float? canvasWidth;

        //View Model Specific Variables.
        public ImageSource? ToggleButtonSource => AppInfoSingleton.Instance.CurrentModeToggleButton;
        private void OnCurrentToggleModeButtonChanged()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ToggleButtonSource)));
        }

        public ICommand NavTutorialsMenuCommand { get; }
        public ICommand ToggleButtonCommand { get; }
        public ICommand RadioButton1Command { get; }
        public ICommand RadioButton2Command { get; }
        public ICommand RadioButton3Command { get; }
        public ICommand RadioButton4Command { get; }
        public ICommand RadioButton5Command { get; }
        public ICommand RadioButton6Command { get; }

        public Node DisplayTree = new FunctionNode("Euan", 2, a => a[0] <= a[1] ? 1 : 0, true);
        

        public HomeViewModel()
        {
            //Variables in every viewmodel.
            AppInfoSingleton.Instance.CurrentTextChanged += OnCurrentTextChanged;
            AppInfoSingleton.Instance.CurrentPanel1ColorChanged += OnCurrentPanel1ColourChanged;
            AppInfoSingleton.Instance.CurrentPanel2ColorChanged += OnCurrentPanel2ColourChanged;
            AppInfoSingleton.Instance.CurrentNormalButtonColorChanged += OnCurrentNormalButtonColourChanged;
            AppInfoSingleton.Instance.CurrentNavButtonColorChanged += OnCurrentNavButtonColourChanged;

            //Tree Drawing Variables.
            AppInfoSingleton.Instance.CurrentFunctionNodeOutlineBrushChanged += OnCurrentFunctionNodeOutlineBrushChanged;
            AppInfoSingleton.Instance.CurrentFunctionNodeBackgroundBrushChanged += OnCurrentFunctionNodeBackgroundBrushChanged;
            AppInfoSingleton.Instance.CurrentTerminalNodeOutlineBrushChanged += OnCurrentTerminalNodeOutlineBrushChanged;
            AppInfoSingleton.Instance.CurrentTerminalNodeBackgroundBrushChanged += OnCurrentTerminalNodeBackgroundBrushChanged;
            AppInfoSingleton.Instance.CurrentZoomIconChanged += OnCurrentZoomIconChanged;

            //Viewmodel Specific variables.
            AppInfoSingleton.Instance.CurrentModeToggleButtonChanged += OnCurrentToggleModeButtonChanged;

            NavTutorialsMenuCommand = new RelayCommand(NavTutorialsMenu);
            ToggleButtonCommand = new RelayCommand(ToggleButtonLightDarkMode);
            RadioButton1Command = new RelayCommand(RadioButton1);
            RadioButton2Command = new RelayCommand(RadioButton2);
            RadioButton3Command = new RelayCommand(RadioButton3);
            RadioButton4Command = new RelayCommand(RadioButton4);
            RadioButton5Command = new RelayCommand(RadioButton5);
            RadioButton6Command = new RelayCommand(RadioButton6);

            DisplayTree.ChildNodes.Add(new FunctionNode("Hol", 2, a => a[0] <= a[1] ? 1 : 0, true));
            DisplayTree.ChildNodes.Add(new TerminalNode("mes", 0, 0, false));
            DisplayTree.ChildNodes[0].ChildNodes.Add(new TerminalNode("4045", 0, 0, false));
            DisplayTree.ChildNodes[0].ChildNodes.Add(new TerminalNode("4766", 0, 0, false));;

            TreeDrawingAlgorithm.CalculateNodePositions(DisplayTree);
            DisplayTreePlot = AppInfoSingleton.GetTreePlot(DisplayTreePlot, DisplayTree, FunctionNodeOutlineBrush, FunctionNodeBackgroundBrush,
                TerminalNodeOutlineBrush, TerminalNodeBackgroundBrush);

            CanvasHeight = DisplayTree.Height * 100 / (float)0.75; 
            CanvasWidth = DisplayTree.Width * 100 / (float)0.75;
        }

        public void ToggleButtonLightDarkMode()
        {
            if (Properties.Settings.Default.SettingsMode)
            {
                AppInfoSingleton.Instance.CurrentModeToggleButton = new BitmapImage(new Uri("pack://application:,,,/Images/dark-mode-toggle-icon-inverted.png"));
                AppInfoSingleton.Instance.CurrentZoomIcon = new BitmapImage(new Uri("pack://application:,,,/Images/zoom-pan-icon-inverted.png"));
                AppInfoSingleton.Instance.CurrentBackground = AppInfoSingleton.DarkBackground;
                AppInfoSingleton.Instance.CurrentText = Brushes.White;
                AppInfoSingleton.Instance.CurrentPanel1Color = AppInfoSingleton.DarkPanel1;
                AppInfoSingleton.Instance.CurrentPanel2Color = AppInfoSingleton.DarkPanel2;
                AppInfoSingleton.Instance.CurrentNormalButtonColor = AppInfoSingleton.DarkNormalButton;
                AppInfoSingleton.Instance.CurrentNavButtonColor = AppInfoSingleton.DarkNavButton;
                Properties.Settings.Default.SettingsMode = false;
                Properties.Settings.Default.Save();
            }
            else
            {
                AppInfoSingleton.Instance.CurrentModeToggleButton = new BitmapImage(new Uri("pack://application:,,,/Images/light-mode-toggle-icon.png"));
                AppInfoSingleton.Instance.CurrentZoomIcon = new BitmapImage(new Uri("pack://application:,,,/Images/zoom-pan-icon.png"));
                AppInfoSingleton.Instance.CurrentBackground = AppInfoSingleton.LightBackground;
                AppInfoSingleton.Instance.CurrentText = Brushes.Black;
                AppInfoSingleton.Instance.CurrentPanel1Color = AppInfoSingleton.LightPanel1;
                AppInfoSingleton.Instance.CurrentPanel2Color = AppInfoSingleton.LightPanel2;
                AppInfoSingleton.Instance.CurrentNormalButtonColor = AppInfoSingleton.LightNormalButton;
                AppInfoSingleton.Instance.CurrentNavButtonColor = AppInfoSingleton.LightNavButton;
                Properties.Settings.Default.SettingsMode = true;
                Properties.Settings.Default.Save();
            }
        }

        public void NavTutorialsMenu()
        {
            AppInfoSingleton.Instance.CurrentViewModel = new TutorialsMenuViewModel();
        }

        public void RadioButton1()
        {
            ChangeBrushes(AppInfoSingleton.BrushSet1);
            Properties.Settings.Default.SettingsRadioButton = 1;
            Properties.Settings.Default.Save();
        }
        public void RadioButton2()
        {
            ChangeBrushes(AppInfoSingleton.BrushSet2);
            Properties.Settings.Default.SettingsRadioButton = 2;
            Properties.Settings.Default.Save();
        }
        public void RadioButton3()
        {
            ChangeBrushes(AppInfoSingleton.BrushSet3);
            Properties.Settings.Default.SettingsRadioButton = 3;
            Properties.Settings.Default.Save();
        }
        public void RadioButton4()
        {
            ChangeBrushes(AppInfoSingleton.BrushSet4);
            Properties.Settings.Default.SettingsRadioButton = 4;
            Properties.Settings.Default.Save();
        }
        public void RadioButton5()
        {
            ChangeBrushes(AppInfoSingleton.BrushSet5);
            Properties.Settings.Default.SettingsRadioButton = 5;
            Properties.Settings.Default.Save();
        }
        public void RadioButton6()
        {
            ChangeBrushes(AppInfoSingleton.BrushSet6);
            Properties.Settings.Default.SettingsRadioButton = 6;
            Properties.Settings.Default.Save();
        }

        public void ChangeBrushes(Brush[] brushSet)
        {
            AppInfoSingleton.Instance.CurrentFunctionNodeOutlineBrush = brushSet[0];
            AppInfoSingleton.Instance.CurrentFunctionNodeBackgroundBrush = brushSet[1];
            AppInfoSingleton.Instance.CurrentTerminalNodeOutlineBrush = brushSet[2];
            AppInfoSingleton.Instance.CurrentTerminalNodeBackgroundBrush = brushSet[3];
            DisplayTreePlot.Clear();
            DisplayTreePlot = AppInfoSingleton.GetTreePlot(DisplayTreePlot, DisplayTree, FunctionNodeOutlineBrush, FunctionNodeBackgroundBrush,
                TerminalNodeOutlineBrush, TerminalNodeBackgroundBrush);
        }
    }
}
