using Core.Repository;
using IPMS.Data.Context;
using IPMS.Domain.Models;
using IPMS.Web.ServiceProxies.Clients;
using IPMS.Web.ServiceProxies.Contracts;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using System;
using IPMS.Web.Api;
using IPMS.Domain.ValueObjects;
using IPMS.ServiceProxies.Contracts;
using IPMS.ServiceProxies.Clients;

namespace IPMS.Web.API
{
    public class ModuleController : ApiControllerBase
    {
        IModuleService _ModuleService;
        /// <summary>
        /// 
        /// </summary>
        public ModuleController()
        {
            _ModuleService = new ModuleClient();
        }
        /// <summary>
        /// Gets the module list
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize]
        //[Route("api/Module")]
        [HttpGet]
        public HttpResponseMessage GetModuleDetails(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<ModuleVO> modules = _ModuleService.GetModules();
                response = request.CreateResponse<List<ModuleVO>>(HttpStatusCode.OK, modules);
                return response;
            });
        }
        /// <summary>
        /// Gets all parent modules list
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        // [Route("api/Modules")]
        [HttpGet]
        public HttpResponseMessage GetParentModules(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<ModuleVO> departments = _ModuleService.GetParentModules();
                response = request.CreateResponse<List<ModuleVO>>(HttpStatusCode.OK, departments);
                return response;
            });
        }


        [Route("api/Modules")]
        [HttpGet]
        public HttpResponseMessage GetModulesTreeview(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (User.Identity.IsAuthenticated)
                {
                    IAccountService _loginservice = new AccountClient();
                    IEnumerable<Module> modules = _loginservice.GetModulesTreeView();
                    response = request.CreateResponse<IEnumerable<Module>>(HttpStatusCode.OK, modules);
                }
                return response;

            });
        }
        
        
        /// <summary>
        /// Add / Inserts the new module
        /// </summary>
        /// <param name="request"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [Authorize]
        [Route("api/Modules")]
        [HttpPost]
        public HttpResponseMessage PostModuleData(HttpRequestMessage request, ModuleVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                ModuleVO moduleCreated = _ModuleService.PostModuleData(value);
                response = request.CreateResponse<ModuleVO>(HttpStatusCode.Created, moduleCreated);
                return response;
            });
        }

        /// <summary>
        /// Modifies the module
        /// </summary>
        /// <param name="request"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [Authorize]
        [Route("api/Modules")]
        [HttpPut]
        public HttpResponseMessage ModifyModule(HttpRequestMessage request, ModuleVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                ModuleVO moduleCreated = _ModuleService.ModifyModule(value);
                response = request.CreateResponse<ModuleVO>(HttpStatusCode.Created, moduleCreated);
                return response;
            });
        }
        //ships in ports only for admin
        [Authorize]
        [Route("api/Module/GetUserRoles")]
        [HttpGet]
        public HttpResponseMessage GetUserRoles(HttpRequestMessage request)
        {
            var name = User.Identity.Name;
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                IEnumerable<UserRole> roles = _ModuleService.GetUserRoles(name);
                response = request.CreateResponse<IEnumerable<UserRole>>(HttpStatusCode.OK, roles);
                return response;
            });
        }
        /// <summary>
        /// Disposes all objects
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _ModuleService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}