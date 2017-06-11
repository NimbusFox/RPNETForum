using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPNETForum.Interfaces.DatabaseMethods {
    public interface IUserMethods {
        bool UserExists(string name);
        bool UserExists(int id);
        bool DisplayNameExists(string name);
        bool EmailExists(string email);
        bool IsVerified(string username);
        bool IsVerified(int uid);
        void AddVerification(int uid, string token);
        void Verify(string troken);
        void CreateUser(IUser user);
        void UpdateUser(IUser user);
        void RemoveUser(IUser user);
        int CountUsers();
        IUser GetLastUser();
        IUser GetCurrentUser(string token);
        IUser GetUserByID(int uid);
        IUser GetUserByUsername(string username);
    }

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
