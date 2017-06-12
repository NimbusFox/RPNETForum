using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPNETForum.Interfaces.DatabaseMethods {
    public interface IEmailTemplateMethods {
        bool CreateTemplate(string name, string template);
        bool TemplateExists(string name);
        List<string> GetTemplates();
        string GetTemplate(string name);
        int GetTemplateCount();
    }
}
