using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.SessionState;
using RPNETForum.Classes.Users;

namespace RPNETForum {
    public static class UserSession {

        private static Dictionary<string, Dictionary<string, object>> sessions = new Dictionary<string, Dictionary<string, object>>();

        public static User CurrentUser {
            get {
                if (sessions.ContainsKey(HttpContext.Current.Session.SessionID)) {
                    var oldSession = sessions[HttpContext.Current.Session.SessionID];

                    foreach (var item in oldSession.Keys) {
                        HttpContext.Current.Session[item] = oldSession[item];
                    }

                    sessions.Remove(HttpContext.Current.Session.SessionID);
                }
                return (User)HttpContext.Current.Session["User"];
            }
            set => HttpContext.Current.Session["User"] = value;
        }

        public static HttpContext CurrentContext => HttpContext.Current;

        public static bool IsLoggedIn => CurrentUser != null;

        public static string PreviousURL {
            get => (string) HttpContext.Current.Session["PreviousURL"];
            set => HttpContext.Current.Session["PreviousUrl"] = value;
        }

        public static void AddTempSession(string token, HttpSessionState session) {
            if (!sessions.ContainsKey(token)) {
                var items = new Dictionary<string, object>();

                for (var i = 0; i < session.Keys.Count; i++) {
                    var key = session.Keys.Get(i);
                    var value = session[key];

                    items.Add(key, value);
                }

                sessions.Add(token, items);
            }
        }
    }
}
