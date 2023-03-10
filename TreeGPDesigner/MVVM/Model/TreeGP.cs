using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TreeGPDesigner.MVVM.ViewModel;

namespace TreeGPDesigner.MVVM.Model
{
    //Abstract TreeGP class which will be inherited by all GP templates in the app. Provides GP functions for selection, tree growing etc.
    public abstract class TreeGP
    {
        //TreeGP private variables
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
        private int currentSelectionPercent = 40;
        private int currentMutationPercent = 10;
        private int currentCrossoverPercent = 90;
        private string currentRunName = "My GP Run";
        private List<string> datasetUI;
        private List<bool> currentDatasets = new List<bool>();
        private List<Node> knownAlgorithms = new List<Node>();
        private int currentGenerationNum = 0;
        private float lowestKnownAlgorithmFitness = float.MaxValue;
        private string currentDataPoints = "";

        //Getters and setters for private variables
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
        public string CurrentDataPoints { get => currentDataPoints; set => currentDataPoints = value; }

        //Functions using the Microsoft.CodeAnalysis.CSharp.Scripting package to convert strings to lambda expressions.
        //To add custom nodes.
        public async void AddCustomFunctionNode(string customSymbol, int customNoOperands, string customNodeDescription, string customFunctionString)
        {
            var options = ScriptOptions.Default.AddImports("System");

            try 
            {
                Func<double[], double> customFunction = await CSharpScript.EvaluateAsync<Func<double[], double>>(customFunctionString, options);
                AddFunctionNode(new FunctionNode(customSymbol, customNoOperands, customNodeDescription, true, customFunction));
            }
            catch { }
        }

        public async void AddCustomRootFunctionNode(string customSymbol, int customNoOperands, string customNodeDescription, string customFunctionString)
        {
            var options = ScriptOptions.Default.AddImports("System");

            try
            {
                Func<double[], double> customFunction = await CSharpScript.EvaluateAsync<Func<double[], double>>(customFunctionString, options);
                AddRootNode(new FunctionNode(customSymbol, customNoOperands, customNodeDescription, true, customFunction));
            }
            catch { }
        }

        public async void AddCustomTerminalNode(string customSymbol, string customNodeDescription, string customTerminalFunctionString, int[] customTerminalFunctionData)
        {
            var options = ScriptOptions.Default.AddImports("System");

            try
            {
                Func<object[], double> customTerminalFunction = await CSharpScript.EvaluateAsync<Func<object[], double>>(customTerminalFunctionString, options);
                AddTerminalNode(new TerminalNode(customSymbol, 0, customNodeDescription, true, 0, false, true, customTerminalFunction, customTerminalFunctionData));
            }
            catch { }
            
        }
        public async void AddCustomRootTerminalNode(string customSymbol, string customNodeDescription, string customTerminalFunctionString, int[] customTerminalFunctionData)
        {
            var options = ScriptOptions.Default.AddImports("System");

            try
            {
                Func<object[], double> customTerminalFunction = await CSharpScript.EvaluateAsync<Func<object[], double>>(customTerminalFunctionString, options);
                AddRootNode(new TerminalNode(customSymbol, 0, customNodeDescription, true, 0, false, true, customTerminalFunction, customTerminalFunctionData));
            }
            catch { } 
        }

        //Add node functions.
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

        //Get random node functions
        public TerminalNode GetRandomTerminalNode()
        {
            TerminalNode randomTerminalNode = TerminalNodes[random.Next(0, TerminalNodes.Count)];
            TerminalNode terminalNode = new TerminalNode(randomTerminalNode.Symbol, randomTerminalNode.NoOperands, randomTerminalNode.NodeDescription, 
                randomTerminalNode.IsSelected,randomTerminalNode.Value, randomTerminalNode.DataNeeded, randomTerminalNode.FunctionNeeded, 
                randomTerminalNode.TerminalFunction, randomTerminalNode.TerminalFunctionData);
            return terminalNode;
        }

