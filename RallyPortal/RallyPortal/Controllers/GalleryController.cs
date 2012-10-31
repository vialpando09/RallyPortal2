﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RallyPortal;

namespace RallyPortal.Controllers
{
    [Authorize(Roles = "Administrator, SuperAdministrator")]
    public class GalleryController : BaseController
    {

        //
        // GET: /Gallery/

        public ViewResult Index()
        {
            return View(db.GallerySet.ToList());
        }

        //
        // GET: /Gallery/Details/5

        public ViewResult Details(int id)
        {
            Gallery gallery = db.GallerySet.Find(id);
            return View(gallery);
        }

        //
        // GET: /Gallery/Create

        public ActionResult Create()
        {
            DeleteTempGallery();
            return View();
        }

        private void DeleteTempGallery()
        {

            // Delete every file from tempGallery
            var path = Server.MapPath("~/Images/Galleries/tempGallery");

            if (Directory.Exists(path))
            {
                var files = Directory.GetFiles(path);
                foreach (var file in files)
                {
                    System.IO.File.Delete(file);
                }
            }
            ViewBag.NextImageId = 0;
        } 

        //
        // POST: /Gallery/Create
        /// <param name="gallery">Gallery element</param>
        /// <param name="path">A string which contains the category and subcategories</param>
        /// <param name="ids">Identify the images</param>
        /// <param name="titles">Titles of the images</param>
        /// <param name="descriptions">Descriptions of the images</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(Gallery gallery, string path, string[] ids, string[] titles, string[] descriptions, string[] filenames)
        {
            if (ModelState.IsValid)
            {
                // Gallery ID
                int id;
                try
                {
                    id = db.GallerySet.OrderByDescending(e => e.Id).Select(e => e.Id).First() + 1;
                }
                catch (Exception)
                {
                    id = 0;
                }

                // Check and move images
                var imagesPath = Server.MapPath("~/Images/Galleries/tempGallery");
                var copyPath = Server.MapPath("~/Images/Galleries/" + id.ToString());
                {
                    //Create directory
                    if (!Directory.Exists(copyPath))
                    {
                        Directory.CreateDirectory(copyPath);
                    }
                }
                {
                    //Move files and create image objects
                    var files = Directory.GetFiles(imagesPath);
                    foreach (var file in files)
                    {
                        for (int i = 0; i < ids.Length; i++)
                        {
                            if (filenames[i] == Path.GetFileName(file))
                            {
                                var image = new Image { Description = descriptions[i], Title = titles[i], ImageId = int.Parse(ids[i]), Gallery = gallery };
                                db.ImageSet.Add(image);
                                gallery.Images.Add(image);
                                var destSource = Path.Combine(copyPath, ids[i]);
                                System.IO.File.Move(file, destSource);
                            }
                        }
                    }
                }
                gallery.Category = GenerateGalleryCategories(path);
                db.GallerySet.Add(gallery);
                db.SaveChanges();
                SendMessage(MessageType.Success, "The gallery successfully created!");
                return RedirectToAction("Index");
            }

            SendMessage(MessageType.Error, "Something went wrong! :( Try it again");  
            return View(gallery);
        }
        
        //
        // GET: /Gallery/Edit/5
 
        public ActionResult Edit(int id)
        {
            DeleteTempGallery();

            // Get Gallery
            Gallery gallery = db.GallerySet.Find(id);
            string path = "";
            GalleryCategory category = gallery.Category;
            while (category != null)
            {
                path = "/" + category.Title + path;
                category = category.Parent;
            }
            ViewBag.Path = path;

            // Get next imageId
            int imageId;
            try
            {
                imageId = gallery.Images.Select(e => e.ImageId).OrderByDescending(e => e).First() + 1;
            }
            catch (Exception)
            {
                imageId = 0;
            }

            ViewBag.NextImageId = imageId;
            
            return View(gallery);
        }

        //
        // POST: /Gallery/Edit/5

