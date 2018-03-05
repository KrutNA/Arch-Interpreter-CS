using System;
using System.IO;

namespace Architecture.Parser
{
    public class Blobs
    {
        public Int32 blobLength { get; private set; }
        public byte[] blob { get; private set; }
        
        public Blobs() { }
        
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
