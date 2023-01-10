using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for GPR8FinalSettings.xaml
    /// </summary>
    public partial class GPR8FinalSettings : UserControl
    {
        public GPR8FinalSettings()
        {
            InitializeComponent();
            DataContext = new GPR8FinalSettingsViewModel();
        }

        //Makes sure you only enter numbers in the text box.
        private static readonly Regex _regex = new Regex("[^0-9]+"); 
        private static bool IsTextAllowed(string text)
        {
            return !_regex.IsMatch(text);
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled =!IsTextAllowed(e.Text);
        }

        private void PopulationCountChanged(object sender, TextChangedEventArgs e)
        {
            string popCountString = popCount.Text;
            int popCountInt;
            bool success = Int32.TryParse(popCountString, out popCountInt);

            if (success)
            {
                AppInfoSingleton.Instance.CurrentTemplate.CurrentPopulationCount = popCountInt;
            }
        }

        private void MaxDepthChanged(object sender, TextChangedEventArgs e)
        {
            string maxDepthString = maxDepth.Text;
            int maxDepthInt;
            bool success = Int32.TryParse(maxDepthString, out maxDepthInt);

            if (success)
            {
                AppInfoSingleton.Instance.CurrentTemplate.CurrentMaxDepth = maxDepthInt;
            }
        }
    }
}
