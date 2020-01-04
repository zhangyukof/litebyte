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
// Purpose: Parse bytes to basetype
// Author: ZhangYu
// CreateDate: 2019-08-13
// LastModifiedDate: 2019-12-27
#endregion
namespace LiteByte.Converters {

    using System;
    using System.Text;
    using LiteByte.Common;

    /// <summary>
    /// <para>BinaryReader</para>
    /// <para>解析字节为基本类型 | Parse bytes to basetype</para>
    /// </summary>
    public class LBReader : IDisposable {

        #region Init
        private byte[] bytes;
        private int byteIndex;
        private int bitIndex = -1;
        private int bitLocation = 8;
        private static readonly int[] BitLocationValues = new int[] { 0x00, 0x01, 0x03, 0x07, 0x0F, 0x1F, 0x3F, 0x7F };
        private static readonly int[] Bit1LocationValues = new int[] { 0x01, 0x02, 0x04, 0x08, 0x10, 0x20, 0x40, 0x80 };
        #if BIGENDIAN
        private static readonly bool isLittleEndian /* = false */;
        #else
        private static readonly bool isLittleEndian = true;
        #endif

        public LBReader() {}

        public LBReader(byte[] bytes) {
            this.bytes = bytes;
        }
        #endregion

        #region Bit
        public bool ReadBit1() {
            if (bitLocation > 7) {
                bitIndex = byteIndex++;
                bitLocation = 1;
                return (bytes[bitIndex] & 1) != 0;
            } else {
                return (bytes[bitIndex] & Bit1LocationValues[bitLocation++]) != 0;
            }
        }

        public byte ReadBit2() {
            if (bitLocation > 7) {
                bitIndex = byteIndex++;
                bitLocation = 2;
                return (byte)(bytes[bitIndex] & 0x03);
            } else if (bitLocation <= 6) {
                int value = (bytes[bitIndex] >> bitLocation) & 0x03;
                bitLocation += 2;
                return (byte)value;
            } else {
                // 计算过程 | Process
                // (bitLocation = 7)
                // byte b1 = bytes[bitIndex]
                // byte b2 = bytes[byteIndex]
                // int  n1 = 8 - bitLocation = 8 - 7 = 1
                // int  n2 = 2 - n1 = 2 - 1 = 1
                // int  v1 = b1 >> bitLocation = bytes[bitIndex] >> 7
                // int  v2 = (b2 & Bit3LocationValues[n2]) << n1 = (bytes[byteIndex] & Bit2LocationValues[1]) << 1 = (bytes[byteIndex] & 1) << 1
                // int  v  = v1 | v2 << n1 = bytes[bitIndex] >> 7 | (bytes[byteIndex] & 1) << 1
                // bitLocation = bitLocation + 2 - 8 = bitLocation - 6 = 7 - 6 = 1
                int value = bytes[bitIndex] >> 7 | (bytes[byteIndex] & 1) << 1;
                bitIndex = byteIndex++;
                bitLocation = 1;
                return (byte)value;
            }
        }

        public byte ReadBit3() {
            if (bitLocation > 7) {
                bitIndex = byteIndex++;
                bitLocation = 3;
                return (byte)(bytes[bitIndex] & 0x07);
            } else if (bitLocation <= 5) {
                int value = (bytes[bitIndex] >> bitLocation) & 0x07;
                bitLocation += 3;
                return (byte)value;
            } else {
                // 计算过程 | Process
                // (bitLocation = 6, 7)
                // byte b1 = bytes[bitIndex]
                // byte b2 = bytes[byteIndex]
                // int  n1 = 8 - bitLocation
                // int  n2 = 3 - n1 = 3 - (8 - bitLocation) = bitLocation - 5
                // int  v1 = b1 >> bitLocation = bytes[bitIndex] >> bitLocation
                // int  v2 = (b2 & Bit3LocationValues[n2]) << n1 = (bytes[byteIndex] & Bit3LocationValues[bitLocation - 5]) << (8 - bitLocation)
                // int  v  = v1 | v2 << n1 = bytes[bitIndex] >> bitLocation | (bytes[byteIndex] & Bit3LocationValues[bitLocation - 5]) << (8 - bitLocation)
                // bitLocation = bitLocation + 3 - 8 = bitLocation - 5
                int value = bytes[bitIndex] >> bitLocation | (bytes[byteIndex] & BitLocationValues[bitLocation - 5]) << 8 - bitLocation;
                bitLocation -= 5;
                bitIndex = byteIndex++;
                return (byte)value;
            }
        }

