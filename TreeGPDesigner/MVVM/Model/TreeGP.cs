using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeGPDesigner.MVVM.Model
{
    public abstract class TreeGP
    {
        private List<FunctionNode> functionNodes = new List<FunctionNode>();
        private List<TerminalNode> terminalNodes = new List<TerminalNode>();
        private List<FunctionNode> functionRootNodes = new List<FunctionNode>();
        private List<TerminalNode> terminalRootNodes = new List<TerminalNode>();
        private List<Node> generation = new List<Node>();
        private List<Node> geneticFunctionPool = new List<Node>();
        private static Random random = new Random();
        private List<FunctionModel> wrappersUI;
        private List<FunctionModel> fitnessFunctionsUI;
        private int currentWrapper = 0;
        private int currentFitnessFunction = 0;
        private int currentTreeGrowingMethod = 0;
        private int currentSelectionMethod = 0;
        private int currentPopulationCount = 100;
        private int currentMaxDepth = 3;
        private int currentMinDepth = 1;
        private int currentSelectionPercent = 34;
        private int currentMutationPercent = 10;
        private int currentCrossoverPercent = 90;
        private string currentRunName = "My GP Run";
        private List<string> datasetUI;
        private List<bool> currentDatasets = new List<bool>();
        private List<Node> knownAlgorithms = new List<Node>();
        private int currentGenerationNum = 0;
        private float lowestKnownAlgorithmFitness = float.MaxValue;

        public List<FunctionNode> FunctionNodes { get => functionNodes; set => functionNodes = value; }
        public List<TerminalNode> TerminalNodes { get => terminalNodes; set => terminalNodes = value; }
        public List<FunctionNode> FunctionRootNodes { get => functionRootNodes; set => functionRootNodes = value; }
        public List<TerminalNode> TerminalRootNodes { get => terminalRootNodes; set => terminalRootNodes = value; }
        public List<Node> Generation { get => generation; set => generation = value; }
        public List<Node> GeneticFunctionPool { get => geneticFunctionPool; set => geneticFunctionPool = value; }
        public List<FunctionModel> WrappersUI { get => wrappersUI; set => wrappersUI = value; }
        public int CurrentWrapper { get => currentWrapper; set => currentWrapper = value; }
        public List<FunctionModel> FitnessFunctionsUI { get => fitnessFunctionsUI; set => fitnessFunctionsUI = value; }
        public int CurrentFitnessFunction { get => currentFitnessFunction; set => currentFitnessFunction = value; }
        public int CurrentTreeGrowingMethod { get => currentTreeGrowingMethod; set => currentTreeGrowingMethod = value; }
        public int CurrentSelectionMethod { get => currentSelectionMethod; set => currentSelectionMethod = value; }
        public int CurrentPopulationCount { get => currentPopulationCount; set => currentPopulationCount = value; }
        public int CurrentMaxDepth { get => currentMaxDepth; set => currentMaxDepth = value; }
        public int CurrentSelectionPercent { get => currentSelectionPercent; set => currentSelectionPercent = value; }
        public int CurrentMutationPercent { get => currentMutationPercent; set => currentMutationPercent = value; }
        public int CurrentCrossoverPercent { get => currentCrossoverPercent; set => currentCrossoverPercent = value; }
        public string CurrentRunName { get => currentRunName; set => currentRunName = value; }
        public List<string> DatasetUI { get => datasetUI; set => datasetUI = value; }
        public List<bool> CurrentDatasets { get => currentDatasets; set => currentDatasets = value; }
        public List<Node> KnownAlgorithms { get => knownAlgorithms; set => knownAlgorithms = value; }
        public int CurrentMinDepth { get => currentMinDepth; set => currentMinDepth = value; }
        public int CurrentGenerationNum { get => currentGenerationNum; set => currentGenerationNum = value; }
        public float LowestKnownAlgorithmFitness { get => lowestKnownAlgorithmFitness; set => lowestKnownAlgorithmFitness = value; }

        public void AddFunctionNode(FunctionNode functionNode)
        {
            FunctionNodes.Add(functionNode);
        }

        public void AddTerminalNode(TerminalNode terminalNode)
        {
            TerminalNodes.Add(terminalNode);
        }

        public void AddRootNode(Node rootNode)
        {
            rootNode.Root = true;

            if (rootNode.GetType() == typeof(FunctionNode))
            {
                FunctionRootNodes.Add((FunctionNode)rootNode);
            }
            else
            {
                TerminalRootNodes.Add((TerminalNode)rootNode);
            }
        }

        public TerminalNode GetRandomTerminalNode()
        {
            TerminalNode randomTerminalNode = TerminalNodes[random.Next(0, TerminalNodes.Count)];
            TerminalNode terminalNode = new TerminalNode(randomTerminalNode.Symbol, randomTerminalNode.NoOperands,
                randomTerminalNode.Value, randomTerminalNode.DataNeeded);
            return terminalNode;
        }

        public FunctionNode GetRandomFunctionNode()
        {
            FunctionNode randomFunctionNode = FunctionNodes[random.Next(0, FunctionNodes.Count)];
            FunctionNode functionNode = new FunctionNode(randomFunctionNode.Symbol, randomFunctionNode.NoOperands,
                randomFunctionNode.Function, randomFunctionNode.BooleanFunction);
            return functionNode;
        }

        public Node GetRandomRootNode()
        {
            int randomNum = random.Next(1, 3);

            if (TerminalRootNodes.Count == 0 && FunctionRootNodes.Count == 0)
            {
                return null;
            }
            else if (TerminalRootNodes.Count == 0 && FunctionRootNodes.Count > 0)
            {
                FunctionNode randomFunctionRootNode = FunctionRootNodes[random.Next(0, FunctionRootNodes.Count)];
                FunctionNode functionRootNode = new FunctionNode(randomFunctionRootNode.Symbol, randomFunctionRootNode.NoOperands,
                    randomFunctionRootNode.Function, randomFunctionRootNode.BooleanFunction);
                return functionRootNode;
            }
            else if (TerminalRootNodes.Count > 0 && FunctionRootNodes.Count == 0)
            {
                TerminalNode randomTerminalRootNode = TerminalRootNodes[random.Next(0, TerminalRootNodes.Count)];
                TerminalNode terminalRootNode = new TerminalNode(randomTerminalRootNode.Symbol, randomTerminalRootNode.NoOperands,
                    randomTerminalRootNode.Value, randomTerminalRootNode.DataNeeded);
                return terminalRootNode;
            }
            else
            {
                if (randomNum == 1)
                {
                    TerminalNode randomTerminalRootNode = TerminalRootNodes[random.Next(0, TerminalRootNodes.Count)];
                    TerminalNode terminalRootNode = new TerminalNode(randomTerminalRootNode.Symbol, randomTerminalRootNode.NoOperands,
                        randomTerminalRootNode.Value, randomTerminalRootNode.DataNeeded);
                    return terminalRootNode;
                }
                else
                {
                    FunctionNode randomFunctionRootNode = FunctionRootNodes[random.Next(0, FunctionRootNodes.Count)];
                    FunctionNode functionRootNode = new FunctionNode(randomFunctionRootNode.Symbol, randomFunctionRootNode.NoOperands,
                        randomFunctionRootNode.Function, randomFunctionRootNode.BooleanFunction);
                    return functionRootNode;
                }
            }
        }

        public Node FullTree(Node node, int depth)
        {
            if (depth < 1)
            {
            }
            else if (depth == 1)
            {
                for (int i = 0; i < node.NoOperands; i++)
                {
                    node.ChildNodes.Add(GetRandomTerminalNode());
                }
            }
            else
            {
                for (int i = 0; i < node.NoOperands; i++)
                {
                    node.ChildNodes.Add(GetRandomFunctionNode());
                }

                for (int i = 0; i < node.NoOperands; i++)
                {
                    node.ChildNodes[i] = FullTree(node.ChildNodes[i], depth - 1);
                }
            }
            return node;
        }

        public Node GrowTree(Node node, int depth)
        {
            if (depth < 1 || node == null)
            {
            }
            else if (depth == 1)
            {
                for (int i = 0; i < node.NoOperands; i++)
                {
                    node.ChildNodes.Add(GetRandomTerminalNode());
                }
            }
            else
            {
                for (int i = 0; i < node.NoOperands; i++)
                {
                    int randomNum = random.Next(1, 3);

                    if (randomNum == 1)
                    {
                        node.ChildNodes.Add(GetRandomTerminalNode());
                    }
                    else
                    {
                        node.ChildNodes.Add(GetRandomFunctionNode());
                    }
                }

                for (int i = 0; i < node.NoOperands; i++)
                {
                    if (node.ChildNodes[i].NoOperands > 0)
                    {
                        node.ChildNodes[i] = GrowTree(node.ChildNodes[i], depth - 1);
                    }
                }
            }

            return node;
        }

        public abstract void GetPopulationFitness();

        public void GeneratePopulationAllFull(int populationNum, int minDepth, int maxDepth)
        {
            List<Node> population = new List<Node>();
            int depthLevels = maxDepth - minDepth + 1;
            int depthSplit = Convert.ToInt32(Math.Floor((float)populationNum / depthLevels));
            int lastSplit = depthSplit;

            if (populationNum - (depthSplit * depthLevels) > 0)
            {
                lastSplit += populationNum - (depthSplit * depthLevels);
            }


            if (minDepth < 0 || maxDepth < 0 || depthLevels > populationNum)
            {
            }
            else if (minDepth == 0 && maxDepth == 0)
            {
                for (int i = 0; i < populationNum; i++)
                {
                    population.Add(GetRandomRootNode());
                }
            }
            else
            {
                for (int i = minDepth; i <= maxDepth; i++)
                {
                    if (i == maxDepth)
                    {
                        for (int j = 0; j < lastSplit; j++)
                        {
                            population.Add(FullTree(GetRandomRootNode(), i));
                        }
                    }
                    else
                    {
                        for (int j = 0; j < depthSplit; j++)
                        {
                            population.Add(FullTree(GetRandomRootNode(), i));
                        }
                    }
                }
            }

            Generation = population;
        }

        public void GeneratePopulationAllGrow(int populationNum, int minDepth, int maxDepth)
        {
            List<Node> population = new List<Node>();
            int depthLevels = maxDepth - minDepth + 1;
            int depthSplit = Convert.ToInt32(Math.Floor((float)populationNum / depthLevels));
            int lastSplit = depthSplit;

            if (populationNum - (depthSplit * depthLevels) > 0)
            {
                lastSplit += populationNum - (depthSplit * depthLevels);
            }


            if (minDepth < 0 || maxDepth < 0 || depthLevels > populationNum)
            {
            }
            else if (minDepth == 0 && maxDepth == 0)
            {
                for (int i = 0; i < populationNum; i++)
                {
                    population.Add(GetRandomRootNode());
                }
            }
            else
            {
                for (int i = minDepth; i <= maxDepth; i++)
                {
                    if (i == maxDepth)
                    {
                        for (int j = 0; j < lastSplit; j++)
                        {
                            population.Add(GrowTree(GetRandomRootNode(), i));
                        }
                    }
                    else
                    {
                        for (int j = 0; j < depthSplit; j++)
                        {
                            population.Add(GrowTree(GetRandomRootNode(), i));
                        }
                    }
                }
            }

            Generation = population;
        }

        public void GeneratePopulationRampedHalfAndHalf(int populationNum, int minDepth, int maxDepth)
        {
            List<Node> population = new List<Node>();
            int depthLevels = maxDepth - minDepth + 1;
            int depthSplit = Convert.ToInt32(Math.Floor((float)populationNum / depthLevels));
            int lastSplit = depthSplit;

            if (populationNum - (depthSplit * depthLevels) > 0)
            {
                lastSplit += populationNum - (depthSplit * depthLevels);
            }


            if (minDepth < 0 || maxDepth < 0 || depthLevels > populationNum)
            {
            }
            else if (minDepth == 0 && maxDepth == 0)
            {
                for (int i = 0; i < populationNum; i++)
                {
                    population.Add(GetRandomRootNode());
                }
            }
            else
            {
                for (int i = minDepth; i <= maxDepth; i++)
                {
                    if (i == maxDepth)
                    {
                        for (int j = 0; j < lastSplit; j++)
                        {
                            if (j < Convert.ToInt32(Math.Floor((float)lastSplit / 2)))
                            {
                                population.Add(FullTree(GetRandomRootNode(), i));
                            }
                            else
                            {
                                population.Add(GrowTree(GetRandomRootNode(), i));
                            }
                        }
                    }
                    else
                    {
                        for (int j = 0; j < depthSplit; j++)
                        {
                            if (j < Convert.ToInt32(Math.Floor((float)depthSplit / 2)))
                            {
                                population.Add(FullTree(GetRandomRootNode(), i));
                            }
                            else
                            {
                                population.Add(GrowTree(GetRandomRootNode(), i));
                            }
                        }
                    }
                }
            }

            Generation = population;
        }

        public void GetInitialPopulation()
        {
            RemoveUnselectedNodes();

            if (CurrentTreeGrowingMethod == 0)
            {
                GeneratePopulationRampedHalfAndHalf(CurrentPopulationCount, CurrentMinDepth, CurrentMaxDepth);
            }
            else if (CurrentTreeGrowingMethod == 1)
            {
                GeneratePopulationAllGrow(CurrentPopulationCount, CurrentMinDepth, CurrentMaxDepth);
            }
            else
            {
                GeneratePopulationAllFull(CurrentPopulationCount, CurrentMinDepth, CurrentMaxDepth);
            }

            GetPopulationFitness();
            SortGenerationByFitness();
            GetLowestKnownAlgorithmFitness();
        }

        public void GetNextGeneration()
        {
            Selection();

            int mutationNum = (int)Math.Floor((CurrentMutationPercent / 100d) * GeneticFunctionPool.Count);
            int crossoverNum = (int)Math.Floor((CurrentCrossoverPercent / 100d) * GeneticFunctionPool.Count);

            if ((mutationNum + crossoverNum) < CurrentPopulationCount)
            {
                while ((mutationNum + crossoverNum) < CurrentPopulationCount)
                {
                    crossoverNum++;
                }
            }
            else if ((mutationNum + crossoverNum) > CurrentPopulationCount)
            {
                while ((mutationNum + crossoverNum) > CurrentPopulationCount)
                {
                    if (crossoverNum != 0)
                    {
                        crossoverNum--;
                    }
                    else if (mutationNum != 0)
                    {
                        mutationNum--;
                    }
                    else
                    {
                        crossoverNum = CurrentPopulationCount;
                    }
                }
            }

            ApplyGeneticFunctions(crossoverNum, mutationNum, CurrentMaxDepth);
            GetPopulationFitness();
            SortGenerationByFitness();
            GetLowestKnownAlgorithmFitness();
            currentGenerationNum++;
        }

        public void Selection()
        {
            int selectionNum = (int)Math.Floor((CurrentSelectionPercent / 100d) * CurrentPopulationCount);

            //Trace.WriteLine($"Selection Num = {selectionNum}, CurrentSelectionPercent = {CurrentSelectionPercent}, CurrentPopulationCount = {CurrentPopulationCount}");

            if (selectionNum < 2)
            {
                selectionNum = 2;
            }

            if (currentSelectionMethod == 0)
            {
                TournamentSelection(selectionNum);
            }
            else if (currentSelectionMethod == 1)
            {
                FitnessProportionateSelection(selectionNum);
            }
            else
            {
                TruncationSelection(selectionNum);
            }
        }

        public void SortGenerationByFitness()
        {
            Generation = Generation.OrderByDescending(a => a.Fitness).ToList();
        }

        public void RemoveUnselectedNodes()
        {
            List<FunctionNode> chosenFunctionNodes = new();
            List<TerminalNode> chosenTerminalNodes = new();
            List<FunctionNode> chosenFunctionRootNodes = new();
            List<TerminalNode> chosenTerminalRootNodes = new();

            foreach (FunctionNode node in FunctionNodes)
            {
                if (node.IsSelected)
                {
                    chosenFunctionNodes.Add(node);
                }
            }

            foreach (TerminalNode node in TerminalNodes)
            {
                if (node.IsSelected)
                {
                    chosenTerminalNodes.Add(node);
                }
            }

            foreach (FunctionNode node in FunctionRootNodes)
            {
                if (node.IsSelected)
                {
                    chosenFunctionRootNodes.Add(node);
                }
            }

            foreach (TerminalNode node in TerminalRootNodes)
            {
                if (node.IsSelected)
                {
                    chosenTerminalRootNodes.Add(node);
                }
            }

            FunctionNodes = chosenFunctionNodes;
            TerminalNodes = chosenTerminalNodes;
            FunctionRootNodes = chosenFunctionRootNodes;
            TerminalRootNodes = chosenTerminalRootNodes;
        }

        public void GetLowestKnownAlgorithmFitness()
        {
            Generation = Generation.OrderByDescending(a => a.Fitness).ToList();

            LowestKnownAlgorithmFitness = KnownAlgorithms.OrderByDescending(a => a.Fitness).ToList()[knownAlgorithms.Count - 1].Fitness;
        }

        public Node GetRandomChildNodeAtDepthLevelDown(Node node, int depth)
        {
            if (node.Parent == null && node.ChildNodes.Count == 0)
            {
                return node;
            }
            else if (depth == 0 || node.ChildNodes.Count == 0)
            {
                return node.Parent.ChildNodes[random.Next(0, node.Parent.ChildNodes.Count)];
            }
            else
            {
                return GetRandomChildNodeAtDepthLevelDown(node.ChildNodes[random.Next(0, node.ChildNodes.Count)], depth - 1);
            }
        }

        public Node GetRandomTerminalNodeOfTree(Node node)
        {
            if (node.GetType() == typeof(TerminalNode))
            {
                return node;
            }
            else
            {
                return GetRandomTerminalNodeOfTree(node.ChildNodes[random.Next(0, node.ChildNodes.Count)]);
            }
        }

        public void Mutate(Node node, int maxDepth)
        {
            TreeDrawingAlgorithm.CalculateNodePositions(node);
            int mutationNodeDepth = random.Next(1, node.Height + 1);
            Node mutationNode = GetRandomChildNodeAtDepthLevelDown(node, mutationNodeDepth);
            Node newNode;

            int mutateDepth;

            if (maxDepth - mutationNodeDepth + 1 < 1)
            {
                mutateDepth = 0;
            }
            else
            {
                mutateDepth = random.Next(1, maxDepth - mutationNodeDepth + 1);
            }

            if (mutationNode.Height == 0 || mutationNodeDepth == maxDepth || mutateDepth == 0)
            {
                newNode = GetRandomTerminalNode();
            }
            else
            {
                int functionOrTerminal = random.Next(1, 3);

                if (functionOrTerminal == 1)
                {
                    newNode = GetRandomTerminalNode();
                }
                else
                {
                    newNode = GetRandomFunctionNode();
                    GrowTree(newNode, mutateDepth);
                }
            }

            newNode.Parent = mutationNode.Parent;
            newNode.Parent.ChildNodes[newNode.Parent.ChildNodes.IndexOf(mutationNode)] = newNode;
            TreeDrawingAlgorithm.CalculateNodePositions(node);
        }

        //Not fixed for all cases yet.
        public void Crossover(Node node1, Node node2, int maxDepth)
        {
            TreeDrawingAlgorithm.CalculateNodePositions(node1);
            int crossoverPoint1 = random.Next(1, node1.Height + 1);
            Node crossoverNode1 = GetRandomChildNodeAtDepthLevelDown(node1, crossoverPoint1);
            int crossoverIndex = crossoverNode1.Parent.ChildNodes.IndexOf(crossoverNode1);
            Node newNode;
            Node crossoverNode2;

            if (crossoverNode1.Height + 1 == maxDepth && crossoverNode1.GetType() == typeof(TerminalNode))
            {
                crossoverNode2 = GetRandomTerminalNodeOfTree(node2);
            }
            else
            {
                TreeDrawingAlgorithm.CalculateNodePositions(node2);
                int crossoverPoint2 = random.Next(1, node2.Height + 1);
                crossoverNode2 = GetRandomChildNodeAtDepthLevelDown(node2, crossoverPoint2);
            }

            newNode = CopyNode(crossoverNode2);
            newNode.Parent = crossoverNode1.Parent;
            newNode.Parent.ChildNodes[crossoverIndex] = newNode;
            TreeDrawingAlgorithm.CalculateNodePositions(node1);
        }

        public void TruncationSelection(int selectionNumber)
        {
            Generation = Generation.OrderByDescending(a => a.Fitness).ToList();
            Generation.RemoveRange(selectionNumber, Generation.Count - selectionNumber);
            GeneticFunctionPool = new List<Node>(Generation);
            Generation.Clear();
        }

        public void FitnessProportionateSelection(int selectionNumber)
        {
            TruncationSelection(selectionNumber);
        }

        public void TournamentSelection(int selectionNumber)
        {
            TruncationSelection(selectionNumber);
        }

        public void ApplyGeneticFunctions(int crossoverNumber, int mutationNumber, int maxDepth)
        {
            for (int i = 0; i < crossoverNumber; i++)
            {
                Node parent1 = GeneticFunctionPool[random.Next(0, geneticFunctionPool.Count)];
                Node parent2;
                do
                {
                    parent2 = GeneticFunctionPool[random.Next(0, geneticFunctionPool.Count)];
                } while (parent2 == parent1);

                Node crossoverNode1 = CopyNode(parent1);
                Node crossoverNode2 = CopyNode(parent2);
                TreeDrawingAlgorithm.CalculateNodePositions(crossoverNode1);
                TreeDrawingAlgorithm.CalculateNodePositions(crossoverNode2);
                Crossover(crossoverNode1, crossoverNode2, maxDepth);

                if (!Generation.Contains(parent1))
                {
                    Generation.Add(parent1);
                    i++;
                }

                if (i < crossoverNumber && !Generation.Contains(parent2))
                {
                    Generation.Add(parent2);
                    i++;
                }

                if (i < crossoverNumber)
                {
                    Generation.Add(crossoverNode1);
                }
            }

            for (int i = 0; i < mutationNumber; i++)
            {
                Node originalNode = GeneticFunctionPool[random.Next(0, geneticFunctionPool.Count)];
                Node mutationNode = CopyNode(originalNode);
                Mutate(mutationNode, maxDepth);

                if (!Generation.Contains(originalNode))
                {
                    Generation.Add(originalNode);
                    i++;
                }

                if (i < mutationNumber)
                {
                    Generation.Add(mutationNode);
                }
            }

            GeneticFunctionPool.Clear();
        }

        public Node CopyNode(Node node)
        {
            Node copiedNode;

            if (node.GetType() == typeof(FunctionNode))
            {
                FunctionNode functionNode = (FunctionNode)node;
                copiedNode = new FunctionNode(functionNode.Symbol, functionNode.Root, functionNode.Data, functionNode.NoOperands, functionNode.Fitness,
                    true, functionNode.Function, functionNode.BooleanFunction);
            }
            else
            {
                TerminalNode terminalNode = (TerminalNode)node;
                copiedNode = new TerminalNode(terminalNode.Symbol, terminalNode.Root, terminalNode.Data, terminalNode.NoOperands, terminalNode.Fitness,
                    true, terminalNode.Value, terminalNode.DataNeeded);
            }

            foreach (Node childNode in node.ChildNodes)
            {
                copiedNode.ChildNodes.Add(CopyNode(childNode));
            }

            return copiedNode;
        }
    }
}
