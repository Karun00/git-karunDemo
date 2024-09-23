using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using IPMS.ServiceProxies.Clients;
using IPMS.ServiceProxies.Contracts;
using IPMS.Web.Api;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace IPMS.Web.API
{
    public class ResourceGroupingController : ApiControllerBase
    {
        IResourceGroupService _resourceGroupService;

        public ResourceGroupingController()
        {
            _resourceGroupService = new ResourceGroupClient();
        }

        /// <summary>
        /// To get designation details
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// 
        [Authorize]
        [Route("api/DesignationDetails")]
        [HttpGet]
        public HttpResponseMessage GetDesignations(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                List<SubCategory> designations = _resourceGroupService.GetDesignations();
                response = request.CreateResponse<List<SubCategory>>(HttpStatusCode.OK, designations);

                return response;
            });
        }

        /// <summary>
        ///  To get employee details by desigantion code
        /// </summary>
        /// <param name="request"></param>
        /// <param name="designationcode"></param>
        /// <returns></returns>
        /// 
        [Route("api/Employees/{ResourceGroupCode,designationcode,mode}")]
        [HttpGet]
        public HttpResponseMessage GetEmployees(HttpRequestMessage request, string ResourceGroupCode, string designationcode, string mode)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                List<Employee> employees = _resourceGroupService.GetEmployees(ResourceGroupCode, designationcode, mode);
                response = request.CreateResponse<List<Employee>>(HttpStatusCode.OK, employees);

                return response;
            });
        }

        /// <summary>
        /// To add respurce group details
        /// </summary>
        /// <param name="request"></param>
        /// <param name="resourcegrp"></param>
        /// <returns></returns>
        /// 
        [Authorize]
        [Route("api/ResourceGrouping")]
        [HttpPost]
        public HttpResponseMessage PostResourceGroupData(HttpRequestMessage request, ResourceGroupVO resourcegrp)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                ResourceGroupVO resourcegroup = _resourceGroupService.AddResourceGroupDetails(resourcegrp);
                response = request.CreateResponse<ResourceGroupVO>(HttpStatusCode.Created, resourcegroup);

                return response;
            });
        }

        /// <summary>
        ///  To modify resource group details
        /// </summary>
        /// <param name="request"></param>
        /// <param name="resourcegrp"></param>
        /// <returns></returns>
        /// 
        [Authorize]
        [Route("api/ResourceGrouping")]
        [HttpPut]
        public HttpResponseMessage PutResourceGroupData(HttpRequestMessage request, ResourceGroupVO resourcegrp)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                ResourceGroupVO resourcegroup = _resourceGroupService.ModifyResourceGroups(resourcegrp);
                response = request.CreateResponse<ResourceGroupVO>(HttpStatusCode.Created, resourcegroup);

                return response;
            });
        }

        /// <summary>
        ///  To get resource group details
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize]
        [Route("api/ResourceGrouping")]
        [HttpGet]
        public HttpResponseMessage GetResourceGroupDetails(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                List<ResourceGroupVO> resgrps = _resourceGroupService.GetResourceGroupDetails();
                response = request.CreateResponse<List<ResourceGroupVO>>(HttpStatusCode.OK, resgrps);

                return response;
            });
        }
    }
}
