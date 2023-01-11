using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeGPDesigner.MVVM.Model
{
    public abstract class Node
    {
        private static int idCount = 0;
        private int id = idCount++;
        private string symbol;
        private List<Node> childNodes = new List<Node>();
        private bool root = false;
        private int[] data;
        private int noOperands = 0;
        private float xPos;
        private int yPos;
        private float width;
        private int height;
        private float mod;
        private Node parent = null;
        private float fitness = 0;
        private bool notFailedYet = true;
        private string nodeDescription;
        private bool isSelected;

        public Node(string symbol, int noOperands)
        {
            this.symbol = symbol;
            this.noOperands = noOperands;
        }

        public Node(string symbol, int noOperands, bool root)
        {
            this.symbol = symbol;
            this.noOperands = noOperands;
            this.root = root;
        }

        public Node(string symbol, int noOperands, string nodeDescription, bool isSelected)
        {
            this.symbol = symbol;
            this.noOperands = noOperands;
            this.nodeDescription = nodeDescription;
            IsSelected = isSelected;
        }

        public Node(string symbol, bool root, int[] data, int noOperands, float fitness, bool notFailedYet)
        {
            Symbol = symbol;
            Root = root;
            Data = data;
            NoOperands = noOperands;
            Fitness = fitness;
            NotFailedYet = notFailedYet;
        }

        public abstract int Eval();

        public void SetDataAll(int[] data)
        {
            Data = data;

            if (ChildNodes.Count > 0)
            {
                foreach (Node node in ChildNodes)
                {
                    node.SetDataAll(data);
                }
            }
        }

        public void SetParents()
        {
            if (ChildNodes.Count > 0)
            {
                foreach (Node child in ChildNodes)
                {
                    child.Parent = this;
                    child.SetParents();
                }
            }
        }

        public void PrintAllNodes()
        {
            Console.WriteLine($"Node = {Symbol}, id = {Id}, fitness = {fitness}");

            if (ChildNodes.Count > 0)
            {
                foreach (Node node in ChildNodes)
                {
                    node.PrintAllNodes();
                }
            }
        }

        //Functions needed for FindTreePosition class Author = Rachel Lim
        public bool IsLeftMost()
        {
            if (this.Parent == null)
                return true;

            return this.Parent.ChildNodes[0] == this;
        }

        public bool IsRightMost()
        {
            if (this.Parent == null)
                return true;

            return this.Parent.ChildNodes[this.Parent.ChildNodes.Count - 1] == this;
        }

        public Node GetPreviousSibling()
        {
            if (this.Parent == null || this.IsLeftMost())
                return null;

            return this.Parent.ChildNodes[this.Parent.ChildNodes.IndexOf(this) - 1];
        }

        public Node GetNextSibling()
        {
            if (this.Parent == null || this.IsRightMost())
                return null;

            return this.Parent.ChildNodes[this.Parent.ChildNodes.IndexOf(this) + 1];
        }

        public Node GetLeftMostChild()
        {
            if (this.ChildNodes.Count == 0)
                return null;

            return this.ChildNodes[0];
        }

        public Node GetRightMostChild()
        {
            if (this.ChildNodes.Count == 0)
                return null;

            return this.ChildNodes[ChildNodes.Count - 1];
        }

        public Node GetLeftMostSibling()
        {
            if (this.Parent == null)
                return null;

            if (this.IsLeftMost())
                return this;

            return this.Parent.ChildNodes[0];
        }

        //Getters and setters for private node variables.
        public string Symbol { get => symbol; set => symbol = value; }
        public bool Root { get => root; set => root = value; }
        public int[] Data { get => data; set => data = value; }
        public List<Node> ChildNodes { get => childNodes; set => childNodes = value; }
        public int Id { get => id; }
        public int NoOperands { get => noOperands; set => noOperands = value; }
        public int YPos { get => yPos; set => yPos = value; }
        public int Height { get => height; set => height = value; }
        public Node Parent { get => parent; set => parent = value; }
        public float Mod { get => mod; set => mod = value; }
        public float XPos { get => xPos; set => xPos = value; }
        public float Width { get => width; set => width = value; }
        public float Fitness { get => fitness; set => fitness = value; }
        public bool NotFailedYet { get => notFailedYet; set => notFailedYet = value; }
        public string NodeDescription { get => nodeDescription; set => nodeDescription = value; }
        public bool IsSelected { get => isSelected; set => isSelected = value; }
    }
}
