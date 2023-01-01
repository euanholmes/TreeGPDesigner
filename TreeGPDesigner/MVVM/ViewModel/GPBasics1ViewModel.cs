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
    public partial class GPBasics1ViewModel : ObservableObject, INotifyPropertyChanged
    {
        public new event PropertyChangedEventHandler? PropertyChanged;

        public Brush? TextColour => AppInfoSingleton.Instance.CurrentText;

        private void OnCurrentTextChanged()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TextColour)));
        }

        public ICommand NavHomeMenuCommand { get; }
        public ICommand NavTutorialsMenuCommand { get; }


        public GPBasics1ViewModel()
        {
            AppInfoSingleton.Instance.CurrentTextChanged += OnCurrentTextChanged;

            NavHomeMenuCommand = new RelayCommand(NavHomeMenu);
            NavTutorialsMenuCommand = new RelayCommand(NavTutorialsMenu);
        }

        public void NavHomeMenu()
        {
            AppInfoSingleton.Instance.CurrentViewModel = new HomeViewModel();
        }

        public void NavTutorialsMenu()
        {
            AppInfoSingleton.Instance.CurrentViewModel = new TutorialsMenuViewModel();
        }
    }
}
