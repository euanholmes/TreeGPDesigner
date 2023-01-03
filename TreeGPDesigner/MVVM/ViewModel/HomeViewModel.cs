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
        public Brush[]? BrushSet => AppInfoSingleton.Instance.CurrentBrushSet;
        private void OnCurrentBrushSetChanged()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(BrushSet)));
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
        public Brush[]? BrushSet1 => AppInfoSingleton.Instance.CurrentBrushSet1;
        private void OnCurrentBrushSet1Changed()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(BrushSet1)));
        }

        public Brush[]? BrushSet2 => AppInfoSingleton.Instance.CurrentBrushSet2;
        private void OnCurrentBrushSet2Changed()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(BrushSet2)));
        }

        public Brush[]? BrushSet3 => AppInfoSingleton.Instance.CurrentBrushSet3;
        private void OnCurrentBrushSet3Changed()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(BrushSet3)));
        }

        public Brush[]? BrushSet4 => AppInfoSingleton.Instance.CurrentBrushSet4;
        private void OnCurrentBrushSet4Changed()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(BrushSet4)));
        }

        public Brush[]? BrushSet5 => AppInfoSingleton.Instance.CurrentBrushSet5;
        private void OnCurrentBrushSet5Changed()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(BrushSet5)));
        }

        public Brush[]? BrushSet6 => AppInfoSingleton.Instance.CurrentBrushSet6;
        private void OnCurrentBrushSet6Changed()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(BrushSet6)));
        }


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
            AppInfoSingleton.Instance.CurrentBrushSetChanged += OnCurrentBrushSetChanged;
            AppInfoSingleton.Instance.CurrentZoomIconChanged += OnCurrentZoomIconChanged;

            //Brush sets.
            AppInfoSingleton.Instance.CurrentBrushSet1Changed += OnCurrentBrushSet1Changed;
            AppInfoSingleton.Instance.CurrentBrushSet2Changed += OnCurrentBrushSet2Changed;
            AppInfoSingleton.Instance.CurrentBrushSet3Changed += OnCurrentBrushSet3Changed;
            AppInfoSingleton.Instance.CurrentBrushSet4Changed += OnCurrentBrushSet4Changed;
            AppInfoSingleton.Instance.CurrentBrushSet5Changed += OnCurrentBrushSet5Changed;
            AppInfoSingleton.Instance.CurrentBrushSet6Changed += OnCurrentBrushSet6Changed;

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
            DisplayTreePlot = AppInfoSingleton.GetTreePlot(DisplayTreePlot, DisplayTree, BrushSet);

            CanvasHeight = DisplayTree.Height * 100 / (float)0.75; 
            CanvasWidth = DisplayTree.Width * 100 / (float)0.75;
        }

        public void ToggleButtonLightDarkMode()
        {
            SwitchBrushSets();
            SwitchCurrentBrushSet();

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

            DisplayTreePlot.Clear();
            DisplayTreePlot = AppInfoSingleton.GetTreePlot(DisplayTreePlot, DisplayTree, BrushSet);
        }

        public void NavTutorialsMenu()
        {
            AppInfoSingleton.Instance.CurrentViewModel = new TutorialsMenuViewModel();
        }

        public void RadioButton1()
        {
            ChangeBrushes(AppInfoSingleton.Instance.CurrentBrushSet1);
            Properties.Settings.Default.SettingsRadioButton = 1;
            Properties.Settings.Default.Save();
        }
        public void RadioButton2()
        {
            ChangeBrushes(AppInfoSingleton.Instance.CurrentBrushSet2);
            Properties.Settings.Default.SettingsRadioButton = 2;
            Properties.Settings.Default.Save();
        }
        public void RadioButton3()
        {
            ChangeBrushes(AppInfoSingleton.Instance.CurrentBrushSet3);
            Properties.Settings.Default.SettingsRadioButton = 3;
            Properties.Settings.Default.Save();
        }
        public void RadioButton4()
        {
            ChangeBrushes(AppInfoSingleton.Instance.CurrentBrushSet4);
            Properties.Settings.Default.SettingsRadioButton = 4;
            Properties.Settings.Default.Save();
        }
        public void RadioButton5()
        {
            ChangeBrushes(AppInfoSingleton.Instance.CurrentBrushSet5);
            Properties.Settings.Default.SettingsRadioButton = 5;
            Properties.Settings.Default.Save();
        }
        public void RadioButton6()
        {
            ChangeBrushes(AppInfoSingleton.Instance.CurrentBrushSet6);
            Properties.Settings.Default.SettingsRadioButton = 6;
            Properties.Settings.Default.Save();
        }

        public void ChangeBrushes(Brush[] brushSet)
        {
            AppInfoSingleton.Instance.CurrentBrushSet = brushSet;
            DisplayTreePlot.Clear();
            DisplayTreePlot = AppInfoSingleton.GetTreePlot(DisplayTreePlot, DisplayTree, BrushSet);
        }

        public void SwitchBrushSets()
        {
            if (!Properties.Settings.Default.SettingsMode)
            {
                AppInfoSingleton.Instance.CurrentBrushSet1 = AppInfoSingleton.BrushSet1;
                AppInfoSingleton.Instance.CurrentBrushSet2 = AppInfoSingleton.BrushSet2;
                AppInfoSingleton.Instance.CurrentBrushSet3 = AppInfoSingleton.BrushSet3;
                AppInfoSingleton.Instance.CurrentBrushSet4 = AppInfoSingleton.BrushSet4;
                AppInfoSingleton.Instance.CurrentBrushSet5 = AppInfoSingleton.BrushSet5;
                AppInfoSingleton.Instance.CurrentBrushSet6 = AppInfoSingleton.BrushSet6;
            }
            else
            {
                AppInfoSingleton.Instance.CurrentBrushSet1 = AppInfoSingleton.BrushSet1Dark;
                AppInfoSingleton.Instance.CurrentBrushSet2 = AppInfoSingleton.BrushSet2Dark;
                AppInfoSingleton.Instance.CurrentBrushSet3 = AppInfoSingleton.BrushSet3Dark;
                AppInfoSingleton.Instance.CurrentBrushSet4 = AppInfoSingleton.BrushSet4Dark;
                AppInfoSingleton.Instance.CurrentBrushSet5 = AppInfoSingleton.BrushSet5Dark;
                AppInfoSingleton.Instance.CurrentBrushSet6 = AppInfoSingleton.BrushSet6Dark;
            }
        }

        public void SwitchCurrentBrushSet()
        {
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
        }
    }
}
