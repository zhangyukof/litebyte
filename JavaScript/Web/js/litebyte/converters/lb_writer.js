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
// CreateDate: 2019-12-27
// LastModifiedDate: 2020-01-07

/** Binary Writer */
class LBWriter {

    // _________________________ Init _________________________
    /** @param {number} capacity */
    constructor(capacity) {
        if (capacity == null) capacity = this.DEFAULT_CAPACITY;
        if (capacity < 1) capacity = 1;
        this._buffer = new Uint8Array(new ArrayBuffer(capacity));
        this._tempView = new DataView(new ArrayBuffer(8));
        this._byteIndex = 0;
        this._bitIndex = -1;
        this._bitLocation = 8;
        this._capacity = 1;
        this._length = 0;
    }

    requireSize(addSize) {
        let needSize = this._byteIndex + addSize;
        if (needSize > this._buffer.length) {
            let newSize =  this._buffer.length * 2;
            if (newSize < needSize) newSize = needSize;
            this.capacity = newSize;
        }
    }

    // _________________________ Bit _________________________
    /** @param {boolean} value */
    writeBit1(value) {
        if (this._bitLocation > 7) {
            this.requireSize(1);
            this._bitIndex = this._byteIndex++;
            this._bitLocation = 1;
            this._buffer[this._bitIndex] = value ? 1 : 0;
        } else {
            if (value) this._buffer[this._bitIndex] = this._buffer[this._bitIndex] | LBWriter.BIT1_LOCATION_VALUES[this._bitLocation];
            this._bitLocation += 1;
        }
    }

    /** @param {number} value */
    writeBit2(value) {
        if (value < LBBit2.MIN_VALUE || value > LBBit2.MAX_VALUE) {
            throw "value:" + value + " out of range! Bit2 valid range:[0 ~ 3]";
        }
        if (this._bitLocation > 7) {
            this.requireSize(1);
            this._bitIndex = this._byteIndex++;
            this._bitLocation = 2;
            this._buffer[this._bitIndex] = value;
        } else if (this._bitLocation <= 6) {
            this._buffer[this._bitIndex] = this._buffer[this._bitIndex] | value << this._bitLocation;
            this._bitLocation += 2;
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
            this.requireSize(1);
            if ((value & 1) != 0) this._buffer[this._bitIndex] = this._buffer[this._bitIndex] | 0x80;
            this._buffer[this._byteIndex] = value >> 1;
            this._bitIndex = this._byteIndex++;
            this._bitLocation = 1;
        }
    }

    /** @param {number} value */
    writeBit3(value) {
        if (value < LBBit3.MIN_VALUE || value > LBBit3.MAX_VALUE) {
            throw "value:" + value + " out of range! Bit3 valid range:[0 ~ 7]";
        }
        if (this._bitLocation > 7) {
            this.requireSize(1);
            this._bitIndex = this._byteIndex++;
            this._bitLocation = 3;
            this._buffer[this._bitIndex] = value;
        } else if (this._bitLocation <= 5) {
            this._buffer[this._bitIndex] = this._buffer[this._bitIndex] | value << this._bitLocation;
            this._bitLocation += 3;
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
            this.requireSize(1);
            this._buffer[this._bitIndex] = this._buffer[this._bitIndex] | (value & LBWriter.BIT_LOCATION_VALUES[8 - this._bitLocation]) << this._bitLocation;
            this._buffer[this._byteIndex] = value >> 8 - this._bitLocation;
            this._bitLocation = this._bitLocation - 5;
            this._bitIndex = this._byteIndex++;
        }
    }

    /** @param {number} value */
    writeBit4(value) {
        if (value < LBBit4.MIN_VALUE || value > LBBit4.MAX_VALUE) {
            throw "value:" + value + " out of range! Bit4 valid range:[0 ~ 15]";
        }
        if (this._bitLocation > 7) {
            this.requireSize(1);
            this._bitIndex = this._byteIndex++;
            this._bitLocation = 4;
            this._buffer[this._bitIndex] = value;
        } else if (this._bitLocation <= 4) {
            this._buffer[this._bitIndex] = this._buffer[this._bitIndex] | value << this._bitLocation;
            this._bitLocation += 4;
        } else {
            // 计算过程(和WriteBit3相同) | Process(Same as WriteBit3)
            this.requireSize(1);
            this._buffer[this._bitIndex] = this._buffer[this._bitIndex] | (value & LBWriter.BIT_LOCATION_VALUES[8 - this._bitLocation]) << this._bitLocation;
            this._buffer[this._byteIndex] = value >> 8 - this._bitLocation;
            this._bitLocation = this._bitLocation - 4;
            this._bitIndex = this._byteIndex++;
        }
    }

