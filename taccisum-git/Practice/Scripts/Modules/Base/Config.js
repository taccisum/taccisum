/**
 * @author tac
 * @desc requireJS配置及常用模块路径
 * @date 16/09/05
 */

(function () {
    //path
    var base = "Base/";
    var common = "Common/";
    var wrapper = "Common/Wrapper/";
    var scripts = "../";

    require.config({
        baseUrl: "/Scripts/Modules",
        paths: {
            //public
            "base": base + "BaseModule",
            "mockdata": common + "mockdata",
            "sidebar": "ViewJs/Layout/Sidebar",


            //private
            "jquery": scripts + "jQuery/jquery-2.2.3", //这里定义jquery是为了给其它jquery插件的amd加载提供依赖，jquery.js不通过requirejs而是直接通过标签加载
            "systools": common + "systools",
            "systools-plus": common + "systools-plus",
            "jq_ext": common + "jq_extend",
            "js_ext": common + "js_extend",
            "w_datatables": wrapper + "datatables",
            "w_art_dialog": wrapper + "artDialog",
            "w_jq_ac": wrapper + "jq_autocomplete",
            "mockjs": scripts + "MockJS/mock",
            "composite": scripts + "CommonClass/composite",
            "list": scripts + "CommonClass/list",

            //plugins
            "jq_ui": "../../css/ace/js/jquery-ui-1.10.3.full.min",
            "datatables": scripts + "jQueryPlugins/Datatables/jquery.dataTables",
            "artDialog": scripts + "jQueryPlugins/artDialog/dist/dialog-plus",
        },
        shim: {
            //为一些非amd规范的js提供依赖
            "jq_ui": "jquery"
        }
    });
})();
