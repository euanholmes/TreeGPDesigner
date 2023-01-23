using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;

namespace TreeGPDesigner.MVVM.ViewModel
{
    //Viewmodel class for Tutorials Menu View
    public partial class TutorialsMenuViewModel : ViewModelBase
    {
        //Commands
        public ICommand NavGPBasics1Command { get; }
        public ICommand NavGPBasics2Command { get; }
        public ICommand NavAddCustomNodesTutorialCommand { get; }

        //Constructor
        public TutorialsMenuViewModel()
        {
            NavGPBasics1Command = new RelayCommand(NavGPBasics1);
            NavGPBasics2Command = new RelayCommand(NavGPBasics2);
            NavAddCustomNodesTutorialCommand = new RelayCommand(NavAddCustomNodesTutorial);
        }

        //Navigation Functions
        public void NavGPBasics1()
        {
            AppInfoSingleton.Instance.CurrentViewModel = new GPBasics1ViewModel();
        }

        public void NavGPBasics2()
        {
            AppInfoSingleton.Instance.CurrentViewModel = new GPBasics5ViewModel();
        }

        public void NavAddCustomNodesTutorial()
        {
            AppInfoSingleton.Instance.CurrentViewModel = new CustomNodesTutorialViewModel();
        }
    }
}
