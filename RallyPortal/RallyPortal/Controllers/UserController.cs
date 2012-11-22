using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Microsoft.Web.WebPages.OAuth;
using WebMatrix.WebData;
using RallyPortal.Helpers;
using RallyPortal.Models;

namespace RallyPortal.Controllers
{

    [Authorize(Roles = "Administrator, SuperAdministrator")]
    public class UserController : BaseController
    {
        //
        // GET: /User/

        public ActionResult Index()
        {
            ViewBag.SuperAdministrator = Roles.GetUsersInRole("SuperAdministrator").GetUserProfiles(userContext);
            ViewBag.Administrator = Roles.GetUsersInRole("Administrator").GetUsersNotInRoles( new string[] { "SuperAdministrator" }).GetUserProfiles(userContext);
            ViewBag.Reader = ((SimpleMembershipProvider)(Membership.Provider)).GetUsersNotInRoles(new string[] { "Administrator", "SuperAdministrator" }, userContext);
            
            return View();
        }

        public ActionResult Promotion(string UserName)
        {
            Roles.AddUserToRole(UserName, "Administrator");
            SendMessage(MessageType.Success, UserName + " has been promoted for Administrator.");
            return RedirectToAction("Index");
        }

        public ActionResult Demotion(string UserName)
        {
            Roles.RemoveUserFromRole(UserName, "Administrator");
            SendMessage(MessageType.Success, UserName + " has been demoted for Reader.");
            return RedirectToAction("Index");
        }

        public ActionResult Delete(string UserName)
        {
            if (Roles.IsUserInRole(UserName, "SuperAdministrator"))
            {
                SendMessage(MessageType.Error, UserName + " is the SuperAdministrator.");
                return RedirectToAction("Index");
            }

            CurrentMembershipProvider.DeleteUser(UserName, true);
            CurrentMembershipProvider.DeleteAccount(UserName);

            SendMessage(MessageType.Success, UserName + " successfully deleted.");
            return RedirectToAction("Index");
            
        }
    }
}
