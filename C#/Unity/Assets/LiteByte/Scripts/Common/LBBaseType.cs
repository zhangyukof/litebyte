#region License
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
#endregion
#region Intro
// Purpose: Define base type value range
// Author: ZhangYu
// CreateDate: 2019-11-14
// LastModifiedDate: 2020-01-07
#endregion
namespace LiteByte.Common {

    #region BaseTypeEnum
    /// <summary> 基本类型 | BaseType </summary>
    public enum LBBaseType : byte {
        None,
        Bit1, Bit2, Bit3, Bit4, Bit5, Bit6, Bit7,
        Int8, Int16, Int24, Int32, Int40, Int48, Int56, Int64,
        UInt8, UInt16, UInt24, UInt32, UInt40, UInt48, UInt56, UInt64,
        Float8, Float16, Float24, Float32, Float64,
        VarInt16, VarInt32, VarInt64, VarUInt16, VarUInt32, VarUInt64,
        ASCII, Unicode, UTF8, VarUnicode
    }
    #endregion

    #region Bit
    /// <summary> 1位无符号整数(bool 1/8 byte) | 1-bit unsigned integer(bool 1/8 byte) </summary>
    public struct LBBit1 {
        public const byte MaxValue = 1;
        public const byte MinValue = 0;
        public const float ByteSize = 1F / 8F;
    }

    /// <summary> 2位无符号整数(2/8 byte) | 2-bits unsigned integer(2/8 byte) </summary>
    public struct LBBit2 {
        public const byte MaxValue = 3;
        public const byte MinValue = 0;
        public const float ByteSize = 2F / 8F;
    }

    /// <summary> 3位无符号整数(3/8 byte) | 3-bits unsigned integer(3/8 byte) </summary>
    public struct LBBit3 {
        public const byte MaxValue = 7;
        public const byte MinValue = 0;
        public const float ByteSize = 3F / 8F;
    }

    /// <summary> 4位无符号整数(4/8 byte) | 4-bits unsigned integer(4/8 byte) </summary>
    public struct LBBit4 {
        public const byte MaxValue = 15;
        public const byte MinValue = 0;
        public const float ByteSize = 4F / 8F;
    }

    /// <summary> 5位无符号整数(5/8 byte) | 5-bits unsigned integer(5/8 byte) </summary>
    public struct LBBit5 {
        public const byte MaxValue = 31;
        public const byte MinValue = 0;
        public const float ByteSize = 5F / 8F;
    }

    /// <summary> 6位无符号整数(6/8 byte) | 6-bits unsigned integer(6/8 byte) </summary>
    public struct LBBit6 {
        public const byte MaxValue = 63;
        public const byte MinValue = 0;
        public const float ByteSize = 6F / 8F;
    }

    /// <summary> 7位无符号整数(7/8 byte) | 7-bits unsigned integer(7/8 byte) </summary>
    public struct LBBit7 {
        public const byte MaxValue = 127;
        public const byte MinValue = 0;
        public const float ByteSize = 7F / 8F;
    }
    #endregion

    #region Integer
    /// <summary> 1字节有符号整数(sbyte) | 1-byte signed integer(sbyte) </summary>
    public struct LBInt8 {
        public const sbyte MaxValue = sbyte.MaxValue;
        public const sbyte MinValue = sbyte.MinValue;
        public const int ByteSize = 1;
    }

    /// <summary> 2字节有符号整数(short) | 2-bytes signed integer(short) </summary>
    public struct LBInt16 {
        public const short MaxValue = short.MaxValue;
        public const short MinValue = short.MinValue;
        public const int ByteSize = 2;
    }

    /// <summary> 3字节有符号整数(3/4 int) | 3-bytes signed integer(3/4 int) </summary>
    public struct LBInt24 {
        public const int MaxValue = 8388607;
        public const int MinValue = -8388608;
        public const int ByteSize = 3;
    }

    /// <summary> 4字节有符号整数(int) | 4-bytes signed integer(int) </summary>
    public struct LBInt32 {
        public const int MaxValue = int.MaxValue;
        public const int MinValue = int.MinValue;
        public const int ByteSize = 4;
    }

    /// <summary> 5字节有符号整数(5/8 long) | 5-bytes signed integer(5/8 long) </summary>
    public struct LBInt40 {
        public const long MaxValue = 549755813887L;
        public const long MinValue = -549755813888L;
        public const int ByteSize = 5;
    }

