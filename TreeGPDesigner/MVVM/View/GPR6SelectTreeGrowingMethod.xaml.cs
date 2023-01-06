using System;
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
    /// Interaction logic for GPR6SelectTreeGrowingMethod.xaml
    /// </summary>
    public partial class GPR6SelectTreeGrowingMethod : UserControl
    {
        public GPR6SelectTreeGrowingMethod()
        {
            InitializeComponent();
            DataContext = new GPR6SelectTreeGrowingMethodViewModel();
        }
    }
}
