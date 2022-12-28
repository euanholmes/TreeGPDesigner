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

namespace TreeGPDesigner
{
    public partial class DebugWindowViewModel : ObservableObject
    {
        private List<int> testItems = new List<int>() { 6, 6, 8, 8, 24, 17, 22, 44, 24, 21 };
        private int testBinCapacity = 60;
        private int testBinCapacity2 = 61;
        public static int testDisplayTreeCount = 0;
        public static BinPackingTemplate bpTemplate = new();
        public static int populationCount = 50;
        public static int minDepth = 1;
        public static int maxDepth = 3;
        public static Brush redBrush = Brushes.Red;
        public static Brush pinkBrush = Brushes.Pink;
        public static Brush blueBrush = Brushes.Blue;
        public static Brush lightBlueBrush = Brushes.LightBlue;
        public static CornerRadius fiftyCornerRadius = new CornerRadius(50);
        public static CornerRadius zeroCornerRadius = new CornerRadius(0);
        public static Node currentTree;

        [ObservableProperty]
        private string debugText = "Debug: ";

        public ICommand NextTreeCommand { get; }
        public ICommand MutateCommand { get; }

        [ObservableProperty]
        private string id = "ID:";

        [ObservableProperty]
        private string fitness = "Fitness:";

        [ObservableProperty]
        private ObservableCollection<NodePlot> currentTreePlot = new ObservableCollection<NodePlot>();

        public DebugWindowViewModel()
        {
            NextTreeCommand = new RelayCommand(NextTree);
            MutateCommand = new RelayCommand(MutateTree);


            bpTemplate.AddFunctionNode(new FunctionNode("<=", 2, a => a[0] <= a[1] ? 1 : 0, true));
            bpTemplate.AddFunctionNode(new FunctionNode("+", 2, a => a[0] + a[1], false));
            bpTemplate.AddFunctionNode(new FunctionNode("+3", 3, a => a[0] + a[1] + a[2], false));
            bpTemplate.AddTerminalNode(new TerminalNode("CBW", 0, 0, true));
            bpTemplate.AddTerminalNode(new TerminalNode("CI", 0, 1, true));
            bpTemplate.AddTerminalNode(new TerminalNode("BC", 0, 2, true));
            bpTemplate.AddTerminalNode(new TerminalNode("-1", 0, -1, false));
            bpTemplate.AddRootNode(new FunctionNode("<=", 2, a => a[0] <= a[1] ? 1 : 0, true));

            //Node ffdTree = bpTemplate.MakeFFDTree();
            //bpTemplate.GetFitness(ffdTree, testItems, testBinCapacity);
            //Console.WriteLine($"FFD Tree ID: {ffdTree.Id}. Node Fitness: {ffdTree.Fitness}");
            //TreeDrawingAlgorithm.CalculateNodePositions(ffdTree);

            //Generate population then get fitness of all trees.
            bpTemplate.GeneratePopulationRampedHalfAndHalf(populationCount, minDepth, maxDepth);
            bpTemplate.GetPopulationFitness();
            bpTemplate.MakeAllFitnessPositive();

            //Select trees then apply genetic functions.
            bpTemplate.TruncationSelection(20);
            bpTemplate.ApplyGeneticFunctions(50, 0, maxDepth);
            bpTemplate.GetPopulationFitness();
            bpTemplate.MakeAllFitnessPositive();

            DrawTree();
        }


        private void NextTree()
        {
            testDisplayTreeCount++;

            if (testDisplayTreeCount >= populationCount)
            {
                testDisplayTreeCount = 0;
            }

            DrawTree();
        }

        private void MutateTree()
        {
            CurrentTreePlot.Clear();
            bpTemplate.Mutate(currentTree, maxDepth);
            TreeDrawingAlgorithm.CalculateNodePositions(currentTree);
            GetTreePlot(currentTree);
        }

        private void DrawTree()
        {
            CurrentTreePlot.Clear();
            currentTree = bpTemplate.Generation[testDisplayTreeCount];
            TreeDrawingAlgorithm.CalculateNodePositions(currentTree);
            GetTreePlot(currentTree);
            Id = "ID: " + currentTree.Id.ToString();
            Fitness = "Fitness: " + currentTree.Fitness.ToString();
        }

        private void GetTreePlot(Node node)
        {
            if (node.GetType() == typeof(FunctionNode))
            {
                if (node.Parent == null)
                {
                    NodePlot nodePlot = new NodePlot(node.XPos * 100, node.YPos * 100, 50, 50, pinkBrush, redBrush, fiftyCornerRadius, node.Symbol, 0, 0, 0, 0);
                    CurrentTreePlot.Add(nodePlot);
                }
                else
                {
                    NodePlot nodePlot = new NodePlot(node.XPos * 100, node.YPos * 100, 50, 50, pinkBrush, redBrush, fiftyCornerRadius, node.Symbol,
                        (node.XPos * 100) + 25, (node.YPos * 100) + 25, (node.Parent.XPos * 100) + 25, (node.Parent.YPos * 100) + 25);
                    CurrentTreePlot.Add(nodePlot);
                }
            }
            else
            {
                if (node.Parent == null)
                {
                    NodePlot nodePlot = new NodePlot(node.XPos * 100, node.YPos * 100, 50, 50, lightBlueBrush, blueBrush, zeroCornerRadius, node.Symbol, 0, 0, 0, 0);
                    CurrentTreePlot.Add(nodePlot);
                }
                else
                {
                    NodePlot nodePlot = new NodePlot(node.XPos * 100, node.YPos * 100, 50, 50, lightBlueBrush, blueBrush, zeroCornerRadius, node.Symbol,
                        (node.XPos * 100) + 25, (node.YPos * 100) + 25, (node.Parent.XPos * 100) + 25, (node.Parent.YPos * 100) + 25);
                    CurrentTreePlot.Add(nodePlot);
                }
            }

            foreach (Node childNode in node.ChildNodes)
            {
                GetTreePlot(childNode);
            }
        }
    }
}
