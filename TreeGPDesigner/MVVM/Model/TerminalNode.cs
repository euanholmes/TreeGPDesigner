using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeGPDesigner.MVVM.Model
{
    public class TerminalNode : Node
    {
        private double value;
        private bool dataNeeded;
        private bool functionNeeded = false;
        private Func<double[], double> terminalFunction = null;
        private int[] terminalFunctionData = null;

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
            bool functionNeeded, Func<double[], double> terminalFunction, int[] terminalFunctionData) : base(symbol, noOperands, nodeDescription, isSelected)
        {
            this.value = value;
            this.dataNeeded = dataNeeded;
            this.functionNeeded = functionNeeded;
            this.terminalFunction = terminalFunction;
            this.terminalFunctionData = terminalFunctionData;
        }

        public TerminalNode(string symbol, bool root, double[] data, int noOperands, float fitness, bool notFailedYet,
            double value, bool dataNeeded, bool functionNeeded, Func<double[], double> terminalFunction, 
            int[] terminalFunctionData) : base(symbol, root, data, noOperands, fitness, notFailedYet)
        {
            this.value = value;
            this.dataNeeded = dataNeeded;
            this.functionNeeded = functionNeeded;
            this.terminalFunction = terminalFunction;
            this.terminalFunctionData = terminalFunctionData;
        }

        public double Value { get => value; set => this.value = value; }
        public bool DataNeeded { get => dataNeeded; set => dataNeeded = value; }
        public bool FunctionNeeded { get => functionNeeded; set => functionNeeded = value; }
        public Func<double[], double> TerminalFunction { get => terminalFunction; set => terminalFunction = value; }
        public int[] TerminalFunctionData { get => terminalFunctionData; set => terminalFunctionData = value; }

        public override double Eval()
        {
            if (functionNeeded)
            {
                double[] operands = new double[TerminalFunctionData.Length];

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
                    return Data[(int)value];
                }
                else
                {
                    return value;
                }
            }
        }
    }
}
