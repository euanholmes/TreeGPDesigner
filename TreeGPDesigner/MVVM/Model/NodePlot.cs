using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace TreeGPDesigner.MVVM.Model
{
    public class NodePlot
    {
        private float x;
        private float y;
        private float width;
        private float height;
        private Brush background;
        private Brush border;
        private CornerRadius cornerRadius;
        private string symbol;
        private float linex1;
        private float liney1;
        private float linex2;
        private float liney2;

        public NodePlot(float x, float y, float width, float height, Brush background, Brush border, CornerRadius cornerRadius, string symbol, float linex1, float liney1, float linex2, float liney2)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
            this.background = background;
            this.border = border;
            this.cornerRadius = cornerRadius;
            this.symbol = symbol;
            this.linex1 = linex1;
            this.liney1 = liney1;
            this.linex2 = linex2;
            this.liney2 = liney2;
        }

        public float X { get => x; set => x = value; }
        public float Y { get => y; set => y = value; }
        public float Width { get => width; set => width = value; }
        public float Height { get => height; set => height = value; }
        public Brush Background { get => background; set => background = value; }
        public Brush Border { get => border; set => border = value; }
        public CornerRadius CornerRadius { get => cornerRadius; set => cornerRadius = value; }
        public string Symbol { get => symbol; set => symbol = value; }
        public float Linex1 { get => linex1; set => linex1 = value; }
        public float Liney1 { get => liney1; set => liney1 = value; }
        public float Linex2 { get => linex2; set => linex2 = value; }
        public float Liney2 { get => liney2; set => liney2 = value; }
    }
}
