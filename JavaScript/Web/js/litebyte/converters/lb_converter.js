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
// Purpose: Convert object to bytes and bytes to object
// Author: ZhangYu
// CreateDate: 2020-01-09
// LastModifiedData: 2020-01-09


/** LiteByte Converter */
class LBConverter {

    // _________________________ 调用接口 | API _________________________
    /**
     * 把对象转成字节序列
     * @param {LBType} customType 自定义类型
     * @param {*} obj 对象
     * @returns {Uint8Array}
     */
    static toBytes(customType, obj) {
        try {
            this.writeCustomType(customType, obj);
            return this.writer.toBytes();
        } catch (e) {
            throw e;
        } finally {
            this.writer.erase();
        }
    }
    
    /**
     * 把字节序列还原成对象
     * @param {LBType} customType 自定义类型
     * @param {Uint8Array} bytes 字节序列 
     */
    static toObject(customType, bytes) {
        if (bytes == null || bytes.length == 0) return null;
        try {
            this.reader.bytes = bytes;
            return this.readCustomType(customType);
        } catch (e) {
            throw e;
        } finally {
            this.reader.dispose();
        }
    }

    // _________________________ 写入数据 | Write Data _________________________
    static writeBaseType(type, obj) {
        let value = obj[type.name];
        switch (type.baseType) {
            case LBBaseType.BIT1:
                this.writer.writeBit1(value);
                break;
            case LBBaseType.BIT2:
                this.writer.writeBit2(value);
                break;
            case LBBaseType.BIT3:
                this.writer.writeBit3(value);
                break;
            case LBBaseType.BIT4:
                this.writer.writeBit4(value);
                break;
            case LBBaseType.BIT5:
                this.writer.writeBit5(value);
                break;
            case LBBaseType.BIT6:
                this.writer.writeBit6(value);
                break;
            case LBBaseType.BIT7:
                this.writer.writeBit7(value);
                break;
            case LBBaseType.INT8:
                this.writer.writeInt8(value);
                break;
            case LBBaseType.INT16:
                this.writer.writeInt16(value);
                break;
            case LBBaseType.INT24:
                this.writer.writeInt24(value);
                break;
            case LBBaseType.INT32:
                this.writer.writeInt32(value);
                break;
            case LBBaseType.INT40:
                this.writer.writeInt40(value);
                break;
            case LBBaseType.INT48:
                this.writer.writeInt48(value);
                break;
            case LBBaseType.INT56:
                this.writer.writeInt56(value);
                break;
            case LBBaseType.INT64:
                this.writer.writeInt64(value);
                break;
            case LBBaseType.UINT8:
                this.writer.writeUInt8(value);
                break;
            case LBBaseType.UINT16:
                this.writer.writeUInt16(value);
                break;
            case LBBaseType.UINT24:
                this.writer.writeUInt24(value);
                break;
            case LBBaseType.UINT32:
                this.writer.writeUInt32(value);
                break;
            case LBBaseType.UINT40:
                this.writer.writeUInt40(value);
                break;
            case LBBaseType.UINT48:
                this.writer.writeUInt48(value);
                break;
            case LBBaseType.UINT56:
                this.writer.writeUInt56(value);
                break;
            case LBBaseType.UINT64:
                this.writer.writeUInt64(value);
                break;
            case LBBaseType.FLOAT8:
                this.writer.writeFloat8(value);
                break;
            case LBBaseType.FLOAT16:
                this.writer.writeFloat16(value);
                break;
            case LBBaseType.FLOAT24:
                this.writer.writeFloat24(value);
                break;
            case LBBaseType.FLOAT32:
                this.writer.writeFloat32(value);
                break;
            case LBBaseType.FLOAT64:
                this.writer.writeFloat64(value);
                break;
            case LBBaseType.VAR_INT16:
                this.writer.writeVarInt16(value);
                break;
            case LBBaseType.VAR_INT32:
                this.writer.writeVarInt32(value);
                break;
            case LBBaseType.VAR_INT64:
                this.writer.writeVarInt64(value);
                break;
            case LBBaseType.VAR_UINT16:
                this.writer.writeVarUInt16(value);
                break;
            case LBBaseType.VAR_UINT32:
                this.writer.writeVarUInt32(value);
                break;
            case LBBaseType.VAR_UINT64:
                this.writer.writeVarUInt64(value);
                break;
            case LBBaseType.ASCII:
                this.writer.writeASCII(value);
                break;
            case LBBaseType.UNICODE:
                this.writer.writeUnicode(value);
                break;
            case LBBaseType.UTF8:
                this.writer.writeUTF8(value);
                break;
            case LBBaseType.VAR_UNICODE:
                this.writer.writeVarUnicode(value);
                break;
            default:
                break;
        }
    }

