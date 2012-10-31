using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Telerik.Web.Mvc.UI;

namespace RallyPortal.Controllers
{
    public class EditorImageBrowserController : EditorFileBrowserController
    {
        private const string contentFolderRoot = "~/Images/ArticleContents/";
        private const string prettyName = "Images/";
        private static readonly string[] foldersToCopy = new[] { "~/Images/ArticleContents/" };
        /// <summary>
        /// Gets the base paths from which content will be served.
        /// </summary>
        public override string[] ContentPaths
        {
            get
            {
                return new[] { CreateUserFolder() };
            }
        }

        private string CreateUserFolder()
        {
            var path = Server.MapPath(contentFolderRoot);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
                foreach (var sourceFolder in foldersToCopy)
                {
                    CopyFolder(Server.MapPath(sourceFolder), path);
                }
            }
            return contentFolderRoot;
        }
        private void CopyFolder(string source, string destination)
        {
            if (!Directory.Exists(destination))
            {
                Directory.CreateDirectory(destination);
            }
            foreach (var file in Directory.EnumerateFiles(source))
            {
                var dest = Path.Combine(destination, Path.GetFileName(file));
                System.IO.File.Copy(file, dest);
            }
            foreach (var folder in Directory.EnumerateDirectories(source))
            {
                var dest = Path.Combine(destination, Path.GetFileName(folder));
                CopyFolder(folder, dest);
            }
        }

    }
}