        public byte ReadBit4() {
            if (bitLocation > 7) {
                bitIndex = byteIndex++;
                bitLocation = 4;
                return (byte)(bytes[bitIndex] & 0x0F);
            } else if (bitLocation <= 4) {
                int value = (bytes[bitIndex] >> bitLocation) & 0x0F;
                bitLocation += 4;
                return (byte)value;
            } else {
                // 计算过程(和ReadBit3类似) | Process(Same as ReadBit3)
                int value = bytes[bitIndex] >> bitLocation | (bytes[byteIndex] & BitLocationValues[bitLocation - 4]) << 8 - bitLocation;
                bitLocation -= 4;
                bitIndex = byteIndex++;
                return (byte)value;
            }
        }

        public byte ReadBit5() {
            if (bitLocation > 7) {
                bitIndex = byteIndex++;
                bitLocation = 5;
                return (byte)(bytes[bitIndex] & 0x1F);
            } else if (bitLocation <= 3) {
                int value = (bytes[bitIndex] >> bitLocation) & 0x1F;
                bitLocation += 5;
                return (byte)value;
            } else {
                // 计算过程(和ReadBit3类似) | Process(Same as ReadBit3)
                int value = bytes[bitIndex] >> bitLocation | (bytes[byteIndex] & BitLocationValues[bitLocation - 3]) << 8 - bitLocation;
                bitLocation -= 3;
                bitIndex = byteIndex++;
                return (byte)value;
            }
        }

        public byte ReadBit6() {
            if (bitLocation > 7) {
                bitIndex = byteIndex++;
                bitLocation = 6;
                return (byte)(bytes[bitIndex] & 0x3F);
            } else if (bitLocation <= 2) {
                int value = (bytes[bitIndex] >> bitLocation) & 0x3F;
                bitLocation += 6;
                return (byte)value;
            } else {
                // 计算过程(和ReadBit3类似) | Process(Same as ReadBit3)
                int value = bytes[bitIndex] >> bitLocation | (bytes[byteIndex] & BitLocationValues[bitLocation - 2]) << 8 - bitLocation;
                bitLocation -= 2;
                bitIndex = byteIndex++;
                return (byte)value;
            }
        }

        public byte ReadBit7() {
            if (bitLocation > 7) {
                bitIndex = byteIndex++;
                bitLocation = 7;
                return (byte)(bytes[bitIndex] & 0x7F);
            } else if (bitLocation <= 1) {
                int value = bytes[bitIndex] >> 1;
                bitLocation += 7;
                return (byte)value;
            } else {
                // 计算过程(和ReadBit3类似) | Process(Same as ReadBit3)
                int value = bytes[bitIndex] >> bitLocation | (bytes[byteIndex] & BitLocationValues[bitLocation - 1]) << 8 - bitLocation;
                bitLocation -= 1;
                bitIndex = byteIndex++;
                return (byte)value;
            }
        }
        #endregion

        #region Integer
        public sbyte ReadInt8() {
            return (sbyte)bytes[byteIndex++];
        }

        public short ReadInt16() {
            return (short)(bytes[byteIndex++] | bytes[byteIndex++] << 8);
        }