    static writeCustomType(lbType, obj) {
        // 检查是否为空 | Check if it is null
        if (lbType.isClass) {
            if (obj == null) {
                this.writer.writeBit1(true);
                return;
            } else {
                this.writer.writeBit1(false);
            }
        }

        // 写入字段 | Write fields
        let fields = lbType.fieldTypes;
        for (let i = 0; i < fields.length; i++) {
            let field = fields[i];
            try {
                if (field.isBaseType) {
                    this.writeBaseType(field, obj);
                } else if (field.isArray) {
                    this.writeArray(field, obj);
                } else {
                    this.writeCustomType(field, obj[field.name]);
                }
            } catch (e) {
                let error = "Convert object." + field.name + " error! Field type:" + (field.type == null ? "null" : field.type) + "\n" + e;
                throw error;
            }
        }
    }

    static writeArray(type, obj) {
        if (type.elementType.isBaseType) {
            // 基本类型 | Base Type
            this.writeBaseTypeArray(type, obj);
        } else if (type.elementType.isArray) {
            // 数组套数组(不支持该格式) | Array in array(Not support)
            throw "Not support array in array! Type name:" + type.name;
        } else {
            // 自定义类型
            let array = obj[type.name];
            if (array == null) {
                this.writer.writeVarLength(-1);
            } else if (array.length == 0) {
                this.writer.writeVarLength(0);
            } else {
                this.writer.writeVarLength(array.length);
                let elementType = type.elementType;
                for (let i = 0; i < array.length; i++) {
                    this.writeCustomType(elementType, array[i]);
                }
            }
        }
    }

    static writeBaseTypeArray(type, obj) {
        let value = obj[type.name];
        switch (type.elementType.baseType) {
            case LBBaseType.BIT1:
                this.writer.writeBit1Array(value);
                break;
            case LBBaseType.BIT2:
                this.writer.writeBit2Array(value);
                break;
            case LBBaseType.BIT3:
                this.writer.writeBit3Array(value);
                break;
            case LBBaseType.BIT4:
                this.writer.writeBit4Array(value);
                break;
            case LBBaseType.BIT5:
                this.writer.writeBit5Array(value);
                break;
            case LBBaseType.BIT6:
                this.writer.writeBit6Array(value);
                break;
            case LBBaseType.BIT7:
                this.writer.writeBit7Array(value);
                break;
            case LBBaseType.INT8:
                this.writer.writeInt8Array(value);
                break;
            case LBBaseType.INT16:
                this.writer.writeInt16Array(value);
                break;
            case LBBaseType.INT24:
                this.writer.writeInt24Array(value);
                break;
            case LBBaseType.INT32:
                this.writer.writeInt32Array(value);
                break;
            case LBBaseType.INT40:
                this.writer.writeInt40Array(value);
                break;
            case LBBaseType.INT48:
                this.writer.writeInt48Array(value);
                break;
            case LBBaseType.INT56:
                this.writer.writeInt56Array(value);
                break;
            case LBBaseType.INT64:
                this.writer.writeInt64Array(value);
                break;
            case LBBaseType.UINT8:
                this.writer.writeUInt8Array(value);
                break;
            case LBBaseType.UINT16:
                this.writer.writeUInt16Array(value);
                break;
            case LBBaseType.UINT24:
                this.writer.writeUInt24Array(value);
                break;
            case LBBaseType.UINT32:
                this.writer.writeUInt32Array(value);
                break;
            case LBBaseType.UINT40:
                this.writer.writeUInt40Array(value);
                break;
            case LBBaseType.UINT48:
                this.writer.writeUInt48Array(value);
                break;
            case LBBaseType.UINT56:
                this.writer.writeUInt56Array(value);
                break;
            case LBBaseType.UINT64:
                this.writer.writeUInt64Array(value);
                break;
            case LBBaseType.FLOAT8:
                this.writer.writeFloat8Array(value);
                break;
            case LBBaseType.FLOAT16:
                this.writer.writeFloat16Array(value);
                break;
            case LBBaseType.FLOAT24:
                this.writer.writeFloat24Array(value);
                break;
            case LBBaseType.FLOAT32:
                this.writer.writeFloat32Array(value);
                break;
            case LBBaseType.FLOAT64:
                this.writer.writeFloat64Array(value);
                break;
            case LBBaseType.VAR_INT16:
                this.writer.writeVarInt16Array(value);
                break;
            case LBBaseType.VAR_INT32:
                this.writer.writeVarInt32Array(value);
                break;
            case LBBaseType.VAR_INT64:
                this.writer.writeVarInt64Array(value);
                break;
            case LBBaseType.VAR_UINT16:
                this.writer.writeVarUInt16Array(value);
                break;
            case LBBaseType.VAR_UINT32:
                this.writer.writeVarUInt32Array(value);
                break;
            case LBBaseType.VAR_UINT64:
                this.writer.writeVarUInt64Array(value);
                break;
            case LBBaseType.ASCII:
                this.writer.writeASCIIArray(value);
                break;
            case LBBaseType.UNICODE:
                this.writer.writeUnicodeArray(value);
                break;
            case LBBaseType.UTF8:
                this.writer.writeUTF8Array(value);
                break;
            case LBBaseType.VAR_UNICODE:
                this.writer.writeVarUnicodeArray(value);
                break;
            default:
                break;
        }
    }

