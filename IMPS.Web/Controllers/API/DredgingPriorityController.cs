using IPMS.Domain.ValueObjects;
using IPMS.ServiceProxies.Clients;
using IPMS.ServiceProxies.Contracts;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace IPMS.Web.Api
{
    public class DredgingPriorityController : ApiControllerBase
    {
         IDredgingPriorityService _DredgingPriorityService;
         public DredgingPriorityController()
        {
            _DredgingPriorityService = new DredgingPriorityClient();
        }
        /// <summary>
        /// To  Dredging Priority Reference Data While initialization
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize]
        [Route("api/DredgingPriorityReferenceData")]
        [HttpGet]
        public HttpResponseMessage GetDredgingPriorityReferenceDetails(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                DredgingPriorityVO DredgingPriorityDetails = _DredgingPriorityService.GetDredgingPriorityReferenceVO();
                response = request.CreateResponse(HttpStatusCode.OK, DredgingPriorityDetails);

                return response;
            });
        }
        /// <summary>
        /// Get Months.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("api/GetDredgingMonthes/{FinancialYearID}")]
        [HttpGet]
        public HttpResponseMessage GetMonths(HttpRequestMessage request, int financialYearId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<FinancialYearVO> months = _DredgingPriorityService.GetMonths(financialYearId);
                response = request.CreateResponse<List<FinancialYearVO>>(HttpStatusCode.OK, months);
                return response;
            });
        }
        /// <summary>
        ///  To Get Dredging Priority Details
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize]
        [Route("api/DredgingPrioritys/{FinancialYearID}")]
        [HttpGet]
        public HttpResponseMessage GetDredgingPriorityDetails(HttpRequestMessage request, int financialYearId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<DredgingPriorityVO> dredgingprioritys = _DredgingPriorityService.DredgingPriorityDetails(financialYearId);
                response = request.CreateResponse<List<DredgingPriorityVO>>(HttpStatusCode.OK, dredgingprioritys);
                return response;
            });
        }
      
        /// <summary>
        /// To Get Dredging Priority Volumes
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize]
        [Route("api/DredgingPriorityVolumes/{FinancialYearID}")]
        [HttpGet]
        public HttpResponseMessage GetDredgingPriorityVolumes(HttpRequestMessage request, int financialYearId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<DredgingPriorityVolumeVO> DredgingPriorityVolumes = _DredgingPriorityService.GetDredgingPriorityVolumes(financialYearId);
                response = request.CreateResponse<List<DredgingPriorityVolumeVO>>(HttpStatusCode.OK, DredgingPriorityVolumes);
                return response;
            });
        }
        
        /// <summary>
        /// To Get Financial Year Data
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize]
        [Route("api/DredgingPriority/GetFinancialYear")]
        [HttpGet]
        public HttpResponseMessage GetFinancialYear(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<FinancialYearVO> FinancialYearReference = _DredgingPriorityService.GetFinancialYear();
                response = request.CreateResponse<List<FinancialYearVO>>(HttpStatusCode.OK, FinancialYearReference);

                return response;
            });
        }
        /// <summary>
        /// This method is used for fetch the Berth data
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("api/DredgingPriorityBerthData")]
        [HttpGet]
        public HttpResponseMessage GetBerthTypes(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<DredgingPriorityVO> Berth = _DredgingPriorityService.GetBerthTypes();
                response = request.CreateResponse<List<DredgingPriorityVO>>(HttpStatusCode.OK, Berth);
                return response;
            });
        }
        /// <summary>
        /// This method is used for fetch the Location data
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("api/DredgingPriorityLocationData")]
        [HttpGet]
        public HttpResponseMessage GetLocationTypes(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<DredgingPriorityVO> Location = _DredgingPriorityService.GetLocationTypes();
                response = request.CreateResponse<List<DredgingPriorityVO>>(HttpStatusCode.OK, Location);
                return response;
            });
        }

        /// <summary>
        /// To add Dredging Priority data
        /// </summary>
        /// <param name="request"></param>
        /// <param name="dredgingPriorityData"></param>
        /// <returns></returns>
        /// 
        [Route("api/DredgingPriority")]
        [HttpPost]
        public HttpResponseMessage PostDredgingPriorityData(HttpRequestMessage request, DredgingPriorityVO dredgingPriorityData)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                DredgingPriorityVO DredgingPriority = _DredgingPriorityService.AddDredgingPriorityDetails(dredgingPriorityData);
                response = request.CreateResponse<DredgingPriorityVO>(HttpStatusCode.Created, DredgingPriority);
                return response;
            });
        }

        /// <summary>
        /// To Update Dredging Priority data
        /// </summary>
        /// <param name="request"></param>
        /// <param name="dredgingPriorityData"></param>
        /// <returns></returns>
        /// 
        [Route("api/DredgingPriority")]
        [HttpPut]
        public HttpResponseMessage PutDredgingPriorityData(HttpRequestMessage request, DredgingPriorityVO dredgingPriorityData)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                DredgingPriorityVO DredgingPriority = _DredgingPriorityService.ModifyDredgingPriorityDetails(dredgingPriorityData);
                response = request.CreateResponse<DredgingPriorityVO>(HttpStatusCode.Created, DredgingPriority);
                return response;
            });
        }

     
        /// <summary>
        ///  To Get Dredging Area Details
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize]
        [Route("api/DredgingPriorityArea/{DredgingPriorityID}")]
        [HttpGet]
        public HttpResponseMessage DredgingPriorityAreaDetails(HttpRequestMessage request, int dredgingPriorityId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<DredgingOperationVO> dredgingArea = _DredgingPriorityService.DredgingPriorityAreaDetails(dredgingPriorityId);
                response = request.CreateResponse<List<DredgingOperationVO>>(HttpStatusCode.OK, dredgingArea);
                return response;
            });
        }
        /// <summary>
        ///  To Get Dredging Area Details
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize]
        [Route("api/DredgingPriorityAreas/{DredgingPriorityID}")]
        [HttpGet]
        public HttpResponseMessage DredgingPriorityAreaDetailsPending(HttpRequestMessage request, int dredgingPriorityId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<DredgingOperationVO> dredgingArea = _DredgingPriorityService.DredgingPriorityAreaDetailsPending(dredgingPriorityId);
                response = request.CreateResponse<List<DredgingOperationVO>>(HttpStatusCode.OK, dredgingArea);
                return response;
            });
        }

        #region Workflow Integrated Methods

        /// <summary>
        /// To Approve Dredging Priority
        /// </summary>
        /// <param name="request"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [Route("api/DredgingPriority/Approve")]
        [HttpPost]
        public HttpResponseMessage ApproveDredgingPriority(HttpRequestMessage request, PendingTaskVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                _DredgingPriorityService.ApproveDredgingPriority(value.ReferenceID, value.Remarks, value.TaskCode);

                response = request.CreateResponse(HttpStatusCode.Created);
                return response;
            });
        }

        /// <summary>
        /// To Reject DredgingPriority
        /// </summary>
        /// <param name="request"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [Route("api/DredgingPriority/Reject")]
        [HttpPost]
        public HttpResponseMessage RejectDredgingPriority(HttpRequestMessage request, PendingTaskVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                _DredgingPriorityService.RejectDredgingPriority(value.ReferenceID, value.Remarks, value.TaskCode);
                response = request.CreateResponse(HttpStatusCode.Created);
                return response;
            });
        }
        /// <summary>
        ///  To get  Dredging Priority based on DredgingPriorityID
        /// </summary>
        /// <param name="request"></param>
        /// <param name="dredgingpriorityid"></param>
        /// <returns></returns>
        [Authorize]
        [Route("api/DredgingPriority/{dredgingpriorityid}")]
        [HttpGet]
        public HttpResponseMessage GetDredgingPriority(HttpRequestMessage request, int dredgingPriorityId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<DredgingPriorityVO> dredgingpriorityrtype = _DredgingPriorityService.GetDredgingPriorityPendingView(dredgingPriorityId);
                response = request.CreateResponse<List<DredgingPriorityVO>>(HttpStatusCode.OK, dredgingpriorityrtype);
                return response;
            });
        }
        #endregion


        /// <summary>
        /// This method is used for DocumentData
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("api/DredgingPriorityDocument/{dredgingpriorityid}")]
        [HttpGet]
        public HttpResponseMessage GetBerthTypes(HttpRequestMessage request, int dredgingPriorityId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<DredgingPriorityDocumentVO> Document = _DredgingPriorityService.GetDocument(dredgingPriorityId);
                response = request.CreateResponse<List<DredgingPriorityDocumentVO>>(HttpStatusCode.OK, Document);
                return response;
            });
        }

    }
}