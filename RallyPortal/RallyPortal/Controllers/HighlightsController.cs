using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RallyPortal;

namespace RallyPortal.Controllers
{
    [Authorize(Roles = "Administrator, SuperAdministrator")]
    public class HighlightsController : BaseController
    {
        //
        // GET: /Highlights/

        public ViewResult Index()
        {
            return View(db.ArticleSet.Where(e => e is Highlights).ToList());
        }

        //
        // GET: /Highlights/Details/5

        public ViewResult Details(int id)
        {
            Article article = db.ArticleSet.Find(id);
            if (!(article is Highlights))
                return null;

            return View(article);
        }

        //
        // GET: /Highlights/Create

        public ActionResult Create()
        {
            (new UploadController()).DeleteFile(Server.MapPath("~/Images/temp"));
            return View();
        }

        //
        // POST: /Highlights/Create

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Highlights article)
        {
            if (ModelState.IsValid)
            {
                int id;
                try
                {
                    id = db.ArticleSet.Select(e => e.Id).OrderByDescending(e => e).First() + 1;
                }
                catch (Exception)
                {
                    id = 0;
                }

                var sourcePath = Path.Combine(Server.MapPath("~/Images"), "temp");
                var destPath = Path.Combine(Server.MapPath("~/Images/FeaturedImages"), id.ToString());

                var success = (new UploadController()).SaveFeaturedImage(sourcePath, destPath);
                if (!success)
                {
                    sourcePath = Path.Combine(Server.MapPath("~/Images"), "no_image");
                    System.IO.File.Copy(sourcePath, destPath);
                }
                article.ImageUrl = "~/Images/FeaturedImages/" + id.ToString();
                article.LastModifiedDate = DateTime.Now;
                article.PublishedDate = DateTime.Now;
                article.Published = false;
                db.ArticleSet.Add(article);
                db.SaveChanges();

                SendMessage(MessageType.Success, "The highlights successfully created!");
                return RedirectToAction("Index");
            }

            SendMessage(MessageType.Error, "Something went wrong! :( Try it again");  

            return View(article);
        }

        //
        // GET: /Highlights/Edit/5

        public ActionResult Edit(int id)
        {
            Article article = db.ArticleSet.Find(id);
            if (!(article is Highlights))
                return null;


            (new UploadController()).DeleteFile(Server.MapPath("~/Images/temp"));
            return View(article);
        }

        //
        // POST: /Highlights/Edit/5

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Highlights article)
        {
            if (ModelState.IsValid)
            {
                var sourcePath = Path.Combine(Server.MapPath("~/Images"), "temp");
                var destPath = Path.Combine(Server.MapPath("~/Images/FeaturedImages"), article.Id.ToString());

                if (System.IO.File.Exists(sourcePath) && System.IO.File.Exists(destPath))
                {
                    System.IO.File.Delete(destPath);
                }
                (new UploadController()).SaveFeaturedImage(sourcePath, destPath);

                article.LastModifiedDate = DateTime.Now;
                db.Entry(article).State = EntityState.Modified;
                db.SaveChanges();
                SendMessage(MessageType.Success, "The highlights successfully modified!");
                return RedirectToAction("Index");
            }
            SendMessage(MessageType.Error, "Something went wrong! :( Try it again");           
            return View(article);
        }

        //
        // GET: /Highlights/Delete/5

        public ActionResult Delete(int id)
        {
            Article article = db.ArticleSet.Find(id);
            if (!(article is Highlights))
                return null;

            SendMessage(MessageType.Warning, "If you delete the highlights, you cannot recover it later!");
            return View(article);
        }

        //
        // POST: /Highlights/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Article article = db.ArticleSet.Find(id);
            foreach (var comment in article.Comment)
            {
                db.CommentSet.Remove(comment);
            }
            db.ArticleSet.Remove(article);
            db.SaveChanges();

            // ImageUrl contains the full path
            (new UploadController()).DeleteFile(Server.MapPath("~/Images/FeaturedImages/" + id.ToString()));

            SendMessage(MessageType.Success, "The highlights successfully deleted!");
            return RedirectToAction("Index");
        }

        public ActionResult Publish(int id)
        {
            Article entry = db.ArticleSet.Single(e => e.Id == id);
            entry.Published = true;

            db.Entry(entry).State = EntityState.Modified;
            db.SaveChanges();

            SendMessage(MessageType.Success, "The highlights successfully published!");
            return RedirectToAction("Index");
        }

        public ActionResult Unpublish(int id)
        {
            Article entry = db.ArticleSet.Single(e => e.Id == id);
            entry.Published = false;

            db.Entry(entry).State = EntityState.Modified;
            db.SaveChanges();

            SendMessage(MessageType.Success, "The highlights successfully unpublished!");
            return RedirectToAction("Index");
        }
    }
}