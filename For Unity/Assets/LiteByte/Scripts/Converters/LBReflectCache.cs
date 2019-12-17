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
// Purpose: Reflection Optimize Performance
// Author: ZhangYu
// CreateDate: 2019-11-22
// LastModifiedDate: 2019-11-22
#endregion
namespace LiteByte.Converters {

    using System;
    using System.Reflection;
    using System.Collections.Generic;
    using LiteByte.Common;

    /// <summary>
    /// <para>LBReflectCache</para>
    /// <para> 缓存反射后的信息 | Cache reflection info </para>
    /// <para>参考手册 | Reference:https://segmentfault.com/u/bingfengbaidu</para>
    /// </summary>
    public static class LBReflectCache {

        private static Dictionary<string, FieldInfo[]> fieldInfoDict = new Dictionary<string, FieldInfo[]>();

        /// <summary> 从缓存中获取FieldInfos | Get FieldInfos from cache </summary>
        public static FieldInfo[] GetFieldInfos(Type type, List<LBType> fields) {
            FieldInfo[] fieldInfos;
            if (fieldInfoDict.TryGetValue(type.FullName, out fieldInfos)) {
                return fieldInfos;
            } else {
                // 缓存FieldInfos | Cache FieldInfos
                fieldInfos = new FieldInfo[fields.Count];
                for (int i = 0; i < fields.Count; i++) {
                    FieldInfo fieldInfo = type.GetField(fields[i].Name);
                    if (fieldInfo == null) {
                        throw new Exception(string.Format("[{0}] does not contain field:[{1}]", type.FullName, fields[i].Name));
                    }
                    fieldInfos[i] = fieldInfo;
                }
                fieldInfoDict.Add(type.FullName, fieldInfos);
            }
            return fieldInfos;
        }

        /// <summary> 从缓存中移除FieldInfos | Remove FieldInfos from cache </summary>
        public static void RemoveFieldInfos(Type type) {
            fieldInfoDict.Remove(type.FullName);
        }

        /// <summary> 清理所有缓存的FieldInfos | Clear all cached fieldInfos </summary>
        public static void ClearFieldInfos() {
            fieldInfoDict.Clear();
        }

        private static Dictionary<Type, CloneInfo> cloneInfoDict = new Dictionary<Type, CloneInfo>();

        /// <summary> 克隆信息 </summary>
        private class CloneInfo {

            public object template;
            public Func<object, object> cloneDelegate;

            public CloneInfo(object template, Func<object, object> cloneDelegate) {
                this.template = template;
                this.cloneDelegate = cloneDelegate;
            }

        }

        /// <summary> 获取克隆的对象 | Get clone object by type </summary>
        public static object GetClone(Type type) {
            object template;
            Func<object, object> cloneDelegate;
            GetCloneInfo(type, out template, out cloneDelegate);
            return cloneDelegate(template);
        }

        /// <summary> 获取克隆信息 | Get clone info </summary>
        public static void GetCloneInfo(Type type, out object template, out Func<object, object> cloneDelegate) {
            CloneInfo cloneInfo;
            if (!cloneInfoDict.TryGetValue(type, out cloneInfo)) {
                template = Activator.CreateInstance(type);
                MethodInfo cloneMethod = type.GetMethod("MemberwiseClone", BindingFlags.NonPublic | BindingFlags.Instance);
                cloneInfo = new CloneInfo(template, (Func<object, object>)Delegate.CreateDelegate(typeof(Func<object, object>), cloneMethod));
                cloneInfoDict.Add(type, cloneInfo);
            }
            template = cloneInfo.template;
            cloneDelegate = cloneInfo.cloneDelegate;
        }

        /// <summary> 移除克隆信息 | Remove clone info </summary>
        public static void RemoveCloneInfo(Type type) {
            cloneInfoDict.Remove(type);
        }

        /// <summary> 清理所有克隆信息 | Clear all clone infos </summary>
        public static void ClearCloneInfo() {
            cloneInfoDict.Clear();
        }

    }

}
