using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace TreeGPDesigner.MVVM.View
{
    //Code behind for GP Basics 6 View
    public partial class GPBasics6 : UserControl
    {
        //Constructor
        public GPBasics6()
        {
            InitializeComponent();

            //Pan and zoom code.
            TransformGroup transformGroup = new TransformGroup();
            ScaleTransform scaleTransform = new ScaleTransform();
            MatrixTransform matrixTransform = new MatrixTransform();
            var clickPosition = new Point(0, 0);
            var isDragged = false;
            transformGroup.Children.Add(matrixTransform);
            transformGroup.Children.Add(scaleTransform);
            treeCanvas.RenderTransform = transformGroup;

            treeBorder.MouseWheel += (sender, e) =>
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

            treeBorder.MouseLeftButtonDown += (sender, e) =>
            {
                clickPosition = e.GetPosition(this);
                isDragged = true;
                panZoomImg.Visibility = Visibility.Hidden;
            };

            treeBorder.MouseLeftButtonUp += (sender, e) =>
            {
                isDragged = false;
                panZoomImg.Visibility = Visibility.Visible;
            };

            treeBorder.MouseMove += (sender, e) =>
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
