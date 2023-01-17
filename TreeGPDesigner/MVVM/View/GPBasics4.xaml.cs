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
using System.Windows.Navigation;
using System.Windows.Shapes;
using TreeGPDesigner.MVVM.ViewModel;

namespace TreeGPDesigner.MVVM.View
{
    /// <summary>
    /// Interaction logic for GPBasics4.xaml
    /// </summary>
    public partial class GPBasics4 : UserControl
    {
        public GPBasics4()
        {
            InitializeComponent();
            DataContext = new GPBasics4ViewModel();

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
            };

            treeBorder.MouseLeftButtonUp += (sender, e) =>
            {
                isDragged = false;
            };

            treeBorder.MouseMove += (sender, e) =>
            {
                if (isDragged == false)
                {
                    return;
                }

                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    Point mousePosition = e.GetPosition(this);
                    var matrix = matrixTransform.Matrix;

                    matrix.Translate(mousePosition.X - clickPosition.X, mousePosition.Y - clickPosition.Y);
                    matrixTransform.Matrix = matrix;
                    clickPosition = mousePosition;
                }
            };
        }
    }
}
