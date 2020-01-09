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
// Purpose: Split and get keywords from source code
// Author: ZhangYu
// CreateDate: 2019-12-27
// LastModifiedDate: 2020-01-08

/** 词法分析器(静态类) | Lexical analyzer(static class) */
class LBLexer {

    /**
    * 词法分析(拆分成词法单元) | Lexical analyze(split and get lexical element)
    * @param {String} code 
    * @returns {LBToken[]}
    */
    static analyze(code) {
        if (code == null || code.length == 0) {
            throw "Code can not be null or empty";
        }
        
        let tokens = [];
     
        // 当前词法单元信息 | Current lexical element info
        let matchState = this.MatchState.Normal;
        let row = 1;
        let column = 0;
     
        // 上一个词法单元信息 | Last lexical element info
        let lastChar = '\0';     // 上一个字符（默认空字符NUL) | Last character(default NUL)
        let lastTag = LBTag.NONE;
        let lastIndex = 0;
        let lastLength = 0;
        let lastRow = 1;
        let lastColumn = 0;
        let needClear = false;
     
        // 扫描所有字符 | Scan all characters
        let total = code.length;
        for (let index = 0; index < total; index++) {
            let c = code[index];
     
            // 如果注释开始 忽略除换行符和注释结束符以外的所有字符 | If Comments start Ignore all characters except newline and comments end characters 
            if (matchState == this.MatchState.SingleLineComments) {
                // 查找单行注释结束 '\n' | Find single-line comments end '\n'
                if (c == '\n') {
                    row += 1;
                    column = 0;
                    needClear = true;
                    matchState = this.MatchState.Normal;
                }
            } else if (matchState == this.MatchState.MultiLineComments) {
                if (c == '\n') {
                    // 换行符 | New line character
                    row += 1;
                    column = 0;
                    needClear = true;
                } else if (c == '/' && lastChar == '*') {
                    // 查找多行注释结束 "*/" | Find multi-line comments end "*/"
                    needClear = true;
                    matchState = this.MatchState.Normal;
                }
            } else if (c == ' ' || c == '\t') {
                // 分隔符 | Separator characters
                if (lastLength > 0) {
                    needClear = true;
                    tokens.push(new LBToken(lastTag, lastIndex, lastLength, lastRow, lastColumn));
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
                this.addToken(tokens, lastTag, lastIndex, lastLength, lastRow, lastColumn);
            } else if (c == ';') {
                this.addToken(tokens, lastTag, lastIndex, lastLength, lastRow, lastColumn);
                tokens.push(new LBToken(LBTag.SEMICOLON, index, 1, row, column));
                needClear = true;
            } else if (c == ',') {
                this.addToken(tokens, lastTag, lastIndex, lastLength, lastRow, lastColumn);
                tokens.push(new LBToken(LBTag.COMMA, index, 1, row, column));
                needClear = true;
            } else if (c == '[') {
                this.addToken(tokens, lastTag, lastIndex, lastLength, lastRow, lastColumn);
                tokens.push(new LBToken(LBTag.L_SQUARE_BRACKET, index, 1, row, column));
                needClear = true;
            } else if (c == ']') {
                this.addToken(tokens, lastTag, lastIndex, lastLength, lastRow, lastColumn);
                tokens.push(new LBToken(LBTag.R_SQUARE_BRACKET, index, 1, row, column));
                needClear = true;
            } else if (c == '<') {
                this.addToken(tokens, lastTag, lastIndex, lastLength, lastRow, lastColumn);
                tokens.push(new LBToken(LBTag.L_ANGLEB_RACKET, index, 1, row, column));
                needClear = true;
            } else if (c == '>') {
                this.addToken(tokens, lastTag, lastIndex, lastLength, lastRow, lastColumn);
                tokens.push(new LBToken(LBTag.R_ANGLEB_RACKET, index, 1, row, column));
                needClear = true;
            } else if (c == '{') {
                this.addToken(tokens, lastTag, lastIndex, lastLength, lastRow, lastColumn);
                tokens.push(new LBToken(LBTag.L_CURLY_BRACE, index, 1, row, column));
                needClear = true;
            } else if (c == '}') {
                this.addToken(tokens, lastTag, lastIndex, lastLength, lastRow, lastColumn);
                tokens.push(new LBToken(LBTag.R_CURLY_BRACE, index, 1, row, column));
                needClear = true;
            } else {
                // 其他符号 | Other characters
                if (lastLength == 0) {
                    // 新的单词开始 | New word start
                    lastTag = LBTag.WORD;
                    lastIndex = index;
                    lastLength = 1;
                } else {
                    // 添加新字符
                    if (matchState == this.MatchState.Normal) {
                        if (lastChar == '/') {
                            // 判断注释是否开始 | Check if comments start
                            if (c == '/') {
                                // 单行注释 | Single-line comments start
                                matchState = this.MatchState.SingleLineComments;
                            } else if (c == '*') {
                                // 多行注释开始 | Multi-line comments start 
                                matchState = this.MatchState.MultiLineComments;
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
                lastChar = '\0';
                lastTag = LBTag.NONE;
                lastIndex = index;
                lastLength = 0;
                lastRow = row;
                lastColumn = column;
            }
            column += 1;
            lastChar = c;
        }
        this.addToken(tokens, lastTag, lastIndex, lastLength, lastRow, lastColumn);
        return tokens;
    }

    /**
     * 添加一个词法单元信息到列表 | Add a token to list
     */
    static addToken(list, tag, index, length, row, column) {
        if (length > 0) list.push(new LBToken(tag, index, length, row, column));
    }

}

/** 
 * 匹配状态 | Match state
 */
LBLexer.MatchState = {
    Normal: 0,               // 普通状态 | Nothing special
    SingleLineComments: 1,   // 单行注释 | Single-line comments
    MultiLineComments: 2     // 多行注释 | Single-line comments
}