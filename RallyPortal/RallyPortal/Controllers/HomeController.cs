using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RallyPortal.Models;

namespace RallyPortal.Controllers
{
    public class HomeController : BaseController
    {
        private const int MAX_ARTICLE = 4;

        public ActionResult Index(int page = 0)
        {
            int skip = page * MAX_ARTICLE + 4;
            ViewBag.Page = page + 1;
            double allCount = (db.ArticleSet.Where(e => e.Published).Count() - 4 ) / (double)MAX_ARTICLE;
            ViewBag.AllCount = Math.Ceiling(allCount);
            ViewBag.FirstFour = db.ArticleSet.OrderByDescending(e => e.LastModifiedDate).Where(e => e.Published).Take(4);
            return View(db.ArticleSet.OrderByDescending(e => e.LastModifiedDate).Where(e => e.Published).Skip(skip).Take(MAX_ARTICLE));
        }

        public ActionResult News(int page = 0)
        {
            int skip = page * MAX_ARTICLE + 4;
            ViewBag.Page = page + 1;
            double allCount = (db.ArticleSet.Where(e => !(e is Highlights)).Where(e => e.Published).Count() - 4) / (double)MAX_ARTICLE;
            ViewBag.AllCount = Math.Ceiling(allCount);
            ViewBag.FirstFour = db.ArticleSet.Where(e => !(e is Highlights)).OrderByDescending(e => e.LastModifiedDate).Where(e => e.Published).Take(4);
            return View(db.ArticleSet.Where(e => !(e is Highlights)).OrderByDescending(e => e.LastModifiedDate).Where(e => e.Published).Skip(skip).Take(MAX_ARTICLE));
        }

        public ActionResult Highlights(int page = 0)
        {
            int skip = page * MAX_ARTICLE + 4;
            ViewBag.Page = page + 1;
            double allCount = (db.ArticleSet.Where(e => (e is Highlights)).Where(e => e.Published).Count() - 4) / (double)MAX_ARTICLE;
            ViewBag.AllCount = Math.Ceiling(allCount);
            ViewBag.FirstFour = db.ArticleSet.Where(e => (e is Highlights)).OrderByDescending(e => e.LastModifiedDate).Where(e => e.Published).Take(4);
            return View(db.ArticleSet.Where(e => (e is Highlights)).OrderByDescending(e => e.LastModifiedDate).Where(e => e.Published).Skip(skip).Take(MAX_ARTICLE));
        }

        public ActionResult Galleries()
        {
            return View(db.GalleryCategorySet.Where(e => e.Parent == null));
        }

        public ActionResult Teams()
        {
            return View(db.TeamSet.Select(e => new TeamData { CoDriver = e.CoDriverName, Driver = e.DriverName, TeamImageUrl = e.TeamImageUrl, TeamName = e.TeamName, Id = e.Id}));
        }

        [HttpPost]
        public ActionResult Search(string filter)
        {
            ViewBag.Filter = filter;
            ViewBag.FirstFour = db.ArticleSet.Where(e => (e is Highlights)).OrderByDescending(e => e.LastModifiedDate).Where(e => e.Published).Take(4);
            return View(db.ArticleSet.Where(e => e.Title.Contains(filter)).Union(db.ArticleSet.Where(e => e.Content.Contains(filter))));
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