    // _________________________ 读取数据 | Read Data _________________________
    static readBaseType(field) {
        let value;
        switch (field.baseType) {
            case LBBaseType.BIT1:
                value = this.reader.readBit1();
                break;
            case LBBaseType.BIT2:
                value = this.reader.readBit2();
                break;
            case LBBaseType.BIT3:
                value = this.reader.readBit3();
                break;
            case LBBaseType.BIT4:
                value = this.reader.readBit4();
                break;
            case LBBaseType.BIT5:
                value = this.reader.readBit5();
                break;
            case LBBaseType.BIT6:
                value = this.reader.readBit6();
                break;
            case LBBaseType.BIT7:
                value = this.reader.readBit7();
                break;
            case LBBaseType.INT8:
                value = this.reader.readInt8();
                break;
            case LBBaseType.INT16:
                value = this.reader.readInt16();
                break;
            case LBBaseType.INT24:
                value = this.reader.readInt24();
                break;
            case LBBaseType.INT32:
                value = this.reader.readInt32();
                break;
            case LBBaseType.INT40:
                value = this.reader.readInt40();
                break;
            case LBBaseType.INT48:
                value = this.reader.readInt48();
                break;
            case LBBaseType.INT56:
                value = this.reader.readInt56();
                break;
            case LBBaseType.INT64:
                value = this.reader.readInt64();
                break;
            case LBBaseType.UINT8:
                value = this.reader.readUInt8();
                break;
            case LBBaseType.UINT16:
                value = this.reader.readUInt16();
                break;
            case LBBaseType.UINT24:
                value = this.reader.readUInt24();
                break;
            case LBBaseType.UINT32:
                value = this.reader.readUInt32();
                break;
            case LBBaseType.UINT40:
                value = this.reader.readUInt40();
                break;
            case LBBaseType.UINT48:
                value = this.reader.readUInt48();
                break;
            case LBBaseType.UINT56:
                value = this.reader.readUInt56();
                break;
            case LBBaseType.UINT64:
                value = this.reader.readUInt64();
                break;
            case LBBaseType.FLOAT8:
                value = this.reader.readFloat8();
                break;
            case LBBaseType.FLOAT16:
                value = this.reader.readFloat16();
                break;
            case LBBaseType.FLOAT24:
                value = this.reader.readFloat24();
                break;
            case LBBaseType.FLOAT32:
                value = this.reader.readFloat32();
                break;
            case LBBaseType.FLOAT64:
                value = this.reader.readFloat64();
                break;
            case LBBaseType.VAR_INT16:
                value = this.reader.readVarInt16();
                break;
            case LBBaseType.VAR_INT32:
                value = this.reader.readVarInt32();
                break;
            case LBBaseType.VAR_INT64:
                value = this.reader.readVarInt64();
                break;
            case LBBaseType.VAR_UINT16:
                value = this.reader.readVarUInt16();
                break;
            case LBBaseType.VAR_UINT32:
                value = this.reader.readVarUInt32();
                break;
            case LBBaseType.VAR_UINT64:
                value = this.reader.readVarUInt64();
                break;
            case LBBaseType.ASCII:
                value = this.reader.readASCII();
                break;
            case LBBaseType.UNICODE:
                value = this.reader.readUnicode();
                break;
            case LBBaseType.UTF8:
                value = this.reader.readUTF8();
                break;
            case LBBaseType.VAR_UNICODE:
                value = this.reader.readVarUnicode();
                break;
            default:
                break;
        }
        return value;
    }

    static readCustomType(lbType) {
        // 如果是Class 检查是否为空 |  If class then check if null 
        if (lbType.isClass && this.reader.readBit1()) return null;

        // 读取字段信息 | Read field info
        let obj = {};
        let fields = lbType.fieldTypes;
        for (let i = 0; i < fields.length; i++) {
            let field = fields[i];
            try {
                if (field.isBaseType) {
                    obj[field.name] = this.readBaseType(field);
                } else if (field.isArray) {
                    obj[field.name] = this.readArray(field);
                } else {
                    obj[field.name] = this.readCustomType(field);
                }
            } catch (e) {
                let error = "Read Field:[" + field.type + " " + field.name + "] error!\n";
                error += "Struct:[" + lbType.name + "]\n" + e;
                throw new Exception(error);
            }
        }
        return obj;
    }

