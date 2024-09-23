using IPMS.Domain.ValueObjects;
using IPMS.Web.ServiceProxies.Clients;
using IPMS.Web.ServiceProxies.Contracts;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using IPMS.ServiceProxies.Clients;
using IPMS.ServiceProxies.Contracts;
using System.Threading;

namespace IPMS.Web.Api
{
    public class BerthPlanningController : ApiControllerBase
    {
        IBerthPlanningService _berthPlanningService;
        IAccountService _accountService;

        public BerthPlanningController()
        {
            _berthPlanningService = new BerthPlanningClient();
            _accountService = new AccountClient();

        }

        /// <summary>
        /// Get Quays List
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize]
        [Route("api/QuaysForBerthPlanning")]
        [HttpGet]
        public HttpResponseMessage GetQuaysInPort(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<QuayVO> quaysInBerth = _berthPlanningService.GetQuaysInPort();
                response = request.CreateResponse<List<QuayVO>>(HttpStatusCode.OK, quaysInBerth);
                return response;
            });
        }


        [Authorize]
        [Route("api/UserDetails")]
        [HttpGet]
        public HttpResponseMessage UserDetails(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                UserData userdata = _berthPlanningService.GetUserDetails();
                response = request.CreateResponse<UserData>(HttpStatusCode.OK, userdata);
                return response;
            });
        }



        [Authorize]
        [Route("api/GetBerthPlanningConfiguration")]
        [HttpGet]
        public HttpResponseMessage BerthPlanningConfigurations(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<BerthPlanningConfiguration> bpcdata = _berthPlanningService.GetBerthPlanningConfigurations();
                response = request.CreateResponse<List<BerthPlanningConfiguration>>(HttpStatusCode.OK, bpcdata);
                return response;
            });
        }

        /// <summary>
        /// Get Berths Bollards List
        /// </summary>
        /// <param name="request"></param>
        ///  /// <param name="quaycode"></param>
        /// <returns></returns>
        [Authorize]
        [Route("api/BerthsBollards/{quaycode}")]
        [HttpGet]
        public HttpResponseMessage GetBerthswithBollard(HttpRequestMessage request, string quaycode)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<BerthsData> bollardsInBerths = _berthPlanningService.GetBerthsWithBollard(quaycode);
                response = request.CreateResponse<List<BerthsData>>(HttpStatusCode.OK, bollardsInBerths);
                return response;
            });
        }





        [Authorize]
        [Route("api/QuaysBerthsBollards")]
        [HttpGet]
        public HttpResponseMessage GetQuayBerthswithBollard(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<QuayBerthBollardData> bollardsInBerths = _berthPlanningService.GetQuayBerthsBollard();
                response = request.CreateResponse<List<QuayBerthBollardData>>(HttpStatusCode.OK, bollardsInBerths);
                return response;
            });
        }


        [Authorize]
        [Route("api/CheckBerthAvailability")]
        [HttpGet]
        public HttpResponseMessage CheckBerthAvailability(HttpRequestMessage request, string VCN, string VCMID, string QuayCode, string FromBerthCode, string FromBollardMeter, string ToBerthCode, string ToBollardMeter, string FromTime, string ToTime)
        {
            return GetHttpResponse(request, () =>
            {

                HttpResponseMessage response = null;
                Boolean VesselCallDetails = _berthPlanningService.CheckBerthAvailability(VCN, VCMID, QuayCode, FromBerthCode, FromBollardMeter, ToBerthCode, ToBollardMeter, FromTime, ToTime);
                response = request.CreateResponse<Boolean>(HttpStatusCode.OK, VesselCallDetails);
                return response;
            });
        }

        /// <summary>
        /// Get VesselCallMovements List
        /// </summary>
        /// <param name="request"></param>
        /// <param name="fromdate"></param>
        ///<param name="todate"></param>
        /// <returns></returns>
        [Authorize]
        [Route("api/GetVesselCallMovements/{fromdate}/{todate}")]
        [HttpGet]
        public HttpResponseMessage GetVesselCallDetails(HttpRequestMessage request, string fromdate, string todate)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                List<BerthPlanningVO> VesselCallDetails = _berthPlanningService.GetVesselInformation(fromdate, todate);
                response = request.CreateResponse<List<BerthPlanningVO>>(HttpStatusCode.OK, VesselCallDetails);
                return response;
            });
        }

        /// <summary>
        /// Update VesselCallMovementInformation
        /// </summary>
        /// <param name="request"></param>
        /// <param name="plannedvessel"></param>
        /// <returns></returns>
        [Authorize]
        [Route("api/SaveVesselCallMovement")]
        [HttpPut]
        public HttpResponseMessage SaveVesselCallMovements(HttpRequestMessage request, [FromBody] List<BerthPlanningVO> plannedvessels)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<BerthPlanningVO> vesselsWithSuitableBerths = _berthPlanningService.SaveVesselCallMovements(plannedvessels);
                Controllers.ChatHubs.ChatHub bHub = new Controllers.ChatHubs.ChatHub();
                bHub.Show();
                response = request.CreateResponse<List<BerthPlanningVO>>(HttpStatusCode.OK, plannedvessels);
                return response;
            });
        }

        [Authorize]
        [Route("api/GetBerthMaintainenceRequests/{fromdate}/{todate}")]
        [HttpGet]
        public HttpResponseMessage GetBerthMaintainenceRequests(HttpRequestMessage request, string fromdate, string todate)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                List<BerthMaintenanceData> BerthMaintenance = _berthPlanningService.GetBerthMaintenance(fromdate, todate);
                response = request.CreateResponse<List<BerthMaintenanceData>>(HttpStatusCode.OK, BerthMaintenance);
                return response;
            });
        }

        [Authorize]
        [Route("api/GetBerthPlanningPrivileges")]
        [HttpGet]
        public HttpResponseMessage GetBerthPlanningPrivileges(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                PrivilegeVO privilege = new PrivilegeVO();
                string result = "false";
                using (IAccountService _accountService = new AccountClient())
                {
                    string username = Thread.CurrentPrincipal.Identity.Name;
                    string controllername = this.GetType().Name;
                    controllername = controllername.Replace("Controller", "");
                    privilege.Privileges = _accountService.GetUserPrivilegesWithControllerName(controllername, username);
                    if (privilege.HasAddPrivilege || privilege.HasEditPrivilege)
                        result = "true";
                }

                response = request.CreateResponse<string>(HttpStatusCode.OK, result);
                return response;
            });
        }

        /// <summary>
        /// Get Quays List
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize]
        [Route("api/GetGisMapPath/{portcodegis}")]
        [HttpGet]
        public HttpResponseMessage GetGisMapPath(HttpRequestMessage request, string portcodegis)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                GISMapPathVo gisMapPath = _berthPlanningService.GetGisMapPath(portcodegis);
                response = request.CreateResponse<GISMapPathVo>(HttpStatusCode.OK, gisMapPath);
                return response;
            });
        }

        /// <summary>
        /// Get VesselCallMovements List
        /// </summary>
        /// <param name="request"></param>
        /// <param name="fromdate"></param>
        ///<param name="todate"></param>
        /// <returns></returns>
        [Authorize]
        [Route("api/GetVesselCallMovementsGIS/{portcode}/{fromdate}/{todate}")]
        [HttpGet]
        public HttpResponseMessage GetVesselCallDetailsGIS(HttpRequestMessage request, string portcode,string fromdate, string todate)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                List<BerthPlanningVO> VesselCallDetails = _berthPlanningService.GetVesselInformationGIS(portcode,fromdate, todate);
                response = request.CreateResponse<List<BerthPlanningVO>>(HttpStatusCode.OK, VesselCallDetails);
                return response;
            });

        }

        //Anchored vessel code


        [Authorize]
        [Route("api/GetAnchorVesselInfoGIS/{portcode}")]
        [HttpGet]
        public HttpResponseMessage GetVesselCallDetailsGIS(HttpRequestMessage request, string portcode)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                List<AnchorVesselInfoGISVO> AnchorVesselCallDetails = _berthPlanningService.GetAnchorVesselInformationGIS(portcode);
                response = request.CreateResponse<List<AnchorVesselInfoGISVO>>(HttpStatusCode.OK, AnchorVesselCallDetails);
                return response;
            });

        }

        [Authorize]
        [Route("api/GetBerthedVesselDetails/{portcode}")]
        [HttpGet]
        public HttpResponseMessage GetBerthedVesselDetails(HttpRequestMessage request, string portcode)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                List<BerthedVessels> GetBerthedDtls = _berthPlanningService.GetBerthedVesselDetails(portcode);

                response = request.CreateResponse<List<BerthedVessels>>(HttpStatusCode.OK, GetBerthedDtls);
                return response;
            });
        }
        [Authorize]
        [Route("api/GetSailedVesselDetails/{portcode}")]
        [HttpGet]
        public HttpResponseMessage GetSailedVesselDetails(HttpRequestMessage request, string portcode)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                List<SailedVessels> GetSailedDtls = _berthPlanningService.GetSailedVesselDetails(portcode);

                response = request.CreateResponse<List<SailedVessels>>(HttpStatusCode.OK, GetSailedDtls);
                return response;
            });
        }
        [Authorize]
        [Route("api/GetAnchoredVesselDetails/{portcode}")]
        [HttpGet]
        public HttpResponseMessage GetAnchoredVesselDetails(HttpRequestMessage request, string portcode)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                List<AnchoredVessels> GetAnchoredDtls = _berthPlanningService.GetAnchoredVesselDetails(portcode);

                response = request.CreateResponse<List<AnchoredVessels>>(HttpStatusCode.OK, GetAnchoredDtls);
                return response;
            });
        }
    }
}

