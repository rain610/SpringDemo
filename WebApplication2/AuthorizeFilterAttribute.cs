using Rain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication2
{
    public class AuthorizeFilterAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var isAuthorized = false;
            if (httpContext != null && httpContext.Session != null)
            {
                var mycookie = httpContext.Request.Cookies["RainCookie"];
                if (mycookie != null && SessionStateManage.GetUser(mycookie.Value) != null)
                {
                    isAuthorized = true;
                }
                //if (HttpContext.Current.Session["RainCookie"] != null)
                //{
                //    isAuthorized = true;
                //}
            }
            if (isAuthorized)
            {
                httpContext.Response.Redirect("~/Home/Index");
            }
            return isAuthorized;
        }

        /// <summary>
        /// 重写AuthorizeAttribute的HandleUnauthorizedRequest跳转到相应的页面
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            base.HandleUnauthorizedRequest(filterContext);
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }
            else
            {
                filterContext.HttpContext.Response.Redirect("~/Login/Index");
            }
        }
    }
}