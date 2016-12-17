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
    public class AnouncementModelsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: AnouncementModels
        public ActionResult Index()
        {
            return View(db.Anouncements.ToList());
        }

        public JsonResult AjaxRetrieve(int id)
        {
            var anouncements = db.Anouncements.ToList();
            return Json(anouncements, JsonRequestBehavior.AllowGet);
        }

        // GET: AnouncementModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AnouncementModels anouncementModels = db.Anouncements.Find(id);
            if (anouncementModels == null)
            {
                return HttpNotFound();
            }
            return View(anouncementModels);
        }

        // GET: AnouncementModels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AnouncementModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Title,Text,publishTime,LectureNo")] AnouncementModels anouncementModels)
        {
            if (ModelState.IsValid)
            {
                db.Anouncements.Add(anouncementModels);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(anouncementModels);
        }

        // GET: AnouncementModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AnouncementModels anouncementModels = db.Anouncements.Find(id);
            if (anouncementModels == null)
            {
                return HttpNotFound();
            }
            return View(anouncementModels);
        }

        // POST: AnouncementModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Title,Text,publishTime,LectureNo")] AnouncementModels anouncementModels)
        {
            if (ModelState.IsValid)
            {
                db.Entry(anouncementModels).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(anouncementModels);
        }

        // GET: AnouncementModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AnouncementModels anouncementModels = db.Anouncements.Find(id);
            if (anouncementModels == null)
            {
                return HttpNotFound();
            }
            return View(anouncementModels);
        }

        // POST: AnouncementModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AnouncementModels anouncementModels = db.Anouncements.Find(id);
            db.Anouncements.Remove(anouncementModels);
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
