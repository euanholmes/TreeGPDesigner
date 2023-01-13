using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

namespace TreeGPDesigner.MVVM.ViewModel
{
    public partial class GPR8FinalSettingsViewModel : ObservableObject
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
        public ICommand NavStartRunCommand { get; }
        public ICommand NavBackCommand { get; }

        //Final Settings Variables
        [ObservableProperty]
        private string runName = AppInfoSingleton.Instance.CurrentTemplate.CurrentRunName;

        [ObservableProperty]
        private string populationCount = AppInfoSingleton.Instance.CurrentTemplate.CurrentPopulationCount.ToString();

        [ObservableProperty]
        private string maxDepth = AppInfoSingleton.Instance.CurrentTemplate.CurrentMaxDepth.ToString();

        [ObservableProperty]
        private int selectionSliderValue = AppInfoSingleton.Instance.CurrentTemplate.CurrentSelectionPercent;

        [ObservableProperty]
        private int mutationCrossoverSliderValue = AppInfoSingleton.Instance.CurrentTemplate.CurrentMutationPercent;

        //Constructor
        public GPR8FinalSettingsViewModel()
        {
            NavHomeMenuCommand = new RelayCommand(NavHomeMenu);
            NavStartRunCommand = new RelayCommand(NavStartRun);
            NavBackCommand = new RelayCommand(NavBack);
        }

        //Navigation Functions
        public void NavHomeMenu()
        {
            AppInfoSingleton.Instance.CurrentViewModel = new HomeViewModel();
        }

        public void NavStartRun()
        {
            if (AppInfoSingleton.Instance.CurrentTemplate.CurrentPopulationCount < 10)
            {
                AppInfoSingleton.Instance.CurrentTemplate.CurrentPopulationCount = 10;
            }

            if (AppInfoSingleton.Instance.CurrentTemplate.CurrentMaxDepth < AppInfoSingleton.Instance.CurrentTemplate.CurrentMinDepth)
            {
                AppInfoSingleton.Instance.CurrentTemplate.CurrentMaxDepth = AppInfoSingleton.Instance.CurrentTemplate.CurrentMinDepth;
            }

            AppInfoSingleton.Instance.CurrentViewModel = new GPR9MainScreenViewModel();
        }

        public void NavBack()
        {
            AppInfoSingleton.Instance.CurrentViewModel = new GPR7SelectSelectionMethodViewModel();
        }
    }
}
