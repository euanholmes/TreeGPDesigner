using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Windows.Input;

namespace TreeGPDesigner.MVVM.ViewModel
{
    //Viewmodel class for Select Tree Growing Method View
    public partial class GPR6SelectTreeGrowingMethodViewModel : ViewModelBase
    {
        //Select Tree Growing Method Variables.
        [ObservableProperty]
        public bool radioButton1IsChecked;

        [ObservableProperty]
        public bool radioButton2IsChecked;

        [ObservableProperty]
        public bool radioButton3IsChecked;

        //Commands
        public ICommand NavNextCommand { get; }
        public ICommand NavBackCommand { get; }
        public ICommand SetTreeGrowingMethodCommand { get; }

        //Constructor
        public GPR6SelectTreeGrowingMethodViewModel()
        {
            NavNextCommand = new RelayCommand(NavNext);
            NavBackCommand = new RelayCommand(NavBack);
            SetTreeGrowingMethodCommand = new RelayCommand<string>(param => SetTreeGrowingMethod(param));

            SetRadioButton();
        }

        //Navigation functions.
        public void NavNext()
        {
            AppInfoSingleton.Instance.CurrentViewModel = new GPR7SelectSelectionMethodViewModel();
        }

        public void NavBack()
        {
            AppInfoSingleton.Instance.CurrentViewModel = new GPR5SelectTrainingDataViewModel();
        }

        //Select Tree Growing Method Functions
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
