using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using TreeGPDesigner.MVVM.Model;

namespace TreeGPDesigner.MVVM.ViewModel
{
    //Viewmodel class for Select Nodes View
    public partial class GPR4SelectNodesViewModel : ViewModelBase
    {
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

        [ObservableProperty]
        private string errorMessage = "";

        //Commands
        public ICommand NavNextCommand { get; }
        public ICommand NavBackCommand { get; }
        public ICommand AddFunctionNodeCommand { get; }
        public ICommand AddTerminalNodeCommand { get; }
        public ICommand AddRootNodeCommand { get; }

        //Constructor
        public GPR4SelectNodesViewModel()
        {
            NavNextCommand = new RelayCommand(NavNext);
            NavBackCommand = new RelayCommand(NavBack);
            AddFunctionNodeCommand = new RelayCommand(AddFunctionNode);
            AddTerminalNodeCommand = new RelayCommand(AddTerminalNode);
            AddRootNodeCommand = new RelayCommand(AddRootNode);

            GetSelectNodes();
        }

        //Navigation Functions
        public void NavNext()
        {
            bool terminalNodeListEmpty = IsNodeListEmpty(new List<Node>(AppInfoSingleton.Instance.CurrentTemplate.TerminalNodes));
            bool functionRootNodeListEmpty = IsNodeListEmpty(new List<Node>(AppInfoSingleton.Instance.CurrentTemplate.FunctionRootNodes));
            bool terminalRootNodeListEmpty = IsNodeListEmpty(new List<Node>(AppInfoSingleton.Instance.CurrentTemplate.TerminalRootNodes));

            if ((functionRootNodeListEmpty && terminalRootNodeListEmpty) ||  (terminalNodeListEmpty && !functionRootNodeListEmpty))
            {
                ErrorMessage = "*Select more nodes.";
            }
            else
            {
                AppInfoSingleton.Instance.CurrentViewModel = new GPR5SelectTrainingDataViewModel();
            }
        }

        public void NavBack()
        {
            AppInfoSingleton.Instance.CurrentViewModel = new GPR3SelectFitnessFunctionViewModel();
        }

        //Select Nodes Functions
        public void AddFunctionNode()
        {
            AppInfoSingleton.Instance.CurrentViewModel = new AddCustomFunctionNodeViewModel(false);
        }

        public void AddTerminalNode() 
        {
            AppInfoSingleton.Instance.CurrentViewModel = new AddCustomTerminalNodeViewModel(false);
        }

        public void AddRootNode()
        {
            AppInfoSingleton.Instance.CurrentViewModel = new AddCustomFunctionNodeViewModel(true);
        }

        public bool IsNodeListEmpty(List<Node> nodeList)
        {
            bool nodeListEmpty = true;

            foreach(Node node in nodeList)
            {
                if (node.IsSelected)
                {
                    nodeListEmpty = false;
                    break;
                }
            }
            return nodeListEmpty;
        }

        public void GetSelectNodes()
        {
            foreach (FunctionNode node in FunctionNodes)
            {
                SelectFunctionNodes.Add(new SelectNode(node.Symbol, node.NodeDescription, BrushSet[0], BrushSet[1], TextColour, new CornerRadius(30), Background, NormalButtonColour,
                    FunctionNodes.IndexOf(node).ToString(), node.IsSelected, "function", this));
            }

            foreach (TerminalNode node in TerminalNodes)
            {
                SelectTerminalNodes.Add(new SelectNode(node.Symbol, node.NodeDescription, BrushSet[2], BrushSet[3], TextColour, new CornerRadius(0), Background, NormalButtonColour,
                    TerminalNodes.IndexOf(node).ToString(), node.IsSelected, "terminal", this));
            }

            foreach (FunctionNode node in FunctionRootNodes)
            {
                SelectRootNodes.Add(new SelectNode(node.Symbol, node.NodeDescription, BrushSet[0], BrushSet[1], TextColour, new CornerRadius(30), Background, NormalButtonColour,
                    FunctionRootNodes.IndexOf(node).ToString(), node.IsSelected, "rootFunction", this));
            }

            foreach (TerminalNode node in TerminalRootNodes)
            {
                SelectRootNodes.Add(new SelectNode(node.Symbol, node.NodeDescription, BrushSet[2], BrushSet[3], TextColour, new CornerRadius(0), Background, NormalButtonColour,
                    TerminalRootNodes.IndexOf(node).ToString(), node.IsSelected, "rootTerminal", this));
            }
        }
    }

    //Class used for the item control in this view.
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
        private GPR4SelectNodesViewModel? selectNodesVM;

        public SelectNode(string? symbol, string? nodeDescription, Brush? nodeOutline, Brush? nodeFill, Brush? textColour, CornerRadius? cornerRadius, Brush? backgroundColour,
            Brush? normalButtonColour, string? checkBoxCommandParameter, bool? isSelected, string? functionType, GPR4SelectNodesViewModel? selectNodesVM)
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
            this.selectNodesVM = selectNodesVM;
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
        public GPR4SelectNodesViewModel? SelectNodesVM { get => selectNodesVM; set => selectNodesVM = value; }

        public void CheckBox(string nodeNumString)
        {
            selectNodesVM.ErrorMessage = "";
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
