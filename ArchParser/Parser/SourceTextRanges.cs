using System;
using System.IO;

namespace Architecture.Parser
{
    public class SourceTextRanges
    {
        public Int32 sourceFileIndex { get; private set; }
        public Int32 position { get; private set; }
        public Int32 length { get; private set; }
        public Int32 line { get; private set; }
        public Int32 column { get; private set; }

        public SourceTextRanges() { }

        public void Read(BinaryReader reader)
        {
            sourceFileIndex = reader.ReadInt32();
            position = reader.ReadInt32();
            length = reader.ReadInt32();
            line = reader.ReadInt32();
            column = reader.ReadInt32();
        }
    }
}
