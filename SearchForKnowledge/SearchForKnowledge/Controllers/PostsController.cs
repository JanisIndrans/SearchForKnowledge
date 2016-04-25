using System.Collections.Generic;
using System.Web.Mvc;
using SearchForKnowledge.Models;
using SearchForKnowledge.ViewModels;
using SearchForKnowledge.Database;
using System;

namespace SearchForKnowledge.Controllers
{
    public class PostsController : Controller
    {
        public ActionResult Index()
        {
            List<Post> posts = new List<Post>();
           Post p = new Post("HarryPotter","JK","picpath",120,1,"description");
           //Post p1 = new Post(103, "Nikita", "/12/34v/ghaf/", 33, 2, "Some book about something");
           //Post p2 = new Post(103, "Viktor", "/12/34v/ghaf/", 33, 3, "Some book about something");
           posts.Add(p);
          // posts.Add(p1);
          // posts.Add(p2);

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
            //var post = new Post();
            PostDB db = new PostDB();
            db.createPost(form.BookTitle, form.Author, form.PicturePath, Int32.Parse(form.UserId), Int32.Parse(form.CategoryId), form.Description);
            
            return RedirectToRoute("Home");
        }
        }

    }
