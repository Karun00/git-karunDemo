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
using IPMS.Domain.DTOS;
using System.Web;
using System.Threading;


namespace IPMS.Web.API
{
    public class UserController : ApiControllerBase
    {
        IUserService _userService;

        IServiceRequestService _serviceRequestService;
        public UserController()
        {
            _userService = new UserClient();

            _serviceRequestService = new ServiceRequestClient();
        }


        [Route("api/EmployeesDetails")]
        [HttpGet]
        [Authorize]
        public HttpResponseMessage GetEmpDetails(HttpRequestMessage request)
        {

            return GetHttpResponse(request, () =>
            {

                HttpResponseMessage response = null;
                string searchValue = HttpContext.Current.Request.Params["filter[filters][0][value]"];
                List<UserMasterVO> user = _userService.GetEmployeesDetails(searchValue);
                response = request.CreateResponse<List<UserMasterVO>>(HttpStatusCode.OK, user);
                return response;

            });
        }


        [Route("api/AgentsDetails")]
        [HttpGet]
        [Authorize]
        public HttpResponseMessage GetAgntDetails(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {

                HttpResponseMessage response = null;
                string searchValue = HttpContext.Current.Request.Params["filter[filters][0][value]"];
                List<UserMasterVO> user = _userService.GetAgentDetails(searchValue);
                response = request.CreateResponse<List<UserMasterVO>>(HttpStatusCode.OK, user);
                return response;

            });
        }

      
        [Route("api/TerminalOperatorsDetails")]
        [HttpGet]
        [Authorize]
        public HttpResponseMessage GetToDetails(HttpRequestMessage request)
        {

            return GetHttpResponse(request, () =>
            {

                HttpResponseMessage response = null;
                string searchValue = HttpContext.Current.Request.Params["filter[filters][0][value]"];
                List<UserMasterVO> user = _userService.GetToDetails(searchValue);
                response = request.CreateResponse<List<UserMasterVO>>(HttpStatusCode.OK, user);
                return response;

            });

        }


