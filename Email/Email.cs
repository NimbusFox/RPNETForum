using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace RPNETForum {
    public static class Email {
        public static void Send(string subject, string toAddress, string template,
            Dictionary<string, string> templateVariables) {
            var client = new SmtpClient(Settings.SmtpHost, Settings.SmtpPort);

            client.Credentials = new NetworkCredential(Settings.SmtpUser, Settings.SmtpPassword);

            var from = new MailAddress(Settings.SmtpUser);

            var to = new MailAddress(toAddress);

            var message = new MailMessage(from, to);

            message.IsBodyHtml = true;

            var body = template;

            foreach (var identifier in templateVariables.Keys) {
                if (body.Contains("{" + identifier + "}")) {
                    body = body.Replace("{" + identifier + "}", templateVariables[identifier]);
                }
            }

            message.Body = body;
            message.BodyEncoding = Encoding.UTF8;
            message.Subject = subject;
            message.SubjectEncoding = Encoding.UTF8;

            client.Send(message);
        }
    }
}
