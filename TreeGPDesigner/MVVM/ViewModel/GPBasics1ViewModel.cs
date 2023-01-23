using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using TreeGPDesigner.MVVM.Model;

namespace TreeGPDesigner.MVVM.ViewModel
{
    //Viewmodel class for GP Basics 1 View
    public partial class GPBasics1ViewModel : ViewModelBase
    {
        //GP Basics 1 Variables
        private static Random random = new Random();

        public Node? DisplayTree;

        public List<FunctionNode> FunctionNodes = new() { new FunctionNode("+", 2, a => a[0] + a[1]), new FunctionNode("<", 2, a => a[0] < a[1]? 1 : 0),
            new FunctionNode("/", 2, a => a[0] / a[1]), new FunctionNode("-", 2, a => a[0] - a[1]), new FunctionNode("*", 2, a => a[0] * a[1]),
            new FunctionNode("ABS", 1, a => Math.Abs(a[0]))};

        public List<TerminalNode> TerminalNodes = new() { new TerminalNode("-1", 0, -1, false), new TerminalNode("1", 0, 1, false), new TerminalNode("34", 0, 34, false), 
            new TerminalNode("X", 0, 0, true), new TerminalNode("Y", 0, 1, true), new TerminalNode("AB", 0, 2, true)};

        public List<Node> RootNodes = new() {new FunctionNode("+", 2, a => a[0] + a[1]), new FunctionNode("<", 2, a => a[0] < a[1] ? 1 : 0),
            new FunctionNode(">=", 2, a => a[0] >= a[1] ? 1 : 0), new FunctionNode("-", 2, a => a[0] - a[1]), new TerminalNode("X", 0, 0, true),
            new TerminalNode("1", 0, 1, false) };

        [ObservableProperty]
        public ObservableCollection<bool> selectedFunctionNodes = new(){ true, true, false, false, false, false };

        [ObservableProperty]
        public ObservableCollection<bool> selectedTerminalNodes = new() { true, true, false, false, false, false };

        [ObservableProperty]
        public ObservableCollection<bool> selectedRootNodes = new() { true, true, false, false, false, false };

        [ObservableProperty]
        private string informationText = "Genetic programming is the automatic generation of programs through evolutionary functions such as crossover,  " +
            "selection and mutation. In genetic programming programs are represented as tree structures. These tree structures are made up of function " +
            "and terminal nodes. Function nodes will have one or more operands which will have a function applied to them. For example an addition function " +
            "node would add two nodes together. A terminal node can be a variable or a static number like one. Terminal nodes are so called because they " +
            "are placed at the end of a branch and have no operands or nodes below them. Root nodes appear at the top of a tree and can be function or " +
            "terminal nodes depending on the GP application. The function nodes you use are called your \"function set\" and the terminal nodes you use are " +
            "called your \"terminal set.\" Both these sets together make up your \"primitive set.\"\n\nTry out selecting different function, terminal and root " +
            "nodes and see the different program trees you can generate.";

        [ObservableProperty]
        private string errorMessage = "";

        //Commands
        public ICommand NavNextCommand { get; }
        public ICommand GenerateTreeCommand { get; }
        public ICommand FunctionNodeCheckBoxCommand { get; set; }
        public ICommand TerminalNodeCheckBoxCommand { get; set; }
        public ICommand RootNodeCheckBoxCommand { get; set; }

        //Constructor
        public GPBasics1ViewModel()
        {
            NavNextCommand = new RelayCommand(NavNext);
            GenerateTreeCommand = new RelayCommand(GenerateTree);
            FunctionNodeCheckBoxCommand = new RelayCommand<string>(param => FunctionNodeCheckBox(param));
            TerminalNodeCheckBoxCommand = new RelayCommand<string>(param => TerminalNodeCheckBox(param));
            RootNodeCheckBoxCommand = new RelayCommand<string>(param => RootNodeCheckBox(param));
        }

        //Navigation Functions
        public void NavNext()
        {
            AppInfoSingleton.Instance.CurrentViewModel = new GPBasics2ViewModel();
        }

        //GP Basics 1 Functions
        public void GenerateTree()
        {
            BinPackingTemplate bpTemplate = new();
            bpTemplate.FunctionNodes.Clear();
            bpTemplate.TerminalNodes.Clear();
            bpTemplate.FunctionRootNodes.Clear();
            bpTemplate.TerminalRootNodes.Clear();
            AddSelectedNodes(bpTemplate);

            if ((bpTemplate.TerminalNodes.Count == 0 && SelectedRootNodes[4] == false && SelectedRootNodes[5] == false) ||
                (bpTemplate.TerminalRootNodes.Count == 0 && bpTemplate.FunctionRootNodes.Count == 0))
            {
                ErrorMessage = "*Select More Nodes.";
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
                CanvasHeight = (DisplayTree.Height * 100) + 100;
                CanvasWidth = (DisplayTree.Width * 100) + 100;
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
            ErrorMessage = "";
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
            ErrorMessage = "";
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
            ErrorMessage = "";
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
