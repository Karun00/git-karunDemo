using System;
using System.Configuration;
using System.Net.Http.Headers;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web;

using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.DirectoryServices.Protocols;
using System.Net;
using log4net;

namespace IPMSFeedService
{
    public class BasicHttpAuthenticationModule : IHttpModule
    {
        private const string Realm = "IPMS";

        private static readonly ILog log = LogManager.GetLogger(typeof(BasicHttpAuthenticationModule));

        public void Init(HttpApplication context)
        {
            log4net.Config.XmlConfigurator.Configure();
            context.AuthenticateRequest += OnApplicationAuthenticateRequest;
            context.EndRequest += OnApplicationEndRequest;
        }

        private static void SetPrincipal(IPrincipal principal)
        {
            Thread.CurrentPrincipal = principal;
            if (HttpContext.Current != null)
            {
                HttpContext.Current.User = principal;
            }
        }

        private static bool Checkcredentials(string username, string password, out StringBuilder msg)
        {
            string _username = string.Empty;
            string _pwd = string.Empty;
            bool isValid = false;
            bool useAD = false;
            msg = new StringBuilder();
            string _errmsg = string.Empty;

            if (ConfigurationManager.AppSettings["userName"] == null)
            {
                _errmsg = "missing appsetting values";
                log.Error(LogErrorMessage("Checkcredentials", new Exception(_errmsg)));
                msg.Append(_errmsg);
                return false;
            }
            else
                _username = ConfigurationManager.AppSettings["userName"].ToString();

            if (ConfigurationManager.AppSettings["passWord"] == null)
            {
                _errmsg = "missing appsetting values";
                log.Error(LogErrorMessage("Checkcredentials", new Exception(_errmsg)));
                msg.Append(_errmsg);
                return false;
            }
            else
                _pwd = ConfigurationManager.AppSettings["passWord"].ToString();

            isValid = username == _username && password == _pwd;

            return isValid;
        }

        private static void AuthenticateUser(string credentials)
        {
            StringBuilder msg = new StringBuilder();
            try
            {
                var encoding = Encoding.GetEncoding("iso-8859-1");
                credentials = encoding.GetString(Convert.FromBase64String(credentials));

                int separator = credentials.IndexOf(':');
                string name = credentials.Substring(0, separator);
                string password = credentials.Substring(separator + 1);

                if (Checkcredentials(name, password, out msg))
                {
                    var identity = new GenericIdentity(name);
                    SetPrincipal(new GenericPrincipal(identity, null));
                }
                else
                {
                    HttpContext.Current.Response.StatusCode = 401;
                }
            }
            catch (FormatException)
            {
                HttpContext.Current.Response.StatusCode = 401;

            }
        }

        private static void OnApplicationAuthenticateRequest(object sender, EventArgs e)
        {
            var request = HttpContext.Current.Request;
            var authHeader = request.Headers["Authorization"];
            if (authHeader != null)
            {
                var authHeaderVal = AuthenticationHeaderValue.Parse(authHeader);

                if (authHeaderVal.Scheme.Equals("basic",
                        StringComparison.OrdinalIgnoreCase) &&
                    authHeaderVal.Parameter != null)
                {
                    AuthenticateUser(authHeaderVal.Parameter);
                }
            }
        }

        private static void OnApplicationEndRequest(object sender, EventArgs e)
        {
            var response = HttpContext.Current.Response;
            if (response.StatusCode == 401)
            {
                response.Headers.Add("WWW-Authenticate",
                    string.Format("Basic realm=\"{0}\"", Realm));
            }
        }

        public void Dispose()
        {
        }

        private static string LogErrorMessage(string pretext, Exception ex)
        {
            string msg = pretext + " " + ex.Message;
            if (ex.InnerException != null)
            {
                if (!string.IsNullOrEmpty(ex.InnerException.Message))
                {
                    msg = msg + " Inner Exception:" + ex.InnerException.Message + (string.IsNullOrEmpty(ex.StackTrace) ? "" : ex.StackTrace);
                }
            }
            return msg;
        }

    }
}