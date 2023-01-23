using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
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

        [ObservableProperty]
        private string titleText = "";

        [ObservableProperty]
        private Visibility functionNodeButtonVisibility;

        //Commands
        public ICommand NavBackCommand { get; }
        public ICommand NavAddFunctionNodeCommand { get; }
        public ICommand AddCustomTerminalNodeCommand { get; }
        public ICommand FunctionNeededCommand { get; }
        public ICommand DataNeededCommand { get; }

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
                    if (root)
                    {
                        AppInfoSingleton.Instance.CurrentTemplate.AddRootNode(new TerminalNode(SymbolText, 0, NodeDescriptionText, true, Convert.ToDouble(ValueText), true));
                    }
                    else
                    {
                        AppInfoSingleton.Instance.CurrentTemplate.AddTerminalNode(new TerminalNode(SymbolText, 0, NodeDescriptionText, true, Convert.ToDouble(ValueText), true));
                    }
                }
                else if (FunctionNeededIsChecked && !DataNeededIsChecked)
                {
                    int[] FunctionData = ConvertStringToIntArray(ValueText);

                    if (root)
                    {
                        AppInfoSingleton.Instance.CurrentTemplate.AddCustomRootTerminalNode(SymbolText, NodeDescriptionText, FunctionText, FunctionData);
                    }
                    else
                    {
                        AppInfoSingleton.Instance.CurrentTemplate.AddCustomTerminalNode(SymbolText, NodeDescriptionText, FunctionText, FunctionData);
                    }
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
