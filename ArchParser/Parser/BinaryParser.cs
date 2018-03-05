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
            var sections = new List<Sections>();
            var symbols = new List<Symbols>();
            var sourceFiles = new List<SourceFiles>();
            var sourceTextRanges = new List<SourceTextRanges>();
            var sourceCodePoints = new List<SourceCodePoints>();
            var blobs = new List<Blobs>();
            var strings = new List<Strings>();

            var moduleHeader = new ModuleHeader();
            moduleHeader.Read(reader);

            var tableHeader = new TableHeader();
            tableHeader.Read(reader);

            for (var i = 0; i < tableHeader.sectionCount; ++i)
            {
                var section = new Sections();
                section.Read(reader);
                sections.Add(section);
            }

            for (var i = 0; i < tableHeader.symbolsCount; ++i)
            {
                var symbol = new Symbols();
                symbol.Read(reader);
                symbols.Add(symbol);
            }

            for (var i = 0; i < tableHeader.sourceFilesCount; ++i)
            {
                var sourceFile = new SourceFiles();
                sourceFile.Read(reader);
                sourceFiles.Add(sourceFile);
            }

            for (var i = 0; i < tableHeader.sourceTextRangesCount; ++i)
            {
                var sourceTextRange = new SourceTextRanges();
                sourceTextRange.Read(reader);
                sourceTextRanges.Add(sourceTextRange);
            }

            for (var i = 0; i < tableHeader.sourceCodePointsCount; ++i)
            {
                var sourceCodePoint = new SourceCodePoints();
                sourceCodePoint.Read(reader);
                sourceCodePoints.Add(sourceCodePoint);
            }

            for (var i = 0; i < tableHeader.blobsCount; ++i)
            {
                var blob = new Blobs();
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
                var str = new Strings();
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
