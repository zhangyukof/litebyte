//__________________________________ License __________________________________
// MIT License
//
// Copyright(c) 2019-2020 ZhangYu
// https: //github.com/zhangyukof/litebyte
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions: 

// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.

// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
//____________________________________ Intro ____________________________________
// Purpose:  Convert base type to bytes
// Author:  ZhangYu
// CreateDate:  2019-12-27
// LastModifiedDate:  2020-01-07

/** 基本类型(枚举类型) | BaseType(enum)  */
const LBBaseType = {
    NONE: 0,
    BIT1: 1, BIT2: 2, BIT3: 3, BIT4: 4, BIT5: 5, BIT6: 6, BIT7: 7,
    INT8: 8, INT16: 9, INT24: 10, INT32: 11, INT40: 12, INT48: 13, INT56: 14, INT64: 15,
    UINT8: 16, UINT16: 17, UINT24: 18, UINT32: 19, UINT40: 20, UINT48: 21, UINT56: 22, UINT64: 23,
    FLOAT8: 24, FLOAT16: 25, FLOAT24: 26, FLOAT32: 27, FLOAT64: 28,
    VAR_INT16: 29, VAR_INT32: 30, VAR_INT64: 31, VAR_UINT16: 32, VAR_UINT32: 33, VAR_UINT64: 34,
    UTF8: 35, UNICODE: 36, ASCII: 37, VAR_UNICODE : 38
}

// ___________ Bit ___________
/** 1位无符号整数(bool 1/8 byte) | 1-bit unsigned integer(bool 1/8 byte) */
const LBBit1 = {
    MAX_VALUE: 1,
    MIN_VALUE: 0,
    BYTE_SIZE: 1 / 8
}

/** 2位无符号整数(2/8 byte) | 2-bits unsigned integer(2/8 byte) */
const LBBit2 = {
    MAX_VALUE: 3,
    MIN_VALUE: 0,
    BYTE_SIZE: 2 / 8
}

/** 3位无符号整数(3/8 byte) | 3-bits unsigned integer(3/8 byte) */
const LBBit3 = {
    MAX_VALUE: 7,
    MIN_VALUE: 0,
    BYTE_SIZE: 4 / 8
}

/** 4位无符号整数(4/8 byte) | 4-bits unsigned integer(4/8 byte) */
const LBBit4 = {
    MAX_VALUE: 15,
    MIN_VALUE: 0,
    BYTE_SIZE: 4 / 8
}

/** 5位无符号整数(5/8 byte) | 5-bits unsigned integer(5/8 byte) */
const LBBit5 = {
    MAX_VALUE: 31,
    MIN_VALUE: 0,
    BYTE_SIZE: 5 / 8
}

/** 6位无符号整数(6/8 byte) | 6-bits unsigned integer(6/8 byte) */
const LBBit6 = {
    MAX_VALUE: 63,
    MIN_VALUE: 0,
    BYTE_SIZE: 6 / 8
}

/** 7位无符号整数(7/8 byte) | 7-bits unsigned integer(7/8 byte) */
const LBBit7 = {
    MAX_VALUE: 127,
    MIN_VALUE: 0,
    BYTE_SIZE: 7 / 8
}

// ___________ Int ___________
/** 1字节有符号整数(sbyte) | 1-byte signed integer(sbyte) */
const LBInt8 = {
    MAX_VALUE: 127,
    MIN_VALUE: -128,
    BYTE_SIZE: 1
}

/** 2字节有符号整数(short) | 2-bytes signed integer(short) */
const LBInt16 = {
    MAX_VALUE: 32767,
    MIN_VALUE: -32768,
    BYTE_SIZE: 2
}

/** 3字节有符号整数(3/4 int) | 3-bytes signed integer(3/4 int) */
const LBInt24 = {
    MAX_VALUE: 8388607,
    MIN_VALUE: -8388608,
    BYTE_SIZE: 3
}

