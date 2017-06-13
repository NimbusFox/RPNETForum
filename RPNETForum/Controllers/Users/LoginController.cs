using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RPNETForum.Classes.Models;
using RPNETForum.Classes.Models.Users;
using RPNETForum.Interfaces.DatabaseMethods;

namespace RPNETForum.Controllers.Users {
    public class LoginController : Controller {
        
        public ActionResult Index() {
            return View(new Tuple<bool, bool, string>(true, true, ""));
        }

        [HttpPost]
        public ActionResult Index(string dummy) {
            var login = new LoginModel(Request);

            var output = RPNETForum.Users.Login(login.Username, login.Password);

            if (!output.password || !output.verified) {
                return View(new Tuple<bool, bool, string>(output.Item1, output.Item2, login.Username));
            } else {

                var user = UserSession.CurrentUser;

                UserSession.CurrentUser = null;

                return View("Redirect", new RedirectModel {
                    Message = "You have been logged in successfully",
                    RedirectSeconds = 3,
                    RedirectTo = string.IsNullOrWhiteSpace(UserSession.PreviousURL) || string.IsNullOrWhiteSpace(UserSession.PreviousURL) ? Settings.Url : UserSession.PreviousURL,
                    Title = "Welcome back " + user.DisplayName
                });
            }
        }
    }
}