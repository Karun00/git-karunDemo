using System.Collections.Generic;
using System.Web.Http;
using IPMS.ServiceProxies.Clients;
using IPMS.ServiceProxies.Contracts;
using IPMS.Domain.ValueObjects;
using IPMS.Web.Api;
using System.Net.Http;
using System.Net;
using IPMS.Domain.Models;
using System.Web;

namespace IPMS.Web.API
{
    public class SuppDryDockController : ApiControllerBase
    {
        private ISuppDryDockService _suppDryDockService = null;

        public SuppDryDockController()
        {
            _suppDryDockService = new SuppDryDockClient();
        }

        [HttpGet]
        [Route("api/SuppDryDockApplication")]
        public HttpResponseMessage GetSuppDryDockApplicationList(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
              {
                  HttpResponseMessage response = null;

                  List<SuppDryDockVO> suppDryDockVOs = _suppDryDockService.GetSuppDryDockApplicationList();
                  response = request.CreateResponse<List<SuppDryDockVO>>(HttpStatusCode.OK, suppDryDockVOs);

                  return response;
              });
        }

        [HttpGet]
        [Route("api/SuppVCNList")]
        public HttpResponseMessage GetSuppVCNDetails(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                string searchvalue = HttpContext.Current.Request.Params["filter[filters][0][value]"];

                List<ServiceRequestVCNDetails> suppDryDockVOs = _suppDryDockService.GetSuppVCNDetails(searchvalue);
                response = request.CreateResponse<List<ServiceRequestVCNDetails>>(HttpStatusCode.OK, suppDryDockVOs);

                return response;
            });
        }

       

        [HttpPost]
        [Route("api/SuppDryDockApplication")]
        public HttpResponseMessage PostSuppDryDockApplication(HttpRequestMessage request,SuppDryDockVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                SuppDryDockVO suppDryDockVO = _suppDryDockService.PostSuppDryDockApplication(value);
                response = request.CreateResponse<SuppDryDockVO>(HttpStatusCode.Created, suppDryDockVO);

                return response;
            });
        }

        [HttpPut]
        [Route("api/SuppDryDockApplication")]
        public HttpResponseMessage PutSuppDryDockApplication(HttpRequestMessage request, SuppDryDockVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                SuppDryDockVO suppDryDockVO = _suppDryDockService.PutSuppDryDockApplication(value);
                response = request.CreateResponse<SuppDryDockVO>(HttpStatusCode.Created, suppDryDockVO);

                return response;
            });
        }



        [Route("api/GetSuppDryDockVCN/{vcn}")]
        [HttpGet]
        public HttpResponseMessage GetSuppDryDockVCN(HttpRequestMessage request, string vcn)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                SuppDryDockVO suppDryDockVOs = _suppDryDockService.GetSuppDryDockVCN(vcn);
                response = request.CreateResponse<SuppDryDockVO>(HttpStatusCode.OK, suppDryDockVOs);

                return response;
            });
        }

        #region Workflow Integrated Methods
        /// <summary>
        /// To Approve Supplementary Dry Dock
        /// </summary>
        /// <param name="request"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [Route("api/SuppDryDock/Approve")]
        [HttpPost]
        public HttpResponseMessage ApproveSuppDryDock(HttpRequestMessage request, PendingTaskVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                _suppDryDockService.ApproveSuppDryDock(value.ReferenceID, value.Remarks, value.TaskCode);

                response = request.CreateResponse(HttpStatusCode.Created);
                return response;
            });
        }

        /// <summary>
        /// To Reject Supplementary Dry Dock
        /// </summary>
        /// <param name="request"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [Route("api/SuppDryDock/Reject")]
        [HttpPost]
        public HttpResponseMessage RejectSuppDryDock(HttpRequestMessage request, PendingTaskVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                _suppDryDockService.RejectSuppDryDock(value.ReferenceID, value.Remarks, value.TaskCode);
                response = request.CreateResponse(HttpStatusCode.Created);
                return response;
            });
        }

        /// <summary>
        ///  To Confirm Supplementary Dry Dock
        /// </summary>
        /// <param name="request"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [Route("api/SuppDryDock/Confirm")]
        public HttpResponseMessage ConfirmSuppDryDock(HttpRequestMessage request, PendingTaskVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                _suppDryDockService.ConfirmSuppDryDock(value.ReferenceID, value.Remarks, value.TaskCode);
                response = request.CreateResponse(HttpStatusCode.Created);
                return response;
            });
        }

        /// <summary>
        /// To Cancel Supplementary Dry Dock
        /// </summary>
        /// <param name="request"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [Route("api/SuppDryDock/Cancel")]
        public HttpResponseMessage CancelSuppDryDock(HttpRequestMessage request, PendingTaskVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                _suppDryDockService.CancelSuppDryDock(value.ReferenceID, value.Remarks, value.TaskCode);
                response = request.CreateResponse(HttpStatusCode.Created);
                return response;
            });
        }

        /// <summary>   
        ///  To get Supplementary Dry Dock based on suppdrydockid
        /// </summary>
        /// <param name="request"></param>
        /// <param name="dockingplanid"></param>
        /// <returns></returns>
        [Authorize]
        [Route("api/SuppDryDockApplication/{suppdrydockid}")]
        [HttpGet]
        public HttpResponseMessage GetSuppDryDock(HttpRequestMessage request, int SuppDryDockID)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<SuppDryDockVO> dockingplantype = _suppDryDockService.GetSuppDryDock(SuppDryDockID);
                response = request.CreateResponse<List<SuppDryDockVO>>(HttpStatusCode.OK, dockingplantype);
                return response;
            });
        }

        #endregion

        /// <summary>
        /// To Docuemtns types 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
