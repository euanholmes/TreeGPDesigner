using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using TreeGPDesigner.MVVM.Model;

namespace TreeGPDesigner
{
    public sealed partial class AppInfoSingleton
    {
        private static AppInfoSingleton instance = null;

        private AppInfoSingleton()
        {
        }

        public static AppInfoSingleton Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AppInfoSingleton();
                }
                return instance;
            }
        }

        //Current view model
        public event Action CurrentViewModelChanged;
        private object? currentViewModel;

        public object? CurrentViewModel
        {
            get => currentViewModel;
            set
            {
                currentViewModel = value;
                OnCurrentViewModelChanged();
            }
        }

        private void OnCurrentViewModelChanged()
        {
            CurrentViewModelChanged?.Invoke();
        }

        //Current function node outline brush
        public event Action CurrentFunctionNodeOutlineBrushChanged;
        private Brush? currentFunctionNodeOutlineBrush;

        public Brush? CurrentFunctionNodeOutlineBrush
        {
            get => currentFunctionNodeOutlineBrush;
            set
            {
                currentFunctionNodeOutlineBrush = value;
                OnCurrentFunctionNodeOutlineBrushChanged();
            }
        }

        private void OnCurrentFunctionNodeOutlineBrushChanged()
        {
            CurrentFunctionNodeOutlineBrushChanged?.Invoke();
        }

        //Current function node background brush.
        public event Action CurrentFunctionNodeBackgroundBrushChanged;
        private Brush? currentFunctionNodeBackgroundBrush;

        public Brush? CurrentFunctionNodeBackgroundBrush
        {
            get => currentFunctionNodeBackgroundBrush;
            set
            {
                currentFunctionNodeBackgroundBrush = value;
                OnCurrentFunctionNodeBackgroundBrushChanged();
            }
        }

        private void OnCurrentFunctionNodeBackgroundBrushChanged()
        {
            CurrentFunctionNodeBackgroundBrushChanged?.Invoke();
        }

        //Current terminal node outline brush.
        public event Action CurrentTerminalNodeOutlineBrushChanged;
        private Brush? currentTerminalNodeOutlineBrush;

        public Brush? CurrentTerminalNodeOutlineBrush
        {
            get => currentTerminalNodeOutlineBrush;
            set
            {
                currentTerminalNodeOutlineBrush = value;
                OnCurrentTerminalNodeOutlineBrushChanged();
            }
        }

        private void OnCurrentTerminalNodeOutlineBrushChanged()
        {
            CurrentTerminalNodeOutlineBrushChanged?.Invoke();
        }

        //Current terminal node background brush
        public event Action CurrentTerminalNodeBackgroundBrushChanged;
        private Brush? currentTerminalNodeBackgroundBrush;

        public Brush? CurrentTerminalNodeBackgroundBrush
        {
            get => currentTerminalNodeBackgroundBrush;
            set
            {
                currentTerminalNodeBackgroundBrush = value;
                OnCurrentTerminalNodeBackgroundBrushChanged();
            }
        }

        private void OnCurrentTerminalNodeBackgroundBrushChanged()
        {
            CurrentTerminalNodeBackgroundBrushChanged?.Invoke();
        }









        public static ObservableCollection<NodePlot> GetTreePlot(ObservableCollection<NodePlot> treePlot, Node node, Brush functionOutlineBrush, Brush functionBackgroundBrush, 
            Brush terminalOutlineBrush, Brush terminalBackgroundBrush)
        {
            if (node.GetType() == typeof(FunctionNode))
            {
                if (node.Parent == null)
                {
                    NodePlot nodePlot = new NodePlot(node.XPos * 100, node.YPos * 100, 50, 50, functionBackgroundBrush, functionOutlineBrush, new CornerRadius(50), node.Symbol, 0, 0, 0, 0);
                    treePlot.Add(nodePlot);
                }
                else
                {
                    NodePlot nodePlot = new NodePlot(node.XPos * 100, node.YPos * 100, 50, 50, functionBackgroundBrush, functionOutlineBrush, new CornerRadius(50), node.Symbol,
                        (node.XPos * 100) + 25, (node.YPos * 100) + 25, (node.Parent.XPos * 100) + 25, (node.Parent.YPos * 100) + 25);
                    treePlot.Add(nodePlot);
                }
            }
            else
            {
                if (node.Parent == null)
                {
                    NodePlot nodePlot = new NodePlot(node.XPos * 100, node.YPos * 100, 50, 50, terminalBackgroundBrush, terminalOutlineBrush, new CornerRadius(0), node.Symbol, 0, 0, 0, 0);
                    treePlot.Add(nodePlot);
                }
                else
                {
                    NodePlot nodePlot = new NodePlot(node.XPos * 100, node.YPos * 100, 50, 50, terminalBackgroundBrush, terminalOutlineBrush, new CornerRadius(0), node.Symbol,
                        (node.XPos * 100) + 25, (node.YPos * 100) + 25, (node.Parent.XPos * 100) + 25, (node.Parent.YPos * 100) + 25);
                    treePlot.Add(nodePlot);
                }
            }

            foreach (Node childNode in node.ChildNodes)
            {
                GetTreePlot(treePlot, childNode, functionOutlineBrush, functionBackgroundBrush, terminalOutlineBrush, terminalBackgroundBrush);
            }

            return treePlot;
        }
    }
}
