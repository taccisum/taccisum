/**
 * @author tac
 * @desc 封装art dialog插件，使用缺省参数将其封装成模板
 * @see jquery.js dialog.js
 */
define(["artDialog"], function () {
    var btn = {
        "cancel": {
            id: "cancel",
            value: "<i class='icon-remove'></i>取消",
            callback: function() {
                return true;
            },
            cls: "btn btn-default btn-sm",
        },
        "ok": {
            id: "ok",
            value: "<i class='icon-ok'></i>确认",
            callback: function() {
                return true;
            },
            cls: "btn btn-success btn-sm",
        }
    };      //预置的按钮


    var path = {
        "y": "/Image/yes_128px_1132501_easyicon.net.png",
        "n": "/Image/close_128px_1132502_easyicon.net.png",
        "i": "/Image/information_128px_1132498_easyicon.net.png",
        "w": "/Image/alert_information_128px_1132500_easyicon.net.png",
        "q": "/Image/why_help_128px_1132499_easyicon.net.png"
    };

    function DefConfig() {

    }; //art dialog 默认配置

    function DefConfig1() {
        this.title = "系统提示";
        this.quickClose = true;     //快速关闭（可通过esc或点击dialog以外的区域关闭）
        this.timer = 3000;      //存在时间
        this.content = "<table style='width:100%; min-width: 220px; max-width:500px;'><tr>" +
            "<td style='vertical-align:top;'><img style='height:70px; width:70px; margin-right: 20px;' src='@img' class='img-circle'></td>" +
            "<td class='text-center' style='vertical-align: middle; width:100%; font-size:22px;'>@msg</td>" +
            "</tr></table>";
    }; //art dialog message box默认配置

    var isNull = function(obj) {
        return typeof obj == "undefined" || obj == null;
    }


    return {
        dialog: function(config) {
            var conf = $.extend(new DefConfig(), config);
            for (var index in conf.button) {
                if (conf.button.hasOwnProperty(index)) {
                    //preset的类型是string，则从预置中获取btn并覆盖之前btn对象
                    var item = conf.button[index].preset;
                    var callback = conf.button[index].callback;
                    if (typeof item == "string") {
                        if (isNull(btn[item])) {
                            delete conf.button[index]; //未在预置按钮中找到，则不展示
                        } else {
                            conf.button[index] = btn[item];
                            if (typeof callback == "function") {
                                conf.button[index].callback = callback;     //覆盖回调
                            }
                        }
                    }
                }
            }

            var d = dialog(conf);

            return d;
        },

        msgbox: function (config) {
            if (isNull(config.content)) {
                throw Error("msgbox(): argument msg is required");
            }

            if (isNull(config.icon)) {
                config.icon = "i";
            }

            var conf = new DefConfig1();
            
            //title
            if (!isNull(config.title)) {
                conf.title = config.title;
            }
            //timer
            if (!isNull(config.timer)) {
                conf.timer = config.timer;
            }
            //icon
            var _path = isNull(path[config.icon]) ? path["i"] : path[config.icon];

            //fill content
            conf.content = conf.content.replace("@img", _path).replace("@msg", config.content);

            var d = dialog(conf);
            d.show();

            setTimeout(function() {
                if (typeof d != "undefined")
                    d.close().remove();
            }, conf.timer);

            return d;
        }
    }
})