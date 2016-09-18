using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common.Tool.Extend;
using Model.Common;
using Model.CommonModel;
using Model.Entity;
using Model.Models;
using Practice.Controllers.Base;
using Service.Interf.Sys;


namespace Practice.Controllers
{
    [Export]
    public class UserDemoController : BaseController
    {
        [Import]
        protected ISysUserDemoService SysUserDemoService { get; set; }

        // GET: UserDemo
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult GetUserList(UserDemoQuery args)
        {
            var user = SysUserDemoService.GetAll();
            var total = user.Count();

            var flag = new
            {
                account = string.IsNullOrWhiteSpace(args.account),
                nickname = string.IsNullOrWhiteSpace(args.nickname)
            };

            user = from u in user
                   where flag.account || u.Account.Contains(args.account)
                   where flag.nickname || u.NickName.Contains(args.nickname)                   
                   select u;


            if (args.order != null)
            {
                //todo::
            }
            else
            {
                user = user.OrderByDescending(u => u.CreatedOn);
            }


            var tableData = new DataTablesResult()
            {
                draw = args.draw,
                recordsTotal = total,
                recordsFiltered = user.Count(),
                data = user.QueryForPage(args.pageindex, args.length)
            };

            return Json(tableData, JsonRequestBehavior.AllowGet);
        }


        public ActionResult Verify(string uid, string psd)
        {
            var valid = false;

            var user = SysUserDemoService.Verify(uid, psd);
            if (user != null)
            {
                valid = true;
            }
            return Json(valid, JsonRequestBehavior.DenyGet);
        }



        public JsonResult InsertUser(SysUserDemo user)
        {
            user.Password = user.Password.ToMD5();

            var r = SysUserDemoService.Create(user);
            var result = new ApiResult()
            {
                Success = r != null,
                Data = user,
                Message = r != null ? "成功" : "失败"
            };

            return Json(result, JsonRequestBehavior.DenyGet);
        }


        public JsonResult Remove(string idList)
        {
            ApiResult result;

            var idArr = idList.Split(',');

            if (idArr.Any() && !string.IsNullOrWhiteSpace(idArr[0]))
            {
                var ids = idArr.Select(id => id.ToGuid());

                foreach (var id in ids)
                {
                    SysUserDemoService.Delete(id);
                }

                result = ApiResult.SuccessResult(ids.Count(), "删除成功");
            }
            else
            {
                result = ApiResult.FailedResult("未选中任何数据");
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}