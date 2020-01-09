// 导入LiteByte所需的文件
function importLiteByte() {
    var root = "js/litebyte/";
    var files = [
        "common/lb_base_type.js",
        "common/lb_type.js",
        "common/lb_config.js",
        "converters/lb_encoding.js",
        "converters/lb_reader.js",
        "converters/lb_writer.js",
        "converters/lb_converter.js",
        "analyzers/lb_lexer.js",
        "analyzers/lb_parser.js",
        "analyzers/lb_report.js",
        "analyzers/lb_tag.js",
        "analyzers/lb_token.js",
        "lb_object.js",
        "lb_util.js"
    ];
    for (i = 0; i < files.length; i++) {
        var url = root + files[i];
        document.write('<script src="' + url + '" type="text/javascript" charset="utf-8"></script>');
    }
    document.write('<script src="js/custom_config.js" type="text/javascript" charset="utf-8"></script>');
    document.write('<script src="js/custom_config_loader.js" type="text/javascript" charset="utf-8"></script>');
    document.write('<script src="js/custom_types.js" type="text/javascript" charset="utf-8"></script>');
}

importLiteByte();