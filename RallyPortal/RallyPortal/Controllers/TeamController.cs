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
    public class TeamController : BaseController
    {
        private void DeleteTempFiles()
        {
            string path = Server.MapPath("~/Images/");
            string pathTeam = Path.Combine(path, "tempTeam");
            string pathDriver = Path.Combine(path, "tempDriver");
            string pathCoDriver = Path.Combine(path, "tempCoDriver");
            if (System.IO.File.Exists(pathTeam))
                System.IO.File.Delete(pathTeam);
            if (System.IO.File.Exists(pathDriver))
                System.IO.File.Delete(pathDriver);
            if (System.IO.File.Exists(pathCoDriver))
                System.IO.File.Delete(pathCoDriver);
        }

        //
        // GET: /Team/

        public ViewResult Index()
        {
            return View(db.TeamSet.ToList());
        }

        //
        // GET: /Team/Details/5

        [AllowAnonymous()]
        public ViewResult Details(int id)
        {
            Team team = db.TeamSet.Find(id);
            return View(team);
        }

        //
        // GET: /Team/Create

        public ActionResult Create()
        {
            DeleteTempFiles();
            return View();
        }

        //
        // POST: /Team/Create

        [HttpPost]
        public ActionResult Create(Team team)
        {
            if (ModelState.IsValid)
            {
                int id = db.TeamSet.OrderByDescending(e => e.Id).Select(e => e.Id).FirstOrDefault() + 1;

                string folder = Path.Combine(Server.MapPath("~/Images/Teams"), id.ToString());
                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);

                //TeamImage
                ManageTeamImage(folder);

                //DriverImage
                ManageDriverImage(folder);

                //CoDriverImage
                ManageCoDriverImage(folder);

                team.TeamImageUrl = "~/Images/Teams/" + id.ToString() + "/team";
                team.DriverImageUrl = "~/Images/Teams/" + id.ToString() + "/driver";
                team.CoDriverImageUrl = "~/Images/Teams/" + id.ToString() + "/coDriver";

                db.TeamSet.Add(team);
                db.SaveChanges();

                DeleteTempFiles();
                SendMessage(MessageType.Success, "The team successfully created!");
                return RedirectToAction("Index");
            }

            SendMessage(MessageType.Error, "Something went wrong! :( Try it again");
            return View(team);
        }

        private void ManageCoDriverImage(string folder)
        {
            string tempCoDriverImage = Path.Combine(Server.MapPath("~/Images"), "tempCoDriver");
            string coDriverImage = Path.Combine(folder, "coDriver");
            if (!System.IO.File.Exists(tempCoDriverImage))
                tempCoDriverImage = Path.Combine(Server.MapPath("~/Images"), "no_image.jpg");
            if (System.IO.File.Exists(coDriverImage))
                System.IO.File.Delete(coDriverImage);
            System.IO.File.Copy(tempCoDriverImage, coDriverImage);
        }

        private void ManageDriverImage(string folder)
        {
            string tempDriverImage = Path.Combine(Server.MapPath("~/Images"), "tempDriver");
            string driverImage = Path.Combine(folder, "driver");
            if (!System.IO.File.Exists(tempDriverImage))
                tempDriverImage = Path.Combine(Server.MapPath("~/Images"), "no_image.jpg");
            if (System.IO.File.Exists(driverImage))
                System.IO.File.Delete(driverImage);
            System.IO.File.Copy(tempDriverImage, driverImage);
        }

        private void ManageTeamImage(string folder)
        {
            string tempTeamImage = Path.Combine(Server.MapPath("~/Images"), "tempTeam");
            string teamImage = Path.Combine(folder, "team");
            if (!System.IO.File.Exists(tempTeamImage))
                tempTeamImage = Path.Combine(Server.MapPath("~/Images"), "no_image.jpg");
            if (System.IO.File.Exists(teamImage))
                System.IO.File.Delete(teamImage);
            System.IO.File.Copy(tempTeamImage, teamImage);
        }

        //
        // GET: /Team/Edit/5

        public ActionResult Edit(int id)
        {
            DeleteTempFiles();
            Team team = db.TeamSet.Find(id);
            return View(team);
        }

        //
        // POST: /Team/Edit/5

        [HttpPost]
        public ActionResult Edit(Team team)
        {
            if (ModelState.IsValid)
            {
                int id = team.Id;

                string folder = Path.Combine(Server.MapPath("~/Images/Teams"), id.ToString());
                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);

                //TeamImage
                OverwriteTeamImage(folder);

                //DriverImage
                OverwriteDriverImage(folder);

                //CoDriverImage
                OverwriteCoDriverImage(folder);

                db.Entry(team).State = EntityState.Modified;
                db.SaveChanges();

                DeleteTempFiles();
                SendMessage(MessageType.Success, "The team successfully modified!");
                return RedirectToAction("Index");
            }
            SendMessage(MessageType.Error, "Something went wrong! :( Try it again");
            return View(team);
        }

        private void OverwriteCoDriverImage(string folder)
        {
            string tempCoDriverImage = Path.Combine(Server.MapPath("~/Images"), "tempCoDriver");
            string coDriverImage = Path.Combine(folder, "coDriver");
            if (System.IO.File.Exists(tempCoDriverImage))
            {
                if (System.IO.File.Exists(coDriverImage))
                    System.IO.File.Delete(coDriverImage);
                System.IO.File.Move(tempCoDriverImage, coDriverImage);
            }
        }

        private void OverwriteDriverImage(string folder)
        {
            string tempDriverImage = Path.Combine(Server.MapPath("~/Images"), "tempDriver");
            string driverImage = Path.Combine(folder, "driver");
            if (System.IO.File.Exists(tempDriverImage))
            {
                if (System.IO.File.Exists(driverImage))
                    System.IO.File.Delete(driverImage);
                System.IO.File.Move(tempDriverImage, driverImage);
            }
        }

        private void OverwriteTeamImage(string folder)
        {
            string tempTeamImage = Path.Combine(Server.MapPath("~/Images"), "tempTeam");
            string teamImage = Path.Combine(folder, "team");
            if (System.IO.File.Exists(tempTeamImage))
            {
                if (System.IO.File.Exists(teamImage))
                    System.IO.File.Delete(teamImage);
                System.IO.File.Move(tempTeamImage, teamImage);
            }
        }

        //
        // GET: /Team/Delete/5

        public ActionResult Delete(int id)
        {
            Team team = db.TeamSet.Find(id);
            SendMessage(MessageType.Warning, "If you delete the team, you cannot recover it later!");
            return View(team);
        }

        //
        // POST: /Team/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Team team = db.TeamSet.Find(id);

            string coDriverImage = Server.MapPath(team.CoDriverImageUrl);
            string driverImage = Server.MapPath(team.DriverImageUrl);
            string teamImage = Server.MapPath(team.TeamImageUrl);

            db.TeamSet.Remove(team);
            db.SaveChanges();

            if (System.IO.File.Exists(teamImage))
                System.IO.File.Delete(teamImage);
            if (System.IO.File.Exists(driverImage))
                System.IO.File.Delete(driverImage);
            if (System.IO.File.Exists(coDriverImage))
                System.IO.File.Delete(coDriverImage);

            SendMessage(MessageType.Success, "The team successfully deleted!");
            return RedirectToAction("Index");
        }
    }
}