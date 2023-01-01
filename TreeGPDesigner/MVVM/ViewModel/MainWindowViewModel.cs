using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

namespace TreeGPDesigner.MVVM.ViewModel
{
    public partial class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public object? CurrentView => AppInfoSingleton.Instance.CurrentViewModel;
        public Brush? Background => AppInfoSingleton.Instance.CurrentBackground;

        private void OnCurrentViewModelChanged()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentView)));
        }

        private void OnCurrentBackgroundChanged()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Background)));
        }


        public MainWindowViewModel()
        {
            AppInfoSingleton.Instance.CurrentViewModelChanged += OnCurrentViewModelChanged;
            AppInfoSingleton.Instance.CurrentBackgroundChanged += OnCurrentBackgroundChanged;
        }
    }
}
