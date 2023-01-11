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
    public partial class GPR4SelectNodesViewModel : ObservableObject
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

        //Node/Tree Variables
        [ObservableProperty]
        private Brush[]? brushSet = AppInfoSingleton.Instance.CurrentBrushSet;

        [ObservableProperty]
        private Brush? background = AppInfoSingleton.Instance.CurrentBackground;

        //Commands
        public ICommand NavHomeMenuCommand { get; }
        public ICommand NavNextCommand { get; }
        public ICommand NavBackCommand { get; }

        //Select Nodes Variables
        [ObservableProperty]
        private List<FunctionNode> functionNodes = AppInfoSingleton.Instance.CurrentTemplate.FunctionNodes;

        [ObservableProperty]
        private List<SelectNode> selectFunctionNodes = new List<SelectNode>();

        //Constructor
        public GPR4SelectNodesViewModel()
        {
            NavHomeMenuCommand = new RelayCommand(NavHomeMenu);
            NavNextCommand = new RelayCommand(NavNext);
            NavBackCommand = new RelayCommand(NavBack);

            GetSelectFunctionNodes();
        }

        //Navigation Functions
        public void NavHomeMenu()
        {
            AppInfoSingleton.Instance.CurrentViewModel = new HomeViewModel();
        }

        public void NavNext()
        {
            AppInfoSingleton.Instance.CurrentViewModel = new GPR5SelectTrainingDataViewModel();
        }

        public void NavBack()
        {
            AppInfoSingleton.Instance.CurrentViewModel = new GPR3SelectFitnessFunctionViewModel();
        }

        //Select Nodes Functions
        public void GetSelectFunctionNodes()
        {
            foreach (FunctionNode node in FunctionNodes)
            {
                SelectFunctionNodes.Add(new SelectNode(node.Symbol, node.NodeDescription, BrushSet[0], BrushSet[1], TextColour, new CornerRadius(30), Background, NormalButtonColour,
                    FunctionNodes.IndexOf(node).ToString()));
            }
        }
    }

    public class SelectNode
    {
        private string? symbol;
        private string? nodeDescription;
        private Brush? nodeOutline;
        private Brush? nodeFill;
        private Brush? textColour;
        private CornerRadius? cornerRadius;
        private Brush? backgroundColour;
        private Brush? normalButtonColour;
        private ICommand checkBoxCommand;
        private string? checkBoxCommandParameter;

        public SelectNode(string? symbol, string? nodeDescription, Brush? nodeOutline, Brush? nodeFill, Brush? textColour, CornerRadius? cornerRadius, Brush? backgroundColour, 
            Brush? normalButtonColour, string? checkBoxCommandParameter)
        {
            this.symbol = symbol;
            this.nodeDescription = nodeDescription;
            this.nodeOutline = nodeOutline;
            this.nodeFill = nodeFill;
            this.textColour = textColour;
            this.cornerRadius = cornerRadius;
            this.backgroundColour = backgroundColour;
            this.normalButtonColour = normalButtonColour;
            this.checkBoxCommandParameter = checkBoxCommandParameter;
            this.checkBoxCommand = new RelayCommand<string>(param => CheckBox(param));
        }

        public string? Symbol { get => symbol; set => symbol = value; }
        public Brush? NodeOutline { get => nodeOutline; set => nodeOutline = value; }
        public Brush? NodeFill { get => nodeFill; set => nodeFill = value; }
        public Brush? TextColour { get => textColour; set => textColour = value; }
        public CornerRadius? CornerRadius { get => cornerRadius; set => cornerRadius = value; }
        public Brush? BackgroundColour { get => backgroundColour; set => backgroundColour = value; }
        public Brush? NormalButtonColour { get => normalButtonColour; set => normalButtonColour = value; }
        public ICommand CheckBoxCommand { get => checkBoxCommand; set => checkBoxCommand = value; }
        public string? NodeDescription { get => nodeDescription; set => nodeDescription = value; }
        public string? CheckBoxCommandParameter { get => checkBoxCommandParameter; set => checkBoxCommandParameter = value; }

        public void CheckBox(string nodeNumString)
        {
            int nodeNumInt = Convert.ToInt32(nodeNumString);
            Trace.WriteLine(nodeNumInt);
        }
    }
}
