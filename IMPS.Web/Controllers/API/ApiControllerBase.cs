using IPMS.Domain.ValueObjects;
using IPMS.ServiceProxies.Clients;
using IPMS.ServiceProxies.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security;
using System.ServiceModel;
using System.Threading;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace IPMS.Web.Api
{
    public class ApiControllerBase : ApiController
    {
       
        public PrivilegeVO privilege;
        protected void ValidateAuthorizedUser(string userRequested)
        {
            string userLoggedIn = User.Identity.Name;
            if (userLoggedIn != userRequested)
                throw new SecurityException("Attempting to access data for another user.");
        }

        protected HttpResponseMessage GetHttpResponse(HttpRequestMessage request, Func<HttpResponseMessage> codeToExecute)
        {
           
            HttpResponseMessage response = null;
            privilege = new PrivilegeVO();
            string username = Thread.CurrentPrincipal.Identity.Name;
            string controllername = this.GetType().Name;
            controllername = controllername.Replace("Controller", "");


            //TODO:Present privileges are not validating all locations, where ever it is validating there below code to be place
            //using (IAccountService _accountService = new AccountClient())
            //{
            //    controllername = controllername.Replace("Controller", "");
            //    privilege.Privileges = _accountService.GetUserPrivilegesWithControllerName(controllername, username);
            //}
            
            try
            {
                if (codeToExecute != null)
                {
                    response = codeToExecute.Invoke();
                }
            }
            catch (SecurityException ex)
            {
                response = request.CreateResponse(HttpStatusCode.Unauthorized, ex.Message);
            }
           
            catch (FaultException ex)
            {
                response = request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
            catch (Exception ex)
            {
                response = request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

            return response;
        }

        protected T ExecuteWithException<T>(Func<T> codeToExecute)
        {
            try
            {
               
                    T response = default(T);
                if (codeToExecute != null)
                {
                    response = codeToExecute.Invoke();
                }
                return response;
               
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Internal Server Error"));
            }
        }

    }
}