using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RallyPortal.Controllers
{
    public class UploadController : Controller
    {
        // Default Save/Remove
        public ActionResult SaveFile(IEnumerable<HttpPostedFileBase> files, string path, string[] fileNames)
        {
            bool notUseOriginalFileNames = fileNames != null;
            if (notUseOriginalFileNames && fileNames.Count() != files.Count())
                return Content("Error");

            for (int i = 0; i < files.Count(); i++)
            {
                var file = files.ElementAt(i);
                // Some browsers send file names with full path. This needs to be stripped.
                string fileName = "";
                if (!notUseOriginalFileNames)
                    fileName = Path.GetFileName(file.FileName);
                else
                    fileName = fileNames[i];

                var physicalPath = Path.Combine(Server.MapPath(path), fileName);
                file.SaveAs(physicalPath);
            }
            // Return an empty string to signify success
            return Content("");
        }
        public ActionResult RemoveFile(string[] fileNames, string path)
        {
            // The parameter of the Remove action must be called "fileNames"
            foreach (var fullName in fileNames)
            {
                var fileName = Path.GetFileName(fullName);
                var physicalPath = Path.Combine(Server.MapPath(path), fileName);

                if (System.IO.File.Exists(physicalPath))
                {
                    System.IO.File.Delete(physicalPath);
                }
            }
            // Return an empty string to signify success
            return Content("");
        }

        // TempImage Save/Remove
        public ActionResult SaveTempFeaturedImage(IEnumerable<HttpPostedFileBase> attachments)
        {
            List<string> fileNames = new List<string>();
            foreach (var image in attachments)
            {
                fileNames.Add("temp");
            }
            return SaveFile(attachments, "~/Images", fileNames.ToArray());
        }
        public ActionResult RemoveTempFeaturedImage(string[] fileNames)
        {
            return RemoveFile(fileNames, "~/Images");
        }

        // TempTeamImage Save/Remove
        public ActionResult SaveTempTeamImage(IEnumerable<HttpPostedFileBase> teamImageUrl)
        {
            List<string> fileNames = new List<string>();
            foreach (var image in teamImageUrl)
            {
                fileNames.Add("tempTeam");
            }
            return SaveFile(teamImageUrl, "~/Images", fileNames.ToArray());
        }
        public ActionResult RemoveTempTeamImage(string[] fileNames)
        {
            return RemoveFile(fileNames, "~/Images");
        }

        // TempDriverImage Save/Remove
        public ActionResult SaveTempDriverImage(IEnumerable<HttpPostedFileBase> teamDriverImageUrl)
        {
            List<string> fileNames = new List<string>();
            foreach (var image in teamDriverImageUrl)
            {
                fileNames.Add("tempDriver");
            }
            return SaveFile(teamDriverImageUrl, "~/Images", fileNames.ToArray());
        }
        public ActionResult RemoveTempDriverImage(string[] fileNames)
        {
            return RemoveFile(fileNames, "~/Images");
        }

        // TempCoDriverImage Save/Remove
        public ActionResult SaveTempCoDriverImage(IEnumerable<HttpPostedFileBase> teamCoDriverImageUrl)
        {
            List<string> fileNames = new List<string>();
            foreach (var image in teamCoDriverImageUrl)
            {
                fileNames.Add("tempCoDriver");
            }
            return SaveFile(teamCoDriverImageUrl, "~/Images", fileNames.ToArray());
        }
        public ActionResult RemoveTempCoDriverImage(string[] fileNames)
        {
            return RemoveFile(fileNames, "~/Images");
        }


        // TempGalleryImages Save/Remove
        public ActionResult SaveTempGalleryImage(IEnumerable<HttpPostedFileBase> attachments)
        {
            return SaveFile(attachments, "~/Images/Galleries/tempGallery", null);
        }
        public ActionResult RemoveTempGalleryImage(string[] fileNames)
        {
            return RemoveFile(fileNames, "~/Images/Galleries/tempGallery");
        }

        // Create/Edit FeatureImage Save/Remove
        public bool SaveFeaturedImage(string sourcePath, string destPath)
        {
            if (System.IO.File.Exists(sourcePath))
            {
                System.IO.File.Move(sourcePath, destPath);
                return true;
            }
            return false;
        }
        public ActionResult RemoveFeaturedImage(string fileNames)
        {
            return RemoveFile((new List<string> { fileNames }).ToArray(), "~/Images/FeaturedImages");
        }

        // Delete temp FeaturedImage
        public void DeleteFile(string physicalPath)
        {
            if (System.IO.File.Exists(physicalPath))
            {
                System.IO.File.Delete(physicalPath);
            }
        }
    }
}

