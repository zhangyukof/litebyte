using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

namespace LiteByte.Demo {

    /// <summary>
    /// 二进制文件加载器(通过Http下载到内存)
    /// ZhangYu 2019-08-21
    /// </summary>
    public class LBHttpFileLoader : MonoBehaviour {

        public delegate void Callback<T>(T arg);

        public void LoadAsync(string url, Callback<byte[]> onComplete, Callback<float> onProgress, Callback<string> onError, int priority = 0) {
            StartCoroutine(DoLoadAsync(url, onComplete, onProgress, onError, priority));
        }

        private IEnumerator DoLoadAsync(string url, Callback<byte[]> onComplete, Callback<float> onProgress, Callback<string> onError, int priority = 0) {
            using (UnityWebRequest request = UnityWebRequest.Get(url)) {
                request.SendWebRequest().priority = priority;
                float progress = -1;
                while (!request.isDone) {
                    // 加载进度
                    if (onProgress != null && progress != request.downloadProgress) {
                        progress = request.downloadProgress;
                        onProgress(progress * 1.1111111f);
                    }
                    yield return null;
                }
                if (!string.IsNullOrEmpty(request.error)) {
                    string error = "HttpFileLoader error! url:" + url + "\n" + request.error;
                    Debug.LogError(error);
                    // 加载出错
                    if (onError != null) onError(error);
                } else {
                    // 加载完毕
                    if (onProgress != null) onProgress(1);
                    byte[] bytes = request.downloadHandler.data;
                    request.Dispose();
                    if (onComplete != null) onComplete(bytes);
                }
            }
        }

    }

}