using IPMS.Domain.ValueObjects;
using IPMS.ServiceProxies.Clients;
using IPMS.ServiceProxies.Contracts;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace IPMS.Web.Api
{
    public class BerthMaintenanceController : ApiControllerBase
    {
        IBerthMaintenanceService _BerthMaintenanceService;
        public BerthMaintenanceController()
        {
            _BerthMaintenanceService = new BerthMaintenanceClient();
        }

        /// <summary>
        /// To get Berth Maintenance Details
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize]
        [Route("api/BerthMaintenances")]
        [HttpGet]       
        public HttpResponseMessage GetBerthMaintenanceDetails(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<BerthMaintenanceVO> BerthMaintenanceList = _BerthMaintenanceService.GetBerthMaintenanceDetails();
                response = request.CreateResponse<List<BerthMaintenanceVO>>(HttpStatusCode.OK, BerthMaintenanceList);
                return response;
            });
        }

        /// <summary>
        /// To Get Berth Maintenance Reference data While initialization
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
       [Authorize]
       [Route("api/BerthMaintenanceReferenceData")]
       [HttpGet]
       public HttpResponseMessage GetBerthMaintenanceReferenceDetails(HttpRequestMessage request)
       {
           return GetHttpResponse(request, () =>
           {
               HttpResponseMessage response = null;
               BerthMaintenanceVO berthmaintenanceDetails = _BerthMaintenanceService.GetBerthMaintenanceReferenceVO();
               response = request.CreateResponse(HttpStatusCode.OK, berthmaintenanceDetails);

               return response;
           });
       }

        /// <summary>
       /// To Add Berth Maintenance Data
        /// </summary>
        /// <param name="request"></param>
        /// <param name="value"></param>
        /// <returns></returns>
       [Authorize]
       [Route("api/BerthMaintenances")]
       [HttpPost]
       public HttpResponseMessage PostBerthMaintenanceData(HttpRequestMessage request,BerthMaintenanceVO value)
       {
           return GetHttpResponse(request, () =>
           {
               HttpResponseMessage response = null;
               BerthMaintenanceVO berthmaintenanceCreated = _BerthMaintenanceService.AddBerthMaintenance(value);
               response = request.CreateResponse<BerthMaintenanceVO>(HttpStatusCode.Created, berthmaintenanceCreated);
               return response;
           });
       }

       /// <summary>
       /// To Modify Berth Maintenance Data
       /// </summary>
       /// <param name="request"></param>
       /// <param name="value"></param>
       /// <returns></returns>
       [Authorize]
       [Route("api/BerthMaintenances")]
       [HttpPut]
       public HttpResponseMessage ModifyBerthMaintenanceData(HttpRequestMessage request, BerthMaintenanceVO value)
       {
           return GetHttpResponse(request, () =>
           {
               HttpResponseMessage response = null;
               BerthMaintenanceVO berthmaintenanceModified = _BerthMaintenanceService.ModifyBerthMaintenance(value);
               response = request.CreateResponse<BerthMaintenanceVO>(HttpStatusCode.Created, berthmaintenanceModified);
               return response;
           });
       }

       /// <summary>
       ///  To Get Bollards based on Berth
       /// </summary>
       /// <param name="request"></param>
       /// <param name="portcode"></param>
       /// <param name="quaycode"></param>
       /// <param name="berthcode"></param>
       /// <returns></returns>
       [Authorize]
       [Route("api/BerthMaintenances/{portcode}/{quaycode}/{berthcode}")]
       [HttpGet]
       public HttpResponseMessage GetBerthBollards(HttpRequestMessage request, string portcode, string quaycode, string berthcode)
       {
           return GetHttpResponse(request, () =>
           {
               HttpResponseMessage response = null;

               List<BollardVO> bollards = _BerthMaintenanceService.GetBerthBollards(portcode, quaycode, berthcode);
               response = request.CreateResponse<List<BollardVO>>(HttpStatusCode.OK, bollards);
               return response;
           });
       }

       /// <summary>
       ///  To get Berth Maintenance Details based on berthmaintenanceid
       /// </summary>
       /// <param name="request"></param>
       /// <param name="berthmaintenanceid"></param>
       /// <returns></returns>
       [Authorize]
       [Route("api/BerthMaintenances/{berthmaintenanceid}")]
       [HttpGet]
       public HttpResponseMessage GetBerthMaintenance(HttpRequestMessage request, int berthmaintenanceid)
       {
           return GetHttpResponse(request, () =>
           {
               HttpResponseMessage response = null;
               List<BerthMaintenanceVO> berthmaintenancetype = _BerthMaintenanceService.GetBerthMaintenance(berthmaintenanceid);
               response = request.CreateResponse<List<BerthMaintenanceVO>>(HttpStatusCode.OK, berthmaintenancetype);
               return response;
           });
       }


       #region Workflow Integrated Methods
       [Route("api/BerthMaintenances/Approve")]
       [HttpPost]
       public HttpResponseMessage ApproveBerthMaintenance(HttpRequestMessage request, PendingTaskVO value)
       {
           return GetHttpResponse(request, () =>
           {
               HttpResponseMessage response = null;

               _BerthMaintenanceService.ApproveBerthMaintenance(value.ReferenceID, value.Remarks, value.TaskCode);

               response = request.CreateResponse(HttpStatusCode.Created);
               return response;
           });
       }

       [Route("api/BerthMaintenances/Reject")]
       [HttpPost]
       public HttpResponseMessage RejectBerthMaintenance(HttpRequestMessage request, PendingTaskVO value)
       {
           return GetHttpResponse(request, () =>
           {
               HttpResponseMessage response = null;           
               _BerthMaintenanceService.RejectBerthMaintenance(value.ReferenceID, value.Remarks, value.TaskCode);
               response = request.CreateResponse(HttpStatusCode.Created);
               return response;
           });
       }     

       #endregion

        
       /// <summary>
       /// To get Workflow Remarks
       /// </summary>
       /// <param name="request"></param>
       /// <param name="workflowinstanceId"></param>
       /// <returns></returns>
       [Authorize]
       [Route("api/WorkFlowRemarks/{workflowinstanceId}")]
       [HttpGet]
       public string GetWorkFlowRemarks(int workflowinstanceId)
       {
           string workflowremarksinfo = _BerthMaintenanceService.GetWorkFlowRemarks(workflowinstanceId);
           return workflowremarksinfo;
       }


    }
}