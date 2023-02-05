using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
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
        private string titleText = "";

        [ObservableProperty]
        private Visibility terminalNodeButtonVisibility;

        //Commands
        public ICommand NavBackCommand { get; }
        public ICommand NavAddTerminalNodeCommand { get; }
        public ICommand AddCustomFunctionNodeCommand { get; }

        //Constructor
        public AddCustomFunctionNodeViewModel(bool root)
        {
            NavBackCommand = new RelayCommand(NavBack);
            NavAddTerminalNodeCommand = new RelayCommand(NavAddTerminalNode);
            AddCustomFunctionNodeCommand = new RelayCommand(AddCustomFunctionNode);
           
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
    }
}
