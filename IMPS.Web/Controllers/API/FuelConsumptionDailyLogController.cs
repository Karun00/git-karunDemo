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
namespace IPMS.Web.API
{
    public class FuelConsumptionDailyLogController : ApiControllerBase
    {
        IFuelConsumptionDailyLogService _fuelconsumptiondailylogservice;
        //ISecurityAdapter _SecurityAdapter;


        public FuelConsumptionDailyLogController()
        {
            _fuelconsumptiondailylogservice = new FuelConsumptionDailyLogClient();
          //  _SecurityAdapter = new SecurityAdapter();
        }



        /// <summary>
        /// To get all Previous Fuel consumption daily logs
        /// </summary>
        /// <param name="request"></param>
        /// <param name="craftId"></param>
        /// <returns></returns>
        [Route("api/GetFUELLogGriddetails")]
        [HttpGet]
        public HttpResponseMessage GetFuelConsumptionDailyLoggridDetails(HttpRequestMessage request, int craftId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<FuelConsumptionDailyLogVO> fuelconsumptiondailylog = _fuelconsumptiondailylogservice.GetFuelConsumptionDailyLoggridDetails(craftId);
                response = request.CreateResponse<List<FuelConsumptionDailyLogVO>>(HttpStatusCode.Created, fuelconsumptiondailylog);

                return response;
            });
        }



        /// <summary>
        /// Get Crafts data.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("api/GetCraftDetails")]
        [HttpGet]
        public HttpResponseMessage GetCraftDetails(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                string searchvalue = HttpContext.Current.Request.Params["filter[filters][0][value]"];
                List<CraftVO> VCNDtls = _fuelconsumptiondailylogservice.GetCraftDetails(searchvalue); //.Where(x => x.CraftName.StartsWith(searchValue, StringComparison.InvariantCultureIgnoreCase)).ToList();
                response = request.CreateResponse<List<CraftVO>>(HttpStatusCode.OK, VCNDtls);
                return response;

            });
        }

        /// <summary>
        /// This method is used to get all Fuel consumption daily logs records.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("api/GetAllFuelConsumptionDailyLogDetails")]
        [HttpGet]
        public HttpResponseMessage GetAllFuelConsumptionDailyLogDetails(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<FuelConsumptionDailyLogVO> fuelconsumptiondailylog = _fuelconsumptiondailylogservice.GetAllFuelConsumptionDailyLogDetails();
                response = request.CreateResponse<List<FuelConsumptionDailyLogVO>>(HttpStatusCode.OK, fuelconsumptiondailylog);
                return response;
            });
        }


        /// <summary>
        /// This method is used to Insert the data.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="fuelconsumptiondailylogdata"></param>
        /// <returns></returns>
        [Route("api/AddFuelConsumptionDailyLog")]
        [HttpPost]
        public HttpResponseMessage PostFuelConsumptionDailyLog(HttpRequestMessage request, FuelConsumptionDailyLogVO fuelconsumptiondailylogdata)
         {
             return GetHttpResponse(request, () =>
             {
                 HttpResponseMessage response = null;
                 //if (user.identity.isauthenticated)
                 //{
                 //    if (!websecurity.initialized)
                 //        websecurity.initializedatabaseconnection("tnpacontext", "users", "userid", "username", autocreatetables: false);
                 //    var userid = _securityadapter.getuserid(user.identity.name);
                 //    fuelconsumptiondailylogdata.modifiedby = userid;
                 //    fuelconsumptiondailylogdata.createdby = userid;
                 //    fuelconsumptiondailylogdata.modifieddate = system.datetime.now;
                 //    fuelconsumptiondailylogdata.createddate = system.datetime.now;
                 //}
                 //else
                 //{
                 //    fuelconsumptiondailylogdata.createdby = 1;
                 //    fuelconsumptiondailylogdata.modifiedby = 1;
                 //    fuelconsumptiondailylogdata.modifieddate = system.datetime.now;
                 //    fuelconsumptiondailylogdata.createddate = system.datetime.now;
                 //}

                 FuelConsumptionDailyLogVO fuelconsumptiondailylog = _fuelconsumptiondailylogservice.AddFuelConsumptionDailyLog(fuelconsumptiondailylogdata);
                 response = request.CreateResponse<FuelConsumptionDailyLogVO>(HttpStatusCode.Created, fuelconsumptiondailylog);
    
                 return response;
             });
         }


        /// <summary>
        /// This method is used to Modify the data.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="fuelconsumptiondailylogdata"></param>
        /// <returns></returns>
        [Route("api/ModifyFuelConsumptionDailyLog")]
        [HttpPut]
        public HttpResponseMessage PutFuelConsumptionDailyLog(HttpRequestMessage request, FuelConsumptionDailyLogVO fuelconsumptiondailylogdata)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                //if (User.Identity.IsAuthenticated)
                //{
                //    if (!WebSecurity.Initialized)
                //        WebSecurity.InitializeDatabaseConnection("TnpaContext", "Users", "UserID", "UserName", autoCreateTables: false);
                //    var userId = _SecurityAdapter.GetUserId(User.Identity.Name);
                //    fuelconsumptiondailylogdata.ModifiedBy = userId;
                //    fuelconsumptiondailylogdata.CreatedBy = userId;
                //    fuelconsumptiondailylogdata.ModifiedDate = System.DateTime.Now;
                //    fuelconsumptiondailylogdata.CreatedDate = System.DateTime.Now;
                //}
                //else
                //{
                //    fuelconsumptiondailylogdata.CreatedBy = 1;
                //    fuelconsumptiondailylogdata.ModifiedBy = 1;
                //    fuelconsumptiondailylogdata.ModifiedDate = System.DateTime.Now;
                //    fuelconsumptiondailylogdata.CreatedDate = System.DateTime.Now;
                //}


                FuelConsumptionDailyLogVO fuelconsumptiondailylog = _fuelconsumptiondailylogservice.ModifyFuelConsumptionDailyLog(fuelconsumptiondailylogdata);
                response = request.CreateResponse<FuelConsumptionDailyLogVO>(HttpStatusCode.Created, fuelconsumptiondailylog);

                return response;
            });
        }

       

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _fuelconsumptiondailylogservice.Dispose();
            }
            base.Dispose(disposing);
        }


    }
}
