﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TreeGPDesigner.MVVM.ViewModel;

namespace TreeGPDesigner
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            AppInfoSingleton.Instance.CurrentViewModel = new HomeViewModel();
            AppInfoSingleton.Instance.CurrentFunctionNodeOutlineBrush = Brushes.Red;
            AppInfoSingleton.Instance.CurrentFunctionNodeBackgroundBrush = Brushes.Pink;
            AppInfoSingleton.Instance.CurrentTerminalNodeOutlineBrush = Brushes.Blue;
            AppInfoSingleton.Instance.CurrentTerminalNodeBackgroundBrush = Brushes.LightBlue;
            AppInfoSingleton.Instance.CurrentModeToggleButton = new BitmapImage(new Uri("pack://application:,,,/Images/light-mode-toggle-icon.png"));
            AppInfoSingleton.Instance.CurrentBackground = Brushes.White;
            AppInfoSingleton.Instance.CurrentText = Brushes.Black;
            AppInfoSingleton.Instance.CurrentRadioButton1 = true;
            AppInfoSingleton.Instance.CurrentRadioButton2 = false;
            AppInfoSingleton.Instance.CurrentRadioButton3 = false;
            AppInfoSingleton.Instance.CurrentRadioButton4 = false;
            AppInfoSingleton.Instance.CurrentRadioButton5 = false;
            AppInfoSingleton.Instance.CurrentRadioButton6 = false;
            AppInfoSingleton.Instance.CurrentRadioButtonCheck = 1;
            DataContext = new MainWindowViewModel();
        }
    }
}