    /** @param {number} value */
    writeBit5(value) {
        if (value < LBBit5.MIN_VALUE || value > LBBit5.MAX_VALUE) {
            throw "value:" + value + " out of range! Bit5 valid range:[0 ~ 31]";
        }
        if (this._bitLocation > 7) {
            this.requireSize(1);
            this._bitIndex = this._byteIndex++;
            this._bitLocation = 5;
            this._buffer[this._bitIndex] = value;
        } else if (this._bitLocation <= 3) {
            this._buffer[this._bitIndex] = this._buffer[this._bitIndex] | value << this._bitLocation;
            this._bitLocation += 5;
        } else {
            // 计算过程(和WriteBit3相同) | Process(Same as WriteBit3)
            this.requireSize(1);
            this._buffer[this._bitIndex] = this._buffer[this._bitIndex] | (value & LBWriter.BIT_LOCATION_VALUES[8 - this._bitLocation]) << this._bitLocation;
            this._buffer[this._byteIndex] = value >> 8 - this._bitLocation;
            this._bitLocation = this._bitLocation - 3;
            this._bitIndex = this._byteIndex++;
        }
    }

    /** @param {number} value */
    writeBit6(value) {
        if (value < LBBit6.MIN_VALUE || value > LBBit6.MAX_VALUE) {
            throw "value:" + value + " out of range! Bit6 valid range:[0 ~ 63]";
        }
        if (this._bitLocation > 7) {
            this.requireSize(1);
            this._bitIndex = this._byteIndex++;
            this._bitLocation = 6;
            this._buffer[this._bitIndex] = value;
        } else if (this._bitLocation <= 2) {
            this._buffer[this._bitIndex] = this._buffer[this._bitIndex] | value << this._bitLocation;
            this._bitLocation += 6;
        } else {
            // 计算过程(和WriteBit3相同) | Process(Same as WriteBit3)
            this.requireSize(1);
            this._buffer[this._bitIndex] = this._buffer[this._bitIndex] | (value & LBWriter.BIT_LOCATION_VALUES[8 - this._bitLocation]) << this._bitLocation;
            this._buffer[this._byteIndex] = value >> 8 - this._bitLocation;
            this._bitLocation = this._bitLocation - 2;
            this._bitIndex = this._byteIndex++;
        }
    }

    /** @param {number} value */
    writeBit7(value) {
        if (value < LBBit7.MIN_VALUE || value > LBBit7.MAX_VALUE) {
            throw "value:" + value + " out of range! Bit7 valid range:[0 ~ 127]";
        }
        if (this._bitLocation > 7) {
            this.requireSize(1);
            this._bitIndex = this._byteIndex++;
            this._bitLocation = 7;
            this._buffer[this._bitIndex] = value;
        } else if (this._bitLocation <= 1) {
            this._buffer[this._bitIndex] = this._buffer[this._bitIndex] | value << this._bitLocation;
            this._bitLocation += 7;
        } else {
            // 计算过程(和WriteBit3相同) | Process(Same as WriteBit3)
            this.requireSize(1);
            this._buffer[this._bitIndex] = this._buffer[this._bitIndex] | (value & LBWriter.BIT_LOCATION_VALUES[8 - this._bitLocation]) << this._bitLocation;
            this._buffer[this._byteIndex] = value >> 8 - this._bitLocation;
            this._bitLocation = this._bitLocation - 1;
            this._bitIndex = this._byteIndex++;
        }
    }

    // _________________________ Int _________________________
    /** @param {number} value */
    writeInt8(value) {
        if (value < LBInt8.MIN_VALUE || value > LBInt8.MAX_VALUE) {
            throw "value:" + value + " out of range! Int8 valid range:[-128 ~ 127]";
        }
        this.requireSize(1);
        this._buffer[this._byteIndex++] = value;
    }

    /** @param {number} value */
    writeInt16(value) {
        if (value < LBInt16.MIN_VALUE || value > LBInt16.MAX_VALUE) {
            throw "value:" + value + " out of range! Int16 valid range:[-32768 ~ 32767]";
        }
        this.requireSize(2);
        this._buffer[this._byteIndex++] = value;
        this._buffer[this._byteIndex++] = value >> 8;
    }

    /** @param {number} value */
    writeInt24(value) {
        if (value < LBInt24.MIN_VALUE || value > LBInt24.MAX_VALUE) {
            throw "value:" + value + " out of range! Int24 valid range:[-8388608 ~ 8388607]";
        }
        this.requireSize(3);
        this._buffer[this._byteIndex] = value;
        this._buffer[this._byteIndex + 1] = value >> 8;
        this._buffer[this._byteIndex + 2] = value >> 16;
        this._byteIndex += 3;
    }

    /** @param {number} value */
    writeInt32(value) {
        if (value < LBInt32.MIN_VALUE || value > LBInt32.MAX_VALUE) {
            throw "value:" + value + " out of range! Int32 valid range:[-2147483648 ~ 2147483647]";
        }
        this.requireSize(4);
        this._buffer[this._byteIndex] = value;
        this._buffer[this._byteIndex + 1] = value >> 8;
        this._buffer[this._byteIndex + 2] = value >> 16;
        this._buffer[this._byteIndex + 3] = value >> 24;
        this._byteIndex += 4;
    }

