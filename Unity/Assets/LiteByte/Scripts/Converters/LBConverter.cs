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
// Purpose: Convert object to bytes and bytes to object
// Author: ZhangYu
// CreateDate: 2019-10-10
// LastModifiedData: 2019-12-24
#endregion
namespace LiteByte.Converters {

    using System;
    using System.Reflection;
    using System.Collections.Generic;
    using LiteByte.Common;

    /// <summary>
    /// <para>LiteByte Converter</para>
    /// </summary>
    public static class LBConverter {

        private static LBWriter writer = new LBWriter(1024);
        private static LBReader reader = new LBReader();

        // ===================================== 调用接口 | API =====================================
        #region To Bytes
        public static byte[] ToBytes(LBType customType, LBObject obj) {
            try {
                WriteCustomType(customType, obj);
                return writer.ToBytes();
            } catch (Exception e) {
                throw e;
            } finally {
                writer.Erase();
            }
        }

        public static byte[] ToBytes(LBType customType, object obj) {
            try {
                WriteCustomType(customType, obj);
                return writer.ToBytes();
            } catch (Exception e) {
                throw e;
            } finally {
                writer.Erase();
            }
        }
        #endregion

        #region To Object
        public static LBObject ToObject(LBType customType, byte[] bytes) {
            if (bytes == null || bytes.Length == 0) return null;
            try {
                reader.Bytes = bytes;
                return ReadCustomType(customType);
            } catch (Exception e) {
                throw e;
            } finally {
                reader.Dispose();
            }
        }

        public static T ToObject<T>(LBType customType, byte[] bytes) where T : new() {
            if (bytes == null || bytes.Length == 0) return default(T);
            try {
                reader.Bytes = bytes;
                return (T)ReadCustomType(customType, typeof(T));
            } catch (Exception e) {
                throw e;
            } finally {
                reader.Dispose();
            }
        }
        #endregion

