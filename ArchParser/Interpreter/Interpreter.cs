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

        int _instructionPointer;
        byte[] _ram;

        public Interpreter(byte[] ram)
        {
            this._ram = ram;
        }

        public void DoInterpretation()
        {
            int endOfBinary = _ram.Length;
            var alternatives = new Alternative();
            curLine = 1;
            isError = false;

            _instructionPointer = endOfRegs;
            //for (int i = endOfRegs; i < endOfBinary; i++)
            while (_instructionPointer < endOfBinary)
            {
                var isOk = TryInterpret(endOfBinary, ref alternatives);
                if (breakPoint || !isOk)
                {
                    isError = true;
                    break;
                }
                curLine++;
                _instructionPointer++;
            }
        }

        private bool TryInterpret(int endOfBinary, ref Alternative alternatives)
        {
            bool isError = false;
            byte command = _ram[_instructionPointer];
            byte[] instrArray;
            if (command == CommandCode.EXT || command == CommandCode.BRP)
                return false;

            byte mod = GetMOD(command, ref isError);
            if (isError)
                return false;

            if (mod == 0)
                return true;

            instrArray = alternatives.ReadAlternative(mod, ref _instructionPointer, ref _ram, curLine);

            var modInstructions = new MODInstruction();
            if (modInstructions.Execute(instrArray, command, mod, ref _ram))
                return false;

            return true;
        }

        private byte GetMOD(byte command, ref bool isError)
        {

            // var specificInstruction = new SpecificInstruction();
            switch (command)
            {
                case CommandCode.NOP:
                    return 0;
                case CommandCode.JMP:
                    this.ExecuteJump();
                    return 0;
                case CommandCode.JE:
                    this.ExecuteConditionJump(true);
                    return 0;
                case CommandCode.JNE:
                    this.ExecuteConditionJump(false);
                    return 0;
                case CommandCode.ETR:
                    Console.WriteLine();
                    return 0;
                case CommandCode.IN:
                    this.ExecuteStream(true, ref isError);
                    return 0;
                case CommandCode.OUT:
                    this.ExecuteStream(false, ref isError);
                    return 0;
                case CommandCode.ALLOC:
                    this.ExecuteAllocate(true, ref isError);
                    return 0;
                case CommandCode.ALLOCr:
                    this.ExecuteAllocate(false, ref isError);
                    return 0;
                case CommandCode.INC:
                    return Alternative.rmMOD2_;
                case CommandCode.DEC:
                    return Alternative.rmMOD2_;
                default:
                    return _ram[++_instructionPointer];
            }
        }


        private void ExecuteJump()
        {
            _instructionPointer = BitConverter.ToInt16(_ram, ++_instructionPointer) + Alternative.regs - 1;
            return;
        }

        // JE - true
        // JNE - false
        private void ExecuteConditionJump(bool onEquality)
        {
            short regVal = BitConverter.ToInt16(_ram, (_ram[++_instructionPointer] - Alternative.reg) * 2);
            bool result = regVal != 0;

            if (result && onEquality)
                _instructionPointer = BitConverter.ToInt16(_ram, ++_instructionPointer) + Alternative.regs - 1;
            else if (!result && !onEquality)
                _instructionPointer = BitConverter.ToInt16(_ram, ++_instructionPointer) + Alternative.regs - 1;
            else
                _instructionPointer += 2;
            return;
        }

        // IN - true
        // OUT - false
        private void ExecuteStream(bool streamMod, ref bool isError)
        {
            try
            {
                if (streamMod)
                {
                    byte[] temp = BitConverter.GetBytes(Convert.ToInt16(Console.ReadLine()));
                    Array.Copy(temp, 0, _ram, (_ram[++_instructionPointer] - Alternative.reg) * 2, 2);
                }
                else
                {
                    short temp = BitConverter.ToInt16(_ram, (_ram[++_instructionPointer] - Alternative.reg) * 2);
                    Console.Write($"{temp} ");
                }
            }
            catch
            {
                ErrorHandler.DisplayError(31);
                isError = true;
                return;
            }
        }

        //  val - true
        //  reg - false
        private void ExecuteAllocate(bool allocateMod, ref bool isError)
        {
            try
            {
                byte[] length = BitConverter.GetBytes(Convert.ToInt16(_ram.Length));
                byte tempB = _ram[++_instructionPointer];
                short tempS;

                if (allocateMod)
                {
                    tempS = BitConverter.ToInt16(_ram, ++_instructionPointer);
                    _instructionPointer++;
                }
                else
                {
                    tempS = BitConverter.ToInt16(_ram, (_ram[++_instructionPointer] - Alternative.reg) * 2);
                }

                Array.Resize(ref _ram, _ram.Length + tempS);

                if (_ram.Length >= short.MaxValue)
                {
                    ErrorHandler.DisplayError(63);
                    isError = true;
                    return;
                }

                Array.Copy(length, 0, _ram, (tempB - Alternative.reg) * 2, 2);
                return;
            }
            catch
            {
                ErrorHandler.DisplayError(63);
                isError = true;
                return;
            }
        }
    }
}
