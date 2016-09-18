using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.Entity;

namespace Practice.Controllers.Attributes
{
    /// <summary>
    /// todo::not implement
    /// </summary>

    public class RequireAuthorizeAttribute : AuthorizeAttribute
    {



        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            return base.AuthorizeCore(httpContext);
        }

        public override void OnAuthorization(System.Web.Mvc.AuthorizationContext filterContext)
        {

        }
    }
}