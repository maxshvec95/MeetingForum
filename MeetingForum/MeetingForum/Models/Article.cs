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
        [Display(Name = "Изображение")]
        public byte[] Image { get; set; }
        [Required(ErrorMessage = "Введите заголовок")]
        [Display(Name = "Заголовок")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Введите текст статьи")]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Текст")]
        public string Text { get; set; }
        [Required(ErrorMessage = "Введите принятое решение")]
        [Display(Name = "Принятое решение")]
        public string Accepted { get; set; }

        private DateTime? datePublish = null;
        [Required]
        [Display(Name = "Дата публикации")]
        [DataType(DataType.Date)]
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