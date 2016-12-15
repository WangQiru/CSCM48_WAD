using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using WebApplication.Attribute;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class OptionController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Option
        public ActionResult Index()
        {
            var options = db.Options.Include(o => o.QuestionModels);
            return View(options.ToList());
        }

        [ValidateAntiForgeryToken]
        [ClaimsAuthorize(ClaimTypes.Role, "Lecturer")]
        public JsonResult AjaxUpdate(string text, string id, bool correct)
        {
            OptionModels optionModels = db.Options.Find(int.Parse(id));
            optionModels.Text = text;
            optionModels.correct = correct;
            db.Entry(optionModels).State = EntityState.Modified;
            db.SaveChanges();

            return Json("Response from update");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
