using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeGPDesigner.MVVM.Model
{
    public class FunctionNode : Node
    {
        private Func<double[], double> function = null;

        public FunctionNode(string symbol, int noOperands, Func<double[], double> function) : base(symbol, noOperands)
        {
            this.function = function;
        }

        //node description constructor
        public FunctionNode(string symbol, int noOperands, string nodeDescription, bool isSelected, Func<double[], double> function) : base(symbol, noOperands, nodeDescription, isSelected)
        {
            this.function = function;
        }

        public FunctionNode(string symbol, bool root, double[] data, int noOperands, float fitness, bool notFailedYet,
            Func<double[], double> function) : base(symbol, root, data, noOperands, fitness, notFailedYet)
        {
            this.function = function;
        }

        public Func<double[], double> Function { get => function; set => function = value; }

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
