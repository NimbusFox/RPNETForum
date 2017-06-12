using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.SessionState;
using RPNETForum.Classes.Models.Users;
using RPNETForum.Classes.Users;
using RPNETForum.Interfaces.DatabaseMethods;
using RPNETForum.Validation;

namespace RPNETForum {
    public static class Users {

        public static bool LoggedIn => Session.CurrentUser != null;

        public static bool Create(RegisterModel data, IUserMethods userMethods, IEmailTemplateMethods emailTemplateMethods) {
            if (userMethods.UserExists(data.Username)) {
                return false;
            }

            if (userMethods.EmailExists(data.Email)) {
                return false;
            }

            var newUser = new User {
                Username = data.Username,
                DisplayName = data.Username,
                Email = data.Email,
                Salt = UserValidation.GenerateSalt()
            };

            newUser.Password = UserValidation.HashText(data.Password, newUser.Salt, new SHA512CryptoServiceProvider());

            userMethods.CreateUser(newUser);

            if (!userMethods.UserExists(newUser.Username)) {
                return false;
            }

            var user = userMethods.GetUserByUsername(newUser.Username);

            var verification = UserValidation.GenerateSalt();

            userMethods.AddVerification(user.Id, verification);

            Email.Send("Registration", data.Email, "Verification", new Dictionary<string, string> { { "username", data.Username }, {"verification", verification}, { "url", Session.CurrentContext.Request.Url.Host} }, emailTemplateMethods);

            return true;
        }

        public static (bool password, bool verified) Login(string usernameEmail, string password, IUserMethods userMethods) {
            User user = null;

            var verified = true;
            var vPassword = true;

            if (userMethods.UserExists(usernameEmail)) {
                user = (User) userMethods.GetUserByUsername(usernameEmail);
            } else if (userMethods.EmailExists(usernameEmail)) {
                user = (User) userMethods.GetUserByEmail(usernameEmail);
            } else {
                vPassword = false;
            }

            if (!vPassword) {
                return (vPassword, verified);
            }

            if (!user.Verified) {
                verified = false;
            }

            vPassword = user.Password == UserValidation.HashText(password, user.Salt, new SHA512CryptoServiceProvider());

            if (vPassword && verified) {
                var manager = new SessionIDManager();

                var id = manager.CreateSessionID(Session.CurrentContext);

                Session.CurrentUser = user;
            }

            return (vPassword, verified);
        }

        public static void Logout() {
            Session.CurrentUser = null;

            new SessionIDManager().CreateSessionID(Session.CurrentContext);
        }
    }
}
