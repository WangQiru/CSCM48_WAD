using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication.Models;
using WebApplication.ViewModels;

namespace WebApplication.Controllers
{
    public class TestController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Test
        public ActionResult Index(){
            List<MCQModels> mCQModels = db.MCQs.ToList();
            List<MCQQuestionModel> mCQQuestionModels = new List<MCQQuestionModel>();
            foreach (var mcq in mCQModels)
            {
                MCQQuestionModel obj = new MCQQuestionModel();
                obj.MCQID = mcq.ID;
                obj.MCQTitle = mcq.Title;
                obj.MCQDesc = mcq.Description;
                obj.ReleaseDate = mcq.ReleaseDate;
                obj.DueDate = mcq.DueDate;
                obj.QuestionCount = db.Questions.Count(q=>q.MCQID==mcq.ID);
                mCQQuestionModels.Add(obj);
            }
                
            return View(mCQQuestionModels);
        }

        // GET: Test/Details/5
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


        // GET: Test/Start/5
        public ActionResult Start(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuestionOptionViewModel qQuestionOptionViewModel = new QuestionOptionViewModel();
            qQuestionOptionViewModel.QuestionModelsList = new List<QuestionModels>();

            qQuestionOptionViewModel.MCQModels = db.MCQs.Find(id);
            if (qQuestionOptionViewModel.MCQModels == null)
            {
                return HttpNotFound();
            }

            var questions = db.Questions.Where(u => u.MCQID == id).Select(u => new
            {
                ID = u.ID
            }).ToList();

            int count = 0;

            foreach (var question in questions)
            {
                qQuestionOptionViewModel.QuestionModelsList.Add(db.Questions.Find(question.ID));
                qQuestionOptionViewModel.QuestionModelsList[count].OptionsModelsList = new List<OptionModels>();
                var options = db.Options.Where(u => u.QuestionID == question.ID);

                foreach (var option in options)
                {
                    OptionModels om = db.Options.Find(option.ID);
                    qQuestionOptionViewModel.QuestionModelsList[count].OptionsModelsList.Add(om);
                }
                count++;
            }

            return View(qQuestionOptionViewModel);
        }
    

        // POST: Test/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Start([Bind(Include = "ID,Title,Description,ReleaseDate,DueDate")] MCQModels mCQModels)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mCQModels).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(mCQModels);
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
