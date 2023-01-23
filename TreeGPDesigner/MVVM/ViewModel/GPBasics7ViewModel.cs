using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;
using TreeGPDesigner.MVVM.Model;

namespace TreeGPDesigner.MVVM.ViewModel
{
    //Viewmodel class for GP Basics 7 View
    public partial class GPBasics7ViewModel : ViewModelBase
    {
        //GP Basics 7 Variables
        private BinPackingTemplate bpTemplate = new();

        private Node displayTree;

        private Node parentTree1 = new FunctionNode("+", 2, a => a[0] + a[1]); 

        private Node parentTree2 = new FunctionNode(">=", 2, a => a[0] >= a[1] ? 1 : 0);

        [ObservableProperty]
        private string informationText = "Crossover is a genetic function that takes two parent trees and combines parts " +
            "of them to make an entirely new program tree. Unlike mutation " +
            "which only applies to one tree crossover requires two parents. For crossover to occur two crossover points are " +
            "required, one for each tree. The crossover point of parent 1 will have a branch cut off and be replaced with a " +
            "branch from parent 2. In most GP applications crossover is considered more important than mutation and therefore " +
            "used at a much higher percentage typically around 90% of selected programs will be used in crossover with the " +
            "remaining being mutated.\n\nTry using the crossover operation on the two parent trees to the left.\n\nWith that you " +
            "are now ready to start your own GP runs by selecting the \"Start a GP Run\" button in the main menu. :)";

        //Commands
        public ICommand NavPreviousCommand { get; }
        public ICommand CrossoverTreesCommand { get; }

        //Constructor
        public GPBasics7ViewModel()
        {
            NavPreviousCommand = new RelayCommand(NavPrevious);
            CrossoverTreesCommand = new RelayCommand(CrossoverTrees);

            parentTree1.ChildNodes.Add(new FunctionNode(">", 2, a => a[0] > a[1] ? 1 : 0));
            parentTree1.ChildNodes.Add(new TerminalNode("-1", 0, -1, false));
            parentTree1.ChildNodes[0].ChildNodes.Add(new TerminalNode("1", 0, 1, false));
            parentTree1.ChildNodes[0].ChildNodes.Add(new TerminalNode("X", 0, 1, true));
            parentTree2.ChildNodes.Add(new FunctionNode("-", 2, a => a[0] - a[1]));
            parentTree2.ChildNodes.Add(new TerminalNode("2Y", 0, 1, true));
            parentTree2.ChildNodes[0].ChildNodes.Add(new TerminalNode("AB", 0, 1, true));
            parentTree2.ChildNodes[0].ChildNodes.Add(new TerminalNode("34", 0, -1, false));
        }

        //Navigation Functions
        public void NavPrevious()
        {
            AppInfoSingleton.Instance.CurrentViewModel = new GPBasics6ViewModel();
        }

        //GP Basics 7 Functions
        public void CrossoverTrees()
        {
            Node parentTree1Copy = bpTemplate.CopyNode(parentTree1);
            bpTemplate.Crossover(parentTree1Copy, parentTree2, 2);
            displayTree = bpTemplate.CopyNode(parentTree1Copy);
            TreeDrawingAlgorithm.CalculateNodePositions(displayTree);
            DisplayTreePlot.Clear();
            DisplayTreePlot = AppInfoSingleton.GetTreePlot(DisplayTreePlot, displayTree, BrushSet);
            CanvasHeight = (displayTree.Height * 100) + 100;
            CanvasWidth = (displayTree.Width * 100) + 100;
        }
    }
}
