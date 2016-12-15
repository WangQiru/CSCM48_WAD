using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using WebApplication.Models;
using WebApplication.ViewModels;

namespace WebApplication.Controllers
{
    public class MCQController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: MCQ
        [Schaeflein.Community.MVC5AuthZPolicy.Authorize(Policy = "Lecturer")]
        public ActionResult Index()
        {
            return View(db.MCQs.ToList());
        }

        [Schaeflein.Community.MVC5AuthZPolicy.Authorize(Policy = "Lecturer")]
        public ActionResult Stats(int? id)
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

        public JsonResult AjaxRetrieve(int id)
        {
            List<QuestionModels> questionModelsList = db.Questions.Where(r => r.MCQID == id).ToList();
            List<Result> results = new List<Result>();

            int questionNO = 1;
            foreach (var question in questionModelsList)
            {                
                int correct = 0;
                int incorrect = 0;
                foreach (var response in db.Responses.Where(r => r.QuestionID == question.ID).ToList())
                {
                    if (response.correct)
                        correct++;
                    else
                        incorrect++;
                }

                var result = new Result();
                result.questionNo = questionNO;
                result.correct = correct;
                result.incorrect = incorrect;
                results.Add(result);

                questionNO++;
            }

            return Json(results, JsonRequestBehavior.AllowGet);
        }

        public class Result
        {
            public int questionNo { get; set; }
            public int correct { get; set; }
            public int incorrect { get; set; }
        }

        // GET: MCQ/Create
        [Schaeflein.Community.MVC5AuthZPolicy.Authorize(Policy = "Lecturer")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: MCQ/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Schaeflein.Community.MVC5AuthZPolicy.Authorize(Policy = "Lecturer")]
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
        [Schaeflein.Community.MVC5AuthZPolicy.Authorize(Policy = "Lecturer")]
        public ActionResult Edit(int? id)
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

        // POST: MCQ/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Schaeflein.Community.MVC5AuthZPolicy.Authorize(Policy = "Lecturer")]
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
        [Schaeflein.Community.MVC5AuthZPolicy.Authorize(Policy = "Lecturer")]
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
        [Schaeflein.Community.MVC5AuthZPolicy.Authorize(Policy = "Lecturer")]
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