    /*
    // BUG
    writeInt40(value) {
        if (value < LBInt40.MIN_VALUE || value > LBInt40.MAX_VALUE) {
            throw "value:" + value + " out of range! Int40 valid range:[-549755813888 ~ 549755813887]";
        }
        this.requireSize(5);
        this._buffer[this._byteIndex] = value;
        this._buffer[this._byteIndex + 1] = value >> 8;
        this._buffer[this._byteIndex + 2] = value >> 16;
        this._buffer[this._byteIndex + 3] = value >> 24;
        this._buffer[this._byteIndex + 4] = value >> 32;
        this._byteIndex += 5;
    }

    // BUG
    writeInt48(value) {
        if (value < LBInt48.MIN_VALUE || value > LBInt48.MAX_VALUE) {
            throw "value:" + value + " out of range! Int48 valid range:[-140737488355328 ~ 140737488355327]";
        }
        this.requireSize(6);
        this._buffer[this._byteIndex] = value;
        this._buffer[this._byteIndex + 1] = value >> 8;
        this._buffer[this._byteIndex + 2] = value >> 16;
        this._buffer[this._byteIndex + 3] = value >> 24;
        this._buffer[this._byteIndex + 4] = value >> 32;
        this._buffer[this._byteIndex + 5] = value >> 40;
        this._byteIndex += 6;
    }

    // BUG
    writeInt56(value) {
        // Error! when value > NumberMAX_SAFE_INTEGER(9007199254740991 = 2^53 − 1).
        if (value < LBInt56.MIN_VALUE || value > LBInt56.MAX_VALUE) {
            throw "value:" + value + " out of range! Int56 valid range:[-36028797018963968 ~ 36028797018963967]";
        }
        this.requireSize(7);
        this._buffer[this._byteIndex] = value;
        this._buffer[this._byteIndex + 1] = value >> 8;
        this._buffer[this._byteIndex + 2] = value >> 16;
        this._buffer[this._byteIndex + 3] = value >> 24;
        this._buffer[this._byteIndex + 4] = value >> 32;
        this._buffer[this._byteIndex + 5] = value >> 40;
        this._buffer[this._byteIndex + 6] = value >> 48;
        this._byteIndex += 7;
    }

    // BUG
    writeInt64(value) {
        // Error! when value > NumberMAX_SAFE_INTEGER(9007199254740991 = 2^53 − 1).
        if (value < LBInt64.MIN_VALUE || value > LBInt64.MAX_VALUE) {
            throw "value:" + value + " out of range! Int64 valid range:[-9223372036854775808 ~ 9223372036854775807]";
        }
        this.requireSize(8);
        this._buffer[this._byteIndex] = value;
        this._buffer[this._byteIndex + 1] = value >> 8;
        this._buffer[this._byteIndex + 2] = value >> 16;
        this._buffer[this._byteIndex + 3] = value >> 24;
        this._buffer[this._byteIndex + 4] = value >> 32;
        this._buffer[this._byteIndex + 5] = value >> 40;
        this._buffer[this._byteIndex + 6] = value >> 48;
        this._buffer[this._byteIndex + 7] = value >> 56;
        this._byteIndex += 8;
    }
    */

    // _________________________ UInt _________________________
    /** @param {number} value */
    writeUInt8(value) {
        if (value < LBUInt8.MIN_VALUE || value > LBUInt8.MAX_VALUE) {
            throw "value:" + value + " out of range! UInt8 valid range:[0 ~ 255]";
        }
        this.requireSize(1);
        this._buffer[this._byteIndex++] = value;
    }

    /** @param {number} value */
    writeUInt16(value) {
        if (value < LBUInt16.MIN_VALUE || value > LBUInt16.MAX_VALUE) {
            throw "value:" + value + " out of range! UInt16 valid range:[0 ~ 65535]";
        }
        this.requireSize(2);
        this._buffer[this._byteIndex++] = value;
        this._buffer[this._byteIndex++] = value >> 8;
    }

    /** @param {number} value */
    writeUInt24(value) {
        if (value < LBUInt24.MIN_VALUE || value > LBUInt24.MAX_VALUE) {
            throw "value:" + value + " out of range! UInt24 valid range:[0 ~ 16777215]";
        }
        this.requireSize(3);
        this._buffer[this._byteIndex] = value;
        this._buffer[this._byteIndex + 1] = value >> 8;
        this._buffer[this._byteIndex + 2] = value >> 16;
        this._byteIndex += 3;
    }

    /** @param {number} value */
    writeUInt32(value) {
        if (value < LBUInt32.MIN_VALUE || value > LBUInt32.MAX_VALUE) {
            throw "value:" + value + " out of range! UInt32 valid range:[0 ~ 4294967295]";
        }
        this.requireSize(4);
        this._buffer[this._byteIndex] = value;
        this._buffer[this._byteIndex + 1] = value >> 8;
        this._buffer[this._byteIndex + 2] = value >> 16;
        this._buffer[this._byteIndex + 3] = value >> 24;
        this._byteIndex += 4;
    }

