using IPMS.Domain.ValueObjects;
using IPMS.ServiceProxies.Clients;
using IPMS.ServiceProxies.Contracts;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace IPMS.Web.Api
{

    public class SuperCategoriesController : ApiControllerBase
    {
        ISuperCategoryService _SuperCategoryService;
        public SuperCategoriesController()
        {
            _SuperCategoryService = new SuperCategoryClient();
        }

        /// <summary>
        ///  To Get Super Category Details
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize]
        [Route("api/SuperCategories")]
        [HttpGet]  
        public HttpResponseMessage GetSuperCategoryDetails(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<SuperCategoryVO> supcats = _SuperCategoryService.SuperCategoryDetails();
                response = request.CreateResponse<List<SuperCategoryVO>>(HttpStatusCode.OK, supcats);
                return response;
            });
        }

        /// <summary>
        /// To Add Super Category Data
        /// </summary>
        /// <param name="request"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [Authorize]
        [Route("api/SuperCategories")]
        [HttpPost]
        public HttpResponseMessage PostSuperCategoryData(HttpRequestMessage request, SuperCategoryVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                SuperCategoryVO supcatCreated = _SuperCategoryService.AddSuperCategory(value);
                response = request.CreateResponse<SuperCategoryVO>(HttpStatusCode.Created, supcatCreated);
                return response;
            });
        }

        /// <summary>
        /// To Modify Super Category Data
        /// </summary>
        /// <param name="request"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [Authorize]
        [Route("api/SuperCategories")]
        [HttpPut]
        public HttpResponseMessage ModifySupCat(HttpRequestMessage request, SuperCategoryVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                SuperCategoryVO supcatCreated = _SuperCategoryService.ModifySuperCategory(value);
                response = request.CreateResponse<SuperCategoryVO>(HttpStatusCode.Created, supcatCreated);
                return response;
            });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _SuperCategoryService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}