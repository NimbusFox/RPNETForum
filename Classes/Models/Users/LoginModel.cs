using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace RPNETForum.Classes.Models.Users {
    public class LoginModel {
        public string Username { get; set; }
        public string Password { get; set; }

        public LoginModel() { }

        public LoginModel(HttpRequestBase request) {
            Username = request["username"];
            Password = request["password"];
        }
    }
}
