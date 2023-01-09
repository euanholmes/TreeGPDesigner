using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace TreeGPDesigner.MVVM.Model
{
    public class FunctionModel
    {
        private string name;
        private string information;
        private ImageSource image;

        public FunctionModel(string name, string information, ImageSource image)
        {
            this.name = name;
            this.information = information;
            this.image = image;
        }

        public string Name { get => name; set => name = value; }
        public string Information { get => information; set => information = value; }
        public ImageSource Image { get => image; set => image = value; }
    }
}
