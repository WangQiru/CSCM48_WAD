using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication.Models
{
    public class QuestionModels
    {
        public int ID { get; set; }
        [ForeignKey("MCQModels")]
        public virtual int MCQID { get; set; }
        public virtual MCQModels MCQModels { get; set; }
        [Required]
        public string Text { get; set; }
        public List<OptionModels> OptionsModelsList { get; set; }
    }


}