using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using TreeGPDesigner.MVVM.Model;

namespace TreeGPDesigner.MVVM.ViewModel
{
    //Viewmodel class for Add Custom Function Node View
    public partial class AddCustomFunctionNodeViewModel : ViewModelBase
    {
        //Add Custom Function Node Variables
        private bool root;
        private ExampleFunctionNode[] exampleFunctionNodes =
            {
                new ExampleFunctionNode("+", "Addition", "2", "a => a[0] + a[1]"),
                new ExampleFunctionNode("-", "Subtraction", "2", "a => a[0] - a[1]"),
                new ExampleFunctionNode("*", "Multiplication", "2", "a => a[0] * a[1]"),
                new ExampleFunctionNode("%", "Protected Divide", "2", "a => a[1] == 0 ? 1 : a[0] / a[1]"),
                new ExampleFunctionNode("ABS", "Math.Abs", "1", "Math.Abs(a[0])"),
                new ExampleFunctionNode("+ +", "3 Operand Addition", "3", "a => a[0] + a[1] + a[2]"),
                new ExampleFunctionNode("<=", "Less Than or Equal To", "2", "a => a[0] <= a[1] ? a[0] : a[1]")
            };
        private int exampleFunctionNodesIndex = 0;

        [ObservableProperty]
        private string errorMessage = "";

        [ObservableProperty]
        private Brush errorColour = Brushes.Red;

        [ObservableProperty]
        private string symbolText = "";

        [ObservableProperty]
        private string nodeDescriptionText = "";

        [ObservableProperty]
        private string noOperandsText = "";

        [ObservableProperty]
        private string functionText = "";

        [ObservableProperty]
        private string exampleSymbolText = "";

        [ObservableProperty]
        private string exampleNodeDescriptionText = "";

        [ObservableProperty]
        private string exampleNoOperandsText = "";

        [ObservableProperty]
        private string exampleFunctionText = "";

        [ObservableProperty]
        private string titleText = "";

        [ObservableProperty]
        private Visibility terminalNodeButtonVisibility;

        //Commands
        public ICommand NavBackCommand { get; }
        public ICommand NavAddTerminalNodeCommand { get; }
        public ICommand AddCustomFunctionNodeCommand { get; }
        public ICommand NewExampleCommand { get; }

        //Constructor
        public AddCustomFunctionNodeViewModel(bool root)
        {
            NavBackCommand = new RelayCommand(NavBack);
            NavAddTerminalNodeCommand = new RelayCommand(NavAddTerminalNode);
            AddCustomFunctionNodeCommand = new RelayCommand(AddCustomFunctionNode);
            NewExampleCommand = new RelayCommand(NewExample);
           
            this.root = root;

            if (root)
            {
                TitleText = "Add Custom Root Function Node";
                TerminalNodeButtonVisibility = Visibility.Visible;
            }
            else
            {
                TitleText = "Add Custom Function Node";
                TerminalNodeButtonVisibility = Visibility.Collapsed;
            }

            NewExample();
        }

        //Navigation Functions
        public void NavBack()
        {
            AppInfoSingleton.Instance.CurrentViewModel = new GPR4SelectNodesViewModel();
        }

        public void NavAddTerminalNode()
        {
            AppInfoSingleton.Instance.CurrentViewModel = new AddCustomTerminalNodeViewModel(true);
        }

        //Add custom function node functions
        public void AddCustomFunctionNode()
        {
            try
            {
                int initialFunctionNodesCount = AppInfoSingleton.Instance.CurrentTemplate.FunctionNodes.Count;
                int initialRootFunctionNodesCount = AppInfoSingleton.Instance.CurrentTemplate.FunctionRootNodes.Count;

                if (root)
                {
                    AppInfoSingleton.Instance.CurrentTemplate.AddCustomRootFunctionNode(SymbolText, Convert.ToInt32(NoOperandsText), NodeDescriptionText, FunctionText);
                }
                else
                {
                    AppInfoSingleton.Instance.CurrentTemplate.AddCustomFunctionNode(SymbolText, Convert.ToInt32(NoOperandsText), NodeDescriptionText, FunctionText);
                }

                if (root)
                {
                    if (initialRootFunctionNodesCount == AppInfoSingleton.Instance.CurrentTemplate.FunctionRootNodes.Count)
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
                    if (initialFunctionNodesCount == AppInfoSingleton.Instance.CurrentTemplate.FunctionNodes.Count)
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
                        Node functionNodeCopy = AppInfoSingleton.Instance.CurrentTemplate.CopyNode(
                            AppInfoSingleton.Instance.CurrentTemplate.FunctionNodes[AppInfoSingleton.Instance.CurrentTemplate.FunctionNodes.Count - 1]);

                        for (int i = 0; i < Convert.ToInt32(NoOperandsText); i++)
                        {
                            functionNodeCopy.ChildNodes.Add(new TerminalNode("2", 0, 2, false));
                        }

                        functionNodeCopy.Eval();
                    }
                    catch 
                    {
                        AppInfoSingleton.Instance.CurrentTemplate.FunctionNodes.Remove(
                            AppInfoSingleton.Instance.CurrentTemplate.FunctionNodes[AppInfoSingleton.Instance.CurrentTemplate.FunctionNodes.Count - 1]);
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

        public void NewExample()
        {
            if (exampleFunctionNodesIndex == exampleFunctionNodes.Length)
            {
                exampleFunctionNodesIndex = 0;
            }

            ExampleSymbolText = exampleFunctionNodes[exampleFunctionNodesIndex].SymbolText;
            ExampleNodeDescriptionText = exampleFunctionNodes[exampleFunctionNodesIndex].NodeDescriptionText;
            ExampleNoOperandsText = exampleFunctionNodes[exampleFunctionNodesIndex].NoOperandsText;
            ExampleFunctionText = exampleFunctionNodes[exampleFunctionNodesIndex].FunctionText;

            exampleFunctionNodesIndex++;
        }
    }

    public class ExampleFunctionNode
    {
        private string symbolText;
        private string nodeDescriptionText;
        private string noOperandsText;
        private string functionText;

        public ExampleFunctionNode(string symbolText, string nodeDescriptionText, string noOperandsText, string functionText)
        {
            this.symbolText = symbolText;
            this.nodeDescriptionText = nodeDescriptionText;
            this.noOperandsText = noOperandsText;
            this.functionText = functionText;
        }

        public string SymbolText { get => symbolText; set => symbolText = value; }
        public string NodeDescriptionText { get => nodeDescriptionText; set => nodeDescriptionText = value; }
        public string NoOperandsText { get => noOperandsText; set => noOperandsText = value; }
        public string FunctionText { get => functionText; set => functionText = value; }
    }
}
