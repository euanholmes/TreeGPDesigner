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
    public partial class GPBasics2ViewModel : ObservableObject
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
        private float? canvasWidth = 0;

        [ObservableProperty]
        private float? canvasHeight = 0;

        //GP Basics 2 Variables
        [ObservableProperty]
        private int currentGrowMethod = 0;

        private BinPackingTemplate bpTemplate = new();

        private static Random random = new();

        //Commands
        public ICommand NavHomeMenuCommand { get; }
        public ICommand NavTutorialsMenuCommand { get; }
        public ICommand NavNextCommand { get; }
        public ICommand NavPreviousCommand { get; }
        public ICommand ChangeGrowMethodCommand { get; }
        public ICommand GenerateTreeCommand { get; }

        //Constructor
        public GPBasics2ViewModel()
        {
            NavHomeMenuCommand = new RelayCommand(NavHomeMenu);
            NavTutorialsMenuCommand = new RelayCommand(NavTutorialsMenu);
            NavNextCommand = new RelayCommand(NavNext);
            NavPreviousCommand = new RelayCommand(NavPrevious);
            ChangeGrowMethodCommand = new RelayCommand(ChangeGrowMethod);
            GenerateTreeCommand = new RelayCommand(GenerateTree);

            bpTemplate.FunctionNodes = new List<FunctionNode> {new FunctionNode("+", 2, a => a[0] + a[1], false), new FunctionNode("<", 2, a => a[0] < a[1] ? 1 : 0, true),
            new FunctionNode("/", 2, a => a[0] / a[1], false), new FunctionNode("-", 2, a => a[0] - a[1], false), new FunctionNode("*", 2, a => a[0] * a[1], false),
            new FunctionNode("ABS", 1, a => Math.Abs(a[0]), false)};

            bpTemplate.TerminalNodes = new List<TerminalNode> {new TerminalNode("-1", 0, -1, false), new TerminalNode("1", 0, 1, false), new TerminalNode("34", 0, 34, false),
            new TerminalNode("X", 0, 0, true), new TerminalNode("Y", 0, 1, true), new TerminalNode("AB", 0, 2, true) };

            bpTemplate.FunctionRootNodes = new List<FunctionNode> {new FunctionNode("+", 2, a => a[0] + a[1], false), new FunctionNode("<", 2, a => a[0] < a[1] ? 1 : 0, true),
            new FunctionNode(">=", 2, a => a[0] >= a[1] ? 1 : 0, true), new FunctionNode("-", 2, a => a[0] - a[1], false)};
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
            AppInfoSingleton.Instance.CurrentViewModel = new GPBasics3ViewModel();
        }

        public void NavPrevious()
        {
            AppInfoSingleton.Instance.CurrentViewModel = new GPBasics1ViewModel();
        }

        //GP Basics 2 Functions
        public void ChangeGrowMethod()
        {
            if (CurrentGrowMethod == 0)
            {
                CurrentGrowMethod = 1;
            }
            else
            {
                CurrentGrowMethod = 0;
            }
        }

        public void GenerateTree()
        {
            Node displayTree = bpTemplate.CopyNode(bpTemplate.FunctionRootNodes[random.Next(0, bpTemplate.FunctionRootNodes.Count)]);

            int depth = random.Next(1, 4);

            if (CurrentGrowMethod == 0)
            {
                displayTree = bpTemplate.GrowTree(displayTree, depth);
            }
            else
            {
                displayTree = bpTemplate.FullTree(displayTree, depth);
            }

            TreeDrawingAlgorithm.CalculateNodePositions(displayTree);
            DisplayTreePlot.Clear();
            DisplayTreePlot = AppInfoSingleton.GetTreePlot(DisplayTreePlot, displayTree, BrushSet);
            CanvasHeight = (displayTree.Height * 100) + 100;
            CanvasWidth = (displayTree.Width * 100) + 100;
        }
    }
}