        // Getting all User Types List
        /// <summary>
        /// This method is used for fetch the user type data
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("api/UserTypes")]
        [HttpGet]
        [Authorize]
        public HttpResponseMessage GetUserTypes(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<SubCategoryVO> user = _userService.GetUserType();
                response = request.CreateResponse<List<SubCategoryVO>>(HttpStatusCode.OK, user);
                return response;
            });
        }

        /// <summary>
        /// This method is used for fetches the role details
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("api/Roles")]
        [HttpGet]
        [Authorize]
        public HttpResponseMessage GetRoles(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<Role> roles = _userService.GetRoles();

                List<RoleVO> Roles = roles.MapToDto();

                response = request.CreateResponse<List<RoleVO>>(HttpStatusCode.OK, Roles);
                return response;
            });
        }

        /// <summary>
        /// This method is used for fetches the user details
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("api/Users")]
        [HttpGet]
        [Authorize]
        public HttpResponseMessage GetUsers(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {                
                HttpResponseMessage response = null;
                List<UserMasterVO> user = null;
                PrivilegeVO privilege = new PrivilegeVO();
                using (IAccountService _accountService = new AccountClient())
                {
                    string username = Thread.CurrentPrincipal.Identity.Name;
                    string controllername = this.GetType().Name;
                    controllername = controllername.Replace("Controller", "");
                    privilege.Privileges = _accountService.GetUserPrivilegesWithControllerName(controllername, username);
                }
               
                if (privilege.Privileges.Contains("VIEW"))
                {
                    user = _userService.GetUsersList();
                    response = request.CreateResponse<List<UserMasterVO>>(HttpStatusCode.OK, user);
                }
                else
                {
                    //user = null;
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound,"Unauthorized Access");
                }
                
                return response;
            });
        }

        // [Route("api/Users")]
        [HttpGet]
        [Authorize]
        public HttpResponseMessage GetUsersListForGrid(HttpRequestMessage request, string userType, string SearchText, string DarmentUser, string ReferenceNo)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<UserMasterVO> user = _userService.GetUsersListForGrid(userType, SearchText, DarmentUser, ReferenceNo);
                response = request.CreateResponse<List<UserMasterVO>>(HttpStatusCode.OK, user);
                return response;
            });
        }

        /// <summary>
        /// This method is used for update the data
        /// </summary>
        /// <param name="request"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [Route("api/Users")]
        [HttpPut]
        public HttpResponseMessage ModifyUserData(HttpRequestMessage request, User value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                value.ContactNo = value.ContactNo.Replace("(", "").Replace(")", "").Replace("-", "");
                string ipAddress = HttpContext.Current.Request.UserHostAddress.ToString();
                string machineName = "";
                int userModified = _userService.ModifyUser(value, ipAddress, machineName);
                response = request.CreateResponse<int>(HttpStatusCode.Created, userModified);
                return response;
            });
        }

        private static string GetMachineNameFromIPAddress(string ipAdress)
        {
            string machineName = string.Empty;
            try
            {
                machineName = "";

            }
            catch (Exception ex)
            {
            }
            return machineName;
        }

        /// <summary>
        /// This method is used for Insert the data
        /// </summary>
        /// <param name="request"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [Route("api/Users")]
        [HttpPost]
        public HttpResponseMessage PostUserData(HttpRequestMessage request, User value)
        {
            return GetHttpResponse(request, () =>
            {

                HttpResponseMessage response = null;

                value.ContactNo = value.ContactNo.Replace("(", "").Replace(")", "").Replace("-", "");
                value.AnonymousUserYn = "N";
               
                int result = _userService.AddUser(value);
                response = request.CreateResponse<int>(HttpStatusCode.Created, result);

                return response;
            });
        }

        /// <summary>
        /// This method is used for delete the data
        /// </summary>
        /// <param name="request"></param>
        /// <param name="value"></param>
        /// <returns></returns>       
        public HttpResponseMessage PutDeleteEmployee(HttpRequestMessage request, User value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                User userDelete = _userService.DeleteUserById(value);
                response = request.CreateResponse<User>(HttpStatusCode.OK, userDelete);
                return response;
            });
        }
        /// <summary>
        /// This method is used for fetches the user details
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("api/UserDetailsByID")]
        [HttpGet]
        [Authorize]
        public HttpResponseMessage GetUserDetailsById(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                UserMasterVO user = null;
                using (IUserService ls = new UserClient())
                {
                    user = ls.GetUserDetailsById();
                }

                response = request.CreateResponse<UserMasterVO>(HttpStatusCode.OK, user);
                return response;
            });
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public HttpResponseMessage GetUserDetailsByIDView(HttpRequestMessage request, int id)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                UserMasterVO user = null;
                using (IUserService ls = new UserClient())
                {
                    user = ls.GetUserDetailsByIDView(id);
                }

                response = request.CreateResponse<UserMasterVO>(HttpStatusCode.OK, user);
                return response;
            });
        }
        [HttpPut]

        public HttpResponseMessage PutForgetPassword(HttpRequestMessage request, User value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                User userdetbyusername = null;

                using (IUserService ls = new UserClient())
                {
                    userdetbyusername = ls.GetUserByName(value.UserName);
                }

                UserMasterVO user = null;
                using (IUserService ls = new UserClient())
                {

                    user = ls.ResetUserPassword(userdetbyusername.UserID);
                }

                response = request.CreateResponse<UserMasterVO>(HttpStatusCode.OK, user);
                return response;
            });
        }


        [HttpPut]

        public HttpResponseMessage PutResetUserPassword(HttpRequestMessage request, User value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                UserMasterVO user = null;
                using (IUserService ls = new UserClient())
                {
                    user = ls.ResetUserPassword(value.UserID);
                }

                response = request.CreateResponse<UserMasterVO>(HttpStatusCode.OK, user);
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="imo"></param>
        /// <returns></returns>

        [Route("api/CheckUserExists/{UserTypeID}/{UserName}")]
        [HttpGet]
        [Authorize]
        public HttpResponseMessage CheckUserExists(HttpRequestMessage request, string UserTypeID, string UserName)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                int result = _userService.CheckUserExists(UserTypeID, UserName);
                response = request.CreateResponse(HttpStatusCode.OK, result);
                return response;
            });
        }


        [Route("api/GetAllPortsDetails")]
        [HttpGet]
        public HttpResponseMessage GetAllPortsDetails(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                IEnumerable<PortVO> ports = null;
                ports = _userService.GetAllPortsDetails();
                response = request.CreateResponse<IEnumerable<PortVO>>(HttpStatusCode.OK, ports);
                return response;
            });
        }
    }
}
