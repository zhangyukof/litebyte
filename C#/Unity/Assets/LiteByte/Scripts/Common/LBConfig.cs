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
// Purpose: Custom type configuration
// Author: ZhangYu
// CreateDate: 2019-10-10
// LastModifiedDate: 2020-01-07
#endregion
namespace LiteByte.Common {

    using System;
    using System.Collections.Generic;

    /// <summary>
    /// <para>LiteByte 类型和结构配置 | Type and type configurator </para>
    /// </summary>
    public static class LBConfig {

        #region Base type
        /// <summary> 内置基本类型名称字典 | Built-in base type name dictionary </summary>
        private static Dictionary<string, LBBaseType> baseTypeNameDict = new Dictionary<string, LBBaseType>() {
            {"Bit1", LBBaseType.Bit1}, {"Bit2", LBBaseType.Bit2}, {"Bit3", LBBaseType.Bit3},
            {"Bit4", LBBaseType.Bit4}, {"Bit5", LBBaseType.Bit5}, {"Bit6", LBBaseType.Bit6}, {"Bit7", LBBaseType.Bit7},
            {"Int8", LBBaseType.Int8}, {"Int16", LBBaseType.Int16}, {"Int24", LBBaseType.Int24}, {"Int32", LBBaseType.Int32},
            {"Int40", LBBaseType.Int40}, {"Int48", LBBaseType.Int48}, {"Int56", LBBaseType.Int56}, {"Int64", LBBaseType.Int64},
            {"UInt8", LBBaseType.UInt8}, {"UInt16", LBBaseType.UInt16}, {"UInt24", LBBaseType.UInt24}, {"UInt32", LBBaseType.UInt32},
            {"UInt40", LBBaseType.UInt40}, {"UInt48", LBBaseType.UInt48}, {"UInt56", LBBaseType.UInt56}, {"UInt64", LBBaseType.UInt64},
            {"Float8", LBBaseType.Float8}, {"Float16", LBBaseType.Float16}, {"Float24", LBBaseType.Float24}, {"Float32", LBBaseType.Float32}, {"Float64", LBBaseType.Float64},
            {"VarInt16", LBBaseType.VarInt16}, {"VarInt32", LBBaseType.VarInt32}, {"VarInt64", LBBaseType.VarInt64},
            {"VarUInt16", LBBaseType.VarUInt16}, {"VarUInt32", LBBaseType.VarUInt32}, {"VarUInt64", LBBaseType.VarUInt64},
            {"UTF8", LBBaseType.UTF8}, {"Unicode", LBBaseType.Unicode}, {"ASCII", LBBaseType.ASCII}, {"VarUTF", LBBaseType.VarUTF},
        };

        /// <summary> 基本类型简称字典 | base type short name dictionary </summary>
        private static Dictionary<string, LBBaseType> baseTypeShortNameDict = new Dictionary<string, LBBaseType>() { };

        /// <summary> 判断是否是基本数据类型 并获取该类型 | If it is a base data type then get it </summary>
        public static bool TryGetBaseType(string type, out LBBaseType baseType) {
            if (baseTypeShortNameDict.TryGetValue(type, out baseType)) return true;
            if (baseTypeNameDict.TryGetValue(type, out baseType)) return true;
            return false;
        }

        /// <summary> 添加基本数据类型的简称 | Add a short name for base type </summary>
        public static void AddBaseTypeShortName(string type, string shortName) {
            LBBaseType baseType;
            if (baseTypeNameDict.TryGetValue(type, out baseType)) {
                baseTypeShortNameDict.Add(shortName, baseType);
            } else {
                throw new Exception("Unsupported baseType:" + type);
            }
        }

        /// <summary> 删除基本数据类型的简称 | Remove a short name for base type </summary>
        public static void RemoveBaseTypeShortName(string shortName) {
            baseTypeShortNameDict.Remove(shortName);
        }

        /// <summary> 清理所有基本类型的简称 | Clear all short names </summary>
        public static void ClearBaseTypeShortNames() {
            baseTypeShortNameDict.Clear();
        }
        #endregion

        #region Custom type
        private static Dictionary<string, LBType> customTypeDict = new Dictionary<string, LBType>();

        /// <param name="lbType">判断是否是自定义类型 并获取该类型 | If it is a custem type then get it</param>
        public static bool TryGetCustomType(string name, out LBType lbType) {
            if (customTypeDict.TryGetValue(name, out lbType)) return true;
            return false;
        }

        /// <summary> 添加一个自定义类型 | Add a custom type </summary>
        /// <param name="lbType"> LB类型 | LBType</param>
        public static void AddCustomType(LBType lbType) {
            if (!customTypeDict.ContainsKey(lbType.Name)) {
                customTypeDict.Add(lbType.Name, lbType);
            } else {
                string error = "Custom type [{0}] already exists.";
                error = string.Format(error, lbType.Name);
                throw new ArgumentException(error);
            }
        }

        /// <summary> 移除一个自定义类型 | Remove a custom type </summary>
        /// <param name="name"> 自定义类型名称 | Custom type name</param>
        public static void RemoveCustomType(string name) {
            customTypeDict.Remove(name);
        }

        /// <summary> 清理所有自定义结构 | Clear all custom types </summary>
        public static void ClearCustomTypes() {
            customTypeDict.Clear();
        }

        /// <summary> 检查所有自定义类型的有效性 | Check the validity of all custom types </summary>
        public static void CheckAllCustomTypes() {
            if (customTypeDict.Count == 0) return;
            foreach (LBType lbType in customTypeDict.Values) {
                try {
                    List<LBType> fields = lbType.FieldTypes;
                    for (int i = 0; i < fields.Count; i++) {
                        CheckType(fields[i]);
                    }
                } catch (Exception e) {
                    throw new Exception("Not a valid type:\"" + lbType.Name + "\"\n" + e);
                }
            }
        }

        /// <summary> 检查字段的有效性 | Check the validity of all custom types </summary>
        private static void CheckType(LBType type) {
            if (type.IsArray) {
                // 数组 | Array
                CheckType(type.ElementType);
            } else {
                LBBaseType baseType;
                if (TryGetBaseType(type.Type, out baseType)) {
                    // 基本类型 | Base type
                    type.InitBaseType(baseType);
                } else {
                    LBType customType;
                    if (TryGetCustomType(type.Type, out customType)) {
                        // 自定义结构 | Custom type
                        type.InitCustomType(customType);
                    } else {
                        // 未知类型 | Unknown type
                        throw new Exception("Unknown type type:\"" + type.Type + "\"");
                    }
                }
            }
        }
        #endregion

    }

}