/** 4字节有符号整数(int) | 4-bytes signed integer(int) */
const LBInt32 = {
    MAX_VALUE: 2147483647,
    MIN_VALUE: -2147483648,
    BYTE_SIZE: 4
}

/** 5字节有符号整数(5/8 long) | 5-bytes signed integer(5/8 long) */
const LBInt40 = {
    MAX_VALUE: 549755813887,
    MIN_VALUE: -549755813888,
    BYTE_SIZE: 5
}

/** 6字节有符号整数(6/8 long) | 6-bytes signed integer(6/8 long) */
const LBInt48 = {
    MAX_VALUE: 140737488355327,
    MIN_VALUE: -140737488355328,
    BYTE_SIZE: 6
}

/** 7字节有符号整数(7/8 long) | 7-bytes signed integer(7/8 long) */
const LBInt56 = {
    MAX_VALUE: 36028797018963967,
    MIN_VALUE: -36028797018963968,
    BYTE_SIZE: 7
}

/** 8字节有符号整数(long) | 8-bytes signed integer(long) */
const LBInt64 = {
    MAX_VALUE: 9223372036854775807,
    MIN_VALUE: -9223372036854775808,
    BYTE_SIZE: 8
}

// ___________ UInt ___________
/** 1字节无符号整数(byte) | 1-byte unsigned integer(byte) */
const LBUInt8 = {
    MAX_VALUE: 255,
    MIN_VALUE: 0,
    BYTE_SIZE: 1
}

/** 2字节无符号整数(ushort) | 2-bytes unsigned integer(ushort) */
const LBUInt16 = {
    MAX_VALUE: 65535,
    MIN_VALUE: 0,
    BYTE_SIZE: 2
}

/** 3字节无符号整数(3/4 uint) | 3-bytes unsigned integer(3/4 uint) */
const LBUInt24 = {
    MAX_VALUE: 16777215,
    MIN_VALUE: 0,
    BYTE_SIZE: 3
}

/** 4字节无符号整数(uint) | 4-bytes unsigned integer(uint) */
const LBUInt32 = {
    MAX_VALUE: 4294967295,
    MIN_VALUE: 0,
    BYTE_SIZE: 4
}

/** 5字节无符号整数(5/8 ulong) | 5-bytes unsigned integer(5/8 ulong) */
const LBUInt40 = {
    MAX_VALUE: 1099511627775,
    MIN_VALUE: 0,
    BYTE_SIZE: 5
}

/** 6字节无符号整数(6/8 ulong) | 6-bytes unsigned integer(6/8 ulong) */
const LBUInt48 = {
    MAX_VALUE: 281474976710655,
    MIN_VALUE: 0,
    BYTE_SIZE: 6
}

/** 7字节无符号整数(7/8 ulong) | 7-bytes unsigned integer(7/8 ulong) */
const LBUInt56 = {
    MAX_VALUE: 72057594037927935,
    MIN_VALUE: 0,
    BYTE_SIZE: 7
}

/** 8字节无符号整数(ulong) | 8-bytes unsigned integer(ulong) */
const LBUInt64 = {
    MAX_VALUE: 18446744073709551615,
    MIN_VALUE: 0,
    BYTE_SIZE: 8
}

// ___________ Float ___________
/** 1字节无符号浮点数(取值范围:0F/255F ~ 255F/255F) | 1-byte unsigned float(value range:0F/255F ~ 255F/255F) */
const LBFloat8 = {
    MAX_VALUE: 255 / 255,
    MIN_VALUE: 0 / 255,
    BYTE_SIZE: 1
}

/** 
 * 2字节有符号浮点数(有效数字:3位) | 2-bytes signed float(significant digits:3)
 * [符号(1位) 指数(5位) 尾数(10位) 偏移(15)] | [Sign(1bit) Exponent(5bits) Mantissa(10bits) Bias(15)]
 */
