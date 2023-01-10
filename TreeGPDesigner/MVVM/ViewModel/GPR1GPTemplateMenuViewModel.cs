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
    public partial class GPR1GPTemplateMenuViewModel : ObservableObject
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
        public ICommand NavSelectCommand { get; }

        //GP Template Menu variables
        public new event PropertyChangedEventHandler? PropertyChanged2;
        public TreeGP? CurrentTemplate => AppInfoSingleton.Instance.CurrentTemplate;
        private void OnCurrentTemplateChanged()
        {
            PropertyChanged2?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentTemplate)));
        }

        //Constructor
        public GPR1GPTemplateMenuViewModel()
        {
            AppInfoSingleton.Instance.CurrentTemplateChanged += OnCurrentTemplateChanged;

            NavHomeMenuCommand = new RelayCommand(NavHomeMenu);
            NavSelectCommand = new RelayCommand(NavSelect);
        }

        //Navigation Functions
        public void NavHomeMenu()
        {
            AppInfoSingleton.Instance.CurrentViewModel = new HomeViewModel();
        }

        public void NavSelect()
        {
            AppInfoSingleton.Instance.CurrentTemplate = new BinPackingTemplate();
            AppInfoSingleton.Instance.CurrentViewModel = new GPR2SelectWrapperViewModel();
        }
    }
}