    /// <summary> 6字节有符号整数(6/8 long) | 6-bytes signed integer(6/8 long) </summary>
    public struct LBInt48 {
        public const long MaxValue = 140737488355327L;
        public const long MinValue = -140737488355328L;
        public const int ByteSize = 6;
    }

    /// <summary> 7字节有符号整数(7/8 long) | 7-bytes signed integer(7/8 long) </summary>
    public struct LBInt56 {
        public const long MaxValue = 36028797018963967L;
        public const long MinValue = -36028797018963968L;
        public const int ByteSize = 7;
    }

    /// <summary> 8字节有符号整数(long) | 8-bytes signed integer(long) </summary>
    public struct LBInt64 {
        public const long MaxValue = long.MaxValue;
        public const long MinValue = long.MinValue;
        public const int ByteSize = 8;
    }

    /// <summary> 1字节无符号整数(byte) | 1-byte unsigned integer(byte) </summary>
    public struct LBUInt8 {
        public const byte MaxValue = byte.MaxValue;
        public const byte MinValue = byte.MinValue;
        public const int ByteSize = 1;
    }

    /// <summary> 2字节无符号整数(ushort) | 2-bytes unsigned integer(ushort) </summary>
    public struct LBUInt16 {
        public const ushort MaxValue = ushort.MaxValue;
        public const ushort MinValue = ushort.MinValue;
        public const int ByteSize = 2;
    }

    /// <summary> 3字节无符号整数(3/4 uint) | 3-bytes unsigned integer(3/4 uint) </summary>
    public struct LBUInt24 {
        public const uint MaxValue = 16777215;
        public const uint MinValue = 0;
        public const int ByteSize = 3;
    }

    /// <summary> 4字节无符号整数(uint) | 4-bytes unsigned integer(uint) </summary>
    public struct LBUInt32 {
        public const uint MaxValue = uint.MaxValue;
        public const uint MinValue = uint.MinValue;
        public const int ByteSize = 4;
    }

    /// <summary> 5字节无符号整数(5/8 ulong) | 5-bytes unsigned integer(5/8 ulong) </summary>
    public struct LBUInt40 {
        public const ulong MaxValue = 1099511627775L;
        public const ulong MinValue = 0L;
        public const int ByteSize = 5;
    }

    /// <summary> 6字节无符号整数(6/8 ulong) | 6-bytes unsigned integer(6/8 ulong) </summary>
    public struct LBUInt48 {
        public const ulong MaxValue = 281474976710655L;
        public const ulong MinValue = 0L;
        public const int ByteSize = 6;
    }

    /// <summary> 7字节无符号整数(7/8 ulong) | 7-bytes unsigned integer(7/8 ulong) </summary>
    public struct LBUInt56 {
        public const ulong MaxValue = 72057594037927935L;
        public const ulong MinValue = 0L;
        public const int ByteSize = 7;
    }

    /// <summary> 8字节无符号整数(ulong) | 8-bytes unsigned integer(ulong) </summary>
    public struct LBUInt64 {
        public const ulong MaxValue = ulong.MaxValue;
        public const ulong MinValue = ulong.MinValue;
        public const int ByteSize = 8;
    }
    #endregion

    #region Float
    /// <summary> 1字节无符号浮点数(取值范围:0F/255F ~ 255F/255F) | 1-byte unsigned float(value range:0F/255F ~ 255F/255F) </summary>
    public struct LBFloat8 {
        public const float MaxValue = 255F / 255F;
        public const float MinValue = 0F / 255F;
        public const int ByteSize = 1;
    }

    /// <summary> 2字节有符号浮点数(有效数字:3位) | 2-bytes signed float(significant digits:3)
    /// <para>[符号(1位) 指数(5位) 尾数(10位) 偏移(15)] [Sign(1bit) Exponent(5bits) Mantissa(10bits) Bias(15)]</para>
    /// </summary>
    public struct LBFloat16 {
        public const float MaxValue = 1.31E+5F;
        public const float MinValue = -1.31E+5F;
        public const int ByteSize = 2;
    }

    /// <summary> 3字节有符号浮点数(有效数字:5位) | 3-bytes signed float(significant digits:5)
    /// <para>[符号(1位) 指数(7位) 尾数(16位) 偏移(63)] [Sign(1bit) Exponent(7bits) Mantissa(16bits) Bias(63)]</para>
    /// </summary>
    public struct LBFloat24 {
        public const float MaxValue = 3.6893E+19F;
        public const float MinValue = -3.6893E+19F;
        public const int ByteSize = 3;
    }

