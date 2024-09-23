using Core.Repository;
using IPMS.Data.Context;
using IPMS.Domain.Models;
using IPMS.ServiceProxies.Clients;
using IPMS.ServiceProxies.Contracts;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;
using IPMS.Web.Adapters;
using IPMS.Domain.ValueObjects;
using IPMS.Web.Api;
using IPMS.Web.ServiceProxies.Clients;
using WebMatrix.WebData;
using System.Linq;
using System.Web;
using IPMS.ServiceProxies;

namespace IPMS.Web.API
{
    public class PortGeneralConfigsController : ApiControllerBase
    {

        IPortGeneralConfigsService _portgeneralconfigsservice;
    //    ISecurityAdapter _SecurityAdapter;


        public PortGeneralConfigsController()
        {
            _portgeneralconfigsservice = new PortGeneralConfigsClient();
          //  _SecurityAdapter = new SecurityAdapter();
        }


        /// <summary>
        /// This method is used to get all records.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("api/GetAllPortGeneralConfigsDetails")]
        [HttpGet]
        public HttpResponseMessage GetAllPortGeneralConfigsDetails(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<PortGeneralConfigsVO> portgeneralconfigs = _portgeneralconfigsservice.GetAllPortGeneralConfigsDetails();
                response = request.CreateResponse<List<PortGeneralConfigsVO>>(HttpStatusCode.OK, portgeneralconfigs);
                return response;
            });
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="GroupName"></param>
        /// <returns></returns>
        [Authorize]
        [Route("api/GroupNamesDetails")]
        [HttpGet]
        public HttpResponseMessage GetAllGroupNames(HttpRequestMessage request, string GroupName)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<PortGeneralConfigsVO> portgeneralconfigsgroupname = _portgeneralconfigsservice.GetAllGroupNames(GroupName);
                response = request.CreateResponse<List<PortGeneralConfigsVO>>(HttpStatusCode.OK, portgeneralconfigsgroupname);
                return response;
            });
        }


        /// <summary>
        /// This method is used to Update Port General Configs.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="portgeneralconfigdata"></param>
        /// <returns></returns>
        [Authorize]
        [Route("api/UpdatePortGeneralConfigsDetails")]
        [HttpPut]
        public HttpResponseMessage UpdatePortGeneralConfigs(HttpRequestMessage request, PortGeneralConfigsVO portgeneralconfigdata)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                int updateportgeneralconfigsgroupname = _portgeneralconfigsservice.UpdatePortGeneralConfigs(portgeneralconfigdata);
                response = request.CreateResponse<int>(HttpStatusCode.OK, updateportgeneralconfigsgroupname);
                return response;
            });
        }
        


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _portgeneralconfigsservice.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
