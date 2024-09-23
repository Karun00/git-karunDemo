using Core.Repository;
using IPMS.Core.Repository.Exceptions;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using IPMS.ServiceProxies.Clients;
using IPMS.ServiceProxies.Contracts;
using log4net;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using System.Web.Security;
using WebApi.OutputCache.V2;



namespace IPMS.Web.Api
{
    public class AccountController : ApiControllerBase
    {
        IAccountService _loginservice;
        IPortService _portService;
        //        ILog log = log4net.LogManager.GetLogger(typeof(AccountController));

        public AccountController()
        {
            _loginservice = new AccountClient();
            _portService = new PortClient(); 
        }

        [HttpPost]

        public HttpResponseMessage CheckCredentials(HttpRequestMessage request, [FromBody]AccountLoginModel logindetail)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                string ipAddress = string.Empty;
                var SessionTimeout = 0;

                try
                {
                    ipAddress = HttpContext.Current.Request.UserHostAddress.ToString();
                }
                catch { }

                AccountLoginModel success = _loginservice.UserLogin(logindetail.UserName, logindetail.Password, ipAddress);
                if (success != null)
                {

   
                    if (logindetail.IsMobile == "Y")
                    {
                        SessionTimeout = _loginservice.GetMobilePortSessiontimeOut();
                    }
                    else
                    {
                        //FormsAuthentication.SetAuthCookie(Logindetail.UserName, false);
                        SessionTimeout = _loginservice.GetPortSessiontimeOut();
                    }

                    var ticket = new FormsAuthenticationTicket(logindetail.UserName, false, SessionTimeout);
                    string encrypted = FormsAuthentication.Encrypt(ticket);
                    var cookie = new HttpCookie(".ASPXAUTH", encrypted);
                    cookie.Expires = System.DateTime.Now.AddMinutes(SessionTimeout);
                    HttpContext.Current.Response.Cookies.Add(cookie);
                    HttpContext.Current.Response.Cookies.Set(cookie);

                    int timeout = logindetail.RememberMe ? 525600 : -1;

                    var ticket1 = new FormsAuthenticationTicket(logindetail.UserName, logindetail.RememberMe, timeout);
                    string encrypted1 = FormsAuthentication.Encrypt(ticket1);
                    var cookie1 = new HttpCookie("userName", encrypted1);
                    cookie1.Expires = System.DateTime.Now.AddMinutes(timeout);
                    HttpContext.Current.Response.Cookies.Add(cookie1);
                    HttpContext.Current.Response.Cookies.Set(cookie1);

                    var ticket2 = new FormsAuthenticationTicket(logindetail.Password, logindetail.RememberMe, timeout);
                    string encrypted2 = FormsAuthentication.Encrypt(ticket2);
                    var cookie2 = new HttpCookie("Password", encrypted2);
                    cookie2.Expires = System.DateTime.Now.AddMinutes(timeout);
                    HttpContext.Current.Response.Cookies.Add(cookie2);
                    HttpContext.Current.Response.Cookies.Set(cookie2);


                    //var ticketp = new FormsAuthenticationTicket(Logindetail.Password, Logindetail.RememberMe, timeout);
                    //string encryptedp = FormsAuthentication.Encrypt(ticketp);
                    //var cookiep = new HttpCookie("Password", encryptedp);
                    //cookiep.Expires = System.DateTime.Now.AddMinutes(timeout);
                    //HttpContext.Current.Response.Cookies.Add(cookiep);
                    //HttpContext.Current.Response.Cookies.Set(cookiep);

                    //HttpCookie cookie = FormsAuthentication.GetAuthCookie(Logindetail.UserName, true);
                    //cookie.Expires=DateTime.Now.Add(new TimeSpan(0,1,0));
                    //HttpContext.Current.Response.Cookies.Add(cookie);

                    response = request.CreateResponse<AccountLoginModel>(HttpStatusCode.OK, success);


                }
                return response;
            });
        }


        [Route("api/ChangePassword")]
        [HttpPost]
        [Authorize]
        public HttpResponseMessage PostChangePassword(HttpRequestMessage request, [FromBody]AccountLoginModel passwordModel)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var username = User.Identity.Name;
                passwordModel.UserName = username;
                ValidateAuthorizedUser(username);
                string msg = _loginservice.ChangePassword(passwordModel);
                response = request.CreateResponse(HttpStatusCode.OK, msg);

                return response;
            });
        }

        /// <summary>
        /// This function will get the list of Ports for logged in user. If user is not authenticated.  This method is not used.
        /// This information is cached on server and client.   
        /// </summary>
        /// <param name="uname"></param>
        /// <returns></returns>
        [Route("api/Account/GetUserPorts/{uname}")]
        [CacheOutput(ClientTimeSpan = 3600, ServerTimeSpan = 3600)]
        public IEnumerable<PortVO> GetUserPorts(string uname)
        {
            return ExecuteWithException<IEnumerable<PortVO>>(() =>
                {
                    IEnumerable<PortVO> ports = null;
                    if (User.Identity.IsAuthenticated)
                    {
                        ports = _loginservice.GetPortsByUser();
                    }
                    else
                    {
                        //TODO: Need to throw proper exception here
                        //throw new Exception("User is not authenticated");
                        throw new BusinessExceptions(BusinessExceptions.NotAuthorizedUser);
                    }
                    return ports;
                });
        }
        //By Mahesh : For Mobile App
        [Route("api/GetUserPortsForMobileByUserName/{uname}")]
        [HttpGet]
        public HttpResponseMessage GetUserPortsForMobileByUserName(HttpRequestMessage request, string uname)
        {
            return GetHttpResponse(request, () =>
            {
                //uname = "admin";
                HttpResponseMessage response = null;
                IEnumerable<PortVO> ports = null;
                ports = _loginservice.GetPortsByUserForMobile(uname);
                response = request.CreateResponse<IEnumerable<PortVO>>(HttpStatusCode.OK, ports);

                return response;

            });
        }

        [Route("api/GetUserPortsForMobile")]
        [HttpGet]
        public HttpResponseMessage GetUserPortsForMobile(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<Port> ports = null;
                ports = _portService.GetPorts();
                response = request.CreateResponse<List<Port>>(HttpStatusCode.OK, ports);

                return response;

            });
        }
        ////////////////////

        /// <summary>
        /// This method is used to get left menu items for logged in user.  This is available onl for authenticated users. 
        /// </summary>
        /// <param name="uname"></param>
        /// <returns></returns>
        [Route("api/Account/GetUserModules/{uname}")]
        [CacheOutput(ClientTimeSpan = 3600, ServerTimeSpan = 3600)]
        public IEnumerable<Module> GetUserModules(string uname)
        {
            return ExecuteWithException<IEnumerable<Module>>(() =>
            {
                IEnumerable<Module> modules = null;
                if (User.Identity.IsAuthenticated)
                {
                    var username = User.Identity.Name;
                    modules = _loginservice.GetModulesByUser();
                }
                else
                {
                    //TODO: Need to throw proper exception here
                    //throw new Exception("User is not authenticated");
                    throw new BusinessExceptions(BusinessExceptions.NotAuthorizedUser);
                }
                return modules;
            });
        }

        public HttpResponseMessage GetRoles(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                IEnumerable<Role> roles = _loginservice.GetRoles();
                response = request.CreateResponse<IEnumerable<Role>>(HttpStatusCode.OK, roles);
                return response;
            });
        }

        public HttpResponseMessage GetPendingTasks(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                IEnumerable<GroupedPendingTaskVO> pendingtask = null;

                using (IAccountService ls = new AccountClient())
                {
                    pendingtask = ls.GetPendingTask();
                }
                response = request.CreateResponse<IEnumerable<GroupedPendingTaskVO>>(HttpStatusCode.OK, pendingtask);
                return response;
            });
        }

        [HttpPost]
        public HttpResponseMessage VAP(HttpRequestMessage request, PendingTaskVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                PendingTaskVO pendingtask = _loginservice.ApprovedPendingTask(value);
                response = request.CreateResponse<PendingTaskVO>(HttpStatusCode.Created, pendingtask);
                return response;
            });
        }

        [HttpPost]
        public HttpResponseMessage RejectPendingTask(HttpRequestMessage request, PendingTaskVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                PendingTaskVO pendingtask = _loginservice.RejectedPendingTask(value);
                response = request.CreateResponse<PendingTaskVO>(HttpStatusCode.Created, pendingtask);
                return response;
            });
        }

        [HttpPost]
        public HttpResponseMessage GetWorkflowtaskcode(HttpRequestMessage request, PendingTaskVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                IEnumerable<PendingTaskVO> pendingtask = _loginservice.GetWorkflowTask(value);
                response = request.CreateResponse<IEnumerable<PendingTaskVO>>(HttpStatusCode.Created, pendingtask);
                return response;
            });
        }


        public HttpResponseMessage GetSystemNotifications(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                IEnumerable<SystemNotificationVO> sysnotifications = _loginservice.GetSystemNotifications();
                response = request.CreateResponse<IEnumerable<SystemNotificationVO>>(HttpStatusCode.OK, sysnotifications);
                return response;
            });
        }

        public HttpResponseMessage GetPendingTaskCount(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                int GetPendingTaskCount = _loginservice.GetPendingTaskCount();
                response = request.CreateResponse<int>(HttpStatusCode.OK, GetPendingTaskCount);
                return response;
            });
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _loginservice.Dispose();
            }
            base.Dispose(disposing);
        }

        public string GetUserPrivilegesWithControllerName(string controllername, string username)
        {
            string Priv = _loginservice.GetUserPrivilegesWithControllerName(controllername, username);
            return Priv;
        }
    }
}
