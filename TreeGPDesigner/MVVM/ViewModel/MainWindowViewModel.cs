using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TreeGPDesigner.MVVM.ViewModel
{
    public partial class MainWindowViewModel : ObservableObject
    {

        [ObservableProperty]
        private object? currentView;

        public HomeViewModel HomeVM { get; set; }

        public TutorialsMenuViewModel TutorialsMenuVM { get; set; }


        public MainWindowViewModel()
        {
            HomeVM = new HomeViewModel();
            TutorialsMenuVM = new TutorialsMenuViewModel();
            CurrentView = HomeVM;
        }
    }
}
