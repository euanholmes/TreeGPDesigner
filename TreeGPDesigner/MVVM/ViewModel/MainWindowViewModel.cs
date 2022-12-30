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

namespace TreeGPDesigner.MVVM.ViewModel
{
    public partial class MainWindowViewModel : INotifyPropertyChanged
    {
        public object? CurrentView => AppInfoSingleton.Instance.CurrentViewModel;
        public event PropertyChangedEventHandler? PropertyChanged;

        public MainWindowViewModel()
        {
            AppInfoSingleton.Instance.CurrentViewModelChanged += OnCurrentViewModelChanged;
        }

        private void OnCurrentViewModelChanged()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentView)));
        }
    }
}
