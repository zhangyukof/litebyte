namespace LiteByte.Demo {

    using UnityEngine;
    using UnityEngine.Events;

    /// <summary>
    /// LiteByte InfoUI
    /// ZhangYu 2019-11-23
    /// </summary>
    public class LBInfoUI : MonoBehaviour {

        public StringEvent onTextUpdate;
        public StringEvent onByteSizeUpdate;
        public StringEvent onSerializeTimeUpdate;
        public StringEvent onDeserializeTimeUpdate;
        [System.Serializable] public class StringEvent : UnityEvent<string> { }
        private string m_text;
        private string m_byteSize;
        private string m_serializeTime;
        private string m_deserializeTime;
        private bool isTextUpdate;
        private bool isByteSizeUpdate;
        private bool isSerializeTimeUpdate;
        private bool isDeserializeTimeUpdate;

        private void Start() {
            Clear();
            Refresh();
        }

        public string text {
            set {
                if (value == m_text) return;
                m_text = value;
                isTextUpdate = true;
            }
        }

        public string byteSize {
            set {
                if (value == m_byteSize) return;
                m_byteSize = value;
                isByteSizeUpdate = true;
            }
        }

        public string serializeTime {
            set {
                if (value == m_serializeTime) return;
                m_serializeTime = value;
                isSerializeTimeUpdate = true;
            }
        }

        public string deserializeTime {
            set {
                if (value == m_deserializeTime) return;
                m_deserializeTime = value;
                isDeserializeTimeUpdate = true;
            }
        }

        public void Refresh() {
            if (isTextUpdate) onTextUpdate.Invoke(m_text);
            if (isByteSizeUpdate) onByteSizeUpdate.Invoke(m_byteSize);
            if (isSerializeTimeUpdate) onSerializeTimeUpdate.Invoke(m_serializeTime);
            if (isDeserializeTimeUpdate) onDeserializeTimeUpdate.Invoke(m_deserializeTime);
            isTextUpdate = false;
            isByteSizeUpdate = false;
            isSerializeTimeUpdate = false;
            isDeserializeTimeUpdate = false;
        }

        public void Clear() {
            text = "";
            byteSize = "";
            serializeTime = "";
            deserializeTime = "";
        }

    }

}