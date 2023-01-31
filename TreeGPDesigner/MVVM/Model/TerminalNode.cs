using System;

namespace TreeGPDesigner.MVVM.Model
{
    //Terminal node class which inherits from node
    public class TerminalNode : Node
    {
        //Private terminal node variables
        private double value;
        private bool dataNeeded;
        private bool functionNeeded = false;
        private Func<object[], double> terminalFunction = null;
        private int[] terminalFunctionData = null;

        //Terminal node constructors
        public TerminalNode(string symbol, int noOperands, double value, bool dataNeeded) : base(symbol, noOperands)
        {
            this.value = value;
            this.dataNeeded = dataNeeded;
        }

        public TerminalNode(string symbol, int noOperands, string nodeDescription, bool isSelected, double value, bool dataNeeded) : base(symbol, noOperands, nodeDescription, isSelected)
        {
            this.value = value;
            this.dataNeeded = dataNeeded;
        }

        public TerminalNode(string symbol, int noOperands, string nodeDescription, bool isSelected, double value, bool dataNeeded,
            bool functionNeeded, Func<object[], double> terminalFunction, int[] terminalFunctionData) : base(symbol, noOperands, nodeDescription, isSelected)
        {
            this.value = value;
            this.dataNeeded = dataNeeded;
            this.functionNeeded = functionNeeded;
            this.terminalFunction = terminalFunction;
            this.terminalFunctionData = terminalFunctionData;
        }

        //Constructor used in copynode functions which copies all variables
        public TerminalNode(string symbol, bool root, object[] data, int noOperands, float fitness, bool notFailedYet, string nodeDescription,
            double value, bool dataNeeded, bool functionNeeded, Func<object[], double> terminalFunction, 
            int[] terminalFunctionData) : base(symbol, root, data, noOperands, fitness, notFailedYet, nodeDescription)
        {
            this.value = value;
            this.dataNeeded = dataNeeded;
            this.functionNeeded = functionNeeded;
            this.terminalFunction = terminalFunction;
            this.terminalFunctionData = terminalFunctionData;
        }

        //Getters and setters for private variables
        public double Value { get => value; set => this.value = value; }
        public bool DataNeeded { get => dataNeeded; set => dataNeeded = value; }
        public bool FunctionNeeded { get => functionNeeded; set => functionNeeded = value; }
        public Func<object[], double> TerminalFunction { get => terminalFunction; set => terminalFunction = value; }
        public int[] TerminalFunctionData { get => terminalFunctionData; set => terminalFunctionData = value; }

        //Eval function which ovverides node eval()
        public override double Eval()
        {
            if (functionNeeded)
            {
                object[] operands = new object[TerminalFunctionData.Length];

                for (int i = 0; i < TerminalFunctionData.Length; i++)
                {
                    operands[i] = Data[TerminalFunctionData[i]];
                }

                return TerminalFunction(operands);
            }
            else
            {
                if (dataNeeded)
                {
                    return Convert.ToDouble(Data[(int)value]);
                }
                else
                {
                    return value;
                }
            }
        }
    }
}
