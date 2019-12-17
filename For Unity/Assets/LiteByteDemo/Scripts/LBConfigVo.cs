namespace LiteByte.Demo {

    /// <summary>
    /// LiteByte配置数据 | Config Data
    /// ZhangYu 2019-12-10
    /// </summary>
    [System.Serializable]
    public class LBConfigVo {

        public ShortNameVo[] baseTypeShortNames;
        public string typeFileRoot;
        public string[] typeFileNames;

    }

    [System.Serializable]
    public class ShortNameVo {

        public string type;
        public string name;

    }

}