    // _________________________ Float _________________________
    /** @param {number} value */
    writeFloat8(value) {
        if (!LBWriter.isFloat8(value)) {
            throw "value:" + value + " out of range! LBFloat8 valid range:[0 ~ 1 (0/255 ~ 255/255)]";
        }
        this.requireSize(1);
        this._buffer[this._byteIndex++] = value * 255;
    }

    /** @param {number} value */
    writeFloat16(value) {
        if (value < LBFloat16.MIN_VALUE || value > LBFloat16.MAX_VALUE) {
            throw "value:" + value + " out of range! LBFloat16 valid range:[-1.31E+5 ~ 1.31E+5]";
        }
        // Float32(IEEE 754 32-bit) to Float16 (Custom 16-bit) 
        // [Sign(1bit) Exponent(8bits) Mantissa(23bits) Bias(127)] -> [Sign(1bit) Exponent(5bits) Mantissa(10bits) Bias(15)]
        // [S EEEEEEEE MMMMMMMM MMMMMMMM MMMMMMM] -> [S EEEEE MM MMMMMMMM]
        this.requireSize(2);
        this._tempView.setFloat32(0, value, true);
        let v = this._tempView.getInt32(0, true);
        this._tempView.setInt32(0, 0, true);
        let sign = v >> 31;
        let exponent = ((v & 0x7F800000) >> 23) - 112; // -112 = (-127 + 15)
        let mantissa = (v & 0x7FFFFF) >> 13;
        // 最后一位是四舍五入位 | The last digit is rounded
        if ((v & 0x1000) != 0 && mantissa < 0x3FF) mantissa += 1;
        this._buffer[this._byteIndex++] = mantissa;
        this._buffer[this._byteIndex++] = sign << 7 | exponent << 2 | mantissa >> 8;
    }

    /** @param {number} value */
    writeFloat24(value) {
        if (value < LBFloat24.MIN_VALUE || value > LBFloat24.MAX_VALUE) {
            throw "value:" + value + " out of range! LBFloat24 valid range:[-3.6893+19 ~ 3.6893+19]";
        }
        // Float32(IEEE 754 32-bit) to Float24 (Custom 24-bit) 
        // [Sign(1bit) Exponent(8bits) Mantissa(23bits) Bias(127)] -> [Sign(1bit) Exponent(7bits) Mantissa(16bits) Bias(63)]
        // [S EEEEEEEE MMMMMMMM MMMMMMMM MMMMMMM] -> [S EEEEEEE MMMMMMMM MMMMMMMM]
        this.requireSize(3);
        this._tempView.setFloat32(0, value, true);
        let v = this._tempView.getInt32(0, true);
        this._tempView.setInt32(0, 0, true);
        let sign = v >> 31;
        let exponent = ((v & 0x7F800000) >> 23) - 64; // -64 = (-127 + 63)
        let mantissa = (v & 0x7FFFFF) >> 7;
        // 最后一位是四舍五入位 | The last digit is rounded
        if ((v & 0x40) != 0 && mantissa < 0xFFFF) mantissa += 1;
        this._buffer[this._byteIndex] = mantissa;
        this._buffer[this._byteIndex + 1] = mantissa >> 8;
        this._buffer[this._byteIndex + 2] = sign << 7 | exponent;
        this._byteIndex += 3;
    }

    /** @param {number} value */
    writeFloat32(value) {
        if (value < LBFloat32.MIN_VALUE || value > LBFloat32.MAX_VALUE) {
            throw "value:" + value + " out of range! LBFloat32 valid range:[-3.40282347E+38 ~ 3.40282347E+38]";
        }
        this.requireSize(4);
        this._tempView.setFloat32(0, value, true);
        let v = this._tempView.getInt32(0, true);
        this._tempView.setInt32(0, 0, true);
        this._buffer[this._byteIndex] = v;
        this._buffer[this._byteIndex + 1] = v >> 8;
        this._buffer[this._byteIndex + 2] = v >> 16;
        this._buffer[this._byteIndex + 3] = v >> 24;
        this._byteIndex += 4;
    }

    /** @param {number} value */
    writeFloat64(value) {
        this.requireSize(8);
        this._tempView.setFloat64(0, value, true);
        let low = this._tempView.getInt32(0, true);
        let high = this._tempView.getInt32(4, true);
        this._tempView.setFloat64(0, 0, true);
        this._buffer[this._byteIndex] = low;
        this._buffer[this._byteIndex + 1] = low >> 8;
        this._buffer[this._byteIndex + 2] = low >> 16;
        this._buffer[this._byteIndex + 3] = low >> 24;
        this._buffer[this._byteIndex + 4] = high;
        this._buffer[this._byteIndex + 5] = high >> 8;
        this._buffer[this._byteIndex + 6] = high >> 16;
        this._buffer[this._byteIndex + 7] = high >> 24;
        this._byteIndex += 8;
    }