        public int ReadInt24() {
            if (bytes[byteIndex + 2] >> 7 == 0) {
                return bytes[byteIndex++] | bytes[byteIndex++] << 8 | bytes[byteIndex++] << 16;
            } else {
                // 如果是负数 补充缺少的位 0xFF000000 | If it's a negative number fill in the missing bits
                return bytes[byteIndex++] | bytes[byteIndex++] << 8 | bytes[byteIndex++] << 16 | -16777216;
            }
        }

        public int ReadInt32() {
            return bytes[byteIndex++] | bytes[byteIndex++] << 8 | bytes[byteIndex++] << 16 | bytes[byteIndex++] << 24;
        }

        public long ReadInt40() {
            int low = bytes[byteIndex++] | bytes[byteIndex++] << 8 | bytes[byteIndex++] << 16 | bytes[byteIndex++] << 24;
            int high = bytes[byteIndex++];
            // 如果是负数 补充缺少的位 0xFFFFFF00 | If it's a negative number fill in the missing bits
            if (high >> 7 == 1) high = high | -256;
            return (uint)low | (long)high << 32;
        }

        public long ReadInt48() {
            int low = bytes[byteIndex++] | bytes[byteIndex++] << 8 | bytes[byteIndex++] << 16 | bytes[byteIndex++] << 24;
            int high = bytes[byteIndex++] | bytes[byteIndex++] << 8;
            // 如果是负数 补充缺少的位 0xFFFF0000 | If it's a negative number fill in the missing bits
            if (high >> 15 == 1) high = high | -65536;
            return (uint)low | (long)high << 32;
        }

        public long ReadInt56() {
            int low = bytes[byteIndex++] | bytes[byteIndex++] << 8 | bytes[byteIndex++] << 16 | bytes[byteIndex++] << 24;
            int high = bytes[byteIndex++] | bytes[byteIndex++] << 8 | bytes[byteIndex++] << 16;
            // 如果是负数 补充缺少的位 0xFF000000 | If it's a negative number fill in the missing bits
            if (high >> 23 == 1) high = high | -16777216;
            return (uint)low | (long)high << 32;
        }

        public long ReadInt64() {
            int low = bytes[byteIndex++] | bytes[byteIndex++] << 8 | bytes[byteIndex++] << 16 | bytes[byteIndex++] << 24;
            int high = bytes[byteIndex++] | bytes[byteIndex++] << 8 | bytes[byteIndex++] << 16 | bytes[byteIndex++] << 24;
            return (uint)low | (long)high << 32;
        }

        public byte ReadUInt8() {
            return bytes[byteIndex++];
        }

        public ushort ReadUInt16() {
            return (ushort)(bytes[byteIndex++] | bytes[byteIndex++] << 8);
        }

        public uint ReadUInt24() {
            return (uint)(bytes[byteIndex++] | bytes[byteIndex++] << 8 | bytes[byteIndex++] << 16);
        }

        public uint ReadUInt32() {
            return (uint)(bytes[byteIndex++] | bytes[byteIndex++] << 8 | bytes[byteIndex++] << 16 | bytes[byteIndex++] << 24);
        }

        public ulong ReadUInt40() {
            int low = bytes[byteIndex++] | bytes[byteIndex++] << 8 | bytes[byteIndex++] << 16 | bytes[byteIndex++] << 24;
            int high = bytes[byteIndex++];
            return (uint)low | (ulong)high << 32;
        }

        public ulong ReadUInt48() {
            int low = bytes[byteIndex++] | bytes[byteIndex++] << 8 | bytes[byteIndex++] << 16 | bytes[byteIndex++] << 24;
            int high = bytes[byteIndex++] | bytes[byteIndex++] << 8;
            return (uint)low | (ulong)high << 32;
        }

        public ulong ReadUInt56() {
            int low = bytes[byteIndex++] | bytes[byteIndex++] << 8 | bytes[byteIndex++] << 16 | bytes[byteIndex++] << 24;
            int high = bytes[byteIndex++] | bytes[byteIndex++] << 8 | bytes[byteIndex++] << 16;
            return (uint)low | (ulong)high << 32;
        }

