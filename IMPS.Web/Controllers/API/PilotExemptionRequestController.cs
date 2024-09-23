using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using IPMS.ServiceProxies.Clients;
using IPMS.ServiceProxies.Contracts;
using IPMS.Web.Api;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace IPMS.Web.API
{
    public class PilotExemptionRequestController : ApiControllerBase
    {
        IPilotExemptionRequestService _pilotexemptionrequestservice;

        public PilotExemptionRequestController()
        {
            _pilotexemptionrequestservice = new PilotExemptionRequestClient();

        }

        #region GetPilotExemptionRequestReferencesData
        /// <summary>
        /// To Get Pilot Exemption Request Reference data
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("api/PilotExemptionRequestReferencesData")]
        public HttpResponseMessage GetPilotExemptionRequestReferencesData(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
           {
               HttpResponseMessage response = null;
               PilotexemptionRequestReferenceVO pilotexemptionrequest = _pilotexemptionrequestservice.GetPilotExemptionRequestReferencesVO();
               response = request.CreateResponse<PilotexemptionRequestReferenceVO>(HttpStatusCode.OK, pilotexemptionrequest);
               return response;
           });
        }
        #endregion

        #region PostAddPilotExemptionRequest
        /// <summary>
        /// To Add Pilot Exemption Request 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="PilotExemptionRequestData"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/PilotExemptionRequest")]
        public HttpResponseMessage PostAddPilotExemptionRequest(HttpRequestMessage request, PioltVO pilotexemptionrequestdata)
        {
            return GetHttpResponse(request, () =>
            {
                if (!User.Identity.IsAuthenticated)
                {
                    var anonymousUserId = Convert.ToInt32(ConfigurationManager.AppSettings["AnonymousUserId"]);

                    pilotexemptionrequestdata.ModifiedBy = anonymousUserId; // 1;
                    pilotexemptionrequestdata.CreatedBy = anonymousUserId; // 1;
                }

                HttpResponseMessage response = null;
                PioltVO PilotExemptionRequest = _pilotexemptionrequestservice.AddPilotExemptionRequest(pilotexemptionrequestdata);
                response = request.CreateResponse<PioltVO>(HttpStatusCode.OK, PilotExemptionRequest);
                return response;
            });
        }
        #endregion

        #region ModifyPilotExemptionRequest
        /// <summary>
        /// To Modify Pilot Exemption Request
        /// </summary>
        /// <param name="request"></param>
        /// <param name="PilotExemptionRequestData"></param>
        /// <returns></returns>
        [HttpPut]
        [Authorize]
        [Route("api/PilotExemptionRequest")]
        public HttpResponseMessage ModifyPilotExemptionRequest(HttpRequestMessage request, PioltVO pilotexemptionrequestdata)
        {
            return GetHttpResponse(request, () =>
            {
                if (!User.Identity.IsAuthenticated)
                {
                    var anonymousUserId = Convert.ToInt32(ConfigurationManager.AppSettings["AnonymousUserId"]);

                    pilotexemptionrequestdata.ModifiedBy = anonymousUserId; // 1; 
                    pilotexemptionrequestdata.CreatedBy = anonymousUserId; // 1;
                }

                HttpResponseMessage response = null;
                PioltVO PilotExemptionRequest = _pilotexemptionrequestservice.ModifyPilotExemptionRequest(pilotexemptionrequestdata);
                response = request.CreateResponse<PioltVO>(HttpStatusCode.OK, PilotExemptionRequest);
                return response;
            });
        }
        #endregion

        #region GetPilotExemptionRequestList
        /// <summary>
        /// To Get Pilot Exemption Request List
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("api/PilotExemptionRequestlist")]
        public HttpResponseMessage GetPilotExemptionRequestList(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<PioltVO> pilotexemptionrequest = _pilotexemptionrequestservice.GetPilotExemptionRequestlist();
                response = request.CreateResponse<List<PioltVO>>(HttpStatusCode.OK, pilotexemptionrequest);
                return response;
            });
        }
        #endregion

        #region GetPilotExemptionRequest
        /// <summary>
        /// To Get Pilot Exemption Request List
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public HttpResponseMessage GetPilotExemptionRequest(HttpRequestMessage request, int id)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                PioltVO pilotexemptionrequest = _pilotexemptionrequestservice.GetPilotExemptionRequest(id);
                response = request.CreateResponse<PioltVO>(HttpStatusCode.OK, pilotexemptionrequest);
                return response;
            });
        }
        #endregion

        #region GetVesselNames
        public HttpResponseMessage GetVesselNames(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                string searchValue = HttpContext.Current.Request.Params["filter[filters][0][value]"];
                HttpResponseMessage response = null;
                List<VesselVO> Vessels = _pilotexemptionrequestservice.GetVesselNamesautoComplete(searchValue);
                response = request.CreateResponse<List<VesselVO>>(HttpStatusCode.OK, Vessels);
                return response;
            });
        }
        #endregion

        #region Workflow Integrated Methods

        /// <summary>
        /// To Approve Pilot Exemption Request
        /// </summary>
        /// <param name="request"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage ApprovePilotExemptionRegistration(HttpRequestMessage request, PendingTaskVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                using (IPilotExemptionRequestService ls = new PilotExemptionRequestClient())
                {
                    ls.ApprovePilotExemptionRegistration(value.ReferenceID, value.Remarks, value.TaskCode);
                }
                response = request.CreateResponse(HttpStatusCode.Created);
                return response;
            });
        }

        /// <summary>
        /// To Reject  Pilot Exemption Request
        /// </summary>
        /// <param name="request"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public HttpResponseMessage RejectPilotExemptionRegistration(HttpRequestMessage request, PendingTaskVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                using (IPilotExemptionRequestService ls = new PilotExemptionRequestClient())
                {
                    ls.RejectPilotExemptionRegistration(value.ReferenceID, value.Remarks, value.TaskCode);
                }
                response = request.CreateResponse(HttpStatusCode.Created);
                return response;
            });
        }

        #endregion
    }
}
