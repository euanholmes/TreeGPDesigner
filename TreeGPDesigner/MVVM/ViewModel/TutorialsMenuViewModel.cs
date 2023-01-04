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
    public partial class TutorialsMenuViewModel : ObservableObject
    {
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

        public ICommand NavHomeMenuCommand { get; }
        public ICommand NavGPBasics1Command { get; }

        public TutorialsMenuViewModel()
        {
            NavHomeMenuCommand = new RelayCommand(NavHomeMenu);
            NavGPBasics1Command = new RelayCommand(NavGPBasics1);
        }

        public void NavHomeMenu()
        {
            AppInfoSingleton.Instance.CurrentViewModel = new HomeViewModel();
        }

        public void NavGPBasics1()
        {
            AppInfoSingleton.Instance.CurrentViewModel = new GPBasics1ViewModel();
        }
    }
}
