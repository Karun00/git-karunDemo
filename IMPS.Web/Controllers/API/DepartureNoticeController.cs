using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Linq;
using System.Web;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using IPMS.ServiceProxies.Clients;
using IPMS.ServiceProxies.Contracts;
using IPMS.Web.Api;

namespace IPMS.Web.API
{
    public class DepartureNoticeController : ApiControllerBase
    {
        IDepartureNoticeService _departureNoticeService;

        public DepartureNoticeController()
        {
            _departureNoticeService = new DepartureNoticeClient();
        }

        #region api/DepartureNotice Post-New Insert
        [Authorize]
        [Route("api/DepartureNotice")]
        [HttpPost]
        public HttpResponseMessage PostDepartureNoticeData(HttpRequestMessage request, DepartureNoticeVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                value.CreatedDate = DateTime.Now;
                value.ModifiedDate = DateTime.Now;

                List<DepartureNoticeVO> DepartureNoticeCreated = _departureNoticeService.AddDepartureNotice(value);
                response = request.CreateResponse<List<DepartureNoticeVO>>(HttpStatusCode.Created, DepartureNoticeCreated);
                return response;
            });
        }
        #endregion

        #region api/DepartureNotice Put-Update
        [Authorize]
        [Route("api/DepartureNotice")]
        [HttpPut]
        public HttpResponseMessage ModifyDepartureNoticeData(HttpRequestMessage request, DepartureNoticeVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                value.CreatedDate = DateTime.Now;
                value.ModifiedDate = DateTime.Now;
                List<DepartureNoticeVO> DepartureNoticeCreated = _departureNoticeService.ModifyDepartureNotice(value);
                response = request.CreateResponse<List<DepartureNoticeVO>>(HttpStatusCode.Created, DepartureNoticeCreated);
                return response;
            });
        }
        #endregion

        #region api/GetPendingArrivalNotifications
        [Route("api/GetPendingArrivalNotifications")]
        [HttpGet]
        public HttpResponseMessage GetPendingArrivalNotifications(HttpRequestMessage request, string DepartureID, string VCN, string VesselName, string SubmissionDateFrom, string SubmissionDateTO)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<DepartureNoticeVO> servicetype = _departureNoticeService.GetPendingArrivalNotifications(DepartureID, VCN, VesselName, SubmissionDateFrom, SubmissionDateTO);
                response = request.CreateResponse<List<DepartureNoticeVO>>(HttpStatusCode.OK, servicetype);
                return response;
            });
        }
        #endregion

        /// <summary>
        /// VCN Autocomplete for Search
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// 
        [Route("api/DepartureNoticeVcnDetailsforAutocomplete")]
        [HttpGet]
        public HttpResponseMessage DepartureNoticeVcnDetailsforAutocomplete(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                string searchValue = HttpContext.Current.Request.Params["filter[filters][0][value]"];
                List<RevenuePostingVO> arrivalcommodities = _departureNoticeService.DepartureNoticeVcnDetailsforAutocomplete(searchValue);
                response = request.CreateResponse<List<RevenuePostingVO>>(HttpStatusCode.OK, arrivalcommodities);
                return response;
            });
        }

        /// <summary>
        /// Vessel / IMO No. Autocomplete for Search
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// 
        [Route("api/DepartureNoticeVesselDetailsforAutocomplete")]
        [HttpGet]
        public HttpResponseMessage DepartureNoticeVesselDetailsforAutocomplete(HttpRequestMessage request)
        {

            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                string serchcolumn = HttpContext.Current.Request.Params["columnName"];
                string searchvalue = HttpContext.Current.Request.Params["filter[filters][0][value]"];
                List<VesselVO> Vessels = _departureNoticeService.DepartureNoticeVesselDetailsforAutocomplete(searchvalue);
                response = request.CreateResponse<List<VesselVO>>(HttpStatusCode.OK, Vessels);
                return response;
            });
        }

        #region Workflow Integrated Methods to Acknowledge
        [Route("api/DepartureNotice/Acknowledge")]
        [HttpPost]
        public HttpResponseMessage DepartureNoticeAcknowledge(HttpRequestMessage request, PendingTaskVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                _departureNoticeService.AcknowledgeDepartureNotice(value.ReferenceID, value.Remarks, value.TaskCode);
                response = request.CreateResponse(HttpStatusCode.Created);
                return response;
            });
        }
        #endregion

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _departureNoticeService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
