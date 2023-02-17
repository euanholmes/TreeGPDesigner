using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using TreeGPDesigner.MVVM.Model;

namespace TreeGPDesigner.MVVM.ViewModel
{
    //Viewmodel class for Add Custom Terminal Node View
    public partial class AddCustomTerminalNodeViewModel : ViewModelBase
    {
        //Add Custom Terminal Node Variables
        private bool root;
        private ExampleTerminalNode[] exampleTerminalNodes =
            {
                new ExampleTerminalNode("CBW", "Current Bin Weight", "0", "", true, false),
                new ExampleTerminalNode("CI", "Current Item", "1", "", true, false),
                new ExampleTerminalNode("BC", "Bin Capacity", "2", "", true, false),
                new ExampleTerminalNode("FS", "Free Space", "2, 0", "a => Convert.ToDouble(a[0]) - Convert.ToDouble(a[1])", false, true),
                new ExampleTerminalNode("-1", "-1", "-1", "", false, false),
                new ExampleTerminalNode("1", "1", "1", "", false, false),
                new ExampleTerminalNode("0", "0", "0", "", false, false),
                new ExampleTerminalNode("3.14", "3.14", "3.14", "", false, false),
                new ExampleTerminalNode("2BC", "2 X Bin Capacity", "2", "a => Convert.ToDouble(a[0]) * 2", false, true),
                new ExampleTerminalNode("LB", "On Last Bin", "3", "a => (bool)a[0] == true ? 1 : 0", false, true),
                new ExampleTerminalNode("BFB", "Best Fitting Bin", "4", "a => (bool)a[0] == true ? 1 : 0", false, true)
            };
        private int exampleTerminalNodesIndex = 0;

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
        private string exampleSymbolText = "";

        [ObservableProperty]
        private string exampleNodeDescriptionText = "";

        [ObservableProperty]
        private string exampleValueText = "";

        [ObservableProperty]
        private string exampleFunctionText = "";

        [ObservableProperty]
        private Visibility functionVisibility = Visibility.Hidden;

        [ObservableProperty]
        private string dataPointsText = AppInfoSingleton.Instance.CurrentTemplate.CurrentDataPoints;

        [ObservableProperty]
        private bool functionNeededIsChecked = false;

        [ObservableProperty]
        private bool dataNeededIsChecked = false;

        [ObservableProperty]
        private bool exampleFunctionNeededIsChecked = false;

        [ObservableProperty]
        private bool exampleDataNeededIsChecked = false;

        [ObservableProperty]
        private string titleText = "";

        [ObservableProperty]
        private Visibility functionNodeButtonVisibility;

        [ObservableProperty]
        private Visibility exampleFunctionVisibility;

        //Commands
        public ICommand NavBackCommand { get; }
        public ICommand NavAddFunctionNodeCommand { get; }
        public ICommand AddCustomTerminalNodeCommand { get; }
        public ICommand FunctionNeededCommand { get; }
        public ICommand DataNeededCommand { get; }
        public ICommand NewExampleCommand { get; }

        //Constructor
        public AddCustomTerminalNodeViewModel(bool root)
        {
            this.root = root;

            if (root)
            {
                TitleText = "Add Custom Root Terminal Node";
                FunctionNodeButtonVisibility = Visibility.Visible;
            }
            else
            {
                TitleText = "Add Custom Terminal Node";
                FunctionNodeButtonVisibility = Visibility.Collapsed;
            }

            NavBackCommand = new RelayCommand(NavBack);
            NavAddFunctionNodeCommand = new RelayCommand(NavAddFunctionNode);
            AddCustomTerminalNodeCommand = new RelayCommand(AddCustomTerminalNode);
            FunctionNeededCommand = new RelayCommand(FunctionNeeded);
            DataNeededCommand = new RelayCommand(DataNeeded);
            NewExampleCommand = new RelayCommand(NewExample);

            NewExample();
        }

        //Navigation Functions
        public void NavBack()
        {
            AppInfoSingleton.Instance.CurrentViewModel = new GPR4SelectNodesViewModel();
        }

        public void NavAddFunctionNode()
        {
            AppInfoSingleton.Instance.CurrentViewModel = new AddCustomFunctionNodeViewModel(true);
        }