        public ulong ReadUInt64() {
            int low = bytes[byteIndex++] | bytes[byteIndex++] << 8 | bytes[byteIndex++] << 16 | bytes[byteIndex++] << 24;
            int high = bytes[byteIndex++] | bytes[byteIndex++] << 8 | bytes[byteIndex++] << 16 | bytes[byteIndex++] << 24;
            return (uint)low | (ulong)high << 32;
        }
        #endregion

        #region Float
        public float ReadFloat8() {
            return bytes[byteIndex++] / 255F;
        }

        public unsafe float ReadFloat16() {
            // Float16 (Custom 16-bit) to Float32(IEEE 754 32-bit)
            // [Sign(1bit) Exponent(5bits) Mantissa(10bits) Bias(15)] -> [Sign(1bit) Exponent(8bits) Mantissa(23bits) Bias(127)]
            // [S EEEEE MM MMMMMMMM] -> [S EEEEEEEE MMMMMMMM MMMMMMMM MMMMMMM]
            byte sem = bytes[byteIndex + 1];
            int v = (sem & 0x80) << 24 | ((sem & 0x7C) >> 2) + 112 << 23 | (sem & 0x3) << 21 | bytes[byteIndex] << 13;
            float value = *(float*)&v;
            value = RoundSignificantDigits(value, 3);
            byteIndex += 2;
            return value;
        }

        public unsafe float ReadFloat24() {
            // Float24 (Custom 24-bit) to Float32(IEEE 754 32-bit)
            // [Sign(1bit) Exponent(7bits) Mantissa(16bits) Bias(63)] -> [Sign(1bit) Exponent(8bits) Mantissa(23bits) Bias(127)]
            // [S EEEEEEE MMMMMMMM MMMMMMMM] -> [S EEEEEEEE MMMMMMMM MMMMMMMM MMMMMMM]
            byte se = bytes[byteIndex + 2];
            int v = (se & 0x80) << 24 | ((se & 0x7F) + 64) << 23 | bytes[byteIndex + 1] << 15 | bytes[byteIndex] << 7;
            float value = *(float*)&v;
            value = RoundSignificantDigits(value, 5);
            byteIndex += 3;
            return value;
        }

        public unsafe float ReadFloat32() {
            int v = ReadInt32();
            return *(float*)&v;
        }

        public unsafe double ReadFloat64() {
            long v = ReadInt64();
            return *(double*)&v;
        }

        private static float RoundSignificantDigits(float f, int n) {
            if (f == 0f) return 0f;
            double num = f;
            int power = n - (int)Math.Ceiling(Math.Log10(num < 0.0 ? -num : num));
            double magnitude = Math.Pow(10, power);
            long shifted = (long)Math.Round(num * magnitude);
            return (float)(shifted / magnitude);
        }
        #endregion

        #region Variable Integer
        public short ReadVarInt16() {
            if (ReadBit1()) return ReadInt16();
            return ReadInt8();
        }

        public int ReadVarInt32() {
            switch (ReadBit2()) {
                case 0: return ReadInt8();
                case 1: return ReadInt16();
                case 2: return ReadInt24();
                case 3: return ReadInt32();
                default: throw new ArgumentOutOfRangeException("ByteSize", "ReadVarInt32() byte size error!");
            }
        }

        public long ReadVarInt64() {
            switch (ReadBit3()) {
                case 0: return ReadInt8();
                case 1: return ReadInt16();
                case 2: return ReadInt24();
                case 3: return ReadInt32();
                case 4: return ReadInt40();
                case 5: return ReadInt48();
                case 6: return ReadInt56();
                case 7: return ReadInt64();
                default: throw new ArgumentOutOfRangeException("ByteSize", "ReadVarInt64() byte size error!");
            }
        }

        public ushort ReadVarUInt16() {
            if (ReadBit1()) return ReadUInt16();
            return ReadUInt8();
        }

