using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MeetingForum.Models
{
    public class Event
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Message { get; set; }
        public DateTime Date { get; set; }
    }
}