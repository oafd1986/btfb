using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Text;

namespace btfb.Helpers
{
    /// <summary>
    /// Class to send email
    /// </summary>
    public class Email
    {
        public string mailSubject { get; set; }
        public StringBuilder msgbody { get; set; }
        public string fromAddress { get; set; }
        public string toAddresses { get; set; }
        string vsmtpHost { get; set; }
        string vsendUserid { get; set; }
        string vsendPassword { get; set; }

        public void SendEmail()
        {
            if(fromAddress == string.Empty || fromAddress == null)
            {
                fromAddress = System.Configuration.ConfigurationManager.AppSettings["fromAddress"].ToString();
            }
            if(toAddresses == string.Empty || toAddresses == null)
            {
                toAddresses = System.Configuration.ConfigurationManager.AppSettings["toAddresses"].ToString();
            }
            if (vsendPassword == string.Empty || vsendPassword == null)
            {
                vsendPassword = System.Configuration.ConfigurationManager.AppSettings["vsendPassword"].ToString();
            }
            if (vsendUserid == string.Empty || vsendUserid == null)
            {
                vsendUserid = System.Configuration.ConfigurationManager.AppSettings["vsendUserid"].ToString();
            }
            if (vsmtpHost == string.Empty || vsmtpHost == null)
            {
                vsmtpHost = System.Configuration.ConfigurationManager.AppSettings["vsmtpHost"].ToString();
            }
            if (toAddresses == string.Empty || toAddresses == null)
            {
                toAddresses = System.Configuration.ConfigurationManager.AppSettings["toAddresses"].ToString();
            }

            MailMessage mail = new MailMessage();

            string[] toAddressesSplitted = toAddresses.Split(',').Select(sValue => sValue.Trim()).ToArray();

            foreach (string value in toAddressesSplitted)
            {
                mail.To.Add(value);
            }
            mail.From = new MailAddress(fromAddress);
            mail.Subject = mailSubject;
            mail.Body = msgbody.ToString();
            mail.IsBodyHtml = false;
            mail.Priority = MailPriority.High;
            mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

            SmtpClient smtp = new SmtpClient();
            smtp.Host = vsmtpHost;

            smtp.Port = 587;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.EnableSsl = true;
            System.Net.NetworkCredential smtpUserInfo = new System.Net.NetworkCredential(vsendUserid, vsendPassword);
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = smtpUserInfo;

            smtp.Send(mail);
        }


    }
}