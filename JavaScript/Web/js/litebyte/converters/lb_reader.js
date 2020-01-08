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

// Static Properties
const LBReaderStatic = {
    BIT_LOCATION_VALUES:[0x00, 0x01, 0x03, 0x07, 0x0F, 0x1F, 0x3F, 0x7F],
    BIT1_LOCATION_VALUES:[0x01, 0x02, 0x04, 0x08, 0x10, 0x20, 0x40, 0x80],
}

// Binary Writer
class LBReader {

    // _________________________ Init _________________________
    constructor(bytes) {
        this.bytes = bytes;
    }

    // _________________________ Bit _________________________
    readBit1() {
        if (this._bitLocation > 7) {
            this._bitIndex = this._byteIndex++;
            this._bitLocation = 1;
            return (this._bytes[this._bitIndex] & 1) != 0;
        } else {
            return (this._bytes[this._bitIndex] & this.BIT1_LOCATION_VALUES[this._bitLocation++]) != 0;
        }
    }

    readBit2() {
        if (this._bitLocation > 7) {
            this._bitIndex = this._byteIndex++;
            this._bitLocation = 2;
            return this._bytes[this._bitIndex] & 0x03;
        } else if (this._bitLocation <= 6) {
            let value = (this._bytes[this._bitIndex] >> this._bitLocation) & 0x03;
            this._bitLocation += 2;
            return value;
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
            let value = this._bytes[this._bitIndex] >> 7 | (this._bytes[this._byteIndex] & 1) << 1;
            this._bitIndex = this._byteIndex++;
            this._bitLocation = 1;
            return value;
        }
    }

    readBit3() {
        if (this._bitLocation > 7) {
            this._bitIndex = this._byteIndex++;
            this._bitLocation = 3;
            return this._bytes[this._bitIndex] & 0x07;
        } else if (this._bitLocation <= 5) {
            let value = (this._bytes[this._bitIndex] >> this._bitLocation) & 0x07;
            this._bitLocation += 3;
            return value;
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
            let value = this._bytes[this._bitIndex] >> this._bitLocation | (this._bytes[this._byteIndex] & this.BIT_LOCATION_VALUES[this._bitLocation - 5]) << 8 - this._bitLocation;
            this._bitLocation -= 5;
            this._bitIndex = this._byteIndex++;
            return value;
        }
    }

    readBit4() {
        if (this._bitLocation > 7) {
            this._bitIndex = this._byteIndex++;
            this._bitLocation = 4;
            return this._bytes[this._bitIndex] & 0x0F;
        } else if (this._bitLocation <= 4) {
            let value = (this._bytes[this._bitIndex] >> this._bitLocation) & 0x0F;
            this._bitLocation += 4;
            return value;
        } else {
            // 计算过程(和ReadBit3类似) | Process(Same as ReadBit3)
            let value = this._bytes[this._bitIndex] >> this._bitLocation | (this._bytes[this._byteIndex] & this.BIT_LOCATION_VALUES[this._bitLocation - 4]) << 8 - this._bitLocation;
            this._bitLocation -= 4;
            this._bitIndex = this._byteIndex++;
            return value;
        }
    }

    readBit5() {
        if (this._bitLocation > 7) {
            this._bitIndex = this._byteIndex++;
            this._bitLocation = 5;
            return this._bytes[this._bitIndex] & 0x1F;
        } else if (this._bitLocation <= 3) {
            let value = (this._bytes[this._bitIndex] >> this._bitLocation) & 0x1F;
            this._bitLocation += 5;
            return value;
        } else {
            // 计算过程(和ReadBit3类似) | Process(Same as ReadBit3)
            let value = this._bytes[this._bitIndex] >> this._bitLocation | (this._bytes[this._byteIndex] & this.BIT_LOCATION_VALUES[this._bitLocation - 3]) << 8 - this._bitLocation;
            this._bitLocation -= 3;
            this._bitIndex = this._byteIndex++;
            return value;
        }
    }

    readBit6() {
        if (this._bitLocation > 7) {
            this._bitIndex = this._byteIndex++;
            this._bitLocation = 6;
            return this._bytes[this._bitIndex] & 0x3F;
        } else if (this._bitLocation <= 2) {
            let value = (this._bytes[this._bitIndex] >> this._bitLocation) & 0x3F;
            this._bitLocation += 6;
            return value;
        } else {
            // 计算过程(和ReadBit3类似) | Process(Same as ReadBit3)
            let value = this._bytes[this._bitIndex] >> this._bitLocation | (this._bytes[this._byteIndex] & this.BIT_LOCATION_VALUES[this._bitLocation - 2]) << 8 - this._bitLocation;
            this._bitLocation -= 2;
            this._bitIndex = this._byteIndex++;
            return value;
        }
    }

