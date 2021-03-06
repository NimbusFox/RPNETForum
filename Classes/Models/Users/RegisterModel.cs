﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RPNETForum.Classes.Models.Users {
    public class RegisterModel {
        public string Username { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Email { get; set; }
    }

    public class RegisterResponseModel {
        public Response Username { get; set; }
        public Response Password { get; set; }
        public Response Email { get; set; }
        public Response ReCaptcha { get; set; }

        public RegisterResponseModel() {
            Username = new Response();
            Password = new Response();
            Email = new Response();
            ReCaptcha = new Response();
        }

        public bool IsValidRegistration() {
            return Username.Success && Password.Success && Email.Success && ReCaptcha.Success;
        }
    }

    public class Response {
        public bool Success { get; set; }
        public string Reason { get; set; }
    }
}