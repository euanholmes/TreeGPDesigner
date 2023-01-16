using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using TreeGPDesigner.MVVM.Model;

namespace TreeGPDesigner.MVVM.ViewModel
{
    public partial class GPBasics6ViewModel : ObservableObject
    {
        //Common Variables
        [ObservableProperty]
        private Brush? textColour = AppInfoSingleton.Instance.CurrentText;

        [ObservableProperty]
        private Brush? normalButtonColour = AppInfoSingleton.Instance.CurrentNormalButtonColor;

        [ObservableProperty]
        private Brush? navButtonColour = AppInfoSingleton.Instance.CurrentNavButtonColor;

        [ObservableProperty]
        private Brush? panel1Colour = AppInfoSingleton.Instance.CurrentPanel1Color;

        [ObservableProperty]
        private Brush? panel2Colour = AppInfoSingleton.Instance.CurrentPanel2Color;

        //Tree Drawing Variables
        [ObservableProperty]
        private Brush[]? brushSet = AppInfoSingleton.Instance.CurrentBrushSet;

        [ObservableProperty]
        private ImageSource? zoomIconSource = AppInfoSingleton.Instance.CurrentZoomIcon;

        [ObservableProperty]
        public ObservableCollection<NodePlot> displayTreePlot = new();

        [ObservableProperty]
        private float? canvasWidth;

        [ObservableProperty]
        private float? canvasHeight;

        //Commands
        public ICommand NavHomeMenuCommand { get; }
        public ICommand NavTutorialsMenuCommand { get; }
        public ICommand NavNextCommand { get; }
        public ICommand NavPreviousCommand { get; }
        public ICommand MutateTreeCommand { get; }

        //GP Basics 6 Variables
        private BinPackingTemplate bpTemplate = new();

        private static Random random = new();

        private Node displayTree;
        

        //Constructor
        public GPBasics6ViewModel()
        {
            NavHomeMenuCommand = new RelayCommand(NavHomeMenu);
            NavTutorialsMenuCommand = new RelayCommand(NavTutorialsMenu);
            NavNextCommand = new RelayCommand(NavNext);
            NavPreviousCommand = new RelayCommand(NavPrevious);
            MutateTreeCommand = new RelayCommand(MutateTree);

            bpTemplate.FunctionNodes = new List<FunctionNode> {new FunctionNode("+", 2, a => a[0] + a[1], false), new FunctionNode("<", 2, a => a[0] < a[1] ? 1 : 0, true),
            new FunctionNode("/", 2, a => a[0] / a[1], false), new FunctionNode("-", 2, a => a[0] - a[1], false), new FunctionNode("*", 2, a => a[0] * a[1], false),
            new FunctionNode("ABS", 1, a => Math.Abs(a[0]), false)};

            bpTemplate.TerminalNodes = new List<TerminalNode> {new TerminalNode("-1", 0, -1, false), new TerminalNode("1", 0, 1, false), new TerminalNode("34", 0, 34, false),
            new TerminalNode("X", 0, 0, true), new TerminalNode("Y", 0, 1, true), new TerminalNode("AB", 0, 2, true) };

            bpTemplate.FunctionRootNodes = new List<FunctionNode> {new FunctionNode("+", 2, a => a[0] + a[1], false), new FunctionNode("<", 2, a => a[0] < a[1] ? 1 : 0, true),
            new FunctionNode(">=", 2, a => a[0] >= a[1] ? 1 : 0, true), new FunctionNode("-", 2, a => a[0] - a[1], false)};

            displayTree = bpTemplate.CopyNode(bpTemplate.FunctionRootNodes[random.Next(0, bpTemplate.FunctionRootNodes.Count)]);
            displayTree = bpTemplate.GrowTree(displayTree, random.Next(1, 3));
            TreeDrawingAlgorithm.CalculateNodePositions(displayTree);
            DisplayTreePlot.Clear();
            DisplayTreePlot = AppInfoSingleton.GetTreePlot(DisplayTreePlot, displayTree, BrushSet);
            CanvasHeight = (displayTree.Height * 100) + 100;
            CanvasWidth = (displayTree.Width * 100) + 100;
        }

        


        //Navigation Functions
        public void NavHomeMenu()
        {
            AppInfoSingleton.Instance.CurrentViewModel = new HomeViewModel();
        }

        public void NavTutorialsMenu()
        {
            AppInfoSingleton.Instance.CurrentViewModel = new TutorialsMenuViewModel();
        }

        public void NavNext()
        {
            AppInfoSingleton.Instance.CurrentViewModel = new GPBasics7ViewModel();
        }

        public void NavPrevious()
        {
            AppInfoSingleton.Instance.CurrentViewModel = new GPBasics5ViewModel();
        }

        //GP Basics 6 Functions.
        public void MutateTree()
        {
            bpTemplate.Mutate(displayTree, 3);
            TreeDrawingAlgorithm.CalculateNodePositions(displayTree);
            DisplayTreePlot.Clear();
            DisplayTreePlot = AppInfoSingleton.GetTreePlot(DisplayTreePlot, displayTree, BrushSet);
            CanvasHeight = (displayTree.Height * 100) + 100;
            CanvasWidth = (displayTree.Width * 100) + 100;
        }
    }
}