    // _________________________ VarInt _________________________
    /** @param {number} value */
    writeVarInt16(value) {
        if (value <= LBInt8.MAX_VALUE && value >= LBInt8.MIN_VALUE) {
            this.writeBit1(false);
            this.writeInt8(value);
        } else {
            this.writeBit1(true);
            this.writeInt16(value);
        }
    }

    /** @param {number} value */
    writeVarInt32(value) {
        if (value <= LBInt8.MAX_VALUE && value >= LBInt8.MIN_VALUE) {
            this.writeBit2(0);
            this.writeInt8(value);
        } else if (value <= LBInt16.MAX_VALUE && value >= LBInt16.MIN_VALUE) {
            this.writeBit2(1);
            this.writeInt16(value);
        } else if (value <= LBInt24.MAX_VALUE && value >= LBInt24.MIN_VALUE) {
            this.writeBit2(2);
            this.writeInt24(value);
        } else {
            this.writeBit2(3);
            this.writeInt32(value);
        }
    }

    /** @param {number} value */
    writeVarInt64(value) {
        if (value <= LBInt8.MAX_VALUE && value >= LBInt8.MIN_VALUE) {
            this.writeBit3(0);
            this.writeInt8(value);
        } else if (value <= LBInt16.MAX_VALUE && value >= LBInt16.MIN_VALUE) {
            this.writeBit3(1);
            this.writeInt16(value);
        } else if (value <= LBInt24.MAX_VALUE && value >= LBInt24.MIN_VALUE) {
            this.writeBit3(2);
            this.writeInt24(value);
        } else if (value <= LBInt32.MAX_VALUE && value >= LBInt32.MIN_VALUE) {
            this.writeBit3(3);
            this.writeInt32(value);
        } else if (value <= LBInt40.MAX_VALUE && value >= LBInt40.MIN_VALUE) {
            this.writeBit3(4);
            this.writeInt40(value);
        } else if (value <= LBInt48.MAX_VALUE && value >= LBInt48.MIN_VALUE) {
            this.writeBit3(5);
            this.writeInt48(value);
        } else if (value <= LBInt56.MAX_VALUE && value >= LBInt56.MIN_VALUE) {
            this.writeBit3(6);
            this.writeInt56(value);
        } else {
            this.writeBit3(7);
            this.writeInt64(value);
        }
    }

    // _________________________ VarUInt _________________________
    /** @param {number} value */
	writeVarUInt16(value) {
        if (value <= LBUInt8.MAX_VALUE) {
            this.writeBit1(false);
            this.writeUInt8(value);
        } else {
            this.writeBit1(true);
            this.writeUInt16(value);
        }
    }

    /** @param {number} value */
    writeVarUInt32(value) {
        if (value <= LBUInt8.MAX_VALUE) {
            this.writeBit2(0);
            this.writeUInt8(value);
        } else if (value <= LBUInt16.MAX_VALUE) {
            this.writeBit2(1);
            this.writeUInt16(value);
        } else if (value <= LBUInt24.MAX_VALUE) {
            this.writeBit2(2);
            this.writeUInt24(value);
        } else {
            this.writeBit2(3);
            this.writeUInt32(value);
        }
    }

    /** @param {number} value */
    writeVarUInt64(value) {
        if (value <= LBUInt8.MAX_VALUE) {
            this.writeBit3(0);
            this.writeUInt8(value);
        } else if (value <= LBUInt16.MAX_VALUE) {
            this.writeBit3(1);
            this.writeUInt16(value);
        } else if (value <= LBUInt24.MAX_VALUE) {
            this.writeBit3(2);
            this.writeUInt24(value);
        } else if (value <= LBUInt32.MAX_VALUE) {
            this.writeBit3(3);
            this.writeUInt32(value);
        } else if (value <= LBUInt40.MAX_VALUE) {
            this.writeBit3(4);
            this.writeUInt40(value);
        } else if (value <= LBUInt48.MAX_VALUE) {
            this.writeBit3(5);
            this.writeUInt48(value);
        } else if (value <= LBUInt56.MAX_VALUE) {
            this.writeBit3(6);
            this.writeUInt56(value);
        } else {
            this.writeBit3(7);
            this.writeUInt64(value);
        }
    }

    writeVarLength(value) {
        value = value + 1;
        // 7位有效位+1位后续位 每个字节的最高位代表是否有下一个字节 [1xxxxxxx] | 7Bits numbers + 1Bit subsequent. The highest bit means has next byte or not.
        while (value >> 7 != 0) {
            this._buffer[this._byteIndex++] = value & 0x7F | 0x80;
            value = value >> 7;
        }
        this._buffer[this._byteIndex++] = value;
    }

    // _________________________ String _________________________
    /** @param {String} value */
    writeASCII(value) {
        if (!this.writeValidStringLength(value)) return;
        this.writeVarLength(value.length);
        this.requireSize(value.length);
        this._byteIndex += LBEncoding.ASCII.getBytes(value, this._buffer, this._byteIndex);
    }

