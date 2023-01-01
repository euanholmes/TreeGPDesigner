﻿using CommunityToolkit.Mvvm.ComponentModel;
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
        public ICommand NavGPBasics1Command { get; }

        public TutorialsMenuViewModel()
        {
            AppInfoSingleton.Instance.CurrentTextChanged += OnCurrentTextChanged;

            NavHomeMenuCommand = new RelayCommand(NavHomeMenu);
            NavGPBasics1Command = new RelayCommand(NavGPBasics1);
        }

        public void NavHomeMenu()
        {
            AppInfoSingleton.Instance.CurrentViewModel = new HomeViewModel();
        }

        public void NavGPBasics1()
        {
            AppInfoSingleton.Instance.CurrentViewModel = new GPBasics1ViewModel();
        }
    }
}
