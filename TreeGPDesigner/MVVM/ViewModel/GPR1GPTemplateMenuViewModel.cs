using CommunityToolkit.Mvvm.Input;
using System.ComponentModel;
using System.Windows.Input;
using TreeGPDesigner.MVVM.Model;

namespace TreeGPDesigner.MVVM.ViewModel
{
    //Viewmodel class for GP Template Menu View
    public partial class GPR1GPTemplateMenuViewModel : ViewModelBase
    {
        //GP Template Menu variables
        public event PropertyChangedEventHandler? PropertyChanged2;
        public TreeGP? CurrentTemplate => AppInfoSingleton.Instance.CurrentTemplate;
        private void OnCurrentTemplateChanged()
        {
            PropertyChanged2?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentTemplate)));
        }

        //Commands
        public ICommand NavSelectBPCommand { get; }

        //Constructor
        public GPR1GPTemplateMenuViewModel()
        {
            NavSelectBPCommand = new RelayCommand(NavSelectBP);

            AppInfoSingleton.Instance.CurrentTemplateChanged += OnCurrentTemplateChanged;
        }

        //Navigation Functions
        public void NavSelectBP()
        {
            AppInfoSingleton.Instance.CurrentTemplate = new BinPackingTemplate();
            AppInfoSingleton.Instance.CurrentViewModel = new GPR2SelectWrapperViewModel();
        }
    }
}
