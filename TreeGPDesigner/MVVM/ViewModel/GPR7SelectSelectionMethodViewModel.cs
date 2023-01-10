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
    public partial class GPR7SelectSelectionMethodViewModel : ObservableObject
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
        public ICommand NavNextCommand { get; }
        public ICommand NavBackCommand { get; }
        public ICommand SetSelectionMethodCommand { get; }

        //Selection Methods Variables.
        [ObservableProperty]
        public bool radioButton1IsChecked;

        [ObservableProperty]
        public bool radioButton2IsChecked;

        [ObservableProperty]
        public bool radioButton3IsChecked;

        //Constructor
        public GPR7SelectSelectionMethodViewModel()
        {
            NavHomeMenuCommand = new RelayCommand(NavHomeMenu);
            NavNextCommand = new RelayCommand(NavNext);
            NavBackCommand = new RelayCommand(NavBack);

            SetSelectionMethodCommand = new RelayCommand<string>(param => SetSelectionMethod(param));

            SetRadioButton();
        }

        //Navigation Functions.
        public void NavHomeMenu()
        {
            AppInfoSingleton.Instance.CurrentViewModel = new HomeViewModel();
        }

        public void NavNext()
        {
            AppInfoSingleton.Instance.CurrentViewModel = new GPR8FinalSettingsViewModel();
        }

        public void NavBack()
        {
            AppInfoSingleton.Instance.CurrentViewModel = new GPR6SelectTreeGrowingMethodViewModel();
        }

        //Select tree growing method functions.
        public void SetSelectionMethod(string radioButtonNum)
        {
            int radioButtonInt = Convert.ToInt32(radioButtonNum);
            AppInfoSingleton.Instance.CurrentTemplate.CurrentSelectionMethod = radioButtonInt;
        }

        public void SetRadioButton()
        {
            if (AppInfoSingleton.Instance.CurrentTemplate.CurrentSelectionMethod == 0)
            {
                radioButton1IsChecked = true;
            }
            else if (AppInfoSingleton.Instance.CurrentTemplate.CurrentSelectionMethod == 1)
            {
                radioButton2IsChecked = true;
            }
            else
            {
                radioButton3IsChecked = true;
            }
        }
    }
}
