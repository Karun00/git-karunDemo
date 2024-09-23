using System.Collections.Generic;
using System.Web.Http;
using IPMS.ServiceProxies.Clients;
using IPMS.ServiceProxies.Contracts;
using IPMS.Domain.ValueObjects;
using IPMS.Web.Api;
using System.Net.Http;
using System.Net;
using IPMS.Domain.Models;

namespace IPMS.Web.Controllers.API
{
    public class SuppDryDockExtensionController : ApiControllerBase
    {
        private ISuppDryDockExtensionService _suppDryDockExtService = null;
        public SuppDryDockExtensionController()
        {
            _suppDryDockExtService = new SuppDryDockExtensionClient();
        }

        [HttpPost]
        [Route("api/SuppDryDockExtension")]
        public HttpResponseMessage PostSuppDryDockExtension(HttpRequestMessage request, SuppDryDockExtensionVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                SuppDryDockExtensionVO suppDryDockVO = _suppDryDockExtService.PostSuppDryDockExtension(value);
                response = request.CreateResponse<SuppDryDockExtensionVO>(HttpStatusCode.Created, suppDryDockVO);

                return response;
            });
        }

        [HttpPut]
        [Route("api/SuppDryDockExtension")]
        public HttpResponseMessage PutSuppDryDockExtension(HttpRequestMessage request, SuppDryDockExtensionVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                SuppDryDockExtensionVO suppDryDockVO = _suppDryDockExtService.PutSuppDryDockExtension(value);
                response = request.CreateResponse<SuppDryDockExtensionVO>(HttpStatusCode.Created, suppDryDockVO);

                return response;
            });
        }

        [HttpGet]
        [Route("api/SuppVCNListForExtension")]
        public HttpResponseMessage GetSuppVCNDetailsForExtension(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                List<ServiceRequestVCNsForDryDockExts> suppDryDockVOs = _suppDryDockExtService.GetSuppVCNDetailsForExtension();
                response = request.CreateResponse<List<ServiceRequestVCNsForDryDockExts>>(HttpStatusCode.OK, suppDryDockVOs);

                return response;
            });
        }
        [Route("api/GetSuppVCNDetailsForExtensionByVCN/{vcn}")]
        [HttpGet]
        public HttpResponseMessage GetSuppVCNDetailsForExtensionByVCN(HttpRequestMessage request, string vcn)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                AgentVO agent = _suppDryDockExtService.GetSuppVCNDetailsForExtensionByVCN(vcn);
                response = request.CreateResponse<AgentVO>(HttpStatusCode.OK, agent);

                return response;
            });
        }

        [HttpGet]
        [Route("api/SuppDryDockExtension")]
        public HttpResponseMessage GetSuppDryDockApplicationList(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                List<ServiceRequestVCNsForDryDockExts> suppDryDockVOs = _suppDryDockExtService.GetSuppDryDockExtensionList();
                response = request.CreateResponse<List<ServiceRequestVCNsForDryDockExts>>(HttpStatusCode.OK, suppDryDockVOs);

                return response;
            });
        }

        [HttpGet]
        [Route("api/SuppDryDockExtension/{SuppDryDockExtensionID}")]
        public HttpResponseMessage GetSuppDryDockExtensionByID(HttpRequestMessage request, string SuppDryDockExtensionID)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                List<ServiceRequestVCNsForDryDockExts> suppDryDockVOs = _suppDryDockExtService.GetSuppDryDockExtensionByID(SuppDryDockExtensionID);
                response = request.CreateResponse<List<ServiceRequestVCNsForDryDockExts>>(HttpStatusCode.OK, suppDryDockVOs);

                return response;
            });
        }

        //List<ServiceRequestVCNsForDryDockExts> GetSuppDryDockExtensionByID(string SuppDryDockExtensionByID)

        #region Workflow Integrated Methods
        /// <summary>
        /// To Approve Supplementary Dry Dock Extension
        /// </summary>
        /// <param name="request"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [Route("api/SuppDryDockExtension/ApproveSuppDryDockExtension")]
        [HttpPost]
        public HttpResponseMessage ApproveSuppDryDockExtension(HttpRequestMessage request, PendingTaskVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                _suppDryDockExtService.ApproveSuppDryDockExtension(value.ReferenceID, value.Remarks, value.TaskCode);

                response = request.CreateResponse(HttpStatusCode.Created);
                return response;
            });
        }

        /// <summary>
        /// To Reject Supplementary Dry Dock Extension
        /// </summary>
        /// <param name="request"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [Route("api/SuppDryDockExtension/RejectSuppDryDockExtension")]
        [HttpPost]
        public HttpResponseMessage RejectSuppDryDockExtension(HttpRequestMessage request, PendingTaskVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                _suppDryDockExtService.RejectSuppDryDockExtension(value.ReferenceID, value.Remarks, value.TaskCode);

                response = request.CreateResponse(HttpStatusCode.Created);
                return response;
            });
        }
        


        //SuppDryDockExtensionID
        #endregion
    }
}