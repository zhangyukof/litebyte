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
// Purpose: Split and get keywords from source code
// Author: ZhangYu
// CreateDate: 2019-10-18
// LastModifiedDate: 2020-01-08
#endregion
namespace LiteByte.Analyzers {

    using System.Collections.Generic;

    /// <summary>
    /// <para> 词法分析器 | Lexical analyzer </para>
    /// </summary>
    public static class LBLexer {

        /// <summary> 匹配状态 | Match state </summary>
        private enum MatchState {
            Normal,         // 普通状态 | Nothing special
            SingleLineComments, // 单行注释 | Single-line comments
            MultiLineComments   // 多行注释 | Single-line comments
        }

        /// <summary> 词法分析(拆分成词法单元) | Lexical analyze(split and get lexical element) </summary>
        public static List<LBToken> Analyze(string code) {
            // 检查参数
            if (string.IsNullOrEmpty(code)) {
                throw new System.ArgumentNullException("code", "Code can not be null or empty");
            }

            List<LBToken> tokens = new List<LBToken>(16);

            // 当前词法单元信息 | Current lexical element info
            MatchState matchState = MatchState.Normal;
            int row = 1;
            int column = 0;

            // 上一个词法单元信息 | Last lexical element info
            char lastChar = '\x0'; // 上一个字符（默认空字符NUL) | Last character(default NUL)
            LBTag lastTag = LBTag.None;
            int lastIndex = 0;
            int lastLength = 0;
            int lastRow = 1;
            int lastColumn = 0;
            bool needClear = false;

            // 扫描所有字符 | Scan all characters
            int total = code.Length;
            for (int index = 0; index < total; index++) {
                char c = code[index];

                // 如果注释开始 忽略除换行符和注释结束符以外的所有字符 | If Comments start Ignore all characters except newline and comments end characters 
                if (matchState == MatchState.SingleLineComments) {
                    // 查找单行注释结束 '\n' | Find single-line comments end '\n'
                    if (c == '\n') {
                        row += 1;
                        column = 0;
                        needClear = true;
                        matchState = MatchState.Normal;
                    }
                } else if (matchState == MatchState.MultiLineComments) {
                    if (c == '\n') {
                        // 换行符 | New line character
                        row += 1;
                        column = 0;
                        needClear = true;
                    } else if (c == '/' && lastChar == '*') {
                        // 查找多行注释结束 "*/" | Find multi-line comments end "*/"
                        needClear = true;
                        matchState = MatchState.Normal;
                    }
                } else if (c == ' ' || c == '\t') {
                    // 分隔符 | Separator characters
                    if (lastLength > 0) {
                        needClear = true;
                        tokens.Add(new LBToken(lastTag, lastIndex, lastLength, lastRow, lastColumn));
                    } else {
                        needClear = false;
                    }
                } else if (c == '\r') {
                    // 忽略符 | Ignore character
                    continue;
                } else if (c == '\n') {
                    // 拆分符 | Split characters
                    row += 1;
                    column = 0;
                    needClear = true;
                    AddToken(tokens, lastTag, lastIndex, lastLength, lastRow, lastColumn);
                } else if (c == ';') {
                    AddToken(tokens, lastTag, lastIndex, lastLength, lastRow, lastColumn);
                    tokens.Add(new LBToken(LBTag.Semicolon, index, 1, row, column));
                    needClear = true;
                } else if (c == ',') {
                    AddToken(tokens, lastTag, lastIndex, lastLength, lastRow, lastColumn);
                    tokens.Add(new LBToken(LBTag.Comma, index, 1, row, column));
                    needClear = true;
                } else if (c == '[') {
                    AddToken(tokens, lastTag, lastIndex, lastLength, lastRow, lastColumn);
                    tokens.Add(new LBToken(LBTag.LSquareBracket, index, 1, row, column));
                    needClear = true;
                } else if (c == ']') {
                    AddToken(tokens, lastTag, lastIndex, lastLength, lastRow, lastColumn);
                    tokens.Add(new LBToken(LBTag.RSquareBracket, index, 1, row, column));
                    needClear = true;
                } else if (c == '<') {
                    AddToken(tokens, lastTag, lastIndex, lastLength, lastRow, lastColumn);
                    tokens.Add(new LBToken(LBTag.LAngleBracket, index, 1, row, column));
                    needClear = true;
                } else if (c == '>') {
                    AddToken(tokens, lastTag, lastIndex, lastLength, lastRow, lastColumn);
                    tokens.Add(new LBToken(LBTag.RAngleBracket, index, 1, row, column));
                    needClear = true;
                } else if (c == '{') {
                    AddToken(tokens, lastTag, lastIndex, lastLength, lastRow, lastColumn);
                    tokens.Add(new LBToken(LBTag.LCurlyBrace, index, 1, row, column));
                    needClear = true;
                } else if (c == '}') {
                    AddToken(tokens, lastTag, lastIndex, lastLength, lastRow, lastColumn);
                    tokens.Add(new LBToken(LBTag.RCurlyBrace, index, 1, row, column));
                    needClear = true;
                } else {
                    // 其他符号 | Other characters
                    if (lastLength == 0) {
                        // 新的单词开始 | New word start
                        lastTag = LBTag.Word;
                        lastIndex = index;
                        lastLength = 1;
                    } else {
                        // 添加新字符
                        if (matchState == MatchState.Normal) {
                            if (lastChar == '/') {
                                // 判断注释是否开始 | Check if comments start
                                if (c == '/') {
                                    // 单行注释 | Single-line comments start
                                    matchState = MatchState.SingleLineComments;
                                } else if (c == '*') {
                                    // 多行注释开始 | Multi-line comments start 
                                    matchState = MatchState.MultiLineComments;
                                } else {
                                    // 非注释 添加字符 | Not comments. Add character
                                    lastLength += 1;
                                }
                            } else {
                                // 非注释 添加字符 | Not comments. Add character
                                lastLength += 1;
                            }
                        }
                    }
                }

                // 已添加了词法单元信息 重置临时数据 | Reset temp data after add token
                if (needClear) {
                    needClear = false;
                    lastChar = '\x0';
                    lastTag = LBTag.None;
                    lastIndex = index;
                    lastLength = 0;
                    lastRow = row;
                    lastColumn = column;
                }
                column += 1;
                lastChar = c;
            }
            AddToken(tokens, lastTag, lastIndex, lastLength, lastRow, lastColumn);
            return tokens;
        }

        /// <summary> 添加一个词法单元信息到列表 | Add a token to list </summary>
        private static void AddToken(List<LBToken> list, LBTag tag, int index, int length, int row, int column) {
            if (length > 0) list.Add(new LBToken(tag, index, length, row, column));
        }

    }

}