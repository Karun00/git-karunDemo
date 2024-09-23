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
using IPMS.Core.Repository.Exceptions;
using System.Diagnostics;


namespace IPMS.Notifications.Engine
{
    public class SMSSender : IDisposable
    {
        private string smsuid;
        private string smspwd;
        private string senderid;
        private string smsservice;
        private string domain;

        private IWebProxy objProxy1 = null;
        private ILog log;
        //private static int count = 0;

        public SMSSender()
        {
            domain = string.Empty;
            smsuid = System.Configuration.ConfigurationManager.AppSettings.Get("SMSUID");
            smspwd = ConfigurationManager.AppSettings["SMSPWD"].ToString();
            senderid = ConfigurationManager.AppSettings["SENDERID"].ToString();
            smsservice = ConfigurationManager.AppSettings["SERVICE"].ToString();
            domain = ConfigurationManager.AppSettings["DOMAIN"].ToString();

            XmlConfigurator.Configure();
            log = LogManager.GetLogger(typeof(SMSSender));
        }

        public string SendSMS_TNPA(string Mobile_Number, string Message)
        {
            string stringResult = "No Mobile_Number";
            string _Mobile_Number = Mobile_Number;
            System.Object stringpost;
            HttpWebRequest objWebRequest = null;
            HttpWebResponse objWebResponse = null;
            StreamWriter objStreamWriter = null;
            StreamReader objStreamReader = null;
            stringpost = null;

            //stringpost = smsservice + "<usareq NODE=" + Convert.ToChar(34) + smsuid +  Convert.ToChar(34) + " USERNAME=" + Convert.ToChar(34) + smsuid + Convert.ToChar(34) + " PASSWORD=" + Convert.ToChar(34) + smspwd + Convert.ToChar(34) + " TRANSFORM=" + Convert.ToChar(34) + senderid + Convert.ToChar(34) + "><command><submit_sm><a_number>278393001781188</a_number>";

            ///NOTE: Here passing SMS ProviderNo in Mobile_Number itself with semi-colon separator, if not by default MTM provider will be taken


            if (_Mobile_Number.IndexOf(",") == -1)
            {
                _Mobile_Number += ",";
            }
            string[] _contactnos = _Mobile_Number.Split(',');
            string _providerno = "278393001781188"; //Default MTM provider no

            for (int _cno = 0; _cno < _contactnos.Length; _cno++)
            {
                if (!string.IsNullOrEmpty(_contactnos[_cno]))
                {
                    _providerno = (string.IsNullOrEmpty(_contactnos[_cno].Split(';')[1].ToString()) ? _providerno : _contactnos[_cno].Split(';')[1].ToString());
                    
                    _contactnos[_cno] = _contactnos[_cno].Split(';')[0].ToString();

                    stringpost = smsservice + "<usareq NODE=" + Convert.ToChar(34) + smsuid + Convert.ToChar(34) + " USERNAME=" + Convert.ToChar(34) + smsuid + Convert.ToChar(34) + " PASSWORD=" + Convert.ToChar(34) + smspwd + Convert.ToChar(34) + " TRANSFORM=" + Convert.ToChar(34) + senderid + Convert.ToChar(34) + "><command><submit_sm><a_number>" + _providerno + "</a_number>";
                    stringpost += "<b_number>" + _contactnos[_cno] + "</b_number>";
                    stringpost += "<service_type/><message>" + Message + "</message><registered_delivery/></submit_sm></command></usareq>";
                    log.Debug("stringpost : " + stringpost);

                    smsservice = stringpost.ToString();

                    try
                    {
                        if (string.IsNullOrEmpty(smsuid))
                            throw new BusinessExceptions("SMS gateway invalid credentials");
                        if (string.IsNullOrEmpty(smspwd))
                            throw new BusinessExceptions("SMS gateway invalid credentials");
                        if (string.IsNullOrEmpty(senderid))
                            throw new BusinessExceptions("SMS gateway invalid credentials SENDERID");
                        if (string.IsNullOrEmpty(Message))
                            throw new BusinessExceptions("SMS Message should not be empty");
                        log.Debug("Url : " + smsservice);
                        objWebRequest = (HttpWebRequest)WebRequest.Create(smsservice);
                        objWebRequest.Method = "GET";
                        if ((objProxy1 != null))
                        {
                            objWebRequest.Proxy = objProxy1;
                        }

                        Stopwatch wfstopwatch = Stopwatch.StartNew();

                        // objWebRequest.ContentType = "application/x-www-form-urlencoded";
                        // objStreamWriter = new StreamWriter(objWebRequest.GetRequestStream());
                        // objStreamWriter.Write(stringpost);
                        // objStreamWriter.Flush();
                        // objStreamWriter.Close();
                        objWebResponse = (HttpWebResponse)objWebRequest.GetResponse();
                        objStreamReader = new StreamReader(objWebResponse.GetResponseStream());
                        stringResult = objStreamReader.ReadToEnd();
                        wfstopwatch.Stop();
                        log.Debug("Time taken for SendSMS " + wfstopwatch.Elapsed.ToString());
                        log.Debug("Result : " + stringResult);

                        objStreamReader.Close();
                        return (stringResult);
                    }
                    catch (BusinessExceptions ex)
                    {
                        stringResult = "Business Validation Error: " + ex.Message;
                        log_error(stringResult, ex);
                        return (stringResult);
                    }
                    catch (Exception ex)
                    {
                        stringResult = "Message failed for sending " + _contactnos[_cno];
                        log_error(stringResult, ex);
                        return (stringResult);

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
            return (stringResult);
        }

        public string SendSMS(string Mobile_Number, string Message)
        {
            string stringResult = "No Mobile_Number";
            string _Mobile_Number = Mobile_Number;
            System.Object stringpost;
            HttpWebRequest objWebRequest = null;
            HttpWebResponse objWebResponse = null;
            StreamWriter objStreamWriter = null;
            StreamReader objStreamReader = null;
            stringpost = null;

            if (domain == "TNPA")
            {
                stringResult = SendSMS_TNPA(_Mobile_Number, Message);
            }
            else
            {
                stringpost = "User=" + smsuid + "&passwd=" + smspwd + "&mobilenumber=" + _Mobile_Number + "&message=" + Message + "&SID=" + senderid + "&MType=N&DR=Y";

                try
                {
                    if (string.IsNullOrEmpty(smsuid))
                        throw new BusinessExceptions("SMS gateway invalid credentials");
                    if (string.IsNullOrEmpty(smspwd))
                        throw new BusinessExceptions("SMS gateway invalid credentials");
                    if (string.IsNullOrEmpty(senderid))
                        throw new BusinessExceptions("SMS gateway invalid credentials SENDERID");
                    if (string.IsNullOrEmpty(Message))
                        throw new BusinessExceptions("SMS Message should not be empty");

                    objWebRequest = (HttpWebRequest)WebRequest.Create(smsservice);
                    objWebRequest.Method = "GET";
                    if ((objProxy1 != null))
                    {
                        objWebRequest.Proxy = objProxy1;
                    }

                    Stopwatch wfstopwatch = Stopwatch.StartNew();

                    //objWebRequest.ContentType = "application/x-www-form-urlencoded";
                    //objStreamWriter = new StreamWriter(objWebRequest.GetRequestStream());
                    //objStreamWriter.Write(stringpost);
                    //objStreamWriter.Flush();
                    //objStreamWriter.Close();
                    objWebResponse = (HttpWebResponse)objWebRequest.GetResponse();
                    objStreamReader = new StreamReader(objWebResponse.GetResponseStream());
                    stringResult = objStreamReader.ReadToEnd();
                    wfstopwatch.Stop();
                    log.Debug("Time taken for SendSMS " + wfstopwatch.Elapsed.ToString());

                    objStreamReader.Close();
                    return (stringResult);
                }
                catch (BusinessExceptions ex)
                {
                    stringResult = "Business Validation Error: " + ex.Message;
                    log_error(stringResult, ex);
                    return (stringResult);
                }
                catch (Exception ex)
                {
                    stringResult = "Message failed for sending " + _Mobile_Number;
                    log_error(stringResult, ex);
                    return (stringResult);

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
            return (stringResult);

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
