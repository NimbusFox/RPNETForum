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

        public ActionResult Index(int id) {
            if (_userMethods.UserExists(id)) {
                return View((User)_userMethods.GetUserByID(id));
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