    /** @param {String} value */
    writeUnicode(value) {
        if (!this.writeValidStringLength(value)) return;
        this.writeVarLength(value.length);
        this.requireSize(value.length * 2);
        this._byteIndex += LBEncoding.Unicode.getBytes(value, this._buffer, this._byteIndex);
    }

    /** @param {String} value */
    writeUTF8(value) {
        if (!this.writeValidStringLength(value)) return;
        let byteCount = LBEncoding.UTF8.getByteCount(value);
        this.writeVarLength(byteCount);
        this.requireSize(byteCount);
        this._byteIndex += LBEncoding.UTF8.getBytes(value, this._buffer, this._byteIndex);
    }

    /** @param {String} value */
    writeVarUnicode(value) {
        if (!this.writeValidStringLength(value)) return;
        // 获取字符数和字节数 | Get char count and byte count
        let charCount = 0;
        let byteCount = 0;
        for (let i = 0; i < value.length;) {
            let charCode = value.codePointAt(i);
            if (charCode < 256) {
                byteCount += 1;
            } else if (charCode < 65536) {
                byteCount += 2;
            } else if (charCode < 16777216) {
                byteCount += 3;
            } else {
                byteCount += 4;
            }
            charCount += 1;
            charCode < 65536 ? i += 1 : i += 2;
        }

        // 写入Unicode码点 | Write Unicode code point.
        this.writeVarLength(charCount);
        this.requireSize(byteCount + Math.ceil(charCount * 0.25));
        for (let i = 0; i < value.length;) {
            let code = value.codePointAt(i);
            this.writeVarUInt32(code);
            code < 65536 ? i += 1 : i += 2;
        }
    }

    // _________________________ Bit Array _________________________
    /** @param {boolean[]} array */
    writeBit1Array(array) {
        if (!this.writeValidArrayLength(array, LBBit1.BYTE_SIZE)) return;
        for (let i = 0; i < array.length; i++) {
            this.writeBit1(array[i]);
        }
    }

    /** @param {number[]} array */
    writeBit2Array(array) {
        if (!this.writeValidArrayLength(array, LBBit2.BYTE_SIZE)) return;
        for (let i = 0; i < array.length; i++) {
            this.writeBit2(array[i]);
        }
    }

    /** @param {number[]} array */
    writeBit3Array(array) {
        if (!this.writeValidArrayLength(array, LBBit3.BYTE_SIZE)) return;
        for (let i = 0; i < array.length; i++) {
            this.writeBit3(array[i]);
        }
    }

    /** @param {number[]} array */
    writeBit4Array(array) {
        if (!this.writeValidArrayLength(array, LBBit4.BYTE_SIZE)) return;
        for (let i = 0; i < array.length; i++) {
            this.writeBit4(array[i]);
        }
    }

    /** @param {number[]} array */
    writeBit5Array(array) {
        if (!this.writeValidArrayLength(array, LBBit5.BYTE_SIZE)) return;
        for (let i = 0; i < array.length; i++) {
            this.writeBit5(array[i]);
        }
    }

    /** @param {number[]} array */
    writeBit6Array(array) {
        if (!this.writeValidArrayLength(array, LBBit6.BYTE_SIZE)) return;
        for (let i = 0; i < array.length; i++) {
            this.writeBit6(array[i]);
        }
    }

    /** @param {number[]} array */
    writeBit7Array(array) {
        if (!this.writeValidArrayLength(array, LBBit7.BYTE_SIZE)) return;
        for (let i = 0; i < array.length; i++) {
            this.writeBit7(array[i]);
        }
    }

    // _________________________ Int Array _________________________
    /** @param {number[]} array */
    writeInt8Array(array) {
        if (!this.writeValidArrayLength(array, LBInt8.BYTE_SIZE)) return;
        for (let i = 0; i < array.length; i++) {
            this.writeInt8(array[i]);
        }
    }

    /** @param {number[]} array */
    writeInt16Array(array) {
        if (!this.writeValidArrayLength(array, LBInt16.BYTE_SIZE)) return;
        for (let i = 0; i < array.length; i++) {
            this.writeInt16(array[i]);
        }
    }

    /** @param {number[]} array */
    writeInt24Array(array) {
        if (!this.writeValidArrayLength(array, LBInt24.BYTE_SIZE)) return;
        for (let i = 0; i < array.length; i++) {
            this.writeInt24(array[i]);
        }
    }

    /** @param {number[]} array */
    writeInt32Array(array) {
        if (!this.writeValidArrayLength(array, LBInt32.BYTE_SIZE)) return;
        for (let i = 0; i < array.length; i++) {
            this.writeInt32(array[i]);
        }
    }

