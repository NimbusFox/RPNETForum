using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RPNETForum.Interfaces.DatabaseMethods;
using RPNETForum.Models.Users;

namespace RPNETForum.Controllers.Users {
    public class RegisterController : Controller {
        private IUserMethods _userMethods;

        public RegisterController(IUserMethods userMethods) {
            _userMethods = userMethods;
        }

        public ActionResult Index() {
            return View();
        }

        [HttpPost]
        public JsonResult Submit(RegisterModel data) {
            var response = new RegisterResponseModel();

            if (_userMethods.UserExists(data.Username)) {
                response.Username = false;
                response.UsernameReason = "This username has already been taken";
            } else {
                response.Username = true;
            }

            if (data.Password != data.ConfirmPassword) {
                response.Password = false;
                response.PasswordReason = "The passwords do not match";
            } else {
                response.Password = true;
            }

            if (!Validation.UserValidation.IsValidEmail(data.Email)) {
                response.Email = false;
                response.EmailReason = "Invalid email address";
            } else {
                response.Email = true;
            }

            return Json(response);
        } 
    }
}