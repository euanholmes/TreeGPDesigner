using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using TreeGPDesigner;
using TreeGPDesigner.MVVM.Model;
using TreeGPDesigner.MVVM.ViewModel;

namespace TreeGPDesignerUnitTesting
{
    [TestClass]
    public class ViewModelUnitTests
    {
        [TestMethod]
        public void VMTestGPBasics1()
        {
            //Start required viewmodels and set variables
            AppInfoSingleton.Instance.CurrentBrushSet1 = AppInfoSingleton.BrushSet1;
            AppInfoSingleton.Instance.CurrentBrushSet = AppInfoSingleton.Instance.CurrentBrushSet1;

            //Start GP Basics 1 viemodel
            GPBasics1ViewModel vm = new();

            //Set the function, terminal and root node check values
            vm.SelectedFunctionNodes = new() { true, true, true, true, true, true };
            vm.SelectedTerminalNodes = new() { true, true, true, true, true, true };
            vm.SelectedRootNodes = new() { true, true, true, true, true, true };

            //Call GenerateTree()
            vm.GenerateTree();

            //Set the function, terminal and root node check values for another case
            vm.SelectedFunctionNodes = new() { false, false, false, false, false, false };
            vm.SelectedTerminalNodes = new() { true, true, true, true, true, true };
            vm.SelectedRootNodes = new() { true, true, true, true, true, true };

            //Call GenerateTree()
            vm.GenerateTree();

            //Set the function, terminal and root node check values for another case
            vm.SelectedFunctionNodes = new() { false, false, false, false, false, false };
            vm.SelectedTerminalNodes = new() { false, false, false, false, false, false };
            vm.SelectedRootNodes = new() { true, true, true, true, true, true };

            //Call GenerateTree()
            vm.GenerateTree();

            //Set the function, terminal and root node check values for another case
            vm.SelectedFunctionNodes = new() { false, false, false, false, false, false };
            vm.SelectedTerminalNodes = new() { false, false, false, false, false, false };
            vm.SelectedRootNodes = new() { false, false, false, false, false, false };

            //Call GenerateTree()
            vm.GenerateTree();

            //If theres no crash with these cases then the test has passed
        }

        [TestMethod]
        public void VMTestAddCustomFunctionNode()
        {
            //Start required viewmodels and set variables
            MainWindowViewModel mainVM = new();
            GPR1GPTemplateMenuViewModel gptVM = new();
            BinPackingTemplate bpTemplate = new();
            AppInfoSingleton.Instance.CurrentTemplate = bpTemplate;

            //Start add custom function node viewmodel
            AddCustomFunctionNodeViewModel vm = new(false);

            //Set the variables that will be used in a CORRECT add custom function node function
            vm.SymbolText = "+";
            vm.NoOperandsText = "2";
            vm.NodeDescriptionText = "Addition";
            vm.FunctionText = "a => a[0] + a[1]";

            //Call AddCustomFunctionNode
            vm.AddCustomFunctionNode();

            //Initalise actual error message string with the error message from the viewmodel
            string actualErrorMessage = vm.ErrorMessage;

            //Set the variables that will be used in an INCORRECT add custom function node function
            vm.SymbolText = "+";
            vm.NoOperandsText = "2";
            vm.NodeDescriptionText = "Addition";
            vm.FunctionText = "ab => a + b";

            //Call AddCustomFunctionNode
            vm.AddCustomFunctionNode();

            //Add to the actual error message
            actualErrorMessage += vm.ErrorMessage;

            //Set the variables that will be used in an INCORRECT add custom function node function
            vm.SymbolText = "+";
            vm.NoOperandsText = "2";
            vm.NodeDescriptionText = "Addition";
            vm.FunctionText = "a[0] + a[1]";

            //Call AddCustomFunctionNode
            vm.AddCustomFunctionNode();

            //Add to the actual error message
            actualErrorMessage += vm.ErrorMessage;

            //Set the variables that will be used in an INCORRECT add custom function node function
            vm.SymbolText = "+";
            vm.NoOperandsText = "2";
            vm.NodeDescriptionText = "Addition";
            vm.FunctionText = "a => a[0] + a[1] + a[2]";

            //Call AddCustomFunctionNode
            vm.AddCustomFunctionNode();

            //Add to the actual error message
            actualErrorMessage += vm.ErrorMessage;

            //Set the expected Error Message, it should work then fail twice
            string expectedErrorMessage = "*Added Node*Failed to add node.*Failed to add node.*Failed to add node.";

            //Print the expected and actual error messages
            Trace.WriteLine($"Expected Error Message: {expectedErrorMessage}, Actual Error Message: {actualErrorMessage}");

            //Assert that the expected and actual error messages are the same
            Assert.AreEqual(expectedErrorMessage, actualErrorMessage);
        }

