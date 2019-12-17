#region License
// MIT License
//
// Copyright(c) 2019 ZhangYu
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
// Purpose: Storage syntax analyze report contains "Struct" and "error"
// Author: ZhangYu
// CreateDate: 2019-10-18
// LastModifiedDate: 2019-12-11
#endregion
namespace LiteByte.Analyzers {

    using LiteByte.Common;

    /// <summary>
    /// <para> 语法分析结果 | Syntax analysis report </para>
    /// </summary>
    public class LBReport {

        public LBReport(bool isSuccess, LBType type, string error) {
            IsSuccess = isSuccess;
            Type = type;
            Error = error;
        }

        /// <summary> 语法分析是否成功 | Syntax analyze is success or not </summary>
        public bool IsSuccess { get; private set; }
        /// <summary> 分析成功后得到的类型 | LBType after analyze success </summary>
        public LBType Type { get; private set; }
        /// <summary> 分析失败后得到的错误 | Error after analyze failed </summary>
        public string Error { get; private set; }

    }

}