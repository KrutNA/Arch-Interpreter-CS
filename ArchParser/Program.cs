using System;
using System.Threading;
using System.Collections.Generic;
using System.IO;
using Architecture.Parser;
using Architecture.Interpreter;

namespace Architecture
{
    class Program
    {
        public static void Main(string[] args)
        {
            byte[] ram = new byte[65];

            Console.Title = "KrutNA's Interpreter";
            Console.Write("Path: ");
            
            var path = Console.ReadLine().Replace("\"", "");
            BinaryParser binaryParser;

            // Checks file
            if (!File.Exists(path))
            {
                Console.WriteLine("File not founded!\n" +
                                  "Press any key to continue...");
                Console.Read();
                return;
            }

            using (BinaryReader reader = new BinaryReader(File.OpenRead(path)))
            {
                binaryParser = new BinaryParser();
                binaryParser.ReadBytes(reader, ref ram);
            }

            var interpreter = new Interpreter.Interpreter();
            interpreter.DoInterpretation(ref ram);

            if (interpreter.isError)
                return;

            Console.Write("\nDone!\n" +
                          "Press any key to continue...");
            Console.ReadKey();

        }
    }
}
