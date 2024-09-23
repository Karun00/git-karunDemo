using Core.Repository;
using IPMS.Data.Context;
using IPMS.Domain.Models;
using IPMS.ServiceProxies.Clients;
using IPMS.ServiceProxies.Contracts;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using System;
using IPMS.Web.Api;
using IPMS.Domain.ValueObjects;
using System.ServiceModel;
using System.Web;
using System.Linq;


namespace IPMS.Web.API
{
    public class UserPreferenceController : ApiControllerBase
    {
        IUserPreferenceService _userpreferenceService;

        public UserPreferenceController()
        {
            _userpreferenceService = new UserPreferenceClient();
           
        }

         [HttpGet]
        public HttpResponseMessage GetUserPreferenceDetails(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<UserPreferenceVO> val = _userpreferenceService.GetUserPreferenceDetails();
                response = request.CreateResponse(HttpStatusCode.OK, val);
                return response;
            });
        }

         public HttpResponseMessage GetUserPreferenceDetailsByUser(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<UserPreferenceVO> val = _userpreferenceService.GetUserPreferenceDetailsByUser();
                response = request.CreateResponse(HttpStatusCode.OK, val);
                return response;
            });
        }
        
        [Authorize]
        [HttpPost]
         public HttpResponseMessage PostUserPreference(HttpRequestMessage request, UserPreferenceVO data)
        {
            return GetHttpResponse(request, () =>
            {

                HttpResponseMessage response = null;
                UserPreferenceVO userpreferenceCreated = null;
                using (IUserPreferenceService ls = new UserPreferenceClient())
                {
                    userpreferenceCreated = ls.AddUserPreference(data);
                }
                response = request.CreateResponse<UserPreferenceVO>(HttpStatusCode.Created, userpreferenceCreated);
                return response;
            });
        }
       

       protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _userpreferenceService.Dispose();
            }
            base.Dispose(disposing);
        }      

    }
}
