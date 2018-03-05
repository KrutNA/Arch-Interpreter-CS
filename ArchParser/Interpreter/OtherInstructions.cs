using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Architecture.Interpreter
{
    class OtherInstructions
    {
        public OtherInstructions() { }

        public Execute(byte[] instArray, ref int curByte, ref byte[] ram, byte command);
    }
}
