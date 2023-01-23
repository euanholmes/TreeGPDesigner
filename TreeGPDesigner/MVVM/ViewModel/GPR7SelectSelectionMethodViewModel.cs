using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Windows.Input;

namespace TreeGPDesigner.MVVM.ViewModel
{
    //Viewmodel class for Select Selection Method View
    public partial class GPR7SelectSelectionMethodViewModel : ViewModelBase
    {
        //Select Selection Method Variables.
        [ObservableProperty]
        public bool radioButton1IsChecked;

        [ObservableProperty]
        public bool radioButton2IsChecked;

        [ObservableProperty]
        public bool radioButton3IsChecked;

        //Commands
        public ICommand NavNextCommand { get; }
        public ICommand NavBackCommand { get; }
        public ICommand SetSelectionMethodCommand { get; }

        //Constructor
        public GPR7SelectSelectionMethodViewModel()
        {
            NavNextCommand = new RelayCommand(NavNext);
            NavBackCommand = new RelayCommand(NavBack);
            SetSelectionMethodCommand = new RelayCommand<string>(param => SetSelectionMethod(param));

            SetRadioButton();
        }

        //Navigation Functions.
        public void NavNext()
        {
            AppInfoSingleton.Instance.CurrentViewModel = new GPR8FinalSettingsViewModel();
        }

        public void NavBack()
        {
            AppInfoSingleton.Instance.CurrentViewModel = new GPR6SelectTreeGrowingMethodViewModel();
        }

        //Select Selection Method Functions
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
