using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TreeGPDesigner.MVVM.ViewModel;

namespace TreeGPDesigner
{
    /// <summary>
    /// Interaction logic for DebugWindow.xaml
    /// </summary>
    public partial class DebugWindow : Window
    {
        public DebugWindow()
        {
            InitializeComponent();
            DataContext = new DebugWindowViewModel();


            //Pan and zoom code.
            TransformGroup transformGroup = new TransformGroup();
            ScaleTransform scaleTransform = new ScaleTransform();
            MatrixTransform matrixTransform = new MatrixTransform();
            var clickPosition = new Point(0, 0);
            var isDragged = false;
            transformGroup.Children.Add(matrixTransform);
            transformGroup.Children.Add(scaleTransform);
            testContainerCanvas.RenderTransform = transformGroup;

            testBorder.MouseWheel += (sender, e) =>
            {
                if (e.Delta > 0)
                {
                    scaleTransform.ScaleX *= 1.1;
                    scaleTransform.ScaleY *= 1.1;
                }
                else
                {
                    scaleTransform.ScaleX /= 1.1;
                    scaleTransform.ScaleY /= 1.1;
                }
            };

            testBorder.MouseLeftButtonDown += (sender, e) =>
            {
                clickPosition = e.GetPosition(this);
                isDragged = true;
                panZoomImg.Visibility = Visibility.Hidden;
            };

            testBorder.MouseLeftButtonUp += (sender, e) =>
            {
                isDragged = false;
                panZoomImg.Visibility = Visibility.Visible;
            };

            testBorder.MouseMove += (sender, e) =>
            {
                if (isDragged == false)
                {
                    panZoomImg.Visibility = Visibility.Visible;
                    return;
                }

                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    Point mousePosition = e.GetPosition(this);
                    var matrix = matrixTransform.Matrix;

                    matrix.Translate(mousePosition.X - clickPosition.X, mousePosition.Y - clickPosition.Y);
                    matrixTransform.Matrix = matrix;
                    clickPosition = mousePosition;
                    panZoomImg.Visibility = Visibility.Hidden;
                }
            };
        }
    }
}
