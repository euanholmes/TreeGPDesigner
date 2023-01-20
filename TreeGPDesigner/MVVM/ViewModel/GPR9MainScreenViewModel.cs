﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using TreeGPDesigner.MVVM.Model;

namespace TreeGPDesigner.MVVM.ViewModel
{
    public partial class GPR9MainScreenViewModel : ObservableObject
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

        [ObservableProperty]
        private Brush? background = AppInfoSingleton.Instance.CurrentBackground;

        //Tree Drawing Variables
        public event PropertyChangedEventHandler? PropertyChanged2;

        public ObservableCollection<NodePlot>? DisplayTreePlot => AppInfoSingleton.Instance.MainDisplayTree;
        private void OnMainDisplayTreeChanged()
        {
            PropertyChanged2?.Invoke(this, new PropertyChangedEventArgs(nameof(DisplayTreePlot)));
        }

        [ObservableProperty]
        private Brush[]? brushSet = AppInfoSingleton.Instance.CurrentBrushSet;

        [ObservableProperty]
        private ImageSource? zoomIconSource = AppInfoSingleton.Instance.CurrentZoomIcon;

        [ObservableProperty]
        private float? canvasWidth = 0;

        [ObservableProperty]
        private float? canvasHeight = 0;

        //Commands
        public ICommand NavHomeMenuCommand { get; }
        public ICommand GetNextGenerationCommand { get; }

        //Final Screen Variables
        [ObservableProperty]
        private string generationNumber = "Generation #" + AppInfoSingleton.Instance.CurrentTemplate.CurrentGenerationNum;

        [ObservableProperty]
        private string runTitleSetting;

        [ObservableProperty]
        private string wrapperSetting;

        [ObservableProperty]
        private string populationCountSetting;

        [ObservableProperty]
        private string maxDepthSetting;

        [ObservableProperty]
        private string treeGrowingMethodSetting;

        [ObservableProperty]
        private string fitnessFunctionSetting;

        [ObservableProperty]
        private string selectionMethodSetting;

        [ObservableProperty]
        private string selectionPercentSetting;

        [ObservableProperty]
        private string mutationPercentSetting;

        [ObservableProperty]
        private string crossoverPercentSetting;

        [ObservableProperty]
        private List<Tree> knownAlgorithmTrees = new List<Tree>();

        private List<Node> knownAlgorithms = AppInfoSingleton.Instance.CurrentTemplate.KnownAlgorithms;

        [ObservableProperty]
        private List<Node> currentGeneration = new();

        [ObservableProperty]
        private ObservableCollection<Tree> generationTrees = new();

        //Constructor
        public GPR9MainScreenViewModel()
        {
            var splashScreen = new SplashScreen("Images/tempLoadScreen.jpg");
            splashScreen.Show(false);

            AppInfoSingleton.Instance.MainDisplayTreeChanged += OnMainDisplayTreeChanged;

            NavHomeMenuCommand = new RelayCommand(NavHomeMenu);
            GetNextGenerationCommand = new RelayCommand(GetNextGeneration);

            InitialiseSettings();
            GetKnownAlgorithmTrees();
            GetInitialPopulation();
            AppInfoSingleton.Instance.MainDisplayTree = new();

            splashScreen.Close(TimeSpan.FromSeconds(0.5));
        }

        //Navigation Functions
        public void NavHomeMenu()
        {
            AppInfoSingleton.Instance.CurrentViewModel = new HomeViewModel();
        }

        

