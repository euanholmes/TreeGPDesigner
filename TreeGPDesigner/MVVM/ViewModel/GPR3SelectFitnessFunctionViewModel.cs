using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using TreeGPDesigner.MVVM.Model;

namespace TreeGPDesigner.MVVM.ViewModel
{
    public partial class GPR3SelectFitnessFunctionViewModel : ObservableObject
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

        //Fitness Function UI Variables
        [ObservableProperty]
        private string fitnessFunctionName;

        [ObservableProperty]
        private string fitnessFunctionInfo;

        [ObservableProperty]
        private ImageSource fitnessFunctionImage;

        private List<FunctionModel> fitnessFunctions;

        //Commands
        public ICommand NavHomeMenuCommand { get; }
        public ICommand NavNextCommand { get; }
        public ICommand NavBackCommand { get; }
        public ICommand NextFitnessFunctionCommand { get; }
        public ICommand PreviousFitnessFunctionCommand { get; }

        //Constructor
        public GPR3SelectFitnessFunctionViewModel()
        {
            NavHomeMenuCommand = new RelayCommand(NavHomeMenu);
            NavNextCommand = new RelayCommand(NavNext);
            NavBackCommand = new RelayCommand(NavBack);

            NextFitnessFunctionCommand = new RelayCommand(NextFitnessFunction);
            PreviousFitnessFunctionCommand = new RelayCommand(PreviousFitnessFunction);

            fitnessFunctions = AppInfoSingleton.Instance.CurrentTemplate.FitnessFunctionsUI;
            SetFitnessFunctionUI();
        }

        //Navigation Functions
        public void NavHomeMenu()
        {
            AppInfoSingleton.Instance.CurrentViewModel = new HomeViewModel();
        }

        public void NavNext()
        {
            AppInfoSingleton.Instance.CurrentViewModel = new GPR4SelectNodesViewModel();
        }

        public void NavBack()
        {
            AppInfoSingleton.Instance.CurrentViewModel = new GPR2SelectWrapperViewModel();
        }

        //Next, previous fitness function functions.
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

        //Set wrapper UI
        public void SetFitnessFunctionUI()
        {
            FitnessFunctionName = fitnessFunctions[AppInfoSingleton.Instance.CurrentTemplate.CurrentFitnessFunction].Name;
            FitnessFunctionInfo = fitnessFunctions[AppInfoSingleton.Instance.CurrentTemplate.CurrentFitnessFunction].Information;
            FitnessFunctionImage = fitnessFunctions[AppInfoSingleton.Instance.CurrentTemplate.CurrentFitnessFunction].Image;
        }
    }
}
