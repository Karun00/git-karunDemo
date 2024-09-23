using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using IPMS.ServiceProxies.Clients;
using IPMS.ServiceProxies.Contracts;
using IPMS.Web.Api;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace IPMS.Web.API
{
    public class VesselRegistrationController : ApiControllerBase
    {

        IVesselRegistrationService _vesselRegService;


        public VesselRegistrationController()
        {
            _vesselRegService = new VesselRegistrationClient();

        }

        /// <summary>
        /// To get vessel reference data
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// 
        [Route("api/ReferenceData")]
        [HttpGet]
        public HttpResponseMessage GetReferenceData(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                VesselRegistrationReferenceVO vesselRefData = _vesselRegService.GetVesselRegistrationReferenceData();
                response = request.CreateResponse(HttpStatusCode.OK, vesselRefData);

                return response;
            });
        }

        /// <summary>
        /// To get veseel registration data
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// 
        [Route("api/VesselRegistration")]
        [HttpGet]
        public HttpResponseMessage GetVesselRegistrationData(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<VesselVO> vesselDet = _vesselRegService.GetVesselRegistrationData();
                response = request.CreateResponse(HttpStatusCode.OK, vesselDet);
                return response;
            });
        }


        [Authorize]
        [Route("api/GetVesselList/{IMONO}/{VesselName}/{PortofRegistry}/{VesselNationality}/{VesselType}/{clallsign}")]
        [HttpGet]
        public HttpResponseMessage GetVesselSearchData(HttpRequestMessage request, string IMONO, string VesselName, string PortofRegistry, string VesselNationality, string VesselType, string clallsign)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<VesselVO> vesselDet = _vesselRegService.GetSearchVesselData(IMONO, VesselName, PortofRegistry, VesselNationality, VesselType, clallsign);
                response = request.CreateResponse(HttpStatusCode.OK, vesselDet);
                return response;
            });

        }

        /// <summary>
        /// To view vessel registration data from pending tasks 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="vcn"></param>
        /// <returns></returns>
        /// 
        [Route("api/VesselRegistration/{vcn}")]
        [HttpGet]
        public HttpResponseMessage GetzVesselRegistrationData(HttpRequestMessage request, string vcn)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<VesselVO> vesselDet = _vesselRegService.GetzVesselRegistrationData(vcn);
                response = request.CreateResponse(HttpStatusCode.OK, vesselDet);
                return response;
            });
        }

        /// <summary>
        /// To add vessel regitration data
        /// </summary>
        /// <param name="request"></param>
        /// <param name="Vesseldata"></param>
        /// <returns></returns>
        /// 
        [Route("api/VesselRegistration")]
        [HttpPost]
        public HttpResponseMessage PostVesselData(HttpRequestMessage request, VesselVO Vesseldata)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                VesselVO vessel = _vesselRegService.AddVesselRegistrationDetails(Vesseldata);
                response = request.CreateResponse<VesselVO>(HttpStatusCode.Created, vessel);
                return response;
            });
        }

        /// <summary>
        /// To modify vessel registration data
        /// </summary>
        /// <param name="request"></param>
        /// <param name="Vesseldata"></param>
        /// <returns></returns>
        /// 
        [Route("api/VesselRegistration")]
        [HttpPut]
        public HttpResponseMessage PutVesselData(HttpRequestMessage request, VesselVO Vesseldata)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                VesselVO vessel = _vesselRegService.ModifyVesselRegistrationDetails(Vesseldata);
                response = request.CreateResponse<VesselVO>(HttpStatusCode.Created, vessel);
                return response;
            });
        }


        #region Workflow Integrated Methods
        /// <summary>
        /// To approve vessel registration request
        /// </summary>
        /// <param name="request"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        /// 
        [Route("api/VesselRegistration/Approve")]
        [HttpPost]
        public HttpResponseMessage ApproveVesselRegistration(HttpRequestMessage request, PendingTaskVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                _vesselRegService.ApproveVesselRegistration(value.ReferenceID, value.Remarks, value.TaskCode);

                response = request.CreateResponse(HttpStatusCode.Created);
                return response;
            });
        }

        /// <summary>
        /// To verify vessel registration request
        /// </summary>
        /// <param name="request"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        /// 
        [Route("api/VesselRegistration/Verify")]
        [HttpPost]
        public HttpResponseMessage VerifyVesselRegistration(HttpRequestMessage request, PendingTaskVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                _vesselRegService.VerifyVesselRegistration(value.ReferenceID, value.Remarks, value.TaskCode);

                response = request.CreateResponse(HttpStatusCode.Created);
                return response;
            });
        }

        /// <summary>
        /// To reject vessel registration request 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        /// 
        [Route("api/VesselRegistration/Reject")]
        [HttpPost]
        public HttpResponseMessage RejectVesselRegistration(HttpRequestMessage request, PendingTaskVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                //using (IAgentService ls = new AgentClient())
                //{
                _vesselRegService.RejectVesselRegistration(value.ReferenceID, value.Remarks, value.TaskCode);
                //}
                response = request.CreateResponse(HttpStatusCode.Created);
                return response;
            });
        }

        #endregion


        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="imo"></param>
        /// <returns></returns>

        [Route("api/CheckIMOExists/{imo}")]
        [HttpGet]
        public HttpResponseMessage CheckIMOExists(HttpRequestMessage request, string imo)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                int result = _vesselRegService.CheckIMOExists(imo);
                response = request.CreateResponse(HttpStatusCode.OK, result);
                return response;
            });
        }

        [Route("api/GetVesselDetailsFromService/{imo}")]
        [HttpGet]
        public HttpResponseMessage GetVesselDetailsFromService(HttpRequestMessage request, string imo)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                VesselVO vslVO = _vesselRegService.GetVesselDetailsFromService(imo);
                response = request.CreateResponse<VesselVO>(HttpStatusCode.OK, vslVO);
                return response;
            });
        }


    }
}
