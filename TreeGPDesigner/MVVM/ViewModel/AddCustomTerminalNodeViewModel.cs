using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

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
        }

        //Navigation Functions
        public void NavBack()
        {
            AppInfoSingleton.Instance.CurrentViewModel = new GPR4SelectNodesViewModel();
        }

        //Add custom function node functions
        public void AddCustomTerminalNode()
        {
           /* try
            {
                AppInfoSingleton.Instance.CurrentTemplate.AddCustomFunctionNode(SymbolText, Convert.ToInt32(NoOperandsText), NodeDescriptionText, FunctionText);
                ErrorColour = Brushes.Green;
                ErrorMessage = "*Added Node";
            }
            catch
            {
                ErrorColour = Brushes.Red;
                ErrorMessage = "*Failed to add node.";
            }*/
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
    }
}
