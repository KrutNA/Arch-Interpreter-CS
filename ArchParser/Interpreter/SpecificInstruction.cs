using System;

namespace Architecture.Interpreter
{
    class SpecificInstruction
    {
        public SpecificInstruction() { }

        public void ExecuteJump(ref int curByte, ref byte[] ram) 
        {
            curByte = BitConverter.ToInt16(ram, ++curByte) + Alternative.regs - 1;
            return;
        }

        // JE - true
        // JNE - false
        public void ExecuteConditionJump(ref int curByte, ref byte[] ram, bool jumpMod)
        {
            short regVal = BitConverter.ToInt16(ram, (ram[++curByte] - Alternative.reg) * 2);
            bool result = regVal != 0;

            if (result && jumpMod)
                curByte = BitConverter.ToInt16(ram, ++curByte) + Alternative.regs - 1;
            else if (!result && !jumpMod)
                curByte = BitConverter.ToInt16(ram, ++curByte) + Alternative.regs - 1;
            else
                curByte += 2;
            return;
        }

        // IN - true
        // OUT - false
        public void ExecuteStream(ref int curByte, ref byte[] ram, bool streamMod, ref bool isError)
        {
            try
            {
                if (streamMod)
                {
                    byte[] temp = BitConverter.GetBytes(Convert.ToInt16(Console.ReadLine()));
                    Array.Copy(temp, 0, ram, (ram[++curByte] - Alternative.reg) * 2, 2);
                }
                else
                {
                    short temp = BitConverter.ToInt16(ram, (ram[++curByte] - Alternative.reg) * 2);
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
        public void ExecuteAllocate(ref int curByte, ref byte[] ram, bool allocateMod, ref bool isError)
        {
            try
            {
                byte[] length = BitConverter.GetBytes(Convert.ToInt16(ram.Length));
                byte tempB = ram[++curByte];
                short tempS;

                if (allocateMod)
                {
                    tempS = BitConverter.ToInt16(ram, ++curByte);
                    curByte++;
                }
                else
                {
                    tempS = BitConverter.ToInt16(ram, (ram[++curByte] - Alternative.reg) * 2);
                }

                Array.Resize(ref ram, ram.Length + tempS);

                if (ram.Length >= short.MaxValue)
                {
                    ErrorHandler.DisplayError(63);
                    isError = true;
                    return;
                }

                Array.Copy(length, 0, ram, (tempB - Alternative.reg) * 2, 2);
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
