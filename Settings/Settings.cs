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

        private static string _url;

        public static DatabaseTypes DatabaseType;

        public static string ReCaptchaPublic;

        public static string ReCaptchaPrivate;

        public static string SmtpHost;
        public static string SmtpUser;
        public static string SmtpPassword;

        public static int SmtpPort;

        public static string Url {
            get => _url[_url.Length - 1] == '/' ? _url.Substring(0, _url.Length - 1) : _url;
            set => _url = value;
        }

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
                defaultSettings.SmtpHost = "";
                defaultSettings.SmtpUser = "";
                defaultSettings.SmtpPassword = "";
                defaultSettings.SmtpPort = 25;
                defaultSettings.Url = "";
                var setting = _db.GetCollection<Classes.Settings>();
                setting.Insert(defaultSettings);
            } else {
                OpenDatabase();
            }

            var settings = _db.GetCollection<Classes.Settings>().FindAll().First();

            DatabaseType = settings.DatabaseType;

            ReCaptchaPrivate = settings.ReCaptchaPrivate;

            ReCaptchaPublic = settings.ReCaptchaPublic;

            SmtpHost = settings.SmtpHost;
            SmtpUser = settings.SmtpUser;
            SmtpPassword = settings.SmtpPassword;
            SmtpPort = settings.SmtpPort;

            Url = settings.Url;
        }

        private static void OpenDatabase() {
            _db = new LiteDatabase(HostingEnvironment.MapPath("~/App_Data/Settings/Settings.db3"));
        }
    }
}
