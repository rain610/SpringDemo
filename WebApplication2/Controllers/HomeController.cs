using Rain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication2.Controllers
{
    //[AuthorizeFilter]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var test = new AddressRepository();
            var aa = test.List();
            //var bb = new DocumentCenterFileRepository();
            //var b1 = bb.List();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult ExportTestWord()
        {
            return File(new AddressRepository().Test(), "application/msword", "Test" + ".docx");
        }
    }
}