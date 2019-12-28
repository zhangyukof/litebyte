#region License
// MIT License
//
// Copyright(c) 2019 ZhangYu
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
// Purpose: Convert base type to bytes
// Author: ZhangYu
// CreateDate: 2019-08-13
// LastModifiedDate: 2019-12-27
#endregion
namespace LiteByte.Converters {

    using System;
    using System.Text;
    using LiteByte.Common;

    /// <summary>
    /// <para>BinaryWriter</para>
    /// <para>转换基本类型为字节数组 | Convert base type to bytes</para>
    /// </summary>
    public class LBWriter : IDisposable {

        #region Init
        private byte[] buffer;      // 字节数组缓冲区 | Byte[] buffer
        private int byteIndex;      // 当前的字节序号(指向字节数组中的哪一个字节) | Current byte index at byte[] buffer
        private int bitIndex = -1;  // 当前的位序号(指向字节数组中的哪一个字节)   | Current bit index at byte[] buffer
        private int bitLocation = 8;// 当前的位的位置(指向一个字节中的哪一位)     | Current bit location at one byte
        private static readonly int[] BitLocationValues = new int[] { 0x00, 0x01, 0x03, 0x07, 0x0F, 0x1F, 0x3F, 0x7F };
        private static readonly int[] Bit1LocationValues = new int[] { 0x01, 0x02, 0x04, 0x08, 0x10, 0x20, 0x40, 0x80 };
        private const int DefaultCapacity = 16; // 默认容量 | Default capacity
        private static readonly Encoding EncodingUTF8 = Encoding.UTF8;
        private static readonly Encoding EncodingUnicode = Encoding.Unicode;
        private static readonly Encoding EncodingASCII = Encoding.ASCII;
        #if BIGENDIAN
        private static readonly bool isLittleEndian /* = false */;
        #else
        private static readonly bool isLittleEndian = true;
        #endif

        public LBWriter() {
            buffer = new byte[DefaultCapacity];
        }

        public LBWriter(int capacity) {
            if (capacity < 1) capacity = 1;
            buffer = new byte[capacity];
        }

        private void RequireSize(int addSize) {
            int needSize = byteIndex + addSize;
            if (needSize > buffer.Length) {
                int newSize = buffer.Length * 2;
                if (newSize < needSize) newSize = needSize;
                Capacity = newSize;
            }
        }
        #endregion

        #region Bit
        public void WriteBit1(bool value) {
            if (bitLocation > 7) {
                RequireSize(1);
                bitIndex = byteIndex++;
                bitLocation = 1;
                buffer[bitIndex] = value ? (byte)1 : (byte)0;
            } else {
                if (value) buffer[bitIndex] = (byte)(buffer[bitIndex] | Bit1LocationValues[bitLocation]);
                bitLocation += 1;
            }
        }

        public void WriteBit2(byte value) {
            if (value > LBBit2.MaxValue) {
                string error = "value:" + value.ToString() + " out of range! Bit2 valid range:[0 ~ 3]";
                throw new ArgumentOutOfRangeException("value", error);
            }
            if (bitLocation > 7) {
                RequireSize(1);
                bitIndex = byteIndex++;
                bitLocation = 2;
                buffer[bitIndex] = value;
            } else if (bitLocation <= 6) {
                buffer[bitIndex] = (byte)(buffer[bitIndex] | value << bitLocation);
                bitLocation += 2;
            } else {
                // 计算过程 | Process
                // (bitLocation = 7)
                // int n1 = 8 - bitLocation = 8 - 7 = 1
                // int n2 = 2 - n1 = 2 - 1 = 1
                // int b1 = value & BitLocationValues[n1] = value & 1
                // int b2 = value >> n1 = value >> 1
                // bytes[bitIndex] = bytes[bitIndex] | b1 << bitLocation = bytes[bitIndex] | (value & 1) << 7
                // bytes[byteIndex] = b2 = value >> 1
                // bitLocation = bitLocation + 2 - 8 = bitLocation - 6 = 7 - 6 = 1
                RequireSize(1);
                if ((value & 1) != 0) buffer[bitIndex] = (byte)(buffer[bitIndex] | 0x80);
                buffer[byteIndex] = (byte)(value >> 1);
                bitIndex = byteIndex++;
                bitLocation = 1;
            }
        }

        public void WriteBit3(byte value) {
            if (value > LBBit3.MaxValue) {
                string error = "value:" + value.ToString() + " out of range! Bit3 valid range:[0 ~ 7]";
                throw new ArgumentOutOfRangeException("value", error);
            }
            if (bitLocation > 7) {
                RequireSize(1);
                bitIndex = byteIndex++;
                bitLocation = 3;
                buffer[bitIndex] = value;
            } else if (bitLocation <= 5) {
                buffer[bitIndex] = (byte)(buffer[bitIndex] | value << bitLocation);
                bitLocation += 3;
            } else {
                // 计算过程 | Process
                // (bitLocation = 6, 7)
                // int n1 = 8 - bitLocation
                // int n2 = 3 - n1 = 3 - (8 - bitLocation) = bitLocation - 5
                // int b1 = value & BitLocationValues[n1] = value & BitLocationValues[8 - bitLocation]
                // int b2 = value >> n1 = value >> 8 - bitLocation
                // bytes[bitIndex] = bytes[bitIndex] | b1 << bitLocation = bytes[bitIndex] | (value & BitLocationValues[8 - bitLocation]) << bitLocation
                // bytes[byteIndex] = b2 = value >> 8 - bitLocation
                // bitLocation = bitLocation + 3 - 8 = bitLocation - 5
                RequireSize(1);
                buffer[bitIndex] = (byte)(buffer[bitIndex] | (value & BitLocationValues[8 - bitLocation]) << bitLocation);
                buffer[byteIndex] = (byte)(value >> 8 - bitLocation);
                bitLocation = bitLocation - 5;
                bitIndex = byteIndex++;
            }
        }

