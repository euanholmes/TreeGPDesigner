using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using TreeGPDesigner.MVVM.Model;

namespace TreeGPDesigner.MVVM.ViewModel
{
    public partial class AddCustomTerminalNodeViewModel : ObservableObject
    {
        //Common Variables
        [ObservableProperty]
        private Brush? textColour = AppInfoSingleton.Instance.CurrentText;

        [ObservableProperty]
        private Brush? normalButtonColour = AppInfoSingleton.Instance.CurrentNormalButtonColor;

        [ObservableProperty]
        private Brush? navButtonColour = AppInfoSingleton.Instance.CurrentNavButtonColor;

        [ObservableProperty]
        private Brush? panel1Colour = AppInfoSingleton.Instance.CurrentPanel1Color;

        [ObservableProperty]
        private Brush? panel2Colour = AppInfoSingleton.Instance.CurrentPanel2Color;

        //Commands
        public ICommand NavBackCommand { get; }
        public ICommand AddCustomTerminalNodeCommand { get; }
        public ICommand FunctionNeededCommand { get; }
        public ICommand DataNeededCommand { get; }

        //Add Custom Function Node Variables
        [ObservableProperty]
        private string errorMessage = "";

        [ObservableProperty]
        private Brush errorColour = Brushes.Red;

        [ObservableProperty]
        private string symbolText = "";

        [ObservableProperty]
        private string nodeDescriptionText = "";

        [ObservableProperty]
        private string valueText = "";

        [ObservableProperty]
        private string valueTitleText = "Value:";

        [ObservableProperty]
        private string functionText = "";

        [ObservableProperty]
        private Visibility functionVisibility = Visibility.Hidden;

        [ObservableProperty]
        private string dataPointsText = AppInfoSingleton.Instance.CurrentTemplate.CurrentDataPoints;

        [ObservableProperty]
        private bool functionNeededIsChecked = false;

        [ObservableProperty]
        private bool dataNeededIsChecked = false;

        //Constructor
        public AddCustomTerminalNodeViewModel()
        {
            NavBackCommand = new RelayCommand(NavBack);
            AddCustomTerminalNodeCommand = new RelayCommand(AddCustomTerminalNode);
            FunctionNeededCommand = new RelayCommand(FunctionNeeded);
            DataNeededCommand = new RelayCommand(DataNeeded);

            string testString = "1, 2, 0";

            int[] testIntArray = ConvertStringToIntArray(testString);

        }

        //Navigation Functions
        public void NavBack()
        {
            AppInfoSingleton.Instance.CurrentViewModel = new GPR4SelectNodesViewModel();
        }

        //Add custom function node functions
        public void AddCustomTerminalNode()
        {
            try
            {
                if (!FunctionNeededIsChecked && !DataNeededIsChecked)
                {
                    AppInfoSingleton.Instance.CurrentTemplate.AddTerminalNode(new TerminalNode(SymbolText, 0, NodeDescriptionText, true, Convert.ToDouble(ValueText), false));
                }
                else if (DataNeededIsChecked && !FunctionNeededIsChecked)
                {
                    AppInfoSingleton.Instance.CurrentTemplate.AddTerminalNode(new TerminalNode(SymbolText, 0, NodeDescriptionText, true, Convert.ToDouble(ValueText), true));
                }
                else if (FunctionNeededIsChecked && !DataNeededIsChecked)
                {
                    int[] FunctionData = ConvertStringToIntArray(ValueText);
                    AppInfoSingleton.Instance.CurrentTemplate.AddCustomTerminalNode(SymbolText, NodeDescriptionText, FunctionText, FunctionData);
                }

                ErrorColour = Brushes.Green;
                ErrorMessage = "*Added Node";
            }
            catch
            {
                ErrorColour = Brushes.Red;
                ErrorMessage = "*Failed to add node.";
            }
        }

        public void FunctionNeeded()
        {
            DataNeededIsChecked = false;

            if (FunctionNeededIsChecked)
            {
                FunctionVisibility = Visibility.Visible;
                ValueTitleText = "Datapoint(s):";
            }
            else
            {
                FunctionVisibility = Visibility.Hidden;
                ValueTitleText = "Value:";
            }
        }

        public void DataNeeded()
        {
            FunctionNeededIsChecked = false;
            FunctionVisibility = Visibility.Hidden;

            if (DataNeededIsChecked)
            {
                ValueTitleText = "Datapoint:";
            }
            else
            {
                ValueTitleText = "Value:";
            }
        }

        public int[] ConvertStringToIntArray(string originalString)
        {
            string[] stringArray = originalString.Split(", ");
            int[] intArray = new int[stringArray.Length];

            for (int i = 0; i < stringArray.Length; i++)
            {
                intArray[i] = Convert.ToInt32(stringArray[i]);
            }

            return intArray;
        }
    }
}
