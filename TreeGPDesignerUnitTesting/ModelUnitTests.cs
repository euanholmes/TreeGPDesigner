using System.Diagnostics;
using System.Windows.Media.Imaging;
using TreeGPDesigner.MVVM.Model;
using TreeGPDesigner.MVVM.ViewModel;

namespace TreeGPDesignerUnitTesting
{
    [TestClass]
    public class ModelUnitTests
    {
        [AssemblyInitialize]
        public static void IntialiseTest(TestContext ctx)
        {
            new System.Windows.Application();
        }

        [TestMethod]
        public void MTestAddCustomFunctionNode()
        {
            //Start a new bin packing template
            BinPackingTemplate bpTemplate = new();

            //Add a custom addition function node
            bpTemplate.AddCustomFunctionNode("T+", 2, "Test Addition", "a => a[0] + a[1]");

            //Intialise two terminal nodes
            TerminalNode fourTerminalNode = new TerminalNode("4", 0, 4, false);
            TerminalNode eightTerminalNode = new TerminalNode("8", 0, 8, false);

            //Add the terminal nodes to the custom addition function node
            bpTemplate.FunctionNodes[bpTemplate.FunctionNodes.Count - 1].ChildNodes.Add(fourTerminalNode);
            bpTemplate.FunctionNodes[bpTemplate.FunctionNodes.Count - 1].ChildNodes.Add(eightTerminalNode);

            //Add the terminal nodes to the default addition function node (FunctionNodes[0])
            bpTemplate.FunctionNodes[0].ChildNodes.Add(fourTerminalNode);
            bpTemplate.FunctionNodes[0].ChildNodes.Add(eightTerminalNode);

            //Evaluate the result of the default addition function node and the custom function addition node
            double defaultAdditionNodeResult = bpTemplate.FunctionNodes[0].Eval();
            double testAdditionNodeResult = bpTemplate.FunctionNodes[bpTemplate.FunctionNodes.Count - 1].Eval();

            //Print message of the two results
            Trace.WriteLine($"Default Addition Function Node Result: {defaultAdditionNodeResult}, " +
                $"Test Addition Function Node Result: {testAdditionNodeResult}");

            //Assert that the two results are the same
            Assert.AreEqual(defaultAdditionNodeResult, testAdditionNodeResult);
        }

        [TestMethod]
        public void MTestAddCustomTerminalNode()
        {
            //Start a new bin packing template
            BinPackingTemplate bpTemplate = new();

            //Add custom free space terminal node
            bpTemplate.AddCustomTerminalNode("TFS", "Test Free Space", "a => Convert.ToDouble(a[0]) - Convert.ToDouble(a[1])", new int[] { 2, 0 });

            //Set the data for the test free space terminal node and the default free space terminal node (TerminalNodes[3])
            bpTemplate.TerminalNodes[3].Data = new object[] { 20, 10, 15, true, true };
            bpTemplate.TerminalNodes[bpTemplate.TerminalNodes.Count - 1].Data = new object[] { 20, 10, 15, true, true };

            //Evaluate the result of the default free space terminal node and the test free space terminal node
            double defaultFreeSpaceTerminalNodeResult = bpTemplate.TerminalNodes[3].Eval();
            double testFreeSpaceTerminalNodeResult = bpTemplate.TerminalNodes[bpTemplate.TerminalNodes.Count - 1].Eval();

            //Print message of the two results
            Trace.WriteLine($"Default Free Space Terminal Node Result: {defaultFreeSpaceTerminalNodeResult}, " +
                $"Test Free Space Terminal Node Result: {testFreeSpaceTerminalNodeResult}");

            //Assert that the two results are the same
            Assert.AreEqual(defaultFreeSpaceTerminalNodeResult, testFreeSpaceTerminalNodeResult);
        }

