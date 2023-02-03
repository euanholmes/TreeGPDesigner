using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Media.Imaging;
using TreeGPDesigner.MVVM.ViewModel;

namespace TreeGPDesigner.MVVM.Model
{
    //Class for a bin packing template which inherits from TreeGP
    public class BinPackingTemplate : TreeGP
    {
        //Bin packing template variables
        private List<FunctionModel> BPWrappersUI = new List<FunctionModel>()
        { new FunctionModel("BP Offline Wrapper", "This is a wrapper for offline bin packing problems. Items are taken in and sorted " +
                                                  "in descending order, then the program tree solution will decide whether to pack the item " +
                                                  "into a bin or not.",
                            new BitmapImage(new Uri("pack://application:,,,/TreeGPDesigner;component/Images/BPOfflineWrapper.png"))),

          new FunctionModel("BP Online Wrapper",  "This is a wrapper for online bin packing problems. Items are not pre sorted and " +
                                                  "are given to the solution tree as is to evaluate.",
                            new BitmapImage(new Uri("pack://application:,,,/TreeGPDesigner;component/Images/BPOnlineWrapper.png")))
        };

        private List<FunctionModel> BPFitnessFunctionsUI = new List<FunctionModel>()
        { new FunctionModel("Minimum Bins", "This is a fitness function which rewards solutions that pack items into bins with the " +
                                            "lower number of bins being awarded higher fitness scores.",
                            new BitmapImage(new Uri("pack://application:,,,/TreeGPDesigner;component/Images/FitnessFunctionOne.png"))),

          new FunctionModel("Minimum Bins + Bin Fill%", "This is a fitness function that builds on fitness function one by also " +
                            "rewarding bin fill percentage.",
              new BitmapImage(new Uri("pack://application:,,,/TreeGPDesigner;component/Images/FitnessFunctionTwo.png")))
        };

        private List<string> BPDatasetsUI = new List<string>()
        {
            "Faulkner Dataset 1 (20 problems | BC:150 | No. Items:120 | Item Sizes:20-100)",
            "Faulkner Dataset 2 (20 problems | BC:150 | No. Items:250 | Item Sizes:20-100 | *Load Time)",
            "Faulkner Dataset 3 (20 problems | BC:150 | No. Items:500 | Item Sizes:20-100 | *Load Time)",
            "Faulkner Dataset 4 (20 problems | BC:150 | No. Items:1000 | Item Sizes:20-100 | *Load Time)",
            "Faulkner Dataset 5 (20 problems | BC:100 | No. Items:60 | Item Sizes:25-50)",
            "Faulkner Dataset 6 (20 problems | BC:100 | No. Items:120 | Item Sizes:25-50)",
            "Faulkner Dataset 7 (20 problems | BC:100 | No. Items:249 | Item Sizes:25-50 | *Load Time)",
            "Faulkner Dataset 8 (20 problems | BC:100 | No. Items:501 | Item Sizes:25-50 | *Load Time)",
            "Random Dataset 1 (10 problems | BC:50-60 | No. Items:10 | Item Sizes:1-BC)",
            "Random Dataset 2 (50 problems | BC:50-60 | No. Items:10 | Item Sizes:1-BC)",
            "Random Dataset 3 (100 problems | BC:50-60 | No. Items:10 | Item Sizes:1-BC)",
            "Random Dataset 4 (200 problems | BC:50-60 | No. Items:10 | Item Sizes:1-BC | *Load Time)",
            "Random Dataset 5 (500 problems | BC:50-60 | No. Items:10 | Item Sizes:1-BC | *Load Time)"
        };

        private static Random random = new();

        private List<List<BPData>> bpDatasets = new();

        //Constructor
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

            
            AddFunctionNode(new FunctionNode("+", 2, "Addition", true, a => a[0] + a[1]));
            AddFunctionNode(new FunctionNode("-", 2, "Subtraction", true, a => a[0] - a[1]));
            AddFunctionNode(new FunctionNode("*", 2, "Multiplication", true, a => a[0] * a[1]));
            AddFunctionNode(new FunctionNode("%", 2, "Protected Divide", true, a => a[1] == 0 ? 1 : a[0] / a[1]));
            AddFunctionNode(new FunctionNode("ABS", 1, "Math.Abs", false, a => Math.Abs(a[0])));
            AddFunctionNode(new FunctionNode("+ +", 3, "3 Operand Addition", false, a => a[0] + a[1] + a[2]));
            AddFunctionNode(new FunctionNode("<=", 2, "Less Than or Equal To", false, a => a[0] <= a[1] ? a[0] : a[1]));

