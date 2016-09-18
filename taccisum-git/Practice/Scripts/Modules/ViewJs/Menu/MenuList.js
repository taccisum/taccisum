define(["systools", "mockdata"], function (tool, mockdata) {
    var Module = function() {
        this.init = function () {
            var table = tool.table("#table_id", {
                serverSide: true,
                ajax: {
                    url: "/Menu/GetMenusList",
                    type: "post",
                    data: function (d) {
                        //添加额外的参数传给服务器
                        //$.extend(d, ko.mapping.toJS(vm));
                    }
                },
                columns: [
                    //{ data: 'ID', title: 'ID' },
                    { data: 'Name', title: 'Name' },
                    { data: 'ParentId', title: 'ParentId' },
                    { data: 'Url', title: 'Url' },
                    { data: 'Icon', title: 'Icon' },
                    { data: 'SortNo', title: 'SortNo' },
                    { data: 'EnabledState', title: 'EnabledState' },
                    { data: 'Description', title: 'Description' },
                    {
                        data: 'CreatedOn', title: 'CreatedOn', render: function (data, type, row, meta) {
                            var date = Date.parseTimestamp(data);
                            return date.Format("yyyy/MM/dd hh:mm:ss");
                        }
                    },
                    //{ data: 'CreatedBy', title: 'CreatedBy' },
                ],
                rowDbClick: function (api, rowData, tableData) {
                    alert(rowData["ID"]);
                }
            });
            $(".date-picker").datepicker({ autoclose: true }).next().on(ace.click_event, function () {
                $(this).prev().focus();
            });


            var dialog_add_menu_html = $("#dialog-add_menu").html();
            $("#dialog-add_menu").remove();

            $("#btn-add").on("click", function () {
                var menu_vm;

                tool.dialog({
                    id: "dialog-chat",
                    title: "新增用户",
                    content: dialog_add_menu_html,
                    width: "700px",
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
                            value: "<i class='icon-ok'></i>新增",
                            callback: function () {
                                var self = this;
                                self.title("提交中…");

                                var params = ko.mapping.toJS(menu_vm);
                                $.post("/Menu/Add", params, function (result) {
                                    var icon = "n";
                                    if (result.Success) {
                                        icon = "y";
                                        self.remove();
                                        table.ajax.reload();
                                    }
                                    tool.msgbox(result.Message, icon);
                                });

                                return false;
                            },
                            cls: "btn btn-success btn-sm",
                        },
                    ],
                    onshow: function () {
                        menu_vm = {
                            Name: ko.observable(""),
                            ParentId: ko.observable(""),
                            Icon: ko.observable(""),
                            Url: ko.observable(""),
                            Description: ko.observable("")
                        };
                        ko.applyBindings(menu_vm, document.getElementById("content-add_menu"));
                        $("#btn_mock_menu").on("click", function() {
                            menu_vm.Name(mockdata.RandomZhStr(3, 4));
                            menu_vm.Description(mockdata.RandomZhStr(15, 30));
                            menu_vm.Icon("icon-random");
                            menu_vm.ParentId("cfc814c7-b4c3-4b80-96f0-1063ff13be7a");
                            menu_vm.Url("http://www.baidu.com");
                        });
                    }
                }).showModal();

            });

            $("#btn-query").on("click", function () {

            });

            $("#btn-delete").on("click", function () {
                var rows = table.rows('.selected').data();
                var idList = "";

                $.each(rows, function (index, row) {
                    idList += row.ID + ",";
                });

                idList = idList.substr(0, idList.length - 1);
                $.get("/Menu/Remove?idList=" + idList, function (result) {
                    var icon = "n";
                    if (result.Success) {
                        icon = "y";
                        table.ajax.reload();
                    }
                    tool.msgbox(result.Message, icon);
                });
            });
            $("#btn-disable").on("click", function () {
                var rows = table.rows('.selected').data();
                var idList = "";

                $.each(rows, function (index, row) {
                    idList += row.ID + ",";
                });

                idList = idList.substr(0, idList.length - 1);
                $.get("/Menu/Disable?idList=" + idList, function (result) {
                    var icon = "n";
                    if (result.Success) {
                        icon = "y";
                        table.ajax.reload();
                    }
                    tool.msgbox(result.Message, icon);
                });
            });
        }
    };

    return new Module();
})