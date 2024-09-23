using IPMS.Domain.ValueObjects;
using IPMS.ServiceProxies.Clients;
using IPMS.ServiceProxies.Contracts;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace IPMS.Web.Api
{
    public class EntitiesController : ApiControllerBase
    {

        IEntityService _EntityService;      
        public EntitiesController()
        {
            _EntityService = new EntityClient();
        }

        [Authorize]
        [Route("api/Entities")]
        [HttpGet]
        public HttpResponseMessage GetEntityDetails(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<EntityModulesVO> Entities = _EntityService.EntityDetails();
                response = request.CreateResponse<List<EntityModulesVO>>(HttpStatusCode.OK, Entities);
                return response;
            });
        }

        [Authorize]
        [Route("api/EntitySubModules")]
        [HttpGet]
        public HttpResponseMessage GetSubModuleList(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<EntityModulesVO> submods = _EntityService.GetAllSubModules();
                response = request.CreateResponse<List<EntityModulesVO>>(HttpStatusCode.OK, submods);
                return response;
            });
        }

        [Authorize]
        [Route("api/EntityPrivilege")]
        [HttpGet]
        public HttpResponseMessage GetPrivilegeTypes(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<SubCategoryVO> PrivilegeTypes = _EntityService.GetPrivilegeTypes();
                response = request.CreateResponse<List<SubCategoryVO>>(HttpStatusCode.OK, PrivilegeTypes);
                return response;
            });
        }

        [Authorize]      
        [Route("api/Entities")]       
        [HttpPost]
        public HttpResponseMessage PostEntityData(HttpRequestMessage request, EntityModulesVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                EntityModulesVO entityCreated = _EntityService.AddEntity(value);
                response = request.CreateResponse<EntityModulesVO>(HttpStatusCode.Created, entityCreated);
                return response;
            });
        }
        [Authorize]       
        [Route("api/Entities")]
        [HttpPut]
        public HttpResponseMessage ModifyEntityData(HttpRequestMessage request, EntityModulesVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                EntityModulesVO entityModified = _EntityService.ModifyEntity(value);
                response = request.CreateResponse<EntityModulesVO>(HttpStatusCode.Created, entityModified);
                return response;
            });
        }


    }
}