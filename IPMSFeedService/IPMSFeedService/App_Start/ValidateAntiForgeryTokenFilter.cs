using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Helpers;
using System.Web.Http.Controllers;
using Newtonsoft.Json;
using ActionFilterAttribute = System.Web.Http.Filters.ActionFilterAttribute;
using log4net;

namespace IPMSFeedService.Filters
{
    public class ValidateAntiForgeryTokenFilter : ActionFilterAttribute
    {
        private readonly ILog log;
        StringBuilder str = new StringBuilder();
        private const string XsrfHeader = "XSRF-TOKEN";
        private const string XsrfCookie = "xsrf-token";
        public const string Validateparameters = "[\"<>]";
        public const string ValidateparametersForPost = "[<>]";

        public static bool ArgumentsValidation(dynamic listofArguments)
        {
            bool isValid = false;
            foreach (var item in listofArguments)
            {
                if (item.Value != null)
                {
                    if (item.Key != "request")
                    {
                        var json = JsonConvert.SerializeObject(item.Value);
                        //var json = new JavaScriptSerializer().Serialize(item.Value);
                        Regex rgxUrl = new Regex(ValidateparametersForPost);
                        isValid = rgxUrl.IsMatch(json);
                        if (isValid)
                            return isValid;
                    }

                }

            }
            return isValid;
        }
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            string sessionIpAddress = string.Empty;

            if (HttpContext.Current.Session != null)
            {
                var encryptedString = Convert.ToString(HttpContext.Current.Session["encryptedSession"]);
                if (encryptedString != null && encryptedString != string.Empty)
                {
                    byte[] encodedAsBytes = Convert.FromBase64String(encryptedString);
                    string decryptedString = System.Text.Encoding.ASCII.GetString(encodedAsBytes);
                    char[] separator = new char[] { '^' };
                    if (decryptedString != string.Empty && !string.IsNullOrEmpty(decryptedString))
                    {
                        string[] splitStrings = decryptedString.Split(separator);
                        if (splitStrings.Any())
                        {
                            if (splitStrings[2].Any())
                            {
                                string[] userBrowserInfo = splitStrings[2].Split('~');
                                if (userBrowserInfo.Any())
                                {
                                    sessionIpAddress = userBrowserInfo[1];
                                }
                            }
                        }
                    }
                }
                var name = actionContext.ActionDescriptor.ActionName;
                var myRequest = ((HttpContextWrapper)actionContext.Request.Properties["MS_HttpContext"]).Request;
                var currentUseripAddress = myRequest.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (string.IsNullOrEmpty(currentUseripAddress))
                    currentUseripAddress = myRequest.ServerVariables["REMOTE_ADDR"];

                if (sessionIpAddress != "" && sessionIpAddress != string.Empty && !string.IsNullOrEmpty(currentUseripAddress))
                {
                    if (sessionIpAddress != currentUseripAddress)
                    {
                        HttpContext.Current.Request.Cookies.Clear();
                        throw new HttpException((Int32)HttpStatusCode.RequestTimeout, "Session expired.");
                    }
                }

            }


            bool isValidInput = false;
            if (actionContext != null && actionContext.Request != null && actionContext.Request.Method.Method == "POST")
            {
                try
                {
                    if (actionContext.ActionArguments.Count != 0)
                    {
                        var listofArguments = actionContext.ActionArguments;
                        if (listofArguments.Any())
                            isValidInput = ArgumentsValidation(listofArguments);
                        if (isValidInput)
                            throw new HttpException((Int32)HttpStatusCode.NotAcceptable, "Invalid input.");
                    }
                }
                catch (Exception ex)
                {
                    actionContext.Response = new HttpResponseMessage(HttpStatusCode.NotAcceptable);
                    return;
                }

                var headers = actionContext.Request.Headers;
                IEnumerable<string> xsrfTokenList;

                if (!headers.TryGetValues(XsrfHeader, out xsrfTokenList))
                {
                    actionContext.Response = new HttpResponseMessage(HttpStatusCode.BadRequest);
                    str.AppendLine(String.Format("Missing AntiForgery token values : Token value"));
                    //log.Error(str.ToString());
                    return;
                }

                CookieState tokenCookie = actionContext.Request.Headers.GetCookies().Select(c => c[XsrfCookie]).FirstOrDefault();

                string tokenHeaderValue = xsrfTokenList.First();

                if (tokenCookie == null)
                {
                    actionContext.Response = new HttpResponseMessage(HttpStatusCode.BadRequest);
                    str.AppendLine(String.Format("Missing AntiForgery token values : cookie value"));
                    //log.Error(str.ToString());
                    return;
                }

                try
                {
                    AntiForgery.Validate(tokenCookie.Value, tokenHeaderValue);
                }

                catch (Exception ex)
                {
                    actionContext.Response = new HttpResponseMessage(HttpStatusCode.BadRequest);
                    str.AppendLine(string.Format("AntiForgery token validation Exception message : {0}", ex.Message));
                    //log.Error(str.ToString());
                    return;
                }
            }
            base.OnActionExecuting(actionContext);
        }
    }
}
