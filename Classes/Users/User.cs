using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPNETForum.Interfaces.DatabaseMethods;
using RPNETForum.Interfaces.Users;

namespace RPNETForum.Classes.Users {
    public class User : IUser {
        public int Id { get; set; }
        public string Username { get; set; }
        public string DisplayName { get; set; }
        public string Salt { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public bool Verified { get; set; }
    }

    public class Session : ISession {
        public string SessionToken { get; set; }
        public int UID { get; set; }
    }

    public class Verification : IVerification {
        public string VerificationToken { get; set; }
        public int UID { get; set; }
    }
}
