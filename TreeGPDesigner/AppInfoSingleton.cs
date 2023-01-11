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
using System.Windows.Media.Imaging;
using TreeGPDesigner.MVVM.Model;

namespace TreeGPDesigner
{
    public sealed partial class AppInfoSingleton
    {
        private static AppInfoSingleton instance = null;

        //Theme Brushes.
        public static Brush LightBackground = (Brush)new BrushConverter().ConvertFrom("#FFFFFF");
        public static Brush DarkBackground = (Brush)new BrushConverter().ConvertFrom("#121212");
        public static Brush LightPanel1 = (Brush)new BrushConverter().ConvertFrom("#f2f3f5");
        public static Brush DarkPanel1 = (Brush)new BrushConverter().ConvertFrom("#2e2e2e");
        public static Brush LightPanel2 = (Brush)new BrushConverter().ConvertFrom("#e3e5e8");
        public static Brush DarkPanel2 = (Brush)new BrushConverter().ConvertFrom("#1b1b1b");
        public static Brush LightNormalButton = (Brush)new BrushConverter().ConvertFrom("#a5e8b4");
        public static Brush DarkNormalButton = (Brush)new BrushConverter().ConvertFrom("#044343");
        public static Brush LightNavButton = Brushes.LightGray;
        public static Brush DarkNavButton = Brushes.Gray;

        public static LinearGradientBrush RainbowBrush = new();
        public static LinearGradientBrush LightRainbowBrush = new();

        //Node Brushes.
        public static Brush[]? BrushSet1 = { Brushes.Red, 
                                            Brushes.Pink, 
                                            Brushes.Blue, 
                                            Brushes.LightBlue };

        public static Brush[]? BrushSet2 = { (Brush)new BrushConverter().ConvertFrom("#E66100"), 
                                            Brushes.Orange,
                                            (Brush)new BrushConverter().ConvertFrom("#5D3A9B"), 
                                            Brushes.MediumPurple };

        public static Brush[]? BrushSet3 = { Brushes.Orange, 
                                            Brushes.Yellow, 
                                            Brushes.Blue, 
                                            Brushes.Magenta };

        public static Brush[]? BrushSet4 = { Brushes.DarkGreen,
                                            Brushes.LightGreen,
                                            Brushes.DarkCyan,
                                            Brushes.Turquoise };

        public static Brush[]? BrushSet5 = { Brushes.Black, Brushes.White, Brushes.Black, Brushes.White };

