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
// Purpose: Storage type info
// Author: ZhangYu
// CreateDate: 2019-12-10
// LastModifiedDate: 2019-12-11
#endregion
namespace LiteByte.Common {

    using System.Collections.Generic;

    /// <summary>
    /// <para>LiteByte 类型信息 | Type info</para>
    /// <para>参考手册 | Reference:https://segmentfault.com/u/bingfengbaidu</para>
    /// </summary>
    public class LBType {

        public LBType() {}

        public LBType(string type, string name) {
            Type = type;
            Name = name;
        }

        public string Type { get; private set; }
        public string Name { get; private set; }
        public bool IsBaseType { get; private set; }
        public bool IsArray { get; private set; }
        public bool IsDictionary { get; private set; }
        public bool IsCustomType { get; private set; }
        public bool IsClass { get; private set; }
        public bool IsStruct { get; private set; }
        public LBBaseType BaseType { get; private set; }
        public LBType ElementType { get; private set; }
        public LBType KeyType { get; private set; }
        public LBType ValueType { get; private set; }
        public List<LBType> FieldTypes { get; private set; }

        public void InitTypeName(string type, string name) {
            Type = type;
            Name = name;
        }

        public void InitBaseType(LBBaseType baseType) {
            IsBaseType = true;
            BaseType = baseType;
        }

        public void InitArray(LBType elementType) {
            IsArray = true;
            ElementType = elementType;
        }

        public void InitCustomType(bool isClass, bool isStruct, List<LBType> fieldTypes) {
            IsCustomType = true;
            IsClass = isClass;
            IsStruct = isStruct;
            FieldTypes = fieldTypes;
        }

        public void InitCustomType(LBType customType) {
            IsCustomType = true;
            IsClass = customType.IsClass;
            IsStruct = customType.IsStruct;
            FieldTypes = customType.FieldTypes;
        }

    }

}