            AddTerminalNode(new TerminalNode("CBW", 0, "Current Bin Weight", true, 0, true));
            AddTerminalNode(new TerminalNode("CI", 0, "Current Item", true, 1, true));
            AddTerminalNode(new TerminalNode("BC", 0, "Bin Capacity", true, 2, true));
            AddTerminalNode(new TerminalNode("FS", 0, "Free Space", true, 0, false, true, a => Convert.ToDouble(a[0]) - Convert.ToDouble(a[1]), new int[] { 2, 0 }));
            AddTerminalNode(new TerminalNode("-1", 0, "-1", true, -1, false));
            AddTerminalNode(new TerminalNode("1", 0, "1", true, 1, false));
            AddTerminalNode(new TerminalNode("0", 0, "0", true, 0, false));
            AddTerminalNode(new TerminalNode("3.14", 0, "3.14", false, 3.14, false));
            AddTerminalNode(new TerminalNode("2BC", 0, "2 X Bin Capacity", false, 0, false, true, a => Convert.ToDouble(a[0]) * 2, new int[] { 2 }));
            AddTerminalNode(new TerminalNode("LB", 0, "On last bin", false, 0, false, true, a => (bool)a[0] == true ? 1 : 0, new int[] { 3 }));
            AddTerminalNode(new TerminalNode("BFB", 0, "Best Fitting Bin", false, 0, false, true, a => (bool)a[0] == true ? 1 : 0, new int[] { 4 }));

            AddRootNode(new FunctionNode("==", 2, "Equals", true, a => a[0] == a[1] ? 1 : 0));
            AddRootNode(new FunctionNode("<=", 2, "Less Than or Equal To", true, a => a[0] <= a[1] ? 1 : 0));
            AddRootNode(new FunctionNode(">=", 2, "Greater Than or Equal To", true, a => a[0] >= a[1] ? 1 : 0));
            AddRootNode(new FunctionNode(">", 2, "Greater Than", true, a => a[0] > a[1] ? 1 : 0));
            AddRootNode(new FunctionNode("<", 2, "Less Than", true, a => a[0] < a[1] ? 1 : 0));
            AddRootNode(new TerminalNode("BFB", 0, "Best Fitting Bin", false, 0, false, true, a => (bool)a[0] == true ? 1 : 0, new int[] { 4 }));


            Node NFTree = MakeNextFitTree();
            Node FFDTree = MakeFirstFitTree();
            Node BFTree = MakeBestFitTree();
            
            KnownAlgorithms.Add(NFTree);
            KnownAlgorithms.Add(FFDTree);
            KnownAlgorithms.Add(BFTree);

            bpDatasets.Add(BPDatasets.FaulknerDataset1);
            bpDatasets.Add(BPDatasets.FaulknerDataset2);
            bpDatasets.Add(BPDatasets.FaulknerDataset3);
            bpDatasets.Add(BPDatasets.FaulknerDataset4);
            bpDatasets.Add(BPDatasets.FaulknerDataset5);
            bpDatasets.Add(BPDatasets.FaulknerDataset6);
            bpDatasets.Add(BPDatasets.FaulknerDataset7);
            bpDatasets.Add(BPDatasets.FaulknerDataset8);

            bpDatasets.Add(GenerateRandomBPDataset(10));
            bpDatasets.Add(GenerateRandomBPDataset(50));
            bpDatasets.Add(GenerateRandomBPDataset(100));
            bpDatasets.Add(GenerateRandomBPDataset(200));
            bpDatasets.Add(GenerateRandomBPDataset(500));

            CurrentDataPoints = "[0] - Current Bin Weight (double)\n[1] - Current Item (double)\n[2] - Bin Capacity (double)\n[3] - On Last Bin (bool)\n" +
                "[4] - Best Fitting Bin (bool)";
        }

        //Function to generate random BP datasets
        public List<BPData> GenerateRandomBPDataset(int num)
        {
            List<BPData> dataset = new();

            for (int i = 0; i < num; i++)
            {
                int binCapacity = random.Next(50, 61);
                List<double> items = new();

                for (int j = 0; j < 5; j++)
                {
                    items.Add(random.Next(10, binCapacity));
                }

                for (int j = 0; j < 5; j++)
                {
                    items.Add(random.Next(1, 11));
                }

                dataset.Add(new BPData(binCapacity, items));
            }
            return dataset;
        }

