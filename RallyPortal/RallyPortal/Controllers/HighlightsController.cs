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
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Comment(int id, string message)
        {
            if (User.Identity.IsAuthenticated)
            {
                Article article = db.ArticleSet.Find(id);
                article.Comment.Add(new Comment { AuthorEmail = User.Identity.Name, AuthorName = User.Identity.Name, PostDate = DateTime.Now, Content = message });
                db.SaveChanges();

                SendMessage(MessageType.Success, "Your comment has been posted.");
            }
            else
            {
                SendMessage(MessageType.Error, "You cannot send comment.Please log in first!");
            }
            return RedirectToAction("Details", new { id = id });
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult DeleteComment(int articleId, int commentId)
        {
            if (User.Identity.IsAuthenticated)
            {
                Comment comment = db.CommentSet.Find(commentId); 
                if (comment.AuthorName == User.Identity.Name || User.IsInRole("Administrator") || User.IsInRole("SuperAdministrator"))
                {
                    db.CommentSet.Remove(comment);
                    db.SaveChanges();

                    SendMessage(MessageType.Success, "Your comment has been deleted.");
                }
                else
                {
                    SendMessage(MessageType.Error, "You cannot delete this comment!");
                }
            }
            return RedirectToAction("Details", new { id = articleId });
        }

        //
        // GET: /Highlights/

        public ViewResult Index()
        {
            return View(db.ArticleSet.Where(e => e is Highlights).ToList());
        }

        //
        // GET: /Highlights/Details/5

        [AllowAnonymous()]
        public ActionResult Details(int id)
        {
            Article article = db.ArticleSet.Find(id);
            if (!(article is Highlights))
                return new HttpStatusCodeResult(404);

            if (!article.Published && !User.IsInRole("Administrator") && !User.IsInRole("SuperAdministrator"))
            {
                return new HttpStatusCodeResult(404);
            }

            return View(article);
        }

        //
        // GET: /Highlights/Create

        public ActionResult Create()
        {
            DeleteTempFiles();
            return View();
        }

        private void DeleteTempFiles()
        {
            var physicalPath = Server.MapPath("~/Images/temp");
            if (System.IO.File.Exists(physicalPath))
            {
                System.IO.File.Delete(physicalPath);
            }
        }

        //
        // POST: /Highlights/Create

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Highlights article)
        {
            if (ModelState.IsValid)
            {
                int id = GetNextId();

                var sourcePath = Path.Combine(Server.MapPath("~/Images"), "temp");
                var destPath = Path.Combine(Server.MapPath("~/Images/FeaturedImages"), id.ToString());

                var success = MoveTempFeaturedImage(sourcePath, destPath);
                CreateThumbnail(sourcePath, destPath, success);
                
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

        private void CreateThumbnail(string sourcePath, string destPath, bool success)
        {
            if (!success)
            {
                sourcePath = Path.Combine(Server.MapPath("~/Images"), "no_image");
                System.IO.File.Copy(sourcePath, destPath);
            }
        }

        private static bool MoveTempFeaturedImage(string sourcePath, string destPath)
        {
            if (System.IO.File.Exists(sourcePath))
            {
                System.IO.File.Move(sourcePath, destPath);
                return true;
            }
            return false;
        }


        private int GetNextId()
        {
            return db.ArticleSet.Select(e => e.Id).OrderByDescending(e => e).FirstOrDefault() + 1;            
        }

        //
        // GET: /Highlights/Edit/5

        public ActionResult Edit(int id)
        {
            Article article = db.ArticleSet.Find(id);
            if (!(article is Highlights))
                return new HttpStatusCodeResult(404);

            DeleteTempFiles();
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

                DeleteCurrentFeaturedImage(sourcePath, destPath);

                var success = MoveTempFeaturedImage(sourcePath, destPath);
                CreateThumbnail(sourcePath, destPath, success);

                article.LastModifiedDate = DateTime.Now;
                db.Entry(article).State = EntityState.Modified;
                db.SaveChanges();
                SendMessage(MessageType.Success, "The highlights successfully modified!");
                return RedirectToAction("Index");
            }
            SendMessage(MessageType.Error, "Something went wrong! :( Try it again");           
            return View(article);
        }

        private static void DeleteCurrentFeaturedImage(string sourcePath, string destPath)
        {

            if (System.IO.File.Exists(sourcePath) && System.IO.File.Exists(destPath))
            {
                System.IO.File.Delete(destPath);
            }
        }

        //
        // GET: /Highlights/Delete/5

        public ActionResult Delete(int id)
        {
            Article article = db.ArticleSet.Find(id);
            if (!(article is Highlights))
                return new HttpStatusCodeResult(404);

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
            var physicalPath = Server.MapPath("~/Images/FeaturedImages/" + id.ToString());
            if (System.IO.File.Exists(physicalPath))
            {
                System.IO.File.Delete(physicalPath);
            }

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