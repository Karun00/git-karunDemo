using Core.Repository;
using IPMS.Data.Context;
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
    public class ResourceAllocationConfigRuleController : ApiControllerBase
    {
        IResourceAllocationConfigRuleService _resourceallocationconfigruleservice;

        public ResourceAllocationConfigRuleController()
        {
            _resourceallocationconfigruleservice = new ResourceAllocationConfigRuleClient();
        }

        /// <summary>
        /// To Get Agent Registration details in view screens for Pending Task View Screens of the workflow
        /// </summary>
        /// <param name="request"></param>
        /// <param name="vcn"></param>
        /// <returns></returns>
        [Route("api/ResourceallocationConfigruleList")]
        public HttpResponseMessage GetResourceallocationConfigruleList(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                List<ResourceAllocationConfigRuleVO> resourceallocationconfiglist = _resourceallocationconfigruleservice.GetResourceAllocationConfigRuleList();

                response = request.CreateResponse<List<ResourceAllocationConfigRuleVO>>(HttpStatusCode.OK, resourceallocationconfiglist);

                return response;
            });
        }

        [Route("api/ResourceAllocationConfigRuleReferences")]
        public HttpResponseMessage GetResourceAllocationConfigRuleReferencesVO(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                RAConfigruleReferenceVO ResourceAllocationConfigRuleReferences = _resourceallocationconfigruleservice.GetResourceAllocationConfigRuleReferencesVO();

                response = request.CreateResponse<RAConfigruleReferenceVO>(HttpStatusCode.OK, ResourceAllocationConfigRuleReferences);

                return response;
            });
        }

        [HttpPost]
        [Route("api/ResourceAllocationConfigRule")]
        public HttpResponseMessage PostResourceAllocationConfigRuleData(HttpRequestMessage request, ResourceAllocationConfigRuleVO ResourceData)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (User.Identity.IsAuthenticated == false)
                {

                }
                else
                {//This id has to be set to anonymous user id.
                    ResourceData.CreatedBy = 1;
                }
                ResourceAllocationConfigRuleVO add_resourceData = _resourceallocationconfigruleservice.AddResourceAllocationConfigRule(ResourceData);
                response = request.CreateResponse<ResourceAllocationConfigRuleVO>(HttpStatusCode.Created, add_resourceData);
                return response;
            });
        }

        [HttpPut]
        [Route("api/ResourceAllocationConfigRule")]
        public HttpResponseMessage PutResourceAllocationConfigRuleData(HttpRequestMessage request, ResourceAllocationConfigRuleVO ResourceData)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (User.Identity.IsAuthenticated == false)
                {

                }
                else
                {//This id has to be set to anonymous user id.
                    ResourceData.CreatedBy = 1;
                }
                ResourceAllocationConfigRuleVO add_resourceData = _resourceallocationconfigruleservice.ModifyResourceAllocationConfigRule(ResourceData);
                response = request.CreateResponse<ResourceAllocationConfigRuleVO>(HttpStatusCode.Created, add_resourceData);
                return response;
            });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _resourceallocationconfigruleservice.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
