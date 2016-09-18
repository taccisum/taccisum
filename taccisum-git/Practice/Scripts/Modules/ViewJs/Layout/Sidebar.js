/**
 * @file Sidebar.js
 * @see jquery.js Composite.js 
 * @author tac
 * @desc side bar 菜单管理
 */


define(function () {
    var sidebar = null;

    var Sidebar = function(menus, appendTo) {
        var self = this;
        var cb = new CompositeBuilder("ID", "ParentId");
        var root = cb.Build(menus);
        var $container = $(appendTo);

        this.tree = root;

        this.GetById = function(id) {
            return $($container.find("li[data-id=" + id + "]")[0]);
        }

        this.CurrentMenuId = function() {
            return $($container.find("li a[data-rpath='" + location.pathname + "']")[0]).parent("li").data("id");
        };

        //清空容器中的菜单内容
        this.Clear = function() {
            $container.children().remove();
            return self;
        }

        //将菜单加载到容器中
        this.Load = function() {
            self.Clear();
            root.ForEach(new Visitor(function(node) {
                var $li = $("<li></li>");
                var $content = $("<a></a>");
                var $arrow = $("<b class='arrow'></b>");

                $li.attr("data-id", node.data.ID);
                $content.attr("href", node.data.Url);

                var rPath = node.data.Url.match(/(^[^\?]+)\?/g); //相对路径，用于选中当前页菜单
                rPath = rPath != null ? rPath[0].substr(0, rPath[0].length - 1) : node.data.Url; //去掉?号
                $content.attr("data-rpath", rPath);

                $content.append($("<i></i>").addClass(node.data.Icon))
                    .append($("<span></span>").addClass("menu-text").text(" " + node.data.Name + " "))
                    .append($arrow);

                $li.append($content);
                if (node.level == 1) {
                    //顶级菜单
                    $container.append($li);
                } else {
                    //子菜单
                    var $parent = self.GetById(node.data.ParentId);
                    if ($parent.length > 0) {
                        $($parent.children("a")[0]).attr("href", "#").addClass("dropdown-toggle")
                            .append($("<b class='arrow icon-angle-down'></b>"));
                        var $subMenu = $parent.children("ul.submenu");
                        if ($subMenu.length <= 0) {
                            $subMenu = $("<ul class='submenu'></ul>");
                            $parent.append($subMenu);
                        }

                        $subMenu.append($li);
                    }
                }
            }, true));
            return self;
        };

        //打开指定id的菜单
        //isActivate: bool 是否要将菜单标识为active
        this.Open = function(id, isActivate) {
            root.ForEach(new Visitor(function(node) {
                if (node.data.ID == id) {
                    var $now = self.GetById(node.data.ID);
                    if (isActivate) {
                        $now.addClass("active"); //选中当前菜单
                    }

                    var activeParent = function(cnode) {
                        //递归展开
                        if (cnode.pnode.level > 0) {
                            var $p = self.GetById(cnode.pnode.data.ID);
                            var $menu = $p.children("ul");
                            if (isActivate) {
                                $p.addClass("active");
                            }
                            $p.addClass("open");
                            $menu.show();
                            activeParent(cnode.pnode);
                        }
                    }

                    activeParent(node);
                }
            }, true));

            return self;
        };

        //折叠所有菜单
        //isActive: bool 是否要折叠active的菜单
        this.CollapseAll = function(isActive) {
            if (isActive) {
                $container.find("li").removeClass("active open");
                $container.find(".submenu").hide();
            } else {
                var $topMenu = $container.children("li").not(".active").removeClass("open");
                $topMenu.find("li").removeClass("open");
                $topMenu.find(".submenu").hide();
            }
        };


        this.PointAt = function(id) {
            throw Error("function Sidebar.PointAt() is not implement!");
            //sys.dialog({
            //    id: "dialog-tips",
            //    content: "here you find~",
            //    quickClose: true,
            //}).show(self.GetById(id).find("a")[0]);
        };

    }

    return {
        getInstance: function(munus, appendTo) {
            if (sidebar == null)
                sidebar = new Sidebar(munus, appendTo);
            return sidebar;
        }
    }
})