    /*
    writeInt40Array(array) {
        if (!this.writeValidArrayLength(array, LBInt40.BYTE_SIZE)) return;
        for (let i = 0; i < array.length; i++) {
            this.writeInt40(array[i]);
        }
    }

    writeInt48Array(array) {
        if (!this.writeValidArrayLength(array, LBInt48.BYTE_SIZE)) return;
        for (let i = 0; i < array.length; i++) {
            this.writeInt48(array[i]);
        }
    }

    writeInt56Array(array) {
        if (!this.writeValidArrayLength(array, LBInt56.BYTE_SIZE)) return;
        for (let i = 0; i < array.length; i++) {
            this.writeInt56(array[i]);
        }
    }

    writeInt64Array(array) {
        if (!this.writeValidArrayLength(array, LBInt64.BYTE_SIZE)) return;
        for (let i = 0; i < array.length; i++) {
            this.writeInt64(array[i]);
        }
    }
    */

    // _________________________ UInt Array _________________________
    /** @param {number[]} array */
    writeUInt8Array(array) {
        if (!this.writeValidArrayLength(array, LBUInt8.BYTE_SIZE)) return;
        for (let i = 0; i < array.length; i++) {
            this.writeUInt8(array[i]);
        }
    }

    /** @param {number[]} array */
    writeUInt16Array(array) {
        if (!this.writeValidArrayLength(array, LBUInt16.BYTE_SIZE)) return;
        for (let i = 0; i < array.length; i++) {
            this.writeUInt16(array[i]);
        }
    }

    /** @param {number[]} array */
    writeUInt24Array(array) {
        if (!this.writeValidArrayLength(array, LBUInt24.BYTE_SIZE)) return;
        for (let i = 0; i < array.length; i++) {
            this.writeUInt24(array[i]);
        }
    }

    /** @param {number[]} array */
    writeUInt32Array(array) {
        if (!this.writeValidArrayLength(array, LBUInt32.BYTE_SIZE)) return;
        for (let i = 0; i < array.length; i++) {
            this.writeUInt32(array[i]);
        }
    }

    /*
    writeUInt40Array(array) {
        if (!this.writeValidArrayLength(array, LBUInt40.BYTE_SIZE)) return;
        for (let i = 0; i < array.length; i++) {
            this.writeUInt40(array[i]);
        }
    }

    writeUInt48Array(array) {
        if (!this.writeValidArrayLength(array, LBUInt48.BYTE_SIZE)) return;
        for (let i = 0; i < array.length; i++) {
            this.writeUInt48(array[i]);
        }
    }

    writeUInt56Array(array) {
        if (!this.writeValidArrayLength(array, LBUInt56.BYTE_SIZE)) return;
        for (let i = 0; i < array.length; i++) {
            this.writeUInt56(array[i]);
        }
    }

    writeUInt64Array(array) {
        if (!this.writeValidArrayLength(array, LBUInt64.BYTE_SIZE)) return;
        for (let i = 0; i < array.length; i++) {
            this.writeUInt64(array[i]);
        }
    }
    */

    // _________________________ Float Array _________________________
    /** @param {number[]} array */
    writeFloat8Array(array) {
        if (!this.writeValidArrayLength(array, LBFloat8.BYTE_SIZE)) return;
        for (let i = 0; i < array.length; i++) {
            this.writeFloat8(array[i]);
        }
    }

    /** @param {number[]} array */
    writeFloat16Array(array) {
        if (!this.writeValidArrayLength(array, LBFloat16.BYTE_SIZE)) return;
        for (let i = 0; i < array.length; i++) {
            this.writeFloat16(array[i]);
        }
    }

    /** @param {number[]} array */
    writeFloat24Array(array) {
        if (!this.writeValidArrayLength(array, LBFloat24.BYTE_SIZE)) return;
        for (let i = 0; i < array.length; i++) {
            this.writeFloat24(array[i]);
        }
    }

    /** @param {number[]} array */
    writeFloat32Array(array) {
        if (!this.writeValidArrayLength(array, LBFloat32.BYTE_SIZE)) return;
        for (let i = 0; i < array.length; i++) {
            this.writeFloat32(array[i]);
        }
    }

    /** @param {number[]} array */
    writeFloat64Array(array) {
        if (!this.writeValidArrayLength(array, LBFloat64.BYTE_SIZE)) return;
        for (let i = 0; i < array.length; i++) {
            this.writeFloat64(array[i]);
        }
    }

    // _________________________ VarInt Array _________________________
    /** @param {number[]} array */
    writeVarInt16Array(array) {
        if (!this.writeValidArrayLength(array, LBVarInt16.MIN_BYTE_SIZE)) return;
        for (let i = 0; i < array.length; i++) {
            this.writeVarInt16(array[i]);
        }
    }

    /** @param {number[]} array */
    writeVarInt32Array(array) {
        if (!this.writeValidArrayLength(array, LBVarInt32.MIN_BYTE_SIZE)) return;
        for (let i = 0; i < array.length; i++) {
            this.writeVarInt32(array[i]);
        }
    }