        [TestMethod]
        public void MTestGetLowestKnownAlgorithmFitness()
        {
            //Start a new bin packing template
            BinPackingTemplate bpTemplate = new();

            //Add test known algorithms
            FunctionNode knownAlgorithm1 = new FunctionNode("+", 2, "Addition", true, a => a[0] + a[1]);
            FunctionNode knownAlgorithm2 = new FunctionNode("+", 2, "Addition", true, a => a[0] + a[1]);
            FunctionNode knownAlgorithm3 = new FunctionNode("+", 2, "Addition", true, a => a[0] + a[1]);
            FunctionNode knownAlgorithm4 = new FunctionNode("+", 2, "Addition", true, a => a[0] + a[1]);
            FunctionNode knownAlgorithm5 = new FunctionNode("+", 2, "Addition", true, a => a[0] + a[1]);

            //Set the fitnesses of the test known algorithms
            knownAlgorithm1.Fitness = 80;
            knownAlgorithm2.Fitness = 220;
            knownAlgorithm3.Fitness = 64;
            knownAlgorithm4.Fitness = 370;
            knownAlgorithm5.Fitness = 560;

            //Set the fitnesses of the 3 default known algorithms (next fit, first and best fit) to 100
            bpTemplate.KnownAlgorithms[0].Fitness = 100;
            bpTemplate.KnownAlgorithms[1].Fitness = 100;
            bpTemplate.KnownAlgorithms[2].Fitness = 100;

            //Add the test known algorithms
            bpTemplate.KnownAlgorithms.Add(knownAlgorithm1);
            bpTemplate.KnownAlgorithms.Add(knownAlgorithm2);
            bpTemplate.KnownAlgorithms.Add(knownAlgorithm3);
            bpTemplate.KnownAlgorithms.Add(knownAlgorithm4);
            bpTemplate.KnownAlgorithms.Add(knownAlgorithm5);

            //Call GetLowestKnownAlgorithmFitness();
            bpTemplate.GetLowestKnownAlgorithmFitness();

            //Set variables for the expected and actual lowest known algorithm fitness
            double expectedLowestKnownAlgorithmFitness = 64;
            double actualLowestKnownAlgorithmFitness = bpTemplate.LowestKnownAlgorithmFitness;

            //Print message of the two results
            Trace.WriteLine($"Expected Lowest Known Algorithm Fitness: {expectedLowestKnownAlgorithmFitness}, " +
                $"Actual Lowest Known Algorithm Fitness: {actualLowestKnownAlgorithmFitness}");

            //Assert that the actual and expected lowest known algorithm fitnesses are the same
            Assert.AreEqual(expectedLowestKnownAlgorithmFitness, actualLowestKnownAlgorithmFitness);
        }

        [TestMethod]
        public void MTestGPRun()
        {
            BinPackingTemplate bpTemplate = new();

            /*bpTemplate.CurrentDatasets[0] = false;
            bpTemplate.CurrentDatasets[5] = true;*/

            bpTemplate.GetInitialPopulation();

            Trace.WriteLine($"Wrapper: {bpTemplate.WrappersUI[bpTemplate.CurrentWrapper].Name}");
            Trace.WriteLine($"Fitness Function: {bpTemplate.FitnessFunctionsUI[bpTemplate.CurrentFitnessFunction].Name}");
            Trace.WriteLine($"Function Nodes: (+, -, *, %)   Terminal Nodes: (CBW, CI, BC, FS, 1, -1, 0)   Root Nodes: (==, <=, >=, >, <)");
            Trace.WriteLine($"Training Data: {bpTemplate.DatasetUI[5]}");
            Trace.WriteLine($"Tree Growing Method: {bpTemplate.CurrentTreeGrowingMethod}");
            Trace.WriteLine($"Selection Method: {bpTemplate.CurrentSelectionMethod}");
            Trace.WriteLine($"Population Count: {bpTemplate.CurrentPopulationCount}");
            Trace.WriteLine($"Max Depth: {bpTemplate.CurrentMaxDepth}");
            Trace.WriteLine($"Selection %: {bpTemplate.CurrentSelectionPercent}");
            Trace.WriteLine($"Mutation %: {bpTemplate.CurrentMutationPercent}");
            Trace.WriteLine($"Crossover %: {bpTemplate.CurrentCrossoverPercent}");
            Trace.WriteLine("");
            Trace.WriteLine("Top Algorithm Fitness, Average Fitness Of Generation, Number Of Passing Algorithms");

            Trace.WriteLine($"{bpTemplate.Generation[0].Fitness}, " +
                $"{bpTemplate.Generation.Sum(a => a.Fitness) / bpTemplate.CurrentPopulationCount}, " +
                $"{bpTemplate.Generation.Sum(a => a.NotFailedYet == true ? 1 : 0)}");

            for (int i = 1; i < 11; i++)
            {
                bpTemplate.GetNextGeneration();

                Trace.WriteLine($"{bpTemplate.Generation[0].Fitness}, " +
                    $"{bpTemplate.Generation.Sum(a => a.Fitness) / bpTemplate.CurrentPopulationCount}, " +
                    $"{bpTemplate.Generation.Sum(a => a.NotFailedYet == true ? 1 : 0)}");
            }
        }
    }
}