using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using IPMS.Domain.Models;
using IPMS.ServiceProxies.Clients;
using IPMS.ServiceProxies.Contracts;
using log4net;
using Newtonsoft.Json;

namespace IPMS.Web.Filters
{

    public class LogUserActivityAttribute : ActionFilterAttribute
    {
        private string _username = null;
        private int _anonymoususerid;
        private const string StopwatchKey = "StopwatchFilter.Value";


        ILog log = LogManager.GetLogger(typeof(LogUserActivityAttribute));

        public LogUserActivityAttribute()
        {
        }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            //TODO : Pending to be tested
            _anonymoususerid = 0;
            _anonymoususerid = Convert.ToInt32(ConfigurationManager.AppSettings.Get("AnonymousUserId").ToString(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);

            _username = (HttpContext.Current.User.Identity.Name ?? "Anonymous User");

            actionContext.Request.Properties[StopwatchKey] = Stopwatch.StartNew();
            log.Info(string.Format("Method started {0} by User : {1}", actionContext.Request.RequestUri, _username));

            //CommonAction(actionContext, null, "ENTRY");
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            string _actionUrl = actionExecutedContext.ActionContext.Request.RequestUri.ToString();
            string _class = _actionUrl.Split('/')[_actionUrl.Split('/').Length - 2].ToString();
            string _actionMethod = _actionUrl.Split('/')[_actionUrl.Split('/').Length -1].ToString();

            Stopwatch stopwatch = (Stopwatch)actionExecutedContext.Request.Properties[StopwatchKey];


            log.Info(string.Format("Time taken to execute class / method : {0} / {1} is  {2} (Milli seconds) ", _class, _actionMethod, stopwatch.ElapsedMilliseconds.ToString(CultureInfo.InvariantCulture)));


            //CommonAction(null, actionExecutedContext, "EXIT");
        }

        public void CommonAction(HttpActionContext actionContext, HttpActionExecutedContext actionExecutedContext, string actionstatus)
        {
            string isAuditLog = ConfigurationManager.AppSettings.Get("IsAuditLog");

            if (isAuditLog.ToUpper() == "YES")
            {
                DateTime timeStamp = HttpContext.Current.Timestamp;
                string userName = Thread.CurrentPrincipal.Identity.Name;
                string output = string.Empty;
                string controllerName = string.Empty;
                string actionName = string.Empty;
                HttpActionDescriptor actionDescriptor = null;

                try
                {
                    if (actionContext != null)
                    {
                        actionDescriptor = actionContext.ActionDescriptor;

                        var arguments = actionContext.ActionArguments;
                        List<object> values = new List<object>();

                        for (int i = 1; i < arguments.Count; i++)
                        {
                            values.Add(arguments[actionDescriptor.GetParameters()[i].ParameterName]);
                        }

                        output = JsonConvert.SerializeObject(values);
                    }
                    else
                    {
                        if (actionExecutedContext != null)
                        {
                            actionDescriptor = actionExecutedContext.ActionContext.ActionDescriptor;


                            if (actionExecutedContext.Response != null)
                            {
                                var objectContent = actionExecutedContext.Response.Content as ObjectContent;
                                if (objectContent != null)
                                {
                                    var value = objectContent.Value;
                                    output = JsonConvert.SerializeObject(value);
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    log.Error("An error occured in LogUserActivityAttribute", ex);
                }

                controllerName = actionDescriptor.ControllerDescriptor.ControllerName;
                actionName = actionDescriptor.ActionName;

                AuditTrail(controllerName, actionName, timeStamp, userName, output, actionstatus);
            }
        }

        public void AuditTrail(string controllerName, string actionName, DateTime timeStamp, string userName, string output, string status)
        {

            userName = (string.IsNullOrEmpty(userName) ? _username : userName);

            if (!string.IsNullOrEmpty(userName))
            {
                //TODO:  No need to pass userId, pass Username.  
                int userId = 1;

                AuditTrailConfig auditTrailConfig = new AuditTrailConfig
                {
                    ActionName = actionName,
                    ControlerName = controllerName
                };
                AuditTrail auditTrail = new AuditTrail
                {
                    AuditDateTime = timeStamp,
                    UserID = userId,
                    UserName = userName,
                    UserIPAddress = HttpContext.Current.Request.UserHostAddress.ToString(),
                    //UserComputerName = GetMachineNameFromIPAddress(HttpContext.Current.Request.UserHostAddress.ToString()),
                    UserComputerName = "",
                    Content = output,
                    EntryORExit = status
                };

                IAuditLogService _auditlogservice = new AuditLogClient();

                try
                {
                    //TODO: Change the service to take UserName as input and get UserId in the service side, it avoids two service calls. 
                    _auditlogservice.UserActivityLogging(auditTrailConfig, auditTrail);
                }
                finally
                {
                    if (_auditlogservice != null)
                    {
                        _auditlogservice.Dispose();
                        _auditlogservice = null;
                    }

                }



            }
        }

        private static string GetMachineNameFromIPAddress(string ipAdress)
        {
            string machineName = string.Empty;
            try
            {
                machineName = "";
                //IPHostEntry hostEntry = Dns.GetHostEntry(ipAdress);
                //machineName = hostEntry.HostName;
            }
            catch (Exception ex)
            {
                // Machine not found...
            }
            return machineName;
        }
    }
}