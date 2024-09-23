using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using IPMS.ServiceProxies.Clients;
using IPMS.ServiceProxies.Contracts;
using IPMS.Web.Api;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
namespace IPMS.Web.Controllers.API
{
    public class VesselAgentChangeController : ApiControllerBase
    {

        IVesselAgentChangeService _vesselAgentChangeservice;



        public VesselAgentChangeController()
        {
            _vesselAgentChangeservice = new VesselAgentChangeClient();

        }
        /// <summary>
        /// To view change of agent request details data from pending tasks 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="vcn"></param>
        /// <returns></returns>
        /// 
        [Route("api/VesselAgentChange/{vcn}")]
        [HttpGet]
        public HttpResponseMessage GetzVesselAgentChangeRequests(HttpRequestMessage request, string vcn)
        {

            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (!string.IsNullOrEmpty(vcn))
                {
                    List<VesselAgentChangeVO> vesselagentchangeservicedetails = _vesselAgentChangeservice.GetzVesselAgentChangeRequestDetails(vcn);
                    response = request.CreateResponse<List<VesselAgentChangeVO>>(HttpStatusCode.OK, vesselagentchangeservicedetails);
                    return response;
                }
                else return null;
            });

        }
        /// <summary>
        /// To get cahnge of agent request details
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// 
        [Route("api/VesselAgentChange/{etafrom}/{etato}")]
        [HttpGet]
        public HttpResponseMessage GetVesselAgentChangeRequests(HttpRequestMessage request, string etafrom, string etato)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<VesselAgentChangeVO> vesselagentchangeservicedetails = _vesselAgentChangeservice.GetVesselAgentChangeRequestDetails(etafrom, etato);
                response = request.CreateResponse<List<VesselAgentChangeVO>>(HttpStatusCode.OK, vesselagentchangeservicedetails);
                return response;
            });
        }
        /// <summary>
        /// To get approved VCN's
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// 
        [Route("api/VCNDetails")]
        [HttpGet]
        public HttpResponseMessage GetVCNDetails(HttpRequestMessage request)
        {

            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<VesselCallVO> vesselagentchangeservicedetails = _vesselAgentChangeservice.GetVCNDetails();
                response = request.CreateResponse<List<VesselCallVO>>(HttpStatusCode.OK, vesselagentchangeservicedetails);
                return response;
            });

        }
        //To get Active VCN's
        [Route("api/VCNActive")]
        [HttpGet]
        public HttpResponseMessage GetVCNActiveList(HttpRequestMessage request)
        {

            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<VesselCallVO> vesselagentchangeservicedetails = _vesselAgentChangeservice.GetVCNActiveList();
                response = request.CreateResponse<List<VesselCallVO>>(HttpStatusCode.OK, vesselagentchangeservicedetails);
                return response;
            });

        }


        /// <summary>
        /// To Get Vessel Agent reffernce data
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// 
        [Route("api/VesselAgentChangeReferenceData/{mode}")]
        [HttpGet]
        public HttpResponseMessage GetVesselAgentChangeDtl(HttpRequestMessage request, string mode)
        {

            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                VesselAgentReferenceVO vesselagentchangeservicedetails = _vesselAgentChangeservice.GetVesselAgentChangeReferncesVo(mode);
                response = request.CreateResponse(HttpStatusCode.OK, vesselagentchangeservicedetails);

                return response;
            });

        }
        /// <summary>
        /// To add change of agent request details 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        /// 
        [Route("api/VesselAgentChanges")]
        [Authorize]
        [HttpPost]
        public HttpResponseMessage PostVesselAgentChangeData(HttpRequestMessage request, VesselAgentChangeVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                VesselAgentChangeVO vesselagentchangereq = _vesselAgentChangeservice.AddVesselAgentChange(value);
                response = request.CreateResponse<VesselAgentChangeVO>(HttpStatusCode.Created, vesselagentchangereq);
                return response;
            });
        }

        /// <summary>
        /// To verify change of agent request 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        /// 
        [Route("api/VesselAgentChange")]
        [Authorize]
        [HttpPost]
        public HttpResponseMessage PutArrivalNotificationData(HttpRequestMessage request, VesselAgentChangeVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                VesselAgentChangeVO vesselagentchangereq = _vesselAgentChangeservice.ModifyVesselAgentChanges(value);
                response = request.CreateResponse<VesselAgentChangeVO>(HttpStatusCode.Created, vesselagentchangereq);
                return response;
            });
        }

        #region Workflow Integrated Methods
        /// <summary>
        /// To verify change of agent request 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        /// 

        [Route("api/VesselAgentChange/Verify")]
        [HttpPost]
        public HttpResponseMessage VerifyVesselAgentChangeOfRequest(HttpRequestMessage request, PendingTaskVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                //using (IAgentService ls = new AgentClient())
                //{
                _vesselAgentChangeservice.VerifyVesselAgentChangeOfRequest(value.ReferenceID, value.Remarks, value.TaskCode);
                //}
                response = request.CreateResponse(HttpStatusCode.Created);
                return response;
            });
        }
        /// <summary>
        /// To approve change of agent request
        /// </summary>
        /// <param name="request"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        /// 
        [Route("api/VesselAgentChange/Approve")]
        [HttpPost]
        public HttpResponseMessage ApproveVesselAgentChangeOfRequest(HttpRequestMessage request, PendingTaskVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                _vesselAgentChangeservice.ApproveVesselAgentChangeOfRequest(value.ReferenceID, value.Remarks, value.TaskCode);

                response = request.CreateResponse(HttpStatusCode.Created);
                return response;
            });
        }
        /// <summary>
        /// To reject change of agent request 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        /// 
        [Route("api/VesselAgentChange/Reject")]
        [HttpPost]
        public HttpResponseMessage RejectVesselAgentChangeOfRequest(HttpRequestMessage request, PendingTaskVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                _vesselAgentChangeservice.RejectVesselAgentChangeOfRequest(value.ReferenceID, value.Remarks, value.TaskCode);

                response = request.CreateResponse(HttpStatusCode.Created);
                return response;
            });
        }

        #endregion

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _vesselAgentChangeservice.Dispose();
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// TO VALIDATE VCN
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("api/ValidateVCN")]
        [HttpGet]
        public HttpResponseMessage ValidateVCN(HttpRequestMessage request, string vcn)
        {

            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                int result = _vesselAgentChangeservice.ValidateVCN(vcn);
                response = request.CreateResponse(HttpStatusCode.OK, result);
                return response;
            });

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="vcn"></param>
        /// <param name="vesselName"></param>
        /// <param name="etafrom"></param>
        /// <param name="etato"></param>
        /// <returns></returns>
        [Route("api/SearchVesselAgentChange/{vcn}/{vesselName}/{etafrom}/{etato}")]
        [HttpGet]
        public HttpResponseMessage GetVesselAgentChangeRequestsSerarchDtl(HttpRequestMessage request, string vcn, string vesselName, string etafrom, string etato)
        {

            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (!string.IsNullOrEmpty(vcn))
                {
                    List<VesselAgentChangeVO> vesselagentchangeservicedetails = _vesselAgentChangeservice.GetVesselAgentChangeRequestsSearchDetail(vcn, vesselName, etafrom, etato);
                    response = request.CreateResponse<List<VesselAgentChangeVO>>(HttpStatusCode.OK, vesselagentchangeservicedetails);
                    return response;
                }
                else return null;
            });

        }
    }
}

