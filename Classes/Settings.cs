using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPNETForum.Classes {
    public enum DatabaseTypes {
        LiteDB,
        Sqlite,
        MySql
    }

    public class Settings {
        public DatabaseTypes DatabaseType { get; set; }
        public string ReCaptchaPublic { get; set; }
        public string ReCaptchaPrivate { get; set; }
    }
}
