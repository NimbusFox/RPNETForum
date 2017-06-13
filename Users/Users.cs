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
using RPNETForum.Interfaces.Users;
using RPNETForum.Validation;

namespace RPNETForum {
    public class Users {

        private static IUserMethods _userMethods;
        private static IEmailTemplateMethods _emailTemplateMethods;

        public Users(IUserMethods userMethods, IEmailTemplateMethods emailTemplateMethods) {
            _userMethods = userMethods;
            _emailTemplateMethods = emailTemplateMethods;
        }

        public static bool Create(RegisterModel data) {
            if (_userMethods.UserExists(data.Username)) {
                return false;
            }

            if (_userMethods.EmailExists(data.Email)) {
                return false;
            }

            var newUser = new User {
                Username = data.Username,
                DisplayName = data.Username,
                Email = data.Email,
                Salt = UserValidation.GenerateSalt(),
                HasGravatar = false,
                ProfilePic = ""
            };

            newUser.Password = UserValidation.HashText(data.Password, newUser.Salt, new SHA512CryptoServiceProvider());

            _userMethods.CreateUser(newUser);

            if (!_userMethods.UserExists(newUser.Username)) {
                return false;
            }

            var user = _userMethods.GetUserByUsername(newUser.Username);

            var verification = UserValidation.GenerateSalt();

            _userMethods.AddVerification(user.Id, verification);

            Email.Send("Registration", data.Email, "Verification", new Dictionary<string, string> { { "username", data.Username }, { "verification", verification }, { "url", Settings.Url } }, _emailTemplateMethods);

            return true;
        }

        public static (bool password, bool verified) Login(string usernameEmail, string password) {
            User user = null;

            var verified = true;
            var vPassword = true;

            if (_userMethods.UserExists(usernameEmail)) {
                user = (User)_userMethods.GetUserByUsername(usernameEmail);
            } else if (_userMethods.EmailExists(usernameEmail)) {
                user = (User)_userMethods.GetUserByEmail(usernameEmail);
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

                bool redirected;
                bool isAdded;

                var oldID = UserSession.CurrentContext.Session.SessionID;

                var id = manager.CreateSessionID(UserSession.CurrentContext);

                UserSession.CurrentUser = user;

                var oldDate = user.LastLogin;

                user.LastLogin = DateTime.UtcNow;

                _userMethods.UpdateUser(user);

                user.LastLogin = oldDate;

                UserSession.AddTempSession(id, UserSession.CurrentContext.Session);

                manager.RemoveSessionID(UserSession.CurrentContext);
                manager.SaveSessionID(UserSession.CurrentContext, id, out redirected, out isAdded);

                for (var i = 0; i < UserSession.CurrentContext.Response.Cookies.Count; i++) {
                    var cookie = UserSession.CurrentContext.Response.Cookies.Get(i);
                    if (cookie != null && cookie.Value == id) {
                        var current = cookie;

                        current.Expires = DateTime.Now.AddMonths(2);

                        UserSession.CurrentContext.Response.Cookies.Remove(current.Name);
                        UserSession.CurrentContext.Response.Cookies.Add(current);
                    }
                }
            }

            return (vPassword, verified);
        }

        public static void Logout() {
            UserSession.CurrentUser = null;

            var manager = new SessionIDManager();

            bool redirected;
            bool isAdded;

            var id = manager.CreateSessionID(UserSession.CurrentContext);
            manager.RemoveSessionID(UserSession.CurrentContext);
            manager.SaveSessionID(UserSession.CurrentContext, id, out redirected, out isAdded);
        }

        public static bool DeleteUser(IUserMethods userMethods, User user = null) {
            if (user == null) {
                user = UserSession.CurrentUser;
            }

            try {
                userMethods.RemoveUser(user);

                UserSession.CurrentUser = null;

                var session = new SessionIDManager();

                session.RemoveSessionID(UserSession.CurrentContext);

                return true;
            } catch {
                return false;
            }
        }
    }

    
}
