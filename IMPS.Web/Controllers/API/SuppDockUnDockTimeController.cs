using IPMS.ServiceProxies.Clients;
using IPMS.ServiceProxies.Contracts;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using IPMS.Domain.ValueObjects;
using IPMS.Web.Api;

namespace IPMS.Web.Controllers.API
{
    public class SuppDockUnDockTimeController : ApiControllerBase
    {
        ISuppDockUnDockTimeService _SuppDockUnDockTimeservice;
        //ISupplymentaryServiceRequestService _supplymentaryServiceRequestService;

        public SuppDockUnDockTimeController()
        {
            _SuppDockUnDockTimeservice = new SuppDockUnDockTimeClient();
            //_supplymentaryServiceRequestService = new SupplymentaryServiceRequestClient();
        }

        /// <summary>
        /// Gets Supp Dock UnDock Time list
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [Route("api/SuppDockUnDockTimes")]
        [HttpGet]
        public HttpResponseMessage AllSuppDockUnDockTimeDetails(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<SuppDryDockVO> AllSuppDockUnDockTimes = _SuppDockUnDockTimeservice.AllSuppDockUnDockTimeDetails();
                response = request.CreateResponse<List<SuppDryDockVO>>(HttpStatusCode.OK, AllSuppDockUnDockTimes);

                //List<SuppServiceRequestVO> AllSuppDockUnDockTimes = _supplymentaryServiceRequestService.AllSuppDockUnDockTimeDetails();
                //response = request.CreateResponse<List<SuppServiceRequestVO>>(HttpStatusCode.OK, AllSuppDockUnDockTimes);

                return response;
            });
        }

        /// <summary>
        /// Modifies / update the Supp Dock UnDock Time details
        /// </summary>
        /// <param name="SuppHotWorkInspectiondata"></param>
        /// <returns></returns>
        [Authorize]
        [Route("api/SuppDockUnDockTimes")]
        [HttpPut]

        public HttpResponseMessage ModifySuppDockUnDockTime(HttpRequestMessage request, SuppDryDockVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                SuppDryDockVO SuppDockUnDockTimeCreated = _SuppDockUnDockTimeservice.ModifySuppDockUnDockTime(value);
                response = request.CreateResponse<SuppDryDockVO>(HttpStatusCode.Created, SuppDockUnDockTimeCreated);
                return response;
            });
        }
    }
}
