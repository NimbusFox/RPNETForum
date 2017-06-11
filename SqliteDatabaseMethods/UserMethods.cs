using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using RPNETForum.Interfaces.DatabaseMethods;
using SQLite;

namespace RPNETForum.DatabaseMethods.Sqlite {
    public class UserMethods : BaseMethods, IUserMethods {

        private class UserDB : IUser {
            [PrimaryKey]
            [AutoIncrement]
            public int UID { get; set; }
            [Unique]
            public string Username { get; set; }
            [Unique]
            public string DisplayName { get; set; }
            public string Salt { get; set; }
            public string Password { get; set; }
            [Unique]
            public string Email { get; set; }
            public bool Verified { get; set; }
        }

        private class SessionDB: ISession {
            public string SessionToken { get; set; }
            public int UID { get; set; }
        }

        private class VerificationDB: IVerification {
            public string VerificationToken { get; set; }
            public int UID { get; set; }
        }

        public UserMethods() : base() {
            _db.CreateTable<UserDB>();
            _db.CreateTable<SessionDB>();
            _db.CreateTable<VerificationDB>();
        }

        public bool UserExists(string username) {
            return _db.Table<UserDB>().Any(x => string.Equals(x.Username, username, StringComparison.CurrentCultureIgnoreCase));
        }

        public bool UserExists(int id) {
            return _db.Table<UserDB>().Any(x => x.UID == id);
        }

        public bool DisplayNameExists(string name) {
            return _db.Table<UserDB>().Any(x => x.DisplayName == name);
        }

        public bool EmailExists(string email) {
            return _db.Table<UserDB>().Any(x => x.Email == email);
        }

        public bool IsVerified(string username) {
            return _db.Table<UserDB>()
                .First(x => string.Equals(x.Username, username, StringComparison.CurrentCultureIgnoreCase)).Verified;
        }

        public bool IsVerified(int uid) {
            return _db.Table<UserDB>().First(x => x.UID == uid).Verified;
        }

        public void AddVerification(int uid, string token) {
            var verification = new VerificationDB {
                UID = uid,
                VerificationToken = token
            };

            _db.Insert(verification);
        }

        public void Verify(string token) {
            if (_db.Table<VerificationDB>().Any(x => x.VerificationToken == token)) {
                var user = _db.Table<UserDB>()
                    .First(x => x.UID == _db.Table<VerificationDB>().First(y => y.VerificationToken == token).UID);

                user.Verified = true;

                _db.Update(user);

                _db.Delete(_db.Table<VerificationDB>().First(y => y.VerificationToken == token));
            }
        }

        public void CreateUser(IUser user) {
            _db.Insert((UserDB)user);
        }

        public void UpdateUser(IUser user) {
            _db.Update((UserDB)user);
        }

        public void RemoveUser(IUser user) {
            _db.Delete((UserDB)user);
        }

        public int CountUsers() {
            return _db.Table<UserDB>().Count();
        }

        public IUser GetLastUser() {
            return _db.Table<UserDB>().Last();
        }

        public IUser GetCurrentUser(string token) {
            if (!_db.Table<SessionDB>().Any(x => x.SessionToken == token)) {
                return null;
            }

            var sessionUID = _db.Table<SessionDB>().First(x => x.SessionToken == token).UID;

            return _db.Table<UserDB>().First(x => x.UID == sessionUID);
        }

        public IUser GetUserByID(int uid) {
            if (!_db.Table<UserDB>().Any(x => x.UID == uid)) {
                return null;
            }

            return _db.Table<UserDB>().First(x => x.UID == uid);
        }

        public IUser GetUserByUsername(string username) {
            if (!_db.Table<UserDB>().Any(x => x.Username == username)) {
                return null;
            }

            return _db.Table<UserDB>().First(x => x.Username == username);
        }
    }
}
