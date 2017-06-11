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
            return _userDB.Exists(x => string.Equals(x.Username, name, StringComparison.CurrentCultureIgnoreCase));
        }

        public bool UserExists(int id) {
            if (_userDB.Count() == 0) {
                return false;
            }
            return _userDB.Exists(x => x.UID == id);
        }

        public bool DisplayNameExists(string name) {
            if (_userDB.Count() == 0) {
                return false;
            }
            return _userDB.Exists(x => x.Username == name);
        }

        public bool EmailExists(string email) {
            if (_userDB.Count() == 0) {
                return false;
            }
            return _userDB.Exists(x => x.Email == email);
        }

        public bool IsVerified(string username) {
            return _userDB.FindOne(x => x.Username == username).Verified;
        }

        public bool IsVerified(int uid) {
            return _userDB.FindOne(x => x.UID == uid).Verified;
        }

        public void AddVerification(int uid, string token) {
            var verification = new Verification {
                UID = uid,
                VerificationToken = token
            };

            _verificationDB.Insert(verification);
        }

        public void Verify(string token) {
            if (_verificationDB.Exists(x => x.VerificationToken == token)) {
                var verification = _verificationDB.FindOne(x => x.VerificationToken == token);

                if (_userDB.Exists(x => x.UID == verification.UID)) {
                    var user = _userDB.FindOne(x => x.UID == verification.UID);

                    user.Verified = true;

                    _userDB.Update(user);

                    _verificationDB.Delete(x => x.VerificationToken == token);
                }
            }
        }

        public void CreateUser(IUser user) {
            if (!_userDB.Exists(x => x.UID == user.UID)) {
                _userDB.Insert((User)user);
            }
        }

        public void UpdateUser(IUser user) {
            if (_userDB.Exists(x => x.UID == user.UID)) {
                _userDB.Update((User) user);
            }
        }

        public void RemoveUser(IUser user) {
            if (_userDB.Exists(x => x.UID == user.UID)) {
                _userDB.Delete(x => x.UID == user.UID);
            }
        }

        public int CountUsers() {
            return _userDB.Count();
        }

        public IUser GetLastUser() {
            return _userDB.FindAll().Last();
        }

        public IUser GetCurrentUser(string token) {
            if (!_sessionDB.Exists(x => x.SessionToken == token)) {
                return null;
            }

            var uid = _sessionDB.FindOne(x => x.SessionToken == token).UID;

            return !_userDB.Exists(x => x.UID == uid) ? null : _userDB.FindOne(x => x.UID == uid);
        }

        public IUser GetUserByID(int uid) {
            if (!_userDB.Exists(x => x.UID == uid)) {
                return null;
            }

            return _userDB.FindOne(x => x.UID == uid);
        }

        public IUser GetUserByUsername(string username) {
            if (!_userDB.Exists(x => x.Username == username)) {
                return null;
            }

            return _userDB.FindOne(x => x.Username == username);
        }
    }
}
