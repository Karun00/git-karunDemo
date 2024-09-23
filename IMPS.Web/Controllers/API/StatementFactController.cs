using IPMS.Domain.ValueObjects;
using IPMS.ServiceProxies.Clients;
using IPMS.ServiceProxies.Contracts;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;


namespace IPMS.Web.Api
{
    public class StatementFactController : ApiControllerBase
    {
        IStatementFactService _StatemenetFactService;

        public StatementFactController()
        {
            _StatemenetFactService = new StatementFactClient();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize]
        [Route("api/StatementFacts/{vcnSearch}/{vesselName}")]
        [HttpGet]
        public HttpResponseMessage GetStatementFactDetails(HttpRequestMessage request, string vcnsearch, string vesselname)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<StatementVCNVO> Statementfacts = _StatemenetFactService.StatementFactDetails(vcnsearch, vesselname);
                response = request.CreateResponse<List<StatementVCNVO>>(HttpStatusCode.OK, Statementfacts);
                return response;
            });
        }

        /// <summary>
        /// To Get Reference Data 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize]
        [Route("api/StatementFactReferenceData")]
        [HttpGet]
        public HttpResponseMessage GetStatementFactReferenceDataVO(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                StatementFactReferenceDataVO statementfactDetails = _StatemenetFactService.GetStatementFactReferenceDataVO();
                response = request.CreateResponse(HttpStatusCode.OK, statementfactDetails);

                return response;
            });
        }

        /// <summary>
        /// To Get Key Events
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize]
        [Route("api/StatementFactKeyEvents")]
        [HttpGet]
        public HttpResponseMessage GetKeyEventTypes(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<SubCategoryVO> KeyEventTypes = _StatemenetFactService.GetKeyEventTypes();
                response = request.CreateResponse<List<SubCategoryVO>>(HttpStatusCode.OK, KeyEventTypes);
                return response;
            });
        }

        /// <summary>
        ///   To Get VCN Details
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize]
        [Route("api/StatementVCN")]
        [HttpGet]
        public HttpResponseMessage GetVCNs(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                string searchValue = HttpContext.Current.Request.Params["filter[filters][0][value]"];
                List<StatementVCNVO> vcns = _StatemenetFactService.GetStatementVCNS(searchValue);
                response = request.CreateResponse<List<StatementVCNVO>>(HttpStatusCode.OK, vcns);
                return response;
            });
        }

        /// <summary>
        ///  To Get Vessel Information By VCN
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize]
        [Route("api/StatementVCNInfo/{vcn}")]
        [HttpGet]
        public HttpResponseMessage GetVesselByVCN(HttpRequestMessage request, string vcn)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                StatementVCNVO vesselInfo = _StatemenetFactService.GetVesselByVCN(vcn);
                response = request.CreateResponse<StatementVCNVO>(HttpStatusCode.OK, vesselInfo);
                return response;
            });
        }

        /// <summary>
        /// To Add Statement Of Fact
        /// </summary>
        /// <param name="request"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [Authorize]
        [Route("api/StatementFacts")]
        [HttpPost]
        public HttpResponseMessage PostStatementFactData(HttpRequestMessage request, StatementVCNVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                StatementVCNVO statementCreated = _StatemenetFactService.AddStatementFact(value);
                response = request.CreateResponse<StatementVCNVO>(HttpStatusCode.Created, statementCreated);
                return response;
            });
        }

        /// <summary>
        /// To Modify Statement Of Fact
        /// </summary>
        /// <param name="request"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [Authorize]
        [Route("api/StatementFacts")]
        [HttpPut]
        public HttpResponseMessage ModifyStatementFactData(HttpRequestMessage request, StatementVCNVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                StatementVCNVO statementModified = _StatemenetFactService.ModifyStatementFact(value);
                response = request.CreateResponse<StatementVCNVO>(HttpStatusCode.Created, statementModified);
                return response;
            });
        }

        [Authorize]
        [Route("api/StatementTUGInfo/{vcn}")]
        [HttpGet]
        public HttpResponseMessage GetTugsByvcn(HttpRequestMessage request, string vcn)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                StatementVCNVO vesselInfo = _StatemenetFactService.GetTugsByVCN(vcn);
                response = request.CreateResponse<StatementVCNVO>(HttpStatusCode.OK, vesselInfo);
                return response;
            });
        }

    }
}