using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using IPMS.ServiceProxies.Clients;
using IPMS.ServiceProxies.Contracts;

using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace IPMS.Web.Api
{
    public class BerthController : ApiControllerBase
    {
        IBerthService _berthService;


        public BerthController()
        {
            _berthService = new BerthClient();

        }
        /// <summary>
        /// Get Berths List
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize]
        [Route("api/Berths")]
        [HttpGet]
        public HttpResponseMessage GetBerths(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<BerthVO> berths = _berthService.GetBerthsDetails();
                response = request.CreateResponse<List<BerthVO>>(HttpStatusCode.OK, berths);
                return response;
            });
        }

        /// <summary>
        /// Get Quay Berths 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize]
        [Route("api/Berths/{portId}/{quayId}")]
        [HttpGet]
        public HttpResponseMessage GetBerthsInQuay(HttpRequestMessage request, string portId, string quayId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<BerthVO> berths = _berthService.GetBerthsInQuay(portId, quayId);
                response = request.CreateResponse<List<BerthVO>>(HttpStatusCode.OK, berths);
                return response;
            });
        }

        /// <summary>
        /// Add Berth
        /// </summary>
        /// <param name="request"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [Authorize]
        [Route("api/Berths")]
        [HttpPost]
        public HttpResponseMessage PostBerthData(HttpRequestMessage request, BerthVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                BerthVO berthCreated = _berthService.AddBerth(value);
                response = request.CreateResponse<BerthVO>(HttpStatusCode.Created, berthCreated);
                return response;
            });
        }

        /// <summary>
        /// Update Berth 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [Authorize]
        [Route("api/Berths")]
        [HttpPut]
        public HttpResponseMessage ModifyBerthData(HttpRequestMessage request, BerthVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                BerthVO berthModified = _berthService.ModifyBerth(value);
                response = request.CreateResponse<BerthVO>(HttpStatusCode.Created, berthModified);
                return response;
            });
        }
        /// <summary>
        /// Get Berth Types
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize]
        [Route("api/BerthTypes")]
        [HttpGet]
        public HttpResponseMessage GetBerthTypes(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<SubCategory> berth = _berthService.GetBerthType();
                response = request.CreateResponse<List<SubCategory>>(HttpStatusCode.OK, berth);
                return response;
            });
        }


        [Authorize]
        [HttpPost]
        public HttpResponseMessage PutDeleteBerth(HttpRequestMessage request, BerthVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                BerthVO berth = _berthService.DelBerthById(value);
                response = request.CreateResponse<BerthVO>(HttpStatusCode.OK, berth);
                return response;
            });
        }

        /// <summary>
        /// To Get Quays
        /// </summary>
        /// <param name="request"></param>
        /// <param name="Portid"></param>
        /// <returns></returns>
        [Authorize]
        [Route("api/BerthPQDtl")]
        [HttpGet]
        public HttpResponseMessage GetPQDtl(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<QuayVO> pqs = _berthService.GetPortQuayDetails();
                response = request.CreateResponse<List<QuayVO>>(HttpStatusCode.OK, pqs);
                return response;
            });
        }
        /// <summary>
        /// Get Cargo Types
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize]
        [Route("api/BerthCargos")]
        [HttpGet]
        public HttpResponseMessage GetCargoTypes(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
           {
               HttpResponseMessage response = null;
               List<SubCategory> cargo = _berthService.GetCargoType();
               response = request.CreateResponse<List<SubCategory>>(HttpStatusCode.OK, cargo);
               return response;
           });
        }

        /// <summary>
        /// Get Vessel Types
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize]
        [Route("api/BerthVesselTypes")]
        [HttpGet]
        public HttpResponseMessage GetVesselTypes(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<SubCategoryCodeNameVO> vessel = _berthService.GetVesselType();
                response = request.CreateResponse<List<SubCategoryCodeNameVO>>(HttpStatusCode.OK, vessel);
                return response;
            });
        }

        /// <summary>
        /// Get Reason For Visit Types
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize]
        [Route("api/BerthReasonTypes")]
        [HttpGet]
        public HttpResponseMessage GetReasonTypes(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<SubCategoryCodeNameVO> reason = _berthService.GetReasonType();
                response = request.CreateResponse<List<SubCategoryCodeNameVO>>(HttpStatusCode.OK, reason);
                return response;
            });
        }
        [Authorize]
        [Route("api/ServiceRequestBerths")]
        [HttpGet]
        public HttpResponseMessage GetBerthswithBollards(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<BerthVO> berthsnbollards = _berthService.GetBerthsWithBollards();
                response = request.CreateResponse<List<BerthVO>>(HttpStatusCode.OK, berthsnbollards);
                return response;
            });
        }

        [Authorize]
        [Route("api/BerthsWithPort")]
        [HttpGet]
        public HttpResponseMessage GetBerthsWithPortCode(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<BerthVO> berths = _berthService.GetBerthsWithPortCode();
                response = request.CreateResponse<List<BerthVO>>(HttpStatusCode.OK, berths);
                return response;
            });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _berthService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
