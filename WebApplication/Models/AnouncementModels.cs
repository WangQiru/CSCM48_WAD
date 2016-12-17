using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication.Models
{
    public class AnouncementModels
    {
        public int ID { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Text { get; set; }
        public DateTime publishTime { get; set; }
        public int LectureNo { get; set; }
    }
}