using System;

namespace Architecture.Interpreter
{
    public sealed class ErrorHandler
    {
        private ErrorHandler() { }

        public static void DisplayError(int code = 0, int line = 0, int argNum = 0)
        {
            var codeDescription = GetDescription(code);
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

        private static string GetDescription(int code)
        {
            switch (code)
            {
                case 0:
                    return "File not founded";
                case 1:
                    return "Out of memory";
                case 2:
                    return "Input error";
                case 20:
                    return "Register not found";
                case 21:
                    return "Register error";
                case 30:
                    return "Value error";
                case 31:
                    return "Overflow";
                case 60:
                    return "Unknown argument";
                case 61:
                    return "Unknown command";
                case 62:
                    return "Unknown alternative";
                case 63:
                    return "Can't allocate memory";
                default:
                    return "Unknown";
            }
        }
    }
}
