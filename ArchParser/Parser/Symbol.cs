using System;
using System.IO;

namespace Architecture.Parser
{
    public class Symbol
    {
        public Int32 sectionIndex { get; private set; }
        public Int64 blobEntryIndex { get; private set; }
        public Int32 nameIndex { get; private set; }

        public Symbol() { }

        public void Read(BinaryReader reader)
        {
            sectionIndex = reader.ReadInt32();
            blobEntryIndex = reader.ReadInt64();
            nameIndex = reader.ReadInt32();
        }
    }
}
