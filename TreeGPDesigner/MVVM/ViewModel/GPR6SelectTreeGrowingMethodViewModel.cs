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

        public GPR6SelectTreeGrowingMethodViewModel()
        {
            NavHomeMenuCommand = new RelayCommand(NavHomeMenu);
            NavNextCommand = new RelayCommand(NavNext);
            NavBackCommand = new RelayCommand(NavBack);
        }

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
    }
}