        [HttpPost]
        public ActionResult Edit(Gallery gallery, string path, string[] ids, string[] titles, string[] descriptions, string[] filenames)
        {
            if (ModelState.IsValid)
            {
                // Gallery ID
                var id = gallery.Id;                

                // Check and move images
                var imagesPath = Server.MapPath("~/Images/Galleries/tempGallery");
                var copyPath = Server.MapPath("~/Images/Galleries/" + id.ToString());
                {
                    //Create directory
                    if (!Directory.Exists(copyPath))
                    {
                        Directory.CreateDirectory(copyPath);
                    }
                }
                {
                    //Move files and create image objects
                    var files = Directory.GetFiles(imagesPath);
                    foreach (var file in files)
                    {
                        for (int i = 0; i < ids.Length; i++)
                        {
                            if (filenames[i] == Path.GetFileName(file))
                            {
                                var image = new Image { Description = descriptions[i], Title = titles[i], ImageId = int.Parse(ids[i]), Gallery = gallery };
                                db.ImageSet.Add(image);
                                gallery.Images.Add(image);
                                var destSource = Path.Combine(copyPath, ids[i]);
                                System.IO.File.Move(file, destSource);
                            }
                        }
                    }
                    //Delete deleted object's file
                    files = Directory.GetFiles(copyPath);
                    foreach (var file in files)
                    {
                        if (!ids.Contains(Path.GetFileName(file)))
                            System.IO.File.Delete(file);
                    }
                    
                }

                gallery.Category = GenerateGalleryCategories(path);
                DeleteNotUsingCategories();
                db.Entry(gallery).State = EntityState.Modified;
                db.SaveChanges();
                SendMessage(MessageType.Success, "The gallery successfully modified!");
                return RedirectToAction("Index");
            }
            SendMessage(MessageType.Error, "Something went wrong! :( Try it again");           
            return View(gallery);
        }

        //
        // GET: /Gallery/Delete/5
 
        public ActionResult Delete(int id)
        {
            Gallery gallery = db.GallerySet.Find(id);
            SendMessage(MessageType.Warning, "If you delete the gallery, you cannot recover it later!");
            return View(gallery);
        }

        //
        // POST: /Gallery/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Gallery gallery = db.GallerySet.Find(id);
            db.GallerySet.Remove(gallery);
            DeleteNotUsingCategories();
            db.SaveChanges();
            SendMessage(MessageType.Success, "The gallery successfully deleted!");
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Returns the id of last element of the path
        /// </summary>
        /// <param name="path">Contains the category path. I.e.: /Category1/Category2/Category3/...</param>
        /// <param name="parent"></param>
        /// <returns></returns>
        private GalleryCategory GenerateGalleryCategories(string path)
        {
            var pathArray = path.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            return GetCategory(pathArray);           
        }

        /// <summary>
        /// Returns the gallery category with the name "categoryName", and with the parent "parent". If this category not exists, it will be created
        /// </summary>
        /// <param name="title"></param>
        /// <param name="parent"></param>
        /// <returns></returns>
        private GalleryCategory GetCategory(string[] pathArray, GalleryCategory parent = null, int level = 0)
        {
            GalleryCategory category;
            var title = pathArray[level];
            if (parent == null)
            {
                category = db.GalleryCategorySet.Where(e => e.Parent == null && e.Title == title).FirstOrDefault();
            }
            else
            {
                category = parent.SubCategories.Where(e => e.Title == title).FirstOrDefault();
            }
            if (category == null)
            {
                category = new GalleryCategory { Title = pathArray[level], Parent = parent };
                db.GalleryCategorySet.Add(category);
                if (parent != null)
                {
                    parent.SubCategories.Add(category);
                }
            }

            if(pathArray.Length > level +1 )
                return GetCategory(pathArray, category, level+1);

            return category;
        }

        /// <summary>
        /// Delete all of the categories that we are not using
        /// </summary>
        private void DeleteNotUsingCategories()
        {
            bool flag = true;
            while (flag)
            {
                var toDeleteCategories = db.GalleryCategorySet.Where(e => e.SubCategories.Count == 0 && e.Galleries.Count == 0).ToArray();
                if (toDeleteCategories.Length == 0)
                    flag = false;
                else
                    foreach (var category in toDeleteCategories)
                    {
                        db.GalleryCategorySet.Remove(category);
                    }
            }
        }

        public ActionResult Publish(int id)
        {
            Gallery entry = db.GallerySet.Single(e => e.Id == id);
            entry.Published = true;

            db.Entry(entry).State = EntityState.Modified;
            db.SaveChanges();

            SendMessage(MessageType.Success, "The gallery successfully published!");
            return RedirectToAction("Index");
        }

        public ActionResult Unpublish(int id)
        {
            Gallery entry = db.GallerySet.Single(e => e.Id == id);
            entry.Published = false;

            db.Entry(entry).State = EntityState.Modified;
            db.SaveChanges();

            SendMessage(MessageType.Success, "The gallery successfully unpublished!");
            return RedirectToAction("Index");
        }
    }
}