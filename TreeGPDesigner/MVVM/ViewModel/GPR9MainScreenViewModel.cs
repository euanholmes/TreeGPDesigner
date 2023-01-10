using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

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

        //Commands
        public ICommand NavHomeMenuCommand { get; }
        public ICommand GetNextGenerationCommand { get; }

        //Final Screen Variables
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

        //Constructor
        public GPR9MainScreenViewModel()
        {
            NavHomeMenuCommand = new RelayCommand(NavHomeMenu);
            GetNextGenerationCommand = new RelayCommand(GetNextGeneration);

            InitialiseSettings();
        }

        //Navigation Functions
        public void NavHomeMenu()
        {
            AppInfoSingleton.Instance.CurrentViewModel = new HomeViewModel();
        }

        public void GetNextGeneration()
        {

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
    }
}
