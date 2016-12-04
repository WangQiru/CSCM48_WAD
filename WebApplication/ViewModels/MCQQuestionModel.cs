using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.ViewModels
{
    public class MCQQuestionModel
    {
        public int MCQID { get; set; }
        public string MCQTitle { get; set; }
        public string MCQDesc { get; set; }
        public int QuestionCount { get; set; }
        public DateTime ReleaseDate { get; set; }
        public DateTime DueDate { get; set; }
    }
}