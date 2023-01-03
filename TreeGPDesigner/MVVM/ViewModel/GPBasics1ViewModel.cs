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

namespace TreeGPDesigner.MVVM.ViewModel
{
    public partial class GPBasics1ViewModel : ObservableObject
    {
        //Common Variables
        [ObservableProperty]
        private Brush textColour = AppInfoSingleton.Instance.CurrentText;

        [ObservableProperty]
        private Brush normalButtonColour = AppInfoSingleton.Instance.CurrentNormalButtonColor;

        [ObservableProperty]
        private Brush navButtonColour = AppInfoSingleton.Instance.CurrentNavButtonColor;

        [ObservableProperty]
        private Brush panel1Colour = AppInfoSingleton.Instance.CurrentPanel1Color;

        [ObservableProperty]
        private Brush panel2Colour = AppInfoSingleton.Instance.CurrentPanel2Color;


        //Tree Drawing Variables
        [ObservableProperty]
        private Brush functionNodeOutlineBrush = AppInfoSingleton.Instance.CurrentFunctionNodeOutlineBrush;

        [ObservableProperty]
        private Brush functionNodeBackgroundBrush = AppInfoSingleton.Instance.CurrentFunctionNodeBackgroundBrush;

        [ObservableProperty]
        private Brush terminalNodeOutlineBrush = AppInfoSingleton.Instance.CurrentTerminalNodeOutlineBrush;

        [ObservableProperty]
        private Brush terminalNodeBackgroundBrush = AppInfoSingleton.Instance.CurrentTerminalNodeBackgroundBrush;

        [ObservableProperty]
        private ImageSource zoomIconSource = AppInfoSingleton.Instance.CurrentZoomIcon;

        public ICommand NavHomeMenuCommand { get; }
        public ICommand NavTutorialsMenuCommand { get; }

        public GPBasics1ViewModel()
        {
            NavHomeMenuCommand = new RelayCommand(NavHomeMenu);
            NavTutorialsMenuCommand = new RelayCommand(NavTutorialsMenu);
        }

        public void NavHomeMenu()
        {
            AppInfoSingleton.Instance.CurrentViewModel = new HomeViewModel();
        }

        public void NavTutorialsMenu()
        {
            AppInfoSingleton.Instance.CurrentViewModel = new TutorialsMenuViewModel();
        }
    }
}