        public uint ReadVarUInt32() {
            switch (ReadBit2()) {
                case 0: return ReadUInt8();
                case 1: return ReadUInt16();
                case 2: return ReadUInt24();
                case 3: return ReadUInt32();
                default: throw new ArgumentOutOfRangeException("ByteSize", "ReadVarUInt32() byte size error!");
            }
        }

        public ulong ReadVarUInt64() {
            switch (ReadBit3()) {
                case 0: return ReadUInt8();
                case 1: return ReadUInt16();
                case 2: return ReadUInt24();
                case 3: return ReadUInt32();
                case 4: return ReadUInt40();
                case 5: return ReadUInt48();
                case 6: return ReadUInt56();
                case 7: return ReadUInt64();
                default: throw new ArgumentOutOfRangeException("ByteSize", "ReadVarUInt64() byte size error!");
            }
        }

        public int ReadVarLength() {
            int size = bytes[byteIndex] & 3;
            switch (size) {
                case 0: return (bytes[byteIndex++] >> 2) - 1;
                case 1: return (bytes[byteIndex++] >> 2 | bytes[byteIndex++] << 6) - 1;
                case 2: return (bytes[byteIndex++] >> 2 | bytes[byteIndex++] << 6 | bytes[byteIndex++] << 14) - 1;
                default: return (bytes[byteIndex++] >> 2 | bytes[byteIndex++] << 6 | bytes[byteIndex++] << 14 | bytes[byteIndex++] << 22) - 1;
            }
        }
        #endregion

        #region String
        public string ReadUTF8() {
            int length = ReadVarLength();
            if (length == -1) return null;
            if (length == 0) return string.Empty;
            string value = Encoding.UTF8.GetString(bytes, byteIndex, length);
            byteIndex += length;
            return value;
        }

        public string ReadUnicode() {
            int length = ReadVarLength();
            if (length == -1) return null;
            if (length == 0) return string.Empty;
            length = length * 2;
            string value = Encoding.Unicode.GetString(bytes, byteIndex, length);
            byteIndex += length;
            return value;
        }

        public string ReadASCII() {
            int length = ReadVarLength();
            if (length == -1) return null;
            if (length == 0) return string.Empty;
            string value = Encoding.ASCII.GetString(bytes, byteIndex, length);
            byteIndex += length;
            return value;
        }
        #endregion

        #region Bit Array
        public bool[] ReadBit1Array() {
            int length = ReadVarLength();
            if (length == -1) return null;
            if (length == 0) return new bool[0];
            bool[] array = new bool[length];
            for (int i = 0; i < length; i++) {
                array[i] = ReadBit1();
            }
            return array;
        }

        public byte[] ReadBit2Array() {
            int length = ReadVarLength();
            if (length == -1) return null;
            if (length == 0) return new byte[0];
            byte[] array = new byte[length];
            for (int i = 0; i < length; i++) {
                array[i] = ReadBit2();
            }
            return array;
        }

        public byte[] ReadBit3Array() {
            int length = ReadVarLength();
            if (length == -1) return null;
            if (length == 0) return new byte[0];
            byte[] array = new byte[length];
            for (int i = 0; i < length; i++) {
                array[i] = ReadBit3();
            }
            return array;
        }

        public byte[] ReadBit4Array() {
            int length = ReadVarLength();
            if (length == -1) return null;
            if (length == 0) return new byte[0];
            byte[] array = new byte[length];
            for (int i = 0; i < length; i++) {
                array[i] = ReadBit4();
            }
            return array;
        }

        public byte[] ReadBit5Array() {
            int length = ReadVarLength();
            if (length == -1) return null;
            if (length == 0) return new byte[0];
            byte[] array = new byte[length];
            for (int i = 0; i < length; i++) {
                array[i] = ReadBit5();
            }
            return array;
        }

        public byte[] ReadBit6Array() {
            int length = ReadVarLength();
            if (length == -1) return null;
            if (length == 0) return new byte[0];
            byte[] array = new byte[length];
            for (int i = 0; i < length; i++) {
                array[i] = ReadBit6();
            }
            return array;
        }

