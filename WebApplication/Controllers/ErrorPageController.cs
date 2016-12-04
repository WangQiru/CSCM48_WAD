using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication.Controllers
{
    public class ErrorPageController : Controller
    {
        public ActionResult Error(int statusCode, Exception exception)
        {
            Response.StatusCode = statusCode;
            ViewBag.StatusCode = statusCode + " Error";
            return View();
        }

        public ActionResult ErrorPage(int id)
        {
            Response.StatusCode = id;
            ViewBag.StatusCode = "Error " + id;
            return View();
        }
    }
}