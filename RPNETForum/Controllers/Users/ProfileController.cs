using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RPNETForum.Classes.Models;
using RPNETForum.Classes.Users;
using RPNETForum.Interfaces.DatabaseMethods;

namespace RPNETForum.Controllers.Users {
    public class ProfileController : Controller {

        private IUserMethods _userMethods;

        public ProfileController(IUserMethods userMethods) {
            _userMethods = userMethods;
        }

        public ActionResult Index(string id) {
            if (int.TryParse(id, out int userID)) {
                if (_userMethods.UserExists(userID)) {
                    UserSession.PreviousURL = UserSession.CurrentContext.Request.RawUrl;
                    return View("Index", (User) _userMethods.GetUserByID(userID));
                }
            } else {
                if (_userMethods.DisplayNameExists(id)) {
                    UserSession.PreviousURL = UserSession.CurrentContext.Request.RawUrl;
                    return View("Index", (User)_userMethods.GetUserByDisplayName(id));
                }
            }
            return View("Redirect", new RedirectModel {
                Message = "There is no user with this ID",
                RedirectSeconds = 3,
                RedirectTo = Settings.Url,
                Title = "User does not exist"
            });
        }
    }
}