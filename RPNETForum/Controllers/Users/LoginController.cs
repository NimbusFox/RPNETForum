using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RPNETForum.Classes.Models.Users;
using RPNETForum.Interfaces.DatabaseMethods;

namespace RPNETForum.Controllers.Users {
    public class LoginController : Controller {

        private IUserMethods _userMethods;

        public LoginController(IUserMethods userMethods) {
            _userMethods = userMethods;
        }
        
        public ActionResult Index() {
            return View(new Tuple<bool, bool, string>(true, true, ""));
        }

        [HttpPost]
        public ActionResult Index(string dummy) {
            var login = new LoginModel(Request);

            var output = RPNETForum.Users.Login(login.Username, login.Password, _userMethods);

            if (!output.password || !output.verified) {
                return View(new Tuple<bool, bool, string>(output.Item1, output.Item2, login.Username));
            } else {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}