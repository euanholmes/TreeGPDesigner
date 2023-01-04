using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using TreeGPDesigner.MVVM.Model;

namespace TreeGPDesigner.MVVM.ViewModel
{
    public partial class GPBasics1ViewModel : ObservableObject
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
        private float? canvasHeight;

        [ObservableProperty]
        private float? canvasWidth;

        //Commands
        public ICommand NavHomeMenuCommand { get; }
        public ICommand NavTutorialsMenuCommand { get; }
        public ICommand GenerateTreeCommand { get; }

        //Non-Observable Variables
        public Node? DisplayTree;
        public List<FunctionNode> FunctionNodes = new();
        public List<TerminalNode> TerminalNodes = new();
        public List<Node> RootNodes = new();

        public GPBasics1ViewModel()
        {
            NavHomeMenuCommand = new RelayCommand(NavHomeMenu);
            NavTutorialsMenuCommand = new RelayCommand(NavTutorialsMenu);
            GenerateTreeCommand = new RelayCommand(GenerateTree);

            //Debug
            FunctionNodes.Add(new FunctionNode("+", 2, a => a[0] + a[1], false));
            FunctionNodes.Add(new FunctionNode("<", 2, a => a[0] < a[1] ? 1 : 0, true));
            FunctionNodes.Add(new FunctionNode("/", 2, a => a[0] / a[1], false));
            FunctionNodes.Add(new FunctionNode("-", 2, a => a[0] - a[1], false));
            FunctionNodes.Add(new FunctionNode("*", 2, a => a[0] * a[1], false));
            FunctionNodes.Add(new FunctionNode("ABS", 1, a => Math.Abs(a[0]), false));

            TerminalNodes.Add(new TerminalNode("-1", 0, -1, false));
            TerminalNodes.Add(new TerminalNode("1", 0, 1, false));
            TerminalNodes.Add(new TerminalNode("34", 0, 34, false));
            TerminalNodes.Add(new TerminalNode("X", 0, 0, true));
            TerminalNodes.Add(new TerminalNode("Y", 0, 1, true));
            TerminalNodes.Add(new TerminalNode("AB", 0, 2, true));

            RootNodes.Add(new FunctionNode("+", 2, a => a[0] + a[1], false));
            RootNodes.Add(new FunctionNode("<", 2, a => a[0] < a[1] ? 1 : 0, true));
            RootNodes.Add(new FunctionNode(">=", 2, a => a[0] >= a[1] ? 1 : 0, true));
            RootNodes.Add(new FunctionNode("-", 2, a => a[0] - a[1], false));
            RootNodes.Add(new TerminalNode("X", 0, 0, true));
            RootNodes.Add(new TerminalNode("1", 0, 1, false));

            GenerateTree();
        }

        public void NavHomeMenu()
        {
            AppInfoSingleton.Instance.CurrentViewModel = new HomeViewModel();
        }

        public void NavTutorialsMenu()
        {
            AppInfoSingleton.Instance.CurrentViewModel = new TutorialsMenuViewModel();
        }

        public void GenerateTree()
        {
            DisplayTreePlot.Clear();
            BinPackingTemplate bpTemplate = new();
            AddSelectedNodes(bpTemplate);
            DisplayTree = bpTemplate.GrowTree(bpTemplate.GetRandomRootNode(), 3);
            TreeDrawingAlgorithm.CalculateNodePositions(DisplayTree);
            DisplayTreePlot = AppInfoSingleton.GetTreePlot(DisplayTreePlot, DisplayTree, BrushSet);
            CanvasHeight = DisplayTree.Height * 100 / (float)0.75;
            CanvasWidth = DisplayTree.Width * 100 / (float)0.75;
        }

        public void AddSelectedNodes(BinPackingTemplate bp)
        {
            foreach (FunctionNode functionNode in FunctionNodes)
            {
                bp.AddFunctionNode(functionNode);
            }

            foreach (TerminalNode terminalNode in TerminalNodes)
            {
                bp.AddTerminalNode(terminalNode);
            }

            foreach (Node rootNode in RootNodes)
            {
                bp.AddRootNode(rootNode);
            }
        }
    }
}