        public void WriteBit4(byte value) {
            if (value > LBBit4.MaxValue) {
                string error = "value:" + value.ToString() + " out of range! Bit4 valid range:[0 ~ 15]";
                throw new ArgumentOutOfRangeException("value", error);
            }
            if (bitLocation > 7) {
                RequireSize(1);
                bitIndex = byteIndex++;
                bitLocation = 4;
                buffer[bitIndex] = value;
            } else if (bitLocation <= 4) {
                buffer[bitIndex] = (byte)(buffer[bitIndex] | value << bitLocation);
                bitLocation += 4;
            } else {
                // 计算过程(和WriteBit3相同) | Process(Same as WriteBit3)
                RequireSize(1);
                buffer[bitIndex] = (byte)(buffer[bitIndex] | (value & BitLocationValues[8 - bitLocation]) << bitLocation);
                buffer[byteIndex] = (byte)(value >> 8 - bitLocation);
                bitLocation = bitLocation - 4;
                bitIndex = byteIndex++;
            }
        }

        public void WriteBit5(byte value) {
            if (value > LBBit5.MaxValue) {
                string error = "value:" + value.ToString() + " out of range! Bit5 valid range:[0 ~ 31]";
                throw new ArgumentOutOfRangeException("value", error);
            }
            if (bitLocation > 7) {
                RequireSize(1);
                bitIndex = byteIndex++;
                bitLocation = 5;
                buffer[bitIndex] = value;
            } else if (bitLocation <= 3) {
                buffer[bitIndex] = (byte)(buffer[bitIndex] | value << bitLocation);
                bitLocation += 5;
            } else {
                // 计算过程(和WriteBit3相同) | Process(Same as WriteBit3)
                RequireSize(1);
                buffer[bitIndex] = (byte)(buffer[bitIndex] | (value & BitLocationValues[8 - bitLocation]) << bitLocation);
                buffer[byteIndex] = (byte)(value >> 8 - bitLocation);
                bitLocation = bitLocation - 3;
                bitIndex = byteIndex++;
            }
        }

        public void WriteBit6(byte value) {
            if (value > LBBit6.MaxValue) {
                string error = "value:" + value.ToString() + " out of range! Bit6 valid range:[0 ~ 63]";
                throw new ArgumentOutOfRangeException("value", error);
            }
            if (bitLocation > 7) {
                RequireSize(1);
                bitIndex = byteIndex++;
                bitLocation = 6;
                buffer[bitIndex] = value;
            } else if (bitLocation <= 2) {
                buffer[bitIndex] = (byte)(buffer[bitIndex] | value << bitLocation);
                bitLocation += 6;
            } else {
                // 计算过程(和WriteBit3相同) | Process(Same as WriteBit3)
                RequireSize(1);
                buffer[bitIndex] = (byte)(buffer[bitIndex] | (value & BitLocationValues[8 - bitLocation]) << bitLocation);
                buffer[byteIndex] = (byte)(value >> 8 - bitLocation);
                bitLocation = bitLocation - 2;
                bitIndex = byteIndex++;
            }
        }

        public void WriteBit7(byte value) {
            if (value > LBBit7.MaxValue) {
                string error = "value:" + value.ToString() + " out of range! Bit7 valid range:[0 ~ 127]";
                throw new ArgumentOutOfRangeException("value", error);
            }
            if (bitLocation > 7) {
                RequireSize(1);
                bitIndex = byteIndex++;
                bitLocation = 7;
                buffer[bitIndex] = value;
            } else if (bitLocation <= 1) {
                buffer[bitIndex] = (byte)(buffer[bitIndex] | value << bitLocation);
                bitLocation += 7;
            } else {
                // 计算过程(和WriteBit3相同) | Process(Same as WriteBit3)
                RequireSize(1);
                buffer[bitIndex] = (byte)(buffer[bitIndex] | (value & BitLocationValues[8 - bitLocation]) << bitLocation);
                buffer[byteIndex] = (byte)(value >> 8 - bitLocation);
                bitLocation = bitLocation - 1;
                bitIndex = byteIndex++;
            }
        }
        #endregion

        #region Integer
        public void WriteInt8(sbyte value) {
            RequireSize(1);
            buffer[byteIndex++] = (byte)value;
        }

        public void WriteInt16(short value) {
            RequireSize(2);
            buffer[byteIndex++] = (byte)value;
            buffer[byteIndex++] = (byte)(value >> 8);
        }

        public void WriteInt24(int value) {
            // 检查数值范围 | Check value range
            if (value < LBInt24.MinValue || value > LBInt24.MaxValue) {
                string error = "value:" + value.ToString() + " out of range! Int24 valid range:[-8388608 ~ 8388607]";
                throw new ArgumentOutOfRangeException("value", error);
            }
            RequireSize(3);
            buffer[byteIndex] = (byte)value;
            buffer[byteIndex + 1] = (byte)(value >> 8);
            buffer[byteIndex + 2] = (byte)(value >> 16);
            byteIndex += 3;
        }

