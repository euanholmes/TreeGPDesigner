using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TreeGPDesigner.MVVM.ViewModel
{
    public partial class HomeViewModel : ObservableObject
    {
        public ICommand NavTutorialsMenuCommand { get; }

        public HomeViewModel()
        {
            NavTutorialsMenuCommand = new RelayCommand(NavTutorialsMenu);
        }

        public void NavTutorialsMenu()
        {
            AppInfoSingleton.Instance.CurrentViewModel = new TutorialsMenuViewModel();
        }
    }
}
