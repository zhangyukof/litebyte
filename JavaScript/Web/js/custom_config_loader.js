class LBConfigLoader {

    /**
     * 加载配置文件
     * @param {Function} onComlete 完成后的回调方法
     */
    loadConfig(onComlete) {
        this._onComplete = onComlete;
        this.parseShortNames(customConfig.baseTypeShortNames);
        this.parseTypeFiles(customConfig.typeFileNames);
        if (this._onComplete != null) this._onComplete.call();
    }

    /**
     * 解析基本类型简称
     * @param {Array} shortNames 
     */
    parseShortNames(shortNames) {
        for (let i = 0; i < shortNames.length; i++) {
            let shortName = shortNames[i];
            LBConfig.addBaseTypeShortName(shortName.type, shortName.name);
        }
    }

    /**
     * 解析自定义类型文件
     * @param {Array} fileNames 
     */
    parseTypeFiles(fileNames) {
        for (let i = 0; i < fileNames.length; i++) {
            let fileName = fileNames[i];
            let code = customTypeLBS[fileName];
            let tokens = LBLexer.analyze(code)
            let report = LBParser.analyze(code, tokens);
            if (report.isSuccess) {
                LBConfig.addCustomType(report.type);
                LBConfig.checkAllCustomTypes();
            } else {
                throw "Type file analyzing failed\n" + report.error
            }
        }
    }
}

