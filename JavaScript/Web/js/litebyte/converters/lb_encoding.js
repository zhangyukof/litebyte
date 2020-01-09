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
// Purpose: Convert base type to bytes
// Author: ZhangYu
// CreateDate: 2019-12-27
// LastModifiedDate: 2020-01-07

class LBASCIIEncoding {

    constructor() {
        this._decoder = new TextDecoder("ascii");
    }

    getCharCount(str) {
        return str.length;
    }

    getByteCount(str) {
        return str.length;
    }

    getBytes(str, bytes, byteIndex) {
        for (let i = 0; i < str.length; i++) {
            bytes[byteIndex + i] = str.charCodeAt(i);
        }
        return str.length;
    }

    getString(bytes, byteIndex, charCount) {
        return this._decoder.decode(new Uint8Array(bytes.buffer, byteIndex, charCount));
    }

}

class LBUnicodeEncoding {

    getCharCount(str) {
        return str.length;
    }

    getByteCount(str) {
        return str.length * 2;
    }

    getBytes(str, bytes, byteIndex) {
        for (let i = 0; i < str.length; i++) {
            let charCode = str.charCodeAt(i);
            let index = byteIndex + i * 2;
            bytes[index] = charCode;
            bytes[index + 1] = charCode >> 8;
        }
        return str.length * 2;
    }

    getString(bytes, byteIndex, charCount) {
        let value = "";
        for (let i = 0; i < charCount; i++) {
            value += String.fromCharCode(bytes[byteIndex] | bytes[byteIndex + 1] << 8);
            byteIndex += 2;
        }
        return value;
    }

}

class LBUTF8Encoding {

    constructor() {
        this._decoder = new TextDecoder("utf-8");
    }

    getCharCount(str) {
        let charCount = 0;
        for (let i = 0; i < str.length;) {
            charCount++;
            str.codePointAt(i) < 65536 ? i += 1 : i += 2;
        }
        return charCount;
    }

    getByteCount(str) {
        let byteCount = 0;
        for (let i = 0; i < str.length;) {
            let charCode = str.codePointAt(i);
            if (charCode < 0x80) {
                byteCount += 1;
            } else if (charCode < 0x800) {
                byteCount += 2;
            } else if (charCode < 0x10000) {
                byteCount += 3;
            } else {
                byteCount += 4;
            }
            charCode < 65536 ? i += 1 : i += 2;
        }
        return byteCount;
    }

    getBytes(str, bytes, byteIndex) {
        let byteCount = 0;
        for (let i = 0; i < str.length;) {
            let charCode = str.codePointAt(i);
            if (charCode < 0x80) {
                // 1Byte (0xxxxxxx + 1 = 2^7 = 128)
                bytes[byteIndex] = charCode;
                byteIndex += 1;
                byteCount += 1;
            } else if (charCode < 0x800) {
                // 2Byte (110xxxxx 10xxxxxx + 1 = 2^11 = 2048)
                bytes[byteIndex] = charCode >> 6 | 0xC0;
                bytes[byteIndex + 1] = charCode & 0x3F | 0x80;
                byteIndex += 2;
                byteCount += 2;
            } else if (charCode < 0x10000) {
                // 3Byte (1110xxxx 10xxxxxx 10xxxxxx + 1 = 2^16 = 65536)
                bytes[byteIndex] = charCode >> 12 | 0xE0;
                bytes[byteIndex + 1] = charCode >> 6 & 0x3F | 0x80;
                bytes[byteIndex + 2] = charCode & 0x3F | 0x80;
                byteIndex += 3;
                byteCount += 3;
            } else {
                // 4Byte (11110xxx 10xxxxxx 10xxxxxx 10xxxxxx + 1 = 2^21 = 2097152) Unicode max value = 1114111
                bytes[byteIndex] = charCode >> 18 | 0xF0;
                bytes[byteIndex + 1] = charCode >> 12 & 0x3F | 0x80;
                bytes[byteIndex + 2] = charCode >> 6 & 0x3F | 0x80;
                bytes[byteIndex + 3] = charCode & 0x3F | 0x80;
                byteIndex += 4;
                byteCount += 4;
            }
            charCode < 65536 ? i += 1 : i += 2;
        }
        return byteCount;
    }

    getString(bytes, byteIndex, byteCount) {
        return this._decoder.decode(new Uint8Array(bytes.buffer, byteIndex, byteCount));
    }

}

const LBEncoding = {

    ASCII:new LBASCIIEncoding(),
    Unicode:new LBUnicodeEncoding(),
    UTF8:new LBUTF8Encoding(),

}