using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using IPMS.Domain.Models;


namespace IPMS.Notifications
{
    public class EmailSender
    {
        private string FromAddress;
        private string strSmtpClient;
        private string UserID;
        private string Password;
        private string ReplyTo;
        private string SMTPPort;
        private Boolean bEnableSSL;
        private SmtpClient SmtpClient;

        public EmailSender()
        {
            FromAddress = System.Configuration.ConfigurationManager.AppSettings.Get("FromAddress");
            strSmtpClient = System.Configuration.ConfigurationManager.AppSettings.Get("SmtpClient");
            UserID = System.Configuration.ConfigurationManager.AppSettings.Get("UserID");
            Password = System.Configuration.ConfigurationManager.AppSettings.Get("Password");
            SMTPPort = System.Configuration.ConfigurationManager.AppSettings.Get("SMTPPort");
            if (System.Configuration.ConfigurationManager.AppSettings.Get("EnableSSL").ToUpper() == "YES")
            {
                bEnableSSL = true;
            }
            else
            {
                bEnableSSL = false;
            }
            SmtpClient = new SmtpClient();
            SmtpClient.Host = strSmtpClient;
            SmtpClient.EnableSsl = bEnableSSL;
            SmtpClient.Port = Convert.ToInt32(SMTPPort);
            SmtpClient.Credentials = new System.Net.NetworkCredential(UserID, Password);

        }

        public void SendEMail(string msgbody, string subject, string toEmailId)
        {

            string emailId = toEmailId;
            if (emailId.IndexOf(";") == -1)
                emailId += ";";

            dynamic MailMessage = new MailMessage();
            MailMessage.From = new MailAddress(FromAddress);

            string[] toMailIds = emailId.Split(';');
            for (int _mailIdx = 0; _mailIdx < toMailIds.Length; _mailIdx++)
            {
                if (!string.IsNullOrEmpty(toMailIds[_mailIdx]))
                    MailMessage.To.Add(new MailAddress(toMailIds[_mailIdx]));
            }


            MailMessage.Subject = subject;
            MailMessage.IsBodyHtml = true;
            MailMessage.Body = msgbody;
            try
            {
                SmtpClient.Send(MailMessage);
            }
            catch (SmtpFailedRecipientsException ex)
            {
                for (int i = 0; i <= ex.InnerExceptions.Length; i++)
                {
                    SmtpStatusCode status = ex.InnerExceptions[i].StatusCode;
                    if ((status == SmtpStatusCode.MailboxBusy) || (status == SmtpStatusCode.MailboxUnavailable))
                    {
                        System.Threading.Thread.Sleep(5000);
                        //TODO: If exception happens here...?
                        SmtpClient.Send(MailMessage);
                    }
                }
            }
        }

    }
}
