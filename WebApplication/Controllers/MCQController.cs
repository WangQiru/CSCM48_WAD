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
    public class MCQController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: MCQ
        public ActionResult Index()
        {
            return View(db.MCQs.ToList());
        }

        // GET: MCQ/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MCQModels mCQModels = db.MCQs.Find(id);
            if (mCQModels == null)
            {
                return HttpNotFound();
            }
            return View(mCQModels);
        }

        // GET: MCQ/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MCQ/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Title,Description,ReleaseDate,DueDate")] MCQModels mCQModels)
        {
            if (ModelState.IsValid)
            {
                db.MCQs.Add(mCQModels);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(mCQModels);
        }

        // GET: MCQ/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MCQModels mCQModels = db.MCQs.Find(id);
            if (mCQModels == null)
            {
                return HttpNotFound();
            }


            var results = (from q in db.Questions.Where(u => u.MCQID == id).Select(u => new
            {
                ID = u.ID,
                Text = u.Text
            })
                           from o in db.Options.Where(u => u.QuestionID == q.ID).Select(u => new
                           {
                               ID = u.ID,
                               Text = u.Text
                           })
                           select new
                           {
                               questionID = q.ID,
                               optionID = o.ID,
                               optionText = o.Text
                           }
                           );


            var questions = db.Questions.Where(u => u.MCQID == id).Select(u => new
            {
                ID = u.ID,
                Text = u.Text
            }).ToList();

            List<object> QuestionList = new List<object>();
            foreach (var question in questions)
            {
                QuestionList.Add(new
                {
                    ID = question.ID,
                    Text = question.Text
                });
            }

            ViewBag.QuestionList = new SelectList(QuestionList, "ID", "Text");
            ViewBag.results = questions;

            return View(mCQModels);
        }

        // POST: MCQ/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Title,Description,ReleaseDate,DueDate")] MCQModels mCQModels)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mCQModels).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(mCQModels);
        }

        // GET: MCQ/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MCQModels mCQModels = db.MCQs.Find(id);
            if (mCQModels == null)
            {
                return HttpNotFound();
            }
            return View(mCQModels);
        }

        // POST: MCQ/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MCQModels mCQModels = db.MCQs.Find(id);
            db.MCQs.Remove(mCQModels);
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
