using System;
using System.IO;

namespace Architecture.Parser
{
    public class String
    {
        public Int32 strLength { get; private set; }
        public byte[] str { get; private set; }

        public String() { }

        public void Read(BinaryReader reader)
        {
            strLength = reader.ReadInt32();
            str = reader.ReadBytes(strLength);
        }
    }
}