        public void WriteInt32(int value) {
            RequireSize(4);
            buffer[byteIndex] = (byte)value;
            buffer[byteIndex + 1] = (byte)(value >> 8);
            buffer[byteIndex + 2] = (byte)(value >> 16);
            buffer[byteIndex + 3] = (byte)(value >> 24);
            byteIndex += 4;
        }

        public void WriteInt40(long value) {
            // 检查数值范围 | Check value range
            if (value < LBInt40.MinValue || value > LBInt40.MaxValue) {
                string error = "value:" + value.ToString() + " out of range! Int40 valid range:[-549755813888 ~ 549755813887]";
                throw new ArgumentOutOfRangeException("value", error);
            }
            RequireSize(5);
            buffer[byteIndex] = (byte)value;
            buffer[byteIndex + 1] = (byte)(value >> 8);
            buffer[byteIndex + 2] = (byte)(value >> 16);
            buffer[byteIndex + 3] = (byte)(value >> 24);
            buffer[byteIndex + 4] = (byte)(value >> 32);
            byteIndex += 5;
        }

        public void WriteInt48(long value) {
            // 检查数值范围 | Check value range
            if (value < LBInt48.MinValue || value > LBInt48.MaxValue) {
                string error = "value:" + value.ToString() + " out of range! Int48 valid range:[-140737488355328 ~ 140737488355327]";
                throw new ArgumentOutOfRangeException("value", error);
            }
            RequireSize(6);
            buffer[byteIndex] = (byte)value;
            buffer[byteIndex + 1] = (byte)(value >> 8);
            buffer[byteIndex + 2] = (byte)(value >> 16);
            buffer[byteIndex + 3] = (byte)(value >> 24);
            buffer[byteIndex + 4] = (byte)(value >> 32);
            buffer[byteIndex + 5] = (byte)(value >> 40);
            byteIndex += 6;
        }

        public void WriteInt56(long value) {
            // 检查数值范围 | Check value range
            if (value < LBInt56.MinValue || value > LBInt56.MaxValue) {
                string error = "value:" + value.ToString() + " out of range! Int56 valid range:[-36028797018963968 ~ 36028797018963967]";
                throw new ArgumentOutOfRangeException("value", error);
            }
            RequireSize(7);
            buffer[byteIndex] = (byte)value;
            buffer[byteIndex + 1] = (byte)(value >> 8);
            buffer[byteIndex + 2] = (byte)(value >> 16);
            buffer[byteIndex + 3] = (byte)(value >> 24);
            buffer[byteIndex + 4] = (byte)(value >> 32);
            buffer[byteIndex + 5] = (byte)(value >> 40);
            buffer[byteIndex + 6] = (byte)(value >> 48);
            byteIndex += 7;
        }

        public void WriteInt64(long value) {
            RequireSize(8);
            buffer[byteIndex] = (byte)value;
            buffer[byteIndex + 1] = (byte)(value >> 8);
            buffer[byteIndex + 2] = (byte)(value >> 16);
            buffer[byteIndex + 3] = (byte)(value >> 24);
            buffer[byteIndex + 4] = (byte)(value >> 32);
            buffer[byteIndex + 5] = (byte)(value >> 40);
            buffer[byteIndex + 6] = (byte)(value >> 48);
            buffer[byteIndex + 7] = (byte)(value >> 56);
            byteIndex += 8;
        }

        public void WriteUInt8(byte value) {
            RequireSize(1);
            buffer[byteIndex++] = value;
        }

        public void WriteUInt16(ushort value) {
            RequireSize(2);
            buffer[byteIndex++] = (byte)value;
            buffer[byteIndex++] = (byte)(value >> 8);
        }

        public void WriteUInt24(uint value) {
            // 检查数值范围 | Check value range
            if (value > LBUInt24.MaxValue) {
                string error = "value:" + value.ToString() + " out of range! UInt24 valid range:[0 ~ 16777215]";
                throw new ArgumentOutOfRangeException("value", error);
            }
            RequireSize(3);
            buffer[byteIndex] = (byte)value;
            buffer[byteIndex + 1] = (byte)(value >> 8);
            buffer[byteIndex + 2] = (byte)(value >> 16);
            byteIndex += 3;
        }

        public void WriteUInt32(uint value) {
            RequireSize(4);
            buffer[byteIndex] = (byte)value;
            buffer[byteIndex + 1] = (byte)(value >> 8);
            buffer[byteIndex + 2] = (byte)(value >> 16);
            buffer[byteIndex + 3] = (byte)(value >> 24);
            byteIndex += 4;
        }

        public void WriteUInt40(ulong value) {
            // 检查数值范围 | Check value range
            if (value > LBUInt40.MaxValue) {
                string error = "value:" + value.ToString() + " out of range! UInt40 valid range:[0 ~ 1099511627775]";
                throw new ArgumentOutOfRangeException("value", error);
            }
            RequireSize(5);
            buffer[byteIndex] = (byte)value;
            buffer[byteIndex + 1] = (byte)(value >> 8);
            buffer[byteIndex + 2] = (byte)(value >> 16);
            buffer[byteIndex + 3] = (byte)(value >> 24);
            buffer[byteIndex + 4] = (byte)(value >> 32);
            byteIndex += 5;
        }

