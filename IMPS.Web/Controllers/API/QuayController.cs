using IPMS.Domain.ValueObjects;
using IPMS.ServiceProxies.Clients;
using IPMS.ServiceProxies.Contracts;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace IPMS.Web.Api
{
    public class QuayController : ApiControllerBase
    {
        IQuayService _Quayservice;
        public QuayController()
        {
            _Quayservice = new QuayClient();
        }
        /// <summary>
        /// Get Quays List
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize]
        [Route("api/Quays")]
        [HttpGet]        
        public HttpResponseMessage GetQuayDetails(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<QuayVO> quays = _Quayservice.QuayDetails();
                response = request.CreateResponse<List<QuayVO>>(HttpStatusCode.OK, quays);
                return response;
            });
        }

        /// <summary>
        /// Add Quay 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [Authorize]
        [Route("api/Quays")]        
        [HttpPost]
        public HttpResponseMessage PostQuayData(HttpRequestMessage request, QuayVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                QuayVO quayCreated = _Quayservice.AddQuay(value);
                response = request.CreateResponse<QuayVO>(HttpStatusCode.Created, quayCreated);
                return response;
            });
        }

        /// <summary>
        /// Update Quay 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [Authorize]
        [Route("api/Quays")]       
        [HttpPut]
        public HttpResponseMessage ModifyQuay(HttpRequestMessage request, QuayVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                QuayVO quayCreated = _Quayservice.ModifyQuay(value);
                response = request.CreateResponse<QuayVO>(HttpStatusCode.Created, quayCreated);
                return response;
            });
        }

        /// <summary>
        /// To Get Quay Id
        /// </summary>
        /// <param name="request"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        public HttpResponseMessage GetquayDtl(HttpRequestMessage request, long id)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                QuayVO quay = _Quayservice.GetQuayId(id);
                response = request.CreateResponse<QuayVO>(HttpStatusCode.OK, quay);
                return response;
            });
        }
      

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _Quayservice.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}