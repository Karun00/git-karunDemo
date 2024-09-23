using IPMS.Domain.ValueObjects;
using IPMS.ServiceProxies.Contracts;
using IPMS.Web.Api;
using System.Collections.Generic;
using IPMS.ServiceProxies.Clients;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace IPMS.Web.Controllers.API
{
    public class ExternalDivingRegisterController : ApiControllerBase
    {
        IExternalDivingRegisterService _ExternalDivingRegisterservice;

        public ExternalDivingRegisterController()
        {
            _ExternalDivingRegisterservice = new ExternalDivingRegisterClient();
        }

        #region AllExternalDivingRegisterDetails
        /// <summary>
        /// Gets External diving register list
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [Route("api/ExternalDivingRegisters")]
        [HttpGet]
        public HttpResponseMessage AllExternalDivingRegisterDetails(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<ExternalDivingRegisterVO> AllExternalDivingRegisters = _ExternalDivingRegisterservice.AllExternalDivingRegisterDetails();
                response = request.CreateResponse<List<ExternalDivingRegisterVO>>(HttpStatusCode.OK, AllExternalDivingRegisters);
                return response;
            });
        }
        #endregion

        #region PostExternalDivingRegisterData
        /// <summary>
        /// adds / inserts the external diving register details
        /// </summary>
        /// <param name="ExternalDivingRegisterdata"></param>
        /// <returns></returns>
        [Authorize]
        [Route("api/ExternalDivingRegisters")]
        [HttpPost]
        public HttpResponseMessage PostExternalDivingRegisterData(HttpRequestMessage request, ExternalDivingRegisterVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                ExternalDivingRegisterVO ExternalDivingRegisterCreated = _ExternalDivingRegisterservice.AddExternalDivingRegister(value);
                response = request.CreateResponse<ExternalDivingRegisterVO>(HttpStatusCode.Created, ExternalDivingRegisterCreated);
                return response;
            });
        }
        #endregion

        #region ModifyExternalDivingRegister
        /// <summary>
        /// Modifies / update the External diving register details
        /// </summary>
        /// <param name="ExternalDivingRegisterdata"></param>
        /// <returns></returns>
        [Authorize]
        [Route("api/ExternalDivingRegisters")]
        [HttpPut]
        public HttpResponseMessage ModifyExternalDivingRegister(HttpRequestMessage request, ExternalDivingRegisterVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                ExternalDivingRegisterVO ExternalDivingRegisterCreated = _ExternalDivingRegisterservice.ModifyExternalDivingRegister(value);
                response = request.CreateResponse<ExternalDivingRegisterVO>(HttpStatusCode.Created, ExternalDivingRegisterCreated);
                return response;
            });
        }
        #endregion

        #region PostDeleteExternalDivingRegisterData
        /// <summary>
        /// Deletes external diving register - not in use
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public HttpResponseMessage PostDeleteExternalDivingRegisterData(HttpRequestMessage request, long id)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                ExternalDivingRegisterVO ExternalDivingRegisterCreated = _ExternalDivingRegisterservice.DeleteExternalDivingRegister(id);
                response = request.CreateResponse<ExternalDivingRegisterVO>(HttpStatusCode.Created, ExternalDivingRegisterCreated);
                return response;
            });
        }
        #endregion

        #region PutDeleteExternalDivingRegister
        [Authorize]
        public ExternalDivingRegisterVO PutDeleteExternalDivingRegister([FromUri]long id)
        {
            return _ExternalDivingRegisterservice.DeleteExternalDivingRegister(id);
        }
        #endregion

        #region GetAllCompanies
        /// <summary>
        /// Gets all Companies
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [Route("api/GetAllCompanies")]
        [HttpGet]
        public HttpResponseMessage GetAllCompanies(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<LicenseRequestVO> AllCompanies = _ExternalDivingRegisterservice.GetAllCompanies();
                response = request.CreateResponse<List<LicenseRequestVO>>(HttpStatusCode.OK, AllCompanies);
                return response;
            });
        }
        #endregion

        #region GetAllVessels
        /// <summary>
        /// Gets all Vessels
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [Route("api/GetAllVessels")]
        [HttpGet]
        public HttpResponseMessage GetAllVessels(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<VesselVO> Allvessels = _ExternalDivingRegisterservice.GetAllVessels();
                response = request.CreateResponse<List<VesselVO>>(HttpStatusCode.OK, Allvessels);
                return response;
            });
        }
        #endregion     

        /// <summary>
        /// To Get Reference Data 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize]
        [Route("api/DivingReferenceData")]
        [HttpGet]
        public HttpResponseMessage GetDivingReferenceData(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                ExternalDivingRegisterVO divingDetails = _ExternalDivingRegisterservice.GetDivingReferenceData();
                response = request.CreateResponse(HttpStatusCode.OK, divingDetails);

                return response;
            });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _ExternalDivingRegisterservice.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}