        //Function to make known algorithms
        public Node MakeFirstFitTree()
        {
            Node FFTree = new FunctionNode("<=", 2, "Less Than or Equal to", true, a => a[0] <= a[1] ? 1 : 0);
            FFTree.ChildNodes.Add(new FunctionNode("+", 2, "Addition", true, a => a[0] + a[1]));
            FFTree.ChildNodes.Add(new TerminalNode("BC", 0, "Bin Capacity", true, 2, true));
            FFTree.ChildNodes[0].ChildNodes.Add(new TerminalNode("CBW", 0, "Current Bin Weight", true, 0, true));
            FFTree.ChildNodes[0].ChildNodes.Add(new TerminalNode("CI", 0, "Current Item", true, 1, true));

            FFTree.Name = "First Fit";

            return FFTree;
        }

        public Node MakeNextFitTree()
        {
            Node NFTree = new FunctionNode(">", 2, "Greater Than", true, a => a[0] > a[1] ? 1 : 0);
            NFTree.ChildNodes.Add(new FunctionNode("*", 2, "Multiplication", true, a => a[0] * a[1]));
            NFTree.ChildNodes.Add(new TerminalNode("0", 0, "0", true, 0, false));
            NFTree.ChildNodes[0].ChildNodes.Add(new FunctionNode("<=", 2, "Less Than or Equal to", true, a => a[0] <= a[1] ? 1 : 0));
            NFTree.ChildNodes[0].ChildNodes.Add(new TerminalNode("LB", 0, "On last bin", true, 0, false, true, a => (bool)a[0] == true ? 1 : 0, new int[] { 3 }));
            NFTree.ChildNodes[0].ChildNodes[0].ChildNodes.Add(new FunctionNode("+", 2, "Addition", true, a => a[0] + a[1]));
            NFTree.ChildNodes[0].ChildNodes[0].ChildNodes.Add(new TerminalNode("BC", 0, "Bin Capacity", true, 2, true));
            NFTree.ChildNodes[0].ChildNodes[0].ChildNodes[0].ChildNodes.Add(new TerminalNode("CBW", 0, "Current Bin Weight", true, 0, true));
            NFTree.ChildNodes[0].ChildNodes[0].ChildNodes[0].ChildNodes.Add(new TerminalNode("CI", 0, "Current Item", true, 1, true));

            NFTree.Name = "Next Fit";

            return NFTree;
        }

        public Node MakeBestFitTree()
        {
            //Node BFTree = new TerminalNode("BFB", 0, "Best Fitting Bin", true, 4, true);
            Node BFTree = new TerminalNode("BFB", 0, "Best Fitting Bin", true, 0, false, true, a => (bool)a[0] == true ? 1 : 0, new int[] { 4 });
            BFTree.Name = "Best Fit";

            return BFTree;
        }

        //Wrapper Functions
        public List<List<double>> BPOfflineWrapper(List<double> origItems, double binCapacity, Node solution)
        {
            List<double> items = new List<double>(origItems);
            List<List<double>> bins = new List<List<double>>();
            bool binFound;
            items.Sort();
            items.Reverse();
            bins.Add(new List<double>());
            while (items.Count > 0 && bins.Count < origItems.Count)
            {
                binFound = false;
                double maxLoad = -1;
                foreach (List<double> checkBin in bins)
                {
                    if ((checkBin.Sum() > maxLoad) && ((checkBin.Sum() + items[0]) <= binCapacity))
                    {
                        maxLoad = checkBin.Sum();
                    }
                }
                for (int i = 0; i < bins.Count; i++)
                {
                    bool currentBin = false;
                    if (bins[i] == bins[bins.Count - 1])
                    {
                        currentBin = true;
                    }
                    bool bestFittingBin = false;
                    if (bins[i].Sum() == maxLoad && maxLoad > -1)
                    {
                        bestFittingBin = true;
                    }
                    object[] data = new object[] { bins[i].Sum(), items[0], binCapacity, currentBin, bestFittingBin };
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
                    bins.Add(new List<double>());
                }
            }
            return bins;
        }

        public List<List<double>> BPOnlineWrapper(List<double> origItems, double binCapacity, Node solution)
        {
            List<double> items = new List<double>(origItems);
            List<List<double>> bins = new List<List<double>>();
            bool binFound;
            bins.Add(new List<double>());
            while (items.Count > 0 && bins.Count < origItems.Count)
            {
                binFound = false;
                double maxLoad = -1;
                foreach (List<double> checkBin in bins)
                {
                    if ((checkBin.Sum() > maxLoad) && ((checkBin.Sum() + items[0]) <= binCapacity))
                    {
                        maxLoad = checkBin.Sum();
                    }
                }
                for (int i = 0; i < bins.Count; i++)
                {
                    bool currentBin = false;
                    if (bins[i] == bins[bins.Count - 1])
                    {
                        currentBin = true;
                    }
                    bool bestFittingBin = false;
                    if (bins[i].Sum() == maxLoad && maxLoad > -1)
                    {
                        bestFittingBin = true;
                    }
                    object[] data = new object[] { bins[i].Sum(), items[0], binCapacity, currentBin, bestFittingBin };
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
                    bins.Add(new List<double>());
                }
            }
            return bins;
        }

