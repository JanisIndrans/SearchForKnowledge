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
            UserDB db = new UserDB();
            db.addUser(form.Username, hash, form.SchoolName, form.Country, form.City);
            //return RedirectToRoute("Home");
            return View(new UsersNew
            {
                
            });
            
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
            UserDB udb = new UserDB();

            if (!form.Username.IsEmpty())
            {
                string passwordHash = udb.getPassword(form.Username);
                string password = form.Password;
                if (BCrypt.Net.BCrypt.Verify(password, passwordHash)) {
                    Session["username"] = form.Username;
                    RedirectToRoute("Home");
                }
            }
            return RedirectToRoute("Login");
        }
        [HttpPost]
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToRoute("Home");
        }
        
        public ActionResult AdminPage()
        {
            return View(new AdminPage { });
        }

        [HttpPost]
        public ActionResult AdminPage(AdminPage ap)
        {
            if (Session["userName"].Equals("Viktor") || Session["userName"].Equals("Janis"))
            {
                UserDB udb = new UserDB();
                udb.updateUser(ap.Username, ap.SchoolName, ap.Country, ap.City);
                RedirectToRoute("Home");
            }
            else RedirectToRoute("Home");
            


            return View(new AdminPage { 
               
            });
        }
    }
}