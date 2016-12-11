using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication.Models
{
    public class ResponseModels
    {
        public int ID { get; set; }
        [ForeignKey("MCQModels")]
        public virtual int MCQID { get; set; }
        public virtual MCQModels MCQModels { get; set; }
        [ForeignKey("OptionModels")]
        public virtual int OptionID { get; set; }
        public virtual OptionModels OptionModels { get; set; }
        [ForeignKey("QuestionModels")]
        public virtual int QuestionID { get; set; }
        public virtual QuestionModels QuestionModels { get; set; }
        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}