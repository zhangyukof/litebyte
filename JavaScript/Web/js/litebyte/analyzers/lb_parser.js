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
// Purpose: Analyze keywords
// Author: ZhangYu
// CreateDate: 2019-12-27
// LastModifiedDate: 2020-01-09

class LBParser {

    /**
     * 语法解析 | Syntax pars
     * @param {String} code 
     * @param {LBToken[]} tokens 
     * @returns {LBReport}
     */
    static analyze(code, tokens) {
        if (code == null || code.length == 0) {
            throw "Code can not be null or empty";
        }
        if (tokens == null || tokens.length == 0) {
            throw "Tokens can not be null or empty";
        }

        let error = "";
        // 筛选有效单词 | Get valid worlds
        let validTokens = [];
        let total = tokens.length;
        for (let i = 0; i < total; i++) {
            let token = tokens[i];
            if (token.tag == LBTag.WORD) {
                // 检查单词有效性 | Valid word
                if (this.isDigit(code[token.index])) {
                    // 第一个字符不能为数字 | The first character cannot be a number
                    let err = "The first character of an identifier cannot be a number ";
                    err += "'" + code[token.index] + "'";
                    error = error + this.getErrorPrefix(code, token) + err + '\n';
                    continue;
                } else if (this.isAccessModifier(code, token)) {
                    // 忽略访问修饰符 | Ignore access modifier
                    continue;
                }
            }

            // 添加有效单词 | Add valid word
            validTokens.push(token);
        }
        if (error != "") return new LBReport(false, null, error);

        // 检查是否是结构体格式 | Valid type format
        if (validTokens.length < 7) {
            error += "Parse struct error: Not enough words";
            return new LBReport(false, null, error);
        }

        let isClass = this.isMatch(code, validTokens[0], "class");
        let isStruct = this.isMatch(code, validTokens[0], "struct");
        if (!isClass && !isStruct) {
            let token = validTokens[0];
            let err = 'Unexpected word "' + this.getString(code, token) + '" expecting "class" or "struct" ';
            error = error + this.getErrorPrefix(code, token) + err + '\n';
        }
        if (!this.isMatch(code, validTokens[2], "{")) {
            let token = validTokens[2];
            let err = 'Unexpected word "' + this.getString(code, token) + '" expecting \'{\'';
            error = error + this.getErrorPrefix(code, token) + err + '\n';
        }
        if (!this.isMatch(code, validTokens[validTokens.length - 1], "}")) {
            let token = validTokens[validTokens.length - 1];
            let err = 'Unexpected word "' + this.getString(code, token) + '" expecting \'{\'';
            error = error + this.getErrorPrefix(code, token) + err + '\n';
        }
        if (error != "") return new LBReport(false, null, error);

        // 解析结构体字段 | Parse fields
        let count = validTokens.length - 1;
        let fields = [];
        let field = new LBType();
        let hasType = false;
        let hasName = false;
        let hasLSquareBracket = false;
        let hasRSquareBracket = false;
        for (let i = 3; i < count; i++) {
            let token = validTokens[i];
            let tag = token.tag;
            //console.log(this.getString(code, token))
            if (!hasType) {
                // 匹配字段类型 | Match field type
                if (tag == LBTag.WORD) {
                    hasType = true;
                    field.initTypeName(this.getString(code, token), field.name);
                } else {
                    let err = 'Unexpected word "' + this.getString(code, token) + '" expecting [FieldType]';
                    error = error + this.getErrorPrefix(code, token) + err + '\n';
                }
            } else if (!hasName) {
                // 匹配字段名称 | Match field name
                if (tag == LBTag.WORD) {
                    hasName = true;
                    field.initTypeName(field.type, this.getString(code, token));
                } else {
                    // 匹配数组的"[]" | Match array "[]"
                    if (!hasLSquareBracket && tag == LBTag.L_SQUARE_BRACKET) {
                        hasLSquareBracket = true;
                    } else if (hasLSquareBracket && !hasRSquareBracket) {
                        if (tag == LBTag.R_SQUARE_BRACKET) {
                            // 匹配到数组 "[]" | Match to array "[]"
                            hasRSquareBracket = true;
                            let elementType = new LBType(field.type, null);
                            field.initTypeName(field.type + "[]", field.name);
                            field.initArray(elementType);
                        } else {
                            let err = "Unexpected word \"" + this.getString(code, token) + "\" expecting ']'";
                            error = error + this.getErrorPrefix(code, token) + err + '\n';
                        }
                    } else {
                        let err = "Unexpected word \"" + this.getString(code, token) + "\" expecting [FieldName]";
                        error = error + this.getErrorPrefix(code, token) + err + '\n';
                    }
                }
            } else {
                // 匹配字段结束 | Match end
                if (tag == LBTag.SEMICOLON) {
                    fields.push(field);
                } else {
                    let err = "Expecting ';'";
                    error = error + this.getErrorPrefix(code, validTokens[i - 1]) + err + '\n';
                    if (i > 3) i -= 1;
                }

                // 重置临时数据 | Reset temp data
                field = new LBType();
                hasType = false;
                hasName = false;
                hasLSquareBracket = false;
                hasRSquareBracket = false;
            }
        }

        // 最末尾 | At the end
        if (hasType) {
            let token = validTokens[count - 1];
            if (!hasName) {
                let err = "Expecting [FieldName]";
                error = error + this.getErrorPrefix(code, token) + err + '\n';
            } else {
                let err = "Expecting ';'";
                error = error + this.getErrorPrefix(code, validTokens[count - 2]) + err + '\n';
            }
        }

        // 检查字段数量 | Check fields count
        if (fields.length == 0) { error = error + "Valid field count is 0"; }
        if (error != "") return new LBReport(false, null, error);

        // 生成主体类型 | Generate main type
        let customType = new LBType(this.getString(code, validTokens[0]), this.getString(code, validTokens[1]));
        customType.initCustomType(isClass, isStruct, fields);
        return new LBReport(true, customType, null);
    }

