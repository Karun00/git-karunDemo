using IPMS.Domain.ValueObjects;
using IPMS.ServiceProxies.Clients;
using IPMS.ServiceProxies.Contracts;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace IPMS.Web.Api
{
    public class BerthPlanningTableController : ApiControllerBase
    {
        IBerthPreSchedulingService _BerthPreSchedulingService;

        public BerthPlanningTableController()
        {
            _BerthPreSchedulingService = new BerthPreSchedulingClient();
        }

        /// <summary>
        /// To Get Reference Data For Berth Planning Table View
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize]
        [Route("api/BerthPlanningTableReferenceData")]
        [HttpGet]
        public HttpResponseMessage GetBerthPlanningTableReferenceVO(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                BerthPlanningTableReferenceVO berthpreschedulingDetails = _BerthPreSchedulingService.GetBerthPlanningTableReferenceVO();
                response = request.CreateResponse(HttpStatusCode.OK, berthpreschedulingDetails);

                return response;
            });
        }

        /// <summary>
        /// To Get VCM List  For Berth Planning Table View
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize]
        [Route("api/GetVCMTableList/{QuayCode}/{BerthCode}/{VesselStatus}/{ETA}/{ToDate}")]
        [HttpGet]
        public HttpResponseMessage GetVCMTableData(HttpRequestMessage request, string QuayCode, string BerthCode, string VesselStatus, string ETA,string ToDate)
        {
            return GetHttpResponse(request, () =>
            {
                string _QuayCode = QuayCode;
                string _BerthCode = BerthCode;
                string _VesselStatus = VesselStatus; 
                HttpResponseMessage response = null;
                List<VCMTableData> VCMTableList = _BerthPreSchedulingService.GetVesselCallMovementsTable(_QuayCode, _BerthCode, _VesselStatus, ETA, ToDate);
                response = request.CreateResponse(HttpStatusCode.OK, VCMTableList);

                return response;

            });
        }
    }
}


