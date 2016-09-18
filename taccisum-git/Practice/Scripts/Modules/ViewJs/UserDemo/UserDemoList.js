define(["systools", "mockdata"], function (tool, mockdata) {
    var $d_add_user = $("#dialog-add_user");
    var dialogr_add_use_html = $d_add_user.html();
    $d_add_user.remove();

    var ls_args = "query_args" + location.pathname;
    var vm = {
        account: ko.observable(),
        nickname: ko.observable()
    };

    return {
        init: function() {
            if (window.localStorage) {
                var args = eval("(" + window.localStorage.getItem(ls_args) + ")");
                if (!$.isNull(args) && typeof args == "object") {
                    $.each(args, function (key, val) {
                        vm[key] = ko.observable(val);
                    });
                }
            }

            ko.applyBindings(vm, document.getElementById("area-parameters"));

            var table = tool.table("#table_id", {
                serverSide: true,
                ajax: {
                    url: "/UserDemo/GetUserList",
                    type: "post",
                    data: function (d) {
                        //添加额外的参数传给服务器
                        $.extend(d, ko.mapping.toJS(vm));
                    }
                },
                columns: [
                    { data: 'ID', title: 'ID', visible: false, },
                    { data: 'Account', title: 'Account' },
                    { data: 'Password', title: 'Password' },
                    { data: 'NickName', title: 'NickName' },
                    { data: 'CreatedOn', title: 'CreatedOn' },
                    { data: 'CreatedBy', title: 'CreatedBy' },
                ],
            });

            $("#btn-add").bind("click", function () {
                var user_vm;

                tool.dialog({
                    id: "dialog-chat",
                    title: "新增用户",
                    content: dialogr_add_use_html,
                    width: "500px",
                    button: [
                        {
                            id: "cancel",
                            value: "<i class='icon-remove'></i>取消",
                            callback: function () {
                                return true;
                            },
                            cls: "btn btn-default btn-sm",
                        },
                        {
                            id: "ok",
                            value: "<i class='icon-ok'></i>确定",
                            callback: function () {
                                var self = this;
                                self.title("提交中…");

                                var params = ko.mapping.toJS(user_vm);
                                $.post("/UserDemo/InsertUser", params, function (result) {
                                    if (result.Success) {
                                        tool.msgbox("添加用户成功！","y");
                                        self.remove();
                                        table.ajax.reload();
                                    } else {
                                        tool.msgbox("添加用户失败！<br /><a class='click' href=\"javascript:alert('这里显示异常信息')\">点击查看详细异常</a>", "n");
                                    }
                                });

                                return false;
                            },
                            cls: "btn btn-success btn-sm",
                        },
                    ],
                    onshow: function () {
                        user_vm = {
                            Account: ko.observable(""),
                            Password: ko.observable(""),
                            NickName: ko.observable(""),
                            Gender: ko.observable(true),
                        }
                        ko.applyBindings(user_vm, document.getElementById("content-add_user"));
                        $("#btn_mock_user").on("click", function() {
                            user_vm.Account(mockdata.ChineseName(3));
                            user_vm.Password(mockdata.RandomStr(10));
                            user_vm.NickName(mockdata.ChineseName(2));
                        });
                    }
                }).showModal();
            });

            $("#btn-query").bind("click", function () {

                if (window.localStorage) {
                    window.localStorage.setItem(ls_args, JSON.stringify(ko.mapping.toJS(vm)));
                }

                table.ajax.reload();
            });
            $("#btn-delete").bind("click", function () {

                var rows = table.rows('.selected').data();
                var idList = "";

                $.each(rows, function (index, row) {
                    idList += row.ID + ",";
                });

                idList = idList.substr(0, idList.length - 1);
                $.get("/UserDemo/Remove?idList=" + idList, function (result) {
                    var icon = "n";
                    if (result.Success) {
                        icon = "y";
                        table.ajax.reload();
                    }
                    tool.msgbox(result.Message, icon);
                });
            });

            $(".query-input").each(function () {
                $(this).keyup(function (event) {
                    var e = event || window.event;
                    if (e.keyCode == "13") {
                        $("#btn-query").click();
                    }
                });
            });
        }
    }


});
