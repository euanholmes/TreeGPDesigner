using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

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

        //Constructor
        public AddCustomFunctionNodeViewModel()
        {
            NavBackCommand = new RelayCommand(NavBack);
            AddCustomFunctionNodeCommand = new RelayCommand(AddCustomFunctionNode);
        }

        //Navigation Functions
        public void NavBack()
        {
            AppInfoSingleton.Instance.CurrentViewModel = new GPR4SelectNodesViewModel();
        }

        //Add custom function node functions
        public void AddCustomFunctionNode()
        {
            try
            {
                AppInfoSingleton.Instance.CurrentTemplate.AddCustomFunctionNode(SymbolText, Convert.ToInt32(NoOperandsText), NodeDescriptionText, true, FunctionText);
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
