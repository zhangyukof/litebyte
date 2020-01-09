//__________________________________ License __________________________________
// MIT License
//
// Copyright(c) 2019-2020 ZhangYu
// https: //github.com/zhangyukof/litebyte
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
// Purpose: Storage type info
// Author: ZhangYu
// CreateDate: 2019-12-10
// LastModifiedDate: 2019-12-11

/** LiteByte 类型信息 | Type info */
class LBType {

    /**
     * @param {string} type 
     * @param {string} name 
     */
    constructor(type, name) {
        this._type = type;
        this._name = name;
        this._isBaseType = false;
        this._isArray = false;
        this._isCustomType = false;
        this._isClass = false;
        this._isStruct = false;
    }

    /** @returns {string} */
    get type() {
        return this._type;
    }

    /** @returns {string} */
    get name() {
        return this._name;
    }

    /** @returns {boolean} */
    get isBaseType() {
        return this._isBaseType;
    }

    /** @returns {boolean} */
    get isArray() {
        return this._isArray;
    }

    /** @returns {boolean} */
    get isDictionary() {
        return this._isDictionary;
    }

    /** @returns {boolean} */
    get isCustomType() {
        return this._isCustomTyperay;
    }

    /** @returns {boolean} */
    get isClass() {
        return this._isClass;
    }

    /** @returns {boolean} */
    get isStruct() {
        return this._isStruct;
    }

    /** @returns {LBType} */
    get baseType() {
        return this._baseType;
    }

    /** @returns {LBType} */
    get elementType() {
        return this._elementType;
    }

    /** @returns {LBType} */
    get keyType() {
        return this._keyType;
    }

    /** @returns {LBType} */
    get valueType() {
        return this._valueType;
    }

    /** @returns {LBType} */
    get fieldTypes() {
        return this._fieldTypes;
    }

    /**
     * 初始化类型和名称
     * @param {string} type 
     * @param {string} name 
     */
    initTypeName(type, name) {
        this._type = type;
        this._name = name;
    }

    /**
     * 初始化基本类型
     * @param {LBBaseType} baseType 
     */
    initBaseType(baseType) {
        this._isBaseType = true;
        this._baseType = baseType;
    }

    /**
     * 初始化数组
     * @param {LBType} elementType 
     */
    initArray(elementType) {
        this._isArray = true;
        this._elementType = elementType;
    }

    /**
     * 初始化自定义类型
     * @param {boolean} isClass 
     * @param {boolean} isStruct 
     * @param {LBType[]} fieldTypes 
     */
    initCustomType(isClass, isStruct, fieldTypes) {
        this._isCustomType = true;
        this._isClass = isClass;
        this._isStruct = isStruct;
        this._fieldTypes = fieldTypes;
    }

    /**
     * 拷贝自定义类型的值
     * @param {LBType} customType 
     */
    copyCustomType(customType) {
        this._isCustomType = true;
        this._isClass = customType.IsClass;
        this._isStruct = customType.IsStruct;
        this._fieldTypes = customType.FieldTypes;
    }

}