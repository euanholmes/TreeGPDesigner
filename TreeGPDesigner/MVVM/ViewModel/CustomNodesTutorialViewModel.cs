using CommunityToolkit.Mvvm.ComponentModel;

namespace TreeGPDesigner.MVVM.ViewModel
{
    //Viewmodel class for Custom Nodes Tutorial View
    public partial class CustomNodesTutorialViewModel : ViewModelBase
    {
        //Custom Nodes Tutorial Variables
        [ObservableProperty]
        private string customFunctionNodesInfoText = "In this application function nodes can be added to a template. In order to add function nodes its important " +
            "to understand the function node class used in the application. The function node class has properties for name and description which are simple strings. " +
            "On top of this there is a number of operands variable which is an int. As well as a Func<double[ ], double> lambda expression variable. This lambda expression " +
            "takes an array of doubles, the operands, (the size of this array being specfied by the number of operands variable) and returns a double. All these variables " +
            "are entered as strings and then parsed to their appropriate variables.\n\nAn example of an addition node would be:\nName: +\nDescription: Addition" +
            "\nNumber of operands: 2\nFunction: a => a[0] + a[1]\n\nAn example of a less than node would be:\nName: <\nDescription: Less Than\nNumber of operands: 2" +
            "\nFunction: a => a[0] < a[1] ? a[0] : a[1]";

        [ObservableProperty]
        private string customTerminalNodesInfoText = "In this application terminal nodes can be added to a template. Terminal nodes can either be static variables like " +
            "1 or a variable. Static terminal nodes are easy to add simply specify the value to be added. Variable terminal nodes require data which is exposed by the current " +
            "wrapper. For example an offline bin packing wrapper might have the data points: [0] - Current Bin Weight, [1] - Current Item, [2] - Bin Capacity. Then to add a " +
            "simple variable terminal node you'd specify that data is needed then select 0, 1 or 2. To add more complex terminal nodes a function may be needed. This function " +
            "will be a Func<double[ ], double> lambda expression. With the double[ ] being the operands for the function specified by a datapoints int array.\n\nAn example of a simple " +
            "terminal node could be:\nName: 34\nDescription: The number 34\nValue: 34 \n\nAn example of a more complex terminal node could be:\nName: FS\nDescription: " +
            "Free Space\nDatapoints: 2, 0\nFunction: a => a[0] - a[1]";

        //Constructor
        public CustomNodesTutorialViewModel()
        {

        }
    }
}
