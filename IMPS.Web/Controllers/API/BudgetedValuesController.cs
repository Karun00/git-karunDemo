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
    public class BudgetedValuesController : ApiControllerBase
    {
        IBudgetedValuesService _BudgetedValuesService;

        public BudgetedValuesController()
        {
            _BudgetedValuesService = new BudgetedValuesClient();
        }

        #region api/FinanaceYearsList
        /// <summary>
        ///  To Get FinanaceYears List
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize]
        [Route("api/FinanaceYearsList")]
        [HttpGet]
        public HttpResponseMessage GetFinanaceYearsList(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<FinancialYearVO> BudgetedValues = _BudgetedValuesService.FinanceYearDetails();
                response = request.CreateResponse<List<FinancialYearVO>>(HttpStatusCode.OK, BudgetedValues);
                return response;
            });
        }
        #endregion

        #region api/BudgetedValuesDetails
        /// <summary>
        /// To save BudgetedValuesDetails
        /// </summary>
        /// <param name="request"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [Authorize]
        [Route("api/BudgetedValuesDetails")]
        [HttpPost]
        public HttpResponseMessage PostBudgetedValuesData(HttpRequestMessage request, FinancialYearVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                FinancialYearVO BudgetedValuesCreated = _BudgetedValuesService.InsertOrUpdateBudgetedValues(value);
                response = request.CreateResponse<FinancialYearVO>(HttpStatusCode.Created, BudgetedValuesCreated);
                return response;
            });
        }
        #endregion

        #region api/GetFinancialYears
        /// <summary>
        /// This method is used for fetches the Financial Years
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("api/GetFinancialYears")]
        [HttpGet]
        public HttpResponseMessage GetFinancialYears(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<FinancialYear> designations = _BudgetedValuesService.GetFinancialYears();
                response = request.CreateResponse<List<FinancialYear>>(HttpStatusCode.OK, designations);
                return response;
            });
        }
        #endregion

        #region api/GetAllPorts
        /// <summary>
        /// This method is used for fetches the Ports
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("api/GetAllPorts")]
        [HttpGet]
        public HttpResponseMessage GetPortDetails(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<Port> serviceTypes = _BudgetedValuesService.GetPortDetails();
                response = request.CreateResponse<List<Port>>(HttpStatusCode.OK, serviceTypes);
                return response;
            });
        }
        #endregion

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _BudgetedValuesService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
