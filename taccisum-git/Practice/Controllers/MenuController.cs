using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Common.Tool.Extend;
using Model.Entity;
using Model.Models;
using System.ComponentModel.Composition;
using Model.CommonModel;
using Practice.Controllers.Base;
using Service;
using Service.Interf.Sys;

namespace Practice.Controllers
{
    [Export]
    public class MenuController : BaseController
    {
        [Import]
        protected ISysMenuService MenuService { get; set; }

        public ActionResult Test1()
        {
            return View();
        }

        // GET: Menu
        public ActionResult MenuList()
        {
            return View();
        }

        public JsonResult GetMenusList(MenuQuery query)
        {
            var list = MenuService.Query().OrderByDescending(m => m.CreatedOn);

            var tableData = new DataTablesResult
            {
                draw = query.draw,
                recordsTotal = list.Count(),
                recordsFiltered = list.Count(),
                data = list.QueryForStart(query.start, query.length)
            };

            return Json(tableData, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Add(SysMenu menu)
        {
            return Try(() =>
            {
                //todo
                menu.EnabledState = true;
                menu.SortNo = 0;
                MenuService.Insert(menu);
                return null;
            }, "添加失败", "添加成功", JsonRequestBehavior.DenyGet);
        }

        public JsonResult Remove(string idList)
        {
            return Try(() =>
            {
                List<Guid> list = idList.Split(',').Select(i => i.ToGuid()).ToList();
                MenuService.Delete(list);
                return null;
            }, "删除失败", "删除成功");
        }

        public JsonResult Disable(string idList)
        {
            return Try(() =>
            {
                List<Guid> list = idList.Split(',').Select(i => i.ToGuid()).ToList();
                MenuService.Disable(list);
                return null;
            }, "修改菜单状态失败", "修改菜单状态成功");
        }
    }
}