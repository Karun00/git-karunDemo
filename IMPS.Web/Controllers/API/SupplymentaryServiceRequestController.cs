using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using IPMS.ServiceProxies.Clients;
using IPMS.ServiceProxies.Contracts;
using IPMS.Web.Api;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace IPMS.Web.API
{
    public class SupplymentaryServiceRequestController : ApiControllerBase
    {
        ISupplymentaryServiceRequestService _supplymentaryServiceRequestService;

        public SupplymentaryServiceRequestController()
        {
            _supplymentaryServiceRequestService = new SupplymentaryServiceRequestClient();

        }

        [HttpGet]
        [Route("api/ServiceTypes")]
        public HttpResponseMessage GetServiceTypes(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<SubCategoryVO> lstSubCategoryVO = _supplymentaryServiceRequestService.GetServiceType();
                response = request.CreateResponse<List<SubCategoryVO>>(HttpStatusCode.OK, lstSubCategoryVO);
                return response;
            });
        }

        [Route("api/SupplementaryServiceRequests/{frmdate}/{todate}/{vcnSearch}/{vesselName}")]
        [HttpGet]
        public HttpResponseMessage GetSupplymentaryServiceRequestList(HttpRequestMessage request, string frmdate, string todate, string vcnSearch, string vesselName)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<SuppServiceRequestVO> lstSuppServiceRequestVO = _supplymentaryServiceRequestService.GetSupplymentaryServiceRequestList(frmdate, todate, vcnSearch, vesselName);
                response = request.CreateResponse<List<SuppServiceRequestVO>>(HttpStatusCode.OK, lstSuppServiceRequestVO);
                return response;
            });
        }

        [Route("api/SuppServiceRequestsById/{SuppServiceRequestID}")]
        [HttpGet]
        public HttpResponseMessage GetSupplymentaryServiceRequest(HttpRequestMessage request, string SuppServiceRequestID)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                SuppServiceRequestVO lstSuppServiceRequestVO = _supplymentaryServiceRequestService.GetSupplymentaryServiceRequest(SuppServiceRequestID);
                response = request.CreateResponse<SuppServiceRequestVO>(HttpStatusCode.OK, lstSuppServiceRequestVO);
                return response;
            });
        }

        [Route("api/SupplementaryServiceRequests")]
        [HttpPost]
        public HttpResponseMessage PostSuppServiceRequest(HttpRequestMessage request, SuppServiceRequestVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                //value.RecordStatus = "A";
                //value.CreatedDate = DateTime.Now;
                //value.CreatedBy = 1;
                //value.ModifiedDate = DateTime.Now;
                //value.ModifiedBy = 1;

                SuppServiceRequestVO suppServiceRequestVO = _supplymentaryServiceRequestService.PostSupplymentaryServiceRequest(value);
                response = request.CreateResponse<SuppServiceRequestVO>(HttpStatusCode.Created, suppServiceRequestVO);

                return response;
            });
        }


        [Route("api/SupplementaryServiceRequests")]
        [HttpPut]
        public HttpResponseMessage ModifySuppServiceRequest(HttpRequestMessage request, SuppServiceRequestVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                //value.RecordStatus = "A";
                //value.ModifiedDate = DateTime.Now;
                //value.ModifiedBy = 1;

                SuppServiceRequestVO suppServiceRequestVO = _supplymentaryServiceRequestService.ModifySupplymentaryServiceRequest(value);
                response = request.CreateResponse<SuppServiceRequestVO>(HttpStatusCode.Created, suppServiceRequestVO);

                return response;
            });
        }

        #region Workflow Integrated Methods
        /// <summary>
        /// Approves the SupplymentaryServiceRequest
        /// </summary>
        /// <param name="request"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage ApproveSupplymentaryServiceRequest(HttpRequestMessage request, PendingTaskVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                using (ISupplymentaryServiceRequestService ls = new SupplymentaryServiceRequestClient())
                {
                    ls.ApproveSupplymentaryServiceRequest(value.ReferenceID, value.Remarks, value.TaskCode);
                }
                response = request.CreateResponse(HttpStatusCode.Created);
                return response;
            });
        }
        /// <summary>
        /// Verifies the SupplymentaryServiceRequest
        /// </summary>
        /// <param name="request"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage VerifySupplymentaryServiceRequest(HttpRequestMessage request, PendingTaskVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                using (ISupplymentaryServiceRequestService ls = new SupplymentaryServiceRequestClient())
                {
                    ls.VerifySupplymentaryServiceRequest(value.ReferenceID, value.Remarks, value.TaskCode);
                }
                response = request.CreateResponse(HttpStatusCode.Created);
                return response;
            });
        }
        /// <summary>
        /// Rejects the SupplymentaryServiceRequest
        /// </summary>
        /// <param name="request"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage RejectSupplymentaryServiceRequest(HttpRequestMessage request, PendingTaskVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                using (ISupplymentaryServiceRequestService ls = new SupplymentaryServiceRequestClient())
                {
                    ls.RejectSupplymentaryServiceRequest(value.ReferenceID, value.Remarks, value.TaskCode);
                }
                response = request.CreateResponse(HttpStatusCode.Created);
                return response;
            });
        }

        #endregion

        [Route("api/IMDGForSupplementaryServiceRequests")]
        [HttpGet]
        public HttpResponseMessage GetIMDGForSupplymentaryServiceRequest(HttpRequestMessage request, string vcn)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<IMDGInformationVO> lstSuppServiceRequestVO = _supplymentaryServiceRequestService.GetIMDGForSupplymentaryServiceRequest(vcn);
                response = request.CreateResponse<List<IMDGInformationVO>>(HttpStatusCode.OK, lstSuppServiceRequestVO);
                return response;
            });
        }

        [Route("api/ETB_ETUBFromVCM")]
        [HttpGet]
        public HttpResponseMessage GetETB_ETUBFromVCMRequest(HttpRequestMessage request, string vcn)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                VesselCallMovementVO lstETB_ETUBFromVCM = _supplymentaryServiceRequestService.GetEtbEtubFromVcn(vcn);
                response = request.CreateResponse<VesselCallMovementVO>(HttpStatusCode.OK, lstETB_ETUBFromVCM);
                return response;
            });
        }

        [Authorize]
        [Route("api/SuppServiceRequests/WFCancel")]
        [HttpPost]
        public HttpResponseMessage Cancel(HttpRequestMessage request, SuppServiceRequestVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                SuppServiceRequestVO servicerequestCreated = _supplymentaryServiceRequestService.Cancel(value);
                response = request.CreateResponse<SuppServiceRequestVO>(HttpStatusCode.Created, servicerequestCreated);
                return response;
            });
        }

        [HttpGet]
        public HttpResponseMessage GetVCNDetailsForSuppServiceRequest(HttpRequestMessage request)
        {

            return GetHttpResponse(request, () =>
            {
                //HttpResponseMessage response = null;
                //List<ServiceRequestVCNDetails> VCNDtls = _servicerequestService.GetVCNDetailsForServiceRequest();
                //response = request.CreateResponse<List<ServiceRequestVCNDetails>>(HttpStatusCode.OK, VCNDtls);
                //return response;

                HttpResponseMessage response = null;

                string searchValue = HttpContext.Current.Request.Params["filter[filters][0][value]"];
                List<ServiceRequestVCNDetails> VCNDtls = _supplymentaryServiceRequestService.GetVCNDetailsForSuppServiceRequest(searchValue);
                response = request.CreateResponse<List<ServiceRequestVCNDetails>>(HttpStatusCode.OK, VCNDtls);
                return response;
            });
        }

        [Route("api/GetSupplementaryGridDetails/{frmdate}/{todate}/{vcnSearch}/{vesselName}")]
        [HttpGet]
        public HttpResponseMessage GetSupplementaryGridDetails(HttpRequestMessage request, string frmdate, string todate, string vcnSearch, string vesselName)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<SuppServiceRequestVO> lstSuppServiceRequestVO = _supplymentaryServiceRequestService.GetSupplementaryGridDetails(frmdate, todate, vcnSearch, vesselName);
                response = request.CreateResponse<List<SuppServiceRequestVO>>(HttpStatusCode.OK, lstSuppServiceRequestVO);
                return response;
            });
        }
        
    }
}