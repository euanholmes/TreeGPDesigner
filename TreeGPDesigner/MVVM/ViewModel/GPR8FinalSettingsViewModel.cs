using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;

namespace TreeGPDesigner.MVVM.ViewModel
{
    //Viewmodel class for Final Settings View
    public partial class GPR8FinalSettingsViewModel : ViewModelBase
    {
        //Final Settings Variables
        [ObservableProperty]
        private string runName = AppInfoSingleton.Instance.CurrentTemplate.CurrentRunName;

        //Might be a bug with these at some point there was with the sliders
        [ObservableProperty]
        private string populationCount = AppInfoSingleton.Instance.CurrentTemplate.CurrentPopulationCount.ToString();

        [ObservableProperty]
        private string maxDepth = AppInfoSingleton.Instance.CurrentTemplate.CurrentMaxDepth.ToString();

        //Commands
        public ICommand NavStartRunCommand { get; }
        public ICommand NavBackCommand { get; }

        //Constructor
        public GPR8FinalSettingsViewModel()
        {
            NavStartRunCommand = new RelayCommand(NavStartRun);
            NavBackCommand = new RelayCommand(NavBack);
        }

        //Navigation Functions
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
