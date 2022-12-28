using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeGPDesigner.MVVM.Model
{
    public class BinPackingTemplate : TreeGP
    {
        public BinPackingTemplate()
        {
        }

        public Node MakeFFDTree()
        {
            Node FFDTree = new FunctionNode("<=", 2, a => a[0] <= a[1] ? 1 : 0, true);
            FFDTree.ChildNodes.Add(new FunctionNode("+", 2, a => a[0] + a[1], false));
            FFDTree.ChildNodes.Add(new TerminalNode("BC", 0, 2, true));
            FFDTree.ChildNodes[0].ChildNodes.Add(new TerminalNode("CBW", 0, 0, true));
            FFDTree.ChildNodes[0].ChildNodes.Add(new TerminalNode("LI", 0, 1, true));

            return FFDTree;
        }

        public List<List<int>> BPOfflineWrapper(List<int> origItems, int binCapacity, Node solution)
        {
            Console.WriteLine("BP Offline Wrapper starting...");
            List<int> items = new List<int>(origItems);
            List<List<int>> bins = new List<List<int>>();
            bool binFound;
            items.Sort();
            items.Reverse();
            PrintItems(items);
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
            PrintBins(bins);
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

        public Node GetFitness(Node node, List<int> items, int binCapacity)
        {
            List<List<int>> bins = new List<List<int>>();

            Console.WriteLine($"Checking Node {node.Id} fitness:");

            bins = BPOfflineWrapper(items, binCapacity, node);

            int totalBinWeight = 0;
            int totalItemWeight = items.Sum();

            //Rewards programs that pack bins at all vs programs that don't pack any bins.
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

        public void GetPopulationFitness()
        {
            List<int> testItems = new List<int>() { 6, 6, 8, 8, 24, 17, 22, 44, 24, 21 };
            int testBinCapacity = 60;

            foreach (Node node in Generation)
            {
                GetFitness(node, testItems, testBinCapacity);
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
        }
    }
}
