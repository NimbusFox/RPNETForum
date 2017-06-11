using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Hosting;
using LiteDB;
using RPNETForum.Classes.Users;

namespace RPNETForum.DatabaseMethods.LiteDB {
    public class BaseMethods {
        protected LiteDatabase _db;
        protected LiteCollection<User> _userDB;
        protected LiteCollection<Session> _sessionDB;
        protected LiteCollection<Verification> _verificationDB;

        public BaseMethods() {
            if (!Directory.Exists(HostingEnvironment.MapPath("~/App_Data/Database"))) {
                Directory.CreateDirectory(HostingEnvironment.MapPath("~/App_Data/Database"));
            }

            _db = new LiteDatabase(HostingEnvironment.MapPath("~/App_Data/Database/Database.db"));

            _userDB = _db.GetCollection<User>("Users");
            _sessionDB = _db.GetCollection<Session>("Sessions");
            _verificationDB = _db.GetCollection<Verification>("Verifications");
        }
    }
}
