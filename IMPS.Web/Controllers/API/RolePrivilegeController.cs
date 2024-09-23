using IPMS.Domain.ValueObjects;
using IPMS.ServiceProxies.Clients;
using IPMS.ServiceProxies.Contracts;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;



namespace IPMS.Web.Api
{
    public class RolePrivilegeController : ApiControllerBase
    {
        IRolePrivilegeService _RolePrivilegeservice;

        public RolePrivilegeController()
        {
            _RolePrivilegeservice = new RolePrivilegeClient();
        }

        /// <summary>
        /// To Get Role Privilege Details
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>      
        [Authorize]
        [Route("api/RolePrivileges")]
        [HttpGet]
        public HttpResponseMessage GetRolePrivilegeDetails(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                IEnumerable<RoleVO> RolePrivilege = _RolePrivilegeservice.RolePrivilegeDetails();
                response = request.CreateResponse<IEnumerable<RoleVO>>(HttpStatusCode.OK, RolePrivilege);
                return response;
            });
        }


        [Authorize]
        public HttpResponseMessage GetModuleDetails(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                IEnumerable<ModuleVO> Module = _RolePrivilegeservice.GetModuleDetails();
                response = request.CreateResponse<IEnumerable<ModuleVO>>(HttpStatusCode.OK, Module);
                return response;
            });
        }

        /// <summary>
        /// To Get SubModules based on Moduleid
        /// </summary>
        /// <param name="request"></param>
        /// <param name="moduleId"></param>
        /// <returns></returns>      
        [Authorize]
        [Route("api/SubModules/{Moduleid}")]
        [HttpGet]
        public HttpResponseMessage GetSubModulesWithModuleId(HttpRequestMessage request, int moduleId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                IEnumerable<ModuleVO> SubModule = _RolePrivilegeservice.GetSubModulesWithModuleId(moduleId);
                response = request.CreateResponse<IEnumerable<ModuleVO>>(HttpStatusCode.OK, SubModule);
                return response;
            });
        }



        /// <summary>
        /// To Get Module Details
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>       
        [Authorize]
        [Route("api/RoleReferenceData")]
        [HttpGet]
        public HttpResponseMessage GetRoleReferenceData(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                ReferenceDataVO Module = _RolePrivilegeservice.GetRoleReferenceData();
                response = request.CreateResponse<ReferenceDataVO>(HttpStatusCode.OK, Module);
                return response;
            });
        }

       /// <summary>
       /// To Get Entities with Privileges
       /// </summary>
       /// <param name="request"></param>
       /// <param name="Moduleid"></param>
       /// <param name="SubModuleid"></param>
       /// <param name="RoleID"></param>
       /// <returns></returns>
        [Authorize]
        [Route("api/EntitiesWithPrivileges/{Moduleid}/{SubModuleid}/{RoleID}")]
        [HttpGet]
        public HttpResponseMessage GetEntitiesWithPrivileges(HttpRequestMessage request, int moduleId, int subModuleId, int roleId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<EntityListPrivlegesVO> Module = _RolePrivilegeservice.GetEntitiesWithPrivileges(moduleId, subModuleId, roleId);
                response = request.CreateResponse(HttpStatusCode.OK,Module);
                return response;
            });
        }

        [Authorize]
        public HttpResponseMessage GetRolesPrivinEdit(HttpRequestMessage request, int moduleId, int subModuleId, int roleId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<RolePrivlegeListVO> Roles = _RolePrivilegeservice.GetRolesPrivilegeEdit(moduleId, subModuleId, roleId);
                response = request.CreateResponse(HttpStatusCode.OK, Roles);
                return response;
            });
        }

         /// <summary>
         /// To Add Role Privilege Data
         /// </summary>
         /// <param name="request"></param>
         /// <param name="value"></param>
         /// <returns></returns>
        [Authorize]
        [Route("api/RolePrivileges")]  
        [HttpPost]
        public HttpResponseMessage PostRolePrivilegeData(HttpRequestMessage request, RoleVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                RoleVO roleprivilegeCreated = _RolePrivilegeservice.AddRolePrivilege(value);
                response = request.CreateResponse<RoleVO>(HttpStatusCode.Created, roleprivilegeCreated);
                return response;
            });
        }

        /// <summary>
        /// To Modify Role Privilege Data
        /// </summary>
        /// <param name="request"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [Authorize]
        [Route("api/RolePrivileges")] 
        [HttpPut]
        public HttpResponseMessage PutRolePrivilege(HttpRequestMessage request, RoleVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                RoleVO roleprivilegeCreated = _RolePrivilegeservice.ModifyRolePrivilege(value);
                response = request.CreateResponse<RoleVO>(HttpStatusCode.Created, roleprivilegeCreated);
                return response;
            });
        }

        [Authorize]
        public HttpResponseMessage PostDeletesubcategoryData(HttpRequestMessage request, long id)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                RolePrivilegeVO roleprivilegeCreated = _RolePrivilegeservice.DeleteRolePrivilege(id);
                response = request.CreateResponse<RolePrivilegeVO>(HttpStatusCode.Created, roleprivilegeCreated);
                return response;
            });
        }
        [Authorize]
        public RolePrivilegeVO PutDeleteroleprivilege([FromUri]long id)
        {
            return _RolePrivilegeservice.DeleteRolePrivilege(id);
        }
        
    }
}
