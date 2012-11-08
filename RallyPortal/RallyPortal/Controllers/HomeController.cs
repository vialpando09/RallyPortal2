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
        public ActionResult Index(int page = 0)
        {
            int skip = page * 6 + 4;
            ViewBag.Page = page + 1;
            ViewBag.AllCount = (db.ArticleSet.OrderByDescending(e => e.LastModifiedDate).Where(e => e.Published).Count() - 4 ) / 6;
            ViewBag.FirstFour = db.ArticleSet.OrderByDescending(e => e.LastModifiedDate).Where(e => e.Published).Take(4);
            return View(db.ArticleSet.OrderByDescending(e => e.LastModifiedDate).Where(e => e.Published).Skip(page).Take(6));
        }

        public ActionResult News(int page = 0)
        {
            int skip = page * 6 + 4;
            ViewBag.Page = page + 1;
            ViewBag.AllCount = (db.ArticleSet.Where(e => !(e is Highlights)).OrderByDescending(e => e.LastModifiedDate).Where(e => e.Published).Count() - 4) / 6;
            ViewBag.FirstFour = db.ArticleSet.Where(e => !(e is Highlights)).OrderByDescending(e => e.LastModifiedDate).Where(e => e.Published).Take(4);
            return View(db.ArticleSet.Where(e => !(e is Highlights)).OrderByDescending(e => e.LastModifiedDate).Where(e => e.Published).Skip(page).Take(6));
        }

        public ActionResult Highlights(int page = 0)
        {
            int skip = page * 6 + 4;
            ViewBag.Page = page + 1;
            ViewBag.AllCount = (db.ArticleSet.Where(e => !(e is Highlights)).OrderByDescending(e => e.LastModifiedDate).Where(e => e.Published).Count() - 4) / 6;
            ViewBag.FirstFour = db.ArticleSet.Where(e => (e is Highlights)).OrderByDescending(e => e.LastModifiedDate).Where(e => e.Published).Take(4);
            return View(db.ArticleSet.Where(e => (e is Highlights)).OrderByDescending(e => e.LastModifiedDate).Where(e => e.Published).Skip(page).Take(6));
        }

        public ActionResult Galleries()
        {
            return View(db.GalleryCategorySet.Where(e => e.Parent == null));
        }

        public ActionResult Teams()
        {
            return View(db.TeamSet.Select(e => new TeamData { CoDriver = e.CoDriverName, Driver = e.DriverName, TeamImageUrl = e.TeamImageUrl, TeamName = e.TeamName}));
        }

        [HttpPost]
        public ActionResult Search(string filter)
        {
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
