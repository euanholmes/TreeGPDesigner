using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using TreeGPDesigner.MVVM.Model;

namespace TreeGPDesigner.MVVM.ViewModel
{
    public partial class GPR2SelectWrapperViewModel : ObservableObject
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

        //Wrapper UI Variables
        [ObservableProperty]
        private string wrapperName;

        [ObservableProperty]
        private string wrapperInfo;

        [ObservableProperty]
        private ImageSource wrapperImage;

        private List<FunctionModel> wrappers;

        //Commands
        public ICommand NavHomeMenuCommand { get; }
        public ICommand NavNextCommand { get; }
        public ICommand NavBackCommand { get; }
        public ICommand NextWrapperCommand { get; }
        public ICommand PreviousWrapperCommand { get; }

        //Constructor
        public GPR2SelectWrapperViewModel()
        {
            NavHomeMenuCommand = new RelayCommand(NavHomeMenu);
            NavNextCommand = new RelayCommand(NavNext);
            NavBackCommand = new RelayCommand(NavBack);

            NextWrapperCommand = new RelayCommand(NextWrapper);
            PreviousWrapperCommand = new RelayCommand(PreviousWrapper);

            wrappers = AppInfoSingleton.Instance.CurrentTemplate.WrappersUI;
            SetWrapperUI(); 
        }

        //Navigation Functions
        public void NavHomeMenu()
        {
            AppInfoSingleton.Instance.CurrentViewModel = new HomeViewModel();
        }

        public void NavNext()
        {
            AppInfoSingleton.Instance.CurrentViewModel = new GPR3SelectFitnessFunctionViewModel();
        }

        public void NavBack()
        {
            AppInfoSingleton.Instance.CurrentViewModel = new GPR1GPTemplateMenuViewModel();
        }

        //Next, previous wrapper functions.
        public void NextWrapper()
        {
            if (AppInfoSingleton.Instance.CurrentTemplate.CurrentWrapper == wrappers.Count - 1)
            {
                AppInfoSingleton.Instance.CurrentTemplate.CurrentWrapper = 0;
            }
            else
            {
                AppInfoSingleton.Instance.CurrentTemplate.CurrentWrapper++;
            }
            SetWrapperUI();
        }

        public void PreviousWrapper()
        {
            if (AppInfoSingleton.Instance.CurrentTemplate.CurrentWrapper == 0)
            {
                AppInfoSingleton.Instance.CurrentTemplate.CurrentWrapper = wrappers.Count - 1;
            }
            else
            {
                AppInfoSingleton.Instance.CurrentTemplate.CurrentWrapper--;
            }
            SetWrapperUI();
        }

        //Set wrapper UI
        public void SetWrapperUI()
        {
            WrapperName = wrappers[AppInfoSingleton.Instance.CurrentTemplate.CurrentWrapper].Name;
            WrapperInfo = wrappers[AppInfoSingleton.Instance.CurrentTemplate.CurrentWrapper].Information;
            WrapperImage = wrappers[AppInfoSingleton.Instance.CurrentTemplate.CurrentWrapper].Image;
        }
    }
}