    // _________________________ String match _________________________
    /**
     * 是否是数字 | Check if is a digit
     * @param {string} c
     * @returns {boolean} 
     */
    static isDigit(c) {
        return c <= '9' && c >= '0';
    }

    /**
     * 是否是字母 | Check if is a letter
     * @param {string} c
     * @returns {boolean} 
     */
    static isLetter(c) {
        return (c <= 'Z' && c >= 'A') || (c <= 'z' && c >= 'a');
    }

    /**
     * 是否是访问修饰符 | Check if is a access modifier
     * @param {string} code
     * @param {LBToken} token
     * @returns {boolean} 
     */
    static isAccessModifier(code, token) {
        if (this.isMatch(code, token, "public") || this.isMatch(code, token, "private") ||
            this.isMatch(code, token, "protected") || this.isMatch(code, token, "internal")) {
            return true;
        }
        return false;
    }

    /**
     * 字符串是否匹配 | Check if the let matches
     * @param {string} code
     * @param {LBToken} token
     * @param {string} value
     * @returns {boolean} 
     */
    static isMatch(code, token, value) {
        if (token.length < value.length) return false;
        for (let i = 0; i < value.length; i++) {
            if (code[token.index + i] != value[i]) return false;
        }
        return true;
    }

    /**
     * 获取代码中的字符串 | Get a let in the code
     * @param {string} code
     * @param {LBToken} token
     * @returns {boolean} 
     */
    static getString(code, token) {
        return code.substr(token.index, token.length);
    }

    // _________________________ Error _________________________
    /**
     * @param {string} code
     * @param {LBToken} token
     * @returns {string}
     */
    static getErrorPrefix(code, token) {
        let segment = code.substr(token.index, token.length);
        return "Parse type(" + token.row + ", " + token.column + ") error near \"" + segment + "\": ";
    }

}