        public void WriteUInt48(ulong value) {
            // 检查数值范围 | Check value range
            if (value > LBUInt48.MaxValue) {
                string error = "value:" + value.ToString() + " out of range! UInt48 valid range:[0 ~ 281474976710655]";
                throw new ArgumentOutOfRangeException("value", error);
            }
            RequireSize(6);
            buffer[byteIndex] = (byte)value;
            buffer[byteIndex + 1] = (byte)(value >> 8);
            buffer[byteIndex + 2] = (byte)(value >> 16);
            buffer[byteIndex + 3] = (byte)(value >> 24);
            buffer[byteIndex + 4] = (byte)(value >> 32);
            buffer[byteIndex + 5] = (byte)(value >> 40);
            byteIndex += 6;
        }

        public void WriteUInt56(ulong value) {
            // 检查数值范围 | Check value range
            if (value > LBUInt56.MaxValue) {
                string error = "value:" + value.ToString() + " out of range! UInt56 valid range:[0 ~ 72057594037927935]";
                throw new ArgumentOutOfRangeException("value", error);
            }
            RequireSize(7);
            buffer[byteIndex] = (byte)value;
            buffer[byteIndex + 1] = (byte)(value >> 8);
            buffer[byteIndex + 2] = (byte)(value >> 16);
            buffer[byteIndex + 3] = (byte)(value >> 24);
            buffer[byteIndex + 4] = (byte)(value >> 32);
            buffer[byteIndex + 5] = (byte)(value >> 40);
            buffer[byteIndex + 6] = (byte)(value >> 48);
            byteIndex += 7;
        }

        public void WriteUInt64(ulong value) {
            RequireSize(8);
            buffer[byteIndex] = (byte)value;
            buffer[byteIndex + 1] = (byte)(value >> 8);
            buffer[byteIndex + 2] = (byte)(value >> 16);
            buffer[byteIndex + 3] = (byte)(value >> 24);
            buffer[byteIndex + 4] = (byte)(value >> 32);
            buffer[byteIndex + 5] = (byte)(value >> 40);
            buffer[byteIndex + 6] = (byte)(value >> 48);
            buffer[byteIndex + 7] = (byte)(value >> 56);
            byteIndex += 8;
        }
        #endregion

        #region Float
        public void WriteFloat8(float value) {
            // 检查数值范围 | Check value range
            if (!IsFloat8(value)) {
                string error = "value:" + value.ToString() + " out of range! Float8 valid range:[0F ~ 1F (0/255F ~ 255/255F)]";
                throw new ArgumentOutOfRangeException("value", error);
            }
            RequireSize(1);
            buffer[byteIndex++] = (byte)(value * 255F);
        }

        public unsafe void WriteFloat16(float value) {
            // 检查数值范围 | Check value range
            if (value > LBFloat16.MaxValue || value < LBFloat16.MinValue) {
                string error = "value:" + value.ToString() + " out of range! Float16 valid range:[-1.31E+5F ~ 1.31E+5F]";
                throw new ArgumentOutOfRangeException("value", error);
            }

            // Float32(IEEE 754 32-bit) to Float16 (Custom 16-bit) 
            // [Sign(1bit) Exponent(8bits) Mantissa(23bits) Bias(127)] -> [Sign(1bit) Exponent(5bits) Mantissa(10bits) Bias(15)]
            // [S EEEEEEEE MMMMMMMM MMMMMMMM MMMMMMM] -> [S EEEEE MM MMMMMMMM]
            RequireSize(2);
            uint v = *(uint*)&value;
            uint sign = v >> 31;
            uint exponent = (v << 1 >> 24) - 112; // -112 = (-127 + 15)
            uint mantissa = (v & 0x7FFFFF) >> 13;
            // 最后一位是四舍五入位 | The last digit is rounded
            if ((v & 0x1000) != 0 && mantissa < 0x3FF) mantissa += 1;
            buffer[byteIndex++] = (byte)mantissa;
            buffer[byteIndex++] = (byte)(sign << 7 | exponent << 2 | mantissa >> 8);
        }

        public unsafe void WriteFloat24(float value) {
            // 检查数值范围 | Check value range
            if (value > LBFloat24.MaxValue || value < LBFloat24.MinValue) {
                string error = "value:" + value.ToString() + " out of range! Float24 valid range:[-3.6893E+19F ~ 3.6893E+19F]";
                throw new ArgumentOutOfRangeException("value", error);
            }

            // Float32(IEEE 754 32-bit) to Float24 (Custom 24-bit) 
            // [Sign(1bit) Exponent(8bits) Mantissa(23bits) Bias(127)] -> [Sign(1bit) Exponent(7bits) Mantissa(16bits) Bias(63)]
            // [S EEEEEEEE MMMMMMMM MMMMMMMM MMMMMMM] -> [S EEEEEEE MMMMMMMM MMMMMMMM]
            RequireSize(3);
            uint v = *(uint*)&value;
            uint sign = v >> 31;
            uint exponent = (v << 1 >> 24) - 64; // -64 = (-127 + 63)
            uint mantissa = (v & 0x7FFFFF) >> 7;
            // 最后一位是四舍五入位 | The last digit is rounded
            if ((v & 0x40) != 0 && mantissa < 0xFFFF) mantissa += 1;
            buffer[byteIndex] = (byte)mantissa;
            buffer[byteIndex + 1] = (byte)(mantissa >> 8);
            buffer[byteIndex + 2] = (byte)(sign << 7 | exponent);
            byteIndex += 3;
        }

