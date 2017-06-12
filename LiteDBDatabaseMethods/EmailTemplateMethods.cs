using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPNETForum.Classes;
using RPNETForum.Interfaces.DatabaseMethods;

namespace RPNETForum.DatabaseMethods.LiteDB {
    public class EmailTemplateMethods : BaseMethods, IEmailTemplateMethods {

        public EmailTemplateMethods() {
            if (GetTemplateCount() == 0) {
                CreateTemplate("Verification", "Hello {username},<br/><br/>Click <a href=\"http://{url}/Verify/{verification}\">here</a> to verify your account");
            }
        }

        public bool CreateTemplate(string name, string template) {
            if (TemplateExists(name)) {
                return false;
            }

            var templateV = new EmailTemplate {
                Name = name,
                Template = template
            };
            _emailTemplateDB.Insert(templateV);
            return true;
        }

        public bool TemplateExists(string name) {
            return _emailTemplateDB.Exists(x => x.Name == name);
        }

        public List<string> GetTemplates() {
            return _emailTemplateDB.FindAll().Select(template => template.Name).ToList();
        }

        public string GetTemplate(string name) {
            return !TemplateExists(name) ? "" : _emailTemplateDB.FindOne(x => x.Name == name).Template;
        }

        public int GetTemplateCount() {
            return _emailTemplateDB.Count();
        }
    }
}
