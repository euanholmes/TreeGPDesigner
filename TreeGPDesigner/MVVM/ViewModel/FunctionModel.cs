using System.Windows.Media;

namespace TreeGPDesigner.MVVM.ViewModel
{
    //Class used to display wrapper and fitness function UI information
    public class FunctionModel
    {
        //Function model private variables
        private string name;
        private string information;
        private ImageSource image;

        //Constructor
        public FunctionModel(string name, string information, ImageSource image)
        {
            this.name = name;
            this.information = information;
            this.image = image;
        }

        //Getters and setters for private variables
        public string Name { get => name; set => name = value; }
        public string Information { get => information; set => information = value; }
        public ImageSource Image { get => image; set => image = value; }
    }
}
