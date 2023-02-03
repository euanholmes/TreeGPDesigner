using System.Windows.Media.Imaging;
using TreeGPDesigner.MVVM.Model;
using TreeGPDesigner.MVVM.ViewModel;

namespace TreeGPDesignerUnitTesting
{
    [TestClass]
    public class TreeGPUnitTests
    {
        [AssemblyInitialize]
        public static void IntialiseTest(TestContext ctx)
        {
            new System.Windows.Application();
        }

        [TestMethod]
        public void TestHomeViewModel()
        {
            //BitmapImage bi = new BitmapImage(new Uri("Images\\BPOnlineWrapper.png"));

            //HomeViewModel hvm = new();
        }

        [TestMethod]
        public void TestGetLowestKnownAlgorithmFitness()
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

            //Assert that the actual and expected lowest known algorithm fitnesses are the same
            Assert.AreEqual(expectedLowestKnownAlgorithmFitness, actualLowestKnownAlgorithmFitness);
        }
    }
}