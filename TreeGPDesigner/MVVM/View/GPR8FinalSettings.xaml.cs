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
        public int maxDepthLoad = 0;
        public int popCountLoad = 0;

        public GPR8FinalSettings()
        {
            InitializeComponent();
            selectionSliderTitle.Text = "Selection = " + AppInfoSingleton.Instance.CurrentTemplate.CurrentSelectionPercent + "%";
            mutationTitle.Text = "Mutation = " + AppInfoSingleton.Instance.CurrentTemplate.CurrentMutationPercent + "%";
            crossoverTitle.Text = "Crossover = " + AppInfoSingleton.Instance.CurrentTemplate.CurrentCrossoverPercent + "%";
            FocusManager.SetFocusedElement(this, runTitle);
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

        private void RunNameChanged(object sender, TextChangedEventArgs e)
        {
            AppInfoSingleton.Instance.CurrentTemplate.CurrentRunName = runTitle.Text;
        }

        private void PopulationCountChanged(object sender, TextChangedEventArgs e)
        {
            string popCountString = popCount.Text;
            int popCountInt;
            bool success = Int32.TryParse(popCountString, out popCountInt);

            popCountLoad = popCountInt;
            if (popCountLoad > 9999 || maxDepthLoad > 14)
            {
                loadMessage.Text = "*Potentially long load time.";
            }
            else
            {
                loadMessage.Text = "";
            }


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

            maxDepthLoad = maxDepthInt;
            if (popCountLoad > 9999 || maxDepthLoad > 14)
            {
                loadMessage.Text = "*Potentially long load time.";
            }
            else
            {
                loadMessage.Text = "";
            }

            if (success)
            {
                AppInfoSingleton.Instance.CurrentTemplate.CurrentMaxDepth = maxDepthInt;
            }
        }

        private void SelectionSliderChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            AppInfoSingleton.Instance.CurrentTemplate.CurrentSelectionPercent = (int)selectionSlider.Value;
            selectionSliderTitle.Text = "Selection % = " + AppInfoSingleton.Instance.CurrentTemplate.CurrentSelectionPercent + "%";
        }

        private void MutationCrossoverSliderChanged(object sender , RoutedPropertyChangedEventArgs<double> e)
        {
            AppInfoSingleton.Instance.CurrentTemplate.CurrentMutationPercent = (int)mutationCrossoverSlider.Value;
            AppInfoSingleton.Instance.CurrentTemplate.CurrentCrossoverPercent = 100 - AppInfoSingleton.Instance.CurrentTemplate.CurrentMutationPercent;
            mutationTitle.Text = "Mutation % = " + AppInfoSingleton.Instance.CurrentTemplate.CurrentMutationPercent + "%";
            crossoverTitle.Text = "Crossover % = " + AppInfoSingleton.Instance.CurrentTemplate.CurrentCrossoverPercent + "%";
        }
    }
}
