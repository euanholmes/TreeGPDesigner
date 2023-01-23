using System.ComponentModel;
using System.Windows.Media;

namespace TreeGPDesigner
{
    //Viewmodel class for Main Window
    public partial class MainWindowViewModel : INotifyPropertyChanged
    {
        //Main Window Variables
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

        //Constructor
        public MainWindowViewModel()
        {
            AppInfoSingleton.Instance.CurrentViewModelChanged += OnCurrentViewModelChanged;
            AppInfoSingleton.Instance.CurrentBackgroundChanged += OnCurrentBackgroundChanged;
        }
    }
}
