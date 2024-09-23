using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using IPMS.ServiceProxies.Clients;
using IPMS.ServiceProxies.Contracts;
using IPMS.Web.Api;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using IPMS.Web.Controllers;

namespace IPMS.Web.API
{
    public class NewsController : ApiControllerBase
    {
        INewsService _Newsservice;
      
        public NewsController()
        {
            _Newsservice = new NewsClient();
           
        }
       
        //[Authorize]
        [Route("api/News")]
        public HttpResponseMessage GetNewsInfo(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<NewsVO> news = _Newsservice.GetNewsList();
                response = request.CreateResponse<List<NewsVO>>(HttpStatusCode.OK, news);
                return response;
            });
        }
        //[Authorize]
        [Route("api/News")]
        [HttpPost]
        public HttpResponseMessage PostNewsData(HttpRequestMessage request, NewsVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (User.Identity.IsAuthenticated)
                {
                    //if (!WebSecurity.Initialized)
                    //    WebSecurity.InitializeDatabaseConnection("TnpaContext", "Users", "UserID", "UserName", autoCreateTables: false);
                    //var userId = 1;
                    //value.CreatedBy = userId;
                    //value.CreatedDate = DateTime.Now;
                    //value.ModifiedBy = userId;
                    //value.ModifiedDate = DateTime.Now;
                    //value.RecordStatus = "A";
                }
                else
                {
                    value.CreatedBy = 1;//This id has to be set to anonymous user id.
                }
                NewsVO newsCreated = _Newsservice.AddNews(value);              
                Controllers.ChatHubs.ChatHub nHub = new Controllers.ChatHubs.ChatHub();
                nHub.Show();
                response = request.CreateResponse<NewsVO>(HttpStatusCode.Created, newsCreated);
                return response;

                //Controllers.ChatHubs.NewsHub nHub = new Controllers.ChatHubs.NewsHub();
                //nHub.Show();
               
            });
        }
        //[Authorize]
        [Route("api/News")]
        [HttpPut]
        public HttpResponseMessage ModifyNews(HttpRequestMessage request, NewsVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (User.Identity.IsAuthenticated)
                {
                    //if (!WebSecurity.Initialized)
                    //    WebSecurity.InitializeDatabaseConnection("TnpaContext", "Users", "UserID", "UserName", autoCreateTables: false);
                    //var userId = 1;
                    //value.CreatedBy = userId;
                    //value.CreatedDate = DateTime.Now;
                    //value.ModifiedBy = userId;
                    //value.ModifiedDate = DateTime.Now;

                }
                else
                {
                    value.CreatedBy = 1;//This id has to be set to anonymous user id.
                }

                NewsVO newsCreated = _Newsservice.ModifyNews(value);
                Controllers.ChatHubs.ChatHub nHub = new Controllers.ChatHubs.ChatHub();
                nHub.Show();
                response = request.CreateResponse<NewsVO>(HttpStatusCode.Created, newsCreated);
                return response;
            });
        }

         [Route("api/NewsScroll")]
        public HttpResponseMessage GetNewsForScroll(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<NewsVO> news = _Newsservice.GetNewsForScroll();
                response = request.CreateResponse<List<NewsVO>>(HttpStatusCode.OK, news);
                return response;
            });
        }

        
      

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _Newsservice.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}