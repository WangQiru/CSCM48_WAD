using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication.Models
{
    public class AnnouncementModels
    {
        public int ID { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Text { get; set; }
        public DateTime PublishTime { get; set; }
        public int LecturerNo { get; set; }
        public string LecturerName { get; set; }
    }
}