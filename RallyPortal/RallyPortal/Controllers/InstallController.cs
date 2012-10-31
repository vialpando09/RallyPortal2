using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace RallyPortal.Controllers
{
    public class InstallController : Controller
    {
        //
        // GET: /Install/

        public ActionResult Index()
        {
            Roles.AddUsersToRoles(new string[] { "vialpando" }, new string[] { "Reader", "Administrator", "SuperAdministrator" });

            return View();
        }

    }
}
