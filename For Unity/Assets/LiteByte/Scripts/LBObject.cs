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
// Purpose: Set and get data
// Author: ZhangYu
// CreateDate: 2019-11-17
// LastModifiedDate: 2019-12-16
#endregion
namespace LiteByte {

    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// <para>LiteByte 自定义对象 | Custom object </para>
    /// </summary>
    public class LBObject {

        private Dictionary<string, byte> byteDict;
        private Dictionary<string, ushort> ushortDict;
        private Dictionary<string, uint> uintDict;
        private Dictionary<string, ulong> ulongDict;
        private Dictionary<string, float> floatDict;
        private Dictionary<string, double> doubleDict;
        private Dictionary<string, string> stringDict;
        private Dictionary<string, IList> arrayDict;
        private Dictionary<string, LBObject> objectDict;

        public LBObject() {}

        #region Set Value
        public void SetBool(string key, bool value) {
            SetByte(key, value ? (byte)1 : (byte)0);
        }

        public void SetSByte(string key, sbyte value) {
            SetByte(key, (byte)value);
        }

        public void SetShort(string key, short value) {
            SetUShort(key, (ushort)value);
        }

        public void SetInt(string key, int value) {
            SetUInt(key, (uint)value);
        }

        public void SetLong(string key, long value) {
            SetULong(key, (ulong)value);
        }

        public void SetByte(string key, byte value) {
            if (value == 0) return;
            if (byteDict == null) byteDict = new Dictionary<string, byte>();
            if (!byteDict.ContainsKey(key)) {
                byteDict.Add(key, value);
            } else {
                byteDict[key] = value;
            }
        }

        public void SetUShort(string key, ushort value) {
            if (value == 0) return;
            if (ushortDict == null) ushortDict = new Dictionary<string, ushort>();
            if (!ushortDict.ContainsKey(key)) {
                ushortDict.Add(key, value);
            } else {
                ushortDict[key] = value;
            }
        }

        public void SetUInt(string key, uint value) {
            if (value == 0) return;
            if (uintDict == null) uintDict = new Dictionary<string, uint>();
            if (!uintDict.ContainsKey(key)) {
                uintDict.Add(key, value);
            } else {
                uintDict[key] = value;
            }
        }

        public void SetULong(string key, ulong value) {
            if (value == 0) return;
            if (ulongDict == null) ulongDict = new Dictionary<string, ulong>();
            if (!ulongDict.ContainsKey(key)) {
                ulongDict.Add(key, value);
            } else {
                ulongDict[key] = value;
            }
        }

        public void SetFloat(string key, float value) {
            if (value == 0) return;
            if (floatDict == null) floatDict = new Dictionary<string, float>();
            if (!floatDict.ContainsKey(key)) {
                floatDict.Add(key, value);
            } else {
                floatDict[key] = value;
            }
        }

        public void SetDouble(string key, double value) {
            if (value == 0) return;
            if (doubleDict == null) doubleDict = new Dictionary<string, double>();
            if (!doubleDict.ContainsKey(key)) {
                doubleDict.Add(key, value);
            } else {
                doubleDict[key] = value;
            }
        }

        public void SetString(string key, string value) {
            if (value == null) return;
            if (stringDict == null) stringDict = new Dictionary<string, string>();
            if (!stringDict.ContainsKey(key)) {
                stringDict.Add(key, value);
            } else {
                stringDict[key] = value;
            }
        }

        public void SetObject(string key, LBObject value) {
            if (value == null) return;
            if (objectDict == null) objectDict = new Dictionary<string, LBObject>();
            if (!objectDict.ContainsKey(key)) {
                objectDict.Add(key, value);
            } else {
                objectDict[key] = value;
            }
        }
        #endregion

        #region Set Array
       	public void SetBoolArray(string key, bool[] array) {
            SetArray(key, array);
        }

        public void SetSByteArray(string key, sbyte[] array) {
            SetArray(key, array);
        }

        public void SetShortArray(string key, short[] array) {
            SetArray(key, array);
        }

        public void SetIntArray(string key, int[] array) {
            SetArray(key, array);
        }

        public void SetLongArray(string key, long[] array) {
            SetArray(key, array);
        }

        public void SetByteArray(string key, byte[] array) {
            SetArray(key, array);
        }

        public void SetUShortArray(string key, ushort[] array) {
            SetArray(key, array);
        }

