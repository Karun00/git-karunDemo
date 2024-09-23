using System.Collections.Generic;
using System.Web.Http;
using IPMS.ServiceProxies.Clients;
using IPMS.ServiceProxies.Contracts;
using IPMS.Domain.ValueObjects;
using IPMS.Web.Api;
using System.Net.Http;
using System.Net;

namespace IPMS.Web.Api
{
    public class SuppMiscServiceController : ApiControllerBase
    {
        ISuppMiscServiceRecordingService _SuppMiscService;
        public SuppMiscServiceController()
        {
            _SuppMiscService = new SuppMiscServiceRecordingClient();
        }

        /// <summary>
        ///  To Get Miscellaneous Details
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize]
        [Route("api/SuppMiscService")]
        [HttpGet]
        public HttpResponseMessage GetSuppMiscServiceDetails(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<SuppDryDockVO> suppMisc = _SuppMiscService.SuppMiscServiceDetails();
                response = request.CreateResponse<List<SuppDryDockVO>>(HttpStatusCode.OK, suppMisc);
                return response;
            });
        }

        /// <summary>
        /// To Get Deployment Reference Data
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize]
        [Route("api/SuppMiscReferenceData")]
        [HttpGet]
        public HttpResponseMessage GetReferenceData(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                SuppMiscServiceVO SuppMiscReference = _SuppMiscService.GetSuppMiscReferenceVO();
                response = request.CreateResponse(HttpStatusCode.OK, SuppMiscReference);

                return response;
            });
        }

        /// <summary>
        ///  To Get Miscellaneous Details
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize]
        [Route("api/SuppMiscServiceRecording/{SuppDryDockID}")]
        [HttpGet]
        public HttpResponseMessage GetSuppMiscServiceRecordingDetails(HttpRequestMessage request, int SuppDryDockID)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<SuppMiscServiceVO> suppMisc = _SuppMiscService.SuppMiscServiceRecordingDetails(SuppDryDockID);
                response = request.CreateResponse<List<SuppMiscServiceVO>>(HttpStatusCode.OK, suppMisc);
                return response;
            });
        }

        /// <summary>
        /// Modifies / update the Supp Misc Service Recording details
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [Authorize]
        [Route("api/SuppMiscServiceRecording")]
        [HttpPut]

        public HttpResponseMessage ModifySuppMiscServiceRecord(HttpRequestMessage request, SuppMiscServiceVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                SuppMiscServiceVO SuppMiscCreated = _SuppMiscService.ModifySuppMiscServiceRecord(value);
                response = request.CreateResponse<SuppMiscServiceVO>(HttpStatusCode.Created, SuppMiscCreated);
                return response;
            });
        }

    }
}

