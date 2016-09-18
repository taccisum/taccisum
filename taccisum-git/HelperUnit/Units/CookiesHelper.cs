using System;
using System.Web;
using Common.Tool.Extend;

namespace Common.Tool.Units
{
    public static class CookiesHelper
    {
        /// <summary>
        /// 添加一个cookies
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="expires"></param>
        public static void Add(string name, string value, DateTime? expires = null)
        {
            HttpCookie cookie = new HttpCookie(name,value);
            if (expires != null)
            {
                cookie.Expires = expires.Value;
            }
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        /// <summary>
        /// 获取指定cookies的值
        /// </summary>
        /// <param name="cookieName"></param>
        /// <returns></returns>
        public static string Get(string cookieName)
        {
            cookieName = cookieName.ToMD5();
            string value = "";
            HttpCookie cook = HttpContext.Current.Request.Cookies[cookieName];
            if (cook != null)
            {
                value = cook.Value;
            }
            else
            {
                value = "";
            }
            return value;
        }

        public static void Remove(string cookieName)
        {
            cookieName = cookieName.ToMD5();
            HttpCookie cook = HttpContext.Current.Request.Cookies[cookieName];
            if (cook != null)
            {
                cook.Value = null;
                cook.Expires = System.DateTime.Now.AddSeconds(-1);
                HttpContext.Current.Response.Cookies.Add(cook);
            }

        }
    }
}
