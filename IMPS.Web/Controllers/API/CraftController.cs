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

namespace IPMS.Web.Controllers.API
{
    public class CraftController : ApiControllerBase
    {
        ICraftMasterService _craftmasterService;

        public CraftController()
        {
            _craftmasterService = new CraftmasterClient();
        }

        //This method is used for get the data to fill grid.
        [Authorize]
        [HttpGet]
        [Route("api/Crafts")]
        public HttpResponseMessage GetCraftList(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<CraftVO> Craftlist = _craftmasterService.GetCraftList();
                response = request.CreateResponse<List<CraftVO>>(HttpStatusCode.OK, Craftlist);
                return response;
            });
        }

        [Authorize]
        [HttpGet]
        [Route("api/Craftreferencedata")]
        //This method is used for get the data to fill alldropdown.
        public HttpResponseMessage GetCraftreferencedata(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                CraftReferenceVO craftDetails = _craftmasterService.GetCraftReferences();
                response = request.CreateResponse(HttpStatusCode.OK, craftDetails);

                return response;
            });
        }

        [Authorize]
        [HttpPost]
        [Route("api/Crafts")]
        //This method is used for insert the data into backend.
        public HttpResponseMessage Postcraftdata(HttpRequestMessage request, CraftVO craftData)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                craftData.CreatedBy = 1;//This id has to be set to anonymous user id.

                CraftVO craft = _craftmasterService.AddCraft(craftData);
                response = request.CreateResponse<CraftVO>(HttpStatusCode.Created, craft);
                return response;
            });
        }

        [Authorize]
        [HttpPut]
        [Route("api/Crafts")]
        //This method is used for Update the data into backend.
        public HttpResponseMessage PutcraftData(HttpRequestMessage request, CraftVO craftData)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                craftData.CreatedBy = 1;//This id has to be set to anonymous user id.
                craftData.ModifiedBy = 1;//This id has to be set to anonymous user id.
                craftData.ModifiedDate = DateTime.Now;
                craftData.CreatedDate = DateTime.Now;

                CraftVO modifyedcraftdata = _craftmasterService.ModifyCraft(craftData);
                response = request.CreateResponse<CraftVO>(HttpStatusCode.Created, modifyedcraftdata);
                return response;
            });
        }

        /// <summary>
        /// To Dispose
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _craftmasterService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
