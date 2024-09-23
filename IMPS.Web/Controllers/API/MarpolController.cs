using IPMS.Domain.ValueObjects;
using IPMS.ServiceProxies.Clients;
using IPMS.ServiceProxies.Contracts;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace IPMS.Web.Api
{
    public class MarpolController : ApiControllerBase
    {
        IMarpolService _marpolService;

        public MarpolController()
        {
            _marpolService = new MarpolClient();
        }


        #region GetMarpolDetails
        /// <summary>
        /// To Get Marpol Details 
        /// </summary>
        /// <returns></returns>        
        [Authorize]
        [Route("api/MarpolDetails")]
        [HttpGet]
        public HttpResponseMessage GetMarpolDetails(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<MarpolVO> marpolDetails = _marpolService.GetMarpolDetails();
                response = request.CreateResponse<List<MarpolVO>>(HttpStatusCode.OK, marpolDetails);
                return response;
            });
        }
        #endregion

        [Route("api/GetMarpolReferenceData")]
        [HttpGet]
        public HttpResponseMessage GetMarpolReferenceData(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                MarpolVO referenceData = _marpolService.GetMarpolReferenceData();
                response = request.CreateResponse<MarpolVO>(HttpStatusCode.OK, referenceData);
                return response;
            });
        }

        /// <summary>
        /// To Add  Marpol Details 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [Authorize]
        [Route("api/MarpolDetails")]
        [HttpPost]
        public HttpResponseMessage PostMarpolDetails(HttpRequestMessage request, MarpolVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                MarpolVO marpolDetails = _marpolService.SaveMarpolDetails(value);
                response = request.CreateResponse<MarpolVO>(HttpStatusCode.Created, marpolDetails);
                return response;
            });
        }

        /// <summary>
        /// To Modify  Marpol Details 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [Authorize]
        [Route("api/MarpolDetails")]
        [HttpPut]
        public HttpResponseMessage ModifyMarpolDetails(HttpRequestMessage request, MarpolVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                MarpolVO marpolDetails = _marpolService.ModifyMarpolDetails(value);
                response = request.CreateResponse<MarpolVO>(HttpStatusCode.Created, marpolDetails);
                return response;
            });
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _marpolService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}