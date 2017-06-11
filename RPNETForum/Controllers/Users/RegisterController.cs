using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RPNETForum.Interfaces.DatabaseMethods;
using RPNETForum.Models.Users;
using RPNETForum.Validation;

namespace RPNETForum.Controllers.Users {
    public class RegisterController : Controller {
        private IUserMethods _userMethods;

        public RegisterController(IUserMethods userMethods) {
            _userMethods = userMethods;
        }


        public ActionResult Index() {
            return View(new Tuple<RegisterModel, RegisterResponseModel, bool>(new RegisterModel(), new RegisterResponseModel(), true));
        }

        [HttpPost]
        public ActionResult Index(string dummy) {
            var data = new RegisterModel();

            var response = new RegisterResponseModel();

            if (Settings.ReCaptchaPublic != "" && Settings.ReCaptchaPrivate != "") {
                if (UserValidation.IsValidReCaptcha(Request["g-recaptcha-response"])) {
                    response.ReCaptcha.Success = true;
                } else {
                    response.ReCaptcha.Success = false;
                    response.ReCaptcha.Reason = "Invalid ReCaptcha";
                }
            } else {
                response.ReCaptcha.Success = true;
            }

            data.Username = Request["username"];
            data.Password = Request["password"];
            data.ConfirmPassword = Request["confirmPassword"];
            data.Email = Request["email"];

            if (_userMethods.UserExists(data.Username)) {
                response.Username.Success = false;
                response.Username.Reason = "This username has already been taken";
            } else {
                response.Username.Success = true;
            }

            if (!UserValidation.IsValidPassword(data.Password)) {
                response.Password.Success = false;
                response.Password.Reason =
                    "Password must contain:<br/>A Capital Letter<br/>A Lowercase Letter<br/>A Number<br/>A Special Character";
            } else if (data.Password != data.ConfirmPassword) {
                response.Password.Success = false;
                response.Password.Reason = "The passwords do not match";
            } else {
                response.Password.Success = true;
            }

            if (!UserValidation.IsValidEmail(data.Email)) {
                response.Email.Success = false;
                response.Email.Reason = "Invalid email address";
            } else {
                response.Email.Success = true;
            }

            return View(new Tuple<RegisterModel, RegisterResponseModel, bool>(data, response, false));
        }
    }
}