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
using System.Windows.Controls;
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
        public ICommand NavNextCommand { get; }
        public ICommand GenerateTreeCommand { get; }
        public ICommand FunctionNodeCheckBoxCommand { get; set; }
        public ICommand TerminalNodeCheckBoxCommand { get; set; }
        public ICommand RootNodeCheckBoxCommand { get; set; }

        //Variables
        public Node? DisplayTree;
        private static Random random = new Random();

        public List<FunctionNode> FunctionNodes = new() { new FunctionNode("+", 2, a => a[0] + a[1], false), new FunctionNode("<", 2, a => a[0] < a[1]? 1 : 0, true),
            new FunctionNode("/", 2, a => a[0] / a[1], false), new FunctionNode("-", 2, a => a[0] - a[1], false), new FunctionNode("*", 2, a => a[0] * a[1], false),
            new FunctionNode("ABS", 1, a => Math.Abs(a[0]), false)};

        public List<TerminalNode> TerminalNodes = new() { new TerminalNode("-1", 0, -1, false), new TerminalNode("1", 0, 1, false), new TerminalNode("34", 0, 34, false), 
            new TerminalNode("X", 0, 0, true), new TerminalNode("Y", 0, 1, true), new TerminalNode("AB", 0, 2, true)};

        public List<Node> RootNodes = new() {new FunctionNode("+", 2, a => a[0] + a[1], false), new FunctionNode("<", 2, a => a[0] < a[1] ? 1 : 0, true),
            new FunctionNode(">=", 2, a => a[0] >= a[1] ? 1 : 0, true), new FunctionNode("-", 2, a => a[0] - a[1], false), new TerminalNode("X", 0, 0, true),
            new TerminalNode("1", 0, 1, false) };

        [ObservableProperty]
        public ObservableCollection<bool> selectedFunctionNodes = new(){ true, true, false, false, false, false };

        [ObservableProperty]
        public ObservableCollection<bool> selectedTerminalNodes = new() { true, true, false, false, false, false };

        [ObservableProperty]
        public ObservableCollection<bool> selectedRootNodes = new() { true, true, false, false, false, false };

        //Constructor
        public GPBasics1ViewModel()
        {
            NavHomeMenuCommand = new RelayCommand(NavHomeMenu);
            NavTutorialsMenuCommand = new RelayCommand(NavTutorialsMenu);
            NavNextCommand = new RelayCommand(NavNext);
            GenerateTreeCommand = new RelayCommand(GenerateTree);

            FunctionNodeCheckBoxCommand = new RelayCommand<string>(param => FunctionNodeCheckBox(param));
            TerminalNodeCheckBoxCommand = new RelayCommand<string>(param => TerminalNodeCheckBox(param));
            RootNodeCheckBoxCommand = new RelayCommand<string>(param => RootNodeCheckBox(param));

            GenerateTree();
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
            AppInfoSingleton.Instance.CurrentViewModel = new GPBasics2ViewModel();
        }

        //Functions
        public void GenerateTree()
        {
            BinPackingTemplate bpTemplate = new();
            AddSelectedNodes(bpTemplate);

            if ((bpTemplate.TerminalNodes.Count == 0 && SelectedRootNodes[4] == false && SelectedRootNodes[5] == false) ||
                (bpTemplate.TerminalRootNodes.Count == 0 && bpTemplate.FunctionRootNodes.Count == 0))
            {
                MessageBox.Show("Error generating tree. Make sure there are root nodes selected. Also, if using only function root " +
                    "nodes make sure there are terminal nodes selected.", "Tree Generation Error");
            }
            else
            {
                DisplayTreePlot.Clear();

                if (bpTemplate.TerminalNodes.Count == 0)
                {
                    if (SelectedRootNodes[4] == false)
                    {
                        DisplayTree = RootNodes[5];
                    }
                    else if (SelectedRootNodes[5] == false)
                    {
                        DisplayTree = RootNodes[4];
                    }
                    else
                    {
                        int[] rootTerminalNodeIndexes = { 4, 5 };
                        int randomRootTerminalNodeIndex = rootTerminalNodeIndexes[random.Next(0, rootTerminalNodeIndexes.Length)];
                        DisplayTree = RootNodes[randomRootTerminalNodeIndex];
                    }
                }
                else if (bpTemplate.FunctionNodes.Count == 0)
                {
                    DisplayTree = bpTemplate.GrowTree(bpTemplate.GetRandomRootNode(), 1);
                }
                else
                {
                    DisplayTree = bpTemplate.GrowTree(bpTemplate.GetRandomRootNode(), 3);
                }

                TreeDrawingAlgorithm.CalculateNodePositions(DisplayTree);
                DisplayTreePlot = AppInfoSingleton.GetTreePlot(DisplayTreePlot, DisplayTree, BrushSet);
                CanvasHeight = DisplayTree.Height * 100 / (float)0.75;
                CanvasWidth = DisplayTree.Width * 100 / (float)0.75;
            } 
        }

        public void AddSelectedNodes(BinPackingTemplate bp)
        {
            for (int i = 0; i < SelectedFunctionNodes.Count; i++)
            {
                if (SelectedFunctionNodes[i] == true)
                {
                    bp.AddFunctionNode(FunctionNodes[i]);
                }
            }

            for (int i = 0; i < SelectedTerminalNodes.Count; i++)
            {
                if (SelectedTerminalNodes[i] == true)
                {
                    bp.AddTerminalNode(TerminalNodes[i]);
                }
            }

            for (int i = 0; i < SelectedRootNodes.Count; i++)
            {
                if (SelectedRootNodes[i] == true)
                {
                    bp.AddRootNode(RootNodes[i]);
                }
            }
        }

        public void FunctionNodeCheckBox(string functionNodeNum)
        {
            if (SelectedFunctionNodes[Convert.ToInt32(functionNodeNum)] == true)
            {
                SelectedFunctionNodes[Convert.ToInt32(functionNodeNum)] = false;
            }
            else
            {
                SelectedFunctionNodes[Convert.ToInt32(functionNodeNum)] = true;
            }
        }

        public void TerminalNodeCheckBox(string terminalNodeNum)
        {
            if (SelectedTerminalNodes[Convert.ToInt32(terminalNodeNum)] == true)
            {
                SelectedTerminalNodes[Convert.ToInt32(terminalNodeNum)] = false;
            }
            else
            {
                SelectedTerminalNodes[Convert.ToInt32(terminalNodeNum)] = true;
            }
        }

        public void RootNodeCheckBox(string rootNodeNum)
        {
            if (SelectedRootNodes[Convert.ToInt32(rootNodeNum)] == true)
            {
                SelectedRootNodes[Convert.ToInt32(rootNodeNum)] = false;
            }
            else
            {
                SelectedRootNodes[Convert.ToInt32(rootNodeNum)] = true;
            }
        }
    }
}
