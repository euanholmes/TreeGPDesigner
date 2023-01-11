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
        private List<TerminalNode> terminalNodes = AppInfoSingleton.Instance.CurrentTemplate.TerminalNodes;

        [ObservableProperty]
        private List<FunctionNode> functionRootNodes = AppInfoSingleton.Instance.CurrentTemplate.FunctionRootNodes;

        [ObservableProperty]
        private List<TerminalNode> terminalRootNodes = AppInfoSingleton.Instance.CurrentTemplate.TerminalRootNodes;

        [ObservableProperty]
        private List<SelectNode> selectFunctionNodes = new List<SelectNode>();

        [ObservableProperty]
        private List<SelectNode> selectTerminalNodes = new List<SelectNode>();

        [ObservableProperty]
        private List<SelectNode> selectRootNodes = new List<SelectNode>();

        //Constructor
        public GPR4SelectNodesViewModel()
        {
            NavHomeMenuCommand = new RelayCommand(NavHomeMenu);
            NavNextCommand = new RelayCommand(NavNext);
            NavBackCommand = new RelayCommand(NavBack);

            GetSelectNodes();
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

        //Select Nodes Function
        public void GetSelectNodes()
        {
            foreach (FunctionNode node in FunctionNodes)
            {
                SelectFunctionNodes.Add(new SelectNode(node.Symbol, node.NodeDescription, BrushSet[0], BrushSet[1], TextColour, new CornerRadius(30), Background, NormalButtonColour,
                    FunctionNodes.IndexOf(node).ToString(), node.IsSelected, "function"));
            }

            foreach (TerminalNode node in TerminalNodes)
            {
                SelectTerminalNodes.Add(new SelectNode(node.Symbol, node.NodeDescription, BrushSet[2], BrushSet[3], TextColour, new CornerRadius(0), Background, NormalButtonColour,
                    TerminalNodes.IndexOf(node).ToString(), node.IsSelected, "terminal"));
            }

            foreach (FunctionNode node in FunctionRootNodes)
            {
                SelectRootNodes.Add(new SelectNode(node.Symbol, node.NodeDescription, BrushSet[0], BrushSet[1], TextColour, new CornerRadius(30), Background, NormalButtonColour,
                    FunctionRootNodes.IndexOf(node).ToString(), node.IsSelected, "rootFunction"));
            }

            foreach (TerminalNode node in TerminalRootNodes)
            {
                SelectRootNodes.Add(new SelectNode(node.Symbol, node.NodeDescription, BrushSet[2], BrushSet[3], TextColour, new CornerRadius(0), Background, NormalButtonColour,
                    TerminalRootNodes.IndexOf(node).ToString(), node.IsSelected, "rootTerminal"));
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
        private bool? isSelected;
        private string? functionType;

        public SelectNode(string? symbol, string? nodeDescription, Brush? nodeOutline, Brush? nodeFill, Brush? textColour, CornerRadius? cornerRadius, Brush? backgroundColour,
            Brush? normalButtonColour, string? checkBoxCommandParameter, bool? isSelected, string? functionType)
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
            this.isSelected = isSelected;
            this.functionType = functionType;

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
        public bool? IsSelected { get => isSelected; set => isSelected = value; }
        public string? FunctionType { get => functionType; set => functionType = value; }

        public void CheckBox(string nodeNumString)
        {
            int nodeNumInt = Convert.ToInt32(nodeNumString);

            if (FunctionType == "function")
            {
                if (AppInfoSingleton.Instance.CurrentTemplate.FunctionNodes[nodeNumInt].IsSelected == false)
                {
                    AppInfoSingleton.Instance.CurrentTemplate.FunctionNodes[nodeNumInt].IsSelected = true;
                }
                else
                {
                    AppInfoSingleton.Instance.CurrentTemplate.FunctionNodes[nodeNumInt].IsSelected = false;
                }
            }
            else if (FunctionType == "terminal")
            {
                if (AppInfoSingleton.Instance.CurrentTemplate.TerminalNodes[nodeNumInt].IsSelected == false)
                {
                    AppInfoSingleton.Instance.CurrentTemplate.TerminalNodes[nodeNumInt].IsSelected = true;
                }
                else
                {
                    AppInfoSingleton.Instance.CurrentTemplate.TerminalNodes[nodeNumInt].IsSelected = false;
                }
            }
            else if(FunctionType == "rootFunction")
            {
                if (AppInfoSingleton.Instance.CurrentTemplate.FunctionRootNodes[nodeNumInt].IsSelected == false)
                {
                    AppInfoSingleton.Instance.CurrentTemplate.FunctionRootNodes[nodeNumInt].IsSelected = true;
                }
                else
                {
                    AppInfoSingleton.Instance.CurrentTemplate.FunctionRootNodes[nodeNumInt].IsSelected = false;
                }
            }
            else if (FunctionType == "rootTerminal")
            {
                if (AppInfoSingleton.Instance.CurrentTemplate.TerminalRootNodes[nodeNumInt].IsSelected == false)
                {
                    AppInfoSingleton.Instance.CurrentTemplate.TerminalRootNodes[nodeNumInt].IsSelected = true;
                }
                else
                {
                    AppInfoSingleton.Instance.CurrentTemplate.TerminalRootNodes[nodeNumInt].IsSelected = false;
                }
            }
        }
    }
}
