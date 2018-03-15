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

        public async Task<ActionResult> Index()
        {
            IEnumerable<Article> articles = await db.Articles.ToListAsync();
            ViewBag.Articles = articles.Reverse();
            IEnumerable<Event> events = await db.Events.ToListAsync();
            ViewBag.Events = events.Reverse();

            return View();
        }

        [HttpGet]
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Article article = await db.Articles.FindAsync(id);
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

        public async Task<ActionResult> EditArticle(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Article article = await db.Articles.FindAsync(id);
            if (article != null)
            {
                return View(article);
            }
            return HttpNotFound();
        }
        [HttpPost]
        public async Task<ActionResult> EditArticle(Article article, HttpPostedFileBase uploadImage)
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
                await db.SaveChangesAsync();
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

        public async Task<ActionResult> DeleteArticle(int id)
        {
            Article article = await db.Articles.FindAsync(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }
        [HttpPost, ActionName("DeleteArticle")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Article article = await db.Articles.FindAsync(id);
            if(article == null)
            {
                return HttpNotFound();
            }
            db.Articles.Remove(article);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
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