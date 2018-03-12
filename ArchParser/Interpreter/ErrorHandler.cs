using System;
using System.Collections.Generic;

namespace Architecture.Interpreter
{
    public sealed class ErrorHandler
    {
        private ErrorHandler() { }

        public static void DisplayError(int code = 0, int line = 0, int argNum = 0)
        {
            var codeDescription = descriptions[code];
            const int errorSeparator = 19;

            var lineText = code != 0 ? line.ToString() : "Unknown";
            var argNumText = code != 0 ? argNum.ToString() : "Unknown";
            var optionalInfo = code <= errorSeparator ? $"\nError: {codeDescription}" : $"\n" +
                                                                                        $"Error: {codeDescription}\n" +
                                                                                        $"Error on line: {lineText}\n" +
                                                                                        $"Error argument: {argNumText}";
            Console.WriteLine($"{optionalInfo}");
        
            Console.WriteLine("Press any key to quit...");
            Console.ReadKey();
            return;
        }

        private static readonly IReadOnlyDictionary<int, string> descriptions = new Dictionary<int, string>()
        {
            { 0, "File not founded" },
            { 1, "Out of memory" },
            { 2, "Input error" },
            { 20, "Register not found" },
            { 21, "Register error" },
            { 30, "Value error" },
            { 31, "Overflow" },
            { 60, "Unknown argument" },
            { 61, "Unknown command" },
            { 62, "Unknown alternative" },
            { 63, "Can't allocate memory" }
        };
    }
}
