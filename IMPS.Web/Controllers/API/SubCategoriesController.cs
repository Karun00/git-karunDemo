using IPMS.Domain.ValueObjects;
using IPMS.ServiceProxies.Clients;
using IPMS.ServiceProxies.Contracts;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace IPMS.Web.Api
{
    public class SubCategoriesController : ApiControllerBase
    {
        ISubCategoryService _SubCategoryservice;
        public SubCategoriesController()
        {
            _SubCategoryservice = new SubCategoryClient();
        }

        #region GetSubCategoryDetails
        /// <summary>
        /// To Get Sub Category Details By Id
        /// </summary>
        /// <param name="request"></param>
        /// <param name="supcatId"></param>
        /// <returns></returns>
        [Authorize]
        [Route("api/SubCategoryDetails/{Supcatid}")]
        [HttpGet]
        public HttpResponseMessage GetSubCategoryDetails(HttpRequestMessage request, string supcatId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<SubCategoryVO> SubCategories = _SubCategoryservice.SubCategoryDetails(supcatId);
                response = request.CreateResponse<List<SubCategoryVO>>(HttpStatusCode.OK, SubCategories);
                return response;
            });
        }
        #endregion

        #region GetAllSubCategoryDetails
        /// <summary>
        /// To Get All Sub Category Details
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize]
        [Route("api/AllSubCategoryDetails")]
        [HttpGet]
        public HttpResponseMessage GetAllSubCategoryDetails(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<SubCategoryVO> AllSubCategories = _SubCategoryservice.AllSubCategoryDetails();
                response = request.CreateResponse<List<SubCategoryVO>>(HttpStatusCode.OK, AllSubCategories);
                return response;
            });
        }
        #endregion

        #region GetSupCatDetails
        /// <summary>
        /// To Get Super Category Details
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize]
        [Route("api/SubCategories")]
        [HttpGet]
        public HttpResponseMessage GetSupCatDetails(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<SuperCategoryVO> supcats = _SubCategoryservice.SuperCategoryDetails();
                response = request.CreateResponse<List<SuperCategoryVO>>(HttpStatusCode.OK, supcats);
                return response;
            });
        }
        #endregion

        #region PostSubCategoryData
        /// <summary>
        /// To Add Sub Category Data
        /// </summary>
        /// <param name="request"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [Authorize]
        [Route("api/SubCategories")]
        [HttpPost]
        public HttpResponseMessage PostSubCategoryData(HttpRequestMessage request, SubCategoryVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                SubCategoryVO subcategoryCreated = _SubCategoryservice.AddSubCategory(value);
                response = request.CreateResponse<SubCategoryVO>(HttpStatusCode.Created, subcategoryCreated);
                return response;
            });
        }
        #endregion

        #region ModifySubCategory
        /// <summary>
        /// To Modify Sub Category
        /// </summary>
        /// <param name="request"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [Authorize]
        [Route("api/SubCategories")]
        [HttpPut]
        public HttpResponseMessage ModifySubCategory(HttpRequestMessage request, SubCategoryVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                SubCategoryVO subcategoryCreated = _SubCategoryservice.ModifySubCategory(value);
                response = request.CreateResponse<SubCategoryVO>(HttpStatusCode.Created, subcategoryCreated);
                return response;
            });
        }
        #endregion

        #region GetsubcategoryDtl
        [Authorize]
        public HttpResponseMessage GetsubcategoryDtl(HttpRequestMessage request, long id)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                SubCategoryVO subcategory = _SubCategoryservice.GetSubCategoryId(id);
                response = request.CreateResponse<SubCategoryVO>(HttpStatusCode.OK, subcategory);
                return response;
            });
        }
        #endregion

        #region PostDeletesubcategoryData
        [Authorize]
        public HttpResponseMessage PostDeletesubcategoryData(HttpRequestMessage request, long id)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                SubCategoryVO subcategoryCreated = _SubCategoryservice.DeleteSubCategory(id);
                response = request.CreateResponse<SubCategoryVO>(HttpStatusCode.Created, subcategoryCreated);
                return response;
            });
        }
        #endregion

        #region PutDeletesubcategory
        [Authorize]
        public SubCategoryVO PutDeletesubcategory([FromUri]long id)
        {
            return _SubCategoryservice.DeleteSubCategory(id);
        }
        #endregion

        #region GetCountriesList
        /// <summary>
        /// To Get Countries List
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        //[Authorize]
        [Route("api/SubCategory/GetCountriesList")]
        [HttpGet]
        public HttpResponseMessage GetCountriesList(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<SubCategoryVO> SubCategories = _SubCategoryservice.GetCountriesList();
                response = request.CreateResponse<List<SubCategoryVO>>(HttpStatusCode.OK, SubCategories);
                return response;
            });
        }
        #endregion

        [Route("api/SubCategory/GetSubCatName")]
        [HttpGet]
        public HttpResponseMessage GetSubCatName(HttpRequestMessage request,string code)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                string subcatName = _SubCategoryservice.GetSubCatName(code);
                response = request.CreateResponse<string>(HttpStatusCode.OK, subcatName);
                return response;
            });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _SubCategoryservice.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}