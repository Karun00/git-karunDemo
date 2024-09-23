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
using System.Web;


namespace IPMS.Web.API
{
    public class ServiceRequestController : ApiControllerBase
    {
        IServiceRequestService _servicerequestService;
       
        public ServiceRequestController()
        {
            _servicerequestService = new ServiceRequestClient();
           
        }
        [HttpGet]
        public HttpResponseMessage GetVCNDetails(HttpRequestMessage request)
        {

            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<ServiceRequestVCNDetails> vcnDtls = _servicerequestService.GetVCNDetails();
                response = request.CreateResponse<List<ServiceRequestVCNDetails>>(HttpStatusCode.OK, vcnDtls);
                return response;
            });
        }
        [HttpGet]
        public HttpResponseMessage GetVCNDetailsForServiceRequest(HttpRequestMessage request)
        {

            return GetHttpResponse(request, () =>
            {              

                HttpResponseMessage response = null;

                string searchValue = HttpContext.Current.Request.Params["filter[filters][0][value]"];
                List<ServiceRequestVCNDetails> vcnDtls = _servicerequestService.GetVCNDetailsForServiceRequest(searchValue);
                response = request.CreateResponse<List<ServiceRequestVCNDetails>>(HttpStatusCode.OK, vcnDtls);
                return response;
            });
        }
        [Route("api/ServiceRequests/{frmdate}/{todate}/{vcnSearch}/{vesselName}/{MovementType}")]            
        [HttpGet]
        public HttpResponseMessage GetServiceRequestDetails(HttpRequestMessage request, string frmdate, string todate, string vcnSearch, string vesselName, string MovementType)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<ServiceRequest_VO> srDtls = _servicerequestService.GetServiceRequestDetails(frmdate, todate, vcnSearch, vesselName, MovementType);
                response = request.CreateResponse<List<ServiceRequest_VO>>(HttpStatusCode.OK, srDtls);
                return response;
            });
        }
        [Route("api/ServiceRequestsGB")]
        [HttpGet]
        public HttpResponseMessage GetBerthBollards(HttpRequestMessage request, string BerthCode)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<BollardVO> momettype = _servicerequestService.GetBollardAtBerth(BerthCode);
                response = request.CreateResponse<List<BollardVO>>(HttpStatusCode.OK, momettype);
                return response;
            });
        }
        [Route("api/ServiceRequestsGC")]
        [HttpGet]
        public HttpResponseMessage GetCurrentBerthsnBollardDtls(HttpRequestMessage request,string vcn)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                List<ServiceRequestCureentBerthnBollards> srbbs = _servicerequestService.GetCurrentBerthAndBollards(vcn);
                response = request.CreateResponse<List<ServiceRequestCureentBerthnBollards>>(HttpStatusCode.OK, srbbs);
                return response;
            });
        }
        [Route("api/GetReferenceData")]
        [HttpGet]
        public HttpResponseMessage GetMovementTypes(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                ServiceRequestVO momettype = _servicerequestService.GetServiceRequestReferenceData();
                response = request.CreateResponse<ServiceRequestVO>(HttpStatusCode.OK, momettype);
                return response;
            });
        }
        [Authorize]
        [Route("api/ServiceRequests")]
        [HttpPost]
        public HttpResponseMessage PostServiceRequestData(HttpRequestMessage request, ServiceRequest_VO value)
        {

            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                value.CreatedDate = DateTime.Now;
                value.ModifiedDate = DateTime.Now;

                List<ServiceRequest_VO> servicerequestCreated = _servicerequestService.AddServiceRequest(value);
                response = request.CreateResponse<List<ServiceRequest_VO>>(HttpStatusCode.Created, servicerequestCreated);
                return response;

            });


        }

        [Authorize]
        [Route("api/ServiceRequests")]
        [HttpPut]
        public HttpResponseMessage ModifyServiceRequestData(HttpRequestMessage request, ServiceRequest_VO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                value.CreatedDate = DateTime.Now;
                value.ModifiedDate = DateTime.Now;
                List<ServiceRequest_VO> servicerequestCreated = _servicerequestService.ModifyServiceRequest(value);
                response = request.CreateResponse<List<ServiceRequest_VO>>(HttpStatusCode.Created, servicerequestCreated);
                return response;
            });
        }

        [Authorize]
        [Route("api/ServiceRequests/GridCancel")]
        [HttpPost]
        public HttpResponseMessage Cancel(HttpRequestMessage request, ServiceRequest_VO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                value.CreatedDate = DateTime.Now;
                value.ModifiedDate = DateTime.Now;

                List<ServiceRequest_VO> servicerequestCreated = _servicerequestService.Cancel(value);
                response = request.CreateResponse<List<ServiceRequest_VO>>(HttpStatusCode.Created, servicerequestCreated);


                return response;
            });
        }
        [Route("api/ServiceRequests/{serviceid}")]
        [HttpGet]
        public HttpResponseMessage GetServicerequest(HttpRequestMessage request, string serviceid)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<ServiceRequest_VO> servicetype = _servicerequestService.GetServiceRequest(serviceid);
                response = request.CreateResponse<List<ServiceRequest_VO>>(HttpStatusCode.OK, servicetype);
                return response;
            });
        }

        [Route("api/ServiceRequestPreferredSlot")]
        [HttpGet]
        public HttpResponseMessage GetPreferredSlot(HttpRequestMessage request, string PreferredDate)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                SlotVO slottype = _servicerequestService.GetPreferredSlot(PreferredDate);
                response = request.CreateResponse<SlotVO>(HttpStatusCode.OK, slottype);
                return response;
            });
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _servicerequestService.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Workflow Integrated Methods
        [Route("api/ServiceRequests/Approve")]
        [HttpPost]
        public HttpResponseMessage ApproveServiceRequest(HttpRequestMessage request, PendingTaskVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                _servicerequestService.ApproveServiceRequest(value.ReferenceID, value.Remarks, value.TaskCode);
                response = request.CreateResponse(HttpStatusCode.Created);
                return response;
            });
        }

        [Route("api/ServiceRequests/Reject")]
        public HttpResponseMessage RejectServiceRequest(HttpRequestMessage request, PendingTaskVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                _servicerequestService.RejectServiceRequest(value.ReferenceID, value.Remarks, value.TaskCode);
                Controllers.ChatHubs.ChatHub bHub = new Controllers.ChatHubs.ChatHub();
                bHub.Show();
                response = request.CreateResponse(HttpStatusCode.Created);
                return response;
            });
        }

        [Route("api/ServiceRequests/Confirm")]
        public HttpResponseMessage ConfirmServiceRequest(HttpRequestMessage request, PendingTaskVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                _servicerequestService.ConfirmServiceRequest(value.ReferenceID, value.Remarks, value.TaskCode);
                //Mahesh: For Planned Movements auto refresh on desktop
                Controllers.ChatHubs.ChatHub bHub = new Controllers.ChatHubs.ChatHub();
                bHub.Show();
                //End
                response = request.CreateResponse(HttpStatusCode.Created);
                return response;
            });
        }

        [Route("api/ServiceRequests/Cancel")]
        public HttpResponseMessage CancelServiceRequest(HttpRequestMessage request, PendingTaskVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                _servicerequestService.CancelServiceRequest(value.ReferenceID, value.Remarks, value.TaskCode);
                Controllers.ChatHubs.ChatHub bHub = new Controllers.ChatHubs.ChatHub();
                bHub.Show();
                response = request.CreateResponse(HttpStatusCode.Created);
                return response;
            });
        }

        [Route("api/ServiceRequests/ConfirmCancel")]
        public HttpResponseMessage ConfirmCancelServiceRequest(HttpRequestMessage request, PendingTaskVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                _servicerequestService.ConfirmCancelServiceRequest(value.ReferenceID, value.Remarks, value.TaskCode);
                Controllers.ChatHubs.ChatHub bHub = new Controllers.ChatHubs.ChatHub();
                bHub.Show();
                response = request.CreateResponse(HttpStatusCode.Created);
                return response;
            });
        }

        [Route("api/ServiceRequests/CancelApprove")]
        [HttpPost]
        public HttpResponseMessage CancelApproveServiceRequest(HttpRequestMessage request, PendingTaskVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                _servicerequestService.CancelApproveServiceRequest(value.ReferenceID, value.Remarks, value.TaskCode);
                response = request.CreateResponse(HttpStatusCode.Created);
                return response;
            });
        }

        [Route("api/ServiceRequests/CancelReject")]
        public HttpResponseMessage CancelRejectServiceRequest(HttpRequestMessage request, PendingTaskVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                _servicerequestService.CancelRejectServiceRequest(value.ReferenceID, value.Remarks, value.TaskCode);
                Controllers.ChatHubs.ChatHub bHub = new Controllers.ChatHubs.ChatHub();
                bHub.Show();
                response = request.CreateResponse(HttpStatusCode.Created);
                return response;
            });
        }

        [Route("api/ServiceRequests/Shifting/Approve")]
        [HttpPost]
        public HttpResponseMessage ApproveServiceRequestShifting(HttpRequestMessage request, PendingTaskVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                _servicerequestService.ApproveServiceRequestShifting(value.ReferenceID, value.Remarks, value.TaskCode);
                response = request.CreateResponse(HttpStatusCode.Created);
                return response;
            });
        }

        [Route("api/ServiceRequests/Shifting/Reject")]
        public HttpResponseMessage RejectServiceRequestShiting(HttpRequestMessage request, PendingTaskVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                _servicerequestService.RejectServiceRequestShiting(value.ReferenceID, value.Remarks, value.TaskCode);
                Controllers.ChatHubs.ChatHub bHub = new Controllers.ChatHubs.ChatHub();
                bHub.Show();
                response = request.CreateResponse(HttpStatusCode.Created);
                return response;
            });
        }


        #endregion

    }
}
