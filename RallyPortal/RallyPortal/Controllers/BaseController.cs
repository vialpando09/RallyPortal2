using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RallyPortal.Controllers
{
    public class BaseController : Controller
    {
        protected ModelContainer db = new ModelContainer();

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        protected void SendMessage(MessageType type, string message)
        {
            TempData["GlobalMessageType"] = type;
            if(type == MessageType.Error)
                TempData["ViewBag.GlobalHeader"] = "Ooops, this is an error message!";
            else if(type == MessageType.Information)
                TempData["ViewBag.GlobalHeader"] = "Information.";
            else if (type == MessageType.Success)
                TempData["ViewBag.GlobalHeader"] = "Hooooray!!";
            else if (type == MessageType.Warning)
                TempData["ViewBag.GlobalHeader"] = "This is a warning!";
            TempData["ViewBag.GlobalMessage"] = message;
        }
    }
}
