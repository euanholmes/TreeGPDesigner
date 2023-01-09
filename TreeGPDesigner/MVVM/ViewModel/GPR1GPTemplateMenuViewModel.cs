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
    public partial class GPR1GPTemplateMenuViewModel : ObservableObject, INotifyPropertyChanged
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

        //
        public new event PropertyChangedEventHandler? PropertyChanged;
        public TreeGP? CurrentTemplate => AppInfoSingleton.Instance.CurrentTemplate;
        private void OnCurrentTemplateChanged()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentTemplate)));
        }

        public GPR1GPTemplateMenuViewModel()
        {
            AppInfoSingleton.Instance.CurrentTemplateChanged += OnCurrentTemplateChanged;

            NavHomeMenuCommand = new RelayCommand(NavHomeMenu);
            NavSelectCommand = new RelayCommand(NavSelect);
        }

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
