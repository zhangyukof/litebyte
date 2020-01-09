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
// Purpose: Custom type configuration
// Author: ZhangYu
// CreateDate: 2019-10-10
// LastModifiedDate: 2020-01-07

class LBConfig {

    // _________________________ BaseType _________________________
    /**
     * 判断是否是基本数据类型 并获取该类型 | If it is a base data type then get it
     * @param {string} type 
     * @returns {LBBaseType}
     */
    static getBaseType(type) {
        let baseType = LBConfig.baseTypeShortNameMap[type];
        if (baseType == null) baseType = LBConfig.baseTypeNameMap[type];
        return baseType;
    }

    /**
     * 添加基本数据类型的简称 | Add a short name for base type
     * @param {string} type 
     * @param {string} shortName 
     */
    static addBaseTypeShortName(type, shortName) {
        let baseType = LBConfig.baseTypeNameMap[type];
        if (baseType == null) {
            throw "Unsupported baseType:" + type;
        } else {
            LBConfig.baseTypeShortNameMap[shortName] = baseType;
        }
    }

    /**
     * 删除基本数据类型的简称 | Remove a short name for base type
     * @param {*} shortName 
     */
    static removeBaseTypeShortName(shortName) {
        LBConfig.baseTypeShortNameMap[shortName] = null;
    }

    /** 清理所有基本类型的简称 | Clear all short names */
    static clearBaseTypeShortNames() {
        LBConfig.baseTypeShortNameMap = {}
    }

    // _________________________ CustomType _________________________
    /**
     * 判断是否是自定义类型 并获取该类型 | If it is a custem type then get it
     * @param {string} name 
     * @returns {LBType} 
     */
    static getCustomType(name) {
        return LBConfig.customTypeMap[name];
    }

    /**
     * 添加一个自定义类型 | Add a custom type
     * @param {LBType} lbType LB类型 | LBType
     */
    static addCustomType(lbType) {
        if (LBConfig.customTypeMap[lbType.name] == null) {
            LBConfig.customTypeMap[lbType.name] = lbType;
        } else {
            throw "Custom type [" + lbType.name + "] already exists."
        }
    }

    /**
     * 移除一个自定义类型 | Remove a custom type
     * @param {string} name 自定义类型名称 | Custom type name
     */
    static removeCustomType(name) {
        LBConfig.customTypeMap[name] = null;
    }

    /** 清理所有自定义结构 | Clear all custom types */
    static clearCustomTypes() {
        LBConfig.customTypeMap = {};
    }

    /** 检查所有自定义类型的有效性 | Check the validity of all custom types */
    static checkAllCustomTypes() {
        for (const name in LBConfig.customTypeMap) {
            let lbType = LBConfig.customTypeMap[name];
            try {
                let fields = lbType.fieldTypes;
                for (let i = 0; i < fields.length; i++) {
                    this.checkType(fields[i]);
                }
            } catch (e) {
                throw "Not a valid type:\"" + lbType.name + "\"\n" + e;
            }
        }
    }

    /**
     * 检查字段的有效性 | Check the validity of all custom types
     * @param {LBType} lbType 
     */
    static checkType(lbType) {
        if (lbType.isArray) {
            // 数组 | Array
            this.checkType(lbType.elementType);
        } else {
            let baseType = this.getBaseType(lbType.type);
            if (baseType != null) {
                // 基本类型 | Base type
                lbType.initBaseType(baseType);
            } else {
                let customType = this.getCustomType(lbType.type);
                if (customType != null) {
                    // 自定义结构 | Custom type
                    lbType.copyCustomType(customType);
                } else {
                    // 未知类型 | Unknown type
                    throw "Unknown type:\"" + lbType.type + "\"";
                }
            }
        }
    }

}

/** 内置基本类型名称字典 | Built-in base type name hashmap */
LBConfig.baseTypeNameMap = {
    Bit1: LBBaseType.BIT1, Bit2: LBBaseType.BIT2, Bit3: LBBaseType.BIT3,
    Bit4: LBBaseType.BIT4, Bit5: LBBaseType.BIT5, Bit6: LBBaseType.BIT6, Bit7: LBBaseType.BIT7,
    Int8: LBBaseType.INT8, Int16: LBBaseType.INT16, Int24: LBBaseType.INT24, Int32: LBBaseType.INT32,
    Int40: LBBaseType.INT40, Int48: LBBaseType.INT48, Int56: LBBaseType.INT56, Int64: LBBaseType.INT64,
    UInt8: LBBaseType.UINT8, UInt16: LBBaseType.UINT16, UInt24: LBBaseType.UINT24, UInt32: LBBaseType.UINT32,
    UInt40: LBBaseType.UINT40, UInt48: LBBaseType.UINT48, UInt56: LBBaseType.UINT56, UInt64: LBBaseType.UINT64,
    Float8: LBBaseType.FLOAT8, Float16: LBBaseType.FLOAT16, Float24: LBBaseType.FLOAT24, Float32: LBBaseType.FLOAT32, Float64: LBBaseType.FLOAT64,
    VarInt16: LBBaseType.VAR_INT16, VarInt32: LBBaseType.VAR_INT32, VarInt64: LBBaseType.VAR_INT64,
    VarUInt16: LBBaseType.VAR_UINT16, VarUInt32: LBBaseType.VAR_UINT32, VarUInt64: LBBaseType.VAR_UINT64,
    ASCII: LBBaseType.ASCII, UTF8: LBBaseType.UTF8, UNICODE: LBBaseType.UNICODE, VAR_UNICODE : LBBaseType.VAR_UNICODE
}

/** 基本类型简称哈希表 | base type short name hashmap */
LBConfig.baseTypeShortNameMap = {}
/** 自定义类型哈希表 | base type short name hashmap */
LBConfig.customTypeMap = {}