    readBit7() {
        if (this._bitLocation > 7) {
            this._bitIndex = this._byteIndex++;
            this._bitLocation = 7;
            return this._bytes[this._bitIndex] & 0x7F;
        } else if (this._bitLocation <= 1) {
            let value = this._bytes[this._bitIndex] >> 1;
            this._bitLocation += 7;
            return value;
        } else {
            // 计算过程(和ReadBit3类似) | Process(Same as ReadBit3)
            let value = this._bytes[this._bitIndex] >> this._bitLocation | (this._bytes[this._byteIndex] & this.BIT_LOCATION_VALUES[this._bitLocation - 1]) << 8 - this._bitLocation;
            this._bitLocation -= 1;
            this._bitIndex = this._byteIndex++;
            return value;
        }
    }

    // _________________________ Int _________________________
    readInt8() {
        let value = this._bytes[this._byteIndex++];
        return value > LBInt8.MAX_VALUE ? value - 256 : value;
    }

    readInt16() {
        let value = this._bytes[this._byteIndex++] | this._bytes[this._byteIndex++] << 8;
        return value > LBInt16.MAX_VALUE ? value - 65536 : value;
    }

    readInt24() {
        let value = this._bytes[this._byteIndex++] | this._bytes[this._byteIndex++] << 8 | this._bytes[this._byteIndex++] << 16;
        return value > LBInt24.MAX_VALUE ? value - 16777216 : value;
    }

    readInt32() {
        return this._bytes[this._byteIndex++] | this._bytes[this._byteIndex++] << 8 | this._bytes[this._byteIndex++] << 16 | this._bytes[this._byteIndex++] << 24;
    }

    readInt40() {
        //this._view.setUint8(0, this._bytes[this._byteIndex++]);
        //this._view.setUint8(1, this._bytes[this._byteIndex++]);
        //this._view.setUint8(2, this._bytes[this._byteIndex++]);
        //this._view.setUint8(3, this._bytes[this._byteIndex++]);
    }

    // _________________________ UInt _________________________
    readUInt8() {
        return this._bytes[this._byteIndex++];
    }

    readUInt16() {
        return this._bytes[this._byteIndex++] | this._bytes[this._byteIndex++] << 8;
    }

    readUInt24() {
        return this._bytes[this._byteIndex++] | this._bytes[this._byteIndex++] << 8 | this._bytes[this._byteIndex++] << 16;
    }

    readUInt32() {
        let value = this._bytes[this._byteIndex++] | this._bytes[this._byteIndex++] << 8 | this._bytes[this._byteIndex++] << 16 | this._bytes[this._byteIndex++] << 24;
        return value < 0 ? 4294967296 + value : value;
    }

    // _________________________ Float _________________________
    readFloat8() {
        let value = this._bytes[this._byteIndex++] / 255;
        return Number.parseFloat(value.toPrecision(7));
    }

    readFloat16() {
        // Float16 (Custom 16-bit) to Float32(IEEE 754 32-bit)
        // [Sign(1bit) Exponent(5bits) Mantissa(10bits) Bias(15)] -> [Sign(1bit) Exponent(8bits) Mantissa(23bits) Bias(127)]
        // [S EEEEE MM MMMMMMMM] -> [S EEEEEEEE MMMMMMMM MMMMMMMM MMMMMMM]
        let sem = this._bytes[this._byteIndex + 1];
        let v = (sem & 0x80) << 24 | ((sem & 0x7C) >> 2) + 112 << 23 | (sem & 0x3) << 21 | this._bytes[this._byteIndex] << 13;
        this._tempView.setUint32(0, v, true);
        let value = this._tempView.getFloat32(0, true);
        this._tempView.setUint32(0, 0, true);
        this._byteIndex += 2;
        return Number.parseFloat(value.toPrecision(3));
    }

