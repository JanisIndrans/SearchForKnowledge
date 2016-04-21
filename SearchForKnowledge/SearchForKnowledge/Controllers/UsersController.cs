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
        public ActionResult Register()
        {

            return View(new UsersNew
            {
                
            });
           
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Register(UsersNew form)
        {
            var user= new User();
            UserDB db = new UserDB();
            db.addUser(form.Username, form.Password, form.SchoolName, form.Country, form.City);
            return RedirectToRoute("Home");
        }

        public ActionResult Login()
        {
            return View(new UsersLogin
            {
                
            });
        }

        [HttpPost]
        public ActionResult Login(UsersLogin form)
        {
            var user = new User();
            UserDB db = new UserDB();
            var result = db.loginUser(form.Username, form.Password);
            if (result.Equals(1))
            {
                return RedirectToRoute("Home");
            }
            else return RedirectToRoute("Login");
        }
	}
}