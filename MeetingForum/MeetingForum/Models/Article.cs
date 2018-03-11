using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MeetingForum.Models
{
    public class Article
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }
        public byte[] Image { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Text { get; set; }
        [Required]
        public string Accepted { get; set; }

        private DateTime? datePublish = null;
        [Required]
        //[DefaultValue(typeof(DateTime), DateTime.Now.ToString("yyyy-MM-dd"))]
        public DateTime DatePublish
        {
            get
            {
                return this.datePublish.HasValue
                    ? this.datePublish.Value
                    : DateTime.Now;
            }
            set
            {
                this.datePublish = value;
            }
        }
    }
}