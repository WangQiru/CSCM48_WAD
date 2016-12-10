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
    public class ResponseController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Response
        public ActionResult Index()
        {
            var responses = db.Responses.Include(r => r.ApplicationUser).Include(r => r.MCQModels).Include(r => r.OptionModels).Include(r => r.QuestionModels);
            return View(responses.ToList());
        }

        // GET: Response/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ResponseModels responseModels = db.Responses.Find(id);
            if (responseModels == null)
            {
                return HttpNotFound();
            }
            return View(responseModels);
        }

        // GET: Response/Create
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name");
            ViewBag.MCQID = new SelectList(db.MCQs, "ID", "Title");
            ViewBag.OptionID = new SelectList(db.Options, "ID", "Text");
            ViewBag.QuestionID = new SelectList(db.Questions, "ID", "Text");
            return View();
        }

        // POST: Response/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,MCQID,OptionID,QuestionID,UserId")] ResponseModels responseModels)
        {
            if (ModelState.IsValid)
            {
                db.Responses.Add(responseModels);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", responseModels.UserId);
            ViewBag.MCQID = new SelectList(db.MCQs, "ID", "Title", responseModels.MCQID);
            ViewBag.OptionID = new SelectList(db.Options, "ID", "Text", responseModels.OptionID);
            ViewBag.QuestionID = new SelectList(db.Questions, "ID", "Text", responseModels.QuestionID);
            return View(responseModels);
        }

        [ValidateAntiForgeryToken]
        public JsonResult AjaxCreate(List<Answer> answerList)
        {
            System.Diagnostics.Debug.WriteLine(answerList);
            System.Diagnostics.Debug.WriteLine(answerList[0].MCQID);
            foreach (Answer ans in answerList)
            {
                ResponseModels res = new ResponseModels();
                res.MCQID = ans.MCQID;
                res.OptionID = ans.OptionID;
                res.QuestionID = ans.QuestionID;
                res.UserId = User.Identity.GetUserId();
                db.Responses.Add(res);
                db.SaveChanges();
            }

            return Json("Response from create");
        }

        public class Answer
        {
            public int MCQID { get; set; }
            public int OptionID { get; set; }
            public int QuestionID { get; set; }
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
