using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication.Controllers
{
    public class SessionController : Controller
    {
        // GET: Session
        public ActionResult Index()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        public JsonResult ClearSession(string type)
        {
            Session[type] = null;
            return Json("Response from ClearSession");
        }
    }
}