        public static Brush[]? BrushSet6 = { RainbowBrush,
                                            new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Images/sunflowers.jpg"))),
                                            RainbowBrush,
                                            new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Images/the-bedroom-at-arles.jpg")))};

        //Node Brushes Dark.
        public static Brush[]? BrushSet1Dark = { Brushes.DarkRed,
                                                Brushes.DeepPink,
                                                Brushes.DarkBlue,
                                                Brushes.Blue };

        public static Brush[]? BrushSet2Dark = { Brushes.Red,
                                                Brushes.DarkOrange,
                                                Brushes.Purple,
                                                Brushes.MediumPurple };

        public static Brush[]? BrushSet3Dark = { Brushes.Orange,
                                                Brushes.YellowGreen,
                                                Brushes.DarkBlue,
                                                Brushes.DarkMagenta };

        public static Brush[]? BrushSet4Dark = { Brushes.DarkOliveGreen,
                                                Brushes.DarkSeaGreen,
                                                Brushes.Aquamarine,
                                                Brushes.DarkCyan };

        public static Brush[]? BrushSet5Dark = { Brushes.White, Brushes.Black, Brushes.White, Brushes.Black  };

        public static Brush[]? BrushSet6Dark = { LightRainbowBrush,
                                                 new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Images/starry-night.jpg"))),
                                                 LightRainbowBrush,
                                                 new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Images/the-starry-night-over-the-rhone.jpg")))};

        private AppInfoSingleton()
        {
            RainbowBrush.StartPoint = new Point(0, 0);
            RainbowBrush.EndPoint = new Point(1, 1);
            RainbowBrush.GradientStops.Add(new GradientStop(Colors.Red, 0));
            RainbowBrush.GradientStops.Add(new GradientStop(Colors.Orange, 0.2));
            RainbowBrush.GradientStops.Add(new GradientStop(Colors.Yellow, 0.4));
            RainbowBrush.GradientStops.Add(new GradientStop(Colors.Green, 0.6));
            RainbowBrush.GradientStops.Add(new GradientStop(Colors.Blue, 0.75));
            RainbowBrush.GradientStops.Add(new GradientStop(Colors.Indigo, 0.875));
            RainbowBrush.GradientStops.Add(new GradientStop(Colors.Violet, 1));

            LightRainbowBrush.StartPoint = new Point(0, 0);
            LightRainbowBrush.EndPoint = new Point(1, 1);
            LightRainbowBrush.GradientStops.Add(new GradientStop(Colors.Pink, 0));
            LightRainbowBrush.GradientStops.Add(new GradientStop(Colors.OrangeRed, 0.2));
            LightRainbowBrush.GradientStops.Add(new GradientStop(Colors.Yellow, 0.4));
            LightRainbowBrush.GradientStops.Add(new GradientStop(Colors.LightGreen, 0.6));
            LightRainbowBrush.GradientStops.Add(new GradientStop(Colors.LightBlue, 0.75));
            LightRainbowBrush.GradientStops.Add(new GradientStop(Colors.PeachPuff, 0.875));
            LightRainbowBrush.GradientStops.Add(new GradientStop(Colors.Purple, 1));
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

        //Current GP Template
        public event Action CurrentTemplateChanged;
        private TreeGP? currentTemplate;

        public TreeGP? CurrentTemplate
        {
            get => currentTemplate;
            set
            {
                currentTemplate = value;
                OnCurrentTemplateChanged();
            }
        }

        private void OnCurrentTemplateChanged()
        {
            CurrentTemplateChanged.Invoke();
        }

        //Main Screen DisplayTree
        public event Action MainDisplayTreeChanged;
        private ObservableCollection<NodePlot>? mainDisplayTree;

        public ObservableCollection<NodePlot>? MainDisplayTree
        {
            get => mainDisplayTree;
            set
            {
                mainDisplayTree = value;
                OnMainDisplayTreeChanged();
            }
        }

        private void OnMainDisplayTreeChanged()
        {
            MainDisplayTreeChanged.Invoke();
        }

       /* //Main Screen Canvas Width
        public event Action MainCanvasWidthChanged;
        private float? mainCanvasWidth;

        public float? MainCanvasWidth
        {
            get => mainCanvasWidth;
            set
            {
                mainCanvasWidth = value;
                OnMainCanvasWidthChanged();
            }
        }

        private void OnMainCanvasWidthChanged()
        {
            MainCanvasWidthChanged.Invoke();
        }

        //Main Screen Canvas Height
        public event Action MainCanvasHeightChanged;
        private float? mainCanvasHeight;

        public float? MainCanvasHeight
        {
            get => mainCanvasHeight;
            set
            {
                mainCanvasHeight = value;
                OnMainCanvasHeightChanged();
            }
        }

        private void OnMainCanvasHeightChanged()
        {
            MainCanvasHeightChanged.Invoke();
        }*/

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

        //Current Panel2 Colour
        public event Action CurrentPanel2ColorChanged;
        private Brush? currentPanel2Color;

        public Brush? CurrentPanel2Color
        {
            get => currentPanel2Color;
            set
            {
                currentPanel2Color = value;
                OnCurrentPanel2ColorChanged();
            }
        }

        private void OnCurrentPanel2ColorChanged()
        {
            CurrentPanel2ColorChanged?.Invoke();
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

        //Current brush set.
        public event Action CurrentBrushSetChanged;
        private Brush[]? currentBrushSet;

        public Brush[]? CurrentBrushSet
        {
            get => currentBrushSet;
            set
            {
                currentBrushSet = value;
                OnCurrentBrushSetChanged();
            }
        }

        private void OnCurrentBrushSetChanged()
        {
            CurrentBrushSetChanged?.Invoke();
        }

        //Current brush set 1.
        public event Action CurrentBrushSet1Changed;
        private Brush[]? currentBrushSet1;

        public Brush[]? CurrentBrushSet1
        {
            get => currentBrushSet1;
            set
            {
                currentBrushSet1 = value;
                OnCurrentBrushSet1Changed();
            }
        }

        private void OnCurrentBrushSet1Changed()
        {
            CurrentBrushSet1Changed?.Invoke();
        }

        //Current brush set 2.
        public event Action CurrentBrushSet2Changed;
        private Brush[]? currentBrushSet2;

        public Brush[]? CurrentBrushSet2
        {
            get => currentBrushSet2;
            set
            {
                currentBrushSet2 = value;
                OnCurrentBrushSet2Changed();
            }
        }

        private void OnCurrentBrushSet2Changed()
        {
            CurrentBrushSet2Changed?.Invoke();
        }

        //Current brush set 3.
        public event Action CurrentBrushSet3Changed;
        private Brush[]? currentBrushSet3;

        public Brush[]? CurrentBrushSet3
        {
            get => currentBrushSet3;
            set
            {
                currentBrushSet3 = value;
                OnCurrentBrushSet3Changed();
            }
        }

        private void OnCurrentBrushSet3Changed()
        {
            CurrentBrushSet3Changed?.Invoke();
        }

        //Current brush set 4.
        public event Action CurrentBrushSet4Changed;
        private Brush[]? currentBrushSet4;

        public Brush[]? CurrentBrushSet4
        {
            get => currentBrushSet4;
            set
            {
                currentBrushSet4 = value;
                OnCurrentBrushSet4Changed();
            }
        }

        private void OnCurrentBrushSet4Changed()
        {
            CurrentBrushSet4Changed?.Invoke();
        }

        //Current brush set 5.
        public event Action CurrentBrushSet5Changed;
        private Brush[]? currentBrushSet5;

        public Brush[]? CurrentBrushSet5
        {
            get => currentBrushSet5;
            set
            {
                currentBrushSet5 = value;
                OnCurrentBrushSet5Changed();
            }
        }

        private void OnCurrentBrushSet5Changed()
        {
            CurrentBrushSet5Changed?.Invoke();
        }

        //Current brush set 6.
        public event Action CurrentBrushSet6Changed;
        private Brush[]? currentBrushSet6;

        public Brush[]? CurrentBrushSet6
        {
            get => currentBrushSet6;
            set
            {
                currentBrushSet6 = value;
                OnCurrentBrushSet6Changed();
            }
        }

        private void OnCurrentBrushSet6Changed()
        {
            CurrentBrushSet6Changed?.Invoke();
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


        //Current zoom icon
        public event Action CurrentZoomIconChanged;
        private ImageSource? currentZoomIcon;

        public ImageSource? CurrentZoomIcon
        {
            get => currentZoomIcon;
            set
            {
                currentZoomIcon = value;
                OnCurrentZoomIconChanged();
            }
        }

        private void OnCurrentZoomIconChanged()
        {
            CurrentZoomIconChanged?.Invoke();
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
        public static ObservableCollection<NodePlot> GetTreePlot(ObservableCollection<NodePlot> treePlot, Node node, Brush[] brushSet)
        {
            Brush symbolColour;

            if (Properties.Settings.Default.SettingsMode)
            {
                symbolColour = Brushes.Black;
            }
            else
            {
                symbolColour = Brushes.White;
            }

            if (node.GetType() == typeof(FunctionNode))
            {
                if (node.Parent == null)
                {
                    NodePlot nodePlot = new NodePlot(node.XPos * 100, node.YPos * 100, 50, 50, brushSet[1], brushSet[0], new CornerRadius(50), node.Symbol, 0, 0, 0, 0, symbolColour);
                    treePlot.Add(nodePlot);
                }
                else
                {
                    NodePlot nodePlot = new NodePlot(node.XPos * 100, node.YPos * 100, 50, 50, brushSet[1], brushSet[0], new CornerRadius(50), node.Symbol,
                        (node.XPos * 100) + 25, (node.YPos * 100) + 25, (node.Parent.XPos * 100) + 25, (node.Parent.YPos * 100) + 25, symbolColour);
                    treePlot.Add(nodePlot);
                }
            }
            else
            {
                if (node.Parent == null)
                {
                    NodePlot nodePlot = new NodePlot(node.XPos * 100, node.YPos * 100, 50, 50, brushSet[3], brushSet[2], new CornerRadius(0), node.Symbol, 0, 0, 0, 0, symbolColour);
                    treePlot.Add(nodePlot);
                }
                else
                {
                    NodePlot nodePlot = new NodePlot(node.XPos * 100, node.YPos * 100, 50, 50, brushSet[3], brushSet[2], new CornerRadius(0), node.Symbol,
                        (node.XPos * 100) + 25, (node.YPos * 100) + 25, (node.Parent.XPos * 100) + 25, (node.Parent.YPos * 100) + 25, symbolColour);
                    treePlot.Add(nodePlot);
                }
            }

            foreach (Node childNode in node.ChildNodes)
            {
                GetTreePlot(treePlot, childNode, brushSet);
            }

            return treePlot;
        }
    }
}
