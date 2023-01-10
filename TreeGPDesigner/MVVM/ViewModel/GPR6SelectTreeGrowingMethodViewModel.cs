using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using TreeGPDesigner.MVVM.Model;

namespace TreeGPDesigner.MVVM.ViewModel
{
    public partial class GPR6SelectTreeGrowingMethodViewModel : ObservableObject
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

        //Tree Variables
        [ObservableProperty]
        private Brush[]? brushSet = AppInfoSingleton.Instance.CurrentBrushSet;

        //Commands
        public ICommand NavHomeMenuCommand { get; }
        public ICommand NavNextCommand { get; }
        public ICommand NavBackCommand { get; }
        public ICommand SetTreeGrowingMethodCommand { get; }

        //Tree Growing Methods Variables.
        [ObservableProperty]
        public bool radioButton1IsChecked;

        [ObservableProperty]
        public bool radioButton2IsChecked;

        [ObservableProperty]
        public bool radioButton3IsChecked;

        public GPR6SelectTreeGrowingMethodViewModel()
        {
            NavHomeMenuCommand = new RelayCommand(NavHomeMenu);
            NavNextCommand = new RelayCommand(NavNext);
            NavBackCommand = new RelayCommand(NavBack);

            SetTreeGrowingMethodCommand = new RelayCommand<string>(param => SetTreeGrowingMethod(param));

            SetRadioButton();
        }

        //Navigation functions.
        public void NavHomeMenu()
        {
            AppInfoSingleton.Instance.CurrentViewModel = new HomeViewModel();
        }

        public void NavNext()
        {
            AppInfoSingleton.Instance.CurrentViewModel = new GPR7SelectSelectionMethodViewModel();
        }

        public void NavBack()
        {
            AppInfoSingleton.Instance.CurrentViewModel = new GPR5SelectTrainingDataViewModel();
        }

        //Select tree growing method functions.
        public void SetTreeGrowingMethod(string radioButtonNum)
        {
            int radioButtonInt = Convert.ToInt32(radioButtonNum);
            AppInfoSingleton.Instance.CurrentTemplate.CurrentTreeGrowingMethod = radioButtonInt;
        }

        public void SetRadioButton()
        {
            if (AppInfoSingleton.Instance.CurrentTemplate.CurrentTreeGrowingMethod == 0)
            {
                radioButton1IsChecked = true;
            }
            else if (AppInfoSingleton.Instance.CurrentTemplate.CurrentTreeGrowingMethod == 1)
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
