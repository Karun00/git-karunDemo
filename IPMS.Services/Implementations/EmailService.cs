using Core.Repository;
using IPMS.Data.Context;
using IPMS.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Net.Mail;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall,
                       ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class EmailService : ServiceBase, IEmailService
    {
        private readonly IUnitOfWork _unitOfWork;

        public EmailService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public EmailService()
        {
            _unitOfWork = new UnitOfWork(new TnpaContext());
        }
        private string FromAddress;
        private string strSmtpClient;
        private string UserID;
        private string Password;
      //  private string ReplyTo;
        private string SMTPPort;
        private Boolean bEnableSSL;
        private bool useDefaultCredentials;
        private void InitMail()
        {

            FromAddress = System.Configuration.ConfigurationManager.AppSettings["FromAddress"];
            strSmtpClient = System.Configuration.ConfigurationManager.AppSettings["SmtpClient"];
            UserID = System.Configuration.ConfigurationManager.AppSettings["UserID"];
            Password = System.Configuration.ConfigurationManager.AppSettings["Password"];
            SMTPPort = System.Configuration.ConfigurationManager.AppSettings["SMTPPort"];
            if (System.Configuration.ConfigurationManager.AppSettings["EnableSSL"].ToUpper(CultureInfo.InvariantCulture) == "YES")
            {
                bEnableSSL = true;
            }
            else
            {
                bEnableSSL = false;
            }
            useDefaultCredentials = System.Configuration.ConfigurationManager.AppSettings["DefaultCredentials"].ToUpper(CultureInfo.InvariantCulture) == "YES";

        }
        public bool SendEmail(string msgbody, string subject, string strToAddress)
        {
            InitMail();

            dynamic MailMessage = new MailMessage();
            MailMessage.From = new MailAddress(FromAddress);
            MailMessage.To.Add(strToAddress);
            MailMessage.Subject = subject;
            MailMessage.IsBodyHtml = true;
            MailMessage.Body = msgbody;

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
                            MailMessage.CC.Add(new MailAddress(ccMailIds[_mailccIdx]));
                           // log.Info("Email CC added successfully to " + ccMailIds[_mailccIdx]);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
               // log.Error($"Unable to assign CC, Exception : {ex.StackTrace}");

            }

            SmtpClient SmtpClient = new SmtpClient();
            SmtpClient.Host = strSmtpClient;
            SmtpClient.EnableSsl = bEnableSSL;
            SmtpClient.Port = Convert.ToInt32(SMTPPort, System.Globalization.CultureInfo.InvariantCulture);
            // SmtpClient.Credentials = new System.Net.NetworkCredential(UserID, Password);
            SmtpClient.Credentials = useDefaultCredentials ? System.Net.CredentialCache.DefaultNetworkCredentials : new System.Net.NetworkCredential(UserID, Password);

            try
            {
                SmtpClient.Send(MailMessage);
            }
            catch (SmtpFailedRecipientsException ex)
            {
                for (int i = 0; i <= ex.InnerExceptions.Length; i++)
                {
                    SmtpStatusCode status = ex.InnerExceptions[i].StatusCode;
                    if ((status == SmtpStatusCode.MailboxBusy) | (status == SmtpStatusCode.MailboxUnavailable))
                    {
                        System.Threading.Thread.Sleep(5000);
                        SmtpClient.Send(MailMessage);
                    }
                }
            }
            return true;
        }
        public void Dispose()
        {
        }

    }
}
