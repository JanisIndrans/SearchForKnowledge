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
            User user = new User();
            user.SetPassword(form.Password);
            string hash = user.Password;
            return View(new UsersNew
            {
                Password = hash
            });
            //UserDB db = new UserDB();
            //db.addUser(form.Username, form.Password, form.SchoolName, form.Country, form.City);
            //return RedirectToRoute("Home");
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
            User user = new User();
            user.Password = "$2a$13$jsfLkGh8K8RyAMobTpaakOYgMi0XcHdOjsjqFSFwcJRh3T53YxCzm";
            user.Username = "Janis";

            if (!user.Username.IsEmpty() && user.CheckPassword(form.Password))
            {
                Session["userName"] = user.Username;
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