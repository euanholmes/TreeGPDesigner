﻿using System;
using System.Collections.Generic;
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

namespace TreeGPDesigner.MVVM.View
{
    /// <summary>
    /// Interaction logic for GPBasics1.xaml
    /// </summary>
    public partial class GPBasics1 : UserControl
    {
        public GPBasics1()
        {
            InitializeComponent();
            DataContext = new GPBasics1ViewModel();
        }
    }
}
