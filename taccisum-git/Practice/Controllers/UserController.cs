using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common.Global;
using Common.Tool.Extend;
using Common.Tool.Units;
using IoC.Manager;
using log4net;
using Model.Common;
using Practice.Controllers.Base;
using Service.Interf.Sys;

namespace Practice.Controllers
{
    [Export]
    public class UserController : BaseController
    {
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }


        public ActionResult Verify(string uid, string psd, bool remeber)
        {
            Log.Info("用户\"" + uid + "\"尝试验证登录");

            var user = SysUserService.LoginVerify(uid, psd);
            var valid = user.ID != Guid.Empty;

            if (valid)
            {
                if (remeber)
                {
                    //var token = _userBusiness.GenerateAutoLoginToken();
                    //CookiesHelper.Add(GlobalConfig.AUTOLOGIN.ToMD5(), token.ToString(), DateTime.Now.AddHours(2));
                }
                CookiesHelper.Add(GlobalConfig.CURRENT_USER.ToMD5(), user.ID.ToString(), DateTime.Now.AddHours(2));
            }

            Log.Info("用户\"" + uid + "\"登录" + (valid ? "成功" : "失败"));

            return Json(valid, JsonRequestBehavior.DenyGet);
        }

        public ActionResult RegisterAccount(string uid, string psd, string rePsd)
        {
            if (psd != rePsd)
            {
                return Json(new ApiResult()
                {
                    Success = false,
                    Data = null,
                    Message = "两次输入的密码不相同"
                },JsonRequestBehavior.DenyGet);
            }
            else
            {
                return Json(new ApiResult()
                {
                    Success = true,
                    Data = SysUserService.Register(uid, psd),
                    Message = "注册成功"
                }, JsonRequestBehavior.DenyGet);
            }
        }

        public ActionResult Logout()
        {
            CookiesHelper.Remove(GlobalConfig.CURRENT_USER);
            CookiesHelper.Remove(GlobalConfig.AUTOLOGIN);
            return View("Login");
        }
    }
}