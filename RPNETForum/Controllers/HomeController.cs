using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
            return _userMethods.Verify(token) ? View("Success") : View("Fail");
        }

        public ActionResult Logout() {
            RPNETForum.Users.Logout();

            return View();
        }
    }
}