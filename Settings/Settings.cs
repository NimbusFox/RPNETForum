using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Hosting;
using LiteDB;
using RPNETForum.Classes;

namespace RPNETForum {
    public static class Settings {
        private static LiteDatabase _db;

        public static DatabaseTypes DatabaseType;

        public static string ReCaptchaPublic;

        public static string ReCaptchaPrivate;

        static Settings() {
            if (!Directory.Exists(HostingEnvironment.MapPath("~/App_Data/Settings"))) {
                Directory.CreateDirectory(HostingEnvironment.MapPath("~/App_Data/Settings"));
            }
            if (!File.Exists(HostingEnvironment.MapPath("~/App_Data/Settings/Settings.db3"))) {
                OpenDatabase();
                var defaultSettings = new Classes.Settings();
                defaultSettings.DatabaseType = DatabaseTypes.LiteDB;
                defaultSettings.ReCaptchaPrivate = "";
                defaultSettings.ReCaptchaPublic = "";
                var setting = _db.GetCollection<Classes.Settings>();
                setting.Insert(defaultSettings);
            } else {
                OpenDatabase();
            }

            var settings = _db.GetCollection<Classes.Settings>().FindAll().First();

            DatabaseType = settings.DatabaseType;

            ReCaptchaPrivate = settings.ReCaptchaPrivate;

            ReCaptchaPublic = settings.ReCaptchaPublic;
        }

        private static void OpenDatabase() {
            _db = new LiteDatabase(HostingEnvironment.MapPath("~/App_Data/Settings/Settings.db3"));
        }
    }
}
