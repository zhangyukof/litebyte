#region License
// MIT License
//
// Copyright(c) 2019 ZhangYu
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
// Purpose: API Tool
// Author: ZhangYu
// CreateDate: 2019-10-10
// LastModifiedDate: 2019-12-12
#endregion
namespace LiteByte {

    using System;
    using LiteByte.Common;
    using LiteByte.Converters;

    /// <summary>
    /// <para>LiteByte Utility</para>
    /// <para>参考手册 | Reference:https://segmentfault.com/u/bingfengbaidu</para>
    /// </summary>
    public static class LBUtil {

        /// <summary> 序列化(对象转字节数组) | Serialize(Convert object to bytes) </summary>
        /// <param name="typeName">自定义类型名称 | Custom type name</param>
        /// <param name="obj">对象(LBObject)</param>
        public static byte[] Serialize(string typeName, LBObject obj) {
            LBType customType;
            if(!LBConfig.TryGetCustomType(typeName, out customType)) {
                throw new Exception("CustomType is null. Type name:" + typeName);
            }
            return LBConverter.ToBytes(customType, obj);
        }

        /// <summary> 序列化(对象转字节数组) | Serialize(Convert object to bytes) </summary>
        /// <param name="typeName">自定义类型名称 | Custom type name</param>
        /// <param name="obj">对象(类或结构) | object(class or struct)</param>
        public static byte[] Serialize(string typeName, object obj) {
            LBType customType;
            if(!LBConfig.TryGetCustomType(typeName, out customType)) {
                throw new Exception("CustomType is null. Type name:" + typeName);
            }
            return LBConverter.ToBytes(customType, obj);
        }

        /// <summary> 反序列化 | Deserialize(Parse bytes to object) </summary>
        /// <param name="typeName">自定义类型名称 | Custom type name</param>
        /// <param name="bytes">字节数组 | Byte array</param>
        public static LBObject Deserialize(string typeName, byte[] bytes) {
            LBType customType;
            if (!LBConfig.TryGetCustomType(typeName, out customType)) {
                throw new Exception("CustomType is null. Type name:" + typeName);
            }
            return LBConverter.ToObject(customType, bytes);
        }

        /// <summary> 反序列化 | Deserialize(Parse bytes to object) </summary>
        /// <param name="typeName">自定义类型名称 | Custom type name</param>
        /// <param name="bytes">字节数组 | Byte array</param>
        public static T Deserialize<T>(string typeName, byte[] bytes) where T : new() {
            LBType customType;
            if (!LBConfig.TryGetCustomType(typeName, out customType)) {
                throw new Exception("CustomType is null. Type name:" + typeName);
            }
            return LBConverter.ToObject<T>(customType, bytes);
        }

    }

}