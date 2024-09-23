using IPMS.Domain.ValueObjects;
using IPMS.ServiceProxies.Clients;
using IPMS.ServiceProxies.Contracts;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace IPMS.Web.Api
{
    public class BollardsController : ApiControllerBase
    {

        IBollardService _bollardservice;

        public BollardsController()
        {
            _bollardservice = new BollardClient();
        }
        /// <summary>
        /// Get Bollards List
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize]
        [Route("api/Bollards")]
        [HttpGet]      
        public HttpResponseMessage GetAllBollards(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<BollardVO> bollards = _bollardservice.GetBollardDetails();
                response = request.CreateResponse<List<BollardVO>>(HttpStatusCode.OK, bollards);
                return response;
            });
        }

        /// <summary>
        /// To get Bollards based on port, quay and berth
        /// </summary>
        /// <param name="request"></param>
        /// <param name="portId"></param>
        /// <param name="quayId"></param>
        /// <param name="berthId"></param>
        /// <returns></returns>
        [Authorize]
        [Route("api/BollardInBerths/{portId}/{quayId}/{berthId}")]
        [HttpGet]
        public HttpResponseMessage GetBollardsInBerths(HttpRequestMessage request, string portId, string quayId, string berthId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<BollardVO> bollards = _bollardservice.GetBollardsInBerths(portId, quayId, berthId);
                response = request.CreateResponse<List<BollardVO>>(HttpStatusCode.OK, bollards);
                return response;
            });
        }

        /// <summary>
        /// To Get Quays based on Port
        /// </summary>
        /// <param name="request"></param>
        /// <param name="portId"></param>
        /// <returns></returns>
        [Authorize]
        [Route("api/BollardLoginPortQuays")]
        [HttpGet]
        public HttpResponseMessage GetLoginPortQuays(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                string portId = "";
                List<QuayVO> quays = _bollardservice.GetPortQuays(portId);
                response = request.CreateResponse<List<QuayVO>>(HttpStatusCode.OK, quays);
                return response;
            });
        }

        /// <summary>
        /// To Get Quays based on Port
        /// </summary>
        /// <param name="request"></param>
        /// <param name="portId"></param>
        /// <returns></returns>
        [Authorize]
        [Route("api/BollardPortQuays/{portId}")]
        [HttpGet] 
        public HttpResponseMessage GetPortQuays(HttpRequestMessage request, string portId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                List<QuayVO> quays = _bollardservice.GetPortQuays(portId);
                response = request.CreateResponse<List<QuayVO>>(HttpStatusCode.OK, quays);
                return response;
            });
        }

        /// <summary>
        /// To Get Berths based on Port and Quay
        /// </summary>
        /// <param name="request"></param>
        /// <param name="portId"></param>
        /// <param name="quayId"></param>
        /// <returns></returns>
        [Authorize]
        [Route("api/BollardQuayBerths/{portId}/{quayId}")]
        [HttpGet]         
        public HttpResponseMessage GetQuayBerths(HttpRequestMessage request, string portId, string quayId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                List<BerthVO> berths = _bollardservice.GetQuayBerths(portId, quayId);
                response = request.CreateResponse<List<BerthVO>>(HttpStatusCode.OK, berths);
                return response;
            });
        }


        /// <summary>
        /// Add Bollards
        /// </summary>
        /// <param name="request"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [Authorize]
        [Route("api/Bollards")]
        [HttpPost]
        public HttpResponseMessage PostBollardData(HttpRequestMessage request, BollardVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                BollardVO bollardCreated = _bollardservice.AddBollard(value);
                response = request.CreateResponse<BollardVO>(HttpStatusCode.Created, bollardCreated);
                return response;
            });
        }
        /// <summary>
        /// Update Bollard
        /// </summary>
        /// <param name="request"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [Authorize]
        [Route("api/Bollards")]
        [HttpPut]
        public HttpResponseMessage ModifyBollardData(HttpRequestMessage request, BollardVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                BollardVO bollardCreated = _bollardservice.ModifyBollard(value);
                response = request.CreateResponse<BollardVO>(HttpStatusCode.Created, bollardCreated);
                return response;
            });
        }

        [Authorize]
        public HttpResponseMessage GetBollardDtl(HttpRequestMessage request, long id)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                BollardVO bollard = _bollardservice.GetBollardID(id);
                response = request.CreateResponse<BollardVO>(HttpStatusCode.OK, bollard);
                return response;
            });
        }
        [Authorize]
        public BollardVO PutDeleteBollard([FromUri]long id)
        {
            return _bollardservice.DelBollard(id);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _bollardservice.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}