        //Main screen functions
        public void InitialiseSettings()
        {
            RunTitleSetting = AppInfoSingleton.Instance.CurrentTemplate.CurrentRunName;
            WrapperSetting = AppInfoSingleton.Instance.CurrentTemplate.WrappersUI[AppInfoSingleton.Instance.CurrentTemplate.CurrentWrapper].Name;
            FitnessFunctionSetting = AppInfoSingleton.Instance.CurrentTemplate.FitnessFunctionsUI[AppInfoSingleton.Instance.CurrentTemplate.CurrentFitnessFunction].Name;
            PopulationCountSetting = "Population Count: " + AppInfoSingleton.Instance.CurrentTemplate.CurrentPopulationCount.ToString();
            MaxDepthSetting = "Max Depth: " + AppInfoSingleton.Instance.CurrentTemplate.CurrentMaxDepth.ToString();
            SelectionPercentSetting = "Selection: " + AppInfoSingleton.Instance.CurrentTemplate.CurrentSelectionPercent.ToString() + "%";
            MutationPercentSetting = "Mutation: " + AppInfoSingleton.Instance.CurrentTemplate.CurrentMutationPercent.ToString() + "%";
            CrossoverPercentSetting = "Crossover: " + AppInfoSingleton.Instance.CurrentTemplate.CurrentCrossoverPercent.ToString() + "%";

            if (AppInfoSingleton.Instance.CurrentTemplate.CurrentTreeGrowingMethod == 0)
            {
                TreeGrowingMethodSetting = "Ramped Half and Half";
            }
            else if(AppInfoSingleton.Instance.CurrentTemplate.CurrentTreeGrowingMethod == 1)
            {
                TreeGrowingMethodSetting = "All Grow";
            }
            else
            {
                TreeGrowingMethodSetting = "All Full";
            }

            if (AppInfoSingleton.Instance.CurrentTemplate.CurrentSelectionMethod == 0)
            {
                SelectionMethodSetting = "Tournament";
            }
            else if (AppInfoSingleton.Instance.CurrentTemplate.CurrentSelectionMethod == 0)
            {
                SelectionMethodSetting = "Fitness Proportionate";
            }
            else
            {
                SelectionMethodSetting = "Truncation";
            }
        }

        public void GetKnownAlgorithmTrees()
        {
            for (int i = 0; i < knownAlgorithms.Count; i++)
            {
                KnownAlgorithmTrees.Add(new Tree(TextColour, Background, NormalButtonColour, knownAlgorithms[i].Name, knownAlgorithms[i].Fitness.ToString(), 
                    AppInfoSingleton.RainbowBrush, i.ToString(), true, this));
            }
        }

        public void GetNextGeneration()
        {
            AppInfoSingleton.Instance.CurrentTemplate.GetNextGeneration();
            CurrentGeneration = AppInfoSingleton.Instance.CurrentTemplate.Generation;
            GenerationNumber = "Generation #" + AppInfoSingleton.Instance.CurrentTemplate.CurrentGenerationNum;
            GetGenerationTrees();
        }

        public void GetInitialPopulation()
        {
            AppInfoSingleton.Instance.CurrentTemplate.GetInitialPopulation();
            CurrentGeneration = AppInfoSingleton.Instance.CurrentTemplate.Generation;
            GetGenerationTrees();
        }