        public byte[] ReadBit7Array() {
            int length = ReadVarLength();
            if (length == -1) return null;
            if (length == 0) return new byte[0];
            byte[] array = new byte[length];
            for (int i = 0; i < length; i++) {
                array[i] = ReadBit7();
            }
            return array;
        }
        #endregion

        #region Integer Array
        public sbyte[] ReadInt8Array() {
            int length = ReadVarLength();
            if (length == -1) return null;
            if (length == 0) return new sbyte[0];
            sbyte[] array = new sbyte[length];
            Buffer.BlockCopy(bytes, byteIndex, array, 0, length);
            byteIndex += length;
            return array;
        }

        public short[] ReadInt16Array() {
            int length = ReadVarLength();
            if (length == -1) return null;
            if (length == 0) return new short[0];
            short[] array = new short[length];
            int size = length * LBInt16.ByteSize;
            if (isLittleEndian) {
                Buffer.BlockCopy(bytes, byteIndex, array, 0, size);
            } else {
                for (int i = 0; i < length; i++) {
                    array[i] = ReadInt16();
                }
            }
            byteIndex += size;
            return array;
        }

        public int[] ReadInt24Array() {
            int length = ReadVarLength();
            if (length == -1) return null;
            if (length == 0) return new int[0];
            int[] array = new int[length];
            for (int i = 0; i < length; i++) {
                array[i] = ReadInt24();
            }
            return array;
        }

        public int[] ReadInt32Array() {
            int length = ReadVarLength();
            if (length == -1) return null;
            if (length == 0) return new int[0];
            int[] array = new int[length];
            int size = length * LBInt32.ByteSize;
            if (isLittleEndian) {
                Buffer.BlockCopy(bytes, byteIndex, array, 0, size);
            } else {
                for (int i = 0; i < length; i++) {
                    array[i] = ReadInt32();
                }
            }
            byteIndex += size;
            return array;
        }

        public long[] ReadInt40Array() {
            int length = ReadVarLength();
            if (length == -1) return null;
            if (length == 0) return new long[0];
            long[] array = new long[length];
            for (int i = 0; i < length; i++) {
                array[i] = ReadInt40();
            }
            return array;
        }

        public long[] ReadInt48Array() {
            int length = ReadVarLength();
            if (length == -1) return null;
            if (length == 0) return new long[0];
            long[] array = new long[length];
            for (int i = 0; i < length; i++) {
                array[i] = ReadInt48();
            }
            return array;
        }

        public long[] ReadInt56Array() {
            int length = ReadVarLength();
            if (length == -1) return null;
            if (length == 0) return new long[0];
            long[] array = new long[length];
            for (int i = 0; i < length; i++) {
                array[i] = ReadInt56();
            }
            return array;
        }

        public long[] ReadInt64Array() {
            int length = ReadVarLength();
            if (length == -1) return null;
            if (length == 0) return new long[0];
            long[] array = new long[length];
            int size = length * LBInt64.ByteSize;
            if (isLittleEndian) {
                Buffer.BlockCopy(bytes, byteIndex, array, 0, size);
            } else {
                for (int i = 0; i < length; i++) {
                    array[i] = ReadInt64();
                }
            }
            byteIndex += size;
            return array;
        }

        public byte[] ReadUInt8Array() {
            int length = ReadVarLength();
            if (length == -1) return null;
            if (length == 0) return new byte[0];
            byte[] array = new byte[length];
            Buffer.BlockCopy(bytes, byteIndex, array, 0, length);
            byteIndex += length;
            return array;
        }

        public ushort[] ReadUInt16Array() {
            int length = ReadVarLength();
            if (length == -1) return null;
            if (length == 0) return new ushort[0];
            ushort[] array = new ushort[length];
            int size = length * LBUInt16.ByteSize;
            if (isLittleEndian) {
                Buffer.BlockCopy(bytes, byteIndex, array, 0, size);
            } else {
                for (int i = 0; i < length; i++) {
                    array[i] = ReadUInt16();
                }
            }
            byteIndex += size;
            return array;
        }

