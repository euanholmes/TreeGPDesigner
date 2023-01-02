using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
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

        //Brushes
        public static LinearGradientBrush LightBackground = new();
        public static LinearGradientBrush DarkBackground = new();
        public static LinearGradientBrush LightPanel1 = new();
        public static LinearGradientBrush DarkPanel1 = new();
        public static Brush LightNormalButton = (Brush)new BrushConverter().ConvertFrom("#a5e8b4");
        public static Brush DarkNormalButton = (Brush)new BrushConverter().ConvertFrom("#044343");
        public static Brush LightNavButton = Brushes.LightGray;
        public static Brush DarkNavButton = Brushes.Gray;

        private AppInfoSingleton()
        {
            LightBackground.StartPoint = new Point(0, 0);
            LightBackground.EndPoint = new Point(1, 1);
            LightBackground.GradientStops.Add(new GradientStop(Colors.White, 0.0));
            LightBackground.GradientStops.Add(new GradientStop(Colors.WhiteSmoke, 1.0));

            DarkBackground.StartPoint = new Point(0, 0);
            DarkBackground.EndPoint = new Point(1, 1);
            DarkBackground.GradientStops.Add(new GradientStop((Color)new ColorConverter().ConvertFrom("#395B64"), 0.0));
            DarkBackground.GradientStops.Add(new GradientStop((Color)new ColorConverter().ConvertFrom("#3F4E4F"), 0.5));
            DarkBackground.GradientStops.Add(new GradientStop((Color)new ColorConverter().ConvertFrom("#2C3639"), 1.0));

            LightPanel1.StartPoint = new Point(0, 0);
            LightPanel1.EndPoint = new Point(1, 1);
            //LightPanel1.GradientStops.Add(new GradientStop((Color)new ColorConverter().ConvertFrom("#FEFCF3"), 0.0));
            //LightPanel1.GradientStops.Add(new GradientStop((Color)new ColorConverter().ConvertFrom("#ECE8DD"), 0.5));
            LightPanel1.GradientStops.Add(new GradientStop((Color)new ColorConverter().ConvertFrom("#DEF5E5"), 1.0));

            DarkPanel1.StartPoint = new Point(0, 0);
            DarkPanel1.EndPoint = new Point(1, 1);
            //DarkPanel1.GradientStops.Add(new GradientStop(Colors.WhiteSmoke, 0.0));
            //DarkPanel1.GradientStops.Add(new GradientStop((Color)new ColorConverter().ConvertFrom("#3F4E4F"), 0.5));
            DarkPanel1.GradientStops.Add(new GradientStop((Color)new ColorConverter().ConvertFrom("#3C6562"), 1.0));
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

        //Which current radio button isChecked (1 - 6)
        public event Action CurrentRadioButtonCheckChanged;
        private int? currentRadioButtonCheck;

        public int? CurrentRadioButtonCheck
        {
            get => currentRadioButtonCheck;
            set
            {
                currentRadioButtonCheck = value;
                OnCurrentRadioButtonCheckChanged();
            }
        }

        private void OnCurrentRadioButtonCheckChanged()
        {
            CurrentRadioButtonCheckChanged?.Invoke();
        }

        //Current Panel1 Colour
        public event Action CurrentPanel1ColorChanged;
        private Brush? currentPanel1Color;

        public Brush? CurrentPanel1Color
        {
            get => currentPanel1Color;
            set
            {
                currentPanel1Color = value;
                OnCurrentPanel1ColorChanged();
            }
        }

        private void OnCurrentPanel1ColorChanged()
        {
            CurrentPanel1ColorChanged?.Invoke();
        }

        //Current Normal Button Colour
        public event Action CurrentNormalButtonColorChanged;
        private Brush? currentNormalButtonColor;

        public Brush? CurrentNormalButtonColor
        {
            get => currentNormalButtonColor;
            set
            {
                currentNormalButtonColor = value;
                OnCurrentNormalButtonColorChanged();
            }
        }

        private void OnCurrentNormalButtonColorChanged()
        {
            CurrentNormalButtonColorChanged?.Invoke();
        }

        //Current Nav Button Colour
        public event Action CurrentNavButtonColorChanged;
        private Brush? currentNavButtonColor;

        public Brush? CurrentNavButtonColor
        {
            get => currentNavButtonColor;
            set
            {
                currentNavButtonColor = value;
                OnCurrentNavButtonColorChanged();
            }
        }

        private void OnCurrentNavButtonColorChanged()
        {
            CurrentNavButtonColorChanged?.Invoke();
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

        //Current toggle button
        public event Action CurrentModeToggleButtonChanged;
        private ImageSource? currentModeToggleButton;

        public ImageSource? CurrentModeToggleButton
        {
            get => currentModeToggleButton;
            set
            {
                currentModeToggleButton = value;
                OnCurrentModeToggleButtonChanged();
            }
        }

        private void OnCurrentModeToggleButtonChanged()
        {
            CurrentModeToggleButtonChanged?.Invoke();
        }

        //Current background colour
        public event Action CurrentBackgroundChanged;
        private Brush? currentBackground;

        public Brush? CurrentBackground
        {
            get => currentBackground;
            set
            {
                currentBackground = value;
                OnCurrentBackgroundChanged();
            }
        }

        private void OnCurrentBackgroundChanged()
        {
            CurrentBackgroundChanged?.Invoke();
        }

        //Current title text colour
        public event Action CurrentTextChanged;
        private Brush? currentText;

        public Brush? CurrentText
        {
            get => currentText;
            set
            {
                currentText = value;
                OnCurrentTextChanged();
            }
        }

        private void OnCurrentTextChanged()
        {
            CurrentTextChanged?.Invoke();
        }

        //Get tree plot function which makes a list of function node and terminal node graphic items based on the trees positons.
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
