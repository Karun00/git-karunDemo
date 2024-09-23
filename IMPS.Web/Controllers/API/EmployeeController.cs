using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using IPMS.ServiceProxies.Clients;
using IPMS.ServiceProxies.Contracts;
using IPMS.Web.ServiceProxies.Clients;
using IPMS.Web.ServiceProxies.Contracts;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;


namespace IPMS.Web.Api
{
    public class EmployeeController : ApiControllerBase
    {
        IEmployeeService _employeeService;


        public EmployeeController()
        {
            _employeeService = new EmployeeClient();

        }

        /// <summary>
        /// This method is used to 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        //[Authorize]
        // [Route("api/Employees")]
        [HttpGet]
        public HttpResponseMessage GetEmployeesList(HttpRequestMessage request, string designation, string searchText)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
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
                    List<EmployeeMasterDetails> employeesList = _employeeService.GetEmployeesDetails(designation, searchText);
                    response = request.CreateResponse<List<EmployeeMasterDetails>>(HttpStatusCode.OK, employeesList);
                }
                else
                {
                    //user = null;
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "Unauthorized Access");
                } 
                return response;
            });
        }

        /// <summary>
        /// This method is used for fetches the Department Details
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("api/Departments")]
        [HttpGet]
        public HttpResponseMessage GetDepartments(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<SubCategory> departments = _employeeService.GetDepartments();
                response = request.CreateResponse<List<SubCategory>>(HttpStatusCode.OK, departments);
                return response;
            });
        }

        /// <summary>
        /// This method is used for fetches the Designation Details
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("api/Designations")]
        [HttpGet]
        public HttpResponseMessage GetDesignations(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<SubCategory> designations = _employeeService.GetDesignations();
                response = request.CreateResponse<List<SubCategory>>(HttpStatusCode.OK, designations);
                return response;
            });
        }

        /// <summary>
        /// This method is used for fetches the BusinessUnit Details
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("api/BusinessUnits")]
        [HttpGet]
        public HttpResponseMessage GetBusinessUnits(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<SubCategory> businessunits = _employeeService.GetBusinessUnits();
                response = request.CreateResponse<List<SubCategory>>(HttpStatusCode.OK, businessunits);
                return response;
            });
        }

        /// <summary>
        /// This method is used for fetches the CostCenter Details
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("api/CostCenters")]
        [HttpGet]
        public HttpResponseMessage GetCostCenters(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<SubCategory> costcenters = _employeeService.GetCostCenters();
                response = request.CreateResponse<List<SubCategory>>(HttpStatusCode.OK, costcenters);
                return response;
            });
        }

        /// <summary>
        /// This method is used for fetches the PayrollArea Details
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("api/PayrollAreas")]
        [HttpGet]
        public HttpResponseMessage GetPayrollAreas(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<SubCategory> Payrollareas = _employeeService.GetPayrollAreas();
                response = request.CreateResponse<List<SubCategory>>(HttpStatusCode.OK, Payrollareas);
                return response;
            });
        }

        /// <summary>
        /// This method is used for fetches the PSGroupd Details
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("api/PSGroups")]
        [HttpGet]
        public HttpResponseMessage GetPSGroups(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<SubCategory> psgroups = _employeeService.GetPSGroups();
                response = request.CreateResponse<List<SubCategory>>(HttpStatusCode.OK, psgroups);
                return response;
            });
        }

        /// <summary>
        /// This method is used for fetches the PersonalSubAreas Details
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("api/PersonalSubAreas")]
        [HttpGet]
        public HttpResponseMessage GetPersonalSubAreas(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<SubCategory> personalsubareas = _employeeService.GetPersonalSubAreas();
                response = request.CreateResponse<List<SubCategory>>(HttpStatusCode.OK, personalsubareas);
                return response;
            });
        }

        /// <summary>
        /// This method is used for fetches the OrganizationalUnit Details
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("api/OrganizationalUnits")]
        [HttpGet]
        public HttpResponseMessage GetOrganizationalUnits(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<SubCategory> organizationalunits = _employeeService.GetOrganizationalUnits();
                response = request.CreateResponse<List<SubCategory>>(HttpStatusCode.OK, organizationalunits);
                return response;
            });
        }

        /// <summary>
        /// This method is used for Insert the data
        /// </summary>
        /// <param name="request"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [Route("api/Employees")]
        [HttpPost]
        public HttpResponseMessage PostEmployeeData(HttpRequestMessage request, Employee value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                //value.CreatedDate = DateTime.Now;
                //value.CreatedBy = 1;
                //value.ModifiedDate = DateTime.Now;
                //value.ModifiedBy = 1;
                value.OfficialMobileNo = value.OfficialMobileNo.Replace("(", "").Replace(")", "").Replace("-", "");
                value.PersonalMobileNo = value.PersonalMobileNo.Replace("(", "").Replace(")", "").Replace("-", "");
                Employee employeeCreated = _employeeService.AddEmployee(value);
                response = request.CreateResponse<Employee>(HttpStatusCode.Created, employeeCreated);
                return response;
            });
        }

        /// <summary>
        /// This method is used for Update the data
        /// </summary>
        /// <param name="request"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [Route("api/Employees")]
        [HttpPut]
        public HttpResponseMessage ModifyEmployeeData(HttpRequestMessage request, Employee value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                //value.CreatedDate = DateTime.Now;
                //value.CreatedBy = 1;
                //value.ModifiedDate = DateTime.Now;
                //value.ModifiedBy = 1;
                value.OfficialMobileNo = value.OfficialMobileNo.Replace("(", "").Replace(")", "").Replace("-", "");
                value.PersonalMobileNo = value.PersonalMobileNo.Replace("(", "").Replace(")", "").Replace("-", "");
                Employee employeeModified = _employeeService.ModifyEmployee(value);
                response = request.CreateResponse<Employee>(HttpStatusCode.Created, employeeModified);
                return response;
            });
        }

        /// <summary>
        /// This method is used for delete the data
        /// </summary>
        /// <param name="request"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [Route("api/Employees")]
        [HttpDelete]
        public HttpResponseMessage PutDeleteEmployee(HttpRequestMessage request, Employee value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                Employee employeeDelete = _employeeService.DeleteEmployeeById(value);
                response = request.CreateResponse<Employee>(HttpStatusCode.OK, employeeDelete);
                return response;
            });
        }

        /// <summary>
        /// This method is used for verifying SAP Number
        /// </summary>
        /// <param name="request"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage GetValidateSAPNumber(HttpRequestMessage request, string value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                bool sapnumber = _employeeService.ValidateSapNumber(value);
                response = request.CreateResponse(HttpStatusCode.OK, sapnumber);
                return response;
            });
        }

        /// <summary>
        /// This method is used for verifying SAP Number
        /// </summary>
        /// <param name="request"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [Route("api/FindEmployeeIDInResourceGroup/{employeeid}")]
        [HttpGet]
        public HttpResponseMessage FindEmployeeIdInResourceGroup(HttpRequestMessage request, string employeeId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                bool foundEmployee = _employeeService.FindEmployeeIdInResourceGroup(int.Parse(employeeId, CultureInfo.InvariantCulture));
                response = request.CreateResponse(HttpStatusCode.OK, foundEmployee);
                return response;
            });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _employeeService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}