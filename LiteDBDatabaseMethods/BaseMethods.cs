using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Hosting;
using LiteDB;
using RPNETForum.Classes;
using RPNETForum.Classes.Forum;
using RPNETForum.Classes.Users;

namespace RPNETForum.DatabaseMethods.LiteDB {
    public class BaseMethods {
        protected LiteDatabase _db;
        protected LiteCollection<User> _userDB;
        protected LiteCollection<Verification> _verificationDB;
        protected LiteCollection<Session> _sessionDB;
        protected LiteCollection<EmailTemplate> _emailTemplateDB;
        protected LiteCollection<Category> _categoryDB;
        protected LiteCollection<Forum> _forumDB;
        protected LiteCollection<Thread> _threadDB;
        protected LiteCollection<Post> _postDB;

        public BaseMethods() {
            if (!Directory.Exists(HostingEnvironment.MapPath("~/App_Data/Database"))) {
                Directory.CreateDirectory(HostingEnvironment.MapPath("~/App_Data/Database"));
            }

            _db = new LiteDatabase(HostingEnvironment.MapPath("~/App_Data/Database/Database.db3"));

            _userDB = _db.GetCollection<User>("Users");
            _verificationDB = _db.GetCollection<Verification>("Verifications");
            _sessionDB = _db.GetCollection<Session>("Sessions");
            _emailTemplateDB = _db.GetCollection<EmailTemplate>("EmailTemplates");
            _categoryDB = _db.GetCollection<Category>("Categories");
            _forumDB = _db.GetCollection<Forum>("Forums");
            _threadDB = _db.GetCollection<Thread>("Threads");
            _postDB = _db.GetCollection<Post>("Posts");
        }
    }
}