        public unsafe void WriteFloat32(float value) {
            RequireSize(4);
            int v = *(int*)&value;
            buffer[byteIndex] = (byte)v;
            buffer[byteIndex + 1] = (byte)(v >> 8);
            buffer[byteIndex + 2] = (byte)(v >> 16);
            buffer[byteIndex + 3] = (byte)(v >> 24);
            byteIndex += 4;
        }

        public unsafe void WriteFloat64(double value) {
            long v = *(long*)&value;
            buffer[byteIndex] = (byte)v;
            buffer[byteIndex + 1] = (byte)(v >> 8);
            buffer[byteIndex + 2] = (byte)(v >> 16);
            buffer[byteIndex + 3] = (byte)(v >> 24);
            buffer[byteIndex + 4] = (byte)(v >> 32);
            buffer[byteIndex + 5] = (byte)(v >> 40);
            buffer[byteIndex + 6] = (byte)(v >> 48);
            buffer[byteIndex + 7] = (byte)(v >> 56);
            byteIndex += 8;
        }
        #endregion

        #region Variable Integer
        public void WriteVarInt16(short value) {
            // 根据数值范围 写入字节(1比特 + 1 ~ 2字节) | Write bytes (1bit + 1 ~ 2bytes) according to numeric range
            if (value <= LBInt8.MaxValue && value >= LBInt8.MinValue) {
                WriteBit1(false);
                WriteInt8((sbyte)value);
            } else {
                WriteBit1(true);
                WriteInt16(value);
            }
        }

        public void WriteVarInt32(int value) {
            // 根据数值范围 写入字节(2比特 + 1 ~ 4字节) | Write bytes (2bits + 1 ~ 4bytes) according to numeric range
            if (value <= LBInt8.MaxValue && value >= LBInt8.MinValue) {
                WriteBit2(0);
                WriteInt8((sbyte)value);
            } else if (value <= LBInt16.MaxValue && value >= LBInt16.MinValue) {
                WriteBit2(1);
                WriteInt16((short)value);
            } else if (value <= LBInt24.MaxValue && value >= LBInt24.MinValue) {
                WriteBit2(2);
                WriteInt24(value);
            } else {
                WriteBit2(3);
                WriteInt32(value);
            }
        }

        public void WriteVarInt64(long value) {
            // 根据数值范围 写入字节(3比特 + 1 ~ 8字节) | Write bytes (3bits + 1 ~ 8bytes) according to numeric range
            if (value <= LBInt8.MaxValue && value >= LBInt8.MinValue) {
                WriteBit3(0);
                WriteInt8((sbyte)value);
            } else if (value <= LBInt16.MaxValue && value >= LBInt16.MinValue) {
                WriteBit3(1);
                WriteInt16((short)value);
            } else if (value <= LBInt24.MaxValue && value >= LBInt24.MinValue) {
                WriteBit3(2);
                WriteInt24((int)value);
            } else if (value <= LBInt32.MaxValue && value >= LBInt32.MinValue) {
                WriteBit3(3);
                WriteInt32((int)value);
            } else if (value <= LBInt40.MaxValue && value >= LBInt40.MinValue) {
                WriteBit3(4);
                WriteInt40(value);
            } else if (value <= LBInt48.MaxValue && value >= LBInt48.MinValue) {
                WriteBit3(5);
                WriteInt48(value);
            } else if (value <= LBInt56.MaxValue && value >= LBInt56.MinValue) {
                WriteBit3(6);
                WriteInt56(value);
            } else {
                WriteBit3(7);
                WriteInt64(value);
            }
        }

        public void WriteVarUInt16(ushort value) {
            if (value <= LBUInt8.MaxValue) {
                WriteBit1(false);
                WriteUInt8((byte)value);
            } else {
                WriteBit1(true);
                WriteUInt16(value);
            }
        }

        public void WriteVarUInt32(uint value) {
            if (value <= LBUInt8.MaxValue) {
                WriteBit2(0);
                WriteUInt8((byte)value);
            } else if (value <= LBUInt16.MaxValue) {
                WriteBit2(1);
                WriteUInt16((ushort)value);
            } else if (value <= LBUInt24.MaxValue) {
                WriteBit2(2);
                WriteUInt24(value);
            } else {
                WriteBit2(3);
                WriteUInt32(value);
            }
        }

        public void WriteVarUInt64(ulong value) {
            if (value <= LBUInt8.MaxValue) {
                WriteBit3(0);
                WriteUInt8((byte)value);
            } else if (value <= LBUInt16.MaxValue) {
                WriteBit3(1);
                WriteUInt16((ushort)value);
            } else if (value <= LBUInt24.MaxValue) {
                WriteBit3(2);
                WriteUInt24((uint)value);
            } else if (value <= LBUInt32.MaxValue) {
                WriteBit3(3);
                WriteUInt32((uint)value);
            } else if (value <= LBUInt40.MaxValue) {
                WriteBit3(4);
                WriteUInt40(value);
            } else if (value <= LBUInt48.MaxValue) {
                WriteBit3(5);
                WriteUInt48(value);
            } else if (value <= LBUInt56.MaxValue) {
                WriteBit3(6);
                WriteUInt56(value);
            } else {
                WriteBit3(7);
                WriteUInt64(value);
            }
        }