    static readArray(field) {
        if (field.elementType.isBaseType) {
            return this.readBaseTypeArray(field);
        } else if (field.elementType.isArray) {
            throw "Not support array in array! Type name:" + field.name;
        } else {
            return this.readCustomTypeArray(field);
        }
    }

    static readBaseTypeArray(field) {
        let array;
        switch (field.baseType) {
            case LBBaseType.BIT1:
                array = this.reader.readBit1Array();
                break;
            case LBBaseType.BIT2:
                array = this.reader.readBit2Array();
                break;
            case LBBaseType.BIT3:
                array = this.reader.readBit3Array();
                break;
            case LBBaseType.BIT4:
                array = this.reader.readBit4Array();
                break;
            case LBBaseType.BIT5:
                array = this.reader.readBit5Array();
                break;
            case LBBaseType.BIT6:
                array = this.reader.readBit6Array();
                break;
            case LBBaseType.BIT7:
                array = this.reader.readBit7Array();
                break;
            case LBBaseType.INT8:
                array = this.reader.readInt8Array();
                break;
            case LBBaseType.INT16:
                array = this.reader.readInt16Array();
                break;
            case LBBaseType.INT24:
                array = this.reader.readInt24Array();
                break;
            case LBBaseType.INT32:
                array = this.reader.readInt32Array();
                break;
            case LBBaseType.INT40:
                array = this.reader.readInt40Array();
                break;
            case LBBaseType.INT48:
                array = this.reader.readInt48Array();
                break;
            case LBBaseType.INT56:
                array = this.reader.readInt56Array();
                break;
            case LBBaseType.INT64:
                array = this.reader.readInt64Array();
                break;
            case LBBaseType.UINT8:
                array = this.reader.readUInt8Array();
                break;
            case LBBaseType.UINT16:
                array = this.reader.readUInt16Array();
                break;
            case LBBaseType.UINT24:
                array = this.reader.readUInt24Array();
                break;
            case LBBaseType.UINT32:
                array = this.reader.readUInt32Array();
                break;
            case LBBaseType.UINT40:
                array = this.reader.readUInt40Array();
                break;
            case LBBaseType.UINT48:
                array = this.reader.readUInt48Array();
                break;
            case LBBaseType.UINT56:
                array = this.reader.readUInt56Array();
                break;
            case LBBaseType.UINT64:
                array = this.reader.readUInt64Array();
                break;
            case LBBaseType.FLOAT8:
                array = this.reader.readFloat8Array();
                break;
            case LBBaseType.FLOAT16:
                array = this.reader.readFloat16Array();
                break;
            case LBBaseType.FLOAT24:
                array = this.reader.readFloat24Array();
                break;
            case LBBaseType.FLOAT32:
                array = this.reader.readFloat32Array();
                break;
            case LBBaseType.FLOAT64:
                array = this.reader.readFloat64Array();
                break;
            case LBBaseType.VAR_INT16:
                array = this.reader.readVarInt16Array();
                break;
            case LBBaseType.VAR_INT32:
                array = this.reader.readVarInt32Array();
                break;
            case LBBaseType.VAR_INT64:
                array = this.reader.readVarInt64Array();
                break;
            case LBBaseType.VAR_UINT16:
                array = this.reader.readVarUInt16Array();
                break;
            case LBBaseType.VAR_UINT32:
                array = this.reader.readVarUInt32Array();
                break;
            case LBBaseType.VAR_UINT64:
                array = this.reader.readVarUInt64Array();
                break;
            case LBBaseType.ASCII:
                array = this.reader.readASCIIArray();
                break;
            case LBBaseType.UNICODE:
                array = this.reader.readUnicodeArray();
                break;
            case LBBaseType.UTF8:
                array = this.reader.readUTF8Array();
                break;
            case LBBaseType.VAR_UNICODE:
                array = this.reader.readVarUnicodeArray();
                break;
            default:
                break;
        }
        return array;
    }

    static readCustomTypeArray(field) {
        let length = this.reader.readVarLength();
        if (length == -1) return null;
        if (length == 0) return [];
        let elementType = field.elementType;
        let array = new Array(length);
        for (let i = 0; i < length; i++) {
            array[i] = this.readCustomType(elementType);
        }
        return array;
    }

}

// 静态属性 | Static Properties
LBConverter.writer = new LBWriter(1024);
LBConverter.reader = new LBReader();