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
        private bool booleanFunction;

        public FunctionNode(string symbol, int noOperands, Func<double[], double> function, bool booleanFunction) : base(symbol, noOperands)
        {
            this.function = function;
            this.booleanFunction = booleanFunction;
        }

        //node description constructor
        public FunctionNode(string symbol, int noOperands, string nodeDescription, bool isSelected, Func<double[], double> function, bool booleanFunction) : base(symbol, noOperands, nodeDescription, isSelected)
        {
            this.function = function;
            this.booleanFunction = booleanFunction;
        }

        public FunctionNode(string symbol, bool root, double[] data, int noOperands, float fitness, bool notFailedYet,
            Func<double[], double> function, bool booleanFunction) : base(symbol, root, data, noOperands, fitness, notFailedYet)
        {
            this.function = function;
            this.booleanFunction = booleanFunction;
        }

        public Func<double[], double> Function { get => function; set => function = value; }
        public bool BooleanFunction { get => booleanFunction; set => booleanFunction = value; }

        public override double Eval()
        {
            double[] operandNodes = new double[ChildNodes.Count];
            for (int i = 0; i < ChildNodes.Count; i++)
            {
                operandNodes[i] = ChildNodes[i].Eval();
            }

            return function(operandNodes);

            //Think this is right. Might not be!!! Update: Probably won't use this just changed boolean function non-root nodes.
            /*if (booleanFunction && !Root)
            {
                if (function(operandNodes) == 1)
                {
                    return ChildNodes[0].Eval();
                }
                else
                {
                    return ChildNodes[1].Eval();
                }
            }
            else
            {
                return function(operandNodes);
            }*/
        }
    }
}
