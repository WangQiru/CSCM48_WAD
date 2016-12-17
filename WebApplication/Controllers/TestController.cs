using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using WebApplication.Models;
using WebApplication.ViewModels;

namespace WebApplication.Controllers
{
    public class TestController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Test
        [Schaeflein.Community.MVC5AuthZPolicy.Authorize(Policy = "studentEnrolled")]
        public ActionResult Index()
        {
            string userId = User.Identity.GetUserId();
            List<MCQModels> mCQModels = db.MCQs.ToList();
            List<MCQQuestionModel> mCQQuestionModels = new List<MCQQuestionModel>();
            foreach (var mcq in mCQModels)
            {
                if (db.Questions.Count(q => q.MCQID == mcq.ID) > 0)
                {
                    MCQQuestionModel obj = new MCQQuestionModel();

                    int questionID = db.Questions.First(q => q.MCQID == mcq.ID).ID;
                    if (db.Responses.Any(u => u.UserId == userId && u.QuestionID == questionID))
                        obj.completed = true;
                    obj.MCQID = mcq.ID;
                    obj.MCQTitle = mcq.Title;
                    obj.MCQDesc = mcq.Description;
                    obj.ReleaseDate = mcq.ReleaseDate;
                    obj.DueDate = mcq.DueDate;
                    obj.QuestionCount = db.Questions.Count(q => q.MCQID == mcq.ID);
                    mCQQuestionModels.Add(obj);
                }

            }

            return View(mCQQuestionModels);
        }

        // GET: Test/Details/5
        [Schaeflein.Community.MVC5AuthZPolicy.Authorize(Policy = "studentEnrolled")]
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

        [Schaeflein.Community.MVC5AuthZPolicy.Authorize(Policy = "studentEnrolled")]
        public ActionResult Result(int? id)
        {
            string userId = User.Identity.GetUserId();
            List<ResponseModels> responsesModels = new List<ResponseModels>();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
                responsesModels = db.Responses.Where(r => r.UserId == userId && r.MCQID == id).OrderBy(r => r.QuestionID).ToList();
            if (responsesModels == null)
            {
                return HttpNotFound();
            }
            return View(responsesModels);
        }


        // GET: Test/Start/5
        [Schaeflein.Community.MVC5AuthZPolicy.Authorize(Policy = "studentEnrolled")]
        public ActionResult Start(int? id)
        {
            string userId = User.Identity.GetUserId();


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
            if (db.Responses.Any(u => u.UserId == userId && u.MCQID == qQuestionOptionViewModel.MCQModels.ID))
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
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


        public JsonResult AjaxRetrieve()
        {
            string userId = User.Identity.GetUserId();
            int testCount = 0;
            List<MCQModels> mCQModels = db.MCQs.Where(o => o.ReleaseDate <= DateTime.Now && o.DueDate >= DateTime.Now).ToList();
            foreach (var mcq in mCQModels)
            {
                if (db.Questions.Count(q => q.MCQID == mcq.ID) > 0 && db.Responses.Count(r => r.UserId == userId && r.MCQID == mcq.ID) == 0)
                    testCount++;
            }

            return Json(testCount, JsonRequestBehavior.AllowGet);
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
