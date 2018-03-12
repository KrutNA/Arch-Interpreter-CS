using System;
using System.IO;

namespace Architecture.Parser
{
    public class Blob
    {
        public Int32 blobLength { get; private set; }
        public byte[] blob { get; private set; }
        
        public Blob() { }
        
        public void ReadLength(BinaryReader reader)
        {
            blobLength = reader.ReadInt32();
        }

        public byte[] Read(BinaryReader reader)
        {
            blob = reader.ReadBytes(blobLength);
            return blob;
        }
    }
}