    readFloat24() {
        // Float24 (Custom 24-bit) to Float32(IEEE 754 32-bit)
        // [Sign(1bit) Exponent(7bits) Mantissa(16bits) Bias(63)] -> [Sign(1bit) Exponent(8bits) Mantissa(23bits) Bias(127)]
        // [S EEEEEEE MMMMMMMM MMMMMMMM] -> [S EEEEEEEE MMMMMMMM MMMMMMMM MMMMMMM]
        let se = this._bytes[this._byteIndex + 2];
        let v = (se & 0x80) << 24 | ((se & 0x7F) + 64) << 23 | this._bytes[this._byteIndex + 1] << 15 | this._bytes[this._byteIndex] << 7;
        this._tempView.setUint32(0, v, true);
        let value = this._tempView.getFloat32(0, true);
        this._tempView.setUint32(0, 0, true);
        this._byteIndex += 3;
        return Number.parseFloat(value.toPrecision(5));
    }

    readFloat32() {
        this._tempView.setInt32(0, this._bytes[this._byteIndex] | this._bytes[this._byteIndex + 1] << 8 | this._bytes[this._byteIndex + 2] << 16 | this._bytes[this._byteIndex + 3] << 24, true)
        let value = this._tempView.getFloat32(0, true);
        this._tempView.setInt32(0, 0, true);
        this._byteIndex += 4;
        return Number.parseFloat(value.toPrecision(7));
    }

    readFloat64() {
        this._tempView.setInt32(0, this._bytes[this._byteIndex] | this._bytes[this._byteIndex + 1] << 8 | this._bytes[this._byteIndex + 2] << 16 | this._bytes[this._byteIndex + 3] << 24, true)
        this._tempView.setInt32(4, this._bytes[this._byteIndex + 4] | this._bytes[this._byteIndex + 5] << 8 | this._bytes[this._byteIndex + 6] << 16 | this._bytes[this._byteIndex + 7] << 24, true)
        let value = this._tempView.getFloat64(0, true);
        this._tempView.setFloat64(0, 0, true);
        this._byteIndex += 8;
        return value;
    }

    // _________________________ VarInt _________________________
    readVarInt16() {
        if (this.readBit1()) return this.readInt16();
        return this.readInt8();
    }

    readVarInt32() {
        switch (this.readBit2()) {
            case 0: return this.readInt8();
            case 1: return this.readInt16();
            case 2: return this.readInt24();
            case 3: return this.readInt32();
            default: throw "ReadVarInt32() byte size error!";
        }
    }

    readVarInt64() {
        switch (this.readBit3()) {
            case 0: return this.readInt8();
            case 1: return this.readInt16();
            case 2: return this.readInt24();
            case 3: return this.readInt32();
            case 4: return this.readInt40();
            case 5: return this.readInt48();
            case 6: return this.readInt56();
            case 7: return this.readInt64();
            default: throw "ReadVarInt64() byte size error!";
        }
    }

    // _________________________ VarUInt _________________________
    readVarUInt16() {
        if (this.readBit1()) return this.readUInt16();
        return this.readUInt8();
    }

    readVarUInt32() {
        switch (this.readBit2()) {
            case 0: return this.readUInt8();
            case 1: return this.readUInt16();
            case 2: return this.readUInt24();
            case 3: return this.readUInt32();
            default: throw "ReadVarUInt32() byte size error!";
        }
    }

    readVarUInt64() {
        switch (this.readBit3()) {
            case 0: return this.readUInt8();
            case 1: return this.readUInt16();
            case 2: return this.readUInt24();
            case 3: return this.readUInt32();
            case 4: return this.readUInt40();
            case 5: return this.readUInt48();
            case 6: return this.readUInt56();
            case 7: return this.readUInt64();
            default: throw "ReadVarUInt64() byte size error!";
        }
    }

    readVarLength() {
        if ((this._bytes[this._byteIndex] & 0x80) == 0) {
            return this._bytes[this._byteIndex++] - 1;
        }
        if ((this._bytes[this._byteIndex + 1] & 0x80) == 0) {
            return (this._bytes[this._byteIndex++] & 0x7F |
                   this._bytes[this._byteIndex++] << 7) - 1
        }
        if ((this._bytes[this._byteIndex + 2] & 0x80) == 0) {
            return (this._bytes[this._byteIndex++] & 0x7F |
                   (this._bytes[this._byteIndex++] & 0x7F) << 7 |
                   this._bytes[this._byteIndex++] << 14) - 1
        }
        if ((this._bytes[this._byteIndex + 3] & 0x80) == 0) {
            return (this._bytes[this._byteIndex++] & 0x7F |
                   (this._bytes[this._byteIndex++] & 0x7F) << 7 |
                   (this._bytes[this._byteIndex++] & 0x7F) << 14 |
                   this._bytes[this._byteIndex++] << 21) - 1
        }
        return (this._bytes[this._byteIndex++] & 0x7F |
               (this._bytes[this._byteIndex++] & 0x7F) << 7 |
               (this._bytes[this._byteIndex++] & 0x7F) << 14 |
               (this._bytes[this._byteIndex++] & 0x7F) << 21 |
               this._bytes[this._byteIndex++] << 28) - 1;
    }

