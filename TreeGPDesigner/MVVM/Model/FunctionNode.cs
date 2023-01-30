using System;

namespace TreeGPDesigner.MVVM.Model
{
    //Funtion node class which inherits from node
    public class FunctionNode : Node
    {
        //Private function variable
        private Func<double[], double> function = null;

        //Basic constructor
        public FunctionNode(string symbol, int noOperands, Func<double[], double> function) : base(symbol, noOperands)
        {
            this.function = function;
        }

        //Node Description constructor
        public FunctionNode(string symbol, int noOperands, string nodeDescription, bool isSelected, Func<double[], double> function) : base(symbol, noOperands, nodeDescription, isSelected)
        {
            this.function = function;
        }

        //Constructor used in copynode functions which copies all variables
        public FunctionNode(string symbol, bool root, double[] data, int noOperands, float fitness, bool notFailedYet, string nodeDescription,
            Func<double[], double> function) : base(symbol, root, data, noOperands, fitness, notFailedYet, nodeDescription)
        {
            this.function = function;
        }

        //Getter and setter for function variable
        public Func<double[], double> Function { get => function; set => function = value; }

        //Eval function which ovverides node eval()
        public override double Eval()
        {
            double[] operandNodes = new double[ChildNodes.Count];
            for (int i = 0; i < ChildNodes.Count; i++)
            {
                operandNodes[i] = ChildNodes[i].Eval();
            }

            return function(operandNodes);
        }
    }
}