    /*
    writeVarInt64Array(array) {
        if (!this.writeValidArrayLength(array, LBVarInt64.MIN_BYTE_SIZE)) return;
        for (let i = 0; i < array.length; i++) {
            this.writeVarInt64(array[i]);
        }
    }
    */

    // _________________________ VarUInt Array _________________________
    /** @param {number[]} array */
    writeVarUInt16Array(array) {
        if (!this.writeValidArrayLength(array, LBVarUInt16.MIN_BYTE_SIZE)) return;
        for (let i = 0; i < array.length; i++) {
            this.writeVarUInt16(array[i]);
        }
    }

    /** @param {number[]} array */
    writeVarUInt32Array(array) {
        if (!this.writeValidArrayLength(array, LBVarUInt32.MIN_BYTE_SIZE)) return;
        for (let i = 0; i < array.length; i++) {
            this.writeVarUInt32(array[i]);
        }
    }

    /*
    writeVarUInt64Array(array) {
        if (!this.writeValidArrayLength(array, LBVarUInt64.MIN_BYTE_SIZE)) return;
        for (let i = 0; i < array.length; i++) {
            this.writeVarUInt64(array[i]);
        }
    }
    */

    // _________________________ String Array _________________________
    /** @param {string[]} array */
    writeASCIIArray(array) {
        if (!this.writeValidArrayLength(array, 1)) return;
        for (let i = 0; i < array.length; i++) {
            this.writeASCII(array[i]);
        }
    }

    /** @param {string[]} array */
    writeUnicodeArray(array) {
        if (!this.writeValidArrayLength(array, 2)) return;
        for (let i = 0; i < array.length; i++) {
            this.writeUnicode(array[i]);
        }
    }

    /** @param {string[]} array */
    writeUTF8Array(array) {
        if (!this.writeValidArrayLength(array, 2)) return;
        for (let i = 0; i < array.length; i++) {
            this.writeUTF8(array[i]);
        }
    }

    /** @param {string[]} array */
    writeVarUnicodeArray(array) {
        if (!this.writeValidArrayLength(array, 2)) return;
        for (let i = 0; i < array.length; i++) {
            this.writeVarUnicode(array[i]);
        }
    }
    
    // _________________________ Tools _________________________
    static copyArray(sourceArray, targetArray) {
        let length = sourceArray.length;
        for (let i = 0; i < length; i++) {
            targetArray[i] = sourceArray[i];
        }
    }

    static isFloat8(value) {
        if (value < LBFloat8.MIN_VALUE || value > LBFloat8.MAX_VALUE) return false;
        return value * 255 % 1 == 0;
    }

    writeValidStringLength(value) {
        if (value == null) {
            WriteVarLength(-1);
            return false;
        } else if (value.length == 0) {
            WriteVarLength(0);
            return false;
        } else {
            return true;
        }
    }

    writeValidArrayLength(array, typeSize) {
        if (array == null) {
            this.writeVarLength(-1);
            return false;
        } else if (array.length == 0) {
            this.writeVarLength(0);
            return false;
        } else {
            this.writeVarLength(array.length);
            let size = array.length * typeSize;
            if (size % 1 != 0) size = Math.ceil(size);
            this.requireSize(size);
            return true;
        }
    }

    // _________________________ API _________________________
    /**
     * 把已写入的数据转换为字节数组 | Convert written data to byte array
     * @returns {Uint8Array}
     */
    toBytes() {
        return this._buffer.slice(0, this._byteIndex);
    }

    /** 擦除已写入的数据 以便重新使用 | Erase written data for reuse */
    erase() {
        for (let i = 0; i < this._byteIndex; i++) {
            this._buffer[i] = 0;
        }
        this.position = 0;
    }

    get capacity() {
        return this._buffer.length;
    }

    set capacity(value) {
        if (value < 0) throw "Capacity can't be negtive!";
        if (value > this._buffer.length) {
            let newBuffer = new Uint8Array(new ArrayBuffer(value));
            LBWriter.copyArray(this._buffer, newBuffer);
            this._buffer = newBuffer;
        } else if (value < this._buffer.length) {
            this.dispose();
            if (value < 1) value = 1;
            this._buffer = new Uint8Array(new ArrayBuffer(value));
        }
    }

    get length() {
        return this._byteIndex;
    }

    get position() {
        return this._byteIndex;
    }

    set position(value) {
        if (value < 0) throw "Position can't be negtive!";
        this._byteIndex = value;
        this._bitIndex = -1;
        this._bitLocation = 8;
    }

    dispose() {
        this.position = 0;
        this.bytes = null;
    }

}

// 静态属性 | Static Properties
LBWriter.DEFAULT_CAPACITY = 16;
LBWriter.BIT_LOCATION_VALUES = [0x00, 0x01, 0x03, 0x07, 0x0F, 0x1F, 0x3F, 0x7F];
LBWriter.BIT1_LOCATION_VALUES = [0x01, 0x02, 0x04, 0x08, 0x10, 0x20, 0x40, 0x80];