    // _________________________ String _________________________
    readASCII() {
        let charCount = this.readVarLength();
        if (charCount == -1) return null;
        if (charCount == 0) return "";
        let value = LBEncoding.ASCII.getString(this._bytes, this._byteIndex, charCount);
        this._byteIndex += charCount;
        return value;
    }

    readUnicode() {
        let charCount = this.readVarLength();
        if (charCount == -1) return null;
        if (charCount == 0) return "";
        let value = LBEncoding.Unicode.getString(this._bytes, this._byteIndex, charCount);
        this._byteIndex += charCount * 2;
        return value;
    }

    readUTF8() {
        let byteCount = this.readVarLength();
        if (byteCount == -1) return null;
        if (byteCount == 0) return "";
        let value = LBEncoding.UTF8.getString(this._bytes, this._byteIndex, byteCount);
        this._byteIndex += byteCount;
        return value;
    }

    readVarUnicode() {
        let charCount = this.readVarLength();
        if (charCount == -1) return null;
        if (charCount == 0) return "";
        let value = "";
        for (let i = 0; i < charCount; i++) {
            value += String.fromCharCode(this.readVarUInt32());
        }
        return value;
    }

    // _________________________ Bit Array _________________________
    readBit1Array() {
        let length = this.readVarLength();
        if (length == -1) return null;
        if (length == 0) return [];
        let array = new Array(length);
        for (let i = 0; i < length; i++) {
            array[i] = this.readBit1();
        }
        return array;
    }

    readBit2Array() {
        let length = this.readVarLength();
        if (length == -1) return null;
        if (length == 0) return new Uint8Array();
        let array = new Uint8Array(length);
        for (let i = 0; i < length; i++) {
            array[i] = this.readBit2();
        }
        return array;
    }

    readBit3Array() {
        let length = this.readVarLength();
        if (length == -1) return null;
        if (length == 0) return new Uint8Array();
        let array = new Uint8Array(length);
        for (let i = 0; i < length; i++) {
            array[i] = this.readBit3();
        }
        return array;
    }

    readBit4Array() {
        let length = this.readVarLength();
        if (length == -1) return null;
        if (length == 0) return new Uint8Array();
        let array = new Uint8Array(length);
        for (let i = 0; i < length; i++) {
            array[i] = this.readBit4();
        }
        return array;
    }

    readBit5Array() {
        let length = this.readVarLength();
        if (length == -1) return null;
        if (length == 0) return new Uint8Array();
        let array = new Uint8Array(length);
        for (let i = 0; i < length; i++) {
            array[i] = this.readBit5();
        }
        return array;
    }

    readBit6Array() {
        let length = this.readVarLength();
        if (length == -1) return null;
        if (length == 0) return new Uint8Array();
        let array = new Uint8Array(length);
        for (let i = 0; i < length; i++) {
            array[i] = this.readBit6();
        }
        return array;
    }

    readBit7Array() {
        let length = this.readVarLength();
        if (length == -1) return null;
        if (length == 0) return new Uint8Array();
        let array = new Uint8Array(length);
        for (let i = 0; i < length; i++) {
            array[i] = this.readBit7();
        }
        return array;
    }

    // _________________________ Int Array _________________________
    readInt8Array() {
        let length = this.readVarLength();
        if (length == -1) return null;
        if (length == 0) return new Int8Array();
        let array = new Int8Array(length);
        for (let i = 0; i < length; i++) {
            array[i] = this.readInt8();
        }
        return array;
    }

    readInt16Array() {
        let length = this.readVarLength();
        if (length == -1) return null;
        if (length == 0) return new Int16Array();
        let array = new Int16Array(length);
        for (let i = 0; i < length; i++) {
            array[i] = this.readInt16();
        }
        return array;
    }