        public void WriteVarLength(int value) {
            // 检查数值范围 | Check value range
            if (value < LBVarLength.MinValue || value > LBVarLength.MaxValue) {
                string error = "value:" + value.ToString() + " out of range! VarLength valid range:[-1 ~ 1073741822]";
                throw new ArgumentOutOfRangeException("value", error);
            }
            // 根据数值范围 写入字节(1 ~ 4字节) | Write bytes (1 ~ 4 bytes) according to numeric range
            value = value + 1;
            if (value < 64) {
                WriteUInt8((byte)(value << 2));
            } else if (value < 16384) {
                WriteUInt16((ushort)(1 | value << 2));
            } else if (value < 4194304) {
                WriteUInt24((uint)(2 | value << 2));
            } else {
                WriteUInt32((uint)(3 | value << 2));
            }
        }
        #endregion

        #region String
        public void WriteUTF8(string value) {
            if (!WriteValidStringLength(value)) return;
            int size = EncodingUTF8.GetByteCount(value);
            WriteVarLength(size);
            RequireSize(size);
            byteIndex += EncodingUTF8.GetBytes(value, 0, value.Length, buffer, byteIndex);
        }

        public void WriteUnicode(string value) {
            if (!WriteValidStringLength(value)) return;
            WriteVarLength(value.Length);
            RequireSize(value.Length * LBUnicode.ByteSize);
            byteIndex += EncodingUnicode.GetBytes(value, 0, value.Length, buffer, byteIndex);
        }

        public void WriteASCII(string value) {
            if (!WriteValidStringLength(value)) return;
            int size = value.Length;
            WriteVarLength(size);
            RequireSize(size);
            byteIndex += EncodingASCII.GetBytes(value, 0, size, buffer, byteIndex);
        }
        #endregion

        #region Bit Array
        public void WriteBit1Array(bool[] array) {
            if (!WriteValidArrayLength(array, LBBit1.ByteSize)) return;
            for (int i = 0; i < array.Length; i++) {
                WriteBit1(array[i]);
            }
        }

        public void WriteBit2Array(byte[] array) {
            if (!WriteValidArrayLength(array, LBBit2.ByteSize)) return;
            for (int i = 0; i < array.Length; i++) {
                WriteBit2(array[i]);
            }
        }

        public void WriteBit3Array(byte[] array) {
            if (!WriteValidArrayLength(array, LBBit3.ByteSize)) return;
            for (int i = 0; i < array.Length; i++) {
                WriteBit3(array[i]);
            }
        }

        public void WriteBit4Array(byte[] array) {
            if (!WriteValidArrayLength(array, LBBit4.ByteSize)) return;
            for (int i = 0; i < array.Length; i++) {
                WriteBit4(array[i]);
            }
        }

        public void WriteBit5Array(byte[] array) {
            if (!WriteValidArrayLength(array, LBBit5.ByteSize)) return;
            for (int i = 0; i < array.Length; i++) {
                WriteBit5(array[i]);
            }
        }

        public void WriteBit6Array(byte[] array) {
            if (!WriteValidArrayLength(array, LBBit6.ByteSize)) return;
            for (int i = 0; i < array.Length; i++) {
                WriteBit6(array[i]);
            }
        }

        public void WriteBit7Array(byte[] array) {
            if (!WriteValidArrayLength(array, LBBit7.ByteSize)) return;
            for (int i = 0; i < array.Length; i++) {
                WriteBit7(array[i]);
            }
        }
        #endregion

        #region Integer Array
        public void WriteInt8Array(sbyte[] array) {
            WriteMemoryContinuousArray(array, LBInt8.ByteSize);
        }

        public void WriteInt16Array(short[] array) {
            if (isLittleEndian) {
                WriteMemoryContinuousArray(array, LBInt16.ByteSize);
            } else {
                if (!WriteValidArrayLength(array, LBInt16.ByteSize)) return;
                for (int i = 0; i < array.Length; i++) {
                    WriteInt16(array[i]);
                }
            }
        }

        public void WriteInt24Array(int[] array) {
            if (!WriteValidArrayLength(array, LBInt24.ByteSize)) return;
            for (int i = 0; i < array.Length; i++) {
                WriteInt24(array[i]);
            }
        }

        public void WriteInt32Array(int[] array) {
            if (isLittleEndian) {
                WriteMemoryContinuousArray(array, LBInt32.ByteSize);
            } else {
                if (!WriteValidArrayLength(array, LBInt32.ByteSize)) return;
                for (int i = 0; i < array.Length; i++) {
                    WriteInt32(array[i]);
                }
            }
        }

        public void WriteInt40Array(long[] array) {
            if (!WriteValidArrayLength(array, LBInt40.ByteSize)) return;
            for (int i = 0; i < array.Length; i++) {
                WriteInt40(array[i]);
            }
        }

        public void WriteInt48Array(long[] array) {
            if (!WriteValidArrayLength(array, LBInt48.ByteSize)) return;
            for (int i = 0; i < array.Length; i++) {
                WriteInt48(array[i]);
            }
        }

        public void WriteInt56Array(long[] array) {
            if (!WriteValidArrayLength(array, LBInt56.ByteSize)) return;
            for (int i = 0; i < array.Length; i++) {
                WriteInt56(array[i]);
            }
        }

