using IPMS.Domain.Models;
using IPMS.ServiceProxies.Clients;
using IPMS.ServiceProxies.Contracts;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using IPMS.Web.Adapters;
using IPMS.Domain.ValueObjects;
using WebMatrix.WebData;

namespace IPMS.Web.Api
{
    public class WorkFlowTaskController : ApiControllerBase
    {
        IWorkFlowTaskService _workflowtaskService;
       // ISecurityAdapter _SecurityAdapter;

        public WorkFlowTaskController()
        {
            _workflowtaskService = new WorkFlowTaskClient();
          //  _SecurityAdapter = new SecurityAdapter();
        }

        [Authorize]
        [HttpGet]
        public HttpResponseMessage GetWorkFlowTaskReferenceDetails(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                WorkFlowTaskReferenceVO wftrDetails = _workflowtaskService.GetWorkFlowTaskReferenceVO();
                response = request.CreateResponse(HttpStatusCode.OK, wftrDetails);

                return response;
            });
        }

        [Authorize]
        [HttpGet]
        public HttpResponseMessage GetWorkFlowTaskDetails(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<WorkFlowTaskVO> WFTDetails = _workflowtaskService.GetWorkFlowTasks();
                response = request.CreateResponse<List<WorkFlowTaskVO>>(HttpStatusCode.OK, WFTDetails);
                return response;
            });
        }

        [Route("api/AddWorkFlowTaskData")]
        [Authorize]
        [HttpPost]
        public HttpResponseMessage PostWorkFlowTaskData(HttpRequestMessage request, EntityVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                EntityVO servicerequestCreated = _workflowtaskService.AddWorkFlowTask(value);
                response = request.CreateResponse<EntityVO>(HttpStatusCode.Created, servicerequestCreated);

                return response;
            });
        }

        [Route("api/ModifyWorkFlowTask")]
        [Authorize]
        [HttpPut]
        public HttpResponseMessage PutWorkFlowTaskData(HttpRequestMessage request, EntityVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                EntityVO servicerequestCreated = _workflowtaskService.ModifyWorkFlowTask(value);
                response = request.CreateResponse<EntityVO>(HttpStatusCode.Created, servicerequestCreated);
                return response;
            });
        }

        [Route("api/WorkFlowTasks/{ReferenceID}/{WorkflowInstanceID}")]
        [HttpGet]
        public HttpResponseMessage GetWorkFlowTaskAction(HttpRequestMessage request, string ReferenceID, int WorkflowInstanceID)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                IEnumerable<PendingTaskVO> pendingtaskaction = _workflowtaskService.GetWorkFlowTaskAction(ReferenceID, WorkflowInstanceID);

                response = request.CreateResponse<IEnumerable<PendingTaskVO>>(HttpStatusCode.OK, pendingtaskaction);

                return response;
            });
        }

        [Route("api/WorkFlowTaskData")]
        [Authorize]
        [HttpGet]
        public HttpResponseMessage WorkFlowTaskData(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                List<EntityVO> servicerequestCreated = _workflowtaskService.GetWorkFlowTask();
                response = request.CreateResponse<List<EntityVO>>(HttpStatusCode.Created, servicerequestCreated);

                return response;
            });
        }


        [Route("api/WorkFlowTaskStatus/{ReferenceID}/{WorkflowInstanceID}/{TaskCode}")]
        [Authorize]
        [HttpGet]
        public HttpResponseMessage GetWorkFlowTaskStatus(HttpRequestMessage request, string ReferenceID,int WorkflowInstanceID,string TaskCode)    
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                PendingTaskVO pendingtask= _workflowtaskService.GetWorkFlowTaskStatus(ReferenceID, WorkflowInstanceID, TaskCode);
                Controllers.ChatHubs.ChatHub bHub = new Controllers.ChatHubs.ChatHub();
                bHub.Show();
                response = request.CreateResponse<PendingTaskVO>(HttpStatusCode.OK, pendingtask);
                return response;
            });
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _workflowtaskService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}