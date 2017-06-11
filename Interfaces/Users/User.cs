using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPNETForum.Interfaces.Users {
    public interface IUser {
        int UID { get; set; }
        string Username { get; set; }
        string DisplayName { get; set; }
        string Salt { get; set; }
        string Password { get; set; }
        string Email { get; set; }
        bool Verified { get; set; }
    }

    public interface ISession {
        string SessionToken { get; set; }
        int UID { get; set; }
    }

    public interface IVerification {
        string VerificationToken { get; set; }
        int UID { get; set; }
    }
}