    readInt24Array() {
        let length = this.readVarLength();
        if (length == -1) return null;
        if (length == 0) return new Int32Array();
        let array = new Int32Array(length);
        for (let i = 0; i < length; i++) {
            array[i] = this.readInt24();
        }
        return array;
    }

    readInt32Array() {
        let length = this.readVarLength();
        if (length == -1) return null;
        if (length == 0) return new Int32Array();
        let array = new Int32Array(length);
        for (let i = 0; i < length; i++) {
            array[i] = this.readInt32();
        }
        return array;
    }

    /*
    readInt40Array() {
        let length = this.readVarLength();
        if (length == -1) return null;
        if (length == 0) return new Int64Array();
        let array = new Int64Array(length);
        for (let i = 0; i < length; i++) {
            array[i] = this.readInt40();
        }
        return array;
    }

    readInt48Array() {
        let length = this.readVarLength();
        if (length == -1) return null;
        if (length == 0) return new Int64Array();
        let array = new Int64Array(length);
        for (let i = 0; i < length; i++) {
            array[i] = this.readInt48();
        }
        return array;
    }

    readInt56Array() {
        let length = this.readVarLength();
        if (length == -1) return null;
        if (length == 0) return new Int64Array();
        let array = new Int64Array(length);
        for (let i = 0; i < length; i++) {
            array[i] = this.readInt56();
        }
        return array;
    }

    readInt64Array() {
        let length = this.readVarLength();
        if (length == -1) return null;
        if (length == 0) return new Int64Array();
        let array = new Int64Array(length);
        for (let i = 0; i < length; i++) {
            array[i] = this.readInt64();
        }
        return array;
    }
    */

    // _________________________ UInt Array _________________________
    readUInt8Array() {
        let length = this.readVarLength();
        if (length == -1) return null;
        if (length == 0) return new Uint8Array();
        let array = new Uint8Array(length);
        for (let i = 0; i < length; i++) {
            array[i] = this.readUInt8();
        }
        return array;
    }

    readUInt16Array() {
        let length = this.readVarLength();
        if (length == -1) return null;
        if (length == 0) return new Uint16Array();
        let array = new Uint16Array(length);
        for (let i = 0; i < length; i++) {
            array[i] = this.readUInt16();
        }
        return array;
    }

    readUInt24Array() {
        let length = this.readVarLength();
        if (length == -1) return null;
        if (length == 0) return new Uint32Array();
        let array = new Uint32Array(length);
        for (let i = 0; i < length; i++) {
            array[i] = this.readUInt24();
        }
        return array;
    }

    readUInt32Array() {
        let length = this.readVarLength();
        if (length == -1) return null;
        if (length == 0) return new Uint32Array();
        let array = new Uint32Array(length);
        for (let i = 0; i < length; i++) {
            array[i] = this.readUInt32();
        }
        return array;
    }

    /*
    readUInt40Array() {
        let length = this.readVarLength();
        if (length == -1) return null;
        if (length == 0) return new Uint64Array();
        let array = new Uint64Array(length);
        for (let i = 0; i < length; i++) {
            array[i] = this.readUInt40();
        }
        return array;
    }

    readUInt48Array() {
        let length = this.readVarLength();
        if (length == -1) return null;
        if (length == 0) return new Uint64Array();
        let array = new Uint64Array(length);
        for (let i = 0; i < length; i++) {
            array[i] = this.readUInt48();
        }
        return array;
    }

    readUInt56Array() {
        let length = this.readVarLength();
        if (length == -1) return null;
        if (length == 0) return new Uint64Array();
        let array = new Uint64Array(length);
        for (let i = 0; i < length; i++) {
            array[i] = this.readUInt56();
        }
        return array;
    }

    readUInt64Array() {
        let length = this.readVarLength();
        if (length == -1) return null;
        if (length == 0) return new Uint64Array();
        let array = new Uint64Array(length);
        for (let i = 0; i < length; i++) {
            array[i] = this.readUInt64();
        }
        return array;
    }
    */

    // _________________________ Float Array _________________________
    readFloat8Array() {
        let length = this.readVarLength();
        if (length == -1) return null;
        if (length == 0) return new Float32Array();
        let array = new Float32Array(length);
        for (let i = 0; i < length; i++) {
            array[i] = this.readFloat8();
        }
        return array;
    }

