using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using RPNETForum.Classes.Users;

namespace RPNETForum {
    public static class UserSession {
        public static User CurrentUser {
            get => (User)HttpContext.Current.Session["User"];
            set => HttpContext.Current.Session["User"] = value;
        }

        public static HttpContext CurrentContext => HttpContext.Current;

        public static bool IsLoggedIn => CurrentUser != null;
    }
}