        //Add custom terminal node functions
        public void AddCustomTerminalNode()
        {
            try
            {
                int initialTerminalNodesCount = AppInfoSingleton.Instance.CurrentTemplate.TerminalNodes.Count;
                int initialRootTerminalNodesCount = AppInfoSingleton.Instance.CurrentTemplate.TerminalRootNodes.Count;
                string[] dataPoints = AppInfoSingleton.Instance.CurrentTemplate.CurrentDataPoints.Split("\n");

                if (!FunctionNeededIsChecked && !DataNeededIsChecked)
                {
                    if (root)
                    {
                        AppInfoSingleton.Instance.CurrentTemplate.AddRootNode(new TerminalNode(SymbolText, 0, NodeDescriptionText, true, Convert.ToDouble(ValueText), false));
                    }
                    else
                    {
                        AppInfoSingleton.Instance.CurrentTemplate.AddTerminalNode(new TerminalNode(SymbolText, 0, NodeDescriptionText, true, Convert.ToDouble(ValueText), false));
                    }
                }
                else if (DataNeededIsChecked && !FunctionNeededIsChecked)
                {
                    if (Convert.ToDouble(ValueText) > dataPoints.Length - 1 || Convert.ToDouble(ValueText) < 0)
                    {

                    }
                    else
                    {
                        if (root)
                        {
                            AppInfoSingleton.Instance.CurrentTemplate.AddRootNode(new TerminalNode(SymbolText, 0, NodeDescriptionText, true, Convert.ToDouble(ValueText), true));
                        }
                        else
                        {
                            AppInfoSingleton.Instance.CurrentTemplate.AddTerminalNode(new TerminalNode(SymbolText, 0, NodeDescriptionText, true, Convert.ToDouble(ValueText), true));
                        }
                    }
                }
                else if (FunctionNeededIsChecked && !DataNeededIsChecked)
                {
                    int[] FunctionData = ConvertStringToIntArray(ValueText);
                    bool validPoints = true;

                    foreach(int dataPoint in FunctionData)
                    {
                        if (dataPoint > dataPoints.Length - 1)
                        {
                            validPoints = false;
                            break;
                        }
                    }

                    if (validPoints)
                    {
                        if (root)
                        {
                            AppInfoSingleton.Instance.CurrentTemplate.AddCustomRootTerminalNode(SymbolText, NodeDescriptionText, FunctionText, FunctionData);
                        }
                        else
                        {
                            AppInfoSingleton.Instance.CurrentTemplate.AddCustomTerminalNode(SymbolText, NodeDescriptionText, FunctionText, FunctionData);
                        }
                    }
                }

                if (root)
                {
                    if (initialRootTerminalNodesCount == AppInfoSingleton.Instance.CurrentTemplate.TerminalRootNodes.Count)
                    {
                        ErrorColour = Brushes.Red;
                        ErrorMessage = "*Failed to add node.";
                    }
                    else
                    {
                        ErrorColour = Brushes.Green;
                        ErrorMessage = "*Added Node";
                    }
                }
                else
                {
                    if (initialTerminalNodesCount == AppInfoSingleton.Instance.CurrentTemplate.TerminalNodes.Count)
                    {
                        ErrorColour = Brushes.Red;
                        ErrorMessage = "*Failed to add node.";
                    }
                    else
                    {
                        ErrorColour = Brushes.Green;
                        ErrorMessage = "*Added Node";
                    }
                }

                //Number of operands check
                if (ErrorMessage == "*Added Node")
                {
                    try
                    {
                        int[] FunctionData = ConvertStringToIntArray(ValueText);
                        Node terminalNodeCopy = AppInfoSingleton.Instance.CurrentTemplate.CopyNode(
                            AppInfoSingleton.Instance.CurrentTemplate.TerminalNodes[AppInfoSingleton.Instance.CurrentTemplate.TerminalNodes.Count - 1]);

                        //need to make this not bp specific
                        terminalNodeCopy.Data = new object[] { 20, 10, 15, true, true };
                        terminalNodeCopy.Eval();
                    }
                    catch
                    {
                        AppInfoSingleton.Instance.CurrentTemplate.TerminalNodes.Remove(
                            AppInfoSingleton.Instance.CurrentTemplate.TerminalNodes[AppInfoSingleton.Instance.CurrentTemplate.TerminalNodes.Count - 1]);
                        ErrorColour = Brushes.Red;
                        ErrorMessage = "*Failed to add node.";
                    }
                }
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

        public void NewExample()
        {
            if (exampleTerminalNodesIndex == exampleTerminalNodes.Length)
            {
                exampleTerminalNodesIndex = 0;
            }

            ExampleSymbolText = exampleTerminalNodes[exampleTerminalNodesIndex].SymbolText;
            ExampleNodeDescriptionText = exampleTerminalNodes[exampleTerminalNodesIndex].NodeDescriptionText;
            ExampleValueText = exampleTerminalNodes[exampleTerminalNodesIndex].ValueText;
            ExampleFunctionText = exampleTerminalNodes[exampleTerminalNodesIndex].FunctionText;
            ExampleDataNeededIsChecked = exampleTerminalNodes[exampleTerminalNodesIndex].IsDataNeeded;
            ExampleFunctionNeededIsChecked = exampleTerminalNodes[exampleTerminalNodesIndex].IsFunctionNeeded;

            if (ExampleFunctionNeededIsChecked)
            {
                ExampleFunctionVisibility = Visibility.Visible;
            }
            else
            {
                ExampleFunctionVisibility = Visibility.Collapsed;
            }

            exampleTerminalNodesIndex++;
        }
    }

    public class ExampleTerminalNode
    {
        private string symbolText;
        private string nodeDescriptionText;
        private string valueText;
        private string functionText;
        private bool isDataNeeded;
        private bool isFunctionNeeded;

        public ExampleTerminalNode(string symbolText, string nodeDescriptionText, string valueText, string functionText, bool isDataNeeded, bool isFunctionNeeded)
        {
            this.symbolText = symbolText;
            this.nodeDescriptionText = nodeDescriptionText;
            this.valueText = valueText;
            this.functionText = functionText;
            this.isDataNeeded = isDataNeeded;
            this.isFunctionNeeded = isFunctionNeeded;
        }

        public string SymbolText { get => symbolText; set => symbolText = value; }
        public string NodeDescriptionText { get => nodeDescriptionText; set => nodeDescriptionText = value; }
        public string ValueText { get => valueText; set => valueText = value; }
        public string FunctionText { get => functionText; set => functionText = value; }
        public bool IsDataNeeded { get => isDataNeeded; set => isDataNeeded = value; }
        public bool IsFunctionNeeded { get => isFunctionNeeded; set => isFunctionNeeded = value; }
    }
}
