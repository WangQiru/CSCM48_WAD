using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class QuestionController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Question
        [Schaeflein.Community.MVC5AuthZPolicy.Authorize(Policy = "Lecturer")]
        public ActionResult Index()
        {
            var questions = db.Questions.Include(q => q.MCQModels);
            return View(questions.ToList());
        }


        [ValidateAntiForgeryToken]
        [Schaeflein.Community.MVC5AuthZPolicy.Authorize(Policy = "Lecturer")]
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


        [ValidateAntiForgeryToken]
        [Schaeflein.Community.MVC5AuthZPolicy.Authorize(Policy = "Lecturer")]
        public JsonResult AjaxUpdate(string text, string id)
        {
            QuestionModels questionModels = db.Questions.Find(int.Parse(id));
            questionModels.Text = text;
            db.Entry(questionModels).State = EntityState.Modified;
            db.SaveChanges();

            return Json("Response from update");
        }

        [ValidateAntiForgeryToken]
        [Schaeflein.Community.MVC5AuthZPolicy.Authorize(Policy = "Lecturer")]
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
