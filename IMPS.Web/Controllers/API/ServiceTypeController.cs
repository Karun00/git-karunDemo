using IPMS.Domain.ValueObjects;
using IPMS.ServiceProxies.Clients;
using IPMS.ServiceProxies.Contracts;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace IPMS.Web.Api
{
    public class ServiceTypeController : ApiControllerBase
    {
        IServiceTypeService _ServiceTypeService;
        public ServiceTypeController()
        {
            _ServiceTypeService = new ServiceTypeClient();
        }

        /// <summary>
        ///  To Get ServiceType Details
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize]
        [Route("api/ServiceTypesList")]
        [HttpGet]
        public HttpResponseMessage GetServiceTypeDetails(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<ServiceTypeVO> ServiceTypes = _ServiceTypeService.ServiceTypeDetails();
                response = request.CreateResponse<List<ServiceTypeVO>>(HttpStatusCode.OK, ServiceTypes);
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
        [Route("api/ServiceTypeDetails")]
        [HttpPost]
        public HttpResponseMessage PostServiceTypeData(HttpRequestMessage request, ServiceTypeVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                ServiceTypeVO ServiceTypeCreated = _ServiceTypeService.AddServiceType(value);
                response = request.CreateResponse<ServiceTypeVO>(HttpStatusCode.Created, ServiceTypeCreated);
                return response;
            });
        }

        /// <summary>
        /// To Modify ServiceType Data
        /// </summary>
        /// <param name="request"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [Authorize]
        [Route("api/ServiceTypeDetails")]
        [HttpPut]
        public HttpResponseMessage ModifyServiceTypeData(HttpRequestMessage request, ServiceTypeVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                ServiceTypeVO ServiceTypeCreated = _ServiceTypeService.ModifyServiceType(value);
                response = request.CreateResponse<ServiceTypeVO>(HttpStatusCode.Created, ServiceTypeCreated);
                return response;
            });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _ServiceTypeService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
