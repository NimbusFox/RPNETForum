using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Hosting;
using SQLite;

namespace RPNETForum.DatabaseMethods.Sqlite {
    public class BaseMethods {
        public static SQLiteConnection _db;

        public BaseMethods() {
            if (_db == null) {
                _db = new SQLiteConnection(HostingEnvironment.MapPath("~/App_Data/Database.db"));
            }
        }
    }
}
