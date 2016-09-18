using System;
using System.ComponentModel.Composition;
using System.Web.Mvc;
using System.Web.Routing;
using Common.Global;
using Common.Tool.Extend;
using Common.Tool.Units;
using IoC.Manager;
using log4net;
using Model.Common;
using Model.Entity;
using Service.Interf.Sys;

namespace Practice.Controllers.Base
{
    public abstract class BaseController : Controller
    {
        [Import]
        protected ISysUserService SysUserService { get; set; }

        private IIoC _ioc;
        protected IIoC IoC { get { return _ioc ?? (_ioc = IoCManager.GetInstance().Create()); } }

        private ILog _log;
        protected ILog Log { get { return _log ?? (_log = Log4NetHelper.GetLogger("Service." + this.GetType().Name)); } }

        protected SysUser CurrentUser
        {
            get
            {
                var userId = CookiesHelper.Get(GlobalConfig.CURRENT_USER).ToGuid();
                return SysUserService.GetById(userId);
            }
        }


        protected BaseController()
        {
            
        }

        protected ApiResult Success(object data, string msg = "")
        {
            return ApiResult.SuccessResult(data, msg);
        }


        protected ApiResult Failure(string msg, object data = null)
        {
            return ApiResult.FailedResult(msg, data);
        }

        /// <summary>
        /// 封装TryCatch操作，只适用于JsonResult
        /// </summary>
        /// <param name="func"></param>
        /// <param name="errMsg"></param>
        /// <param name="succMsg"></param>
        /// <param name="behavior"></param>
        /// <returns></returns>
        protected JsonResult Try(Func<object> func, string errMsg, string succMsg, JsonRequestBehavior behavior = JsonRequestBehavior.AllowGet)
        {
            object data;
            try
            {
                data = func();
            }
            catch (Exception e)
            {
                return Json(Failure(errMsg, e), behavior);
            }

            return Json(Success(data, succMsg), behavior);
        }



        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            if (filterContext.ActionDescriptor.ControllerDescriptor.ControllerName == "User" &&
                filterContext.ActionDescriptor.ActionName == "Login" && CurrentUser != null)
            {
                CookiesHelper.Remove(GlobalConfig.CURRENT_USER);
                CookiesHelper.Remove(GlobalConfig.AUTOLOGIN);
            }
            else if (CurrentUser == null && !(
                filterContext.ActionDescriptor.ControllerDescriptor.ControllerName == "User" &&
                (filterContext.ActionDescriptor.ActionName == "Verify" || filterContext.ActionDescriptor.ActionName == "Login")
                ))
            {
                filterContext.Result = new RedirectToRouteResult("Default",
                    new RouteValueDictionary(new {controller = "User", action = "Login"}));
            }
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);
            //todo:: 感脚下面这句可能会造成奇怪的效果，标记下
            filterContext.Result = Json(Failure("调用API的过程中发生了未经处理的异常", filterContext.Exception),
                JsonRequestBehavior.AllowGet);
            Log.Error("调用API的过程中发生了未经处理的异常", filterContext.Exception);
        }
    }
}