using IPMS.Domain.Models;
using IPMS.Web.Api;
using IPMS.ServiceProxies.Clients;
using IPMS.ServiceProxies.Contracts;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Collections.Generic;
using IPMS.Domain.ValueObjects;

namespace IPMS.Web.API
{
    public class PortInformationController : ApiControllerBase
    {
        private IPortInformationService _portInformationService = null;

        public PortInformationController()
        {
            _portInformationService = new PortInformationClient();
        }

        [Route("api/PortInformationReferenceData")]
        [HttpGet]
        public HttpResponseMessage GetPortReferenceData(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                PortInformationReferenceVO PortRefData = _portInformationService.GetPortInformationReferenceData();
                response = request.CreateResponse(HttpStatusCode.OK, PortRefData);
                return response;
            });
        }

        [Route("api/PortInformation")]
        [HttpPost]
        public HttpResponseMessage PostPortContentData(HttpRequestMessage request, PortContentVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                PortContentVO portContentCreated = _portInformationService.AddPortContent(value);
                response = request.CreateResponse<PortContentVO>(HttpStatusCode.Created, portContentCreated);
                return response;
            });
        }

        [Route("api/PortRoles")]
        [HttpGet]
        public HttpResponseMessage GetRoles(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                List<Role> Roles = _portInformationService.GetRolesForEmployee();
                response = request.CreateResponse<List<Role>>(HttpStatusCode.OK, Roles);
                return response;
            });
        }

        [Route("api/PortRoleInformation/{PortcontentId}")]
        [HttpGet]
        public HttpResponseMessage GetPortContentRoles(HttpRequestMessage request, int PortcontentId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<PortContentRoleVO> portcontentroledetails = _portInformationService.GetPortContentRoles(PortcontentId);
                response = request.CreateResponse<List<PortContentRoleVO>>(HttpStatusCode.OK, portcontentroledetails);
                return response;
            });
        }

        [Route("api/Portdocumentdetails/{DocumentId}")]
        [HttpGet]
        public HttpResponseMessage GetDocumentDetails(HttpRequestMessage request, int id)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                Document documentdetials = _portInformationService.GetDocumentDetails(id);
                response = request.CreateResponse<Document>(HttpStatusCode.OK, documentdetials);
                return response;
            });
        }

        [Route("api/PortInformation")]
        [HttpPut]
        public HttpResponseMessage ModifyPortContentData(HttpRequestMessage request, PortContentVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                PortContentVO portContetedModify = _portInformationService.ModifyPortContent(value);
                response = request.CreateResponse<PortContentVO>(HttpStatusCode.Created, portContetedModify);
                return response;
            });
        }

        [Route("api/PortInformation")]
        [HttpGet]
        public HttpResponseMessage GetPortInformationTreeview(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                IEnumerable<PortContent> portcontentdetails = _portInformationService.GetPortContentForTreeView();
                response = request.CreateResponse<IEnumerable<PortContent>>(HttpStatusCode.OK, portcontentdetails);
                return response;
            });
        }
    }
}

