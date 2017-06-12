using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Newtonsoft.Json;

namespace RPNETForum.Validation {
    public static class UserValidation {
        public static bool IsValidEmail(string email) {
            // https://stackoverflow.com/a/16168103
            return Regex.IsMatch(email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
        }

        private static Random random = new Random();
        public static string GenerateSalt() {
            var length = random.Next(8, 64);
            // https://stackoverflow.com/a/1344242
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        // https://www.geekytidbits.com/one-way-hashing/
        public static string HashText(string text, string salt, HashAlgorithm hasher) {
            byte[] textWithSaltBytes = Encoding.UTF8.GetBytes(string.Concat(text, salt));
            byte[] hashedBytes = hasher.ComputeHash(textWithSaltBytes);
            hasher.Clear();
            return Convert.ToBase64String(hashedBytes);
        }

        public static bool IsValidPassword(string password) {
            if (password.Length < 6) {
                return false;
            }

            if (!Regex.IsMatch(password, @"^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[\W_]).{6,}$")) {
                return false;
            }

            return true;
        }

        public static bool IsValidReCaptcha(string response) {
            //https://code.msdn.microsoft.com/Google-reCAPTCHA-in-ASPNET-f854c476
            try {
                var client = new WebClient();
                var result =
                    client.DownloadString(string.Format(
                        "https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}",
                        Settings.ReCaptchaPrivate, response));
                return (bool) JsonConvert.DeserializeObject<Dictionary<string, object>>(result)["success"];
            } catch {
                return false;
            }
        }
    }
}
