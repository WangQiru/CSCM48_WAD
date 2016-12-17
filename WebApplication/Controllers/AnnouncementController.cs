using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class AnnouncementController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        //Using ajax to accept announcement submissions
        [ValidateAntiForgeryToken]
        public JsonResult AjaxCreate(string title, string text)
        {           
            string userId = User.Identity.GetUserId();
            var user = db.Users.Find(userId);
            AnnouncementModels am = new AnnouncementModels();
            am.PublishTime = DateTime.Now;
            am.Text = Server.HtmlEncode(text);
            am.Title = Server.HtmlEncode(title);
            am.LecturerNo = user.LectureNo;
            am.LecturerName= user.Name;
            db.Announcements.Add(am);
            db.SaveChanges();
            return Json("Response from create");
        }

        // Retrieve all announcements via Ajax
        public JsonResult AjaxRetrieve()
        {
            var announcementModels = db.Announcements.ToList().Take(5).OrderBy(a => a.PublishTime).Reverse();
            return Json(announcementModels, JsonRequestBehavior.AllowGet);
        }


        // GET: Announcement/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AnnouncementModels announcementModels = db.Announcements.Find(id);
            if (announcementModels == null)
            {
                return HttpNotFound();
            }
            return View(announcementModels);
        }

        // POST: Announcement/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AnnouncementModels announcementModels = db.Announcements.Find(id);
            db.Announcements.Remove(announcementModels);
            db.SaveChanges();
            return RedirectToAction("Index");
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
