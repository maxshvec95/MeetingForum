using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MeetingForum.Models
{
    public class HomeViewModel
    {
        public IEnumerable<Article> Articles { get; set; }
        public IEnumerable<Event> Events { get; set; }
        public PageInfo PageInfo { get; set; }
    }
}