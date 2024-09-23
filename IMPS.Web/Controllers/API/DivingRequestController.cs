using IPMS.Domain.ValueObjects;
using IPMS.ServiceProxies.Clients;
using IPMS.ServiceProxies.Contracts;
using IPMS.Web.Api;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace IPMS.Web.API
{
    public class DivingRequestController : ApiControllerBase
    {
        IDivingRequestService _divingrequestservice;

        public DivingRequestController()
        {
            _divingrequestservice = new DivingRequestClient();
        }

        #region GetAllDivingRequests
        /// <summary>
        /// Gets all Diving Request list .
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("api/DivingRequestDetails")]
        [Authorize]
        [HttpGet]
        public HttpResponseMessage GetAllDivingRequests(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<DivingRequestVO> divingrequest = _divingrequestservice.GetAllDivingRequests();
                response = request.CreateResponse<List<DivingRequestVO>>(HttpStatusCode.OK, divingrequest);
                return response;
            });
        }
        #endregion

        #region GetAllDivingRequestOccupation
        /// <summary>
        /// Gets all diving request occupation list
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("api/GetAllDivingRequestOccupation")]
        [Authorize]
        [HttpGet]
        public HttpResponseMessage GetAllDivingRequestOccupation(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<DivingRequestVO> divingrequest = _divingrequestservice.GetAllDivingRequestOccupation();
                response = request.CreateResponse<List<DivingRequestVO>>(HttpStatusCode.OK, divingrequest);
                return response;
            });
        }
        #endregion

        #region GetDivingRequestOccupationById
        /// <summary>
        /// GetDivingRequestOccupationById
        /// </summary>
        /// <param name="request"></param>
        /// <param name="DivingRequestID"></param>
        /// <returns></returns>
        [Authorize]
        [Route("api/GetDivingRequestOccupationById")]
        [HttpGet]
        public HttpResponseMessage GetDivingRequestOccupationById(HttpRequestMessage request, int id)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                DivingRequestVO divingrequest = _divingrequestservice.GetDivingRequestOccupationById(id);
                response = request.CreateResponse<DivingRequestVO>(HttpStatusCode.OK, divingrequest);
                return response;
            });
        }
        #endregion

        #region GetDivingRequestByIDView
        /// <summary>
        /// Gets the Diving Request id by Diving RequestID
        /// </summary>
        /// <param name="request"></param>
        /// <param name="requestid"></param>
        /// <returns></returns>
        [Authorize]
        [Route("api/GetDivingRequestByIDView")]
        [HttpGet]
        public HttpResponseMessage GetDivingRequestByIDView(HttpRequestMessage request, int requestid)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<DivingRequestVO> divingrequest = _divingrequestservice.GetDivingRequestByIdView(requestid);
                response = request.CreateResponse<List<DivingRequestVO>>(HttpStatusCode.OK, divingrequest);
                return response;
            });
        }
        #endregion

        #region AddDivingRequests
        /// <summary>
        /// Adds / Inserts the new  Diving Request
        /// </summary>
        /// <param name="request"></param>
        /// <param name="divingrequest"></param>
        /// <returns></returns>
        [Route("api/DivingRequest")]
        [Authorize]
        [HttpPost]
        public HttpResponseMessage AddDivingRequests(HttpRequestMessage request, DivingRequestVO divingrequest)
        {

            return GetHttpResponse(request, () =>
               {
                   HttpResponseMessage response = null;
                   divingrequest.QuayLocation = divingrequest.FromQuayCode;
                   divingrequest.ToBerthCode = divingrequest.FromBerthCode;
                   divingrequest.ToQuayCode = divingrequest.FromQuayCode;

                   DivingRequestVO DivingRequestCreated = _divingrequestservice.AddDivingRequest(divingrequest);
                   response = request.CreateResponse<DivingRequestVO>(HttpStatusCode.Created, DivingRequestCreated);
                   return response;
               });
        }
        #endregion

        #region GetOtherLocations
        /// <summary>
        /// Get Other Locations data.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("api/GetOtherLocations")]
        [Authorize]
        [HttpGet]
        public HttpResponseMessage GetOtherLocations(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<LocationVO> locations = _divingrequestservice.GetOtherLocations();
                response = request.CreateResponse<List<LocationVO>>(HttpStatusCode.OK, locations);
                return response;
            });
        }
        #endregion

        #region GetPortQuays
        /// <summary>
        /// Get Quays data.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("api/GetQuaysForDivingRequest")]
        [Authorize]
        [HttpGet]
        public HttpResponseMessage GetPortQuays(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<QuayVO> quays = _divingrequestservice.GetPortQuays();
                response = request.CreateResponse<List<QuayVO>>(HttpStatusCode.OK, quays);
                return response;
            });
        }
        #endregion

        #region GetQuayBerths
        /// <summary>
        /// Get Berths data
        /// </summary>
        /// <param name="request"></param>
        /// <param name="QuayCode"></param>
        /// <returns></returns>
        [Route("api/GetBerthsForDivingRequest/{QuayCode}")]
        [Authorize]
        [HttpGet]
        public HttpResponseMessage GetQuayBerths(HttpRequestMessage request, string QuayCode)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<BerthVO> berths = _divingrequestservice.GetQuayBerths(QuayCode);
                response = request.CreateResponse<List<BerthVO>>(HttpStatusCode.OK, berths);
                return response;
            });
        }
        #endregion

        #region GetBerthBollards
        /// <summary>
        /// Get Bollards data.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="QuayCode"></param>
        /// <param name="BerthCode"></param>
        /// <returns></returns>
        [Route("api/GetBollardsForDivingRequest/{QuayCode,BerthCode}")]
        [Authorize]
        [HttpGet]
        public HttpResponseMessage GetBerthBollards(HttpRequestMessage request, string QuayCode, string BerthCode)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<BollardVO> bollards = _divingrequestservice.GetBerthBollards(QuayCode, BerthCode);
                response = request.CreateResponse<List<BollardVO>>(HttpStatusCode.OK, bollards);
                return response;
            });
        }
        #endregion

        #region GetDivingRequestReasons
        /// <summary>
        /// Author  : Sandeep Appana
        /// Date    : 11th Mar 2015
        /// Purpose : To get Diving Request Reasons details in subcategory
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("api/GetDivingRequestReasons")]
        [Authorize]
        [HttpGet]
        public HttpResponseMessage GetDivingRequestReasons(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<SubCategoryCodeNameVO> subCategoryCodeNameVOs = _divingrequestservice.GetDivingRequestReasons();
                response = request.CreateResponse<List<SubCategoryCodeNameVO>>(HttpStatusCode.OK, subCategoryCodeNameVOs);
                return response;
            });
        }
        #endregion

        #region GetAllDivingTaskExecution
        /// <summary>
        /// Gets all diving rtask execution list
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize]
        [Route("api/DivingTaskExecutions")]
        [HttpGet]
        public HttpResponseMessage GetAllDivingTaskExecution(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
                {
                    HttpResponseMessage response = null;
                    List<DivingRequestVO> lstDivingRequestVO = _divingrequestservice.GetAllDivingTaskExecutions();
                    response = request.CreateResponse<List<DivingRequestVO>>(HttpStatusCode.OK, lstDivingRequestVO);
                    return response;
                });
        }
        #endregion

        #region ModifyPreDivingCheckList
        /// <summary>
        /// Modifies or updates the diving request check list
        /// </summary>
        /// <param name="request"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [Authorize]
        [Route("api/PreDivingCheckList")]
        [HttpPost]
        public HttpResponseMessage ModifyPreDivingCheckList(HttpRequestMessage request, DivingRequestVO value)
        {
            return GetHttpResponse(request, () =>
                {
                    HttpResponseMessage response = null;
                    value.ModifiedDate = System.DateTime.Now;

                    DivingRequestVO divingreqtvo = _divingrequestservice.ModifyDivingCheckList(value);
                    response = request.CreateResponse<DivingRequestVO>(HttpStatusCode.Created, divingreqtvo);
                    return response;

                });
        }
        #endregion

        #region ModifyDivingTaskExecution
        /// <summary>
        /// Modifies the Diving request Check list 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [Authorize]
        [Route("api/DivingTaskExecutions")]
        [HttpPost]
        public HttpResponseMessage ModifyDivingTaskExecution(HttpRequestMessage request, DivingRequestVO value)
        {
            return GetHttpResponse(request, () =>
                {
                    HttpResponseMessage response = null;
                    value.ModifiedDate = System.DateTime.Now;

                    DivingRequestVO divingreqtvo = _divingrequestservice.ModifyDivingTaskExecution(value);
                    response = request.CreateResponse<DivingRequestVO>(HttpStatusCode.Created, divingreqtvo);
                    return response;

                });
        }
        #endregion

        #region GetAllLocations
        /// <summary>
        /// Gets Locations
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("api/AllLocations")]
        [Authorize]
        [HttpGet]
        public HttpResponseMessage GetAllLocations(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<LocationVO> lstlocationsvo = _divingrequestservice.GetAllLocations();
                response = request.CreateResponse<List<LocationVO>>(HttpStatusCode.OK, lstlocationsvo);
                return response;
            });
        }
        #endregion

        #region ModifyDivingRequestOccupation
        /// <summary>
        /// Modifies / Updates the Diving Request Occupation Details
        /// </summary>
        /// <param name="request"></param>
        /// <param name="divingrequestvo"></param>
        /// <returns></returns>
        [Authorize]
        [Route("api/ModifyDivingRequestOccupation")]
        [HttpPost]
        public HttpResponseMessage ModifyDivingRequestOccupation(HttpRequestMessage request, DivingRequestVO divingrequestvo)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                divingrequestvo.ModifiedDate = System.DateTime.Now;

                DivingRequestVO divingreqtvo = _divingrequestservice.ModifyDivingRequestOccupation(divingrequestvo);
                response = request.CreateResponse<DivingRequestVO>(HttpStatusCode.Created, divingreqtvo);
                return response;

            });
        }
        #endregion

        #region GetDivingrequestsForScroll
        [Authorize]
        [Route("api/DivingRequestsForScroll")]
        [HttpGet]
        public HttpResponseMessage GetDivingrequestsForScroll(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<DivingRequestVO> lstDivingRequestVO = _divingrequestservice.GetDivingrequestsForScroll();
                response = request.CreateResponse<List<DivingRequestVO>>(HttpStatusCode.OK, lstDivingRequestVO);
                return response;
            });
        }
        #endregion

        #region GetLoggedInUserName
        [Authorize]
        [Route("api/GetLoggedInUserName")]
        [HttpGet]
        public HttpResponseMessage GetLoggedInUserName(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                string userName = _divingrequestservice.GetLoggedInUserName();
                response = request.CreateResponse<string>(HttpStatusCode.OK, userName);
                return response;
            });
        }
        #endregion

        #region Workflow Integrated Methods

        #region ApproveDivingRequestOccupation
        /// <summary>
        /// Approves the diving request occupation
        /// </summary>
        /// <param name="request"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public HttpResponseMessage ApproveDivingRequestOccupation(HttpRequestMessage request, PendingTaskVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                using (IDivingRequestService ls = new DivingRequestClient())
                {
                    ls.ApproveDivingRequestOccupation(value.ReferenceID, value.Remarks, value.TaskCode);
                }
                response = request.CreateResponse(HttpStatusCode.Created);
                return response;
            });
        }
        #endregion

        #region VerifyDivingRequestOccupation
        /// <summary>
        /// Verifies the Diving Request Occupation
        /// </summary>
        /// <param name="request"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public HttpResponseMessage VerifyDivingRequestOccupation(HttpRequestMessage request, PendingTaskVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                using (IDivingRequestService ls = new DivingRequestClient())
                {
                    ls.VerifyDivingRequestOccupation(value.ReferenceID, value.Remarks, value.TaskCode);
                }
                response = request.CreateResponse(HttpStatusCode.Created);
                return response;
            });
        }
        #endregion

        #region RejectDivingRequestOccupation
        /// <summary>
        /// Rejects the Diving Request Occupation
        /// </summary>
        /// <param name="request"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public HttpResponseMessage RejectDivingRequestOccupation(HttpRequestMessage request, PendingTaskVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                using (IDivingRequestService ls = new DivingRequestClient())
                {
                    ls.RejectDivingRequestOccupation(value.ReferenceID, value.Remarks, value.TaskCode);
                }
                response = request.CreateResponse(HttpStatusCode.Created);
                return response;
            });
        }
        #endregion

        #endregion

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _divingrequestservice.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

