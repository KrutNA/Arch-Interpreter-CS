using System;
using System.IO;

namespace Architecture.Parser
{
    public class Section
    {
        public Int32 blobIndex { get; private set; }
        public Int32 symbolsCount { get; private set; }
        public Int64 startAddress { get; private set; }
        public Int16 kind { get; private set; }
        public Int32 customSectionNameIn { get; private set; }
        public Int16 accessMode { get; private set; }

        public Section() { }

        public void Read(BinaryReader reader)
        {
            blobIndex = reader.ReadInt32();
            symbolsCount = reader.ReadInt32();
            startAddress = reader.ReadInt64();
            kind = reader.ReadInt16();
            customSectionNameIn = reader.ReadInt32();
            accessMode = reader.ReadInt16();
        }
    }
}