        // ===================================== 写入数据 | Write Data =====================================
        #region (LBObject) Write BaseType
        private static void WriteBaseType(LBType type, LBObject obj) {
            switch (type.BaseType) {
                case LBBaseType.Bit1:
                    writer.WriteBit1(obj.GetBool(type.Name));
                    break;
                case LBBaseType.Bit2:
                    writer.WriteBit2(obj.GetByte(type.Name));
                    break;
                case LBBaseType.Bit3:
                    writer.WriteBit3(obj.GetByte(type.Name));
                    break;
                case LBBaseType.Bit4:
                    writer.WriteBit4(obj.GetByte(type.Name));
                    break;
                case LBBaseType.Bit5:
                    writer.WriteBit5(obj.GetByte(type.Name));
                    break;
                case LBBaseType.Bit6:
                    writer.WriteBit6(obj.GetByte(type.Name));
                    break;
                case LBBaseType.Bit7:
                    writer.WriteBit7(obj.GetByte(type.Name));
                    break;
                case LBBaseType.Int8:
                    writer.WriteInt8(obj.GetSByte(type.Name));
                    break;
                case LBBaseType.Int16:
                    writer.WriteInt16(obj.GetShort(type.Name));
                    break;
                case LBBaseType.Int24:
                    writer.WriteInt24(obj.GetInt(type.Name));
                    break;
                case LBBaseType.Int32:
                    writer.WriteInt32(obj.GetInt(type.Name));
                    break;
                case LBBaseType.Int40:
                    writer.WriteInt40(obj.GetLong(type.Name));
                    break;
                case LBBaseType.Int48:
                    writer.WriteInt48(obj.GetLong(type.Name));
                    break;
                case LBBaseType.Int56:
                    writer.WriteInt56(obj.GetLong(type.Name));
                    break;
                case LBBaseType.Int64:
                    writer.WriteInt64(obj.GetLong(type.Name));
                    break;
                case LBBaseType.UInt8:
                    writer.WriteUInt8(obj.GetByte(type.Name));
                    break;
                case LBBaseType.UInt16:
                    writer.WriteUInt16(obj.GetUShort(type.Name));
                    break;
                case LBBaseType.UInt24:
                    writer.WriteUInt24(obj.GetUInt(type.Name));
                    break;
                case LBBaseType.UInt32:
                    writer.WriteUInt32(obj.GetUInt(type.Name));
                    break;
                case LBBaseType.UInt40:
                    writer.WriteUInt40(obj.GetULong(type.Name));
                    break;
                case LBBaseType.UInt48:
                    writer.WriteUInt48(obj.GetULong(type.Name));
                    break;
                case LBBaseType.UInt56:
                    writer.WriteUInt56(obj.GetULong(type.Name));
                    break;
                case LBBaseType.UInt64:
                    writer.WriteUInt64(obj.GetULong(type.Name));
                    break;
                case LBBaseType.Float8:
                    writer.WriteFloat8(obj.GetFloat(type.Name));
                    break;
                case LBBaseType.Float16:
                    writer.WriteFloat16(obj.GetFloat(type.Name));
                    break;
                case LBBaseType.Float24:
                    writer.WriteFloat24(obj.GetFloat(type.Name));
                    break;
                case LBBaseType.Float32:
                    writer.WriteFloat32(obj.GetFloat(type.Name));
                    break;
                case LBBaseType.Float64:
                    writer.WriteFloat64(obj.GetDouble(type.Name));
                    break;
                case LBBaseType.VarInt16:
                    writer.WriteVarInt16(obj.GetShort(type.Name));
                    break;
                case LBBaseType.VarInt32:
                    writer.WriteVarInt32(obj.GetInt(type.Name));
                    break;
                case LBBaseType.VarInt64:
                    writer.WriteVarInt64(obj.GetLong(type.Name));
                    break;
                case LBBaseType.VarUInt16:
                    writer.WriteVarUInt16(obj.GetUShort(type.Name));
                    break;
                case LBBaseType.VarUInt32:
                    writer.WriteVarUInt32(obj.GetUInt(type.Name));
                    break;
                case LBBaseType.VarUInt64:
                    writer.WriteVarUInt64(obj.GetULong(type.Name));
                    break;
                case LBBaseType.UTF8:
                    writer.WriteUTF8(obj.GetString(type.Name));
                    break;
                case LBBaseType.Unicode:
                    writer.WriteUnicode(obj.GetString(type.Name));
                    break;
                case LBBaseType.ASCII:
                    writer.WriteASCII(obj.GetString(type.Name));
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region (LBObject) Write CustomType
        private static void WriteCustomType(LBType lbType, LBObject obj) {
            // 检查是否为空 | Check if it is null
            if (lbType.IsClass) {
                if (obj == null) {
                    writer.WriteBit1(true);
                    return;
                } else {
                    writer.WriteBit1(false);
                }
            }

            // 写入字段 | Write fields
            List<LBType> fields = lbType.FieldTypes;
            for (int i = 0; i < fields.Count; i++) {
                LBType field = fields[i];
                try {
                    if (field.IsBaseType) {
                        WriteBaseType(field, obj);
                    } else if (field.IsArray) {
                        WriteArray(field, obj);
                    } else {
                        WriteCustomType(field, obj.GetObject(field.Name));
                    }
                } catch (Exception e) {
                    string error = "Convert LBObject.{0} error! Field type:[{1}]\n{2}";
                    error = string.Format(error, field.Name, field.Type == null ? "null" : field.Type, e);
                    throw new Exception(error);
                }
            }
        }
        #endregion

        #region (LBObject) Write BaseType Array
        private static void WriteBaseTypeArray(LBType type, LBObject obj) {
            switch (type.ElementType.BaseType) {
                case LBBaseType.Bit1:
                    writer.WriteBit1Array(obj.GetBoolArray(type.Name));
                    break;
                case LBBaseType.Bit2:
                    writer.WriteBit2Array(obj.GetByteArray(type.Name));
                    break;
                case LBBaseType.Bit3:
                    writer.WriteBit3Array(obj.GetByteArray(type.Name));
                    break;
                case LBBaseType.Bit4:
                    writer.WriteBit4Array(obj.GetByteArray(type.Name));
                    break;
                case LBBaseType.Bit5:
                    writer.WriteBit5Array(obj.GetByteArray(type.Name));
                    break;
                case LBBaseType.Bit6:
                    writer.WriteBit6Array(obj.GetByteArray(type.Name));
                    break;
                case LBBaseType.Bit7:
                    writer.WriteBit7Array(obj.GetByteArray(type.Name));
                    break;
                case LBBaseType.Int8:
                    writer.WriteInt8Array(obj.GetSByteArray(type.Name));
                    break;
                case LBBaseType.Int16:
                    writer.WriteInt16Array(obj.GetShortArray(type.Name));
                    break;
                case LBBaseType.Int24:
                    writer.WriteInt24Array(obj.GetIntArray(type.Name));
                    break;
                case LBBaseType.Int32:
                    writer.WriteInt32Array(obj.GetIntArray(type.Name));
                    break;
                case LBBaseType.Int40:
                    writer.WriteInt40Array(obj.GetLongArray(type.Name));
                    break;
                case LBBaseType.Int48:
                    writer.WriteInt48Array(obj.GetLongArray(type.Name));
                    break;
                case LBBaseType.Int56:
                    writer.WriteInt56Array(obj.GetLongArray(type.Name));
                    break;
                case LBBaseType.Int64:
                    writer.WriteInt64Array(obj.GetLongArray(type.Name));
                    break;
                case LBBaseType.UInt8:
                    writer.WriteUInt8Array(obj.GetByteArray(type.Name));
                    break;
                case LBBaseType.UInt16:
                    writer.WriteUInt16Array(obj.GetUShortArray(type.Name));
                    break;
                case LBBaseType.UInt24:
                    writer.WriteUInt24Array(obj.GetUIntArray(type.Name));
                    break;
                case LBBaseType.UInt32:
                    writer.WriteUInt32Array(obj.GetUIntArray(type.Name));
                    break;
                case LBBaseType.UInt40:
                    writer.WriteUInt40Array(obj.GetULongArray(type.Name));
                    break;
                case LBBaseType.UInt48:
                    writer.WriteUInt48Array(obj.GetULongArray(type.Name));
                    break;
                case LBBaseType.UInt56:
                    writer.WriteUInt56Array(obj.GetULongArray(type.Name));
                    break;
                case LBBaseType.UInt64:
                    writer.WriteUInt64Array(obj.GetULongArray(type.Name));
                    break;
                case LBBaseType.Float8:
                    writer.WriteFloat8Array(obj.GetFloatArray(type.Name));
                    break;
                case LBBaseType.Float16:
                    writer.WriteFloat16Array(obj.GetFloatArray(type.Name));
                    break;
                case LBBaseType.Float24:
                    writer.WriteFloat24Array(obj.GetFloatArray(type.Name));
                    break;
                case LBBaseType.Float32:
                    writer.WriteFloat32Array(obj.GetFloatArray(type.Name));
                    break;
                case LBBaseType.Float64:
                    writer.WriteFloat64Array(obj.GetDoubleArray(type.Name));
                    break;
                case LBBaseType.VarInt16:
                    writer.WriteVarInt16Array(obj.GetShortArray(type.Name));
                    break;
                case LBBaseType.VarInt32:
                    writer.WriteVarInt32Array(obj.GetIntArray(type.Name));
                    break;
                case LBBaseType.VarInt64:
                    writer.WriteVarInt64Array(obj.GetLongArray(type.Name));
                    break;
                case LBBaseType.VarUInt16:
                    writer.WriteVarUInt16Array(obj.GetUShortArray(type.Name));
                    break;
                case LBBaseType.VarUInt32:
                    writer.WriteVarUInt32Array(obj.GetUIntArray(type.Name));
                    break;
                case LBBaseType.VarUInt64:
                    writer.WriteVarUInt64Array(obj.GetULongArray(type.Name));
                    break;
                case LBBaseType.UTF8:
                    writer.WriteUTF8Array(obj.GetStringArray(type.Name));
                    break;
                case LBBaseType.Unicode:
                    writer.WriteUnicodeArray(obj.GetStringArray(type.Name));
                    break;
                case LBBaseType.ASCII:
                    writer.WriteASCIIArray(obj.GetStringArray(type.Name));
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region (LBObject) Write Array
        private static void WriteArray(LBType type, LBObject obj) {
            if (type.ElementType.IsBaseType) {
                // 基本类型 | Base Type
                WriteBaseTypeArray(type, obj);
            } else if (type.ElementType.IsArray) {
                // 数组套数组(不支持该格式) | Array in array(Not support)
                throw new Exception("Not support array in array! Type name:" + type.Name);
            } else {
                // 自定义类型
                LBObject[] array = obj.GetObjectArray(type.Name);
                if (array == null) {
                    writer.WriteVarLength(-1);
                } else if (array.Length == 0) {
                    writer.WriteVarLength(0);
                } else {
                    writer.WriteVarLength(array.Length);
                    LBType elementType = type.ElementType;
                    for (int i = 0; i < array.Length; i++) {
                        WriteCustomType(elementType, array[i]);
                    }
                }
            }
        }
        #endregion

        #region (System.Object) Write BaseType
        private static void WriteBaseType(LBBaseType baseType, object value) {
            switch (baseType) {
                case LBBaseType.Bit1:
                    writer.WriteBit1((bool)value);
                    break;
                case LBBaseType.Bit2:
                    writer.WriteBit2((byte)value);
                    break;
                case LBBaseType.Bit3:
                    writer.WriteBit3((byte)value);
                    break;
                case LBBaseType.Bit4:
                    writer.WriteBit4((byte)value);
                    break;
                case LBBaseType.Bit5:
                    writer.WriteBit5((byte)value);
                    break;
                case LBBaseType.Bit6:
                    writer.WriteBit6((byte)value);
                    break;
                case LBBaseType.Bit7:
                    writer.WriteBit7((byte)value);
                    break;
                case LBBaseType.Int8:
                    writer.WriteInt8((sbyte)value);
                    break;
                case LBBaseType.Int16:
                    writer.WriteInt16((short)value);
                    break;
                case LBBaseType.Int24:
                    writer.WriteInt24((int)value);
                    break;
                case LBBaseType.Int32:
                    writer.WriteInt32((int)value);
                    break;
                case LBBaseType.Int40:
                    writer.WriteInt40((long)value);
                    break;
                case LBBaseType.Int48:
                    writer.WriteInt48((long)value);
                    break;
                case LBBaseType.Int56:
                    writer.WriteInt56((long)value);
                    break;
                case LBBaseType.Int64:
                    writer.WriteInt64((long)value);
                    break;
                case LBBaseType.UInt8:
                    writer.WriteUInt8((byte)value);
                    break;
                case LBBaseType.UInt16:
                    writer.WriteUInt16((ushort)value);
                    break;
                case LBBaseType.UInt24:
                    writer.WriteUInt24((uint)value);
                    break;
                case LBBaseType.UInt32:
                    writer.WriteUInt32((uint)value);
                    break;
                case LBBaseType.UInt40:
                    writer.WriteUInt40((ulong)value);
                    break;
                case LBBaseType.UInt48:
                    writer.WriteUInt48((ulong)value);
                    break;
                case LBBaseType.UInt56:
                    writer.WriteUInt56((ulong)value);
                    break;
                case LBBaseType.UInt64:
                    writer.WriteUInt64((ulong)value);
                    break;
                case LBBaseType.Float8:
                    writer.WriteFloat8((float)value);
                    break;
                case LBBaseType.Float16:
                    writer.WriteFloat16((float)value);
                    break;
                case LBBaseType.Float24:
                    writer.WriteFloat24((float)value);
                    break;
                case LBBaseType.Float32:
                    writer.WriteFloat32((float)value);
                    break;
                case LBBaseType.Float64:
                    writer.WriteFloat64((double)value);
                    break;
                case LBBaseType.VarInt16:
                    writer.WriteVarInt16((short)value);
                    break;
                case LBBaseType.VarInt32:
                    writer.WriteVarInt32((int)value);
                    break;
                case LBBaseType.VarInt64:
                    writer.WriteVarInt64((long)value);
                    break;
                case LBBaseType.VarUInt16:
                    writer.WriteVarUInt16((ushort)value);
                    break;
                case LBBaseType.VarUInt32:
                    writer.WriteVarUInt32((uint)value);
                    break;
                case LBBaseType.VarUInt64:
                    writer.WriteVarUInt64((ulong)value);
                    break;
                case LBBaseType.UTF8:
                    writer.WriteUTF8((string)value);
                    break;
                case LBBaseType.Unicode:
                    writer.WriteUnicode((string)value);
                    break;
                case LBBaseType.ASCII:
                    writer.WriteASCII((string)value);
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region (System.Object) Write Custom Type
        private static void WriteCustomType(LBType lbType, object obj) {
            // 检查是否为空 | Check if it is null
            if (lbType.IsClass) {
                if (obj == null) {
                    writer.WriteBit1(true);
                    return;
                } else {
                    writer.WriteBit1(false);
                }
            }

            // 写入字段 | Write fields
            Type objType = obj.GetType();
            List<LBType> fields = lbType.FieldTypes;
            FieldInfo[] fieldsInfos = LBReflectCache.GetFieldInfos(objType, fields);
            for (int i = 0; i < fields.Count; i++) {
                LBType field = fields[i];
                FieldInfo fieldInfo = fieldsInfos[i];
                if (fieldInfo == null) {
                    string error = "Object.{0} is null! ObjectType:[{1}]";
                    throw new Exception(string.Format(error, field.Name, objType));
                }
                object value = fieldInfo.GetValue(obj);
                try {
                    if (field.IsBaseType) {
                        WriteBaseType(field.BaseType, value);
                    } else if (field.IsArray) {
                        WriteArray(field, (Array)value);
                    } else {
                        WriteCustomType(field, value);
                    }
                } catch (Exception e) {
                    string error = "Convert object.{0} error! Convert value[{1}] to type[{2}] error.\nObjectType:[{3}]\n{4}";
                    error = string.Format(error, field.Name, value == null ? "null" : value, field.BaseType, objType, e);
                    throw new Exception(error);
                }
            }
        }
        #endregion

        #region (System.Object) Write BaseType Array
        private static void WriteBaseTypeArray(LBBaseType baseType, Array array) {
            switch (baseType) {
                case LBBaseType.Bit1:
                    writer.WriteBit1Array((bool[])array);
                    break;
                case LBBaseType.Bit2:
                    writer.WriteBit2Array((byte[])array);
                    break;
                case LBBaseType.Bit3:
                    writer.WriteBit3Array((byte[])array);
                    break;
                case LBBaseType.Bit4:
                    writer.WriteBit4Array((byte[])array);
                    break;
                case LBBaseType.Bit5:
                    writer.WriteBit5Array((byte[])array);
                    break;
                case LBBaseType.Bit6:
                    writer.WriteBit6Array((byte[])array);
                    break;
                case LBBaseType.Bit7:
                    writer.WriteBit7Array((byte[])array);
                    break;
                case LBBaseType.Int8:
                    writer.WriteInt8Array((sbyte[])array);
                    break;
                case LBBaseType.Int16:
                    writer.WriteInt16Array((short[])array);
                    break;
                case LBBaseType.Int24:
                    writer.WriteInt24Array((int[])array);
                    break;
                case LBBaseType.Int32:
                    writer.WriteInt32Array((int[])array);
                    break;
                case LBBaseType.Int40:
                    writer.WriteInt40Array((long[])array);
                    break;
                case LBBaseType.Int48:
                    writer.WriteInt48Array((long[])array);
                    break;
                case LBBaseType.Int56:
                    writer.WriteInt56Array((long[])array);
                    break;
                case LBBaseType.Int64:
                    writer.WriteInt64Array((long[])array);
                    break;
                case LBBaseType.UInt8:
                    writer.WriteUInt8Array((byte[])array);
                    break;
                case LBBaseType.UInt16:
                    writer.WriteUInt16Array((ushort[])array);
                    break;
                case LBBaseType.UInt24:
                    writer.WriteUInt24Array((uint[])array);
                    break;
                case LBBaseType.UInt32:
                    writer.WriteUInt32Array((uint[])array);
                    break;
                case LBBaseType.UInt40:
                    writer.WriteUInt40Array((ulong[])array);
                    break;
                case LBBaseType.UInt48:
                    writer.WriteUInt48Array((ulong[])array);
                    break;
                case LBBaseType.UInt56:
                    writer.WriteUInt56Array((ulong[])array);
                    break;
                case LBBaseType.UInt64:
                    writer.WriteUInt64Array((ulong[])array);
                    break;
                case LBBaseType.Float8:
                    writer.WriteFloat8Array((float[])array);
                    break;
                case LBBaseType.Float16:
                    writer.WriteFloat16Array((float[])array);
                    break;
                case LBBaseType.Float24:
                    writer.WriteFloat24Array((float[])array);
                    break;
                case LBBaseType.Float32:
                    writer.WriteFloat32Array((float[])array);
                    break;
                case LBBaseType.Float64:
                    writer.WriteFloat64Array((double[])array);
                    break;
                case LBBaseType.VarInt16:
                    writer.WriteVarInt16Array((short[])array);
                    break;
                case LBBaseType.VarInt32:
                    writer.WriteVarInt32Array((int[])array);
                    break;
                case LBBaseType.VarInt64:
                    writer.WriteVarInt64Array((long[])array);
                    break;
                case LBBaseType.VarUInt16:
                    writer.WriteVarUInt16Array((ushort[])array);
                    break;
                case LBBaseType.VarUInt32:
                    writer.WriteVarUInt32Array((uint[])array);
                    break;
                case LBBaseType.VarUInt64:
                    writer.WriteVarUInt64Array((ulong[])array);
                    break;
                case LBBaseType.UTF8:
                    writer.WriteUTF8Array((string[])array);
                    break;
                case LBBaseType.Unicode:
                    writer.WriteUnicodeArray((string[])array);
                    break;
                case LBBaseType.ASCII:
                    writer.WriteASCIIArray((string[])array);
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region (System.Object) Wirte Array
        private static void WriteArray(LBType type, Array array) {
            if (array == null) {
                writer.WriteVarLength(-1);
            } else if (array.Length == 0) {
                writer.WriteVarLength(0);
            } else {
                LBType elementType = type.ElementType;
                if (elementType.IsBaseType) {
                    // 基本类型 | Base Type
                    WriteBaseTypeArray(elementType.BaseType, array);
                } else if (elementType.IsArray) {
                    // 数组 | Array
                    writer.WriteVarLength(array.Length);
                    for (int i = 0; i < array.Length; i++) {
                        WriteArray(elementType, (Array)array.GetValue(i));
                    }
                } else {
                    // 自定义类型
                    writer.WriteVarLength(array.Length);
                    for (int i = 0; i < array.Length; i++) {
                        WriteCustomType(elementType, array.GetValue(i));
                    }
                }
            }
        }
        #endregion

        // ===================================== 读取数据 | Read Data =====================================
        #region (LBObject) Read BaseType
        private static void ReadBaseType(LBType field, LBObject obj) {
            switch (field.BaseType) {
                case LBBaseType.Bit1:
                    obj.SetBool(field.Name, reader.ReadBit1());
                    break;
                case LBBaseType.Bit2:
                    obj.SetByte(field.Name, reader.ReadBit2());
                    break;
                case LBBaseType.Bit3:
                    obj.SetByte(field.Name, reader.ReadBit3());
                    break;
                case LBBaseType.Bit4:
                    obj.SetByte(field.Name, reader.ReadBit4());
                    break;
                case LBBaseType.Bit5:
                    obj.SetByte(field.Name, reader.ReadBit5());
                    break;
                case LBBaseType.Bit6:
                    obj.SetByte(field.Name, reader.ReadBit6());
                    break;
                case LBBaseType.Bit7:
                    obj.SetByte(field.Name, reader.ReadBit7());
                    break;
                case LBBaseType.Int8:
                    obj.SetSByte(field.Name, reader.ReadInt8());
                    break;
                case LBBaseType.Int16:
                    obj.SetShort(field.Name, reader.ReadInt16());
                    break;
                case LBBaseType.Int24:
                    obj.SetInt(field.Name, reader.ReadInt24());
                    break;
                case LBBaseType.Int32:
                    obj.SetInt(field.Name, reader.ReadInt32());
                    break;
                case LBBaseType.Int40:
                    obj.SetLong(field.Name, reader.ReadInt40());
                    break;
                case LBBaseType.Int48:
                    obj.SetLong(field.Name, reader.ReadInt48());
                    break;
                case LBBaseType.Int56:
                    obj.SetLong(field.Name, reader.ReadInt56());
                    break;
                case LBBaseType.Int64:
                    obj.SetLong(field.Name, reader.ReadInt64());
                    break;
                case LBBaseType.UInt8:
                    obj.SetByte(field.Name, reader.ReadUInt8());
                    break;
                case LBBaseType.UInt16:
                    obj.SetUShort(field.Name, reader.ReadUInt16());
                    break;
                case LBBaseType.UInt24:
                    obj.SetUInt(field.Name, reader.ReadUInt24());
                    break;
                case LBBaseType.UInt32:
                    obj.SetUInt(field.Name, reader.ReadUInt32());
                    break;
                case LBBaseType.UInt40:
                    obj.SetULong(field.Name, reader.ReadUInt40());
                    break;
                case LBBaseType.UInt48:
                    obj.SetULong(field.Name, reader.ReadUInt48());
                    break;
                case LBBaseType.UInt56:
                    obj.SetULong(field.Name, reader.ReadUInt56());
                    break;
                case LBBaseType.UInt64:
                    obj.SetULong(field.Name, reader.ReadUInt64());
                    break;
                case LBBaseType.Float8:
                    obj.SetFloat(field.Name, reader.ReadFloat8());
                    break;
                case LBBaseType.Float16:
                    obj.SetFloat(field.Name, reader.ReadFloat16());
                    break;
                case LBBaseType.Float24:
                    obj.SetFloat(field.Name, reader.ReadFloat24());
                    break;
                case LBBaseType.Float32:
                    obj.SetFloat(field.Name, reader.ReadFloat32());
                    break;
                case LBBaseType.Float64:
                    obj.SetDouble(field.Name, reader.ReadFloat64());
                    break;
                case LBBaseType.VarInt16:
                    obj.SetShort(field.Name, reader.ReadVarInt16());
                    break;
                case LBBaseType.VarInt32:
                    obj.SetInt(field.Name, reader.ReadVarInt32());
                    break;
                case LBBaseType.VarInt64:
                    obj.SetLong(field.Name, reader.ReadVarInt64());
                    break;
                case LBBaseType.VarUInt16:
                    obj.SetUShort(field.Name, reader.ReadVarUInt16());
                    break;
                case LBBaseType.VarUInt32:
                    obj.SetUInt(field.Name, reader.ReadVarUInt32());
                    break;
                case LBBaseType.VarUInt64:
                    obj.SetULong(field.Name, reader.ReadVarUInt64());
                    break;
                case LBBaseType.UTF8:
                    obj.SetString(field.Name, reader.ReadUTF8());
                    break;
                case LBBaseType.Unicode:
                    obj.SetString(field.Name, reader.ReadUnicode());
                    break;
                case LBBaseType.ASCII:
                    obj.SetString(field.Name, reader.ReadASCII());
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region (LBObject) Read CustomType
        private static LBObject ReadCustomType(LBType lbType) {
            // 如果是Class 检查是否为空 |  If class then check if null 
            if (lbType.IsClass) if (reader.ReadBit1()) return null;

            // 读取字段信息 | Read field info
            LBObject obj = new LBObject();
            List<LBType> fields = lbType.FieldTypes;
            for (int i = 0; i < fields.Count; i++) {
                LBType field = fields[i];
                try {
                    if (field.IsBaseType) {
                        ReadBaseType(field, obj);
                    } else if (field.IsArray) {
                        ReadArray(field, obj);
                    } else {
                        obj.SetObject(field.Name, ReadCustomType(field));
                    }
                } catch (Exception e) {
                    string error = "Read Field:[{0} {1}] error!\n Struct:[{2}]\n{3}";
                    error = string.Format(error, field.Type, field.Name, lbType.Name, e);
                    throw new Exception(error);
                }
            }
            return obj;
        }

        // 读取自定义类型数组 | Read custom type array
        private static void ReadCustomTypeArray(LBType field, LBObject obj) {
            int length = reader.ReadVarLength();
            if (length == -1) return;
            if (length == 0) {
                obj.SetObjectArray(field.Name, new LBObject[0]);
            } else {
                LBType elementType = field.ElementType;
                LBObject[] array = new LBObject[length];
                for (int i = 0; i < length; i++) {
                    array[i] = ReadCustomType(elementType);
                }
                obj.SetObjectArray(field.Name, array);
            }
        }
        #endregion

        #region (LBObject) Read BaseType Array
        private static void ReadBaseTypeArray(LBType type, LBObject obj) {
            switch (type.ElementType.BaseType) {
                case LBBaseType.Bit1:
                    obj.SetBoolArray(type.Name, reader.ReadBit1Array());
                    break;
                case LBBaseType.Bit2:
                    obj.SetByteArray(type.Name, reader.ReadBit2Array());
                    break;
                case LBBaseType.Bit3:
                    obj.SetByteArray(type.Name, reader.ReadBit3Array());
                    break;
                case LBBaseType.Bit4:
                    obj.SetByteArray(type.Name, reader.ReadBit4Array());
                    break;
                case LBBaseType.Bit5:
                    obj.SetByteArray(type.Name, reader.ReadBit5Array());
                    break;
                case LBBaseType.Bit6:
                    obj.SetByteArray(type.Name, reader.ReadBit6Array());
                    break;
                case LBBaseType.Bit7:
                    obj.SetByteArray(type.Name, reader.ReadBit7Array());
                    break;
                case LBBaseType.Int8:
                    obj.SetSByteArray(type.Name, reader.ReadInt8Array());
                    break;
                case LBBaseType.Int16:
                    obj.SetShortArray(type.Name, reader.ReadInt16Array());
                    break;
                case LBBaseType.Int24:
                    obj.SetIntArray(type.Name, reader.ReadInt24Array());
                    break;
                case LBBaseType.Int32:
                    obj.SetIntArray(type.Name, reader.ReadInt32Array());
                    break;
                case LBBaseType.Int40:
                    obj.SetLongArray(type.Name, reader.ReadInt40Array());
                    break;
                case LBBaseType.Int48:
                    obj.SetLongArray(type.Name, reader.ReadInt48Array());
                    break;
                case LBBaseType.Int56:
                    obj.SetLongArray(type.Name, reader.ReadInt56Array());
                    break;
                case LBBaseType.Int64:
                    obj.SetLongArray(type.Name, reader.ReadInt64Array());
                    break;
                case LBBaseType.UInt8:
                    obj.SetByteArray(type.Name, reader.ReadUInt8Array());
                    break;
                case LBBaseType.UInt16:
                    obj.SetUShortArray(type.Name, reader.ReadUInt16Array());
                    break;
                case LBBaseType.UInt24:
                    obj.SetUIntArray(type.Name, reader.ReadUInt24Array());
                    break;
                case LBBaseType.UInt32:
                    obj.SetUIntArray(type.Name, reader.ReadUInt32Array());
                    break;
                case LBBaseType.UInt40:
                    obj.SetULongArray(type.Name, reader.ReadUInt40Array());
                    break;
                case LBBaseType.UInt48:
                    obj.SetULongArray(type.Name, reader.ReadUInt48Array());
                    break;
                case LBBaseType.UInt56:
                    obj.SetULongArray(type.Name, reader.ReadUInt56Array());
                    break;
                case LBBaseType.UInt64:
                    obj.SetULongArray(type.Name, reader.ReadUInt64Array());
                    break;
                case LBBaseType.Float8:
                    obj.SetFloatArray(type.Name, reader.ReadFloat8Array());
                    break;
                case LBBaseType.Float16:
                    obj.SetFloatArray(type.Name, reader.ReadFloat16Array());
                    break;
                case LBBaseType.Float24:
                    obj.SetFloatArray(type.Name, reader.ReadFloat24Array());
                    break;
                case LBBaseType.Float32:
                    obj.SetFloatArray(type.Name, reader.ReadFloat32Array());
                    break;
                case LBBaseType.Float64:
                    obj.SetDoubleArray(type.Name, reader.ReadFloat64Array());
                    break;
                case LBBaseType.VarInt16:
                    obj.SetShortArray(type.Name, reader.ReadVarInt16Array());
                    break;
                case LBBaseType.VarInt32:
                    obj.SetIntArray(type.Name, reader.ReadVarInt32Array());
                    break;
                case LBBaseType.VarInt64:
                    obj.SetLongArray(type.Name, reader.ReadVarInt64Array());
                    break;
                case LBBaseType.VarUInt16:
                    obj.SetUShortArray(type.Name, reader.ReadVarUInt16Array());
                    break;
                case LBBaseType.VarUInt32:
                    obj.SetUIntArray(type.Name, reader.ReadVarUInt32Array());
                    break;
                case LBBaseType.VarUInt64:
                    obj.SetULongArray(type.Name, reader.ReadVarUInt64Array());
                    break;
                case LBBaseType.UTF8:
                    obj.SetStringArray(type.Name, reader.ReadUTF8Array());
                    break;
                case LBBaseType.Unicode:
                    obj.SetStringArray(type.Name, reader.ReadUnicodeArray());
                    break;
                case LBBaseType.ASCII:
                    obj.SetStringArray(type.Name, reader.ReadASCIIArray());
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region (LBObject) Read Array
        private static void ReadArray(LBType field, LBObject obj) {
            if (field.ElementType.IsBaseType) {
                ReadBaseTypeArray(field, obj);
            } else if (field.ElementType.IsArray) {
                throw new Exception("Not support array in array! Type name:" + field.Name);
            } else {
                ReadCustomTypeArray(field, obj);
            }
        }
        #endregion

        #region (System.Object) Read BaseType
        private static void ReadBaseType(LBType field, object obj, FieldInfo fieldInfo) {
            switch (field.BaseType) {
                case LBBaseType.Bit1:
                    fieldInfo.SetValue(obj, reader.ReadBit1());
                    break;
                case LBBaseType.Bit2:
                    fieldInfo.SetValue(obj, reader.ReadBit2());
                    break;
                case LBBaseType.Bit3:
                    fieldInfo.SetValue(obj, reader.ReadBit3());
                    break;
                case LBBaseType.Bit4:
                    fieldInfo.SetValue(obj, reader.ReadBit4());
                    break;
                case LBBaseType.Bit5:
                    fieldInfo.SetValue(obj, reader.ReadBit5());
                    break;
                case LBBaseType.Bit6:
                    fieldInfo.SetValue(obj, reader.ReadBit6());
                    break;
                case LBBaseType.Bit7:
                    fieldInfo.SetValue(obj, reader.ReadBit7());
                    break;
                case LBBaseType.Int8:
                    fieldInfo.SetValue(obj, reader.ReadInt8());
                    break;
                case LBBaseType.Int16:
                    fieldInfo.SetValue(obj, reader.ReadInt16());
                    break;
                case LBBaseType.Int24:
                    fieldInfo.SetValue(obj, reader.ReadInt24());
                    break;
                case LBBaseType.Int32:
                    fieldInfo.SetValue(obj, reader.ReadInt32());
                    break;
                case LBBaseType.Int40:
                    fieldInfo.SetValue(obj, reader.ReadInt40());
                    break;
                case LBBaseType.Int48:
                    fieldInfo.SetValue(obj, reader.ReadInt48());
                    break;
                case LBBaseType.Int56:
                    fieldInfo.SetValue(obj, reader.ReadInt56());
                    break;
                case LBBaseType.Int64:
                    fieldInfo.SetValue(obj, reader.ReadInt64());
                    break;
                case LBBaseType.UInt8:
                    fieldInfo.SetValue(obj, reader.ReadUInt8());
                    break;
                case LBBaseType.UInt16:
                    fieldInfo.SetValue(obj, reader.ReadUInt16());
                    break;
                case LBBaseType.UInt24:
                    fieldInfo.SetValue(obj, reader.ReadUInt24());
                    break;
                case LBBaseType.UInt32:
                    fieldInfo.SetValue(obj, reader.ReadUInt32());
                    break;
                case LBBaseType.UInt40:
                    fieldInfo.SetValue(obj, reader.ReadUInt40());
                    break;
                case LBBaseType.UInt48:
                    fieldInfo.SetValue(obj, reader.ReadUInt48());
                    break;
                case LBBaseType.UInt56:
                    fieldInfo.SetValue(obj, reader.ReadUInt56());
                    break;
                case LBBaseType.UInt64:
                    fieldInfo.SetValue(obj, reader.ReadUInt64());
                    break;
                case LBBaseType.Float8:
                    fieldInfo.SetValue(obj, reader.ReadFloat8());
                    break;
                case LBBaseType.Float16:
                    fieldInfo.SetValue(obj, reader.ReadFloat16());
                    break;
                case LBBaseType.Float24:
                    fieldInfo.SetValue(obj, reader.ReadFloat24());
                    break;
                case LBBaseType.Float32:
                    fieldInfo.SetValue(obj, reader.ReadFloat32());
                    break;
                case LBBaseType.Float64:
                    fieldInfo.SetValue(obj, reader.ReadFloat64());
                    break;
                case LBBaseType.VarInt16:
                    fieldInfo.SetValue(obj, reader.ReadVarInt16());
                    break;
                case LBBaseType.VarInt32:
                    fieldInfo.SetValue(obj, reader.ReadVarInt32());
                    break;
                case LBBaseType.VarInt64:
                    fieldInfo.SetValue(obj, reader.ReadVarInt64());
                    break;
                case LBBaseType.VarUInt16:
                    fieldInfo.SetValue(obj, reader.ReadVarUInt16());
                    break;
                case LBBaseType.VarUInt32:
                    fieldInfo.SetValue(obj, reader.ReadVarUInt32());
                    break;
                case LBBaseType.VarUInt64:
                    fieldInfo.SetValue(obj, reader.ReadVarUInt64());
                    break;
                case LBBaseType.UTF8:
                    fieldInfo.SetValue(obj, reader.ReadUTF8());
                    break;
                case LBBaseType.Unicode:
                    fieldInfo.SetValue(obj, reader.ReadUnicode());
                    break;
                case LBBaseType.ASCII:
                    fieldInfo.SetValue(obj, reader.ReadASCII());
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region (System.Object) Read CustomType
        private static object ReadCustomType(LBType lbType, Type type) {
            // 如果是Class 检查是否为空 |  If class then check if null
            if (lbType.IsClass) if (reader.ReadBit1()) return null;

            // 读取字段信息 | Read field info
            object obj = LBReflectCache.GetClone(type);
            List<LBType> fields = lbType.FieldTypes;
            FieldInfo[] fieldInfos = LBReflectCache.GetFieldInfos(type, fields);
            for (int i = 0; i < fields.Count; i++) {
                LBType field = fields[i];
                FieldInfo fieldInfo = fieldInfos[i];
                try {
                    if (field.IsBaseType) {
                        ReadBaseType(field, obj, fieldInfo);
                    } else if (field.IsArray) {
                        ReadArray(field, obj, fieldInfo);
                    } else {
                        object value = ReadCustomType(field, fieldInfo.FieldType);
                        if (value != null) fieldInfo.SetValue(obj, value);
                    }
                } catch (Exception e) {
                    string error = "Read Field:[{0} {1}] error!\n Struct:[{2}]\n{3}";
                    error = string.Format(error, field.Type, field.Name, lbType.Name, e);
                    throw new Exception(error);
                }
            }
            return obj;
        }
        #endregion

        #region (System.Object) Read BaseType Array
        private static void ReadBaseTypeArray(LBType field, object obj, FieldInfo fieldInfo) {
            switch (field.ElementType.BaseType) {
                case LBBaseType.Bit1:
                    fieldInfo.SetValue(obj, reader.ReadBit1Array());
                    break;
                case LBBaseType.Bit2:
                    fieldInfo.SetValue(obj, reader.ReadBit2Array());
                    break;
                case LBBaseType.Bit3:
                    fieldInfo.SetValue(obj, reader.ReadBit3Array());
                    break;
                case LBBaseType.Bit4:
                    fieldInfo.SetValue(obj, reader.ReadBit4Array());
                    break;
                case LBBaseType.Bit5:
                    fieldInfo.SetValue(obj, reader.ReadBit5Array());
                    break;
                case LBBaseType.Bit6:
                    fieldInfo.SetValue(obj, reader.ReadBit6Array());
                    break;
                case LBBaseType.Bit7:
                    fieldInfo.SetValue(obj, reader.ReadBit7Array());
                    break;
                case LBBaseType.Int8:
                    fieldInfo.SetValue(obj, reader.ReadInt8Array());
                    break;
                case LBBaseType.Int16:
                    fieldInfo.SetValue(obj, reader.ReadInt16Array());
                    break;
                case LBBaseType.Int24:
                    fieldInfo.SetValue(obj, reader.ReadInt24Array());
                    break;
                case LBBaseType.Int32:
                    fieldInfo.SetValue(obj, reader.ReadInt32Array());
                    break;
                case LBBaseType.Int40:
                    fieldInfo.SetValue(obj, reader.ReadInt40Array());
                    break;
                case LBBaseType.Int48:
                    fieldInfo.SetValue(obj, reader.ReadInt48Array());
                    break;
                case LBBaseType.Int56:
                    fieldInfo.SetValue(obj, reader.ReadInt56Array());
                    break;
                case LBBaseType.Int64:
                    fieldInfo.SetValue(obj, reader.ReadInt64Array());
                    break;
                case LBBaseType.UInt8:
                    fieldInfo.SetValue(obj, reader.ReadUInt8Array());
                    break;
                case LBBaseType.UInt16:
                    fieldInfo.SetValue(obj, reader.ReadUInt16Array());
                    break;
                case LBBaseType.UInt24:
                    fieldInfo.SetValue(obj, reader.ReadUInt24Array());
                    break;
                case LBBaseType.UInt32:
                    fieldInfo.SetValue(obj, reader.ReadUInt32Array());
                    break;
                case LBBaseType.UInt40:
                    fieldInfo.SetValue(obj, reader.ReadUInt40Array());
                    break;
                case LBBaseType.UInt48:
                    fieldInfo.SetValue(obj, reader.ReadUInt48Array());
                    break;
                case LBBaseType.UInt56:
                    fieldInfo.SetValue(obj, reader.ReadUInt56Array());
                    break;
                case LBBaseType.UInt64:
                    fieldInfo.SetValue(obj, reader.ReadUInt64Array());
                    break;
                case LBBaseType.Float8:
                    fieldInfo.SetValue(obj, reader.ReadFloat8Array());
                    break;
                case LBBaseType.Float16:
                    fieldInfo.SetValue(obj, reader.ReadFloat16Array());
                    break;
                case LBBaseType.Float24:
                    fieldInfo.SetValue(obj, reader.ReadFloat24Array());
                    break;
                case LBBaseType.Float32:
                    fieldInfo.SetValue(obj, reader.ReadFloat32Array());
                    break;
                case LBBaseType.Float64:
                    fieldInfo.SetValue(obj, reader.ReadFloat64Array());
                    break;
                case LBBaseType.VarInt16:
                    fieldInfo.SetValue(obj, reader.ReadVarInt16Array());
                    break;
                case LBBaseType.VarInt32:
                    fieldInfo.SetValue(obj, reader.ReadVarInt32Array());
                    break;
                case LBBaseType.VarInt64:
                    fieldInfo.SetValue(obj, reader.ReadVarInt64Array());
                    break;
                case LBBaseType.VarUInt16:
                    fieldInfo.SetValue(obj, reader.ReadVarUInt16Array());
                    break;
                case LBBaseType.VarUInt32:
                    fieldInfo.SetValue(obj, reader.ReadVarUInt32Array());
                    break;
                case LBBaseType.VarUInt64:
                    fieldInfo.SetValue(obj, reader.ReadVarUInt64Array());
                    break;
                case LBBaseType.UTF8:
                    fieldInfo.SetValue(obj, reader.ReadUTF8Array());
                    break;
                case LBBaseType.Unicode:
                    fieldInfo.SetValue(obj, reader.ReadUnicodeArray());
                    break;
                case LBBaseType.ASCII:
                    fieldInfo.SetValue(obj, reader.ReadASCIIArray());
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region (System.Object) Read Array
        private static void ReadArray(LBType field, object obj, FieldInfo fieldInfo) {
            LBType elementField = field.ElementType;
            if (elementField.IsBaseType) {
                // 基本类型
                ReadBaseTypeArray(field, obj, fieldInfo);
            } else {
                // 复合类型
                int length = reader.ReadVarLength();
                if (length == -1) return;
                Type type = fieldInfo.FieldType;
                Type elementType = type.GetElementType();
                Array array = Array.CreateInstance(elementType, length);
                fieldInfo.SetValue(obj, array);
                if (length == 0) return;
                if (elementField.IsArray) {
                    // 数组套数组(不支持该格式) | Array in array(Not support)
                    throw new Exception("Not support array in array! Type name:" + field.Name);
                } else {
                    // 自定义类型 | Custom array
                    for (int i = 0; i < length; i++) {
                        array.SetValue(ReadCustomType(elementField, elementType), i);
                    }
                }
            }
        }
        #endregion

    }

}