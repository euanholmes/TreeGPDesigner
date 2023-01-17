using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using TreeGPDesigner.MVVM.Model;

namespace TreeGPDesigner.MVVM.ViewModel
{
    public partial class GPBasics4ViewModel : ObservableObject
    {
        //Common Variables
        [ObservableProperty]
        private Brush? textColour = AppInfoSingleton.Instance.CurrentText;

        [ObservableProperty]
        private Brush? normalButtonColour = AppInfoSingleton.Instance.CurrentNormalButtonColor;

        [ObservableProperty]
        private Brush? navButtonColour = AppInfoSingleton.Instance.CurrentNavButtonColor;

        [ObservableProperty]
        private Brush? panel1Colour = AppInfoSingleton.Instance.CurrentPanel1Color;

        [ObservableProperty]
        private Brush? panel2Colour = AppInfoSingleton.Instance.CurrentPanel2Color;

        //Tree Drawing Variables
        [ObservableProperty]
        private Brush[]? brushSet = AppInfoSingleton.Instance.CurrentBrushSet;

        [ObservableProperty]
        public ObservableCollection<NodePlot> displayTreePlot = new();

        [ObservableProperty]
        private float? canvasHeight = 0;

        [ObservableProperty]
        private float? canvasWidth = 0;

        //Commands
        public ICommand NavHomeMenuCommand { get; }
        public ICommand NavTutorialsMenuCommand { get; }
        public ICommand NavPreviousCommand { get; }
        public ICommand GetFitnessCommand { get; }
        public ICommand NewTreeCommand { get; }

        //GP Basics 4 Variables
        [ObservableProperty]
        private string informationText = "";

        [ObservableProperty]
        private ObservableCollection<Bin> bins = new();

        [ObservableProperty]
        private string treeFitness = "Fitness = ";

        [ObservableProperty]
        private Brush colourIndicator = Brushes.Gray;

        private BinPackingTemplate bpTemplate = new();

        private Node currentTree;

        private static Random random = new();

        //Constructor
        public GPBasics4ViewModel()
        {
            NavHomeMenuCommand = new RelayCommand(NavHomeMenu);
            NavTutorialsMenuCommand = new RelayCommand(NavTutorialsMenu);
            NavPreviousCommand = new RelayCommand(NavPrevious);
            GetFitnessCommand = new RelayCommand(GetFitness);
            NewTreeCommand = new RelayCommand(NewTree);
        }

        //Navigation Functions
        public void NavHomeMenu()
        {
            AppInfoSingleton.Instance.CurrentViewModel = new HomeViewModel();
        }

        public void NavTutorialsMenu()
        {
            AppInfoSingleton.Instance.CurrentViewModel = new TutorialsMenuViewModel();
        }

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
                bpTemplate.FitnessFunctionOne(currentTree, new List<int>() { 6, 6, 8, 8, 24, 17, 22, 44, 24, 21 }, 60);
                string fitnessNumString = (11 + currentTree.Fitness).ToString();
                Trace.WriteLine(fitnessNumString);
                TreeFitness = "Fitness = " + fitnessNumString;
                List<List<int>> rawBins = bpTemplate.BPOfflineWrapper(new List<int>() { 6, 6, 8, 8, 24, 17, 22, 44, 24, 21 }, 60, currentTree);
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
