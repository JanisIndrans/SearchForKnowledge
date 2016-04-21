using SearchForKnowledge.Models;
using SearchForKnowledge.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SearchForKnowledge.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/
        public ActionResult Login()
        {
            return View();
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