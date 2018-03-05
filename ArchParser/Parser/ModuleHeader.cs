using System;
using System.IO;

namespace Architecture.Parser
{
    public class ModuleHeader
    {
        public byte[] signature { get; private set; }
        public Int32 formatVersion { get; private set; }
        public Int32 platformNameIndex { get; private set; }
        public Int32 platformVersion { get; private set; }
        public Int64 entryPoint { get; private set; }

        public ModuleHeader() { }

        public void Read(BinaryReader reader)
        {
            signature = reader.ReadBytes(12);
            formatVersion = reader.ReadInt32();
            platformNameIndex = reader.ReadInt32();
            platformVersion = reader.ReadInt32();
            entryPoint = reader.ReadInt64();
        }
    }
}
    