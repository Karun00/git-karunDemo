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
using System.Globalization;
namespace IPMS.Web.API
{
    public class Hour24AndSection625Controller : ApiControllerBase
    {
        IHour24Report625Service _hour24report625service;
        //ISecurityAdapter _SecurityAdapter;
        public Hour24AndSection625Controller()
        {
            _hour24report625service = new Hour24Report625ServiceClient();
          
        }
         [Route("api/hour24report625List")]
        public HttpResponseMessage Gethour24Report625List(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                List<Hour24Report625VO> hours24and625SList = _hour24report625service.GetHour24Report625SList();
                response = request.CreateResponse<List<Hour24Report625VO>>(HttpStatusCode.OK, hours24and625SList);
                return response;
            });
        }

         [Route("api/Hour24ReportReferenceData")]
        public HttpResponseMessage GetHour24ReportReferenceData(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                Hour24ReportReferenceDataVO Referencedata = _hour24report625service.GetHour24ReportReferenceData();

                response = request.CreateResponse<Hour24ReportReferenceDataVO>(HttpStatusCode.OK, Referencedata);

                return response;
            });
        }


         [HttpPost]
         [Route("api/Hour24Report625")]
         public HttpResponseMessage PostAddHour24Report625(HttpRequestMessage request, Hour24Report625VO data)
         {
             return GetHttpResponse(request, () =>
             {
                 HttpResponseMessage response = null;
                 Hour24Report625VO Hour24Report625 = null;
                 //if (User.Identity.IsAuthenticated)
                 //{
                 //    //if (!WebSecurity.Initialized)
                 //    //    WebSecurity.InitializeDatabaseConnection("TnpaContext", "Users", "UserID", "UserName", autoCreateTables: false);
                 //    //var userId = 1;
                 //    //LicensingRequestData.ModifiedBy = userId;
                 //    //LicensingRequestData.CreatedBy = userId;
                 //}
                 //else
                 //{
                 //    Data.CreatedBy = 1;
                 //    Data.ModifiedBy = 1;
                 //}

                 using (IHour24Report625Service _hour24report625service = new Hour24Report625ServiceClient())
                 {
                     data.CreatedBy = data.CreatedBy==0 ? 1 : data.CreatedBy;  // User.Identity.Name;
                     Hour24Report625 = _hour24report625service.AddHour24Report625(data);
                 }


                 response = request.CreateResponse<Hour24Report625VO>(HttpStatusCode.OK, Hour24Report625);
                 return response;
             });
         }

         [HttpGet]
         [Route("api/FormHour24Report625")]
         public HttpResponseMessage PostFormHour24Report625(HttpRequestMessage request, string id, string value)
         {
             return GetHttpResponse(request, () =>
             {
                 HttpResponseMessage response = null;
                 Hour24Report625VO Hour24Report625 = null;
               using (IHour24Report625Service _hour24report625service = new Hour24Report625ServiceClient())
                 {
                     Hour24Report625 = _hour24report625service.Gethoursreportdetailsbyid(value, Convert.ToInt32(id, CultureInfo.InvariantCulture));
                 }


                 response = request.CreateResponse<Hour24Report625VO>(HttpStatusCode.OK, Hour24Report625);
                 return response;
             });
         }


         [HttpPut]
         [Route("api/Hour24Report625")]
         public HttpResponseMessage PutAddHour24Report625(HttpRequestMessage request, Hour24Report625VO data)
         {
             return GetHttpResponse(request, () =>
             {
                 HttpResponseMessage response = null;
                 Hour24Report625VO Hour24Report625 = null;
                 //if (User.Identity.IsAuthenticated)
                 //{
                 //    //if (!WebSecurity.Initialized)
                 //    //    WebSecurity.InitializeDatabaseConnection("TnpaContext", "Users", "UserID", "UserName", autoCreateTables: false);
                 //    //var userId = 1;
                 //    //LicensingRequestData.ModifiedBy = userId;
                 //    //LicensingRequestData.CreatedBy = userId;
                 //}
                 //else
                 //{
                 //    Data.CreatedBy = 1;
                 //    Data.ModifiedBy = 1;
                 //}

                 using (IHour24Report625Service _hour24report625service = new Hour24Report625ServiceClient())
                 {
                     Hour24Report625 = _hour24report625service.EditHour24Report625(data);
                 }


                 response = request.CreateResponse<Hour24Report625VO>(HttpStatusCode.OK, Hour24Report625);
                 return response;
             });
         }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _hour24report625service.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}