        public uint[] ReadUInt24Array() {
            int length = ReadVarLength();
            if (length == -1) return null;
            if (length == 0) return new uint[0];
            uint[] array = new uint[length];
            for (int i = 0; i < length; i++) {
                array[i] = ReadUInt24();
            }
            return array;
        }

        public uint[] ReadUInt32Array() {
            int length = ReadVarLength();
            if (length == -1) return null;
            if (length == 0) return new uint[0];
            uint[] array = new uint[length];
            int size = length * LBUInt32.ByteSize;
            if (isLittleEndian) {
                Buffer.BlockCopy(bytes, byteIndex, array, 0, size);
            } else {
                for (int i = 0; i < length; i++) {
                    array[i] = ReadUInt32();
                }
            }
            byteIndex += size;
            return array;
        }

        public ulong[] ReadUInt40Array() {
            int length = ReadVarLength();
            if (length == -1) return null;
            if (length == 0) return new ulong[0];
            ulong[] array = new ulong[length];
            for (int i = 0; i < length; i++) {
                array[i] = ReadUInt40();
            }
            return array;
        }

        public ulong[] ReadUInt48Array() {
            int length = ReadVarLength();
            if (length == -1) return null;
            if (length == 0) return new ulong[0];
            ulong[] array = new ulong[length];
            for (int i = 0; i < length; i++) {
                array[i] = ReadUInt48();
            }
            return array;
        }

        public ulong[] ReadUInt56Array() {
            int length = ReadVarLength();
            if (length == -1) return null;
            if (length == 0) return new ulong[0];
            ulong[] array = new ulong[length];
            for (int i = 0; i < length; i++) {
                array[i] = ReadUInt56();
            }
            return array;
        }

        public ulong[] ReadUInt64Array() {
            int length = ReadVarLength();
            if (length == -1) return null;
            if (length == 0) return new ulong[0];
            ulong[] array = new ulong[length];
            int size = length * LBInt64.ByteSize;
            if (isLittleEndian) {
                Buffer.BlockCopy(bytes, byteIndex, array, 0, size);
            } else {
                for (int i = 0; i < length; i++) {
                    array[i] = ReadUInt64();
                }
            }
            byteIndex += size;
            return array;
        }
        #endregion

        #region Float Array
        public float[] ReadFloat8Array() {
            int length = ReadVarLength();
            if (length == -1) return null;
            if (length == 0) return new float[0];
            float[] array = new float[length];
            for (int i = 0; i < length; i++) {
                array[i] = ReadFloat8();
            }
            return array;
        }

        public float[] ReadFloat16Array() {
            int length = ReadVarLength();
            if (length == -1) return null;
            if (length == 0) return new float[0];
            float[] array = new float[length];
            for (int i = 0; i < length; i++) {
                array[i] = ReadFloat16();
            }
            return array;
        }

        public float[] ReadFloat24Array() {
            int length = ReadVarLength();
            if (length == -1) return null;
            if (length == 0) return new float[0];
            float[] array = new float[length];
            for (int i = 0; i < length; i++) {
                array[i] = ReadFloat24();
            }
            return array;
        }

        public float[] ReadFloat32Array() {
            int length = ReadVarLength();
            if (length == -1) return null;
            if (length == 0) return new float[0];
            int size = length * LBFloat32.ByteSize;
            float[] array = new float[length];
            if (isLittleEndian) {
                Buffer.BlockCopy(bytes, byteIndex, array, 0, size);
            } else {
                for (int i = 0; i < length; i++) {
                    array[i] = ReadFloat32();
                }
            }
            byteIndex += size;
            return array;
        }

        public double[] ReadFloat64Array() {
            int length = ReadVarLength();
            if (length == -1) return null;
            if (length == 0) return new double[0];
            int size = length * LBFloat64.ByteSize;
            double[] array = new double[length];
            if (isLittleEndian) {
                Buffer.BlockCopy(bytes, byteIndex, array, 0, size);
            } else {
                for (int i = 0; i < length; i++) {
                    array[i] = ReadFloat64();
                }
            }
            byteIndex += size;
            return array;
        }
        #endregion

