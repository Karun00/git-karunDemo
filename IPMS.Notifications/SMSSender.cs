using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using IPMS.Domain.Models;
using System.Configuration;
using System.Net;
using System.IO;
using log4net;
using log4net.Config;


namespace IPMS.Notifications
{
    public class SMSSender
    {
        private string smsuid;
        private string smspwd;
        private string senderid;
        private string smsservice;
        private IWebProxy objProxy1 = null;
        private ILog log;

        public SMSSender()
        {
            smsuid = System.Configuration.ConfigurationManager.AppSettings.Get("SMSUID");
            smspwd = ConfigurationManager.AppSettings["SMSPWD"].ToString();
            senderid = ConfigurationManager.AppSettings["SENDERID"].ToString();
            smsservice = ConfigurationManager.AppSettings["SERVICE"].ToString();
            XmlConfigurator.Configure();
            log = LogManager.GetLogger(typeof(SMSSender));

        }


        public string SendSMS(string Mobile_Number, string Message)
        {
            System.Object stringpost = "User=" + smsuid + "&passwd=" + smspwd + "&mobilenumber=" + Mobile_Number + "&message=" + Message + "&SID=" + senderid + "&MType=N&DR=Y";
            HttpWebRequest objWebRequest = null;
            HttpWebResponse objWebResponse = null;
            StreamWriter objStreamWriter = null;
            StreamReader objStreamReader = null;
            try
            {

                string stringResult = null;
                objWebRequest = (HttpWebRequest)WebRequest.Create(smsservice);
                objWebRequest.Method = "POST";
                if ((objProxy1 != null))
                {
                    objWebRequest.Proxy = objProxy1;
                }
                //WebProxy myProxy = new WebProxy("127.0.0.1", 8888);
                //myProxy.BypassProxyOnLocal = true;

                objWebRequest.ContentType = "application/x-www-form-urlencoded";
                objStreamWriter = new StreamWriter(objWebRequest.GetRequestStream());
                objStreamWriter.Write(stringpost);
                objStreamWriter.Flush();
                objStreamWriter.Close();
                //System.Net.ServicePointManager.Expect100Continue = false;
                objWebResponse = (HttpWebResponse)objWebRequest.GetResponse();
                objStreamReader = new StreamReader(objWebResponse.GetResponseStream());
                stringResult = objStreamReader.ReadToEnd();
                objStreamReader.Close();
                return (stringResult);
            }
            catch (Exception ex)
            {
                log.Error("SendSMS() method exception = " + ex.Message + Convert.ToChar(13) + "Location :" + ex.StackTrace);
                return (ex.Message);
            }
            finally
            {
                if ((objStreamWriter != null))
                {
                    objStreamWriter.Close();
                }
                if ((objStreamReader != null))
                {
                    objStreamReader.Close();
                }
                objWebRequest = null;
                objWebResponse = null;
                objProxy1 = null;
            }
        }
    }
}
