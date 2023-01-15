using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using TreeGPDesigner.MVVM.Model;

namespace TreeGPDesigner.MVVM.ViewModel
{
    public partial class GPBasics7ViewModel : ObservableObject
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

        //Tree Drawing Variables
        [ObservableProperty]
        private Brush[]? brushSet = AppInfoSingleton.Instance.CurrentBrushSet;

        [ObservableProperty]
        private ImageSource? zoomIconSource = AppInfoSingleton.Instance.CurrentZoomIcon;

        [ObservableProperty]
        public ObservableCollection<NodePlot> displayTreePlot = new();

        //Commands
        public ICommand NavHomeMenuCommand { get; }
        public ICommand NavTutorialsMenuCommand { get; }
        public ICommand NavPreviousCommand { get; }
        public ICommand CrossoverTreesCommand { get; }

        //GP Basics 7 Variables
        private BinPackingTemplate bpTemplate = new();

        private static Random random = new();

        private Node displayTree;

        private Node parentTree1 = new FunctionNode("+", 2, a => a[0] + a[1], false); 

        private Node parentTree2 = new FunctionNode(">=", 2, a => a[0] >= a[1] ? 1 : 0, true);

        //Constructor
        public GPBasics7ViewModel()
        {
            NavHomeMenuCommand = new RelayCommand(NavHomeMenu);
            NavTutorialsMenuCommand = new RelayCommand(NavTutorialsMenu);
            NavPreviousCommand = new RelayCommand(NavPrevious);
            CrossoverTreesCommand = new RelayCommand(CrossoverTrees);

            parentTree1.ChildNodes.Add(new FunctionNode(">", 2, a => a[0] > a[1] ? 1 : 0, true));
            parentTree1.ChildNodes.Add(new TerminalNode("-1", 0, -1, false));
            parentTree1.ChildNodes[0].ChildNodes.Add(new TerminalNode("1", 0, 1, false));
            parentTree1.ChildNodes[0].ChildNodes.Add(new TerminalNode("X", 0, 1, true));

            parentTree2.ChildNodes.Add(new FunctionNode("-", 2, a => a[0] - a[1], true));
            parentTree2.ChildNodes.Add(new TerminalNode("2Y", 0, 1, true));
            parentTree2.ChildNodes[0].ChildNodes.Add(new TerminalNode("AB", 0, 1, true));
            parentTree2.ChildNodes[0].ChildNodes.Add(new TerminalNode("34", 0, -1, false));
        }

        //Navigation Functions
        public void NavHomeMenu()
        {
            AppInfoSingleton.Instance.CurrentViewModel = new HomeViewModel();
        }

        public void NavTutorialsMenu()
        {
            AppInfoSingleton.Instance.CurrentViewModel = new TutorialsMenuViewModel();
        }

        public void NavPrevious()
        {
            AppInfoSingleton.Instance.CurrentViewModel = new GPBasics6ViewModel();
        }

        //GP Basics 7 Functions
        public void CrossoverTrees()
        {
            Node parentTree1Copy = bpTemplate.CopyNode(parentTree1);
            bpTemplate.Crossover(parentTree1Copy, parentTree2, 3);
            displayTree = bpTemplate.CopyNode(parentTree1Copy);
            TreeDrawingAlgorithm.CalculateNodePositions(displayTree);
            DisplayTreePlot.Clear();
            DisplayTreePlot = AppInfoSingleton.GetTreePlot(DisplayTreePlot, displayTree, BrushSet);
        }
    }
}