        public void WriteInt64Array(long[] array) {
            if (isLittleEndian) {
                WriteMemoryContinuousArray(array, LBInt64.ByteSize);
            } else {
                if (!WriteValidArrayLength(array, LBInt64.ByteSize)) return;
                for (int i = 0; i < array.Length; i++) {
                    WriteInt64(array[i]);
                }
            }
        }

        /// <summary> 写入字节数组 | Write byte[] </summary>
        public void WriteUInt8Array(byte[] array) {
            WriteMemoryContinuousArray(array, LBUInt8.ByteSize);
        }

        /// <summary> 写入字节数组 | Write byte[] </summary>
        /// <param name="source">Source Array</param>
        /// <param name="offset">Source Offset</param>
        /// <param name="length">Write Length</param>
        public void WriteUInt8Array(byte[] source, int offset, int length) {
            if (!WriteValidArrayLength(source, length)) return;
            RequireSize(length);
            Buffer.BlockCopy(source, offset, buffer, byteIndex, length);
            byteIndex += length;
        }

        public void WriteUInt16Array(ushort[] array) {
            if (isLittleEndian) {
                WriteMemoryContinuousArray(array, LBUInt16.ByteSize);
            } else {
                if (!WriteValidArrayLength(array, LBUInt16.ByteSize)) return;
                for (int i = 0; i < array.Length; i++) {
                    WriteUInt16(array[i]);
                }
            }
        }

        public void WriteUInt24Array(uint[] array) {
            if (!WriteValidArrayLength(array, LBUInt24.ByteSize)) return;
            for (int i = 0; i < array.Length; i++) {
                WriteUInt24(array[i]);
            }
        }

        public void WriteUInt32Array(uint[] array) {
            if (isLittleEndian) {
                WriteMemoryContinuousArray(array, LBUInt32.ByteSize);
            } else {
                if (!WriteValidArrayLength(array, LBUInt32.ByteSize)) return;
                for (int i = 0; i < array.Length; i++) {
                    WriteUInt32(array[i]);
                }
            }
        }

        public void WriteUInt40Array(ulong[] array) {
            if (!WriteValidArrayLength(array, LBUInt40.ByteSize)) return;
            for (int i = 0; i < array.Length; i++) {
                WriteUInt40(array[i]);
            }
        }

        public void WriteUInt48Array(ulong[] array) {
            if (!WriteValidArrayLength(array, LBUInt48.ByteSize)) return;
            for (int i = 0; i < array.Length; i++) {
                WriteUInt48(array[i]);
            }
        }

        public void WriteUInt56Array(ulong[] array) {
            if (!WriteValidArrayLength(array, LBUInt56.ByteSize)) return;
            for (int i = 0; i < array.Length; i++) {
                WriteUInt56(array[i]);
            }
        }

        public void WriteUInt64Array(ulong[] array) {
            if (isLittleEndian) {
                WriteMemoryContinuousArray(array, LBUInt64.ByteSize);
            } else {
                if (!WriteValidArrayLength(array, LBUInt64.ByteSize)) return;
                for (int i = 0; i < array.Length; i++) {
                    WriteUInt64(array[i]);
                }
            }
        }
        #endregion

        #region Float Array
        public void WriteFloat8Array(float[] array) {
            if (!WriteValidArrayLength(array, LBFloat8.ByteSize)) return;
            for (int i = 0; i < array.Length; i++) {
                WriteFloat8(array[i]);
            }
        }

        public void WriteFloat16Array(float[] array) {
            if (!WriteValidArrayLength(array, LBFloat16.ByteSize)) return;
            for (int i = 0; i < array.Length; i++) {
                WriteFloat16(array[i]);
            }
        }

        public void WriteFloat24Array(float[] array) {
            if (!WriteValidArrayLength(array, LBFloat24.ByteSize)) return;
            for (int i = 0; i < array.Length; i++) {
                WriteFloat24(array[i]);
            }
        }

        public void WriteFloat32Array(float[] array) {
            if (isLittleEndian) {
                WriteMemoryContinuousArray(array, LBFloat32.ByteSize);
            } else {
                if (!WriteValidArrayLength(array, LBFloat32.ByteSize)) return;
                for (int i = 0; i < array.Length; i++) {
                    WriteFloat32(array[i]);
                }
            }
        }

        public void WriteFloat64Array(double[] array) {
            if (isLittleEndian) {
                WriteMemoryContinuousArray(array, LBFloat64.ByteSize);
            } else {
                if (!WriteValidArrayLength(array, LBFloat64.ByteSize)) return;
                for (int i = 0; i < array.Length; i++) {
                    WriteFloat64(array[i]);
                }
            }
        }
        #endregion

        #region Variable Integer Array
        public void WriteVarInt16Array(short[] array) {
            if (!WriteValidArrayLength(array, LBVarInt16.MinByteSize)) return;
            for (int i = 0; i < array.Length; i++) {
                WriteVarInt16(array[i]);
            }
        }

        public void WriteVarInt32Array(int[] array) {
            if (!WriteValidArrayLength(array, LBVarInt32.MinByteSize)) return;
            for (int i = 0; i < array.Length; i++) {
                WriteVarInt32(array[i]);
            }
        }

        public void WriteVarInt64Array(long[] array) {
            if (!WriteValidArrayLength(array, LBVarInt64.MinByteSize)) return;
            for (int i = 0; i < array.Length; i++) {
                WriteVarInt64(array[i]);
            }
        }

