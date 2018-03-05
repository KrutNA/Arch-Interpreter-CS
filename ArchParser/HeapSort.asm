[ section _ram ]
;===================================================
;	reg1	- count of elements
;	reg32	- memory
;	reg31	- bool
;	reg28	- bool	BuildHeap / HeapSort
;	reg3	- heapSize
;===================================================
Allocate:
	IN		reg1
	ALLOC 	*reg32,		reg1
	MOV		reg30,		reg32
	ADD		reg3,		reg32,		reg1
;===================================================
Input:
	CMP1 	reg31,		reg30,		reg3
	JE 		reg31,		HeapSort

	IN 		reg29
	MOV		*reg30,		reg29
	INC		reg30
	JMP 	Input

;===================================================
Heapify:
;=======================================;
; 		reg19 - *reg20 - Parrent		;
; 		reg21 - *reg22 - Child 1		;
; 		reg23 - *reg24 - Child 2		;
;		reg25 - *reg26 - Largest		;
;=======================================;
	MOV		reg20,		*reg19
	MUL		reg21,		reg19,		0x2
	INC		reg21
	ADD		reg23,		reg21,		0x1

	CMP6	reg31,		reg21,		reg2
	JNE		reg31,		.Else
	MOV		reg22,		*reg21
	CMP3	reg31,		reg22,		reg20
	JNE		reg31,		.Else

	MOV		reg25,		reg21
	MOV		reg26,		*reg21
	JMP 	.If
.Else:
	MOV		reg25,		reg19
	MOV		reg26,		reg20
.If:
	CMP6	reg31,		reg23,		reg2
	JNE		reg31,		.Swap
	MOV		reg24,		*reg23
	CMP3	reg31,		reg24,		reg26
	JNE		reg31,		.Swap

	MOV		reg25,		reg23
	MOV		reg26,		reg24
.Swap:
	CMP2	reg31,		reg19,		reg25
	JNE		reg31,		.Return

	MOV		reg27,		*reg19
	MOV		*reg19,		*reg25
	MOV		*reg25,		reg27

	MOV		reg19,		reg25
	JMP		Heapify
.Return:
	JE		reg28,		HeapSort.Return
	JMP 	BuildHeap.Return

;===================================================
BuildHeap:
	SUB		reg2,		reg1,		0x1
	IOD		reg19,		reg2,		0x2
.Start:
	CMP5	reg31,		reg19,		0x0
	JNE		reg31,		HeapSort.Start

	MOV		reg28,		0x0
	JMP 	Heapify
.Return:
	DEC		reg19
	JMP		.Start

;===================================================
HeapSort:
	SUB		reg5,		reg1,		0x1
	JMP		BuildHeap
.Start:
	CMP3	reg31,		reg5,		0x0
	JNE		reg31,		OutSort

	MOV		reg6,		*reg5
	MOV		*reg5,		*reg32
	MOV		*reg32,		reg6
	DEC		reg2

	MOV		reg19,		0x0
	MOV		reg28,		0x1
	JMP		Heapify
.Return:
	DEC		reg5
	JMP		.Start

;===================================================
OutSort:
	MOV		reg30,		reg32
.Start:
	CMP1	reg31,		reg30,		reg3
	JE		reg31,		END

	MOV 	reg31,		*reg30
	OUT		reg31
	INC 	reg30
	JMP		.Start
END:
;===================================================