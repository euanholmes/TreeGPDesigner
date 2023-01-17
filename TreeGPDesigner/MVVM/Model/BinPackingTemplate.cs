using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace TreeGPDesigner.MVVM.Model
{
    public class BinPackingTemplate : TreeGP
    {
        private List<FunctionModel> BPWrappersUI = new List<FunctionModel>() 
        { new FunctionModel("BP Offline Wrapper", "This is a wrapper for offline bin packing problems. Items are taken in and sorted " +
                                                  "in descending order, then the program tree solution will decide whether to pack the item " +
                                                  "into a bin or not.", 
                            new BitmapImage(new Uri("pack://application:,,,/Images/BPOfflineWrapperImage.png"))),

          new FunctionModel("BP Online Wrapper",  "This is a wrapper for online bin packing problems. Items are not pre sorted and " +
                                                  "are given to the solution tree as is to evaluate.",
                            new BitmapImage(new Uri("pack://application:,,,/Images/BPOnlineWrapper.png")))
        };

        private List<FunctionModel> BPFitnessFunctionsUI = new List<FunctionModel>()
        { new FunctionModel("Minimum Bins", "This is a fitness function which rewards solutions that pack items into bins with the " +
                                            "lower number of bins being awarded higher fitness scores.",
                            new BitmapImage(new Uri("pack://application:,,,/Images/FitnessFunctionOne.png"))),

          new FunctionModel("Minimum Bins + Bin Fill%", "This is a fitness function that builds on fitness function one by also " +
                            "rewarding bin fill percentage.",
              new BitmapImage(new Uri("pack://application:,,,/Images/FitnessFunctionTwo.png")))
        };

        private List<string> BPDatasetsUI = new List<string>()
        {
            "Random Dataset 1 (10 problems)       ",
            "Random Dataset 2 (50 problems)       ",
            "Random Dataset 3 (100 problems)      ",
            "Random Dataset 4 (200 problems)      ",
            "Random Dataset 5 (500 problems)      "
        };

        public BinPackingTemplate()
        {
            WrappersUI = BPWrappersUI;
            FitnessFunctionsUI = BPFitnessFunctionsUI;
            DatasetUI = BPDatasetsUI;

            CurrentDatasets.Add(true);

            for (int i = 1; i < DatasetUI.Count; i++)
            {
                CurrentDatasets.Add(false);
            }

            AddFunctionNode(new FunctionNode("<=", 2,  "Less Than or Equal To   ", true, a => a[0] <= a[1] ? 1 : 0, true));
            AddFunctionNode(new FunctionNode("+", 2,   "Addition                ", true, a => a[0] + a[1], false));
            AddFunctionNode(new FunctionNode("-", 2,   "Subtraction             ", true, a => a[0] - a[1], false));
            AddFunctionNode(new FunctionNode("*", 2,   "Multiplication          ", true, a => a[0] * a[1], false));
            AddFunctionNode(new FunctionNode("ABS", 1, "Math.Abs                ", true, a => Math.Abs(a[0]), false));
            AddFunctionNode(new FunctionNode("++", 3,  "3 Operand Addition      ", true, a => a[0] + a[1] + a[2], false));
            AddTerminalNode(new TerminalNode("CBW", 0, "Current Bin Weight      ", true, 0, true));
            AddTerminalNode(new TerminalNode("CI", 0,  "Current Item            ", true, 1, true));
            AddTerminalNode(new TerminalNode("BC", 0,  "Bin Capacity            ", true, 2, true));
            AddTerminalNode(new TerminalNode("-1", 0,  "-1                      ", true, -1, false));
            AddTerminalNode(new TerminalNode("1", 0,   "1                       ", true, 1, false));
            AddRootNode(new FunctionNode("<=", 2,      "Less Than or Equal To   ", true, a => a[0] <= a[1] ? 1 : 0, true));
            AddRootNode(new FunctionNode(">=", 2,      "Greater Than or Equal To", true, a => a[0] >= a[1] ? 1 : 0, true));
            AddRootNode(new FunctionNode(">", 2,       "Greater Than            ", true, a => a[0] > a[1] ? 1 : 0, true));
            AddRootNode(new FunctionNode("<", 2,       "Less Than               ", true, a => a[0] < a[1] ? 1 : 0, true));

            Node FFDTree = MakeFFDTree();
            KnownAlgorithms.Add(FFDTree);
        }

        public Node MakeFFDTree()
        {
            Node FFDTree = new FunctionNode("<=", 2, a => a[0] <= a[1] ? 1 : 0, true);
            FFDTree.ChildNodes.Add(new FunctionNode("+", 2, a => a[0] + a[1], false));
            FFDTree.ChildNodes.Add(new TerminalNode("BC", 0, 2, true));
            FFDTree.ChildNodes[0].ChildNodes.Add(new TerminalNode("CBW", 0, 0, true));
            FFDTree.ChildNodes[0].ChildNodes.Add(new TerminalNode("LI", 0, 1, true));

            FFDTree.Name = "FFD";

            return FFDTree;
        }

        public List<List<int>> BPOfflineWrapper(List<int> origItems, int binCapacity, Node solution)
        {
            List<int> items = new List<int>(origItems);
            List<List<int>> bins = new List<List<int>>();
            bool binFound;
            items.Sort();
            items.Reverse();
            bins.Add(new List<int>());

            while (items.Count > 0 && bins.Count < origItems.Count)
            {
                binFound = false;

                for (int i = 0; i < bins.Count; i++)
                {
                    int[] data = new int[] { bins[i].Sum(), items[0], binCapacity };
                    solution.SetDataAll(data);

                    if (solution.Eval() == 1)
                    {
                        bins[i].Add(items[0]);
                        items.RemoveAt(0);
                        binFound = true;
                        break;
                    }
                }
                if (binFound == false)
                {
                    bins.Add(new List<int>());
                }
            }
            return bins;
        }

        public List<List<int>> BPOnlineWrapper(List<int> origItems, int binCapacity, Node solution)
        {
            List<int> items = new List<int>(origItems);
            List<List<int>> bins = new List<List<int>>();
            bool binFound;
            bins.Add(new List<int>());

            while (items.Count > 0 && bins.Count < origItems.Count)
            {
                binFound = false;

                for (int i = 0; i < bins.Count; i++)
                {
                    int[] data = new int[] { bins[i].Sum(), items[0], binCapacity };
                    solution.SetDataAll(data);

                    if (solution.Eval() == 1)
                    {
                        bins[i].Add(items[0]);
                        items.RemoveAt(0);
                        binFound = true;
                        break;
                    }
                }
                if (binFound == false)
                {
                    bins.Add(new List<int>());
                }
            }
            return bins;
        }

        private void PrintItems(List<int> items)
        {
            Console.Write("Items: ");

            for (int i = 0; i < items.Count; i++)
            {
                Console.Write(items[i] + " ");
            }
        }

        private void PrintBins(List<List<int>> bins)
        {

            for (int i = 0; i < bins.Count; i++)
            {
                Console.Write("\nBin " + (i + 1) + ": ");

                for (int x = 0; x < bins[i].Count; x++)
                {
                    Console.Write(bins[i][x] + " ");
                }
            }

            Console.Write("\n");
        }

        public Node FitnessFunctionOne(Node node, List<int> items, int binCapacity)
        {
            List<List<int>> bins = new List<List<int>>();
            bins = BPOfflineWrapper(items, binCapacity, node);
            int totalBinWeight = 0;
            int totalItemWeight = items.Sum();

            foreach (List<int> bin in bins)
            {
                totalBinWeight += bin.Sum();

                if (bin.Sum() > binCapacity || bin.Sum() == 0)
                {
                    node.Fitness -= 100;
                    node.NotFailedYet = false;
                }
            }

            if (bins[0].Sum() == totalItemWeight)
            {
                node.Fitness -= 100 * (items.Count - 2);
            }

            if (totalBinWeight != totalItemWeight)
            {
                node.Fitness -= 100;
                node.NotFailedYet = false;
            }

            node.Fitness -= bins.Count;
            return node;
        }

        public Node FitnessFunctionTwo(Node node, List<int> items, int binCapacity)
        {
            List<List<int>> bins = new List<List<int>>();
            bins = BPOfflineWrapper(items, binCapacity, node);
            int totalBinWeight = 0;
            int totalItemWeight = items.Sum();

            foreach (List<int> bin in bins)
            {
                totalBinWeight += bin.Sum();

                if (bin.Sum() > binCapacity || bin.Sum() == 0)
                {
                    node.Fitness -= 100;
                    node.NotFailedYet = false;
                }
            }

            if (bins[0].Sum() == totalItemWeight)
            {
                node.Fitness -= 100 * (items.Count - 2);
            }

            if (totalBinWeight != totalItemWeight)
            {
                node.Fitness -= 100;
                node.NotFailedYet = false;
            }

            foreach(List<int> bin in bins)
            {
                if (bin.Sum() == binCapacity)
                {
                    node.Fitness += 1;
                }
            }

            node.Fitness -= bins.Count;
            return node;
        }

        public void GetBPPopulationFitness()
        {
            List<int> testItems = new List<int>() { 6, 6, 8, 8, 24, 17, 22, 44, 24, 21 };
            int testBinCapacity = 60;

            if (CurrentFitnessFunction == 0)
            {
                foreach (Node node in Generation)
                {
                    node.Fitness = 0;
                    node.NotFailedYet = true;
                    FitnessFunctionOne(node, testItems, testBinCapacity);
                }

                foreach (Node node in KnownAlgorithms)
                {
                    node.Fitness = 0;
                    node.NotFailedYet = true;
                    FitnessFunctionOne(node, testItems, testBinCapacity);
                }
            }
            else
            {
                foreach (Node node in Generation)
                {
                    node.Fitness = 0;
                    node.NotFailedYet = true;
                    FitnessFunctionTwo(node, testItems, testBinCapacity);
                }

                foreach (Node node in KnownAlgorithms)
                {
                    node.Fitness = 0;
                    node.NotFailedYet = true;
                    FitnessFunctionTwo(node, testItems, testBinCapacity);
                }
            } 
        }

        public void MakeAllFitnessPositive()
        {
            float adjustNumber = 0;

            foreach (Node node in Generation)
            {
                if (-node.Fitness > adjustNumber)
                {
                    adjustNumber = -node.Fitness;
                    Console.WriteLine($"Adjust Number: {adjustNumber}");
                }
            }

            foreach (Node node in Generation)
            {
                node.Fitness = adjustNumber + node.Fitness;
            }

            foreach (Node node in KnownAlgorithms)
            {
                node.Fitness = adjustNumber + node.Fitness;
            }
        }

        public override void GetPopulationFitness()
        {
            GetBPPopulationFitness();
            MakeAllFitnessPositive();
        }
    }
}
