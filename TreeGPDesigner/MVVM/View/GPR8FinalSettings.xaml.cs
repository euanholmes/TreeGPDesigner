using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace TreeGPDesigner.MVVM.View
{
    //Code behind for Final Setting View
    public partial class GPR8FinalSettings : UserControl
    {
        //Final Settings Variables
        public int maxDepthLoad = 0;
        public int popCountLoad = 0;

        //Constructor
        public GPR8FinalSettings()
        {
            InitializeComponent();
            selectionSliderTitle.Text = "Selection = " + AppInfoSingleton.Instance.CurrentTemplate.CurrentSelectionPercent + "%";
            mutationTitle.Text = "Mutation = " + AppInfoSingleton.Instance.CurrentTemplate.CurrentMutationPercent + "%";
            crossoverTitle.Text = "Crossover = " + AppInfoSingleton.Instance.CurrentTemplate.CurrentCrossoverPercent + "%";
            FocusManager.SetFocusedElement(this, runTitle);
        }

        //Code that makes sure you only enter numbers in a text box
        private static readonly Regex _regex = new Regex("[^0-9]+"); 
        private static bool IsTextAllowed(string text)
        {
            return !_regex.IsMatch(text);
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled =!IsTextAllowed(e.Text);
        }

        //Final Settings Functions
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
