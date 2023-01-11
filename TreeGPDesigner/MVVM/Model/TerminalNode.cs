using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeGPDesigner.MVVM.Model
{
    public class TerminalNode : Node
    {
        private int value;
        private bool dataNeeded;

        public TerminalNode(string symbol, int noOperands, int value, bool dataNeeded) : base(symbol, noOperands)
        {
            this.value = value;
            this.dataNeeded = dataNeeded;
        }

        public TerminalNode(string symbol, int noOperands, string nodeDescription, bool isSelected, int value, bool dataNeeded) : base(symbol, noOperands, nodeDescription, isSelected)
        {
            this.value = value;
            this.dataNeeded = dataNeeded;
        }

        public TerminalNode(string symbol, bool root, int[] data, int noOperands, float fitness, bool notFailedYet,
            int value, bool dataNeeded) : base(symbol, root, data, noOperands, fitness, notFailedYet)
        {
            this.value = value;
            this.dataNeeded = dataNeeded;
        }

        public int Value { get => value; set => this.value = value; }
        public bool DataNeeded { get => dataNeeded; set => dataNeeded = value; }

        public override int Eval()
        {
            if (dataNeeded)
            {
                return Data[value];
            }
            else
            {
                return value;
            }
        }
    }
}