        [TestMethod]
        public void VMTestAddCustomTerminalNode()
        {
            //Start the viewmodels which initialise certain variables
            MainWindowViewModel mainVM = new();
            GPR1GPTemplateMenuViewModel gptVM = new();
            BinPackingTemplate bpTemplate = new();
            AppInfoSingleton.Instance.CurrentTemplate = bpTemplate;

            AddCustomTerminalNodeViewModel vm = new(false);

            //Set the variables that will be used in a CORRECT one terminal node function
            vm.SymbolText = "1";
            vm.NodeDescriptionText = "One";
            vm.ValueText = "1";
            vm.DataNeededIsChecked = false;
            vm.FunctionNeededIsChecked = false;

            //Call AddCustomTerminalNode
            vm.AddCustomTerminalNode();

            //Initalise actual error message string with the error message from the viewmodel
            string actualErrorMessage = vm.ErrorMessage;

            //Set the variables that will be used in an INCORRECT one terminal node function
            vm.SymbolText = "1";
            vm.NodeDescriptionText = "One";
            vm.ValueText = "one";
            vm.DataNeededIsChecked = false;
            vm.FunctionNeededIsChecked = false;

            //Call AddCustomFunctionNode
            vm.AddCustomTerminalNode();

            //Add to the actual error message
            actualErrorMessage += vm.ErrorMessage;

            //Set the variables that will be used in a CORRECT current bin weight terminal node function
            vm.SymbolText = "CBW";
            vm.NodeDescriptionText = "Current Bin Weight";
            vm.ValueText = "0";
            vm.DataNeededIsChecked = true;
            vm.FunctionNeededIsChecked = false;

            //Call AddCustomFunctionNode
            vm.AddCustomTerminalNode();

            //Add to the actual error message
            actualErrorMessage += vm.ErrorMessage;

            //Set the variables that will be used in an INCORRECT current bin weight terminal node function
            vm.SymbolText = "CBW";
            vm.NodeDescriptionText = "Current Bin Weight";
            vm.ValueText = "zero";
            vm.DataNeededIsChecked = true;
            vm.FunctionNeededIsChecked = false;

            //Call AddCustomFunctionNode
            vm.AddCustomTerminalNode();

            //Add to the actual error message
            actualErrorMessage += vm.ErrorMessage;

            //Set the variables that will be used in an INCORRECT current bin weight terminal node function
            vm.SymbolText = "CBW";
            vm.NodeDescriptionText = "Current Bin Weight";
            vm.ValueText = "34";
            vm.DataNeededIsChecked = true;
            vm.FunctionNeededIsChecked = false;

            //Call AddCustomFunctionNode
            vm.AddCustomTerminalNode();

            //Add to the actual error message
            actualErrorMessage += vm.ErrorMessage;

            //Set the variables that will be used in an INCORRECT current bin weight terminal node function
            vm.SymbolText = "CBW";
            vm.NodeDescriptionText = "Current Bin Weight";
            vm.ValueText = "-1";
            vm.DataNeededIsChecked = true;
            vm.FunctionNeededIsChecked = false;

            //Call AddCustomFunctionNode
            vm.AddCustomTerminalNode();

            //Add to the actual error message
            actualErrorMessage += vm.ErrorMessage;

            //Set the variables that will be used in a CORRECT free space terminal node function
            vm.SymbolText = "FS";
            vm.NodeDescriptionText = "Free Space";
            vm.ValueText = "2, 0";
            vm.FunctionText = "a => Convert.ToDouble(a[0]) - Convert.ToDouble(a[1])";
            vm.DataNeededIsChecked = false;
            vm.FunctionNeededIsChecked = true;

            //Call AddCustomFunctionNode
            vm.AddCustomTerminalNode();

            //Add to the actual error message
            actualErrorMessage += vm.ErrorMessage;

            //Set the variables that will be used in an INCORRECT free space terminal node function
            vm.SymbolText = "FS";
            vm.NodeDescriptionText = "Free Space";
            vm.ValueText = "2,0";
            vm.FunctionText = "a => Convert.ToDouble(a[0]) - Convert.ToDouble(a[1])";
            vm.DataNeededIsChecked = false;
            vm.FunctionNeededIsChecked = true;

            //Call AddCustomFunctionNode
            vm.AddCustomTerminalNode();

            //Add to the actual error message
            actualErrorMessage += vm.ErrorMessage;

            //Set the variables that will be used in an INCORRECT free space terminal node function
            vm.SymbolText = "FS";
            vm.NodeDescriptionText = "Free Space";
            vm.ValueText = "2,0";
            vm.FunctionText = "a => a[0] - a[1]";
            vm.DataNeededIsChecked = false;
            vm.FunctionNeededIsChecked = true;

            //Call AddCustomFunctionNode
            vm.AddCustomTerminalNode();

            //Add to the actual error message
            actualErrorMessage += vm.ErrorMessage;

            //Set the variables that will be used in an INCORRECT free space terminal node function
            vm.SymbolText = "FS";
            vm.NodeDescriptionText = "Free Space";
            vm.ValueText = "2, 0, 6";
            vm.FunctionText = "a => Convert.ToDouble(a[0]) - Convert.ToDouble(a[1])";
            vm.DataNeededIsChecked = false;
            vm.FunctionNeededIsChecked = true;

            //Call AddCustomFunctionNode
            vm.AddCustomTerminalNode();

            //Add to the actual error message
            actualErrorMessage += vm.ErrorMessage;

            //Set the variables that will be used in an INCORRECT free space terminal node function
            vm.SymbolText = "FS";
            vm.NodeDescriptionText = "Free Space";
            vm.ValueText = "2, 0";
            vm.FunctionText = "a => Convert.ToDouble(a[0]) - Convert.ToDouble(a[1]) - Convert.ToDouble(a[2])";
            vm.DataNeededIsChecked = false;
            vm.FunctionNeededIsChecked = true;

            //Call AddCustomFunctionNode
            vm.AddCustomTerminalNode();

            //Add to the actual error message
            actualErrorMessage += vm.ErrorMessage;

            AppInfoSingleton.Instance.CurrentTemplate.TerminalNodes[AppInfoSingleton.Instance.CurrentTemplate.TerminalNodes.Count - 1].Data = new object[] { 20, 10, 15, true, true };
            Trace.WriteLine(AppInfoSingleton.Instance.CurrentTemplate.TerminalNodes[AppInfoSingleton.Instance.CurrentTemplate.TerminalNodes.Count - 1].Eval());

            //Set the expected Error Message
            string expectedErrorMessage = "*Added Node*Failed to add node." +
                "*Added Node*Failed to add node.*Failed to add node.*Failed to add node." +
                "*Added Node*Failed to add node.*Failed to add node.*Failed to add node.*Failed to add node.";

            //Print the expected and actual error messages
            Trace.WriteLine($"Expected Error Message: {expectedErrorMessage}, Actual Error Message: {actualErrorMessage}");

            //Assert that the expected and actual error messages are the same
            Assert.AreEqual(expectedErrorMessage, actualErrorMessage);
        }
    }
}
