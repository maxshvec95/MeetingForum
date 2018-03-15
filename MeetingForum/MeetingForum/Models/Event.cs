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
        [Required(ErrorMessage ="Введите имя заголовка")]
        [Display(Name ="Заголовок")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Введите текст события")]
        [Display(Name = "Текст")]
        public string Message { get; set; }
        [Required(ErrorMessage = "Выберите дату")]
        [Display(Name = "Дата проведения")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
    }
}