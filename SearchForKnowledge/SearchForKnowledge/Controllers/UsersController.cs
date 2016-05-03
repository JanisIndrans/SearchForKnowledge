using SearchForKnowledge.Models;
using SearchForKnowledge.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using SearchForKnowledge;
using SearchForKnowledge.Database;

namespace SearchForKnowledge.Controllers
{
    public class UsersController : Controller
    {
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
            UserDb db = new UserDb();
            if (!db.AddUser(form.Username, hash, form.SchoolName, form.Country, form.City, form.Type))
            {
                return View(new UsersNew
                {
                    DuplicateUserMessage = "This username already exists in database. Please choose a different one.",
                    //City = form.City,
                    //Password = form.Password,
                    //ConfirmPassword = form.ConfirmPassword,
                    //Country = form.Country,
                    //SchoolName = form.SchoolName
                });
            }
            Session["userName"] = form.Username;
            return RedirectToRoute("WelcomePage");

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
            UserDb udb = new UserDb();

            if (!form.Username.IsEmpty())
            {
                string passwordHash = udb.GetPassword(form.Username);
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
            UserDb udb = new UserDb();
            if (udb.GetType(Session["userName"].ToString()) == "Admin")
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
                UserDb udb = new UserDb();
                string hash = ap.Password;

                udb.UpdateUser(ap.Username, BCrypt.Net.BCrypt.HashPassword(hash), ap.SchoolName, ap.Country, ap.City);
                return RedirectToRoute("Home");
        }

        public ActionResult WelcomePage()
        {
            return View();
        }

    }
}