using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPNETForum.Interfaces.DatabaseMethods;
using RPNETForum.Interfaces.Users;

namespace RPNETForum.DatabaseMethods.MySql {
    public class UserMethods : BaseMethods, IUserMethods {
        public bool UserExists(string name) {
            return true;
        }

        public bool UserExists(int id) {
            return true;
        }

        public bool DisplayNameExists(string name) {
            throw new NotImplementedException();
        }

        public bool EmailExists(string email) {
            throw new NotImplementedException();
        }

        public bool IsVerified(string username) {
            throw new NotImplementedException();
        }

        public bool IsVerified(int uid) {
            throw new NotImplementedException();
        }

        public bool IsValidSession(string token) {
            throw new NotImplementedException();
        }

        public void AddVerification(int uid, string token) {
            throw new NotImplementedException();
        }

        public bool Verify(string troken) {
            throw new NotImplementedException();
        }

        public void CreateUser(IUser user) {
            throw new NotImplementedException();
        }

        public void UpdateUser(IUser user) {
            throw new NotImplementedException();
        }

        public void RemoveUser(IUser user) {
            throw new NotImplementedException();
        }

        public int CountUsers() {
            throw new NotImplementedException();
        }

        public void AddSession(int uid, string token) {
            throw new NotImplementedException();
        }

        public void RemoveSession(string token) {
            throw new NotImplementedException();
        }

        public IUser GetLastUser() {
            throw new NotImplementedException();
        }

        public IUser GetCurrentUser(string token) {
            throw new NotImplementedException();
        }

        public IUser GetUserByID(int uid) {
            throw new NotImplementedException();
        }

        public IUser GetUserByUsername(string username) {
            throw new NotImplementedException();
        }

        public IUser GetUserByEmail(string email) {
            throw new NotImplementedException();
        }

        public IUser GetUserBySession(string token) {
            throw new NotImplementedException();
        }
    }
}
