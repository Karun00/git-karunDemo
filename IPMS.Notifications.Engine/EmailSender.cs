using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using IPMS.Domain.Models;
using log4net;
using log4net.Config;
using System.Diagnostics;
using System.Globalization;
using System.Configuration;
using System.Net;

namespace IPMS.Notifications.Engine
{
    public class EmailSender : IDisposable
    {
        private string FromAddress;
        private string strSmtpClient;
        private string UserID;
        private string Password;
        private string SMTPPort;
        private Boolean bEnableSSL;
        private SmtpClient SmtpClient;
        public ILog log;

        private static int count = 0;
        private bool useDefaultCredentials;
        public EmailSender()
        {
            FromAddress = System.Configuration.ConfigurationManager.AppSettings.Get("FromAddress");
            strSmtpClient = System.Configuration.ConfigurationManager.AppSettings.Get("SmtpClient");
            UserID = System.Configuration.ConfigurationManager.AppSettings.Get("UserID");
            Password = System.Configuration.ConfigurationManager.AppSettings.Get("Password");
            SMTPPort = System.Configuration.ConfigurationManager.AppSettings.Get("SMTPPort");
            bEnableSSL = System.Configuration.ConfigurationManager.AppSettings.Get("EnableSSL").ToUpper() == "YES";
            useDefaultCredentials = System.Configuration.ConfigurationManager.AppSettings["DefaultCredentials"].ToUpper(CultureInfo.InvariantCulture) == "YES";

            log = LogManager.GetLogger(typeof(Notifier));
           
        }
        public bool SendEMail(string msgbody, string subject, string toEmailId)
        {

            FromAddress = System.Configuration.ConfigurationManager.AppSettings.Get("FromAddress");
            strSmtpClient = System.Configuration.ConfigurationManager.AppSettings.Get("SmtpClient");
            UserID = System.Configuration.ConfigurationManager.AppSettings.Get("UserID");
            Password = System.Configuration.ConfigurationManager.AppSettings.Get("Password");
            SMTPPort = System.Configuration.ConfigurationManager.AppSettings.Get("SMTPPort");
            bEnableSSL = System.Configuration.ConfigurationManager.AppSettings.Get("EnableSSL").ToUpper() == "YES";
            useDefaultCredentials = System.Configuration.ConfigurationManager.AppSettings["DefaultCredentials"].ToUpper(CultureInfo.InvariantCulture) == "YES";

            int count = 0;
            log.Info("EmailSender started...");
            SmtpClient SmtpClient;
            bool returnVal = false;
            count++;
            string emailId = toEmailId;
            if (emailId.IndexOf(";") == -1)
                emailId += ";";

            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(FromAddress);
            string[] toMailIds = emailId.Split(';');
            for (int _mailIdx = 0; _mailIdx < toMailIds.Length; _mailIdx++)
            {
                if (!string.IsNullOrEmpty(toMailIds[_mailIdx]))
                    mailMessage.To.Add(new MailAddress(toMailIds[_mailIdx]));
            }

            mailMessage.Subject = subject;
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = msgbody;
            try
            {
                string[] ccMailIds = ConfigurationManager.AppSettings["CC"].ToString().Split(';');
                for (int _mailccIdx = 0; _mailccIdx < ccMailIds.Length; _mailccIdx++)
                {
                    if (!string.IsNullOrEmpty(ccMailIds[_mailccIdx]))
                    {
                        mailMessage.CC.Add(new MailAddress(ccMailIds[_mailccIdx]));
                        log.Info("Email CC added successfully to " + ccMailIds[_mailccIdx]);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error($"Unable to assign CC, Exception : {ex.StackTrace}");

            }
            string attachmentFilename = "";

            if (!string.IsNullOrEmpty(attachmentFilename))
            {
                string[] filenames = attachmentFilename.Split(',');
                for (int i = 0; i < filenames.Length; i++)
                {
                    System.Net.Mail.Attachment attachment;
                    attachment = new System.Net.Mail.Attachment(filenames[i]);
                    mailMessage.Attachments.Add(attachment);
                }
            }

            try
            {
                SmtpClient = new SmtpClient();
                SmtpClient.Host = strSmtpClient;
                SmtpClient.Port = Convert.ToInt32(SMTPPort, CultureInfo.InvariantCulture);
                SmtpClient.EnableSsl = false;
              //  SmtpClient.Credentials = new NetworkCredential(smtpUserId, smtpPassword);
                SmtpClient.Credentials = useDefaultCredentials ? System.Net.CredentialCache.DefaultNetworkCredentials : new System.Net.NetworkCredential(UserID, Password);

                Stopwatch wfstopwatch = Stopwatch.StartNew();
                SmtpClient.Send(mailMessage);
                wfstopwatch.Stop();
                log.Debug("Time taken for SmtpClient.Send " + wfstopwatch.Elapsed.ToString());
                log.Debug("Email sent successfully to " + toEmailId + " Subject : " + subject);

                returnVal = true;
            }
            catch (SmtpFailedRecipientsException ex)
            {
                log_error("Failed to deliver message to ", ex);
                returnVal = false;
            }
            catch (Exception ex)
            {
                log_error("User defined unhandled Exception : Server " + strSmtpClient, ex);
                returnVal = false;
            }
            finally
            {
                if (mailMessage != null)
                    mailMessage.Dispose();
            }
            log.Error("EmailSender completed...");

            return returnVal;
        }


        public bool SendEMailold(string msgbody, string subject, string toEmailId)
        {
            bool returnVal = false;
            count++;
            string emailId = toEmailId;
            if (emailId.IndexOf(";") == -1)
                emailId += ";";

            MailMessage mailMessage = new MailMessage();

            mailMessage.From = new MailAddress(FromAddress);

            string[] toMailIds = emailId.Split(';');
            for (int _mailIdx = 0; _mailIdx < toMailIds.Length; _mailIdx++)
            {
                if (!string.IsNullOrEmpty(toMailIds[_mailIdx]))
                {
                    mailMessage.To.Add(new MailAddress(toMailIds[_mailIdx]));
                    log.Info("Email Sender : Email To added successfully to " + toMailIds[_mailIdx]);
                }
                   
            }

            try
            {
                string cc = ConfigurationManager.AppSettings["CC"];
                if (!string.IsNullOrWhiteSpace(cc))
                {
                    string[] ccMailIds = cc.ToString().Split(';');
                    for (int _mailccIdx = 0; _mailccIdx < ccMailIds.Length; _mailccIdx++)
                    {
                        if (!string.IsNullOrEmpty(ccMailIds[_mailccIdx]))
                        {
                            mailMessage.CC.Add(new MailAddress(ccMailIds[_mailccIdx]));
                            log.Info("Email Sender : Email CC added successfully to " + ccMailIds[_mailccIdx]);
                        }
                    }
                }
               
            }
            catch (Exception ex)
            {
                log.Error($"Unable to assign CC, Exception : {ex.StackTrace}");

            }

            mailMessage.Subject = subject;
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = msgbody;
            try
            {
                SmtpClient = new SmtpClient();
                SmtpClient.Host = strSmtpClient;
                SmtpClient.EnableSsl = bEnableSSL;
                SmtpClient.Port = Convert.ToInt32(SMTPPort, CultureInfo.InvariantCulture);
                if (bEnableSSL)
                {
                    System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;  //starttls
                    log.Debug("System.Net.SecurityProtocolType.Tls12 Intiated");
                }
                // SmtpClient.Credentials = new System.Net.NetworkCredential(UserID, Password);
                SmtpClient.Credentials = useDefaultCredentials ? System.Net.CredentialCache.DefaultNetworkCredentials : new System.Net.NetworkCredential(UserID, Password);

                Stopwatch wfstopwatch = Stopwatch.StartNew();
                SmtpClient.Send(mailMessage);
                wfstopwatch.Stop();
                log.Debug("Time taken for SmtpClient.Send " + wfstopwatch.Elapsed.ToString());

                returnVal = true;
            }
            catch (SmtpFailedRecipientsException ex)
            {
                //for (int i = 0; i < ex.InnerExceptions.Length; i++)
                //{
                //    SmtpStatusCode status = ex.InnerExceptions[i].StatusCode;
                //    if (status == SmtpStatusCode.MailboxBusy ||
                //        status == SmtpStatusCode.MailboxUnavailable)
                //    {
                //        Console.WriteLine("Delivery failed - retrying in 5 seconds.");
                //        System.Threading.Thread.Sleep(5000);
                //        SmtpClient.Send(mailMessage);
                //    }
                //    else
                //    {
                log_error("Failed to deliver message to ", ex);
                //}
                //}
            }
            catch (Exception ex)
            {
                log_error("User defined unhandled Exception", ex);
            }
            finally
            {
                if (mailMessage != null)
                    mailMessage.Dispose();
            }
            return returnVal;
        }

        private void log_error(string pretext, Exception ex)
        {
            string msg = "EMAIL SERVER ERROR: " + pretext + " " + ex.Message;
            if (ex.InnerException != null)
            {
                if (!string.IsNullOrEmpty(ex.InnerException.Message))
                {
                    msg = msg + " Inner Exception:" + ex.InnerException.Message;
                }
            }
            log.Error(msg);

        }

        #region IDisposable Methods

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    this.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
