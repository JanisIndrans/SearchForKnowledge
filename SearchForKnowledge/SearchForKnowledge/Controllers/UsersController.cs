using SearchForKnowledge.Models;
using SearchForKnowledge.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
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
            //UserDB db = new UserDB();
            //var result = db.loginUser(form.Username, form.Password);
            var result = "Janis";
            if (!result.IsEmpty())
            {
                Session["userName"] = result;
                return RedirectToRoute("Home");
            }
            return RedirectToRoute("Login");
        }
        [HttpPost]
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToRoute("Home");
        }
    }
}