using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class QuestionController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Question
        [ClaimsAuthorize(ClaimTypes.Role, "Lecturer")]
        public ActionResult Index()
        {
            var questions = db.Questions.Include(q => q.MCQModels);
            return View(questions.ToList());
        }

        // GET: Question/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuestionModels questionModels = db.Questions.Find(id);
            if (questionModels == null)
            {
                return HttpNotFound();
            }
            return View(questionModels);
        }

        // GET: Question/Create
        [ClaimsAuthorize(ClaimTypes.Role, "Lecturer")]
        public ActionResult Create()
        {
            ViewBag.MCQID = new SelectList(db.MCQs, "ID", "Title");
            return View();
        }

        // POST: Question/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ClaimsAuthorize(ClaimTypes.Role, "Lecturer")]
        public ActionResult Create([Bind(Include = "ID,MCQID,Text")] QuestionModels questionModels)
        {
            if (ModelState.IsValid)
            {
                db.Questions.Add(questionModels);
                for (int i = 0; i < 4; i++)
                {
                    OptionModels optionModels = new OptionModels();
                    optionModels.QuestionID = questionModels.ID;
                    optionModels.Text = "";
                    db.Options.Add(optionModels);
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MCQID = new SelectList(db.MCQs, "ID", "Title", questionModels.MCQID);
            return View(questionModels);
        }

        [ValidateAntiForgeryToken]
        [ClaimsAuthorize(ClaimTypes.Role, "Lecturer")]
        public JsonResult AjaxCreate(QuestionModels questionModels)
        {
            db.Questions.Add(questionModels);

            OptionModels optionModels = new OptionModels();
            optionModels.QuestionID = questionModels.ID;
            optionModels.Text = "";
            
            OptionModels optionModels2 = new OptionModels();
            optionModels2.QuestionID = questionModels.ID;
            optionModels2.Text = "";

            OptionModels optionModels3 = new OptionModels();
            optionModels3.QuestionID = questionModels.ID;
            optionModels3.Text = "";

            OptionModels optionModels4 = new OptionModels();
            optionModels4.QuestionID = questionModels.ID;
            optionModels4.Text = "";

            db.Options.Add(optionModels);
            db.Options.Add(optionModels2);
            db.Options.Add(optionModels3);
            db.Options.Add(optionModels4);

            db.SaveChanges();

            return Json("Response from create");
        }

        // GET: Question/Edit/5
        [ClaimsAuthorize(ClaimTypes.Role, "Lecturer")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuestionModels questionModels = db.Questions.Find(id);
            if (questionModels == null)
            {
                return HttpNotFound();
            }
            ViewBag.MCQID = new SelectList(db.MCQs, "ID", "Title", questionModels.MCQID);
            return View(questionModels);
        }

        // POST: Question/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ClaimsAuthorize(ClaimTypes.Role, "Lecturer")]
        public ActionResult Edit([Bind(Include = "ID,MCQID,Text")] QuestionModels questionModels)
        {
            if (ModelState.IsValid)
            {
                db.Entry(questionModels).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MCQID = new SelectList(db.MCQs, "ID", "Title", questionModels.MCQID);
            return View(questionModels);
        }

        [ValidateAntiForgeryToken]
        [ClaimsAuthorize(ClaimTypes.Role, "Lecturer")]
        public JsonResult AjaxUpdate(string text, string id)
        {
            QuestionModels questionModels = db.Questions.Find(int.Parse(id));
            questionModels.Text = text;
            db.Entry(questionModels).State = EntityState.Modified;
            db.SaveChanges();

            return Json("Response from update");
        }

        // GET: Question/Delete/5
        [ClaimsAuthorize(ClaimTypes.Role, "Lecturer")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuestionModels questionModels = db.Questions.Find(id);
            if (questionModels == null)
            {
                return HttpNotFound();
            }
            return View(questionModels);
        }

        // POST: Question/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [ClaimsAuthorize(ClaimTypes.Role, "Lecturer")]
        public ActionResult DeleteConfirmed(int id)
        {
            QuestionModels questionModels = db.Questions.Find(id);
            db.Questions.Remove(questionModels);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [ValidateAntiForgeryToken]
        [ClaimsAuthorize(ClaimTypes.Role, "Lecturer")]
        public JsonResult AjaxDelete(string id)
        {
            QuestionModels questionModels = db.Questions.Find(int.Parse(id));
            db.Questions.Remove(questionModels);
            db.SaveChanges();
            return Json("Response from delete");
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