        public void GetGenerationTrees()
        {
            GenerationTrees.Clear();
            for (int i = 0; i < CurrentGeneration.Count; i++)
            {
                string treeName;

                if (CurrentGeneration[i].Name != null)
                {
                    treeName = CurrentGeneration[i].Name;
                }
                else
                {
                    treeName = "G" + AppInfoSingleton.Instance.CurrentTemplate.CurrentGenerationNum + "-" + (i + 1);
                }

                CurrentGeneration[i].Name = treeName;

                //Trace.WriteLine($"Lowest Known Algorithm Fitness = {AppInfoSingleton.Instance.CurrentTemplate.LowestKnownAlgorithmFitness}");

                if (CurrentGeneration[i].Fitness >= AppInfoSingleton.Instance.CurrentTemplate.LowestKnownAlgorithmFitness)
                {
                    GenerationTrees.Add(new Tree(TextColour, Background, NormalButtonColour, CurrentGeneration[i].Name,
                    CurrentGeneration[i].Fitness.ToString(), AppInfoSingleton.RainbowBrush, i.ToString(), false, this));
                }
                else if (CurrentGeneration[i].NotFailedYet)
                {
                    GenerationTrees.Add(new Tree(TextColour, Background, NormalButtonColour, CurrentGeneration[i].Name,
                    CurrentGeneration[i].Fitness.ToString(), Brushes.Green, i.ToString(), false, this));
                }
                else
                {
                    GenerationTrees.Add(new Tree(TextColour, Background, NormalButtonColour, CurrentGeneration[i].Name,
                    CurrentGeneration[i].Fitness.ToString(), Brushes.Red, i.ToString(), false, this));
                }
            }
        }
    }

    public class Tree
    {
        private Brush? textColour;
        private Brush? background;
        private Brush? normalButtonColour;
        private string name;
        private string fitness;
        private Brush? colourIndicator;
        private ICommand displayTreeCommand;
        private string displayTreeParameter;
        private bool knownAlgorithm;
        private GPR9MainScreenViewModel mainVM;

        public Tree(Brush? textColour, Brush? background, Brush? normalButtonColour, string name, string fitness, Brush? colourIndicator, string displayTreeParameter, bool knownAlgorithm, GPR9MainScreenViewModel mainVM)
        {
            this.TextColour = textColour;
            this.Background = background;
            this.NormalButtonColour = normalButtonColour;
            this.Name = name;
            this.Fitness = fitness;
            this.ColourIndicator = colourIndicator;
            this.DisplayTreeParameter = displayTreeParameter;
            this.KnownAlgorithm = knownAlgorithm;
            this.DisplayTreeCommand = new RelayCommand<string>(param => DisplayTree(param));
            this.MainVM = mainVM;
        }

        public Brush? TextColour { get => textColour; set => textColour = value; }
        public Brush? Background { get => background; set => background = value; }
        public Brush? NormalButtonColour { get => normalButtonColour; set => normalButtonColour = value; }
        public string Name { get => name; set => name = value; }
        public string Fitness { get => fitness; set => fitness = value; }
        public Brush? ColourIndicator { get => colourIndicator; set => colourIndicator = value; }
        public ICommand DisplayTreeCommand { get => displayTreeCommand; set => displayTreeCommand = value; }
        public string DisplayTreeParameter { get => displayTreeParameter; set => displayTreeParameter = value; }
        public bool KnownAlgorithm { get => knownAlgorithm; set => knownAlgorithm = value; }
        public GPR9MainScreenViewModel MainVM { get => mainVM; set => mainVM = value; }

        public void DisplayTree(string treeNumString)
        {
            if (KnownAlgorithm)
            {
                AppInfoSingleton.Instance.MainDisplayTree.Clear();
                int treeNum = Convert.ToInt32(treeNumString);
                TreeDrawingAlgorithm.CalculateNodePositions(AppInfoSingleton.Instance.CurrentTemplate.KnownAlgorithms[treeNum]);
                AppInfoSingleton.Instance.MainDisplayTree = AppInfoSingleton.GetTreePlot(AppInfoSingleton.Instance.MainDisplayTree,
                    AppInfoSingleton.Instance.CurrentTemplate.KnownAlgorithms[treeNum], AppInfoSingleton.Instance.CurrentBrushSet);

                MainVM.CanvasWidth = (AppInfoSingleton.Instance.CurrentTemplate.KnownAlgorithms[treeNum].Width * 100) + 100;
                MainVM.CanvasHeight = (AppInfoSingleton.Instance.CurrentTemplate.KnownAlgorithms[treeNum].Height * 100) + 100;
            }
            else
            {
                AppInfoSingleton.Instance.MainDisplayTree.Clear();
                int treeNum = Convert.ToInt32(treeNumString);
                TreeDrawingAlgorithm.CalculateNodePositions(AppInfoSingleton.Instance.CurrentTemplate.Generation[treeNum]);
                AppInfoSingleton.Instance.MainDisplayTree = AppInfoSingleton.GetTreePlot(AppInfoSingleton.Instance.MainDisplayTree,
                    AppInfoSingleton.Instance.CurrentTemplate.Generation[treeNum], AppInfoSingleton.Instance.CurrentBrushSet);

                MainVM.CanvasWidth = (AppInfoSingleton.Instance.CurrentTemplate.Generation[treeNum].Width * 100) + 100;
                MainVM.CanvasHeight = (AppInfoSingleton.Instance.CurrentTemplate.Generation[treeNum].Height * 100) + 100;
            }
        }
    }
}