        #region Variable Integer Array
        public short[] ReadVarInt16Array() {
            int length = ReadVarLength();
            if (length == -1) return null;
            if (length == 0) return new short[0];
            short[] array = new short[length];
            for (int i = 0; i < length; i++) {
                array[i] = ReadVarInt16();
            }
            return array;
        }

        public int[] ReadVarInt32Array() {
            int length = ReadVarLength();
            if (length == -1) return null;
            if (length == 0) return new int[0];
            int[] array = new int[length];
            for (int i = 0; i < length; i++) {
                array[i] = ReadVarInt32();
            }
            return array;
        }

        public long[] ReadVarInt64Array() {
            int length = ReadVarLength();
            if (length == -1) return null;
            if (length == 0) return new long[0];
            long[] array = new long[length];
            for (int i = 0; i < length; i++) {
                array[i] = ReadVarInt64();
            }
            return array;
        }

        public int[] ReadVarLengthArray() {
            int length = ReadVarLength();
            if (length == -1) return null;
            if (length == 0) return new int[0];
            int[] array = new int[length];
            for (int i = 0; i < length; i++) {
                array[i] = ReadVarLength();
            }
            return array;
        }

        public ushort[] ReadVarUInt16Array() {
            int length = ReadVarLength();
            if (length == -1) return null;
            if (length == 0) return new ushort[0];
            ushort[] array = new ushort[length];
            for (int i = 0; i < length; i++) {
                array[i] = ReadVarUInt16();
            }
            return array;
        }

        public uint[] ReadVarUInt32Array() {
            int length = ReadVarLength();
            if (length == -1) return null;
            if (length == 0) return new uint[0];
            uint[] array = new uint[length];
            for (int i = 0; i < length; i++) {
                array[i] = ReadVarUInt32();
            }
            return array;
        }

        public ulong[] ReadVarUInt64Array() {
            int length = ReadVarLength();
            if (length == -1) return null;
            if (length == 0) return new ulong[0];
            ulong[] array = new ulong[length];
            for (int i = 0; i < length; i++) {
                array[i] = ReadVarUInt64();
            }
            return array;
        }
        #endregion

        #region String Array
        public string[] ReadUTF8Array() {
            int length = ReadVarLength();
            if (length == -1) return null;
            if (length == 0) return new string[0];
            string[] array = new string[length];
            for (int i = 0; i < length; i++) {
                array[i] = ReadUTF8();
            }
            return array;
        }

        public string[] ReadUnicodeArray() {
            int length = ReadVarLength();
            if (length == -1) return null;
            if (length == 0) return new string[0];
            string[] array = new string[length];
            for (int i = 0; i < length; i++) {
                array[i] = ReadUnicode();
            }
            return array;
        }

        public string[] ReadASCIIArray() {
            int length = ReadVarLength();
            if (length == -1) return null;
            if (length == 0) return new string[0];
            string[] array = new string[length];
            for (int i = 0; i < length; i++) {
                array[i] = ReadASCII();
            }
            return array;
        }
        #endregion

        #region API
        /// <summary> 当前读取的数据 | Data currently read </summary>
        public byte[] Bytes {
            set {
                Position = 0;
                bytes = value;
            }
        }

        /// <summary> 当前读取的字节位置 | Byte position currently read </summary>
        public int Position {
            get { return byteIndex; }
            set {
                if (value < 0) throw new ArgumentNullException("Position", "Position can't be negtive!");
                byteIndex = value;
                bitIndex = -1;
                bitLocation = 8;
            }
        }

        /// <summary> 释放并销毁所有数据 | Release and destroy all datas </summary>
        public void Dispose() {
            Position = 0;
            bytes = null;
        }
        #endregion

    }

}