    readFloat16Array() {
        let length = this.readVarLength();
        if (length == -1) return null;
        if (length == 0) return new Float32Array();
        let array = new Float32Array(length);
        for (let i = 0; i < length; i++) {
            array[i] = this.readFloat16();
        }
        return array;
    }

    readFloat24Array() {
        let length = this.readVarLength();
        if (length == -1) return null;
        if (length == 0) return new Float32Array();
        let array = new Float32Array(length);
        for (let i = 0; i < length; i++) {
            array[i] = this.readFloat24();
        }
        return array;
    }

    readFloat32Array() {
        let length = this.readVarLength();
        if (length == -1) return null;
        if (length == 0) return new Float32Array();
        let array = new Float32Array(length);
        for (let i = 0; i < length; i++) {
            array[i] = this.readFloat32();
        }
        return array;
    }

    readFloat64Array() {
        let length = this.readVarLength();
        if (length == -1) return null;
        if (length == 0) return new Float64Array();
        let array = new Float64Array(length);
        for (let i = 0; i < length; i++) {
            array[i] = this.readFloat64();
        }
        return array;
    }

    // _________________________ String Array _________________________
    readASCIIArray() {
        let length = this.readVarLength();
        if (length == -1) return null;
        if (length == 0) return [];
        let array = new Array(length);
        for (let i = 0; i < length; i++) {
            array[i] = this.readASCII();
        }
        return array;
    }

    readUnicodeArray() {
        let length = this.readVarLength();
        if (length == -1) return null;
        if (length == 0) return [];
        let array = new Array(length);
        for (let i = 0; i < length; i++) {
            array[i] = this.readUnicode();
        }
        return array;
    }

    readUTF8Array() {
        let length = this.readVarLength();
        if (length == -1) return null;
        if (length == 0) return [];
        let array = new Array(length);
        for (let i = 0; i < length; i++) {
            array[i] = this.readUTF8();
        }
        return array;
    }

    readVarUnicodeArray() {
        let length = this.readVarLength();
        if (length == -1) return null;
        if (length == 0) return [];
        let array = new Array(length);
        for (let i = 0; i < length; i++) {
            array[i] = this.readVarUnicode();
        }
        return array;
    }

    // _________________________ VarInt Array _________________________
    readVarInt16Array() {
        let length = this.readVarLength();
        if (length == -1) return null;
        if (length == 0) return new Int16Array();
        let array = new Int16Array(length);
        for (let i = 0; i < length; i++) {
            array[i] = this.readVarInt16();
        }
        return array;
    }

    readVarInt32Array() {
        let length = this.readVarLength();
        if (length == -1) return null;
        if (length == 0) return new Int32Array();
        let array = new Int32Array(length);
        for (let i = 0; i < length; i++) {
            array[i] = this.readVarInt32();
        }
        return array;
    }

    /*
    readVarInt64Array() {
        let length = this.readVarLength();
        if (length == -1) return null;
        if (length == 0) return new Int64Array();
        let array = new Int64Array(length);
        for (let i = 0; i < length; i++) {
            array[i] = this.readVarInt32();
        }
        return array;
    }
    */

    // _________________________ VarUInt Array _________________________
    readVarUInt16Array() {
        let length = this.readVarLength();
        if (length == -1) return null;
        if (length == 0) return new Uint16Array();
        let array = new Uint16Array(length);
        for (let i = 0; i < length; i++) {
            array[i] = this.readVarUInt16();
        }
        return array;
    }

    readVarUInt32Array() {
        let length = this.readVarLength();
        if (length == -1) return null;
        if (length == 0) return new Uint32Array();
        let array = new Uint32Array(length);
        for (let i = 0; i < length; i++) {
            array[i] = this.readVarUInt32();
        }
        return array;
    }

    /*
    readVarUInt64Array() {
        let length = this.readVarLength();
        if (length == -1) return null;
        if (length == 0) return new Uint64Array();
        let array = new Uint64Array(length);
        for (let i = 0; i < length; i++) {
            array[i] = this.readVarUInt32();
        }
        return array;
    }
    */

    // _________________________ API _________________________
    set bytes(value) {
        this._bytes = value;
        this._tempView = value == null ? null : new DataView(new ArrayBuffer(8));
        this.position = 0;
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

LBReader.prototype.BIT_LOCATION_VALUES = LBReaderStatic.BIT_LOCATION_VALUES;
LBReader.prototype.BIT1_LOCATION_VALUES = LBReaderStatic.BIT1_LOCATION_VALUES;