//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;
//using IPMS.Web.Api;
//using IPMS.ServiceProxies.Contracts;
//using IPMS.Web.Adapters;
//using IPMS.ServiceProxies.Clients;
//using System.Net.Http;
//using IPMS.Domain.Models;
//using WebMatrix.WebData;

using Core.Repository;
using IPMS.Data.Context;
using IPMS.Domain.Models;
using IPMS.ServiceProxies.Clients;
using IPMS.ServiceProxies.Contracts;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using System;
using IPMS.Web.Api;
using IPMS.Domain.ValueObjects;
using System.Configuration;



namespace IPMS.Web.API
{
    public class UserRegistrationController : ApiControllerBase
    {
        IUserService _userService;

        IServiceRequestService _serviceRequestService;

        public UserRegistrationController()
        {
            _userService = new UserClient();

            _serviceRequestService = new ServiceRequestClient();
        }

        /// <summary>
        /// This method is used for Insert the data
        /// </summary>
        /// <param name="request"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [Route("api/UserRegistration")]
        [HttpPost]
        public HttpResponseMessage PostUserData(HttpRequestMessage request, User value)
        {
            return GetHttpResponse(request, () =>
            {

                //if (!WebSecurity.Initialized)
                //    WebSecurity.InitializeDatabaseConnection("TnpaContext", "Users", "UserID", "UserName", autoCreateTables: true);
                HttpResponseMessage response = null;
                //value.CreatedDate = DateTime.Now;
                //value.CreatedBy = 1;
                //value.ModifiedDate = DateTime.Now;
                //value.ModifiedBy = 1;
                var anonymousUserId = Convert.ToInt32(ConfigurationManager.AppSettings["AnonymousUserId"]);
                value.AnonymousUserYn = "Y";
                value.CreatedBy = anonymousUserId;
                value.ModifiedBy = anonymousUserId;
                // WebSecurity.CreateUserAndAccount(value.UserName, "navayuga", 

                //new
                //{
                //    FirstName = value.FirstName,
                //    LastName = value.LastName,
                //    UserType = value.UserType,
                //    UserTypeID = value.UserTypeID,
                //    ContactNo = value.ContactNo,
                //    EmailID = value.EmailID,
                //    RecordStatus = 'A',
                //    CreatedBy = value.CreatedBy,
                //    CreatedDate = DateTime.Now,
                //    ModifiedDate = DateTime.Now,
                //    ModifiedBy = value.CreatedBy,
                //    AnonymousUserYn = value.AnonymousUserYn
                //},
                //        false);
                //value.UserID = 1;
                value.ContactNo = value.ContactNo.Replace("(", "").Replace(")", "").Replace("-", "");


                User userCreated = _userService.AddUserRegistration(value);
                //User userCreate = null;
                //using (IUserService ls = new UserClient())
                //{
                //    userCreate = ls.AddPorts(value);
                //}
                // _userService.AddUser(value);
                response = request.CreateResponse<User>(HttpStatusCode.Created, userCreated);

                return response;
                //response = request.CreateResponse<User>(HttpStatusCode.Created, userCreated);

                //return response;
            });
        }

        #region Workflow Integrated Methods

        /// <summary>
        /// This method is used for Approve Registered User
        /// </summary>
        /// <param name="request"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage ApproveUserRegistration(HttpRequestMessage request, PendingTaskVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                using (IUserService ls = new UserClient())
                {
                    ls.ApproveUserRegistration(value.ReferenceID, value.Remarks, value.TaskCode);
                }
                response = request.CreateResponse(HttpStatusCode.Created);

                return response;
            });
        }

        /// <summary>
        /// This method is used for Verify Registered User
        /// </summary>
        /// <param name="request"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage VerifyUserRegistration(HttpRequestMessage request, PendingTaskVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                using (IUserService ls = new UserClient())
                {
                    ls.VerifyUserRegistration(value.ReferenceID, value.Remarks, value.TaskCode);
                }
                response = request.CreateResponse(HttpStatusCode.Created);

                return response;
            });
        }

        /// <summary>
        /// This method is used for Reject Registered User
        /// </summary>
        /// <param name="request"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage RejectUserRegistration(HttpRequestMessage request, PendingTaskVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                using (IUserService ls = new UserClient())
                {
                    ls.RejectUserRegistration(value.ReferenceID, value.Remarks, value.TaskCode);
                }
                response = request.CreateResponse(HttpStatusCode.Created);

                return response;
            });
        }

        #endregion



        //Anusha Employee

        [Route("api/GetEmployeesListFetching/{PortCode}/{ReferenceNo}")]
        [HttpGet]
        public HttpResponseMessage GetEmployeesListFetching(HttpRequestMessage request, string PortCode, string ReferenceNo)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                PrivilegeVO privilege = new PrivilegeVO();
                List<EmployeeMasterDetails> employeesList = _userService.GetEmployeesListFetching(PortCode, ReferenceNo);
                response = request.CreateResponse<List<EmployeeMasterDetails>>(HttpStatusCode.OK, employeesList);

                return response;
            });
        }






        //Anusha Agent
        [Route("api/GetAgentListDetailsFetch/{PortCode}/{ReferenceNo}")]
        [HttpGet]
        public HttpResponseMessage GetAgentListDetailsFetch(HttpRequestMessage request, string PortCode, string ReferenceNo)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                PrivilegeVO privilege = new PrivilegeVO();
                List<AgentDetailsVO> employeesList = _userService.GetAgentListDetailsFetch(PortCode, ReferenceNo);
                response = request.CreateResponse<List<AgentDetailsVO>>(HttpStatusCode.OK, employeesList);
                return response;
            });
        }



        //Anusha terminalOperator

        [Route("api/GetTerminalOperatorListDetailsFetch/{PortCode}/{ReferenceNo}")]
        [HttpGet]
        public HttpResponseMessage GetTerminalOperatorListDetailsFetch(HttpRequestMessage request, string PortCode, string ReferenceNo)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                PrivilegeVO privilege = new PrivilegeVO();
                List<TerminalOperatorVO> employeesList = _userService.GetTerminalOperatorListDetailsFetch(PortCode, ReferenceNo);
                response = request.CreateResponse<List<TerminalOperatorVO>>(HttpStatusCode.OK, employeesList);
                return response;
            });
        }













        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _userService.Dispose();
                _serviceRequestService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}