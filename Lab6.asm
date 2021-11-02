;tipo de procesador
.386 ; 32bits

.MODEL flat, stdcall
.STACK 4096


option casemap :none


include windows.inc
include masm32.inc
include user32.inc
include kernerl32.inc
include macros.asm
include masm32rt.inc


includelib masm32.lib
includelib user32.lib
includelib kernerl32.lib

.DATA
; variables inicializadas
A DB 3
B DB 2
C DB 3
Operacion1 DB 0
Operacion2 DB 0
Operacion3 DB 0


; .CONST
; constantes
.CODE
print chr$("Nombre: Catherine Rosario López Vicente")
print chr$("Carrera: Ingenieria en Sistemas")
print chr$("Carné: 1055816")

; A+2B, A*C-B, A+B+C
MOV eax, B
MUL eax, 2
ADD eax, A
MOV Operacion1, eax
print str$(eax)
MOV eax, A
MUL eax, C
SUB eax, B
MOV Operacion2, eax
print str$(eax)
MOV eax, A
ADD eax, B
ADD eax, C
MOV Operacion3,eax
print str$(eax)

invoke ExitProcess,0

main ENDP
END main

