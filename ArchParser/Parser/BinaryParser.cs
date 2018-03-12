using System;
using System.IO;
using System.Collections.Generic;

namespace Architecture.Parser
{
    public class BinaryParser
    {
        private List<byte> byteCode = new List<byte>();

        public BinaryParser() { }

        public void ReadBytes(BinaryReader reader, ref byte[] ram)
        {
            var sections = new List<Section>();
            var symbols = new List<Symbol>();
            var sourceFiles = new List<SourceFile>();
            var sourceTextRanges = new List<SourceTextRange>();
            var sourceCodePoints = new List<SourceCodePoint>();
            var blobs = new List<Blob>();
            var strings = new List<String>();

            var moduleHeader = new ModuleHeader();
            moduleHeader.Read(reader);

            var tableHeader = new TableHeader();
            tableHeader.Read(reader);

            for (var i = 0; i < tableHeader.sectionCount; ++i)
            {
                var section = new Section();
                section.Read(reader);
                sections.Add(section);
            }

            for (var i = 0; i < tableHeader.symbolsCount; ++i)
            {
                var symbol = new Symbol();
                symbol.Read(reader);
                symbols.Add(symbol);
            }

            for (var i = 0; i < tableHeader.sourceFilesCount; ++i)
            {
                var sourceFile = new SourceFile();
                sourceFile.Read(reader);
                sourceFiles.Add(sourceFile);
            }

            for (var i = 0; i < tableHeader.sourceTextRangesCount; ++i)
            {
                var sourceTextRange = new SourceTextRange();
                sourceTextRange.Read(reader);
                sourceTextRanges.Add(sourceTextRange);
            }

            for (var i = 0; i < tableHeader.sourceCodePointsCount; ++i)
            {
                var sourceCodePoint = new SourceCodePoint();
                sourceCodePoint.Read(reader);
                sourceCodePoints.Add(sourceCodePoint);
            }

            for (var i = 0; i < tableHeader.blobsCount; ++i)
            {
                var blob = new Blob();
                blob.ReadLength(reader);
                blobs.Add(blob);
            }

            foreach (var blob in blobs)
            {
                byte[] tempArray = blob.Read(reader);
                var tempInt = ram.Length;
                Array.Resize(ref ram, tempInt + blob.blobLength);
                tempArray.CopyTo(ram, tempInt);
            }   

            for (var i = 0; i < tableHeader.stringsCount; ++i)
            {
                var str = new String();
                str.Read(reader);
                strings.Add(str);
            }
        }

        public List<byte> GetByteCode()
        {
            return byteCode;
        }

    }
}
