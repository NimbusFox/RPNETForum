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
        void AddVerification(int uid, string token);
        bool Verify(string token);
        void CreateUser(IUser user);
        void UpdateUser(IUser user);
        void RemoveUser(IUser user);
        int CountUsers();
        IUser GetLastUser();
        IUser GetUserByID(int uid);
        IUser GetUserByUsername(string username);
        IUser GetUserByEmail(string email);
    }
}
