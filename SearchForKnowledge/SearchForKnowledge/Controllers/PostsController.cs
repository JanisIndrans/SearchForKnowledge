using System.Collections.Generic;
using System.Web.Mvc;
using SearchForKnowledge.Models;
using SearchForKnowledge.ViewModels;

namespace SearchForKnowledge.Controllers
{
    public class PostsController : Controller
    {
        public ActionResult Index()
        {
            List<Post> posts = new List<Post>();
            Post p = new Post(103, "Janis", "/12/34v/ghaf/", 33, 1,"Some book about something");
            Post p1 = new Post(103, "Nikita", "/12/34v/ghaf/", 33, 2,"Some book about something");
            Post p2 = new Post(103, "Viktor", "/12/34v/ghaf/", 33, 3,"Some book about something");
            posts.Add(p);
            posts.Add(p1);
            posts.Add(p2);

            return View(new PostsShowAll
            {
                Posts = posts

            });
        }

    }
}