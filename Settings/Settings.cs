using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Hosting;
using RPNETForum.Classes;
using SQLite;

namespace RPNETForum {
    public static class Settings {
        private static SQLiteConnection _db;

        public static DatabaseTypes DatabaseType;

        static Settings() {
            if (!File.Exists(HostingEnvironment.MapPath("~/App_Data/Settings.db"))) {
                OpenDatabase();
                _db.CreateTable<Classes.Settings>();
                var defaultSettings = new Classes.Settings();
                defaultSettings.DatabaseType = DatabaseTypes.Sqlite;
                _db.Insert(defaultSettings);
            } else {
                OpenDatabase();
            }

            var settings = _db.Table<Classes.Settings>().First();

            DatabaseType = settings.DatabaseType;
        }

        private static void OpenDatabase() {
            _db = new SQLiteConnection(HostingEnvironment.MapPath("~/App_Data/Settings.db"));
        }
    }
}
