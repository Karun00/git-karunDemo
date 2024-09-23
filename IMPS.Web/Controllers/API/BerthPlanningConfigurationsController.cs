using IPMS.Domain.ValueObjects;
using IPMS.ServiceProxies.Clients;
using IPMS.ServiceProxies.Contracts;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace IPMS.Web.Api
{
    public class BerthPlanningConfigurationsController : ApiControllerBase
    {
        IBerthPlanningConfigurationsService _BerthPlanningConfigurationsService;
        public BerthPlanningConfigurationsController()
        {
            _BerthPlanningConfigurationsService = new BerthPlanningConfigurationsClient();
        }

        [Authorize]
        [Route("api/BerthPlanningConfigurations")]
        [HttpGet]
        public HttpResponseMessage BerthPlanningConfigurationsDetails(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<BerthPlanningConfigurationsVO> BerthPlanningConfigurations = _BerthPlanningConfigurationsService.BerthPlanningConfigurationsDetails();
                response = request.CreateResponse<List<BerthPlanningConfigurationsVO>>(HttpStatusCode.OK, BerthPlanningConfigurations);
                return response;
            });
        }

        [Authorize]
        [Route("api/BerthPlanningConfigurations")]
        [HttpPost]
        public HttpResponseMessage PostBerthPlanConfigData(HttpRequestMessage request, BerthPlanningConfigurationsVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                BerthPlanningConfigurationsVO berthplanconfsCreated = _BerthPlanningConfigurationsService.AddBerthPlanConfig(value);
                response = request.CreateResponse<BerthPlanningConfigurationsVO>(HttpStatusCode.Created, berthplanconfsCreated);
                return response;
            });
        }

        [Authorize]
        [Route("api/BerthPlanningConfigurations")]
        [HttpPut]
        public HttpResponseMessage ModifyBerthPlanConfigData(HttpRequestMessage request, BerthPlanningConfigurationsVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                BerthPlanningConfigurationsVO berthplanconfsModified = _BerthPlanningConfigurationsService.ModifyBerthPlanConfig(value);
                response = request.CreateResponse<BerthPlanningConfigurationsVO>(HttpStatusCode.Created, berthplanconfsModified);
                return response;
            });
        }

        //
        // GET: /BerthPlanningConfigurations/
        //public ActionResult Index()
        //{
        //    return View();
        //}
	}
}