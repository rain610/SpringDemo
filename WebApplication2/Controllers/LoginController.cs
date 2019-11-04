using Rain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication2.Controllers
{
    public class LoginController : BaseController
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        //[HttpPost]
        public JsonResult LoginPost(LoginVisualEntity loginVisualEntity)
        {
            if (string.IsNullOrWhiteSpace(loginVisualEntity.UserName))
            {
                //return JsonResult(new { IsSuccess=false,Message=""},);
                //return ContentResult(new { });
            }
            if (string.IsNullOrWhiteSpace(loginVisualEntity.Password))
            {
                
            }
            if (UserSession == null)
            {
                UserSession = new LoginVisualEntity { Password = loginVisualEntity.Password, UserName = loginVisualEntity.UserName };
                Login(UserSession);
            }
            return Json(new { IsSuccess = true, Message = "" });
        }
    }
}