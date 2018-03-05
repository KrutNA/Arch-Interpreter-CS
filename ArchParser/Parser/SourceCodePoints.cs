using System;
using System.IO;

namespace Architecture.Parser
{
    public class SourceCodePoints
    {
        public Int64 address { get; private set; }
        public Int32 sourceOperationRangeIndex { get; private set; }

        public SourceCodePoints() { }

        public void Read(BinaryReader reader)
        {
            address = reader.ReadInt64();
            sourceOperationRangeIndex = reader.ReadInt32();
        }
    }
}
