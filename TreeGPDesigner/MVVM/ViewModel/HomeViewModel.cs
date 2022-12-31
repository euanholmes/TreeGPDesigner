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

        public ICommand NavTutorialsMenuCommand { get; }
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

        public HomeViewModel()
        {
            AppInfoSingleton.Instance.CurrentFunctionNodeOutlineBrushChanged += OnCurrentFunctionNodeOutlineBrushChanged;
            AppInfoSingleton.Instance.CurrentFunctionNodeBackgroundBrushChanged += OnCurrentFunctionNodeBackgroundBrushChanged;
            AppInfoSingleton.Instance.CurrentTerminalNodeOutlineBrushChanged += OnCurrentTerminalNodeOutlineBrushChanged;
            AppInfoSingleton.Instance.CurrentTerminalNodeBackgroundBrushChanged += OnCurrentTerminalNodeBackgroundBrushChanged;

            NavTutorialsMenuCommand = new RelayCommand(NavTutorialsMenu);
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

        public void NavTutorialsMenu()
        {
            AppInfoSingleton.Instance.CurrentViewModel = new TutorialsMenuViewModel();
        }

        public void RadioButton1()
        {
            ChangeBrushes(Brushes.Red, Brushes.Pink, Brushes.Blue, Brushes.LightBlue);
        }
        public void RadioButton2()
        {
            ChangeBrushes(Brushes.DarkOrange, Brushes.Orange, Brushes.Purple, Brushes.MediumPurple);
        }
        public void RadioButton3()
        {
            ChangeBrushes(Brushes.Yellow, Brushes.LightYellow, Brushes.Blue, Brushes.Magenta);
        }
        public void RadioButton4()
        {
            ChangeBrushes(Brushes.Brown, Brushes.RosyBrown, Brushes.Blue, Brushes.DarkBlue);
        }
        public void RadioButton5()
        {
            ChangeBrushes(Brushes.DarkGreen, Brushes.Green, Brushes.DarkTurquoise, Brushes.Turquoise);
        }
        public void RadioButton6()
        {
            ChangeBrushes(Brushes.Black, Brushes.White, Brushes.Black, Brushes.White);
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
