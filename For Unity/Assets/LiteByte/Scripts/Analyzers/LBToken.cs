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
// Purpose: Storage lexical element data
// Author: ZhangYu
// CreateDate: 2019-10-20
// LastModifiedDate: 2019-10-25
#endregion
namespace LiteByte.Analyzers {

    /// <summary>
    /// <para> 词法单元 | Lexical element </para>
    /// <para>参考手册 | Reference:https://segmentfault.com/u/bingfengbaidu</para>
    /// </summary>
    public struct LBToken {

        /// <summary> 字符串类型 | String type </summary>
        public LBTag tag;
        /// <summary> 开始序号 | Start index </summary>
        public int index;
        /// <summary> 字符串长度 | String length </summary>
        public int length;
        /// <summary> 行 | Line number </summary>
        public int row;
        /// <summary> 列 | Index at line </summary>
        public int column;

        public LBToken(LBTag tag, int index, int length, int row, int column) {
            this.tag = tag;
            this.index = index;
            this.length = length;
            this.row = row;
            this.column = column;
        }

    }

}