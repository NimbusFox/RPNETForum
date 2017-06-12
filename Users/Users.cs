﻿using System;
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

                var id = manager.CreateSessionID(UserSession.CurrentContext);

                UserSession.CurrentUser = user;
            }

            return (vPassword, verified);
        }

        public static void Logout() {
            UserSession.CurrentUser = null;

            new SessionIDManager().CreateSessionID(UserSession.CurrentContext);
        }

        public static bool DeleteUser(IUserMethods userMethods, User user = null) {
            if (user == null) {
                user = UserSession.CurrentUser;
            }

            try {
                userMethods.RemoveUser(user);

                UserSession.CurrentUser = null;

                new SessionIDManager().CreateSessionID(UserSession.CurrentContext);

                return true;
            } catch {
                return false;
            }
        }

        public static string GenereateGravatarByEmail(string email, bool hasGravatar = false, string gravatarType = "retro") {
            var hash = UserValidation.HashText(email, "", new MD5CryptoServiceProvider());

            return "https://www.gravatar.com/avatar/" + hash + (!hasGravatar ? "?d=" + gravatarType + "&s=100" : "?s=100");
        }
    }

    public static class UserExtensions {
        public static string GetProfilePic(this IUser user) {
            if (user.HasGravatar || string.IsNullOrWhiteSpace(user.ProfilePic)) {
                return Users.GenereateGravatarByEmail(user.Email, user.HasGravatar);
            }
            var url = Settings.Url[Settings.Url.Length - 1] == '/' ? "avatars/" : "/avatars/";

            url += user.ProfilePic;

            return Settings.Url + url;
        }
    }
}
