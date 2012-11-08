using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebMatrix.WebData;
using RallyPortal.Models;
using System.Web.Helpers;

namespace RallyPortal.Controllers
{
    public class BaseController : Controller
    {
        protected ModelContainer db = new ModelContainer();
        protected UsersContext userContext = new UsersContext();

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            userContext.Dispose();

            base.Dispose(disposing);
        }


        protected SimpleMembershipProvider CurrentMembershipProvider
        {
            get { return ((SimpleMembershipProvider)Membership.Provider); }
        }


        protected void SendMessage(MessageType type, string message)
        {
            TempData["GlobalMessageType"] = type;
            if (type == MessageType.Error)
                TempData["ViewBag.GlobalHeader"] = "Ooops!";
            else if (type == MessageType.Information)
                TempData["ViewBag.GlobalHeader"] = "Information.";
            else if (type == MessageType.Success)
                TempData["ViewBag.GlobalHeader"] = "Hooooray!!";
            else if (type == MessageType.Warning)
                TempData["ViewBag.GlobalHeader"] = "This is a warning!";
            TempData["ViewBag.GlobalMessage"] = message;
        }

        [AllowAnonymous()]
        public void GetPhotoThumbnail(string path, int width)
        {
            using (System.Drawing.Image imgOriginal = System.Drawing.Image.FromFile(Server.MapPath(path)))
            {
                int height = imgOriginal.Height * width / imgOriginal.Width;

                new WebImage(Server.MapPath(path))
                    .Resize(width, height, true, false) // Resizing the image to 100x100 px on the fly...
                    .Crop(1, 1) // Cropping it to remove 1px border at top and left sides (bug in WebImage)
                    .Write();

                // Loading a default photo for realties that don't have a Photo
                new WebImage(Server.MapPath(@"~/Images/no_image.jpg")).Write();
            }
        }
    }
}
