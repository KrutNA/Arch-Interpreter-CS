using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Architecture.Interpreter
{
    class Interpreter
    {
        private const int endOfRegs = 32;
        private const int errorNum = -1;

        private bool breakPoint = false;

        public bool isError;
        private int curLine;

        public Interpreter() { }

        public void DoInterpretation(ref byte[] ram)
        {
            int endOfBinary = ram.Length;
            var alternatives = new Alternatives();
            curLine = 1;
            isError = false;

            for (int i = endOfRegs; i < endOfBinary; i++)
            {
                i = Interpret(ref ram, endOfBinary, i, ref alternatives);
                if (breakPoint || i == errorNum)
                {
                    isError = true;
                    break;
                }
                curLine++;
            }
        }

        private int Interpret(ref byte[] ram, int endOfBinary, int curByte, ref Alternatives alternatives)
        {
            byte command = ram[curByte];
            byte[] instrArray;
            if (command == CommandCodes.EXT || command == CommandCodes.BRP)
                return -1;

            byte mod = GetMOD(ref curByte, command, ref ram);
            if (mod == 0)
                return curByte;

            instrArray = alternatives.ReadAlternative(mod, ref curByte, ref ram, curLine);

            var modInstructions = new MODInstructions();
            if (modInstructions.Execute(instrArray, command, mod, ref ram))
                return -1;

            return curByte;
        }

        private byte GetMOD(ref int curByte, byte command, ref byte[] ram)
        {
            switch (command) {
                case CommandCodes.NOP:
                    return 0;
                case CommandCodes.MOV:

                    return 0;
                case CommandCodes.JMP:

                    return 0;
                case CommandCodes.JE:

                    return 0;
                case CommandCodes.JNE:

                    return 0;
                case CommandCodes.ETR:
                    Console.WriteLine();
                    return 0;
                case CommandCodes.IN:

                    return 0;
                case CommandCodes.OUT:

                    return 0;
                case CommandCodes.ALLOC:

                    return 0;
                case CommandCodes.ALLOCr:

                    return 0;
                case CommandCodes.INC:

                    return 0;
                case CommandCodes.DEC:

                    return 0;
                default:
                    return ram[++curByte];
            }
        }
    }
}
