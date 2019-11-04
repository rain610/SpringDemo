using Rain.Entities;
using Rain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication2.Controllers
{
    public class BaseController : Controller
    {
        public string SessionCookieValue { set; get; }
        public LoginVisualEntity UserSession { set; get; }

        string cookieName;
        public BaseController()
        {
            cookieName = SessionStateManage.CookieName;

            HttpCookie cookie = System.Web.HttpContext.Current.Request.Cookies[cookieName];
            if (cookie != null)
            {
                SessionCookieValue = cookie.Value;
                if (string.IsNullOrEmpty(SessionCookieValue))
                {
                    UserSession = SessionStateManage.GetUser(SessionCookieValue);
                }
            }

        }

        public void Login(LoginVisualEntity info)
        {
            string sessionID = Guid.NewGuid().ToString();
            System.Web.HttpContext.Current.Response.Cookies.Set(new HttpCookie(cookieName, sessionID));
            SessionStateManage.SetLogin(sessionID, info);
        }
        
    }
}