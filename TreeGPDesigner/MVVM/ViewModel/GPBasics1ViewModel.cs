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
        public Brush? FunctionNodeOutlineBrush => AppInfoSingleton.Instance.CurrentFunctionNodeOutlineBrush;
        public Brush? FunctionNodeBackgroundBrush => AppInfoSingleton.Instance.CurrentFunctionNodeBackgroundBrush;
        public Brush? TerminalNodeOutlineBrush => AppInfoSingleton.Instance.CurrentTerminalNodeOutlineBrush;
        public Brush? TerminalNodeBackgroundBrush => AppInfoSingleton.Instance.CurrentTerminalNodeBackgroundBrush;

        private void OnCurrentTextChanged()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TextColour)));
        }
        private void OnCurrentFunctionNodeOutlineBrushChanged()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FunctionNodeOutlineBrush)));
        }
        private void OnCurrentFunctionNodeBackgroundBrushChanged()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FunctionNodeBackgroundBrush)));
        }
        private void OnCurrentTerminalNodeOutlineBrushChanged()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TerminalNodeOutlineBrush)));
        }
        private void OnCurrentTerminalNodeBackgroundBrushChanged()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TerminalNodeBackgroundBrush)));
        }

        public ICommand NavHomeMenuCommand { get; }
        public ICommand NavTutorialsMenuCommand { get; }


        public GPBasics1ViewModel()
        {
            AppInfoSingleton.Instance.CurrentTextChanged += OnCurrentTextChanged;
            AppInfoSingleton.Instance.CurrentFunctionNodeOutlineBrushChanged += OnCurrentFunctionNodeOutlineBrushChanged;
            AppInfoSingleton.Instance.CurrentFunctionNodeBackgroundBrushChanged += OnCurrentFunctionNodeBackgroundBrushChanged;
            AppInfoSingleton.Instance.CurrentTerminalNodeOutlineBrushChanged += OnCurrentTerminalNodeOutlineBrushChanged;
            AppInfoSingleton.Instance.CurrentTerminalNodeBackgroundBrushChanged += OnCurrentTerminalNodeBackgroundBrushChanged;

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
