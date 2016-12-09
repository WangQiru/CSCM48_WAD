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
            ViewBag.Option = new SelectList(db.Options, "ID", "Text");
            ViewBag.QuestionID = new SelectList(db.Options, "ID", "Text");
            return View();
        }

        // POST: Response/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,MCQID,Option,QuestionID,UserId")] ResponseModels responseModels)
        {
            if (ModelState.IsValid)
            {
                db.Responses.Add(responseModels);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", responseModels.UserId);
            ViewBag.MCQID = new SelectList(db.MCQs, "ID", "Title", responseModels.MCQID);
            ViewBag.Option = new SelectList(db.Options, "ID", "Text", responseModels.Option);
            ViewBag.QuestionID = new SelectList(db.Options, "ID", "Text", responseModels.QuestionID);
            return View(responseModels);
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
