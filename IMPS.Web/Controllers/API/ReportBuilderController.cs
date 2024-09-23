using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using IPMS.ServiceProxies.Clients;
using IPMS.ServiceProxies.Contracts;
using IPMS.Web.Api;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Linq;

namespace IPMS.Web.Controllers.API
{
    public class ReportBuilderController : ApiControllerBase
    {
        IReportBuilderService _reportbuilderservice;

        public ReportBuilderController()
        {
            _reportbuilderservice = new ReportBuilderClient();
        }

        public HttpResponseMessage GetReportCategory(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                IEnumerable<ReportBuilderVO> reportcategory = null;

                using (IReportBuilderService ls = new ReportBuilderClient())
                {
                    reportcategory = ls.GetReportCategory();
                }
                response = request.CreateResponse<IEnumerable<ReportBuilderVO>>(HttpStatusCode.OK, reportcategory);
                return response;
            });
        }

        public HttpResponseMessage GetReportQueryOperator(HttpRequestMessage request, string datatype)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                List<ReportQueryOperatorVO> reportqueryoperator = null;

                using (IReportBuilderService ls = new ReportBuilderClient())
                {
                    reportqueryoperator = ls.GetReportQueryOperator(datatype);
                }
                response = request.CreateResponse<List<ReportQueryOperatorVO>>(HttpStatusCode.OK, reportqueryoperator);
                return response;
            });
        }

        public HttpResponseMessage GetReportCategoryColumn(HttpRequestMessage request, int reportbilderid)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                IEnumerable<ReportCategoryColumnVO> reportcategorycolumn = null;

                using (IReportBuilderService ls = new ReportBuilderClient())
                {
                    reportcategorycolumn = ls.GetReportCategoryColumn(reportbilderid);
                }
                response = request.CreateResponse<IEnumerable<ReportCategoryColumnVO>>(HttpStatusCode.OK, reportcategorycolumn);
                return response;
            });
        }
        
      


        [HttpPost]
        public HttpResponseMessage ReportBuilderGridData(HttpRequestMessage request, ReportBuilderVO rbitem)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                IEnumerable<ReportBuilderVO> reportbuildergriddata = null;

                using (IReportBuilderService ls = new ReportBuilderClient())
                {
                    reportbuildergriddata = ls.GetReportBuilderGridData(rbitem);
                }
                response = request.CreateResponse<IEnumerable<ReportBuilderVO>>(HttpStatusCode.OK, reportbuildergriddata);
                return response;
            });
        }

        [Authorize]
        [HttpPost]
        public HttpResponseMessage AddReportQueryTemplate(HttpRequestMessage request, ReportQueryTemplateVO value)
        {

            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                ReportQueryTemplateVO reportqueryTemplateCreated = _reportbuilderservice.AddReportQueryTemplate(value);
                response = request.CreateResponse<ReportQueryTemplateVO>(HttpStatusCode.Created, reportqueryTemplateCreated);
                return response;

            });

        }
        [Authorize]
        [HttpGet]
        public HttpResponseMessage GetReportFilterData(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<ReportQueryTemplateVO> reportfilter = null;
                using (IReportBuilderService ls = new ReportBuilderClient())
                {
                    reportfilter = ls.GetReportFilterData();
                }
                response = request.CreateResponse<List<ReportQueryTemplateVO>>(HttpStatusCode.OK, reportfilter);
                return response;
            });
        }

        [Authorize]
        [HttpGet]
        public HttpResponseMessage GetLookUpData(HttpRequestMessage request, string columnName)
        {
            return GetHttpResponse(request, () =>
            {

                HttpResponseMessage response = null;

                string searchValue = HttpContext.Current.Request.Params["filter[filters][0][value]"];
                List<ReportLookUpVO> lookup = _reportbuilderservice.GetLookUpData(columnName, searchValue).ToList();
                response = request.CreateResponse<List<ReportLookUpVO>>(HttpStatusCode.OK, lookup);
                return response;

                //HttpResponseMessage response = null;
                //List<ReportLookUpVO> reportfilter = null;
                //using (IReportBuilderService ls = new ReportBuilderClient())
                //{
                //    reportfilter = ls.GetLookUpData(rlookVO);
                //}
                //response = request.CreateResponse<List<ReportLookUpVO>>(HttpStatusCode.OK, reportfilter);
                //return response;

            });
        }

        [HttpGet]
        public HttpResponseMessage GetDateFormatConfig(HttpRequestMessage request, string portcode)
        {
            return GetHttpResponse(request, () =>
            {
                var DateFormatType = (Domain.ConfigName.DateFormat).ToString();
                HttpResponseMessage response = null;

                string DateFormatConfig = string.Empty;

                using (IReportBuilderService ls = new ReportBuilderClient())
                {
                    DateFormatConfig = ls.GetDateFormatConfig(DateFormatType, portcode);
                }
                response = request.CreateResponse<string>(HttpStatusCode.OK, DateFormatConfig);
                return response;
            });
        }


        [HttpGet]
        public HttpResponseMessage DeleteReportQueryTemplate(HttpRequestMessage request, int ID)
        {
            return GetHttpResponse(request, () =>
            {
                
                HttpResponseMessage response = null;
                ReportQueryTemplate reportfilter = null;

                using (IReportBuilderService ls = new ReportBuilderClient())
                {
                  reportfilter = ls.DeleteReportQueryTemplate(ID);
                }
                response = request.CreateResponse<ReportQueryTemplate>(HttpStatusCode.OK, reportfilter);
                return response;
            });
        }

    }
}
