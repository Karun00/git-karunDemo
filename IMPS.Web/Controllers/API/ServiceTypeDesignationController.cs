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
    public class ServiceTypeDesignationController : ApiControllerBase
    {
        IServiceTypeDesignationService _ServiceTypeDesignationService;
        public ServiceTypeDesignationController()
        {
            _ServiceTypeDesignationService = new ServiceTypeDesignationClient();
        }

        /// <summary>
        ///  To Get ServiceTypeDesignation Details
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize]
        [Route("api/ServiceTypeDesignationsList")]
        [HttpGet]
        public HttpResponseMessage GetServiceTypeDesignationDetails(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<ServiceTypeVO> ServiceTypeDesignations = _ServiceTypeDesignationService.ServiceTypeDesignationDetails();
                response = request.CreateResponse<List<ServiceTypeVO>>(HttpStatusCode.OK, ServiceTypeDesignations);
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
        [Route("api/ServiceTypeDesignationDetails")]
        [HttpPost]
        public HttpResponseMessage PostServiceTypeDesignationData(HttpRequestMessage request, ServiceTypeVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                ServiceTypeVO ServiceTypeDesignationCreated = _ServiceTypeDesignationService.AddServiceTypeDesignation(value);
                response = request.CreateResponse<ServiceTypeVO>(HttpStatusCode.Created, ServiceTypeDesignationCreated);
                return response;
            });
        }

        /// <summary>
        /// To Modify ServiceTypeDesignation Data
        /// </summary>
        /// <param name="request"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [Authorize]
        [Route("api/ServiceTypeDesignationDetails")]
        [HttpPut]
        public HttpResponseMessage ModifyServiceTypeDesignationData(HttpRequestMessage request, ServiceTypeVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                ServiceTypeVO ServiceTypeDesignationCreated = _ServiceTypeDesignationService.ModifyServiceTypeDesignation(value);
                response = request.CreateResponse<ServiceTypeVO>(HttpStatusCode.Created, ServiceTypeDesignationCreated);
                return response;
            });
        }

        /// <summary>
        /// This method is used for fetches the Designation Details
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("api/GetDesignations")]
        [HttpGet]
        public HttpResponseMessage GetDesignations(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<SubCategory> designations = _ServiceTypeDesignationService.GetDesignations();
                response = request.CreateResponse<List<SubCategory>>(HttpStatusCode.OK, designations);
                return response;
            });
        }

        /// <summary>
        /// This method is used for fetches the Service Types
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("api/GetServiceTypeDetails")]
        [HttpGet]
        public HttpResponseMessage GetServiceTypes(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<ServiceType> serviceTypes = _ServiceTypeDesignationService.GetServiceTypes();
                response = request.CreateResponse<List<ServiceType>>(HttpStatusCode.OK, serviceTypes);
                return response;
            });
        }

        /// <summary>
        /// Get Crafts data.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("api/GetCraftTypeDetails")]
        [HttpGet]
        public HttpResponseMessage GetCraftDetails(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<SubCategory> CraftTypes = _ServiceTypeDesignationService.GetCraftTypes();
                response = request.CreateResponse<List<SubCategory>>(HttpStatusCode.OK, CraftTypes);
                return response;
            });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _ServiceTypeDesignationService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
