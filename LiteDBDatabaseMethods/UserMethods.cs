using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPNETForum.Classes.Users;
using RPNETForum.Interfaces.DatabaseMethods;
using RPNETForum.Interfaces.Users;

namespace RPNETForum.DatabaseMethods.LiteDB {
    public class UserMethods : BaseMethods, IUserMethods {

        public bool UserExists(string name) {
            if (_userDB.Count() == 0) {
                return false;
            }

            return _userDB.FindAll().Any(user => string.Equals(user.Username, name, StringComparison.CurrentCultureIgnoreCase));
        }

        public bool UserExists(int id) {
            if (_userDB.Count() == 0) {
                return false;
            }
            return _userDB.Exists(x => x.Id == id);
        }

        public bool DisplayNameExists(string name) {
            if (_userDB.Count() == 0) {
                return false;
            }
            return _userDB.FindAll().Any(user => string.Equals(user.DisplayName, name, StringComparison.CurrentCultureIgnoreCase));
        }

        public bool EmailExists(string email) {
            if (_userDB.Count() == 0) {
                return false;
            }
            return _userDB.Exists(x => x.Email == email);
        }

        public bool IsVerified(string username) {
            return _userDB.FindAll().Any(user => string.Equals(user.Username, username, StringComparison.CurrentCultureIgnoreCase) && user.Verified);
        }

        public bool IsVerified(int uid) {
            return _userDB.FindOne(x => x.Id == uid).Verified;
        }

        public bool IsValidSession(string token) {
            if (_sessionDB.Count() == 0) {
                return false;
            }
            return _sessionDB.Exists(x => x.SessionToken == token);
        }

        public void AddVerification(int uid, string token) {
            var verification = new Verification {
                UID = uid,
                VerificationToken = token
            };

            _verificationDB.Insert(verification);
        }

        public bool Verify(string token) {
            if (_verificationDB.Exists(x => x.VerificationToken == token)) {
                var verification = _verificationDB.FindOne(x => x.VerificationToken == token);

                if (_userDB.Exists(x => x.Id == verification.UID)) {
                    var user = _userDB.FindOne(x => x.Id == verification.UID);

                    user.Verified = true;

                    _userDB.Update(user);

                    _verificationDB.Delete(x => x.VerificationToken == token);

                    return true;
                }

                return false;
            }

            return false;
        }

        public void CreateUser(IUser user) {
            _userDB.Insert((User)user);
        }

        public void UpdateUser(IUser user) {
            if (_userDB.Exists(x => x.Id == user.Id)) {
                _userDB.Update((User) user);
            }
        }

        public void RemoveUser(IUser user) {
            if (_userDB.Exists(x => x.Id == user.Id)) {
                _userDB.Delete(x => x.Id == user.Id);
            }
        }

        public int CountUsers() {
            return _userDB.Count();
        }

        public void AddSession(int uid, string token) {
            _sessionDB.Insert(new Session {SessionToken = token, UID = uid});
        }

        public void RemoveSession(string token) {
            if (IsValidSession(token)) {
                _sessionDB.Delete(x => x.SessionToken == token);
            }
        }

        public IUser GetLastUser() {
            return _userDB.FindAll().Last();
        }

        public IUser GetUserByID(int uid) {
            return !UserExists(uid) ? null : _userDB.FindOne(x => x.Id == uid);
        }

        public IUser GetUserByUsername(string username) {
            return !UserExists(username) ? null : _userDB.FindAll().First(x => x.Username.ToLower() == username.ToLower());
        }

        public IUser GetUserByEmail(string email) {
            return !EmailExists(email) ? null : _userDB.FindOne(x => x.Email == email);
        }

        public IUser GetUserBySession(string token) {
            if (!IsValidSession(token)) {
                return null;
            }

            var uid = _sessionDB.FindOne(x => x.SessionToken == token).UID;

            return !UserExists(uid) ? null : _userDB.FindOne(x => x.Id == uid);
        }
    }
}
