using IPMS.Domain.ValueObjects;
using IPMS.ServiceProxies.Clients;
using IPMS.ServiceProxies.Contracts;
//using IPMS.Web.Api;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace IPMS.Web.Api
{
    public class RosterController : ApiControllerBase
    {
        IRosterService _Rosterservice;
        public RosterController()
        {
            _Rosterservice = new RosterClient();
        }

        [Authorize]
        [Route("api/Rosters")]
        [HttpGet]
        public HttpResponseMessage GetRosterList(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<RosterVO> rosters = _Rosterservice.GetRosterlist();
                response = request.CreateResponse<List<RosterVO>>(HttpStatusCode.OK, rosters);
                return response;
            });
        }



        //[Authorize]
        //[Route("api/GetReferenceData")]
        //[HttpGet]
        public HttpResponseMessage GetReferenceData(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                RosterReferenceVO referencedata = _Rosterservice.GetRosterReferencesData();
                response = request.CreateResponse<RosterReferenceVO>(HttpStatusCode.OK, referencedata);
                return response;
            });
        }
        [Authorize]
        [Route("api/RosterDetails")]
        [HttpPost]
        public HttpResponseMessage RosterDetails(HttpRequestMessage request, RosterVO data)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<RosterVO> rostershifts = _Rosterservice.GetRosterDetails(data);
                response = request.CreateResponse<List<RosterVO>>(HttpStatusCode.OK, rostershifts);

                return response;
            });
        }

        [Authorize]
        [Route("api/SaveRosterData")]
        [HttpPost]
        public HttpResponseMessage PostRosterData(HttpRequestMessage request, RosterGroupVO data)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                int id = _Rosterservice.SaveRosterDetails(data);
                response = request.CreateResponse<int>(HttpStatusCode.OK, id);

                return response;
            });
        }

    }
}
