using Microsoft.AspNet.Identity;
using System.Web.Mvc;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public JsonResult AjaxRetrieveUserName()
        {
            string userId = User.Identity.GetUserId();
            var user = db.Users.Find(userId);

            return Json(user.Name, JsonRequestBehavior.AllowGet);
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
    }
}