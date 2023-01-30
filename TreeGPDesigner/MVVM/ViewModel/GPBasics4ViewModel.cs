using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Media;
using TreeGPDesigner.MVVM.Model;

namespace TreeGPDesigner.MVVM.ViewModel
{
    //Viewmodel for GP Basics 4 View
    public partial class GPBasics4ViewModel : ViewModelBase
    {
        //GP Basics 4 Variables
        private static Random random = new();

        private BinPackingTemplate bpTemplate = new();

        private Node currentTree;

        [ObservableProperty]
        private string informationText = "Now that you have a population of programs as well as wrappers and datasets to use with them you can start assessing the " +
            "fitness of these programs. Assessing the fitness of programs is important to see firstly which programs complete the task correctly and " +
            "secondly which programs do it the best. Using bin packing as an example again, a bin packing fitness function would reward programs that firstly put items into bins " +
            "not letting the total item weight exceed the bin capacity and not giving empty bins in a solution. The programs that pack bins correctly will " +
            "then be rewarded further for packing the items into less bins.\n\nTry using the fitness function to the right with some " +
            "randomly generated program trees. The fitness indicator will go green when a program packs bins correctly and red if it doesn't. You may notice that " +
            "most programs fail to pack the bins correctly but fail in different ways. The bin capacity for the problem used is 60 while the items are: " +
            "6, 6, 8, 8, 24, 17, 22, 44, 24, 21. Terminal nodes include: current bin weight (CBW), current item (CI) and bin capacity (BC).";

        [ObservableProperty]
        private ObservableCollection<Bin> bins = new();

        [ObservableProperty]
        private string treeFitness = "Fitness = ";

        [ObservableProperty]
        private Brush colourIndicator = Brushes.Gray;

        //Commands
        public ICommand NavPreviousCommand { get; }
        public ICommand GetFitnessCommand { get; }
        public ICommand NewTreeCommand { get; }

        //Constructor
        public GPBasics4ViewModel()
        {
            NavPreviousCommand = new RelayCommand(NavPrevious);
            GetFitnessCommand = new RelayCommand(GetFitness);
            NewTreeCommand = new RelayCommand(NewTree);
        }

        //Navigation Functions
        public void NavPrevious()
        {
            AppInfoSingleton.Instance.CurrentViewModel = new GPBasics3ViewModel();
        }

        //GP Basics 4 Functions
        public void GetFitness()
        {
            if (currentTree != null)
            {
                currentTree.Fitness = 0;
                currentTree.NotFailedYet = true;
                bpTemplate.FitnessFunctionOne(currentTree, new List<double>() { 6, 6, 8, 8, 24, 17, 22, 44, 24, 21 }, 60);
                string fitnessNumString = (11 + currentTree.Fitness).ToString();
                TreeFitness = "Fitness = " + fitnessNumString;
                List<List<double>> rawBins = bpTemplate.BPOfflineWrapper(new List<double>() { 6, 6, 8, 8, 24, 17, 22, 44, 24, 21 }, 60, currentTree);
                GPBasics3ViewModel vmgp3 = new();
                Bins.Clear();
                Bins = vmgp3.GetBinList(rawBins);

                if (currentTree.NotFailedYet)
                {
                    ColourIndicator = Brushes.Green;
                }
                else
                {
                    ColourIndicator = Brushes.Red;
                }
            }
        }

        public void NewTree()
        {
            TreeFitness = "Fitness = ";
            ColourIndicator = Brushes.Gray;
            Bins.Clear();
            DisplayTreePlot.Clear();
            Node randomRootNode = bpTemplate.CopyNode(bpTemplate.FunctionRootNodes[random.Next(0, bpTemplate.FunctionRootNodes.Count)]);
            currentTree = randomRootNode;
            bpTemplate.GrowTree(currentTree, random.Next(1, 4));
            TreeDrawingAlgorithm.CalculateNodePositions(currentTree);
            DisplayTreePlot = AppInfoSingleton.GetTreePlot(DisplayTreePlot, currentTree, BrushSet);
            CanvasHeight = (currentTree.Height * 100) + 100;
            CanvasWidth = (currentTree.Width * 100) + 100;
        }
    }
}
