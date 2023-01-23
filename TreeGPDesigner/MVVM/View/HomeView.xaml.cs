using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;


namespace TreeGPDesigner.MVVM.View
{
    //Code behind for Home View
    public partial class HomeView : UserControl
    {
        //Constructor
        public HomeView()
        {
            InitializeComponent();
            ChangeRadioButtonsIsChecked(Properties.Settings.Default.SettingsRadioButton);

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

        //Home View Functions
        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).Close();
        }

        public void ChangeRadioButtonsIsChecked(int? trueRadioButton)
        {
            if (trueRadioButton == 1)
            {
                RadioButton1.IsChecked = true;
            }
            else if(trueRadioButton == 2)
            {
                RadioButton2.IsChecked = true;
            }
            else if (trueRadioButton == 3)
            {
                RadioButton3.IsChecked = true;
            }
            else if (trueRadioButton == 4)
            {
                RadioButton4.IsChecked = true;
            }
            else if (trueRadioButton == 5)
            {
                RadioButton5.IsChecked = true;
            }
            else
            {
                RadioButton6.IsChecked = true;
            }
        }
    }
}
