using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;


namespace Practice
{
    public class BundleConfig
    {

        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            //以下bundiles已经根据依赖关系按加载顺序排列好，新增js/css时一定要注意顺序
            //virtual path必须不与任何一个physical path匹配，否则会出现broken link

            #region css
            //核心css
            bundles.Add(new StyleBundle("~/bundles/css/core")
                .Include("~/css/bootstrap-3.3.5-dist/css/bootstrap.css"));

            //字体图标
            bundles.Add(new StyleBundle("~/bundles/css/icon-font")
                .Include("~/css/ace/css/font-awesome.min.css", new CssRewriteUrlTransform()));      //在font-awesome中使用了相对路径引用（.woff文件），这里必须使用CssRewriteUrlTransform将css文件中的路径都转换为绝对路径，否则会导致404

            //jquery插件css
            bundles.Add(new StyleBundle("~/bundles/css/jquery/plugins")
                .Include("~/Scripts/jQueryPlugins/Datatables/jquery.dataTables.css"));


            //ace模板的css(必须放在其他样式之后，以ace的样式覆盖原有样式)
            bundles.Add(new StyleBundle("~/bundles/css/ace")
                .Include("~/css/ace/css/ace.min.css",
                    "~/css/ace/css/ace-skins.min.css",
                    "~/css/ace/css/jquery-ui-1.10.3.full.min.css",
                    "~/css/ace/css/datepicker.css"));

            //系统通用的css
            bundles.Add(new StyleBundle("~/bundles/css/common")
                .Include("~/css/mystyle.css",
                    "~/css/commonstyle.css"));
            #endregion

            #region js
            //前端核心js
            bundles.Add(new ScriptBundle("~/bundles/js/core")
                .Include("~/Scripts/jQuery/jquery-2.2.3.js",
                    "~/css/bootstrap-3.3.5-dist/js/bootstrap.js"));

            //ace模板的js
            bundles.Add(new ScriptBundle("~/bundles/js/ace")
                .Include("~/css/ace/js/ace-extra.min.js",
                "~/css/layoutinit.js",
                    "~/css/ace/js/ace.min.js",
                    "~/css/ace/js/ace-elements.min.js"
                ));
            #endregion

        }

    }
}