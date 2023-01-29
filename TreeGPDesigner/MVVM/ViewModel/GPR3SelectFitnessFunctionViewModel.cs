using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.Generic;
using System.Windows.Input;
using System.Windows.Media;

namespace TreeGPDesigner.MVVM.ViewModel
{
    //Viewmodel class for Select Fitness Function View
    public partial class GPR3SelectFitnessFunctionViewModel : ViewModelBase
    {
        //Select Fitness Function Variables
        private List<FunctionModel> fitnessFunctions;

        [ObservableProperty]
        private string fitnessFunctionName;

        [ObservableProperty]
        private string fitnessFunctionInfo;

        [ObservableProperty]
        private ImageSource fitnessFunctionImage;

        //Commands
        public ICommand NavNextCommand { get; }
        public ICommand NavBackCommand { get; }
        public ICommand NextFitnessFunctionCommand { get; }
        public ICommand PreviousFitnessFunctionCommand { get; }

        //Constructor
        public GPR3SelectFitnessFunctionViewModel()
        {
            NavNextCommand = new RelayCommand(NavNext);
            NavBackCommand = new RelayCommand(NavBack);
            NextFitnessFunctionCommand = new RelayCommand(NextFitnessFunction);
            PreviousFitnessFunctionCommand = new RelayCommand(PreviousFitnessFunction);

            fitnessFunctions = AppInfoSingleton.Instance.CurrentTemplate.FitnessFunctionsUI;
            SetFitnessFunctionUI();
        }

        //Navigation Functions
        public void NavNext()
        {
            AppInfoSingleton.Instance.CurrentViewModel = new GPR4SelectNodesViewModel();
        }

        public void NavBack()
        {
            AppInfoSingleton.Instance.CurrentViewModel = new GPR2SelectWrapperViewModel();
        }

        //Select Fitness Function Functions
        public void NextFitnessFunction()
        {
            if (AppInfoSingleton.Instance.CurrentTemplate.CurrentFitnessFunction == fitnessFunctions.Count - 1)
            {
                AppInfoSingleton.Instance.CurrentTemplate.CurrentFitnessFunction = 0;
            }
            else
            {
                AppInfoSingleton.Instance.CurrentTemplate.CurrentFitnessFunction++;
            }
            SetFitnessFunctionUI();
        }

        public void PreviousFitnessFunction()
        {
            if (AppInfoSingleton.Instance.CurrentTemplate.CurrentFitnessFunction == 0)
            {
                AppInfoSingleton.Instance.CurrentTemplate.CurrentFitnessFunction = fitnessFunctions.Count - 1;
            }
            else
            {
                AppInfoSingleton.Instance.CurrentTemplate.CurrentFitnessFunction--;
            }
            SetFitnessFunctionUI();
        }

        public void SetFitnessFunctionUI()
        {
            FitnessFunctionName = fitnessFunctions[AppInfoSingleton.Instance.CurrentTemplate.CurrentFitnessFunction].Name;
            FitnessFunctionInfo = fitnessFunctions[AppInfoSingleton.Instance.CurrentTemplate.CurrentFitnessFunction].Information;
            FitnessFunctionImage = fitnessFunctions[AppInfoSingleton.Instance.CurrentTemplate.CurrentFitnessFunction].Image;
        }
    }
}
