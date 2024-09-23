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
using System.ServiceModel;
using System.Web;
using System.Linq;
using System.Threading;



namespace IPMS.Web.API
{
    public class DashBoardController : ApiControllerBase
    {
        IDashBoardService _dashboardService;
      
        public DashBoardController()
        {
            _dashboardService = new DashBoardClient();
           
        }
        
        [HttpGet]
        public HttpResponseMessage GetReportPeriod(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                string val = _dashboardService.GetReportPeriod();
                response = request.CreateResponse(HttpStatusCode.OK, val);
                return response;
            });
        }

        [Authorize]
        [Route("api/WegoDashBoradData/{FromDate}/{ToDate}")]
        [HttpGet]
        public HttpResponseMessage GetWegoDashBoradDetails(HttpRequestMessage request, DateTime fromDate, DateTime toDate)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<WegoDashBoardVO> wegodetails = null;
                PrivilegeVO privilege = new PrivilegeVO();
                using (IAccountService _accountService = new AccountClient())
                {
                    string username = Thread.CurrentPrincipal.Identity.Name;
                    string controllername = "WegoBerthDashBoard";   // TO DO : Need to Create a new  Controller and Service for Wego DashBoard                    
                    privilege.Privileges = _accountService.GetUserPrivilegesWithControllerName(controllername, username);
                }

                if (privilege.Privileges.Contains("VIEW"))
                {
                    wegodetails = _dashboardService.GetWegoDashBoradDetails(fromDate, toDate);
                    response = request.CreateResponse<List<WegoDashBoardVO>>(HttpStatusCode.OK, wegodetails);
                }
                else
                {                    
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "Unauthorized Access");
                }

                return response;               
            });
        }        


        [Authorize]
        [Route("api/WegoBerthUtilizationData/{FromDate}/{ToDate}")]
        [HttpGet]
        public HttpResponseMessage GetWegoBerthUtilizationData(HttpRequestMessage request, string fromDate, string toDate)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<WegoBerthUtilizationVO> wegodetails = null;
                PrivilegeVO privilege = new PrivilegeVO();
                using (IAccountService _accountService = new AccountClient())
                {
                    string username = Thread.CurrentPrincipal.Identity.Name;
                    string controllername = "WegoBerthDashBoard";     // TO DO : Need to Create a new  Controller and Service for Wego DashBoard            
                    privilege.Privileges = _accountService.GetUserPrivilegesWithControllerName(controllername, username);
                }

                if (privilege.Privileges.Contains("VIEW"))
                {
                    wegodetails = _dashboardService.GetWegoBerthUtilizationData(fromDate, toDate);
                    response = request.CreateResponse<List<WegoBerthUtilizationVO>>(HttpStatusCode.OK, wegodetails);
                }
                else
                {
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "Unauthorized Access");
                }

                return response;                
            });
        }

        [Authorize]
        [Route("api/CargoTypeDashboard/{FromDate}/{ToDate}/{portcode}")]
        [HttpGet]
        public HttpResponseMessage CargoTypeDashboard(HttpRequestMessage request, string fromDate, string toDate, string portcode)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<CargoTypeDashboardVO> cargodetails = null;
                PrivilegeVO privilege = new PrivilegeVO();
                using (IAccountService _accountService = new AccountClient())
                {
                    string username = Thread.CurrentPrincipal.Identity.Name;
                    string controllername = "WegoBerthDashBoard";     // TO DO : Need to Create a new  Controller and Service for Wego DashBoard            
                    privilege.Privileges = _accountService.GetUserPrivilegesWithControllerName(controllername, username);
                }

                if (privilege.Privileges.Contains("VIEW"))
                {
                    cargodetails = _dashboardService.CargoTypeDashboard(fromDate, toDate, portcode);
                    response = request.CreateResponse<List<CargoTypeDashboardVO>>(HttpStatusCode.OK, cargodetails);
                }
                else
                {
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "Unauthorized Access");
                }

                return response;
            });
        }

        ////Total Movements Dashboard
        [Authorize]
        [Route("api/TotalMovementsDashboard/{FromDate}/{ToDate}")]
        [HttpGet]
        public HttpResponseMessage TotalMovementsDashboardDetails(HttpRequestMessage request, DateTime fromDate, DateTime toDate)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<TotalMovementsDashBoardVO> wegodetails = null;
                PrivilegeVO privilege = new PrivilegeVO();
                using (IAccountService _accountService = new AccountClient())
                {
                    string username = Thread.CurrentPrincipal.Identity.Name;
                    string controllername = "WegoBerthDashBoard";   // TO DO : Need to Create a new  Controller and Service for Wego DashBoard                    
                    privilege.Privileges = _accountService.GetUserPrivilegesWithControllerName(controllername, username);
                }

                if (privilege.Privileges.Contains("VIEW"))
                {
                    wegodetails = _dashboardService.TotalMovementsDashboardDetails(fromDate, toDate);
                    response = request.CreateResponse<List<TotalMovementsDashBoardVO>>(HttpStatusCode.OK, wegodetails);
                }
                else
                {
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "Unauthorized Access");
                }

                return response;
            });
        }
    //AllPorts
        [Route("api/getAllPort")]
        [HttpGet]
        public HttpResponseMessage GetAllPorts(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<GetAllPorts> getallports = null;
                getallports = _dashboardService.GetAllPorts();
                response = request.CreateResponse<List<GetAllPorts>>(HttpStatusCode.OK, getallports);
                return response;
            });
        }
       protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _dashboardService.Dispose();
            }
            base.Dispose(disposing);
        }      

    }
}
