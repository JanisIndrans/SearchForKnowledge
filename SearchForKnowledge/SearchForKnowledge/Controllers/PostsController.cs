using System.Collections.Generic;
using System.Web.Mvc;
using SearchForKnowledge.Models;
using SearchForKnowledge.ViewModels;
using SearchForKnowledge.Database;
using System;
using System.IO;
using Microsoft.Ajax.Utilities;

namespace SearchForKnowledge.Controllers
{
    public class PostsController : Controller
    {
        public ActionResult Index()
        {
            PostDB db =new PostDB();
            List<Post> posts = db.getAllPosts();

            return View(new PostsShowAll
            {
                Posts = posts

            });
        }
        public ActionResult CreatePost()
        {
            return View(new PostsNew
            {

            });
        }


        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult CreatePost(PostsNew form)
        {
            PostDB db = new PostDB();
            string imgPath = "";
            if (form.ImgFile != null)
            {
                string pathToSave = Server.MapPath("~/Content/Images/UserImages/");
                string newFileName = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(form.ImgFile.FileName);
                form.ImgFile.SaveAs(Path.Combine(pathToSave, newFileName));
                imgPath = "/content/images/UserImages/" + newFileName;
            }


            db.createPost(form.BookTitle, form.Author, imgPath, Int32.Parse(form.UserId), Int32.Parse(form.CategoryId), form.Description);
            
            return RedirectToRoute("Home");
        }
        }

    }