//[Authorize]
        [Route("api/SuppDryDockDocumentTypes")]
        [HttpGet]
        public HttpResponseMessage GetDocumentTypes(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<SubCategory> subCategoryDocTypeDetails = _suppDryDockService.GetDocumentTypes();
                response = request.CreateResponse(HttpStatusCode.OK, subCategoryDocTypeDetails);
                return response;
            });
        }


        //Add by Srinivas
        /// <summary>
        /// To Cancel Grid DockingPlanAplication
        /// </summary>
        /// <param name="request"></param>
        /// <param name="value"></param>
        /// <returns></returns>

        [Authorize]
        [Route("api/DockingPlanApplication/GridCancel")]
        [HttpPost]
        public HttpResponseMessage Cancel(HttpRequestMessage request, SuppDryDockVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                //value.CreatedBy=DateTime.Now;
                //value.ModifiedDate = DateTime.Now;

                SuppDryDockVO servicerequestCreated = _suppDryDockService.Cancel(value);
                response = request.CreateResponse<SuppDryDockVO>(HttpStatusCode.Created, servicerequestCreated);


                return response;
            });
        }

        [Route("api/SuppDryDock/CancelConfirmApprove")]
        [HttpPost]
        public HttpResponseMessage ApproveCancelConfirmSuppDryDock(HttpRequestMessage request, PendingTaskVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                _suppDryDockService.ApproveCancelConfirmSuppDryDock(value.ReferenceID, value.Remarks, value.TaskCode);
                response = request.CreateResponse(HttpStatusCode.Created);
                return response;
            });
        }

        [Route("api/SuppDryDock/CancelConfirmReject")]
        public HttpResponseMessage RejectCancelConfirmSuppDryDock(HttpRequestMessage request, PendingTaskVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                _suppDryDockService.RejectCancelConfirmSuppDryDock(value.ReferenceID, value.Remarks, value.TaskCode);
                Controllers.ChatHubs.ChatHub bHub = new Controllers.ChatHubs.ChatHub();
                bHub.Show();
                response = request.CreateResponse(HttpStatusCode.Created);
                return response;
            });
        }



    }
}
