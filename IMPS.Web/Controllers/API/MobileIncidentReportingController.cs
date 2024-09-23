using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using IPMS.ServiceProxies.Clients;
using IPMS.ServiceProxies.Contracts;

using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace IPMS.Web.Api
{
    public class MobileIncidentReportingController : ApiControllerBase
    {
        IMobileIncidentReportService _mobileIncidentReportservice;
        IQuayService _quayService;

        //IFileService _fileservice;
        public MobileIncidentReportingController()
        {
            _mobileIncidentReportservice = new MobileIncidentReportClient();
            _quayService = new QuayClient();

            // _fileservice = new FileClient();
        }

        /// <summary>
        /// To get the all incident types
        /// </summary>
        /// <returns>SubCategoryVO--> List of Incident Types</returns>
        [HttpGet]
        public HttpResponseMessage GetIncidentTypes(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<SubCategoryVO> incidents = null;
                using (IMobileIncidentReportService _mobileIncidentReportservice = new MobileIncidentReportClient())
                {
                    incidents = _mobileIncidentReportservice.GetIncidentTypes();
                }
                response = request.CreateResponse<List<SubCategoryVO>>(HttpStatusCode.OK, incidents);
                return response;


            });
        }


        /// <summary>
        /// To add an incident.
        /// This will add the incident data to the IncidentDocument,IncidentNature and Incident tables
        /// </summary>
        /// <param name="IncidentVO">Incident object</param>
        /// <returns>It will return IncidentVO</returns>
        [HttpPost]
        public HttpResponseMessage PostIncidentData(HttpRequestMessage request, IncidentVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                value.CreatedDate = DateTime.Now;
                value.ModifiedDate = DateTime.Now;
                value.RecordStatus = "A";
                IncidentVO incidentCreated = null;
                using (IMobileIncidentReportService _mobileIncidentReportservice = new MobileIncidentReportClient())
                {

                    incidentCreated = _mobileIncidentReportservice.AddIncidentReport(value);
                }
                response = request.CreateResponse<IncidentVO>(HttpStatusCode.Created, incidentCreated);

                return response;
            });
        }

        [Route("api/IncidentReports")]
        [HttpGet]
        public HttpResponseMessage GetIncidentReportList(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<IncidentVO> incidentReportDetails = _mobileIncidentReportservice.GetIncidentReportList();
                response = request.CreateResponse(HttpStatusCode.OK, incidentReportDetails);

                return response;
            });
        }

        [HttpPut]
        public HttpResponseMessage ModifyIncidentData(HttpRequestMessage request, IncidentVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                IncidentVO incidentData = null;
                using (IMobileIncidentReportService _mobileIncidentReportservice = new MobileIncidentReportClient())
                {
                    incidentData = _mobileIncidentReportservice.ModifyIncidentData(value);
                }
                response = request.CreateResponse<IncidentVO>(HttpStatusCode.Created, incidentData);
                return response;
            });
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _mobileIncidentReportservice.Dispose();
                _quayService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
