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
    public class OptionController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Option
        public ActionResult Index()
        {
            var options = db.Options.Include(o => o.QuestionModels);
            return View(options.ToList());
        }

        // GET: Option/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OptionModels optionModels = db.Options.Find(id);
            if (optionModels == null)
            {
                return HttpNotFound();
            }
            return View(optionModels);
        }

        // GET: Option/Create
        public ActionResult Create()
        {
            ViewBag.QuestionID = new SelectList(db.Questions, "ID", "Text");
            return View();
        }

        // POST: Option/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,QuestionID,Text,correct")] OptionModels optionModels)
        {
            if (ModelState.IsValid)
            {
                db.Options.Add(optionModels);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.QuestionID = new SelectList(db.Questions, "ID", "Text", optionModels.QuestionID);
            return View(optionModels);
        }

        // GET: Option/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OptionModels optionModels = db.Options.Find(id);
            if (optionModels == null)
            {
                return HttpNotFound();
            }
            ViewBag.QuestionID = new SelectList(db.Questions, "ID", "Text", optionModels.QuestionID);
            return View(optionModels);
        }

        // POST: Option/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,QuestionID,Text,correct")] OptionModels optionModels)
        {
            if (ModelState.IsValid)
            {
                db.Entry(optionModels).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.QuestionID = new SelectList(db.Questions, "ID", "Text", optionModels.QuestionID);
            return View(optionModels);
        }


        [ValidateAntiForgeryToken]
        public JsonResult AjaxUpdate(string text, string id, bool correct)
        {
            OptionModels optionModels = db.Options.Find(int.Parse(id));
            optionModels.Text = text;
            optionModels.correct = correct;
            db.Entry(optionModels).State = EntityState.Modified;
            db.SaveChanges();

            return Json("Response from update");
        }

        // GET: Option/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OptionModels optionModels = db.Options.Find(id);
            if (optionModels == null)
            {
                return HttpNotFound();
            }
            return View(optionModels);
        }

        // POST: Option/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OptionModels optionModels = db.Options.Find(id);
            db.Options.Remove(optionModels);
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
