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
using System.Windows.Media.Animation;
using TreeGPDesigner.MVVM.Model;

namespace TreeGPDesigner.MVVM.ViewModel
{
    public partial class AddCustomFunctionNodeViewModel : ObservableObject
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
        public ICommand NavAddTerminalNodeCommand { get; }
        public ICommand AddCustomFunctionNodeCommand { get; }
        

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
        private string noOperandsText = "";

        [ObservableProperty]
        private string functionText = "";

        [ObservableProperty]
        private string titleText = "";

        [ObservableProperty]
        private Visibility terminalNodeButtonVisibility;

        private bool root;

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
                if (root)
                {
                    AppInfoSingleton.Instance.CurrentTemplate.AddCustomRootFunctionNode(SymbolText, Convert.ToInt32(NoOperandsText), NodeDescriptionText, FunctionText);
                }
                else
                {
                    AppInfoSingleton.Instance.CurrentTemplate.AddCustomFunctionNode(SymbolText, Convert.ToInt32(NoOperandsText), NodeDescriptionText, FunctionText);
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
    }
}
