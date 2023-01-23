using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using TreeGPDesigner.MVVM.Model;

namespace TreeGPDesigner.MVVM.ViewModel
{
    //Viewmodel class for GP Basics 2 View
    public partial class GPBasics2ViewModel : ViewModelBase
    {
        //GP Basics 2 Variables
        private static Random random = new();

        private BinPackingTemplate bpTemplate = new();

        [ObservableProperty]
        private int currentGrowMethod = 0;

        [ObservableProperty]
        private string informationText = "Once you've selected your primitive set you are ready to start generating programs." +
            " To generate programs you need to grow program trees. There are many ways to grow program trees, " +
            "two common tree growing methods are \"grow\" and \"full.\" Trees grown with the " +
            "\"grow\" method will grow branches from the root node and will place terminal or function nodes on each branch " +
            "up until the max depth where only terminal nodes will be placed. Trees grown with the \"full\" method will place " +
            "function nodes at each depth up until the max depth where only terminal nodes will be placed. When starting a " +
            "GP run all trees can be grown with the \"full\" or \"grow\" methods or a mixture of the two can be used. A popular method " +
            "used in GP is the ramped half and half approach where half the trees at each depth level are grown with the \"full\" " +
            "method and half are grown with the \"grow\" method \n\nTry growing trees with these two different methods and see what " +
            "kind of trees you can grow and how the two methods differ.";

        //Commands
        public ICommand NavNextCommand { get; }
        public ICommand NavPreviousCommand { get; }
        public ICommand ChangeGrowMethodCommand { get; }
        public ICommand GenerateTreeCommand { get; }

        //Constructor
        public GPBasics2ViewModel()
        {
            NavNextCommand = new RelayCommand(NavNext);
            NavPreviousCommand = new RelayCommand(NavPrevious);
            ChangeGrowMethodCommand = new RelayCommand(ChangeGrowMethod);
            GenerateTreeCommand = new RelayCommand(GenerateTree);

            bpTemplate.FunctionNodes = new List<FunctionNode> {new FunctionNode("+", 2, a => a[0] + a[1]), new FunctionNode("<", 2, a => a[0] < a[1] ? 1 : 0),
            new FunctionNode("/", 2, a => a[0] / a[1]), new FunctionNode("-", 2, a => a[0] - a[1]), new FunctionNode("*", 2, a => a[0] * a[1]),
            new FunctionNode("ABS", 1, a => Math.Abs(a[0]))};

            bpTemplate.TerminalNodes = new List<TerminalNode> {new TerminalNode("-1", 0, -1, false), new TerminalNode("1", 0, 1, false), new TerminalNode("34", 0, 34, false),
            new TerminalNode("X", 0, 0, true), new TerminalNode("Y", 0, 1, true), new TerminalNode("AB", 0, 2, true) };

            bpTemplate.FunctionRootNodes = new List<FunctionNode> {new FunctionNode("+", 2, a => a[0] + a[1]), new FunctionNode("<", 2, a => a[0] < a[1] ? 1 : 0),
            new FunctionNode(">=", 2, a => a[0] >= a[1] ? 1 : 0), new FunctionNode("-", 2, a => a[0] - a[1])};
        }

        //Navigation Functions
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
