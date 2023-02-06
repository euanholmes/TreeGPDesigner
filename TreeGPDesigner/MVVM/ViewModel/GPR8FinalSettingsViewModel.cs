﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;

namespace TreeGPDesigner.MVVM.ViewModel
{
    //Viewmodel class for Final Settings View
    public partial class GPR8FinalSettingsViewModel : ViewModelBase
    {
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
