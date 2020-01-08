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
// Purpose: Analyze keywords
// Author: ZhangYu
// CreateDate: 2019-10-18
// LastModifiedDate: 2019-12-11
#endregion
namespace LiteByte.Analyzers {

    using LiteByte.Common;
    using System.Collections.Generic;

    /// <summary>
    /// <para> 语法分析器 | Syntax analyzer </para>
    /// </summary>
    public class LBParser {

        /// <summary> 语法解析 | Syntax parse  </summary>
        public static LBReport Analyze(string code, List<LBToken> tokens) {
            // 检查参数
            if (string.IsNullOrEmpty(code)) {
                throw new System.ArgumentNullException("code", "Code can not be null or empty");
            }
            if (tokens == null || tokens.Count == 0) {
                throw new System.ArgumentNullException("tokens", "Tokens can not be null or empty");
            }

            string error = "";
            // 筛选有效单词 | Get valid worlds
            List<LBToken> validTokens = new List<LBToken>(7);
            int total = tokens.Count;
            for (int i = 0; i < total; i++) {
                LBToken token = tokens[i];
                if (token.tag == LBTag.Word) {
                    // 检查单词有效性 | Valid word
                    if (IsDigit(code[token.index])) {
                        // 第一个字符不能为数字 | The first character cannot be a number
                        string err = "The first character of an identifier cannot be a number ";
                        err += '\'' + code[token.index].ToString() + '\'';
                        error = error + GetErrorPrefix(code, token) + err + '\n';
                        continue;
                    } else if (IsAccessModifier(code, token)) {
                        // 忽略访问修饰符 | Ignore access modifier
                        continue;
                    }
                }

                // 添加有效单词 | Add valid word
                validTokens.Add(token);
            }
            if (error != "") return new LBReport(false, null, error);

            // 检查是否是结构体格式 | Valid type format
            if (validTokens.Count < 7) {
                error += "Parse struct error: Not enough words";
                return new LBReport(false, null, error);
            }

            bool isClass = IsMatch(code, validTokens[0], "class");
            bool isStruct = IsMatch(code, validTokens[0], "struct");
            if (!isClass && !isStruct) {
                LBToken token = validTokens[0];
                string err = "Unexpected word \"" + GetString(code, token) + "\" expecting \"class\" or \"struct\" ";
                error = error + GetErrorPrefix(code, token) + err + '\n';
            }
            if (!IsMatch(code, validTokens[2], "{")) {
                LBToken token = validTokens[2];
                string err = "Unexpected word \"" + GetString(code, token) + "\" expecting '{'";
                error = error + GetErrorPrefix(code, token) + err + '\n';
            }
            if (!IsMatch(code, validTokens[validTokens.Count - 1], "}")) {
                LBToken token = validTokens[validTokens.Count - 1];
                string err = "Unexpected word \"" + GetString(code, token) + "\" expecting '}'";
                error = error + GetErrorPrefix(code, token) + err + '\n';
            }
            if (error != "") return new LBReport(false, null, error);

            // 解析结构体字段 | Parse fields
            int count = validTokens.Count - 1;
            List<LBType> fields = new List<LBType>();
            LBType field = new LBType();
            bool hasType = false;
            bool hasName = false;
            bool hasLSquareBracket = false;
            bool hasRSquareBracket = false;
            for (int i = 3; i < count; i++) {
                LBToken token = validTokens[i];
                LBTag tag = token.tag;
                if (!hasType) {
                    // 匹配字段类型 | Match field type
                    if (tag == LBTag.Word) {
                        hasType = true;
                        field.InitTypeName(GetString(code, token), field.Name);
                    } else {
                        string err = "Unexpected word \"" + GetString(code, token) + "\" expecting [FieldType]";
                        error = error + GetErrorPrefix(code, token) + err + '\n';
                    }
                } else if (!hasName) {
                    // 匹配字段名称 | Match field name
                    if (tag == LBTag.Word) {
                        hasName = true;
                        field.InitTypeName(field.Type, GetString(code, token));
                    } else {
                        // 匹配数组的"[]" | Match array "[]"
                        if (!hasLSquareBracket && tag == LBTag.LSquareBracket) {
                            hasLSquareBracket = true;
                        } else if (hasLSquareBracket && !hasRSquareBracket) {
                            if (tag == LBTag.RSquareBracket) {
                                // 匹配到数组 "[]" | Match to array "[]"
                                hasRSquareBracket = true;
                                LBType elementType = new LBType(field.Type, null);
                                field.InitTypeName(field.Type + "[]", field.Name);
                                field.InitArray(elementType);
                            } else {
                                string err = "Unexpected word \"" + GetString(code, token) + "\" expecting ']'";
                                error = error + GetErrorPrefix(code, token) + err + '\n';
                            }
                        } else {
                            string err = "Unexpected word \"" + GetString(code, token) + "\" expecting [FieldName]";
                            error = error + GetErrorPrefix(code, token) + err + '\n';
                        }
                    }
                } else {
                    // 匹配字段结束 | Match end
                    if (tag == LBTag.Semicolon) {
                        fields.Add(field);
                    } else {
                        string err = "Expecting \";\"";
                        error = error + GetErrorPrefix(code, validTokens[i - 1]) + err + '\n';
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
                LBToken token = validTokens[count - 1];
                if (!hasName) {
                    string err = "Expecting [FieldName]";
                    error = error + GetErrorPrefix(code, token) + err + '\n';
                } else {
                    string err = "Expecting \";\"";
                    error = error + GetErrorPrefix(code, validTokens[count - 2]) + err + '\n';
                }
            }

            // 检查字段数量 | Check fields count
            if (fields.Count == 0) { error = error + "Valid field count is 0"; }
            if (error != "") return new LBReport(false, null, error);

            // 生成主体类型 | Generate main type
            LBType customType = new LBType(GetString(code, validTokens[0]), GetString(code, validTokens[1]));
            customType.InitCustomType(isClass, isStruct, fields);
            return new LBReport(true, customType, null);
        }

        #region String Math
        /// <summary> 是否是数字 | Check if is a digit </summary>
        private static bool IsDigit(char c) {
            return c <= '9' && c >= '0';
        }

        /// <summary> 是否是字母 | Check if is a letter </summary>
        private static bool IsLetter(char c) {
            return (c <= 'Z' && c >= 'A') || (c <= 'z' && c >= 'a');
        }

        /// <summary> 是否是访问修饰符 | Check if is a access modifier </summary>
        private static bool IsAccessModifier(string code, LBToken token) {
            if (IsMatch(code, token, "public") || IsMatch(code, token, "private") ||
                IsMatch(code, token, "protected") || IsMatch(code, token, "internal")) {
                return true;
            }
            return false;
        }

        /*
        /// <summary> 获取期望类型 | Get expect type </summary>
        private static LBTag GetExpectType(LBTag type) {
            if (type == LBTag.LSquareBracket) return LBTag.RSquareBracket;
            if (type == LBTag.LAngleBracket) return LBTag.RAngleBracket;
            if (type == LBTag.LCurlyBrace) return LBTag.RCurlyBrace;
            return LBTag.None;
        }
        */

        /// <summary> 字符串是否匹配 | Check if the string matches </summary>
        private static bool IsMatch(string code, LBToken token, string value) {
            if (token.length < value.Length) return false;
            for (int i = 0; i < value.Length; i++) {
                if (code[token.index + i] != value[i]) return false;
            }
            return true;
        }

        /// <summary> 获取代码中的字符串 | Get a string in the code </summary>
        private static string GetString(string code, LBToken token) {
            return code.Substring(token.index, token.length);
        }
        #endregion

        #region Error
        private static string GetErrorPrefix(string code, LBToken token) {
            string segment = code.Substring(token.index, token.length);
            return "Parse type(" + token.row + ", " + token.column + ") error near \"" + segment + "\": ";
        }
        #endregion
    }

}