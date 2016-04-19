using SearchForKnowledge.Models;
using SearchForKnowledge.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SearchForKnowledge;

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

            return View(new UsersIndex { 
                
            });
           
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Register(UsersNew form)
        {
            var user= new User();
            UserDB db = new UserDB();
            db.addUser(form.Username, form.Password, form.SchoolName, form.Country, form.City);
            return RedirectToAction("Index");
        }
	}
}