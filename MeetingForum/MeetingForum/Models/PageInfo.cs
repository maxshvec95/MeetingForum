using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MeetingForum.Models
{
    public class PageInfo
    {
        public int PageNumber { get; set; } //Номер текущей страницы
        public int PageSize { get; set; }   //кол-во обьектов на странице
        public int TotalItems { get; set; } //всего обьектов
        public int TotalPages                //всего страниц
        {
            get { return (int)Math.Ceiling((decimal)TotalItems / PageSize); }
        }

    }
}