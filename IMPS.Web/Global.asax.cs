using IPMS.Web.App_Start;
using log4net;
using log4net.Config;
using System;
using System.Configuration;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace IPMS.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(MvcApplication));
        private static void ConfigureAntiForgeryTokens()
        {
            // Rename the Anti-Forgery cookie from "__RequestVerificationToken" to "f".
            // This adds a little security through obscurity and also saves sending a
            // few characters over the wire.
            AntiForgeryConfig.CookieName = "f";
            // If you have enabled SSL. Uncomment this line to ensure that the Anti-Forgery
            // cookie requires SSL to be sent accross the wire.
            // AntiForgeryConfig.RequireSsl = true;
        }
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterHttpFilters(GlobalConfiguration.Configuration.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            XmlConfigurator.Configure();

        }
        protected void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs
            GetError();
        }
        protected void GetError()
        {

            Exception CurrentException = Server.GetLastError();

            // Get current exception 
            string ErrorDetails = CurrentException.ToString();

            var serverError = Server.GetLastError() as HttpException;
            if (serverError != null)
            {
                int errorCode = serverError.GetHttpCode();
                switch (errorCode)
                {
                    case 404:
                        Server.ClearError();
                        ErrorDetails = errorCode.ToString() + " - " + serverError.Message;
                        break;
                    case 403:
                        Server.ClearError();
                        ErrorDetails = errorCode.ToString() + " - You do not have permission to view this directory or page";
                        break;
                    default:
                        Server.ClearError();
                        ErrorDetails = errorCode.ToString() + " - " + serverError.Message + "  --InnerException-- " + serverError.InnerException ;
                        break;
                }
                log.Error(ErrorDetails);
                Response.Write(Generic_Message());

            }

            try
            {
               //string pwd = IPMS.Services.Password.Encrypt(ConfigurationManager.AppSettings["FromAddress"].ToString(),true);
                
                //if (Server.GetLastError() is HttpUnhandledException)
                //{
                //    ErrorDetails += "Stack Trace : " + CurrentException.StackTrace.ToString();

                //    // Send notification e-mail
                //    MailMessage Email =
                //        new MailMessage(ConfigurationManager.AppSettings["FromAddress"].ToString(), ConfigurationManager.AppSettings["ExceptionEmailId"].ToString());

                //    Email.Subject = "IPMS-Application Error";
                //    Email.Priority = MailPriority.High;
                //    Email.IsBodyHtml = true;
                //    Email.Body = Build_Html(ErrorDetails);

                //    var smtpHost = ConfigurationManager.AppSettings["SMTPHost"];
                //    //var port = Convert.ToUInt32(ConfigurationManager.AppSettings["port"]);
                //    using (SmtpClient client = new SmtpClient(smtpHost))
                //    {
                //        if (string.IsNullOrEmpty(smtpHost)) return;
                //        client.DeliveryMethod = SmtpDeliveryMethod.Network;
                //        client.UseDefaultCredentials = false;
                //        try
                //        {
                //            client.Send(Email);
                //            Response.Write(Generic_Message());
                //        }
                //        catch (SmtpException smtpEx)
                //        {
                //            ErrorDetails = "<b>Handled exception </b> : " + smtpEx.Message;
                //            ErrorDetails += "</br><b>Stack Trace :</b> " + smtpEx.StackTrace;
                //            Response.Write(Generic_Message());
                //        }
                //    }
                //}
                Server.ClearError();
            }
            catch (Exception ex)
            {
                ErrorDetails = "<b>Handled exception</b> : " + ex.Message;
                ErrorDetails += "</br><b>Stack Trace :</b> " + ex.StackTrace;
                Response.Write(Generic_Message());
                Server.ClearError();
            }
        
        }
        private string Build_Html(string msg)
        {
            StringBuilder strMsg = new StringBuilder();
            try
            {
                strMsg.Append("<body>");
                strMsg.Append("<table width='100%' border='0' align='center' cellpadding='0' cellspacing='0' bgcolor='#fdf3df' style='border-top-width: 1px; border-left-width: 1px; border-top-style: solid; border-left-style: solid; border-top-color: #ad6112; border-left-color: #ad6112; font-family: Arial, Helvetica, sans-serif; font-size: 11px; color: #000;'>");
                strMsg.Append("<tr>");
                strMsg.Append("<td align='center' bgcolor='#eea75b' style='font-family: Georgia, Times New Roman, Times, serif; font-size: 16px; color: #521C01; line-height: 35px; border-right-width: 1px; border-bottom-width: 1px; border-right-style: solid; border-bottom-style: solid; border-right-color: #ad6112; border-bottom-color: #ad6112; font-weight: bold;' valign='middle'>Unhandled Exception Details<span style='color: #0130b4;'></span></td>");
                strMsg.Append("</tr>");
                strMsg.Append("<tr>");
                strMsg.Append("<td align='center' valign='top' style='font-family: Verdana, Geneva, sans-serif; font-size: 11px; line-height: 18px; border-right-width: 1px; border-bottom-width: 1px; border-right-style: solid; border-bottom-style: solid; border-right-color: #ad6112; border-bottom-color: #ad6112; font-weight: normal; padding-left: 3px;'>");
                strMsg.Append("<br />");
                strMsg.Append("<table width='90%' border='0' cellpadding='0' cellspacing='0' style='border-top-width: 1px; border-left-width: 1px; border-top-style: solid; border-left-style: solid; border-top-color: #ad6112; border-left-color: #ad6112; font-family: Arial, Helvetica, sans-serif; font-size: 11px; color: #000;'>");
                strMsg.Append("<tr>");
                strMsg.Append("<td width='157' style='font-family: Verdana, Geneva, sans-serif; font-size: 11px; color: #600; line-height: 20px; border-right-width: 1px; border-bottom-width: 1px; border-right-style: solid; border-bottom-style: solid; border-right-color: #ad6112; border-bottom-color: #ad6112; font-weight: bold; background-color: #fdcd9b; padding-left: 10px;'>Request From</td>");
                strMsg.Append("<td width='104' style='font-family: Verdana, Geneva, sans-serif; font-size: 11px; line-height: 18px; border-right-width: 1px; border-bottom-width: 1px; border-right-style: solid; border-bottom-style: solid; border-right-color: #ad6112; border-bottom-color: #ad6112; font-weight: normal; padding-left: 3px;'>" + Request.ServerVariables["REMOTE_HOST"].ToString() + "</td>");
                strMsg.Append("</tr>");
                strMsg.Append("<tr>");
                strMsg.Append("<td width='157' style='font-family: Verdana, Geneva, sans-serif; font-size: 11px; color: #600; line-height: 20px; border-right-width: 1px; border-bottom-width: 1px; border-right-style: solid; border-bottom-style: solid; border-right-color: #ad6112; border-bottom-color: #ad6112; font-weight: bold; background-color: #fdcd9b; padding-left: 10px;'>Application Pool</td>");
                strMsg.Append("<td width='104' style='font-family: Verdana, Geneva, sans-serif; font-size: 11px; line-height: 18px; border-right-width: 1px; border-bottom-width: 1px; border-right-style: solid; border-bottom-style: solid; border-right-color: #ad6112; border-bottom-color: #ad6112; font-weight: normal; padding-left: 3px;'>" + Request.ServerVariables["APP_POOL_ID"].ToString() + "</td>");
                strMsg.Append("</tr>");
                strMsg.Append("<tr>");
                strMsg.Append("<td width='157' style='font-family: Verdana, Geneva, sans-serif; font-size: 11px; color: #600; line-height: 20px; border-right-width: 1px; border-bottom-width: 1px; border-right-style: solid; border-bottom-style: solid; border-right-color: #ad6112; border-bottom-color: #ad6112; font-weight: bold; background-color: #fdcd9b; padding-left: 10px;'>Application login user</td>");
                strMsg.Append("<td width='104' style='font-family: Verdana, Geneva, sans-serif; font-size: 11px; line-height: 18px; border-right-width: 1px; border-bottom-width: 1px; border-right-style: solid; border-bottom-style: solid; border-right-color: #ad6112; border-bottom-color: #ad6112; font-weight: normal; padding-left: 3px;'>" + Request.ServerVariables["LOGON_USER"].ToString() + "</td>");
                strMsg.Append("</tr>");
                strMsg.Append("<tr>");
                strMsg.Append("<td width='157' style='font-family: Verdana, Geneva, sans-serif; font-size: 11px; color: #600; line-height: 20px; border-right-width: 1px; border-bottom-width: 1px; border-right-style: solid; border-bottom-style: solid; border-right-color: #ad6112; border-bottom-color: #ad6112; font-weight: bold; background-color: #fdcd9b; padding-left: 10px;'>Error occurred</td>");
                strMsg.Append("<td width='104' style='font-family: Verdana, Geneva, sans-serif; font-size: 11px; line-height: 18px; border-right-width: 1px; border-bottom-width: 1px; border-right-style: solid; border-bottom-style: solid; border-right-color: #ad6112; border-bottom-color: #ad6112; font-weight: normal; padding-left: 3px;'>" + DateTime.Now.ToString("dd-MMM-yyyy HH:mm:ss") + "</td>");
                strMsg.Append("</tr>");
                strMsg.Append("<tr>");
                strMsg.Append("<td width='157' style='font-family: Verdana, Geneva, sans-serif; font-size: 11px; color: #600; line-height: 20px; border-right-width: 1px; border-bottom-width: 1px; border-right-style: solid; border-bottom-style: solid; border-right-color: #ad6112; border-bottom-color: #ad6112; font-weight: bold; background-color: #fdcd9b; padding-left: 10px;'>Error at url</td>");
                strMsg.Append("<td width='104' style='font-family: Verdana, Geneva, sans-serif; font-size: 11px; line-height: 18px; border-right-width: 1px; border-bottom-width: 1px; border-right-style: solid; border-bottom-style: solid; border-right-color: #ad6112; border-bottom-color: #ad6112; font-weight: normal; padding-left: 3px;'>" + Request.Url.ToString() + "</td>");
                strMsg.Append("</tr>");
                strMsg.Append("<tr>");
                strMsg.Append("<td width='157' style='font-family: Verdana, Geneva, sans-serif; font-size: 11px; color: #600; line-height: 20px; border-right-width: 1px; border-bottom-width: 1px; border-right-style: solid; border-bottom-style: solid; border-right-color: #ad6112; border-bottom-color: #ad6112; font-weight: bold; background-color: #fdcd9b; padding-left: 10px;'>Details</td>");
                strMsg.Append("<td width='104' style='font-family: Verdana, Geneva, sans-serif; font-size: 11px; line-height: 18px; border-right-width: 1px; border-bottom-width: 1px; border-right-style: solid; border-bottom-style: solid; border-right-color: #ad6112; border-bottom-color: #ad6112; font-weight: normal; padding-left: 3px;'>" + msg + "</td>");
                strMsg.Append("</tr>");
                strMsg.Append("</table>&nbsp;&nbsp;");
                strMsg.Append("</tr>");
                strMsg.Append("</table>");
                strMsg.Append("</body>");
                strMsg.Append("<tr>");
                strMsg.Append("<td>&nbsp</td>");
                strMsg.Append("</tr>");
            }
            catch (Exception ex)
            {
                strMsg.Append("Exception in Global.asax.cs->Build_Html : " + ex.Message);
            }

            return strMsg.ToString();

        }
        private string Generic_Message()
        {
            StringBuilder strMsg = new StringBuilder();
            strMsg.Append("<body>");
            strMsg.Append("<table width='700' border='0' cellspacing='0' cellpadding='0' align='center'>");
            strMsg.Append("<tr><td height='180px'></td></tr>");
            strMsg.Append("<tr><tr>");
            strMsg.Append("<table width='100%' border='0' cellspacing='0' cellpadding='0'>");
            strMsg.Append("<tr><td align='center' style='font-family: verdana; font-size: 12px; font-weight: bold; color: #FFFFFF; height: 25px; background: #336699'>There is a slight problem in the system</td></tr>");
            strMsg.Append("<tr><td align='center' style='font-family: verdana; font-size: 12px; font-weight: bold; color: #000000; height: 25px; background: #D6EBF8'>We will get it rectified at the earliest</td></tr>");
            strMsg.Append("<tr><td align='center' style='font-family: verdana; font-size: 12px; font-weight: bold; color: #000000; height: 25px; background: #BADEF3'>We regret the inconvenience caused.</td></tr>");
            strMsg.Append("</table></tr>");
            strMsg.Append("<tr><td class='Error_Pop_BLcrv'>&nbsp;</td><td class='Error_Pop_BLine'>&nbsp;</td><td class='Error_Pop_BRcrv'>&nbsp;</td></tr>");
            strMsg.Append("</table>");
            strMsg.Append("</body>");
            return strMsg.ToString();
        }

    }
}
