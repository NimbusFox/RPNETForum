using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RPNETForum.Classes.Models;
using RPNETForum.Interfaces.DatabaseMethods;

namespace RPNETForum.Controllers {
    public class HomeController : Controller {
        private IUserMethods _userMethods;

        public HomeController(IUserMethods userMethods) {
            _userMethods = userMethods;
        }

        public ActionResult Index() {
            return View();
        }

        public ActionResult Verify(string token) {
            return _userMethods.Verify(token) ? View("Redirect", new RedirectModel {
                Message = "You will now be redirected to the login page.",
                RedirectSeconds = 3,
                RedirectTo = Settings.Url + "/Login",
                Title = "Verification Successful"
            }) : View("Redirect", new RedirectModel {
                Message = "If you followed a link please check if it's from an official source",
                RedirectSeconds = 3,
                RedirectTo = Settings.Url,
                Title = "Invalid verification"
            });
        }

        public ActionResult Logout() {
            RPNETForum.Users.Logout();

            return View("Redirect", new RedirectModel {
                Message = "You have been successfully logged out",
                RedirectSeconds = 3,
                RedirectTo = string.IsNullOrWhiteSpace(UserSession.PreviousURL) || string.IsNullOrWhiteSpace(UserSession.PreviousURL) ? Settings.Url : UserSession.PreviousURL,
                Title = "Logged out"
            });
        }
    }
}