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
    /// Interaction logic for HomeView.xaml
    /// </summary>
    public partial class HomeView : UserControl
    {
        public HomeView()
        {
            InitializeComponent();
            DataContext = new HomeViewModel();

            ChangeRadioButtonsIsChecked(AppInfoSingleton.Instance.CurrentRadioButtonCheck);

            //Pan and zoom code.
            TransformGroup transformGroup = new TransformGroup();
            ScaleTransform scaleTransform = new ScaleTransform();
            MatrixTransform matrixTransform = new MatrixTransform();
            var clickPosition = new Point(0, 0);
            var isDragged = false;
            transformGroup.Children.Add(matrixTransform);
            transformGroup.Children.Add(scaleTransform);
            mainContainerCanvas.RenderTransform = transformGroup;

            mainBorder.MouseWheel += (sender, e) =>
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

            mainBorder.MouseLeftButtonDown += (sender, e) =>
            {
                clickPosition = e.GetPosition(this);
                isDragged = true;
                panZoomImg.Visibility = Visibility.Hidden;
            };

            mainBorder.MouseLeftButtonUp += (sender, e) =>
            {
                isDragged = false;
                panZoomImg.Visibility = Visibility.Visible;
            };

            mainBorder.MouseMove += (sender, e) =>
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

        private void DebugBtn_Click(object sender, RoutedEventArgs e)
        {
            DebugWindow debugWindow = new DebugWindow();
            debugWindow.Show();
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).Close();
        }

        public void ChangeRadioButtonsIsChecked(int? trueRadioButton)
        {
            if (trueRadioButton == 1)
            {
                RadioButton1.IsChecked = true;
                RadioButton2.IsChecked = false;
                RadioButton3.IsChecked = false;
                RadioButton4.IsChecked = false;
                RadioButton5.IsChecked = false;
                RadioButton6.IsChecked = false;
            }
            else if(trueRadioButton == 2)
            {
                RadioButton1.IsChecked = false;
                RadioButton2.IsChecked = true;
                RadioButton3.IsChecked = false;
                RadioButton4.IsChecked = false;
                RadioButton5.IsChecked = false;
                RadioButton6.IsChecked = false;
            }
            else if (trueRadioButton == 3)
            {
                RadioButton1.IsChecked = false;
                RadioButton2.IsChecked = false;
                RadioButton3.IsChecked = true;
                RadioButton4.IsChecked = false;
                RadioButton5.IsChecked = false;
                RadioButton6.IsChecked = false;
            }
            else if (trueRadioButton == 4)
            {
                RadioButton1.IsChecked = false;
                RadioButton2.IsChecked = false;
                RadioButton3.IsChecked = false;
                RadioButton4.IsChecked = true;
                RadioButton5.IsChecked = false;
                RadioButton6.IsChecked = false;
            }
            else if (trueRadioButton == 5)
            {
                RadioButton1.IsChecked = false;
                RadioButton2.IsChecked = false;
                RadioButton3.IsChecked = false;
                RadioButton4.IsChecked = false;
                RadioButton5.IsChecked = true;
                RadioButton6.IsChecked = false;
            }
            else
            {
                RadioButton1.IsChecked = false;
                RadioButton2.IsChecked = false;
                RadioButton3.IsChecked = false;
                RadioButton4.IsChecked = false;
                RadioButton5.IsChecked = false;
                RadioButton6.IsChecked = true;
            }
        }
    }
}
