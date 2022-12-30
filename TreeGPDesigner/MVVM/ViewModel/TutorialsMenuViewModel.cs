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
    public partial class TutorialsMenuViewModel : ObservableObject
    {
        public ICommand NavHomeMenuCommand { get; }

        public TutorialsMenuViewModel()
        {
            NavHomeMenuCommand = new RelayCommand(NavHomeMenu);
        }

        public void NavHomeMenu()
        {
            AppInfoSingleton.Instance.CurrentViewModel = new HomeViewModel();
        }
    }
}
