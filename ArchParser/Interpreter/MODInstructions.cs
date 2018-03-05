using System;

namespace Architecture.Interpreter
{
    class MODInstructions
    {
        public MODInstructions() { }
        
        public bool Execute(byte[] instrArray, byte command, byte mod, ref byte[] ram)
        {
            var valuesArray = GetValues(mod, instrArray, ref ram);
            bool isError = false;
            short result = 0;
            try
            {
                switch (command)
                {
                    case CommandCodes.ADD:
                        result = ExecuteByParameter(valuesArray[0], valuesArray[1], (x, y) => Convert.ToInt16(x + y));
                        break;
                    case CommandCodes.SUB:
                        result = ExecuteByParameter(valuesArray[0], valuesArray[1], (x, y) => Convert.ToInt16(x - y));
                        break;
                    case CommandCodes.MUL:
                        result = ExecuteByParameter(valuesArray[0], valuesArray[1], (x, y) => Convert.ToInt16(x * y));
                        break;
                    case CommandCodes.INV:
                        result = ExecuteByParameter(valuesArray[0], valuesArray[1], (x, y) => Convert.ToInt16(Math.Pow(x, y)));
                        break;
                    case CommandCodes.IOD:
                        result = ExecuteByParameter(valuesArray[0], valuesArray[1], (x, y) => Convert.ToInt16(x / y));
                        break;
                    case CommandCodes.MOD:
                        result = ExecuteByParameter(valuesArray[0], valuesArray[1], (x, y) => Convert.ToInt16(x % y));
                        break;
                    case CommandCodes.AND:
                        result = ExecuteByParameter(valuesArray[0], valuesArray[1], (x, y) => Convert.ToInt16(x & y));
                        break;
                    case CommandCodes.OR:
                        result = ExecuteByParameter(valuesArray[0], valuesArray[1], (x, y) => Convert.ToInt16(x | y));
                        break;
                    case CommandCodes.XOR:
                        result = ExecuteByParameter(valuesArray[0], valuesArray[1], (x, y) => Convert.ToInt16(x ^ y));
                        break;
                    case CommandCodes.CMP1:
                        result = ExecuteByParameter(valuesArray[0], valuesArray[1], (x, y) => Convert.ToInt16(x == y));
                        break;
                    case CommandCodes.CMP2:
                        result = ExecuteByParameter(valuesArray[0], valuesArray[1], (x, y) => Convert.ToInt16(x != y));
                        break;
                    case CommandCodes.CMP3:
                        result = ExecuteByParameter(valuesArray[0], valuesArray[1], (x, y) => Convert.ToInt16(x > y));
                        break;
                    case CommandCodes.CMP4:
                        result = ExecuteByParameter(valuesArray[0], valuesArray[1], (x, y) => Convert.ToInt16(x < y));
                        break;
                    case CommandCodes.CMP5:
                        result = ExecuteByParameter(valuesArray[0], valuesArray[1], (x, y) => Convert.ToInt16(x >= y));
                        break;
                    case CommandCodes.CMP6:
                        result = ExecuteByParameter(valuesArray[0], valuesArray[1], (x, y) => Convert.ToInt16(x <= y));
                        break;
                }
                byte[] tempArray = BitConverter.GetBytes(result);
                Array.Copy(tempArray, 0, ram, instrArray[0] - Alternatives.reg, 2);
            }
            catch
            {
                ErrorHandler.DisplayError(61);
                isError = true;
            }
            return isError;
        }

        private short[] GetValues(byte mod, byte[] instrArray, ref byte[] ram)
        {
            var valuesArray = new short[2];
            var bytesArray = new byte[4];
            switch (mod)
            {
                case (Alternatives.rmMODr):
                    valuesArray[0] = BitConverter.ToInt16(ram, instrArray[0] - Alternatives.reg);
                    valuesArray[1] = BitConverter.ToInt16(ram, instrArray[1] - Alternatives.reg);
                    break;
                case (Alternatives.rmMODv):
                    valuesArray[0] = BitConverter.ToInt16(ram, instrArray[0] - Alternatives.reg);
                    Array.Copy(bytesArray, 0, instrArray, 1, 2);
                    valuesArray[1] = BitConverter.ToInt16(bytesArray, 0);
                    break;
                case (Alternatives.rmMODrr):
                    valuesArray[0] = BitConverter.ToInt16(ram, instrArray[1] - Alternatives.reg);
                    valuesArray[1] = BitConverter.ToInt16(ram, instrArray[2] - Alternatives.reg);
                    break;
                case (Alternatives.rmMODrv):
                    valuesArray[0] = BitConverter.ToInt16(ram, instrArray[1] - Alternatives.reg);
                    Array.Copy(bytesArray, 0, instrArray, 2, 2);
                    valuesArray[1] = BitConverter.ToInt16(bytesArray, 0);
                    break;
                case (Alternatives.rmMODvr):
                    Array.Copy(bytesArray, 0, instrArray, 1, 2);
                    valuesArray[0] = BitConverter.ToInt16(bytesArray, 0);
                    valuesArray[1] = BitConverter.ToInt16(ram, instrArray[3] - Alternatives.reg);
                    break;
                case (Alternatives.rmMODvv):
                    Array.Copy(bytesArray, 0, instrArray, 1, 2);
                    valuesArray[0] = BitConverter.ToInt16(bytesArray, 0);
                    Array.Copy(bytesArray, 2, instrArray, 3, 2);
                    valuesArray[1] = BitConverter.ToInt16(bytesArray, 2);
                    break;
                case (Alternatives.rmMOD1rr):
                    goto case Alternatives.rmMODrr;
                case (Alternatives.rmMOD1rv):
                    goto case Alternatives.rmMODrv;
                case (Alternatives.rmMOD1vr):
                    goto case Alternatives.rmMODvr;
                case (Alternatives.rmMOD1vv):
                    goto case Alternatives.rmMODvv;
            }
            return valuesArray;
        }

        private short ExecuteByParameter(short Value1, short Value2, Func<short, short, short> parameterFunction)
        {
            return Convert.ToInt16(parameterFunction(Value1, Value2));
        }
    }
}