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

        public int? RadioButtonCheck => AppInfoSingleton.Instance.CurrentRadioButtonCheck;
        private void OnCurrentRadioButtonCheckChanged()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(RadioButtonCheck)));
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
        public bool LightMode = true;
        

        public HomeViewModel()
        {
            //Variables in every viewmodel.
            AppInfoSingleton.Instance.CurrentTextChanged += OnCurrentTextChanged;
            AppInfoSingleton.Instance.CurrentPanel1ColorChanged += OnCurrentPanel1ColourChanged;
            AppInfoSingleton.Instance.CurrentNormalButtonColorChanged += OnCurrentNormalButtonColourChanged;
            AppInfoSingleton.Instance.CurrentNavButtonColorChanged += OnCurrentNavButtonColourChanged;

            //Tree Drawing Variables.
            AppInfoSingleton.Instance.CurrentFunctionNodeOutlineBrushChanged += OnCurrentFunctionNodeOutlineBrushChanged;
            AppInfoSingleton.Instance.CurrentFunctionNodeBackgroundBrushChanged += OnCurrentFunctionNodeBackgroundBrushChanged;
            AppInfoSingleton.Instance.CurrentTerminalNodeOutlineBrushChanged += OnCurrentTerminalNodeOutlineBrushChanged;
            AppInfoSingleton.Instance.CurrentTerminalNodeBackgroundBrushChanged += OnCurrentTerminalNodeBackgroundBrushChanged;

            //Viewmodel Specific variables.
            AppInfoSingleton.Instance.CurrentModeToggleButtonChanged += OnCurrentToggleModeButtonChanged;
            AppInfoSingleton.Instance.CurrentRadioButtonCheckChanged += OnCurrentRadioButtonCheckChanged;

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
            if (LightMode)
            {
                AppInfoSingleton.Instance.CurrentModeToggleButton = new BitmapImage(new Uri("pack://application:,,,/Images/dark-mode-toggle-icon-inverted.png"));
                AppInfoSingleton.Instance.CurrentBackground = AppInfoSingleton.DarkBackground;
                AppInfoSingleton.Instance.CurrentText = Brushes.White;
                AppInfoSingleton.Instance.CurrentPanel1Color = AppInfoSingleton.DarkPanel1;
                AppInfoSingleton.Instance.CurrentNormalButtonColor = AppInfoSingleton.DarkNormalButton;
                AppInfoSingleton.Instance.CurrentNavButtonColor = AppInfoSingleton.DarkNavButton;
                LightMode = false;
            }
            else
            {
                AppInfoSingleton.Instance.CurrentModeToggleButton = new BitmapImage(new Uri("pack://application:,,,/Images/light-mode-toggle-icon.png"));
                AppInfoSingleton.Instance.CurrentBackground = AppInfoSingleton.LightBackground;
                AppInfoSingleton.Instance.CurrentText = Brushes.Black;
                AppInfoSingleton.Instance.CurrentPanel1Color = AppInfoSingleton.LightPanel1;
                AppInfoSingleton.Instance.CurrentNormalButtonColor = AppInfoSingleton.LightNormalButton;
                AppInfoSingleton.Instance.CurrentNavButtonColor = AppInfoSingleton.LightNavButton;
                LightMode = true;
            }
        }

        public void NavTutorialsMenu()
        {
            AppInfoSingleton.Instance.CurrentViewModel = new TutorialsMenuViewModel();
        }

        public void RadioButton1()
        {
            ChangeBrushes(Brushes.Red, Brushes.Pink, Brushes.Blue, Brushes.LightBlue);
            AppInfoSingleton.Instance.CurrentRadioButtonCheck = 1;
        }
        public void RadioButton2()
        {
            ChangeBrushes(Brushes.DarkOrange, Brushes.Orange, Brushes.Purple, Brushes.MediumPurple);
            AppInfoSingleton.Instance.CurrentRadioButtonCheck = 2;
        }
        public void RadioButton3()
        {
            ChangeBrushes(Brushes.Yellow, Brushes.LightYellow, Brushes.Blue, Brushes.Magenta);
            AppInfoSingleton.Instance.CurrentRadioButtonCheck = 3;
        }
        public void RadioButton4()
        {
            ChangeBrushes(Brushes.Brown, Brushes.RosyBrown, Brushes.DarkBlue, Brushes.Blue);
            AppInfoSingleton.Instance.CurrentRadioButtonCheck = 4;
        }
        public void RadioButton5()
        {
            ChangeBrushes(Brushes.DarkGreen, Brushes.Green, Brushes.DarkTurquoise, Brushes.Turquoise);
            AppInfoSingleton.Instance.CurrentRadioButtonCheck = 5;
        }
        public void RadioButton6()
        {
            ChangeBrushes(Brushes.Black, Brushes.White, Brushes.Black, Brushes.White);
            AppInfoSingleton.Instance.CurrentRadioButtonCheck = 6;
        }

        public void ChangeBrushes(Brush newFunctionNodeOutlineBrush, Brush newFunctionNodeBackgroundBrush, Brush newTerminalNodeOutlineBrush,
            Brush newTerminalNodeBackgroundBrush)
        {
            AppInfoSingleton.Instance.CurrentFunctionNodeOutlineBrush = newFunctionNodeOutlineBrush;
            AppInfoSingleton.Instance.CurrentFunctionNodeBackgroundBrush = newFunctionNodeBackgroundBrush;
            AppInfoSingleton.Instance.CurrentTerminalNodeOutlineBrush = newTerminalNodeOutlineBrush;
            AppInfoSingleton.Instance.CurrentTerminalNodeBackgroundBrush = newTerminalNodeBackgroundBrush;
            DisplayTreePlot.Clear();
            DisplayTreePlot = AppInfoSingleton.GetTreePlot(DisplayTreePlot, DisplayTree, FunctionNodeOutlineBrush, FunctionNodeBackgroundBrush,
                TerminalNodeOutlineBrush, TerminalNodeBackgroundBrush);
        }
    }
}
