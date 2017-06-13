using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPNETForum.Interfaces.Users;

namespace RPNETForum.Interfaces.DatabaseMethods {
    public interface IUserMethods {
        bool UserExists(string name);
        bool UserExists(int id);
        bool DisplayNameExists(string name);
        bool EmailExists(string email);
        bool IsVerified(string username);
        bool IsVerified(int uid);
        bool Verify(string token);
        bool IsValidSession(string token);
        void AddVerification(int uid, string token);
        void CreateUser(IUser user);
        void UpdateUser(IUser user);
        void RemoveUser(IUser user);
        int CountUsers();
        void AddSession(int uid, string token);
        void RemoveSession(string token);
        IUser GetLastUser();
        IUser GetUserByID(int uid);
        IUser GetUserByUsername(string username);
        IUser GetUserByEmail(string email);
        IUser GetUserBySession(string token);
    }
}