        public void SetUIntArray(string key, uint[] array) {
            SetArray(key, array);
        }

        public void SetULongArray(string key, ulong[] array) {
            SetArray(key, array);
        }

        public void SetFloatArray(string key, float[] array) {
            SetArray(key, array);
        }

        public void SetDoubleArray(string key, double[] array) {
            SetArray(key, array);
        }

        public void SetStringArray(string key, string[] array) {
            SetArray(key, array);
        }

        public void SetObjectArray(string key, LBObject[] array) {
            SetArray(key, array);
        }
        #endregion

        #region Get Value
        public bool GetBool(string key) {
            return GetByte(key) == 1 ? true : false;
        }

        public sbyte GetSByte(string key) {
            return (sbyte)GetByte(key);
        }

        public short GetShort(string key) {
            return (short)GetUShort(key);
        }

        public int GetInt(string key) {
            return (int)GetUInt(key);
        }

        public long GetLong(string key) {
            return (long)GetULong(key);
        }
        
        public byte GetByte(string key) {
            if (byteDict == null) return 0;
            byte value;
            byteDict.TryGetValue(key, out value);
            return value;
        }

        public ushort GetUShort(string key) {
            if (ushortDict == null) return 0;
            ushort value;
            ushortDict.TryGetValue(key, out value);
            return value;
        }

        
        public uint GetUInt(string key) {
            if (uintDict == null) return 0;
            uint value;
            uintDict.TryGetValue(key, out value);
            return value;
        }

        public ulong GetULong(string key) {
            if (ulongDict == null) return 0;
            ulong value;
            ulongDict.TryGetValue(key, out value);
            return value;
        }

        public string GetString(string key) {
            if (stringDict == null) return null;
            string value;
            stringDict.TryGetValue(key, out value);
            return value;
        }

        public float GetFloat(string key) {
            if (floatDict == null) return 0;
            float value;
            return floatDict.TryGetValue(key, out value) ? value : 0;
        }

        public double GetDouble(string key) {
            if (doubleDict == null) return 0;
            double value;
            return doubleDict.TryGetValue(key, out value) ? value : 0;
        }

        public LBObject GetObject(string key) {
            if (objectDict == null) return null;
            LBObject value;
            objectDict.TryGetValue(key, out value);
            return value;
        }
        #endregion

        #region Get Array
        public bool[] GetBoolArray(string key) {
            return (bool[])GetArray(key);
        }

        public sbyte[] GetSByteArray(string key) {
            return (sbyte[])GetArray(key);
        }

        public int[] GetIntArray(string key) {
            return (int[])GetArray(key);
        }
        
        public short[] GetShortArray(string key) {
            return (short[])GetArray(key);
        }

        public long[] GetLongArray(string key) {
            return (long[])GetArray(key);
        }
        
        public byte[] GetByteArray(string key) {
            return (byte[])GetArray(key);
        }

        public ushort[] GetUShortArray(string key) {
            return (ushort[])GetArray(key);
        }

        public uint[] GetUIntArray(string key) {
            return (uint[])GetArray(key);
        }

        public ulong[] GetULongArray(string key) {
            return (ulong[])GetArray(key);
        }

        public string[] GetStringArray(string key) {
            return (string[])GetArray(key);
        }

        public float[] GetFloatArray(string key) {
            return (float[])GetArray(key);
        }

        public double[] GetDoubleArray(string key) {
            return (double[])GetArray(key);
        }

        public LBObject[] GetObjectArray(string key) {
            return (LBObject[])GetArray(key);
        }
        #endregion

        #region Tools
        private IList GetArray(string key) {
            if (arrayDict == null) return null;
            IList array;
            arrayDict.TryGetValue(key, out array);
            return array;
        }

        public void SetArray(string key, IList array) {
            if (array == null) return;
            if (arrayDict == null) arrayDict = new Dictionary<string, IList>();
            if (!arrayDict.ContainsKey(key)) {
                arrayDict.Add(key, array);
            } else {
                arrayDict[key] = array;
            }
        }
        #endregion

        public void Clear() {
            byteDict.Clear();
            ushortDict.Clear();
            uintDict.Clear();
            ulongDict.Clear();
            floatDict.Clear();
            doubleDict.Clear();
            stringDict.Clear();
            arrayDict.Clear();
            objectDict.Clear();
        }

    }

}