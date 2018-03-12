using System;
using System.IO;

namespace Architecture.Parser
{
    public class SourceFile
    {
        public Int32 fileNameIndex { get; private set; }
        public Int32 sha256hashBytesIndex { get; private set; }

        public SourceFile() { }

        public void Read(BinaryReader reader)
        {
            fileNameIndex = reader.ReadInt32();
            sha256hashBytesIndex = reader.ReadInt32();
        }
    }
}
