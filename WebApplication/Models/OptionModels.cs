using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication.Models
{
    public class OptionModels
    {
        public int ID { get; set; }
        [ForeignKey("QuestionModels")]
        public virtual int QuestionID { get; set; }
        public virtual QuestionModels QuestionModels { get; set; }
        [Required]
        public string Text { get; set; }
        public bool correct { get; set; }
    }


}