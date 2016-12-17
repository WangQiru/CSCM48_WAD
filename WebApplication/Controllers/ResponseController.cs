using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
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

        //Using ajax to accept student submissions
        [ValidateAntiForgeryToken]
        public JsonResult AjaxCreate(List<Answer> answerList)
        {
            string userId = User.Identity.GetUserId();
            foreach (Answer ans in answerList)
            {
                ResponseModels res = new ResponseModels();
                res.MCQID = ans.MCQID;
                res.OptionID = ans.OptionID;
                res.QuestionID = ans.QuestionID;
                res.UserId = userId;
                res.correct = false;
                if (!db.Responses.Any(u => u.UserId == userId && u.QuestionID == ans.QuestionID))
                {
                    if (db.Options.Where(o => o.ID == ans.OptionID).First().correct)
                        res.correct = true;
                    db.Responses.Add(res);
                    db.SaveChanges();
                }
                else
                {

                }
            }

            return Json("Response from create");
        }

        //An object for temporarily holding data from AjaxCreate() 
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
