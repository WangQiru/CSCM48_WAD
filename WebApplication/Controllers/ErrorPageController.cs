using System;
using System.Web.Mvc;

namespace WebApplication.Controllers
{
    public class ErrorPageController : Controller
    {
        public ActionResult Error(int statusCode, string statusMsg, Exception exception)
        {
            Response.StatusCode = statusCode;
            ViewBag.StatusCode = statusCode + " Error";
            if (statusCode == 404)
            {
                statusMsg = "Oh snap, you are lost, maybe try again?";
            }

            else if (statusCode == 401)
            {
                statusMsg = "That's awful, but you are not authorized to view this page.";
            }

            else if (statusCode == 502)
            {
                statusMsg = "Aha, you just witnessed a server failure, maybe try again later?";
            }

            else if (statusCode == 503)
            {
                statusMsg = "Looks like our server is having trouble, maybe try again later?";
            }

            else
            {
                statusMsg = "This error is mysterious just like the universe , maybe try again later?";
            }

            ViewBag.StatusMsg = statusMsg;

            return View();
        }
    }
}