        public FunctionNode GetRandomFunctionNode()
        {
            FunctionNode randomFunctionNode = FunctionNodes[random.Next(0, FunctionNodes.Count)];
            FunctionNode functionNode = new FunctionNode(randomFunctionNode.Symbol, randomFunctionNode.NoOperands, randomFunctionNode.NodeDescription,
                randomFunctionNode.IsSelected, randomFunctionNode.Function);
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
                FunctionNode functionRootNode = new FunctionNode(randomFunctionRootNode.Symbol, randomFunctionRootNode.NoOperands, randomFunctionRootNode.NodeDescription,
                    randomFunctionRootNode.IsSelected, randomFunctionRootNode.Function);
                return functionRootNode;
            }
            else if (TerminalRootNodes.Count > 0 && FunctionRootNodes.Count == 0)
            {
                TerminalNode randomTerminalRootNode = TerminalRootNodes[random.Next(0, TerminalRootNodes.Count)];
                TerminalNode terminalRootNode = new TerminalNode(randomTerminalRootNode.Symbol, randomTerminalRootNode.NoOperands, randomTerminalRootNode.NodeDescription, 
                    randomTerminalRootNode.IsSelected, randomTerminalRootNode.Value, randomTerminalRootNode.DataNeeded, randomTerminalRootNode.FunctionNeeded,
                    randomTerminalRootNode.TerminalFunction, randomTerminalRootNode.TerminalFunctionData);
                return terminalRootNode;
            }
            else
            {
                if (randomNum == 1)
                {
                    TerminalNode randomTerminalRootNode = TerminalRootNodes[random.Next(0, TerminalRootNodes.Count)];
                    TerminalNode terminalRootNode = new TerminalNode(randomTerminalRootNode.Symbol, randomTerminalRootNode.NoOperands, randomTerminalRootNode.NodeDescription,
                        randomTerminalRootNode.IsSelected, randomTerminalRootNode.Value, randomTerminalRootNode.DataNeeded, randomTerminalRootNode.FunctionNeeded,
                        randomTerminalRootNode.TerminalFunction, randomTerminalRootNode.TerminalFunctionData);
                    return terminalRootNode;
                }
                else
                {
                    FunctionNode randomFunctionRootNode = FunctionRootNodes[random.Next(0, FunctionRootNodes.Count)];
                    FunctionNode functionRootNode = new FunctionNode(randomFunctionRootNode.Symbol, randomFunctionRootNode.NoOperands, randomFunctionRootNode.NodeDescription,
                        randomFunctionRootNode.IsSelected, randomFunctionRootNode.Function);
                    return functionRootNode;
                }
            }
        }

        //Function which grows a "full" tree
        public Node FullTree(Node node, int depth)
        {
            if (depth < 1)
            {
            }
            else if (depth == 1)
            {
                for (int i = 0; i < node.NoOperands; i++)
                {
                    TerminalNode randomTerminalNode = GetRandomTerminalNode();
                    randomTerminalNode.DepthLevel = node.DepthLevel++;
                    node.ChildNodes.Add(randomTerminalNode);
                }
            }
            else
            {
                for (int i = 0; i < node.NoOperands; i++)
                {
                    if (FunctionNodes.Count > 0)
                    {
                        FunctionNode randomFunctionNode = GetRandomFunctionNode();
                        randomFunctionNode.DepthLevel = node.DepthLevel++;
                        node.ChildNodes.Add(randomFunctionNode);
                    }
                    else
                    {
                        TerminalNode randomTerminalNode = GetRandomTerminalNode();
                        randomTerminalNode.DepthLevel = node.DepthLevel++;
                        node.ChildNodes.Add(randomTerminalNode);
                    }
                }

                for (int i = 0; i < node.NoOperands; i++)
                {
                    node.ChildNodes[i] = FullTree(node.ChildNodes[i], depth - 1);
                }
            }
            return node;
        }

