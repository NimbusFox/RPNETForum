using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPNETForum.Classes {
    public enum DatabaseTypes {
        LiteDB,
        MySql,
        Sqlite
    }

    public class Settings {
        public DatabaseTypes DatabaseType { get; set; }
        public string ReCaptchaPublic { get; set; }
        public string ReCaptchaPrivate { get; set; }
        public string SmtpHost { get; set; }
        public string SmtpUser { get; set; }
        public string SmtpPassword { get; set; }
        public int SmtpPort { get; set; }
        public string Url { get; set; }
    }
}