    /// <summary> 4字节有符号浮点数(IEEE-754 有效数字:7位) | 4-bytes signed float(IEEE754 significant digits:7)
    /// <para>[符号(1位) 指数(8位) 尾数(23位) 偏移(127)] [Sign(1bit) Exponent(8bits) Mantissa(23bits) Bias(127)]</para>
    /// </summary>
    public struct LBFloat32 {
        public const float MaxValue = float.MaxValue;
        public const float MinValue = float.MinValue;
        public const int ByteSize = 4;
    }

    /// <summary> 8字节有符号浮点数(IEEE-754 有效数字:15位) | 8-bytes signed float(IEEE754 significant digits:15)
    /// <para>[符号(1位) 指数(11位) 尾数(52位) 偏移(1023)] [Sign(1bit) Exponent(11bits) Mantissa(52bits) Bias(1023)]</para>
    /// </summary>
    public struct LBFloat64 {
        public const double MaxValue = double.MaxValue;
        public const double MinValue = double.MinValue;
        public const int ByteSize = 8;
    }

    #endregion

    #region Variant Integer
    /// <summary> 1位+1~2字节变长有符号整数(short) | 1-bit + 1~2bytes variant-length signed integer(short) </summary>
    public struct LBVarInt16 {
        public const short MaxValue = short.MaxValue;
        public const short MinValue = short.MinValue;
        public const float MinByteSize = 1F / 8F + 1F;
        public const float MaxByteSize = 1F / 8F + 2F;
    }

    /// <summary> 2位+1~4字节变长有符号整数(int) | 2-bit + 1~4bytes variant-length signed integer(int) </summary>
    public struct LBVarInt32 {
        public const int MaxValue = int.MaxValue;
        public const int MinValue = int.MinValue;
        public const float MinByteSize = 2F / 8F + 1F;
        public const float MaxByteSize = 2F / 8F + 4F;
    }

    /// <summary> 3位+1~8字节变长有符号整数(int) | 3-bit + 1~8bytes variant-length signed integer(long) </summary>
    public struct LBVarInt64 {
        public const long MaxValue = long.MaxValue;
        public const long MinValue = long.MinValue;
        public const float MinByteSize = 3F / 8F + 1F;
        public const float MaxByteSize = 3F / 8F + 8F;
    }

    /// <summary> 1位+1~2字节变长无符号整数(ushort) | 1-bit + 1~2bytes variant-length unsigned integer(ushort) </summary>
    public struct LBVarUInt16 {
        public const ushort MaxValue = ushort.MaxValue;
        public const ushort MinValue = ushort.MinValue;
        public const float MinByteSize = 1F / 8F + 1F;
        public const float MaxByteSize = 1F / 8F + 2F;
    }

    /// <summary> 2位+1~4字节变长无符号整数(int) | 2-bit + 1~4bytes variant-length unsigned integer(uint) </summary>
    public struct LBVarUInt32 {
        public const uint MaxValue = uint.MaxValue;
        public const uint MinValue = uint.MinValue;
        public const float MinByteSize = 2F / 8F + 1F;
        public const float MaxByteSize = 2F / 8F + 4F;
    }

    /// <summary> 3位+1~8字节变长无符号整数(int) | 3-bit + 1~8bytes variant-length unsigned integer(ulong) </summary>
    public struct LBVarUInt64 {
        public const ulong MaxValue = ulong.MaxValue;
        public const ulong MinValue = ulong.MinValue;
        public const float MinByteSize = 3F / 8F + 1F;
        public const float MaxByteSize = 3F / 8F + 8F;
    }
    #endregion

    #region String
    /// <summary> 字符串(ASCII编码) | String(ASCII Encoding) </summary>
    public struct LBASCII {
        public const int ByteSize = 1;
    }

    /// <summary> 字符串(Unicode编码) | String(Unicode Encoding) </summary>
    public struct LBUnicode {
        public const int ByteSize = 2;
    }

    /// <summary> 字符串(UTF8编码) | String(UTF8 Encoding) </summary>
    public struct LBUTF8 {
        public const int MinByteSize = 1;
        public const int MaxByteSize = 4;
    }
    #endregion
}