        //Function which grows a "grow" tree
        public Node GrowTree(Node node, int depth)
        {
            if (depth < 1 || node == null)
            {
            }
            else if (depth == 1)
            {
                for (int i = 0; i < node.NoOperands; i++)
                {
                    TerminalNode randomTerminalNode = GetRandomTerminalNode();
                    randomTerminalNode.DepthLevel = node.DepthLevel++;
                    node.ChildNodes.Add(randomTerminalNode);
                }
            }
            else
            {
                for (int i = 0; i < node.NoOperands; i++)
                {
                    int randomNum = random.Next(1, 3);

                    if (randomNum == 1)
                    {
                        TerminalNode randomTerminalNode = GetRandomTerminalNode();
                        randomTerminalNode.DepthLevel = node.DepthLevel++;
                        node.ChildNodes.Add(randomTerminalNode);
                    }
                    else
                    {
                        if (FunctionNodes.Count > 0)
                        {
                            FunctionNode randomFunctionNode = GetRandomFunctionNode();
                            randomFunctionNode.DepthLevel = node.DepthLevel++;
                            node.ChildNodes.Add(randomFunctionNode);
                        }
                        else
                        {
                            TerminalNode randomTerminalNode = GetRandomTerminalNode();
                            randomTerminalNode.DepthLevel = node.DepthLevel++;
                            node.ChildNodes.Add(randomTerminalNode);
                        }
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

        //Abstract function inherited by all templates to calculate a populations fitness
        public abstract void GetPopulationFitness();

        //Generates a population all with the "full" method
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

        //Generates a population all with the "grow" method
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

        //Generates a population with the ramped half and half method
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

        //Gets an intial population depending on the selected tree growing method
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

        //Gets the next generation of programs
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

        //Selection function depending on current selection method
        public void Selection()
        {
            int selectionNum = (int)Math.Floor((CurrentSelectionPercent / 100d) * CurrentPopulationCount);

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

        //Function to sort generation by fitness
        public void SortGenerationByFitness()
        {
            Generation = Generation.OrderByDescending(a => a.Fitness).ToList();
            //KnownAlgorithms = KnownAlgorithms.OrderByDescending(a => a.Fitness).ToList();
        }

        //Function to remove unchecked nodes
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

        //Function to get the lowest known algorithm fitness
        public void GetLowestKnownAlgorithmFitness()
        {
            Generation = Generation.OrderByDescending(a => a.Fitness).ToList();
            LowestKnownAlgorithmFitness = KnownAlgorithms.OrderByDescending(a => a.Fitness).ToList()[knownAlgorithms.Count - 1].Fitness;
        }

        //Function which gets a random node in a tree
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

        //Function which gets a random terminal node in a tree
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

        //Mutation function
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

        //Crossover function
        //Not fixed for all cases yet.
        public void Crossover(Node node1, Node node2, int maxDepth)
        {
            TreeDrawingAlgorithm.CalculateNodePositions(node1);
            int crossoverPoint1 = random.Next(1, node1.Height + 1);
            Node crossoverNode1 = GetRandomChildNodeAtDepthLevelDown(node1, crossoverPoint1);
            int crossoverIndex = crossoverNode1.Parent.ChildNodes.IndexOf(crossoverNode1);
            Node newNode;
            Node crossoverNode2;

            if (crossoverNode1.DepthLevel == maxDepth/* && crossoverNode1.GetType() == typeof(TerminalNode)*/)
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

        //Truncation selection function
        public void TruncationSelection(int selectionNumber)
        {
            Generation = Generation.OrderByDescending(a => a.Fitness).ToList();
            Generation.RemoveRange(selectionNumber, Generation.Count - selectionNumber);
            GeneticFunctionPool = new List<Node>(Generation);
            Generation.Clear();
        }

        //Fitness proportionate selection function 
        public void FitnessProportionateSelection(int selectionNumber)
        {
            for (int i = 0; i < selectionNumber; i++)
            {
                double fitnessSum = 0.0;

                for (int j = 0; j < Generation.Count; j++)
                {
                    fitnessSum += Generation[j].Fitness;
                }

                double accum = 0.0;
                double p = random.NextDouble();

                for (int j = 0; j < Generation.Count; j++)
                {
                    accum += (Generation[j].Fitness / fitnessSum);

                    if (p < accum)
                    {
                        GeneticFunctionPool.Add(Generation[j]);
                        Generation.Remove(Generation[j]);
                        break;
                    }

                    if (j == Generation.Count)
                    {
                        GeneticFunctionPool.Add(Generation[j]);
                        Generation.Remove(Generation[j]);
                        break;
                    }
                }
            }
            Generation.Clear();
        }

        //Tournament selection function
        public void TournamentSelection(int selectionNumber)
        {
            int tournamentSelectionNum = (int)Math.Floor((60 / 100d) * CurrentPopulationCount);
            List<Node> tournament = new();

            for (int i = 0; i < selectionNumber; i++)
            {
                tournament = new List<Node>(Generation);
                tournament = tournament.OrderBy(a => random.Next()).ToList();

                if (tournamentSelectionNum < Generation.Count)
                {
                    tournament.RemoveRange(tournamentSelectionNum, tournament.Count - tournamentSelectionNum);
                }

                tournament = tournament.OrderByDescending(a => a.Fitness).ToList();
                GeneticFunctionPool.Add(tournament[0]);
                Generation.Remove(tournament[0]);
            }

            tournament.Clear();
            Generation.Clear();
        }

        //Function which applies genetic functions mutation and crossover
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

        //Copy node function which creates a new node with all the same information
        public Node CopyNode(Node node)
        {
            Node copiedNode;

            if (node.GetType() == typeof(FunctionNode))
            {
                FunctionNode functionNode = (FunctionNode)node;
                copiedNode = new FunctionNode(functionNode.Symbol, functionNode.Root, functionNode.Data, functionNode.NoOperands, functionNode.Fitness,
                    true, functionNode.NodeDescription, functionNode.Function);
            }
            else
            {
                TerminalNode terminalNode = (TerminalNode)node;
                copiedNode = new TerminalNode(terminalNode.Symbol, terminalNode.Root, terminalNode.Data, terminalNode.NoOperands, terminalNode.Fitness,
                    true, terminalNode.NodeDescription, terminalNode.Value, terminalNode.DataNeeded, terminalNode.FunctionNeeded, terminalNode.TerminalFunction, 
                    terminalNode.TerminalFunctionData);
            }

            foreach (Node childNode in node.ChildNodes)
            {
                copiedNode.ChildNodes.Add(CopyNode(childNode));
            }

            return copiedNode;
        }
    }
}
