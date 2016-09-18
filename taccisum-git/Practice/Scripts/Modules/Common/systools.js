/**
 * @author tac
 * @date 16/09/05
 * @desc 系统前端工具模块，用于管理当前系统框架中一些常用插件的使用。
 * @important tips
 * 1、本模块定义为系统前端工具的调用入口。如有需要修改当前已封装好的插件，请确保对该插件的使用已经非常熟悉，以免造成某些前端模块崩溃；
 * 2、非特殊情况下不能直接调用本模块中引入的插件，必须通过本模块提供的方法来使用这些插件；
 * 3、如果在系统中引入了新的插件，需对其进行适当的封装，然后在本模块添加新的调用入口，同时在ToolDemoController新增相应的使用演示demo页；
 */
define(["w_datatables", "w_jq_ac", "w_art_dialog"], function (dt, ac, dg) {
    var sys = {
        /**
         * 
         * @returns {} 
         */
        ajax: function() {
            
        },

        /**
         * @author tac
         * @desc 指定一个table标签，为其为基础生成datatables
         * @param {string} target 目标元素，格式为jquery-selector
         * @param {} config 参数jquery-datatables开发文档——http://datatables.club/reference/  16/09/08
         * @returns {object} datatables实例
         */
        table: function(target, config) {
            dt.selector = target;
            return dt.load(config);
        },
        /**
         * @author tac
         * @desc 指定一个input标签，为其添加自动补全功能
         * @param {string} target 目标元素，格式为jquery-selector
         * @param {config} config 参考jquery-Autocomplete开发文档——http://api.jqueryui.com/autocomplete/  16/09/08
         * @returns {object} 
         */
        autocomplete: function (target, config) {
            ac.selector = target;
            return ac.load(config);
        },
        /**
         * @author tac
         * @desc 弹出一个dialog（需使用返回值手动调用show()或showModal()显示dialog实例），显示指定内容
         * @param {object} config 参考artDialog.js开发文档——http://lab.seaning.com/_doc/API.html#show 16/09/08
         * @returns {object} artDialog实例
         */
        dialog: function (config) {
            return dg.dialog(config);
        },
        /**
         * @author tac
         * @desc 弹出一个message box（排版固定），显示指定内容
         * @param {string} msg 要展示的信息，支持html
         * @param {string} icon 要显示的图片类型：y(yes)、n(no)、i(info)、w(warning)、q(question)
         * @param {number} timer box自动关闭时间，单位ms，小于等于0时不自动关闭
         * @param {string} title 标题，不支持html
         * @returns {object} artDialog实例
         */
        msgbox: function (msg, icon, timer, title) {
            var config = {
                content: msg,
                icon: icon,
                title: title
            };
            if (timer != null) {
                if (timer <= 0) {
                    config.timer = 10000000;
                } else {
                    config.timer = timer;
                }
            }

            return dg.msgbox(config);
        }
    };

    return sys;
});