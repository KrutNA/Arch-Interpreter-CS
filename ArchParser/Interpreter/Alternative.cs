using System.Collections.Generic;

namespace Architecture.Interpreter
{
    class Alternative
    {
        public const int regs = 66;
        public const int reg = 0b11000000;
        //
        // rmMOD
        //
        public const byte rmMODr = 0b01100000;      //r
        public const byte rmMODv = 0b01100001;      //v
        public const byte rmMODo = 0b11111111;      //o
        public const byte rmMODrr = 0b01100010;     //rr
        public const byte rmMODrv = 0b01100011;     //rv
        public const byte rmMODvr = 0b01100100;     //vr
        public const byte rmMODvv = 0b01100101;     //vv
        //
        // rmMOD1
        //
        public const byte rmMOD1rr = 0b01100110;    //rr
        public const byte rmMOD1rv = 0b01100111;    //rv
        public const byte rmMOD1vr = 0b01101000;    //vr
        public const byte rmMOD1vv = 0b01101001;    //vv
        //
        // rmMOD2
        //
        public const byte rmMOD2_ = 0b01101010;     //_
        public const byte rmMOD2r = 0b01101011;     //r
        public const byte rmMOD2v = 0b01101100;     //v
        //
        // rmMOD3
        //
        public const byte rmMOD3r = 0b01101101;     //r
        public const byte rmMOD3v = 0b01101110;     //v
        public const byte rmMOD3p = 0b01101111;     //p
        public const byte rmMOD3P = 0b01110000;     //P
        public const byte rmMOD3p_ = 0b01110001;    //p_
        //
        //  Dictionary, which contains count of bytes for alternatives
        //
        private IReadOnlyDictionary<byte, short> rmMODs = new Dictionary<byte, short>
        {
            { rmMODr,   2 },      //r
            { rmMODv,   3 },      //v
            { rmMODrr,  3 },      //rr
            { rmMODrv,  4 },      //rv
            { rmMODvr,  4 },      //vr
            { rmMODvv,  5 },      //vv
            { rmMOD1rr, 3 },      //r
            { rmMOD1rv, 4 },      //rv
            { rmMOD1vr, 4 },      //vr
            { rmMOD1vv, 5 },      //vv
            { rmMOD2_,  1 },      //_
            { rmMOD2r,  2 },      //r
            { rmMOD2v,  3 },      //v
            { rmMOD3r,  2 },      //r
            { rmMOD3v,  3 },      //v
            { rmMOD3p,  2 },      //p
            { rmMOD3P,  2 },      //P
            { rmMOD3p_, 2 },      //p_
            { rmMODo,   2 }       //o
        };
        
        public Alternative() { }

        //
        //  Gets mod and return array of bytes
        //
        public byte[] ReadAlternative(byte rmMOD, ref int curByte, ref byte[] ram, int curLine)
        {
            var tempByteArray = new byte[20];
            int byteCount = rmMODs[rmMOD];
            
            for (int i = 0; i < byteCount; i++)
            {
                tempByteArray[i] = ram[++curByte];
            }

            return tempByteArray;
        }
    }
}
