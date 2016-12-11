using System.Collections.Generic;
using WebApplication.Models;

namespace WebApplication.ViewModels
{
    public class QuestionOptionViewModel
    {
        public MCQModels MCQModels { get; set; }
        public List<QuestionModels> QuestionModelsList { get; set; }
        public List<OptionModels> OptionsModelsList { get; set; }
    }
}