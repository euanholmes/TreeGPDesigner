using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using TreeGPDesigner.MVVM.Model;

namespace TreeGPDesigner.MVVM.ViewModel
{
    //Viewmodel class for GP Basics 6 View
    public partial class GPBasics6ViewModel : ViewModelBase
    {
        //GP Basics 6 Variables
        private static Random random = new();

        private BinPackingTemplate bpTemplate = new();

        private Node displayTree;

        [ObservableProperty]
        private string informationText = "Mutation is a genetic function which alters a single program tree. There is more than one " +
            "way to perform mutation but the type used here will be \"subtree mutation.\" The first step in subtree mutation is finding " +
            "a mutation point, this is a random point on the tree. The tree is then split at this mutation point with the branch at the mutation point " +
            "being cut off and replaced with a new randomly generated subtree. This subtree could be as simple as a single terminal node or a more " +
            "complex tree with a depth which would make the tree no larger than the max depth set for the population.\n\nTry mutating " +
            "the tree in the panel on the right and see how the tree changes.";

        //Commands
        public ICommand NavNextCommand { get; }
        public ICommand NavPreviousCommand { get; }
        public ICommand MutateTreeCommand { get; }

        //Constructor
        public GPBasics6ViewModel()
        {
            NavNextCommand = new RelayCommand(NavNext);
            NavPreviousCommand = new RelayCommand(NavPrevious);
            MutateTreeCommand = new RelayCommand(MutateTree);

            bpTemplate.FunctionNodes = new List<FunctionNode> {new FunctionNode("+", 2, a => a[0] + a[1]), new FunctionNode("<", 2, a => a[0] < a[1] ? 1 : 0),
            new FunctionNode("/", 2, a => a[0] / a[1]), new FunctionNode("-", 2, a => a[0] - a[1]), new FunctionNode("*", 2, a => a[0] * a[1]),
            new FunctionNode("ABS", 1, a => Math.Abs(a[0]))};

            bpTemplate.TerminalNodes = new List<TerminalNode> {new TerminalNode("-1", 0, -1, false), new TerminalNode("1", 0, 1, false), new TerminalNode("34", 0, 34, false),
            new TerminalNode("X", 0, 0, true), new TerminalNode("Y", 0, 1, true), new TerminalNode("AB", 0, 2, true) };

            bpTemplate.FunctionRootNodes = new List<FunctionNode> {new FunctionNode("+", 2, a => a[0] + a[1]), new FunctionNode("<", 2, a => a[0] < a[1] ? 1 : 0),
            new FunctionNode(">=", 2, a => a[0] >= a[1] ? 1 : 0), new FunctionNode("-", 2, a => a[0] - a[1])};

            displayTree = bpTemplate.CopyNode(bpTemplate.FunctionRootNodes[random.Next(0, bpTemplate.FunctionRootNodes.Count)]);
            displayTree = bpTemplate.GrowTree(displayTree, random.Next(1, 4));
            TreeDrawingAlgorithm.CalculateNodePositions(displayTree);
            DisplayTreePlot.Clear();
            DisplayTreePlot = AppInfoSingleton.GetTreePlot(DisplayTreePlot, displayTree, BrushSet);
            CanvasHeight = (displayTree.Height * 100) + 100;
            CanvasWidth = (displayTree.Width * 100) + 100;
        }

        //Navigation Functions
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
