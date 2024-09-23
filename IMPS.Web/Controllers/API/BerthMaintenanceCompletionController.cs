using IPMS.Domain.ValueObjects;
using IPMS.ServiceProxies.Clients;
using IPMS.ServiceProxies.Contracts;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace IPMS.Web.Api
{
    public class BerthMaintenanceCompletionController : ApiControllerBase
    {

        IBerthMaintenanceCompletionService _BerthMaintenanceCompletionService;

        public BerthMaintenanceCompletionController()
        {
            _BerthMaintenanceCompletionService = new BerthMaintenanceCompletionClient();
        }

        /// <summary>
        /// To get Berth Maintenance Completion Details
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize]
        [Route("api/BerthMaintenanceCompletions")]
        [HttpGet]  
        public HttpResponseMessage GetBerthMaintenanceCompletionList(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<BerthMaintenanceDataVO> BerthMaintComp = _BerthMaintenanceCompletionService.GetBerthMaintenanceCompletionList();
                response = request.CreateResponse<List<BerthMaintenanceDataVO>>(HttpStatusCode.OK, BerthMaintComp);
                return response;
            });
        }

        /// <summary>
        ///  To Add Berth Maintenance Completion Data
        /// </summary>
        /// <param name="request"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [Authorize]
        [Route("api/BerthMaintenanceCompletions")]
        [HttpPost]
        public HttpResponseMessage PostBerthMaintenanceCompletionData(HttpRequestMessage request, BerthMaintenanceCompletionVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                BerthMaintenanceCompletionVO BerthMaintCompCreated = _BerthMaintenanceCompletionService.AddBerthMaintenanceCompletion(value);
                response = request.CreateResponse<BerthMaintenanceCompletionVO>(HttpStatusCode.Created, BerthMaintCompCreated);
                return response;
            });
        }

        /// <summary>
        /// To Modify Berth Maintenance Completion Data
        /// </summary>
        /// <param name="request"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [Authorize]
        [Route("api/BerthMaintenanceCompletions")]
        [HttpPut]
        public HttpResponseMessage ModifyBerthMaintenanceCompletion(HttpRequestMessage request, BerthMaintenanceCompletionVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                BerthMaintenanceCompletionVO BerthMaintCompModified = _BerthMaintenanceCompletionService.ModifyBerthMaintenanceCompletion(value);
                response = request.CreateResponse<BerthMaintenanceCompletionVO>(HttpStatusCode.Created, BerthMaintCompModified);
                return response;
            });
        }

        /// <summary>
        ///  To Get Berth Maintenance Ids
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize]      
        [Route("api/BerthMaintenanceids")]
        [HttpGet]  
        public HttpResponseMessage GetBethMaintenanceIDs(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<DataVO> BerthMaintId = _BerthMaintenanceCompletionService.GetBethMaintenanceIDs();
                response = request.CreateResponse<List<DataVO>>(HttpStatusCode.OK, BerthMaintId);
                return response;
            });
        }

        /// <summary>
        ///  To Get Berth Maintenance Details
        /// </summary>
        /// <param name="request"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        [Route("api/BerthMaintenanceids/{id}")]
        [HttpGet]
        public HttpResponseMessage BethMaintenanceDetails(HttpRequestMessage request,int id)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                IEnumerable<DataVO> BerthMaintId = _BerthMaintenanceCompletionService.BethMaintenanceDetails(id);
                response = request.CreateResponse<IEnumerable<DataVO>>(HttpStatusCode.OK, BerthMaintId);
                return response;
            });
        }
        
        
        
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _BerthMaintenanceCompletionService.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Workflow Integrated Methods
        /// <summary>
        /// To Approve Berth Maintenance Completion
        /// </summary>
        /// <param name="request"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [Route("api/BerthMaintenanceCompletions/Approve")]
        [HttpPost]
        public HttpResponseMessage ApproveBerthMaintenanceCompletion(HttpRequestMessage request, PendingTaskVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                _BerthMaintenanceCompletionService.ApproveBerthMaintenanceCompletion(value.ReferenceID, value.Remarks, value.TaskCode);

                response = request.CreateResponse(HttpStatusCode.Created);
                return response;
            });
        }

        /// <summary>
        ///  To Reject Berth Maintenance Completion
        /// </summary>
        /// <param name="request"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [Route("api/BerthMaintenanceCompletions/Reject")]
        [HttpPost]
        public HttpResponseMessage RejectBerthMaintenanceCompletion(HttpRequestMessage request, PendingTaskVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;                
                _BerthMaintenanceCompletionService.RejectBerthMaintenanceCompletion(value.ReferenceID, value.Remarks, value.TaskCode);
                response = request.CreateResponse(HttpStatusCode.Created);
                return response;
            });
        }

        /// <summary>
        ///  To Get Berth Maintenance Completion Details By ID
        /// </summary>
        /// <param name="request"></param>
        /// <param name="berthmaintenancecompid"></param>
        /// <returns></returns>
        [Authorize]
        [Route("api/BerthMaintenanceCompletions/{id}")]
        [HttpGet]
        public HttpResponseMessage GetBerthMaintenanceCompletion(HttpRequestMessage request, int berthmaintenancecompid)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<BerthMaintenanceDataVO> berthmaintcompletiontype = _BerthMaintenanceCompletionService.GetBerthMaintenanceCompletion(berthmaintenancecompid);
                response = request.CreateResponse<List<BerthMaintenanceDataVO>>(HttpStatusCode.OK, berthmaintcompletiontype);
                return response;
            });
        }
        #endregion
	}
}