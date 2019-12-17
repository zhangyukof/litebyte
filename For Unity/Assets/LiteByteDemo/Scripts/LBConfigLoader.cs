namespace LiteByte.Demo {

    using System;
    using System.Text;
    using System.Collections.Generic;
    using UnityEngine;
    using LiteByte.Common;
    using LiteByte.Analyzers;

    /// <summary>
    /// LiteByte配置加载器 | Config Loader
    /// ZhangYu 2019-11-21
    /// </summary>
    public class LBConfigLoader : MonoBehaviour, IDisposable {

        #region Init
        public delegate void Callback();
        private bool isDisposed;
        private LBHttpFileLoader fileLoader;
        private Callback onCompleteCallback;
        private Callback onErrorCallback;
        private string fileRoot;
        private Queue<string> fileNames;

        private void Awake() {
            fileNames = new Queue<string>();
            fileLoader = gameObject.AddComponent<LBHttpFileLoader>();
        }
        #endregion

        #region Load Config
        /// <summary> 加载配置文件 | Load config </summary>
        /// <param name="url">地址 | Config url</param>
        /// <param name="onComplete">完成回调 | Callback after complete</param>
        public void LoadConfig(string url, Callback onComplete, Callback onError) {
            onCompleteCallback = onComplete;
            onErrorCallback = onError;
            fileLoader.LoadAsync(url, OnLoadConfigComplete, null, OnLoadConfigError);
        }

        private void OnLoadConfigComplete(byte[] bytes) {
            string json = Encoding.UTF8.GetString(bytes);
            LBConfigVo config = JsonUtility.FromJson<LBConfigVo>(json);
            ParseBaseTypeShortNames(config.baseTypeShortNames);
            ParseTypeFiles(config.typeFileRoot, config.typeFileNames);
        }

        private void OnLoadConfigError(string error) {
            Debug.LogError("OnLoadConfigError:" + error);
            if (onErrorCallback != null) onErrorCallback();
        }
        #endregion

        #region Parse Config
        /// <summary> 解析基本类型简称 | Parse base type short name </summary>
        private void ParseBaseTypeShortNames(ShortNameVo[] shortNames) {
            for (int i = 0; i < shortNames.Length; i++) {
                ShortNameVo shortName = shortNames[i];
                LBConfig.AddBaseTypeShortName(shortName.type, shortName.name);
            }
        }

        /// <summary> 解析类型文件 </summary>
        private void ParseTypeFiles(string fileRoot, string[] fileNames) {
            this.fileRoot = fileRoot.Replace("{StreamingAssets}", Application.streamingAssetsPath);
            this.fileNames = new Queue<string>(fileNames);
            LoadNextTypeFile();
        }

        /// <summary> 加载下一个类型文件 | Load next type file </summary>
        private void LoadNextTypeFile() {
            if (fileNames.Count > 0) {
                fileLoader.LoadAsync(fileRoot + fileNames.Dequeue(), OnLoadTypeComplete, null, OnLoadTypeError);
            }
        }

        /// <summary> 加载类型文件完成 | Load type file complete </summary>
        private void OnLoadTypeComplete(byte[] bytes) {
            // 分析类型文件 | Analyze type file
            string code = Encoding.UTF8.GetString(bytes);
            List<LBToken> tokens = LBLexer.Analyze(code);
            LBReport report = LBParser.Analyze(code, tokens);
            if (report.IsSuccess) {
                LBConfig.AddCustomType(report.Type);
                if (fileNames.Count > 0) {
                    LoadNextTypeFile();
                } else {
                    LBConfig.CheckAllCustomTypes();
                    onCompleteCallback();
                }
            } else {
                throw new Exception("Type file analyzing failed\n" + report.Error);
            }
        }

        private void OnLoadTypeError(string error) {
            Debug.LogError("OnLoadTypeError:" + error);
            if (onErrorCallback != null) onErrorCallback();
        }
        #endregion

        #region API
        public void Dispose() {
            if (isDisposed) return;
            isDisposed = true;
            Destroy(gameObject);
            fileRoot = null;
            fileNames = null;
            fileLoader = null;
            onCompleteCallback = null;
        }
        #endregion

    }

}
