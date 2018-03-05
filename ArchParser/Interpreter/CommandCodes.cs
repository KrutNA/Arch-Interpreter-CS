namespace Architecture.Interpreter
{
    public static class CommandCodes
    {
        //
        // Main commands
        //
        public const byte NOP = 0b00000000;
        public const byte MOV = 0b00000001;
        public const byte EXT = 0b00000010;
        public const byte BRP = 0b00000011;
        //
        // Jump
        //
        public const byte JMP = 0b00001000;
        //
        // Jump if yes
        //
        public const byte JE = 0b00001010;
        //
        // Jump if not
        //
        public const byte JNE = 0b00001100;
        //
        // In / Out
        //
        public const byte ETR = 0b00001101;
        public const byte IN = 0b00001110;
        public const byte OUT = 0b00001111;
        //
        // Bit operations
        //
        public const byte CMP1 = 0b00010000;
        public const byte CMP2 = 0b00010001;
        public const byte CMP3 = 0b00010010;
        public const byte CMP4 = 0b00010011;
        public const byte CMP5 = 0b00010100;
        public const byte CMP6 = 0b00010101;
        //
        // Pointers
        //
        public const byte PTR1 = 0b00010110;
        public const byte PTR2 = 0b00010111;
        public const byte PTR3 = 0b00011000;
        public const byte PTR4 = 0b00011001;
        //
        // Allocate
        //
        public const byte ALLOC = 0b00011010;
        public const byte ALLOCr = 0b00011011;
        //
        // Math commands
        //
        public const byte ADD = 0b10000000;
        public const byte SUB = 0b10000001;
        public const byte MUL = 0b10000010;
        public const byte DIV = 0b10000011;
        public const byte INV = 0b10000100;
        public const byte NEG = 0b10000101;
        public const byte MOD = 0b10000110;
        public const byte IOD = 0b10000111;
        public const byte INC = 0b10001000;
        public const byte DEC = 0b10001001;
        //
        // Logical commands
        //
        public const byte AND = 0b10100000;
        public const byte OR = 0b10100001;
        public const byte XOR = 0b10100010;
        public const byte NOT = 0b10100011;
        //
        // Stack commands
        //
        public const byte POP = 0b10101000;
        public const byte POPr = 0b10101001;
        public const byte PUSH = 0b10101010;
        public const byte PUSHr = 0b10101011;
     
    }
}
