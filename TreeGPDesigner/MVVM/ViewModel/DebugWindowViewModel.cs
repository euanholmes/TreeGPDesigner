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
using System.Windows.Media.Media3D;
using TreeGPDesigner.MVVM.Model;

namespace TreeGPDesigner.MVVM.ViewModel
{
    public partial class DebugWindowViewModel : ObservableObject
    {
        public static BinPackingTemplate bpTemplate = new();
        public static int testDisplayTreeCount = 0;
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

        public ICommand NextTreeCommand { get; }
        public ICommand MutateCommand { get; }

        [ObservableProperty]
        private float canvasWidth;

        [ObservableProperty]
        private float canvasHeight;

        [ObservableProperty]
        private string debugText = "Debug: ";

        [ObservableProperty]
        private string id = "ID:";

        [ObservableProperty]
        private string fitness = "Fitness:";

        [ObservableProperty]
        private ObservableCollection<NodePlot> currentTreePlot = new ObservableCollection<NodePlot>();

        //[ObservableProperty]
        //private ScaleTransform zoomScaleTransform = new ScaleTransform();

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
            bpTemplate.Mutate(currentTree, maxDepth);
            DrawTree();
            /*CurrentTreePlot.Clear();
            TreeDrawingAlgorithm.CalculateNodePositions(currentTree);
            GetTreePlot(currentTree);*/
        }

        private void DrawTree()
        {
            CurrentTreePlot.Clear();
            currentTree = bpTemplate.Generation[testDisplayTreeCount];
            TreeDrawingAlgorithm.CalculateNodePositions(currentTree);

            CanvasHeight = currentTree.Height * 100 / 2;
            canvasWidth = currentTree.Width * 100 / 2;

            GetTreePlot(currentTree);
            Id = "ID: " + currentTree.Id.ToString();
            //bpTemplate.GetFitness(currentTree)
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
