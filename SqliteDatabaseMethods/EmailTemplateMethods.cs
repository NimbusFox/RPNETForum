using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPNETForum.Interfaces.DatabaseMethods;

namespace RPNETForum.DatabaseMethods.Sqlite {
    public class EmailTemplateMethods : BaseMethods, IEmailTemplateMethods {
        public bool CreateTemplate(string name, string template) {
            throw new NotImplementedException();
        }

        public bool TemplateExists(string name) {
            throw new NotImplementedException();
        }

        public List<string> GetTemplates() {
            throw new NotImplementedException();
        }

        public string GetTemplate(string name) {
            throw new NotImplementedException();
        }

        public int GetTemplateCount() {
            throw new NotImplementedException();
        }
    }
}
