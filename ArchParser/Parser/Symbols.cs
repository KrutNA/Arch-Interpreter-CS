using System;
using System.IO;

namespace Architecture.Parser
{
    public class Symbols
    {
        public Int32 sectionIndex { get; private set; }
        public Int64 blobEntryIndex { get; private set; }
        public Int32 nameIndex { get; private set; }

        public Symbols() { }

        public void Read(BinaryReader reader)
        {
            sectionIndex = reader.ReadInt32();
            blobEntryIndex = reader.ReadInt64();
            nameIndex = reader.ReadInt32();
        }
    }
}
