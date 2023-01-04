﻿using System;
using System.Collections.Generic;
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

        public List<FunctionNode> FunctionNodes { get => functionNodes; set => functionNodes = value; }
        public List<TerminalNode> TerminalNodes { get => terminalNodes; set => terminalNodes = value; }
        public List<FunctionNode> FunctionRootNodes { get => functionRootNodes; set => functionRootNodes = value; }
        public List<TerminalNode> TerminalRootNodes { get => terminalRootNodes; set => terminalRootNodes = value; }
        public List<Node> Generation { get => generation; set => generation = value; }
        public List<Node> GeneticFunctionPool { get => geneticFunctionPool; set => geneticFunctionPool = value; }

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
            else if(TerminalRootNodes.Count == 0 && FunctionRootNodes.Count > 0)
            {
                FunctionNode randomFunctionRootNode = FunctionRootNodes[random.Next(0, FunctionRootNodes.Count)];
                FunctionNode functionRootNode = new FunctionNode(randomFunctionRootNode.Symbol, randomFunctionRootNode.NoOperands,
                    randomFunctionRootNode.Function, randomFunctionRootNode.BooleanFunction);
                return functionRootNode;
            }
            else if(TerminalRootNodes.Count > 0 && FunctionRootNodes.Count == 0)
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

        public void Mutate(Node node, int maxDepth)
        {
            TreeDrawingAlgorithm.CalculateNodePositions(node);
            int mutationNodeDepth = random.Next(1, node.Height + 1);
            Node mutationNode = GetRandomChildNodeAtDepthLevelDown(node, mutationNodeDepth);
            Node newNode;
            int mutateDepth = random.Next(1, maxDepth - mutationNodeDepth + 1);

            if (mutationNode.Height == 0 || mutationNodeDepth == maxDepth)
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

        public void Crossover(Node node1, Node node2, int maxDepth)
        {
            TreeDrawingAlgorithm.CalculateNodePositions(node1);
            int crossoverPoint1 = random.Next(1, node1.Height + 1);
            Node crossoverNode1 = GetRandomChildNodeAtDepthLevelDown(node1, crossoverPoint1);
            int crossoverIndex = crossoverNode1.Parent.ChildNodes.IndexOf(crossoverNode1);
            TreeDrawingAlgorithm.CalculateNodePositions(node2);
            Node newNode;
            int crossoverPoint2 = random.Next(1, node2.Height + 1);
            Node crossoverNode2 = GetRandomChildNodeAtDepthLevelDown(node2, crossoverPoint2);
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
                    functionNode.NotFailedYet, functionNode.Function, functionNode.BooleanFunction);
            }
            else
            {
                TerminalNode terminalNode = (TerminalNode)node;
                copiedNode = new TerminalNode(terminalNode.Symbol, terminalNode.Root, terminalNode.Data, terminalNode.NoOperands, terminalNode.Fitness,
                    terminalNode.NotFailedYet, terminalNode.Value, terminalNode.DataNeeded);
            }

            foreach (Node childNode in node.ChildNodes)
            {
                copiedNode.ChildNodes.Add(CopyNode(childNode));
            }

            return copiedNode;
        }
    }
}
