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
    public partial class TutorialsMenuViewModel : ObservableObject, INotifyPropertyChanged
    {
        public new event PropertyChangedEventHandler? PropertyChanged;
        public Brush? TextColour => AppInfoSingleton.Instance.CurrentText;

        private void OnCurrentTextChanged()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TextColour)));
        }

        public ICommand NavHomeMenuCommand { get; }

        public TutorialsMenuViewModel()
        {
            AppInfoSingleton.Instance.CurrentTextChanged += OnCurrentTextChanged;

            NavHomeMenuCommand = new RelayCommand(NavHomeMenu);
        }

        public void NavHomeMenu()
        {
            AppInfoSingleton.Instance.CurrentViewModel = new HomeViewModel();
        }
    }
}
