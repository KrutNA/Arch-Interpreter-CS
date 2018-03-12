using System;
using System.Collections.Generic;

namespace Architecture.Interpreter
{
    class MODInstruction
    {
        private const string reg = "reg";
        private const string val = "val";
        private const string ptr = "ptr";

        private IReadOnlyDictionary<byte, Func<short, short, short>> commands = new Dictionary<byte, Func<short, short, short>>
        {
            { CommandCode.ADD, (x, y) => Convert.ToInt16(x + y) },
            { CommandCode.SUB, (x, y) => Convert.ToInt16(x - y) },
            { CommandCode.MUL, (x, y) => Convert.ToInt16(x * y) },
            { CommandCode.INV, (x, y) => Convert.ToInt16(Math.Pow(x, y)) },
            { CommandCode.IOD, (x, y) => Convert.ToInt16(x / y) },
            { CommandCode.MOD, (x, y) => Convert.ToInt16(x % y) },
            { CommandCode.NEG, (x, y) => Convert.ToInt16(-x) },
            { CommandCode.AND, (x, y) => Convert.ToInt16(x & y) },
            { CommandCode.OR, (x, y) => Convert.ToInt16(x | y) },
            { CommandCode.XOR, (x, y) => Convert.ToInt16(x ^ y) },
            { CommandCode.NOT, (x, y) => Convert.ToInt16(~x) },
            { CommandCode.CMP1, (x, y) => Convert.ToInt16(x == y) },
            { CommandCode.CMP2, (x, y) => Convert.ToInt16(x != y) },
            { CommandCode.CMP3, (x, y) => Convert.ToInt16(x > y) },
            { CommandCode.CMP4, (x, y) => Convert.ToInt16(x < y) },
            { CommandCode.CMP5, (x, y) => Convert.ToInt16(x >= y) },
            { CommandCode.CMP6, (x, y) => Convert.ToInt16(x <= y) },
            { CommandCode.MOV, (x, y) => Convert.ToInt16(y) },
            { CommandCode.INC, (x, y) => Convert.ToInt16(++x) },
            { CommandCode.DEC, (x, y) => Convert.ToInt16(--x) }
        };

        private struct Values
        {
            string valueType1;
            string valueType2;
            bool isPointer;

            public Values(string valueType1, string valueType2, bool isPointer = false)
            {
                this.valueType1 = valueType1;
                this.valueType2 = valueType2;
                this.isPointer = isPointer;
            }

            public string[] Get()
            {
                return new string[] { valueType1, valueType2 };
            }
        }

        private IReadOnlyDictionary<byte, Values> values = new Dictionary<byte, Values>
        {
            { Alternative.rmMODr, new Values( reg, null) },
            { Alternative.rmMODv, new Values( val, null) },
            { Alternative.rmMODrr, new Values( reg, reg ) },
            { Alternative.rmMODrv, new Values( reg, val ) },
            { Alternative.rmMODvr, new Values( val, reg ) },
            { Alternative.rmMODvv, new Values( val, val ) },
            { Alternative.rmMOD1rr, new Values( reg, reg ) },
            { Alternative.rmMOD1rv, new Values( reg, val ) },
            { Alternative.rmMOD1vr, new Values( val, reg ) },
            { Alternative.rmMOD1vv, new Values( val, val ) },
            { Alternative.rmMOD2r, new Values( reg, null ) },
            { Alternative.rmMOD2v, new Values( val, null ) },
            { Alternative.rmMOD2_, new Values( null, null ) },
            { Alternative.rmMOD3p, new Values( ptr, reg ) },
            { Alternative.rmMOD3P, new Values( reg, ptr ) },
            { Alternative.rmMOD3p_, new Values( ptr, ptr ) },
            { Alternative.rmMOD3r, new Values( reg, null ) },
            { Alternative.rmMOD3v, new Values( val, null ) }
        };

        public MODInstruction() { }

        public bool Execute(byte[] instrArray, byte command, byte mod, ref byte[] ram)
        {
            bool isPointer = false;
            bool isError = false;
            short[] valuesArray;
            try
            {
                valuesArray = GetValues(ref ram, instrArray, values[mod].Get(), ref isPointer);
            }
            catch
            {
                ErrorHandler.DisplayError(31);
                isError = true;
                return isError;
            }
            short result = 0;
            try
            {
                result = ExecuteByParameter(valuesArray, commands[command]);
                byte[] tempArray = BitConverter.GetBytes(result);
                if (isPointer)
                {
                    int temp = BitConverter.ToInt16(ram, (instrArray[0] - Alternative.reg) * 2);
                    Array.Copy(tempArray, 0, ram, temp, 2);
                }
                else
                    Array.Copy(tempArray, 0, ram, (instrArray[0] - Alternative.reg) * 2, 2);
            }
            catch
            {
                ErrorHandler.DisplayError(31);
                isError = true;
            }
            return isError;
        }

        private short[] GetValues(ref byte[] ram, byte[] instrArray, string[] valueTypes, ref bool isPointer)
        {
            short[] values = new short[2];
            byte[] byteArray = new byte[4];
            int k = 1;
            for (int i = 0; i < 2; i++)
                switch (valueTypes[i])
                {
                    case (reg):
                        values[i] = BitConverter.ToInt16(ram, (instrArray[k++] - Alternative.reg) * 2);
                        break;
                    case (val):
                        Array.Copy(instrArray, k, byteArray, i * 2, 2);
                        values[i] = BitConverter.ToInt16(byteArray, i * 2);
                        k += 2;
                        break;
                    case (ptr):
                        if (i == 0)
                        {
                            isPointer = true;
                        }
                        else
                        {
                            values[i] = BitConverter.ToInt16(ram, (instrArray[i] - Alternative.reg) * 2);
                            values[i] = BitConverter.ToInt16(ram, values[i] + Alternative.regs - 1);
                            k++;
                        }
                        break;
                    case (null):
                        if (i == 0)
                        {
                            values[0] = BitConverter.ToInt16(ram, (instrArray[0] - Alternative.reg) * 2);
                            values[1] = values[0];
                        }
                        else
                        {
                            values[1] = values[0];
                            values[0] = BitConverter.ToInt16(ram, (instrArray[0] - Alternative.reg) * 2);
                        }
                        break;
                }
            return values;
        }

        private short ExecuteByParameter(short[] Values, Func<short, short, short> parameterFunction)
        {
            return Convert.ToInt16(parameterFunction(Values[0], Values[1]));
        }
    }
}