        public void WriteVarLengthArray(int[] array) {
            if (!WriteValidArrayLength(array, LBVarLength.MinByteSize)) return;
            for (int i = 0; i < array.Length; i++) {
                WriteVarLength(array[i]);
            }
        }

        public void WriteVarUInt16Array(ushort[] array) {
            if (!WriteValidArrayLength(array, LBVarUInt16.MinByteSize)) return;
            for (int i = 0; i < array.Length; i++) {
                WriteVarUInt16(array[i]);
            }
        }

        public void WriteVarUInt32Array(uint[] array) {
            if (!WriteValidArrayLength(array, LBVarUInt32.MinByteSize)) return;
            for (int i = 0; i < array.Length; i++) {
                WriteVarUInt32(array[i]);
            }
        }

        public void WriteVarUInt64Array(ulong[] array) {
            if (!WriteValidArrayLength(array, LBVarUInt64.MinByteSize)) return;
            for (int i = 0; i < array.Length; i++) {
                WriteVarUInt64(array[i]);
            }
        }
        #endregion

        #region String Array
        public void WriteUTF8Array(string[] array) {
            if (!WriteValidArrayLength(array, LBVarLength.MinByteSize)) return;
            for (int i = 0; i < array.Length; i++) {
                WriteUTF8(array[i]);
            }
        }

        public void WriteUnicodeArray(string[] array) {
            if (!WriteValidArrayLength(array, LBVarLength.MinByteSize)) return;
            for (int i = 0; i < array.Length; i++) {
                WriteUnicode(array[i]);
            }
        }

        public void WriteASCIIArray(string[] array) {
            if (!WriteValidArrayLength(array, LBVarLength.MinByteSize)) return;
            for (int i = 0; i < array.Length; i++) {
                WriteASCII(array[i]);
            }
        }
        #endregion

        #region Tools
        private static bool IsFloat8(float value) {
            if (value > LBFloat8.MaxValue || value < LBFloat8.MinValue) return false;
            value *= 255F;
            return value % 1F == 0F;
        }

        /// <summary> 写入有效的字符串长度 | Write valid string length </summary>
        private bool WriteValidStringLength(string value) {
            if (value == null) {
                WriteVarLength(-1);
                return false;
            } else if (value.Length == 0) {
                WriteVarLength(0);
                return false;
            } else {
                return true;
            }
        }

        /// <summary> 写入有效的数组长度 | Write valid array length </summary>
        /// <param name="array">数组 | Array</param>
        /// <param name="typeSize"> 元素类型字节大小 | byte size of element type</param>
        /// <returns> 是否写入了有效的数组长度(> 0) | If write a valid array length (> 0)</returns>
        private bool WriteValidArrayLength(Array array, float typeSize) {
            if (array == null) {
                WriteVarLength(-1);
                return false;
            } else if (array.Length == 0) {
                WriteVarLength(0);
                return false;
            } else {
                WriteVarLength(array.Length);
                RequireSize((int)(array.Length * typeSize));
                return true;
            }
        }

        /// <summary> 写入内存连续的数组 | Write an array with memory continuous </summary>
        /// <param name="array">数组 | Array</param>
        /// <param name="typeSize"> 元素类型字节大小 | byte size of element type</param>
        private void WriteMemoryContinuousArray(Array array, int typeSize) {
            if (array == null) {
                WriteVarLength(-1);
            } else if (array.Length == 0) {
                WriteVarLength(0);
            } else {
                WriteVarLength(array.Length);
                int totalSize = array.Length * typeSize;
                RequireSize(totalSize);
                Buffer.BlockCopy(array, 0, buffer, byteIndex, totalSize);
                byteIndex += totalSize;
            }
        }
        #endregion

        #region API
        /// <summary> 把已写入的数据转换为字节数组 | Convert written data to byte array </summary>
        public byte[] ToBytes() {
            byte[] bytes = new byte[byteIndex];
            Buffer.BlockCopy(buffer, 0, bytes, 0, byteIndex);
            return bytes;
        }

        /// <summary> 擦除已写入的数据 以便重新使用 | Erase written data for reuse </summary>
        public void Erase() {
            Array.Clear(buffer, 0, byteIndex);
            Position = 0;
        }

        /// <summary> 字节数组容量 | Byte array capacity </summary>
        public int Capacity {
            get { return buffer.Length; }
            set {
                if (value < 0) throw new ArgumentOutOfRangeException("Capacity", "Capacity can't be negtive!");
                if (value > buffer.Length) {
                    byte[] newBuffer = new byte[value];
                    Buffer.BlockCopy(buffer, 0, newBuffer, 0, buffer.Length);
                    buffer = newBuffer;
                } else if (value < buffer.Length) {
                    Dispose();
                    if (value < 1) value = 1;
                    buffer = new byte[value];
                }
            }
        }

        /// <summary> 已写入数据的字节大小 | The byte size of the written data </summary>
        public int Length {
            get { return byteIndex; }
        }

        /// <summary> 当前写入的字节位置 | The byte location of the current write </summary>
        public int Position {
            get { return byteIndex; }
            set {
                if (value < 0) throw new ArgumentNullException("Position", "Position can't be negtive!");
                byteIndex = value;
                bitIndex = -1;
                bitLocation = 8;
            }
        }

        /// <summary> 释放并重置所有数据 | Release and reset all datas </summary>
        public void Dispose() {
            Position = 0;
            buffer = null;
        }
        #endregion

    }

}
