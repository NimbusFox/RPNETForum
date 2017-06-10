using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RPNETForum.Models.Users {
    public class RegisterModel {
        public string Username { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Email { get; set; }
    }

    public class RegisterResponseModel {
        public bool Username { get; set; }
        public string UsernameReason { get; set; }
        public bool Password { get; set; }
        public string PasswordReason { get; set; }
        public bool Email { get; set; }
        public string EmailReason { get; set; }
    }
}