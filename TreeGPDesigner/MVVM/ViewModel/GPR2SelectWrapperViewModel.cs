using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.Generic;
using System.Windows.Input;
using System.Windows.Media;
using TreeGPDesigner.MVVM.Model;

namespace TreeGPDesigner.MVVM.ViewModel
{
    //Viewmodel class for Select Wrapper View
    public partial class GPR2SelectWrapperViewModel : ViewModelBase
    {
        //Select Wrapper UI Variables
        private List<FunctionModel> wrappers;

        [ObservableProperty]
        private string wrapperName;

        [ObservableProperty]
        private string wrapperInfo;

        [ObservableProperty]
        private ImageSource wrapperImage;

        //Commands
        public ICommand NavNextCommand { get; }
        public ICommand NavBackCommand { get; }
        public ICommand NextWrapperCommand { get; }
        public ICommand PreviousWrapperCommand { get; }

        //Constructor
        public GPR2SelectWrapperViewModel()
        {
            NavNextCommand = new RelayCommand(NavNext);
            NavBackCommand = new RelayCommand(NavBack);
            NextWrapperCommand = new RelayCommand(NextWrapper);
            PreviousWrapperCommand = new RelayCommand(PreviousWrapper);

            wrappers = AppInfoSingleton.Instance.CurrentTemplate.WrappersUI;
            SetWrapperUI(); 
        }

        //Navigation Functions
        public void NavNext()
        {
            AppInfoSingleton.Instance.CurrentViewModel = new GPR3SelectFitnessFunctionViewModel();
        }

        public void NavBack()
        {
            AppInfoSingleton.Instance.CurrentViewModel = new GPR1GPTemplateMenuViewModel();
        }

        //Select Wrapper Functions
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

        public void SetWrapperUI()
        {
            WrapperName = wrappers[AppInfoSingleton.Instance.CurrentTemplate.CurrentWrapper].Name;
            WrapperInfo = wrappers[AppInfoSingleton.Instance.CurrentTemplate.CurrentWrapper].Information;
            WrapperImage = wrappers[AppInfoSingleton.Instance.CurrentTemplate.CurrentWrapper].Image;
        }
    }
}
