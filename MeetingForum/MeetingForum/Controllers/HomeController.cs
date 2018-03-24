using MeetingForum.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MeetingForum.Controllers
{
    public class HomeController : Controller
    {
        //Создаем контекст данных
        ApplicationDbContext db = new ApplicationDbContext();

        //[OutputCache (Duration =360)]
        public async Task<ActionResult> Index(int page=1)
        {
            int pageSize = 3;   //кол-во обьектов на странице
            IEnumerable<Event> events = await (db.Events.ToListAsync());

            IEnumerable<Article> articles = db.Articles.ToList();
            IEnumerable<Article> articlesRev = articles.Reverse();
            IEnumerable<Article> articlesPerPages = articlesRev.Skip((page - 1) * pageSize).Take(pageSize);
            PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = articlesRev.Count() };
            HomeViewModel hvm = new HomeViewModel { PageInfo = pageInfo, Articles = articlesPerPages, Events = events };
            
            return View(hvm);
        }

        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Article article = db.Articles.Find(id);
            if (article != null)
            {
                return View(article);
            }
            return HttpNotFound();
        }

        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Article article, HttpPostedFileBase uploadImage)
        {
            //if (ModelState.IsValid && uploadImage != null)
            if (ModelState.IsValid)
            {
                byte[] imageData = null;
                // считываем переданный файл в массив байтов
                if (uploadImage != null)
                {
                    using (var binaryReader = new BinaryReader(uploadImage.InputStream))
                    {
                        imageData = binaryReader.ReadBytes(uploadImage.ContentLength);
                    }
                }
                // установка массива байтов
                article.Image = imageData;
                db.Articles.Add(article);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(article);
        }

        [HttpGet]
        public ActionResult EditArticle(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Article article = db.Articles.Find(id);
            if (article != null)
            {
                return View(article);
            }
            return HttpNotFound();
        }
        [HttpPost]
        public ActionResult EditArticle(Article article, HttpPostedFileBase uploadImage)
        {
            if (ModelState.IsValid)
            {
                // считываем переданный файл в массив байтов
                if (uploadImage != null)
                {
                    byte[] imageData = null;
                    using (var binaryReader = new BinaryReader(uploadImage.InputStream))
                    {
                        imageData = binaryReader.ReadBytes(uploadImage.ContentLength);
                    }
                    // установка массива байтов
                    article.Image = imageData;
                }
                db.Entry(article).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(article);

            //if (ModelState.IsValid)
            //{
            //    if(uploadImage != null)
            //    {
            //        using (var binaryReader = new BinaryReader(uploadImage.InputStream))
            //        {
            //            imageData = binaryReader.ReadBytes(uploadImage.ContentLength);
            //        }
            //    }
            //    // установка массива байтов
            //    article.Image = imageData;
            //}
            //if (article != null)
            //{
            //    db.Entry(article).State = EntityState.Modified;
            //    await db.SaveChangesAsync();
            //    return RedirectToAction("Index");
            //}
            //return HttpNotFound();
        }

        public ActionResult DeleteArticle(int id)
        {
            Article article = db.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }
        [HttpPost, ActionName("DeleteArticle")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Article article = db.Articles.Find(id);
            if(article == null)
            {
                return HttpNotFound();
            }
            db.Articles.Remove(article);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult JsonSearch (string name)
        {
            var jsondata = db.Articles.Where(a => a.Text.Contains(name)).ToList();
            return PartialView(jsondata);
        }


        
        //public ActionResult ViewUser()
        //{
        //    IList<string> roles = new List<string> { "Роль не определена" };
        //    ApplicationUserManager userManager = HttpContext.GetOwinContext()
        //                                            .GetUserManager<ApplicationUserManager>();
        //    ApplicationUser user = userManager.FindByEmail(User.Identity.Name);
        //    if (user != null)
        //        roles = userManager.GetRoles(user.Id);
        //    return View(roles);
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}