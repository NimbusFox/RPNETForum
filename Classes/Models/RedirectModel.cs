using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPNETForum.Classes.Models {
    public class RedirectModel {
        public string Title { get; set; }
        public string Message { get; set; }
        public string RedirectTo { get; set; }
        public int RedirectSeconds { get; set; }
    }
}
