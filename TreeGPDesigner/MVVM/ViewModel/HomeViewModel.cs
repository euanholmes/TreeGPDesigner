using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using TreeGPDesigner.MVVM.Model;

namespace TreeGPDesigner.MVVM.ViewModel
{
    public partial class HomeViewModel : ObservableObject
    {
        public Brush? FunctionNodeOutlineBrush;
        public Brush? FunctionNodeBackgroundBrush;
        public Brush? TerminalNodeOutlineBrush;
        public Brush? TerminalNodeBackgroundBrush;
        public CornerRadius FunctionCornerRadius = new(50);
        public CornerRadius TerminalCornerRadius = new(0);
        public Node DisplayTree = new FunctionNode("", 2, a => a[0] <= a[1] ? 1 : 0, true);

        public ICommand NavTutorialsMenuCommand { get; }

        [ObservableProperty]
        private ObservableCollection<NodePlot> displayTreePlot = new ObservableCollection<NodePlot>();

        public HomeViewModel()
        {
            NavTutorialsMenuCommand = new RelayCommand(NavTutorialsMenu);
            DisplayTree.ChildNodes.Add(new FunctionNode("", 2, a => a[0] <= a[1] ? 1 : 0, true));
            DisplayTree.ChildNodes.Add(new TerminalNode("", 0, 0, false));
            DisplayTree.ChildNodes[0].ChildNodes.Add(new TerminalNode("", 0, 0, false));
            DisplayTree.ChildNodes[0].ChildNodes.Add(new TerminalNode("", 0, 0, false));

            FunctionNodeOutlineBrush = Brushes.Red;
            FunctionNodeBackgroundBrush = Brushes.Pink;
            TerminalNodeOutlineBrush = Brushes.Blue;
            TerminalNodeBackgroundBrush = Brushes.LightBlue;

            TreeDrawingAlgorithm.CalculateNodePositions(DisplayTree);

            DisplayTreePlot = AppInfoSingleton.GetTreePlot(DisplayTreePlot, DisplayTree, FunctionNodeOutlineBrush, FunctionNodeBackgroundBrush,
                TerminalNodeOutlineBrush, TerminalNodeBackgroundBrush, FunctionCornerRadius, TerminalCornerRadius); 
        }

        public void NavTutorialsMenu()
        {
            AppInfoSingleton.Instance.CurrentViewModel = new TutorialsMenuViewModel();
        }


    }
}
