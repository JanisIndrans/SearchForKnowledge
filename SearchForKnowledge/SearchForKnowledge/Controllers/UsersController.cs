using SearchForKnowledge.Models;
using SearchForKnowledge.ViewModels;
using System.Web.Mvc;
using System.Web.WebPages;
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
            user.Type = "user";
            user.Username = form.Username;
            user.Country = form.Country;
            user.City = form.City;
            user.SchoolName = form.SchoolName;
            UserDb db = new UserDb();

            if (!db.AddUser(user))
            {
                return View(new UsersNew
                {
                    DuplicateUserMessage = "This username already exists in database. Please choose a different one.",
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

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Login(UsersLogin form)
        {
            UserDb db = new UserDb();

            if (!form.Username.IsEmpty())
            {
                if (db.GetUserByUsername(form.Username) != null)
                {
                    if (BCrypt.Net.BCrypt.Verify(form.Password, db.GetUserByUsername(form.Username).Password))
                    {
                        Session["username"] = form.Username;
                        return RedirectToRoute("Home");
                    }
                }

            }
            return View(new UsersLogin
            {
                ErrorMessage = "Username or password is wrong!"
            });
        }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToRoute("Home");
        }
        
        public ActionResult AdminPage()
        {
            UserDb udb = new UserDb();
            if (udb.GetUserByUsername(Session["username"].ToString()).Type == "Admin")
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