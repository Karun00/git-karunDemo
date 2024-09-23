using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using IPMS.ServiceProxies.Clients;
using IPMS.ServiceProxies.Contracts;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace IPMS.Web.Api
{
    public class SAPPostingController : ApiControllerBase
    {

        ISAPPostingService _SAPPostingService;
        public SAPPostingController()
        {
            _SAPPostingService = new SAPPostingClient();
        }

        [Authorize]
        [Route("api/SAPPosting")]
        [HttpGet]
        public HttpResponseMessage GetSAPPostingVCN(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                string searchValue = HttpContext.Current.Request.Params["filter[filters][0][value]"];
                List<SAPPostingVO> vcns = _SAPPostingService.GetSAPPostingVCN(searchValue);
                response = request.CreateResponse<List<SAPPostingVO>>(HttpStatusCode.OK, vcns);
                return response;
            });
        }

        /// <summary>
        ///  To Get SAP Posting Item Details By VCN
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize]
        [Route("api/SAPPostingItemGrid/{VCN}")]
        [HttpGet]
        public HttpResponseMessage GetSAPPostingDetailsByVCN(HttpRequestMessage request, string VCN)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<SAPPostingVO> vcninfo = _SAPPostingService.GetSAPPostingDetailsByVCN(VCN);
                response = request.CreateResponse<List<SAPPostingVO>>(HttpStatusCode.OK, vcninfo);
                return response;
            });
        }

        /// <summary>
        /// To SAP Posting Reference data While initialization
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize]
        [Route("api/SAPPostingReferenceData")]
        [HttpGet]
        public HttpResponseMessage GetSAPPostingReferenceDetails(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                SAPPostingVO sapPostingDetails = _SAPPostingService.GetSAPPostingReferenceVO();
                response = request.CreateResponse(HttpStatusCode.OK, sapPostingDetails);

                return response;
            });
        }

        /// <summary>
        ///  To Get SAP Posting Item Add Details By VCN
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize]
        [Route("api/SAPPostingAddDetails/{VCN}/{MsgType}/{ReceavedARRNO}/{MarineAccNo}/{MarinePostingId}/{PostingStatus}/{RevenueAgentAccNo}")]
        [HttpGet]
        public string GetSAPPostingDetailsAddDetails(HttpRequestMessage request, string VCN, string MsgType, string ReceavedARRNO, string MarineAccNo, string PostingStatus, int MarinePostingId, string RevenueAgentAccNo)
        {

            string vcninfo = _SAPPostingService.GetSAPPostingDetailsAddDetails(VCN, MsgType, ReceavedARRNO, MarineAccNo, MarinePostingId, PostingStatus, "N", RevenueAgentAccNo);
                return vcninfo;
        
        }

        [Authorize]
        [Route("api/MarineSAPPostingUpdatedtls/{VCN}/{MsgType}/{ReceavedARRNO}/{MarineAccNo}/{MarinePostingId}/{PostingStatus}/{RevenueAgentAccNo}")]
        [HttpGet]
        public string MarineSapPostingUpdatedtls(HttpRequestMessage request, string VCN, string MsgType, string ReceavedARRNO, string MarineAccNo, string PostingStatus, int MarinePostingId, string RevenueAgentAccNo)
        {
            string vcninfo = _SAPPostingService.GetSAPPostingDetailsAddDetails(VCN, MsgType, ReceavedARRNO, MarineAccNo, MarinePostingId, PostingStatus, "Y", RevenueAgentAccNo);
            return vcninfo;

        }

        /// <summary>
        /// To Add SAP Posting Data
        /// </summary>
        /// <param name="request"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [Authorize]
        [Route("api/SAPPosting")]
        [HttpPost]
        public HttpResponseMessage PostSAPPostingData(HttpRequestMessage request, SAPPostingVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                SAPPostingVO sapCreated = _SAPPostingService.AddSAPPosting(value);
                response = request.CreateResponse<SAPPostingVO>(HttpStatusCode.Created, sapCreated);
                return response;
            });
        }


        /// <summary>
        ///  To Get SAP Posting Account Details By VCN
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize]
        [Route("api/SAPPostingAccountInfo/{VCN}")]
        [HttpGet]
        public HttpResponseMessage GetSAPPostingAccountDetails(HttpRequestMessage request, string VCN)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<SAPPostingVO> accountinfo = _SAPPostingService.GetSAPPostingAccountDetails(VCN);
                response = request.CreateResponse<List<SAPPostingVO>>(HttpStatusCode.OK, accountinfo);
                return response;
            });
        }

        /// <summary>
        ///  To Get SAP Posting Details By ID
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize]
        [Route("api/SAPPostingDetails/{SAPPostingID}")]
        [HttpGet]
        public HttpResponseMessage GetSAPPostingDetails(HttpRequestMessage request, int SAPPostingID)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                SAPPostingVO sappostings = _SAPPostingService.GetSAPPostingDetails(SAPPostingID);
                response = request.CreateResponse<SAPPostingVO>(HttpStatusCode.OK, sappostings);
                return response;
            });
        }


        /// <summary>
        /// To Add SAP Posting Invoice Data
        /// </summary>
        /// <param name="request"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [Authorize]
        [Route("api/SAPPostingInvoice")]
        [HttpPost]
        public HttpResponseMessage PostSAPPostingInvoiceData(HttpRequestMessage request, SAPPostingVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                SAPPostingVO sapCreated = _SAPPostingService.AddSAPPostingInvoice(value);
                response = request.CreateResponse<SAPPostingVO>(HttpStatusCode.Created, sapCreated);
                return response;
            });
        }

                   
            
        /// <summary>
        ///  To Get SAP Posting Details By ID
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize]
        [Route("api/SAPInvoiceResponse/{MarineOrderNo}")]
        [HttpGet]
        public HttpResponseMessage GetSAPInvoiceResponseDetails(HttpRequestMessage request, string MarineOrderNo)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                SAPInvoiceItem sappostings = _SAPPostingService.GetSAPInvoiceResponseDetails(MarineOrderNo);
                response = request.CreateResponse<SAPInvoiceItem>(HttpStatusCode.OK, sappostings);
                return response;
            });
        }

  
    }
}