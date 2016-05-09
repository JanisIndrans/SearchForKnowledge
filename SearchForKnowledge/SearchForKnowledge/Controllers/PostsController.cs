using System.Collections.Generic;
using System.Web.Mvc;
using SearchForKnowledge.Models;
using SearchForKnowledge.ViewModels;
using SearchForKnowledge.Database;
using System;
using System.IO;
using Microsoft.Ajax.Utilities;
using SearchForKnowledge.Infrastructure;

namespace SearchForKnowledge.Controllers
{
    public class PostsController : Controller
    {

        private const int  PostsPerPage = 14;


        public ActionResult Index(int page = 1)
        {
            PostDb db = new PostDb();
            //List<Post> posts = db.GetAllPosts();
            var totalPostCount = db.CountAllPosts();
            var currentPostPage = db.GetCurrentPagePosts(page, PostsPerPage);
            

            return View(new PostsShowAll
            {
                Posts = new PagedData<Post>(currentPostPage, currentPostPage, totalPostCount, page, PostsPerPage)

            });
        }

        public ActionResult SearchPosts()
        {

            return View(new PostsSearch
            {

            });
        }
        [HttpPost]
        public ActionResult SearchPosts(string searchString)
        {
            PostDb db = new PostDb();
            List<Post> posts = db.GetSearchResults(searchString);
            if (posts != null)
            {
                return View(new PostsSearch
                {
                    
                    Posts = posts

                });
            }
            return View(new PostsSearch
            {
                ErrorMessage = "Sorry nothing was found with this title",
                Posts = new List<Post>()
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
            PostDb db = new PostDb();
            string imgPath = "";
            if (form.ImgFile != null)
            {
                string pathToSave = Server.MapPath("~/Content/Images/UserImages/");
                string newFileName = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(form.ImgFile.FileName);
                form.ImgFile.SaveAs(Path.Combine(pathToSave, newFileName));
                imgPath = "/content/images/UserImages/" + newFileName;
            }


            db.CreatePost(form.BookTitle, form.Author, imgPath, Int32.Parse(form.UserId), Int32.Parse(form.CategoryId), form.Description);
            
            return RedirectToRoute("Home");
        }


        public ActionResult Category()
        {

            return View(new PostsSelection()
            {

            });
        }
        [HttpPost]
        public ActionResult Category(string categoryName)
        {
            PostDb db = new PostDb();
            List<Post> posts = db.GetPostsByCategory(categoryName);
            if (posts != null)
            {
                return View(new PostsSelection()
                {

                    Posts = posts,
                    NameOfCategory = categoryName
                   

                });
            }
            return View(new PostsSelection()
            {
                ErrorMessage = "Sorry nothing was found with this selection",
                Posts = new List<Post>()
            });
        }

        }

    }
