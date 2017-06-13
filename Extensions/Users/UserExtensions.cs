using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using RPNETForum.Interfaces.Users;
using RPNETForum.Validation;

namespace RPNETForum.Extensions.Users {
    public static class UserExtensions {
        public static string GetProfilePic(this IUser user) {
            if (user.HasGravatar || string.IsNullOrWhiteSpace(user.ProfilePic)) {
                return GenereateGravatarByEmail(user.Email, user.HasGravatar);
            }
            var url = Settings.Url[Settings.Url.Length - 1] == '/' ? "avatars/" : "/avatars/";

            url += user.ProfilePic;

            return Settings.Url + url;
        }

        private static string GenereateGravatarByEmail(string email, bool hasGravatar = false, string gravatarType = "retro") {
            var hash = UserValidation.HashText(email, "", new MD5CryptoServiceProvider());

            return "https://www.gravatar.com/avatar/" + hash + (!hasGravatar ? "?d=" + gravatarType + "&s=100" : "?s=100");
        }
    }
}
