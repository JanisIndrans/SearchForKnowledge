using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SearchForKnowledge.Controllers
{
    public class UsersController : Controller
    {
        //
        // GET: /Users/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }
	}
}