        //Fitness functions
        public Node FitnessFunctionOne(Node node, List<double> items, double binCapacity)
        {
            List<List<double>> bins = new List<List<double>>();
            if (CurrentWrapper == 0)
            {
                bins = BPOfflineWrapper(items, binCapacity, node);
            }
            else
            {
                bins = BPOnlineWrapper(items, binCapacity, node);
            }
            double totalBinWeight = 0;
            double totalItemWeight = items.Sum();
            foreach (List<double> bin in bins)
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

        public Node FitnessFunctionTwo(Node node, List<double> items, double binCapacity)
        {
            List<List<double>> bins = new List<List<double>>();
            if (CurrentWrapper == 0)
            {
                bins = BPOfflineWrapper(items, binCapacity, node);
            }
            else
            {
                bins = BPOnlineWrapper(items, binCapacity, node);
            }
            double totalBinWeight = 0;
            double totalItemWeight = items.Sum();
            foreach (List<double> bin in bins)
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
            foreach(List<double> bin in bins)
            {
                if (bin.Sum() == binCapacity)
                {
                    node.Fitness += 1;
                }
            }
            node.Fitness -= bins.Count;
            return node;
        }

        //Function to remove unselected datasets
        public void RemoveUnselectedDatasets()
        {
            List<List<BPData>> chosenDatasets = new();

            if (bpDatasets.Count == CurrentDatasets.Count)
            {
                for (int i = 0; i < CurrentDatasets.Count; i++)
                {
                    if (CurrentDatasets[i])
                    {
                        chosenDatasets.Add(bpDatasets[i]);
                    }
                }
                bpDatasets = chosenDatasets;
            }
        }

        //Function to get the BP population fitness
        public void GetBPPopulationFitness()
        {
            RemoveUnselectedDatasets();

            if (CurrentFitnessFunction == 0)
            {
                foreach (Node node in Generation)
                {
                    node.Fitness = 0;
                    node.NotFailedYet = true;

                    foreach (List<BPData> dataset in bpDatasets)
                    {
                        foreach(BPData data in dataset)
                        {
                            FitnessFunctionOne(node, data.Items, data.BinCapacity);
                        }
                    }
                }

                foreach (Node node in KnownAlgorithms)
                {
                    node.Fitness = 0;
                    node.NotFailedYet = true;
                    foreach (List<BPData> dataset in bpDatasets)
                    {
                        foreach (BPData data in dataset)
                        {
                            FitnessFunctionOne(node, data.Items, data.BinCapacity);
                        }
                    }
                }
            }
            else
            {
                foreach (Node node in Generation)
                {
                    node.Fitness = 0;
                    node.NotFailedYet = true;
                    foreach (List<BPData> dataset in bpDatasets)
                    {
                        foreach (BPData data in dataset)
                        {
                            FitnessFunctionTwo(node, data.Items, data.BinCapacity);
                        }
                    }
                }

                foreach (Node node in KnownAlgorithms)
                {
                    node.Fitness = 0;
                    node.NotFailedYet = true;
                    foreach (List<BPData> dataset in bpDatasets)
                    {
                        foreach (BPData data in dataset)
                        {
                            FitnessFunctionTwo(node, data.Items, data.BinCapacity);
                        }
                    }
                }
            } 
        }

        //Function to make all populations fitnesses positive as the fitness functions take points away past 0.
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

        //Overriden TreeGP method which gets the population fitness
        public override void GetPopulationFitness()
        {
            GetBPPopulationFitness();
            MakeAllFitnessPositive();
        }
    }

    //Class used for BP Datasets which has a variable for bin capacity and a variable for a list of items
    public class BPData
    {
        private double binCapacity;
        private List<double> items;

        public BPData(double binCapacity, List<double> items)
        {
            this.binCapacity = binCapacity;
            this.items = items;
        }

        public double BinCapacity { get => binCapacity; set => binCapacity = value; }
        public List<double> Items { get => items; set => items = value; }
    }
}
