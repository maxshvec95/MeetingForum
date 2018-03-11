using MeetingForum.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MeetingForum.Controllers
{
    public class HomeController : Controller
    {
        //Создаем контекст данных
        ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            //получаем из бд все обьекты Article
            IEnumerable<Article> articles = db.Articles;
            //передаем все обьекты в динамическое свойство Articles в ViewBag
            ViewBag.Articles = articles;
            //возвращаем представление
            return View();
        }

        [HttpGet]
        public ActionResult Show(int id)
        {
            ViewBag.Articles = db.Articles.Find(id);
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public string Create(Article article)
        {
            article.DatePublish = DateTime.Now;
            //добавляем статью в базу данных
            db.Articles.Add(article);
            //сохраняем в бд все изменения
            db.SaveChanges();
            return "Статья добавлена";
        }
    }
}