using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Input;

namespace TreeGPDesigner.MVVM.View
{
    //Code behind for Add Custom Function Node View
    public partial class AddCustomFunctionNode : UserControl
    {
        //Constructor
        public AddCustomFunctionNode()
        {
            InitializeComponent();
        }

        //Code preventing from inputing anything but a number in a text box.
        private static readonly Regex _regex = new Regex("[^0-9]+");
        private static bool IsTextAllowed(string text)
        {
            return !_regex.IsMatch(text);
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }
    }
}
