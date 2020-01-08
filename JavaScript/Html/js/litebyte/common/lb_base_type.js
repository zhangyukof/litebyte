//__________________________________ License __________________________________
// MIT License
//
// Copyright(c) 2019-2020 ZhangYu
// https://github.com/zhangyukof/litebyte
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
// Purpose: Convert base type to bytes
// Author: ZhangYu
// CreateDate: 2019-08-13
// LastModifiedDate: 2020-01-07

// enum
const LBBaseType = {
    NONE:0,
    BIT1:1, BIT2:2, BIT3:3, BIT4:4, BIT5:5, BIT6:6, BIT7:7,
    INT8:8, INT16:9, INT24:10, INT32:11, INT40:12, INT48:13, INT56:14, INT64:15,
    UINT8:16, UINT16:17, UINT24:18, UINT32:19, UINT40:20, UINT48:21, UINT56:22, UINT64:23,
    FLOAT8:24, FLOAT16:25, FLOAT24:26, FLOAT32:27, FLOAT64:28,
    VARINT16:29, VARINT32:30, VARINT64:31, VARUINT16:32, VARUINT32:33, VARUINT64:34,
    UTF8:35, UNICODE:36, ASCII:37
}

// ___________ Bit ___________
const LBBit1 = {
    MAX_VALUE:1,
    MIN_VALUE:0,
    BYTE_SIZE:1 / 8
}

const LBBit2 = {
    MAX_VALUE:3,
    MIN_VALUE:0,
    BYTE_SIZE:2 / 8
}

const LBBit3 = {
    MAX_VALUE:7,
    MIN_VALUE:0,
    BYTE_SIZE:4 / 8
}

const LBBit4 = {
    MAX_VALUE:15,
    MIN_VALUE:0,
    BYTE_SIZE:4 / 8
}

const LBBit5 = {
    MAX_VALUE:31,
    MIN_VALUE:0,
    BYTE_SIZE:5 / 8
}

const LBBit6 = {
    MAX_VALUE:63,
    MIN_VALUE:0,
    BYTE_SIZE:6 / 8
}

const LBBit7 = {
    MAX_VALUE:127,
    MIN_VALUE:0,
    BYTE_SIZE:7 / 8
}

// ___________ Int ___________
const LBInt8 = {
    MAX_VALUE:127,
    MIN_VALUE:-128,
    BYTE_SIZE:1
}

const LBInt16 = {
    MAX_VALUE:32767,
    MIN_VALUE:-32768,
    BYTE_SIZE:2
}

const LBInt24 = {
    MAX_VALUE:8388607,
    MIN_VALUE:-8388608,
    BYTE_SIZE:3
}

const LBInt32 = {
    MAX_VALUE:2147483647,
    MIN_VALUE:-2147483648,
    BYTE_SIZE:4
}

const LBInt40 = {
    MAX_VALUE:549755813887,
    MIN_VALUE:-549755813888,
    BYTE_SIZE:5
}

const LBInt48 = {
    MAX_VALUE:140737488355327,
    MIN_VALUE:-140737488355328,
    BYTE_SIZE:6
}

const LBInt56 = {
    MAX_VALUE:36028797018963967,
    MIN_VALUE:-36028797018963968,
    BYTE_SIZE:7
}

const LBInt64 = {
    MAX_VALUE:9223372036854775807,
    MIN_VALUE:-9223372036854775808,
    BYTE_SIZE:8
}

// ___________ UInt ___________
const LBUInt8 = {
    MAX_VALUE:255,
    MIN_VALUE:0,
    BYTE_SIZE:1
}

const LBUInt16 = {
    MAX_VALUE:65535,
    MIN_VALUE:0,
    BYTE_SIZE:2
}

const LBUInt24 = {
    MAX_VALUE:16777215,
    MIN_VALUE:0,
    BYTE_SIZE:3
}

const LBUInt32 = {
    MAX_VALUE:4294967295,
    MIN_VALUE:0,
    BYTE_SIZE:4
}

const LBUInt40 = {
    MAX_VALUE:1099511627775,
    MIN_VALUE:0,
    BYTE_SIZE:5
}

const LBUInt48 = {
    MAX_VALUE:281474976710655,
    MIN_VALUE:0,
    BYTE_SIZE:6
}

const LBUInt56 = {
    MAX_VALUE:72057594037927935,
    MIN_VALUE:0,
    BYTE_SIZE:7
}

const LBUInt64 = {
    MAX_VALUE:18446744073709551615,
    MIN_VALUE:0,
    BYTE_SIZE:8
}

// ___________ Float ___________
const LBFloat8 = {
    MAX_VALUE:255 / 255,
    MIN_VALUE:0 / 255,
    BYTE_SIZE:1
}

const LBFloat16 = {
    MAX_VALUE:1.31E+5,
    MIN_VALUE:-1.31E+5,
    BYTE_SIZE:2
}

const LBFloat24 = {
    MAX_VALUE:3.6893E+19,
    MIN_VALUE:-3.6893E+19,
    BYTE_SIZE:3
}

const LBFloat32 = {
    MAX_VALUE:3.40282347E+38,
    MIN_VALUE:-3.40282347E+38,
    BYTE_SIZE:4
}

const LBFloat64 = {
    MAX_VALUE:1.7976931348623157E+308,
    MIN_VALUE:-1.7976931348623157E+308,
    BYTE_SIZE:4
}

// ___________ VarInt ___________
const LBVarInt16 = {
    MAX_VALUE:32767,
    MIN_VALUE:-32768,
    MIN_BYTE_SIZE:1 / 8 + 1,
    MAX_BYTE_SIZE:1 / 8 + 2
}

const LBVarInt32 = {
    MAX_VALUE:2147483647,
    MIN_VALUE:-2147483648,
    MIN_BYTE_SIZE:2 / 8 + 1,
    MAX_BYTE_SIZE:2 / 8 + 4
}

const LBVarInt64 = {
    MAX_VALUE:9223372036854775807,
    MIN_VALUE:-9223372036854775808,
    MIN_BYTE_SIZE:3 / 8 + 1,
    MAX_BYTE_SIZE:3 / 8 + 8
}

// ___________ VarUInt ___________
const LBVarUInt16 = {
    MAX_VALUE:65535,
    MIN_VALUE:0,
    MIN_BYTE_SIZE:1 / 8 + 1,
    MAX_BYTE_SIZE:1 / 8 + 2
}

const LBVarUInt32 = {
    MAX_VALUE:4294967295,
    MIN_VALUE:0,
    MIN_BYTE_SIZE:2 / 8 + 1,
    MAX_BYTE_SIZE:2 / 8 + 4
}
const LBVarUInt64 = {
    MAX_VALUE:18446744073709551615,
    MIN_VALUE:0,
    MIN_BYTE_SIZE:3 / 8 + 1,
    MAX_BYTE_SIZE:3 / 8 + 8
}