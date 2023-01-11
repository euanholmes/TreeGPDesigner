using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeGPDesigner.MVVM.Model
{
    public class FunctionNode : Node
    {
        private Func<int[], int> function = null;
        private bool booleanFunction;

        public FunctionNode(string symbol, int noOperands, Func<int[], int> function, bool booleanFunction) : base(symbol, noOperands)
        {
            this.function = function;
            this.booleanFunction = booleanFunction;
        }

        //node description constructor
        public FunctionNode(string symbol, int noOperands, string nodeDescription, bool isSelected, Func<int[], int> function, bool booleanFunction) : base(symbol, noOperands, nodeDescription, isSelected)
        {
            this.function = function;
            this.booleanFunction = booleanFunction;
        }

        public FunctionNode(string symbol, bool root, int[] data, int noOperands, float fitness, bool notFailedYet,
            Func<int[], int> function, bool booleanFunction) : base(symbol, root, data, noOperands, fitness, notFailedYet)
        {
            this.function = function;
            this.booleanFunction = booleanFunction;
        }

        public Func<int[], int> Function { get => function; set => function = value; }
        public bool BooleanFunction { get => booleanFunction; set => booleanFunction = value; }

        public override int Eval()
        {
            //Think this is right. Might not be!!!
            /*if (booleanFunction && !Root)
            {
                if (function(ChildNodes[0].eval(), ChildNodes[1].eval()) == 1)
                {
                    return ChildNodes[0].eval();
                }
                else
                {
                    return ChildNodes[1].eval();
                }
            }*/
            /*else
            {*/
            int[] operandNodes = new int[ChildNodes.Count];
            for (int i = 0; i < ChildNodes.Count; i++)
            {
                operandNodes[i] = ChildNodes[i].Eval();
            }

            return function(operandNodes);
            /*}*/
        }
    }
}
