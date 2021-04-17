using DemoProject.BusinessService;
using DemoProject.Interface;
using MyDemoWebApplication.Models;
using System;
using System.Web.Mvc;

namespace MyDemoWebApplication.Controllers
{
    public class LoginController : Controller
    {
        [AllowAnonymous]
        [HttpGet]
        public ActionResult Login()
        {
            return View("Login", new UserViewModel());
        }

        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult DoLogin(UserViewModel user)
        {
            if (user == null || string.IsNullOrWhiteSpace(user.UserName) || string.IsNullOrWhiteSpace(user.Password))
            {
                ViewBag.error = "Invalid User";
                return View("Login", new UserViewModel());
            }

            UserData objUser = UserData.GetUser(user.UserName, user.Password);
            if (objUser != null)
            {
                Session["username"] = objUser.UserName;
                Session["userid"] = objUser.UserID;
                return RedirectToAction("Notes","Notes");
            }
            else
            {
                ViewBag.error = "Invalid User";
                return View("Login", new UserViewModel());
            }
        }

        [HttpGet]
        public ActionResult Logout()
        {
            Session.Remove("username");
            return RedirectToAction("Login", new UserViewModel());
        }
    }
}