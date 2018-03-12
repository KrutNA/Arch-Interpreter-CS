using System;

namespace Architecture.Interpreter
{
    class Interpreter
    {
        private const int endOfRegs = Alternative.regs;
        private const int errorNum = -1;

        private bool breakPoint = false;

        public bool isError;
        private int curLine;

        public Interpreter() { }

        public void DoInterpretation(ref byte[] ram)
        {
            int endOfBinary = ram.Length;
            var alternatives = new Alternative();
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

        private int Interpret(ref byte[] ram, int endOfBinary, int curByte, ref Alternative alternatives)
        {
            bool isError = false;
            byte command = ram[curByte];
            byte[] instrArray;
            if (command == CommandCode.EXT || command == CommandCode.BRP)
                return errorNum;

            byte mod = GetMOD(ref curByte, command, ref ram, ref isError);
            if (isError)
                return errorNum;

            if (mod == 0)
                return curByte;

            instrArray = alternatives.ReadAlternative( mod, ref curByte, ref ram, curLine );

            var modInstructions = new MODInstruction();
            if (modInstructions.Execute(instrArray, command, mod, ref ram))
                return errorNum;
            
            return curByte;
        }

        private byte GetMOD( ref int curByte, byte command, ref byte[] ram, ref bool isError)
        {

            var specificInstruction = new SpecificInstruction();
            switch (command) {
                case CommandCode.NOP:
                    return 0;
                case CommandCode.JMP:
                    specificInstruction.ExecuteJump( ref curByte, ref ram );
                    return 0;
                case CommandCode.JE:
                    specificInstruction.ExecuteConditionJump( ref curByte, ref ram, true );
                    return 0;
                case CommandCode.JNE:
                    specificInstruction.ExecuteConditionJump( ref curByte, ref ram, false );
                    return 0;
                case CommandCode.ETR:
                    Console.WriteLine();
                    return 0;
                case CommandCode.IN:
                    specificInstruction.ExecuteStream( ref curByte, ref ram, true, ref isError );
                    return 0;
                case CommandCode.OUT:
                    specificInstruction.ExecuteStream( ref curByte, ref ram, false, ref isError );
                    return 0;
                case CommandCode.ALLOC:
                    specificInstruction.ExecuteAllocate( ref curByte, ref ram, true, ref isError);
                    return 0;
                case CommandCode.ALLOCr:
                    specificInstruction.ExecuteAllocate( ref curByte, ref ram, false, ref isError );
                    return 0;
                case CommandCode.INC:
                    return Alternative.rmMOD2_;
                case CommandCode.DEC:
                    return Alternative.rmMOD2_;
                default:
                    return ram[++curByte];
            }
        } 
    }
}
