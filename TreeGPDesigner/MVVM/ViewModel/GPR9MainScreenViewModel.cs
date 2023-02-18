using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using TreeGPDesigner.MVVM.Model;

namespace TreeGPDesigner.MVVM.ViewModel
{
    //Viewmodel class for Final Screen View
    public partial class GPR9MainScreenViewModel : ViewModelBase
    {
        //Final Screen Variables
        public event PropertyChangedEventHandler? PropertyChanged2;
        public new ObservableCollection<NodePlot>? DisplayTreePlot => AppInfoSingleton.Instance.MainDisplayTree;
        private void OnMainDisplayTreeChanged()
        {
            PropertyChanged2?.Invoke(this, new PropertyChangedEventArgs(nameof(DisplayTreePlot)));
        }

        private List<Node> knownAlgorithms = AppInfoSingleton.Instance.CurrentTemplate.KnownAlgorithms;

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
        private ObservableCollection<Tree> knownAlgorithmTrees = new();

        [ObservableProperty]
        private List<Node> currentGeneration = new();

        [ObservableProperty]
        private ObservableCollection<Tree> generationTrees = new();

        [ObservableProperty]
        private string loadingMessage = "";

        [ObservableProperty]
        private string buttonContent = "Get Initial Population";

        //Commands
        public ICommand GetNextGenerationCommand { get; }

        //Constructor
        public GPR9MainScreenViewModel()
        {
            GetNextGenerationCommand = new RelayCommand(GetNextGeneration);
            AppInfoSingleton.Instance.MainDisplayTreeChanged += OnMainDisplayTreeChanged;
            InitialiseSettings();
            AppInfoSingleton.Instance.MainDisplayTree = new();
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
            App.Current.Dispatcher.Invoke((Action)delegate
            {
                KnownAlgorithmTrees.Clear();
                for (int i = 0; i < knownAlgorithms.Count; i++)
                {
                    KnownAlgorithmTrees.Add(new Tree(TextColour, Background, NormalButtonColour, knownAlgorithms[i].Name, knownAlgorithms[i].Fitness.ToString(),
                        AppInfoSingleton.RainbowBrush, i.ToString(), true, this));
                }
            }); 
        }

        public void GetInitialPopulation()
        {
            AppInfoSingleton.Instance.CurrentTemplate.GetInitialPopulation();
            CurrentGeneration = AppInfoSingleton.Instance.CurrentTemplate.Generation;
            GetGenerationTrees();
        }

        public void GetGenerationTrees()
        {
            App.Current.Dispatcher.Invoke((Action)delegate
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

                    if (CurrentGeneration[i].Fitness >= AppInfoSingleton.Instance.CurrentTemplate.LowestKnownAlgorithmFitness && CurrentGeneration[i].NotFailedYet)
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
            });
        }

        public async void GetNextGeneration()
        {
            LoadingMessage = "Loading...";
            await Task.Run(GetNextGenerationTask);
            ButtonContent = "Get Generation #" + (AppInfoSingleton.Instance.CurrentTemplate.CurrentGenerationNum + 1);
            LoadingMessage = "";
        }

        public async Task GetNextGenerationTask()
        {
            if (CurrentGeneration.Count < 1)
            {
                GetInitialPopulation();
            }
            else
            {
                AppInfoSingleton.Instance.CurrentTemplate.GetNextGeneration();
                CurrentGeneration = AppInfoSingleton.Instance.CurrentTemplate.Generation;
                GenerationNumber = "Generation #" + AppInfoSingleton.Instance.CurrentTemplate.CurrentGenerationNum;
                GetGenerationTrees();
            }

            GetKnownAlgorithmTrees();   
        }
    }

    //Class used in item control in this view.
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

        public Tree(Brush? textColour, Brush? background, Brush? normalButtonColour, string name, string fitness, Brush? colourIndicator, string displayTreeParameter, 
            bool knownAlgorithm, GPR9MainScreenViewModel mainVM)
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
