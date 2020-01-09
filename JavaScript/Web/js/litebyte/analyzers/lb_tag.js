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
// Purpose: Define lexical analyze element type
// Author: ZhangYu
// CreateDate: 2019-12-27
// LastModifiedDate: 2019-10-20

/**
 * 词法单元类型(枚举类型) | Token type(enum)
 */
const LBTag = {
    NONE: 0,               // 未定义 | Undefined
    NEW_LINE: 1,           // \n
    SEMICOLON: 2,          // ;
    COMMA: 3,              // ,
    L_SQUARE_BRACKET: 4,   // [
    R_SQUARE_BRACKET: 5,   // ]
    L_ANGLE_BRACKET: 6,    // <
    R_ANGLE_BRACKET: 7,    // >
    L_CURLY_BRACE: 8,      // {
    R_CURLY_BRACE: 9,      // }
    WORD: 10              // 其他字符 | Other characters
}