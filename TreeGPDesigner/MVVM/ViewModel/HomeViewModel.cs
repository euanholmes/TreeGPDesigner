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
        public new event PropertyChangedEventHandler? PropertyChanged;

        public Brush? FunctionNodeOutlineBrush => AppInfoSingleton.Instance.CurrentFunctionNodeOutlineBrush;
        public Brush? FunctionNodeBackgroundBrush => AppInfoSingleton.Instance.CurrentFunctionNodeBackgroundBrush;
        public Brush? TerminalNodeOutlineBrush => AppInfoSingleton.Instance.CurrentTerminalNodeOutlineBrush;
        public Brush? TerminalNodeBackgroundBrush => AppInfoSingleton.Instance.CurrentTerminalNodeBackgroundBrush;
        public ImageSource? ToggleButtonSource => AppInfoSingleton.Instance.CurrentModeToggleButton;
        public Brush? TextColour => AppInfoSingleton.Instance.CurrentText;
        public int? RadioButtonCheck => AppInfoSingleton.Instance.CurrentRadioButtonCheck;

        private void OnCurrentFunctionNodeOutlineBrushChanged()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FunctionNodeOutlineBrush)));
        }
        private void OnCurrentFunctionNodeBackgroundBrushChanged()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FunctionNodeBackgroundBrush)));
        }
        private void OnCurrentTerminalNodeOutlineBrushChanged()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TerminalNodeOutlineBrush)));
        }
        private void OnCurrentTerminalNodeBackgroundBrushChanged()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TerminalNodeBackgroundBrush)));
        }
        private void OnCurrentToggleModeButtonChanged()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ToggleButtonSource)));
        }
        private void OnCurrentTextChanged()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TextColour)));
        }
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

        [ObservableProperty]
        public ObservableCollection<NodePlot> displayTreePlot = new();

        [ObservableProperty]
        private float canvasPositionHeight;

        [ObservableProperty]
        private float canvasPositionWidth;

        public Node DisplayTree = new FunctionNode("Euan", 2, a => a[0] <= a[1] ? 1 : 0, true);
        public bool LightMode = true;
        

        public HomeViewModel()
        {
            AppInfoSingleton.Instance.CurrentFunctionNodeOutlineBrushChanged += OnCurrentFunctionNodeOutlineBrushChanged;
            AppInfoSingleton.Instance.CurrentFunctionNodeBackgroundBrushChanged += OnCurrentFunctionNodeBackgroundBrushChanged;
            AppInfoSingleton.Instance.CurrentTerminalNodeOutlineBrushChanged += OnCurrentTerminalNodeOutlineBrushChanged;
            AppInfoSingleton.Instance.CurrentTerminalNodeBackgroundBrushChanged += OnCurrentTerminalNodeBackgroundBrushChanged;
            AppInfoSingleton.Instance.CurrentModeToggleButtonChanged += OnCurrentToggleModeButtonChanged;
            AppInfoSingleton.Instance.CurrentTextChanged += OnCurrentTextChanged;
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

            CanvasPositionHeight = DisplayTree.Height * 100 / (float)0.75;
            CanvasPositionWidth = DisplayTree.Width * 100 / (float)0.75;
            
            DisplayTreePlot = AppInfoSingleton.GetTreePlot(DisplayTreePlot, DisplayTree, FunctionNodeOutlineBrush, FunctionNodeBackgroundBrush,
                TerminalNodeOutlineBrush, TerminalNodeBackgroundBrush); 
        }

        public void ToggleButtonLightDarkMode()
        {
            if (LightMode)
            {
                AppInfoSingleton.Instance.CurrentModeToggleButton = new BitmapImage(new Uri("pack://application:,,,/Images/dark-mode-toggle-icon-inverted.png"));
                AppInfoSingleton.Instance.CurrentBackground = (Brush)new BrushConverter().ConvertFrom("#2b2828");
                AppInfoSingleton.Instance.CurrentText = Brushes.White;
                LightMode = false;
            }
            else
            {
                AppInfoSingleton.Instance.CurrentModeToggleButton = new BitmapImage(new Uri("pack://application:,,,/Images/light-mode-toggle-icon.png"));
                AppInfoSingleton.Instance.CurrentBackground = Brushes.White;
                AppInfoSingleton.Instance.CurrentText = Brushes.Black;
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
            ChangeBrushes(Brushes.Brown, Brushes.RosyBrown, Brushes.Blue, Brushes.DarkBlue);
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
