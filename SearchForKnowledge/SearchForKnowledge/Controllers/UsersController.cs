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
            db.addUser(form.Username, hash, form.SchoolName, form.Country, form.City, form.Type);
            //return RedirectToRoute("Home");
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
            UserDB udb = new UserDB();

            if (!form.Username.IsEmpty())
            {
                string passwordHash = udb.getPassword(form.Username);
                string password = form.Password;
                if (BCrypt.Net.BCrypt.Verify(password, passwordHash)) {
                    Session["userName"] = form.Username;
                    return RedirectToRoute("Home");
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
            UserDB udb = new UserDB();
            if (udb.getType(Session["userName"].ToString()) == "Admin")
            {
                return View(new AdminPage { });
            }
            else
            {
                
                return RedirectToRoute("Home");
            }
        }

        [HttpPost]
        public ActionResult AdminPage(AdminPage ap)
        {        
                UserDB udb = new UserDB();
                string hash = ap.Password;

                udb.updateUser(ap.Username, BCrypt.Net.BCrypt.HashPassword(hash), ap.SchoolName, ap.Country, ap.City);
                return RedirectToRoute("Home");
        }

    }
}