const LBFloat16 = {
    MAX_VALUE: 1.31E+5,
    MIN_VALUE: -1.31E+5,
    BYTE_SIZE: 2
}

/**
 * 3字节有符号浮点数(有效数字:5位) | 3-bytes signed float(significant digits:5)
 * [符号(1位) 指数(7位) 尾数(16位) 偏移(63)] | [Sign(1bit) Exponent(7bits) Mantissa(16bits) Bias(63)]
 */
const LBFloat24 = {
    MAX_VALUE: 3.6893E+19,
    MIN_VALUE: -3.6893E+19,
    BYTE_SIZE: 3
}

/** 
 * 4字节有符号浮点数(IEEE-754 有效数字:7位) | 4-bytes signed float(IEEE754 significant digits:7)
 * [符号(1位) 指数(8位) 尾数(23位) 偏移(127)] | [Sign(1bit) Exponent(8bits) Mantissa(23bits) Bias(127)]
 */
const LBFloat32 = {
    MAX_VALUE: 3.40282347E+38,
    MIN_VALUE: -3.40282347E+38,
    BYTE_SIZE: 4
}

/** 
 * 8字节有符号浮点数(IEEE-754 有效数字:15位) | 8-bytes signed float(IEEE754 significant digits:15)
 * [符号(1位) 指数(11位) 尾数(52位) 偏移(1023)] | [Sign(1bit) Exponent(11bits) Mantissa(52bits) Bias(1023)]
 */
const LBFloat64 = {
    MAX_VALUE: 1.7976931348623157E+308,
    MIN_VALUE: -1.7976931348623157E+308,
    BYTE_SIZE: 4
}

// ___________ VarInt ___________
/** 1位+1~2字节变长有符号整数(short) | 1-bit + 1~2bytes variant-length signed integer(short) */
const LBVarInt16 = {
    MAX_VALUE: 32767,
    MIN_VALUE: -32768,
    MIN_BYTE_SIZE: 1 / 8 + 1,
    MAX_BYTE_SIZE: 1 / 8 + 2
}

/** 2位+1~4字节变长有符号整数(int) | 2-bit + 1~4bytes variant-length signed integer(int) */
const LBVarInt32 = {
    MAX_VALUE: 2147483647,
    MIN_VALUE: -2147483648,
    MIN_BYTE_SIZE: 2 / 8 + 1,
    MAX_BYTE_SIZE: 2 / 8 + 4
}

/** 3位+1~8字节变长有符号整数(int) | 3-bit + 1~8bytes variant-length signed integer(long) */
const LBVarInt64 = {
    MAX_VALUE: 9223372036854775807,
    MIN_VALUE: -9223372036854775808,
    MIN_BYTE_SIZE: 3 / 8 + 1,
    MAX_BYTE_SIZE: 3 / 8 + 8
}

// ___________ VarUInt ___________
/** 1位+1~2字节变长无符号整数(ushort) | 1-bit + 1~2bytes variant-length unsigned integer(ushort) */
const LBVarUInt16 = {
    MAX_VALUE: 65535,
    MIN_VALUE: 0,
    MIN_BYTE_SIZE: 1 / 8 + 1,
    MAX_BYTE_SIZE: 1 / 8 + 2
}

/** 2位+1~4字节变长无符号整数(int) | 2-bit + 1~4bytes variant-length unsigned integer(uint) */
const LBVarUInt32 = {
    MAX_VALUE: 4294967295,
    MIN_VALUE: 0,
    MIN_BYTE_SIZE: 2 / 8 + 1,
    MAX_BYTE_SIZE: 2 / 8 + 4
}

/** 3位+1~8字节变长无符号整数(int) | 3-bit + 1~8bytes variant-length unsigned integer(ulong) */
const LBVarUInt64 = {
    MAX_VALUE: 18446744073709551615,
    MIN_VALUE: 0,
    MIN_BYTE_SIZE: 3 / 8 + 1,
    MAX_BYTE_SIZE: 3 / 8 + 8
}