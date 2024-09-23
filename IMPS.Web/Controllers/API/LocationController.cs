using IPMS.Domain.ValueObjects;
using IPMS.ServiceProxies.Clients;
using IPMS.ServiceProxies.Contracts;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace IPMS.Web.Api
{
    public class LocationController : ApiControllerBase
    {
          ILocationService _LocationService;
        public LocationController()
        {
            _LocationService = new LocationClient();
        }

        /// <summary>
        ///  To Get Location Details
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize]
        [Route("api/Locations")]
        [HttpGet]  
        public HttpResponseMessage GetLocationDetails(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<LocationVO> locations = _LocationService.LocationDetails();
                response = request.CreateResponse<List<LocationVO>>(HttpStatusCode.OK, locations);
                return response;
            });
        }

        /// <summary>
        /// To Add Super Category Data
        /// </summary>
        /// <param name="request"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [Authorize]
        [Route("api/Locations")]
        [HttpPost]
        public HttpResponseMessage PostLocationData(HttpRequestMessage request, LocationVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                LocationVO locationCreated = _LocationService.AddLocation(value);
                response = request.CreateResponse<LocationVO>(HttpStatusCode.Created, locationCreated);
                return response;
            });
        }

        /// <summary>
        /// To Modify Location Data
        /// </summary>
        /// <param name="request"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [Authorize]
        [Route("api/Locations")]
        [HttpPut]
        public HttpResponseMessage ModifyLocationData(HttpRequestMessage request, LocationVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                LocationVO locationCreated = _LocationService.ModifyLocation(value);
                response = request.CreateResponse<LocationVO>(HttpStatusCode.Created, locationCreated);
                return response;
            });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _LocationService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
   