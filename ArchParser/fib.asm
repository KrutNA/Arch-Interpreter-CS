[ section _ram ]
process:
	ALLOC *reg1, 0x10
	IN reg1
	SUB reg1, 0x2
	MOV reg3, 0x1
	MOV reg4, 0x1
	CMP4 reg2, reg1, 0x1 ; <=
	JE reg2, end
	OUT reg3
	OUT reg4
fib:
	MOV reg5, reg4
	ADD reg4, reg3
	MOV reg3, reg5
	OUT reg4
	DEC reg1
	CMP2 reg2, reg1, 0x0 ; !=
	JE reg2, fib
end: