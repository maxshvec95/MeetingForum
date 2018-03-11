using MeetingForum.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MeetingForum.Controllers
{
    public class HomeController : Controller
    {
        //Создаем контекст данных
        ApplicationDbContext db = new ApplicationDbContext();
        public bool IsAdmin { get; set; }

        public ActionResult Index()
        {
            return View(db.Articles);
        }

        //public async Task<ActionResult> ForumList()       //Асинхронный метод Index
        //{
        //    IEnumerable<Article> articles = await db.Articles.ToListAsync();
        //    ViewBag.Articles = articles;
        //    return View("Index");
        //}

        [HttpGet]
        public async Task<ActionResult> Details(int id)
        {
            Article article = await db.Articles.FindAsync(id);
            //if (article.Id != id) { return RedirectToAction("Index"); }
            return View(article);
        }

        //[Authorize]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Article article, HttpPostedFileBase uploadImage)
        {
            if (ModelState.IsValid && uploadImage != null)
            {
                byte[] imageData = null;
                // считываем переданный файл в массив байтов
                using (var binaryReader = new BinaryReader(uploadImage.InputStream))
                {
                    imageData = binaryReader.ReadBytes(uploadImage.ContentLength);
                }
                // установка массива байтов
                article.Image = imageData;
                db.Articles.Add(article);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(article);
        }
    }
}