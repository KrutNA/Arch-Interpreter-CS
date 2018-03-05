using System;
using System.IO;

namespace Architecture.Parser
{
    public class TableHeader
    {
        public Int32 sectionCount { get; private set; }
        public Int32 symbolsCount { get; private set; }
        public Int32 sourceFilesCount { get; private set; }
        public Int32 sourceTextRangesCount { get; private set; }
        public Int32 sourceCodePointsCount { get; private set; }
        public Int32 blobsCount { get; private set; }
        public Int32 stringsCount { get; private set; }

        public TableHeader() { }

        public void Read(BinaryReader reader)
        {
            sectionCount = reader.ReadInt32();
            symbolsCount = reader.ReadInt32();
            sourceFilesCount = reader.ReadInt32();
            sourceTextRangesCount = reader.ReadInt32();
            sourceCodePointsCount = reader.ReadInt32();
            blobsCount = reader.ReadInt32();
            stringsCount = reader.ReadInt32();
        }
    }
}
