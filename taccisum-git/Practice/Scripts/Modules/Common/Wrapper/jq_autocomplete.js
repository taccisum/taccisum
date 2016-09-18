/**
 * @author tac
 * @desc 封装jquery自动补全插件，使用缺省参数将其封装成模板
 */

define(["jq_ui"], function() {
    function DefConfig() {
        this.delay = 0;
        this.appendTo = "#jq-ui-autocomplete-1";
        this.height = 300;
        this.width = 150;
        this.zindex = 1000; //设置为1000使autocomplete框总显示在最上层
    }; //默认配置

    return {
        selector: "",
        load: function (config) {
            var conf = $.extend(new DefConfig(), config);
            var $this = $(this.selector);

            //
            conf = $.extend(conf, {
                focus: function(event, ui) {
                    $this.val(ui.item.label);
                    $this.attr("data-val", ui.item.value);

                    if ($.isFunction(config.focus)) {
                        config.focus();
                    }
                    return false;
                },
                select: function(event, ui) {
                    $this.val(ui.item.label);
                    $this.attr("data-val", ui.item.value);

                    if ($.isFunction(config.select)) {
                        config.select();
                    }
                    return false;
                },
                descFormatter: function(item) {
                    if ($.isFunction(config.descFormatter))
                        return config.descFormatter(item);
                    return item.desc;
                }
            });


            var $r = $this.autocomplete(conf);


            //show description
            $r.data("ui-autocomplete")._renderItem = function(ul, item) {
                var $html = $("<li>");

                var desc = conf.descFormatter(item);

                if ($.isNullOrEmptyString(desc)) {
                    $html.append("<a><span class='bold'>" + item.label + "</span></a>");
                } else {
                    $html.append("<a><span class='bold'>" + item.label + "</span><br /><span style='font-size: 8px;'>" + desc + "</span></a>");
                }
                return $html.append("<hr style='height:5px; margin:0;' />").appendTo(ul);
            };

            $(conf.appendTo + " .ui-autocomplete").css({
                "max-height": conf.height + "px",
                "min-width": conf.width + "px",
                "overflow-y": "auto",
                /* 防止水平滚动条 */
                "overflow-x": "hidden",
                "z-index": conf.zindex
                //todo:: 可在这里添加更多的样式配置参数
            });

            return $r;
        }
    }
})
