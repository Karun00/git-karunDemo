using IPMS.Domain.ValueObjects;
using IPMS.ServiceProxies.Clients;
using IPMS.ServiceProxies.Contracts;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace IPMS.Web.Api
{
    public class DeploymentPlanController : ApiControllerBase
    {
        IDeploymentPlanService _DeploymentPlanService;

        public DeploymentPlanController()
        {
            _DeploymentPlanService = new DeploymentPlanClient();
        }

        /// <summary>
        /// To Get Deployment Plan Details
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize]
        [Route("api/DeploymentPlan")]
        [HttpGet]
        public HttpResponseMessage GetDeploymentPlanDetails(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<DeploymentPlanVO> Deploymentplans = _DeploymentPlanService.DeploymentPlanDetails();
                response = request.CreateResponse<List<DeploymentPlanVO>>(HttpStatusCode.OK, Deploymentplans);
                return response;
            });
        }


        /// <summary>
        /// To Get Deployment Reference Data
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize]
        [Route("api/DeploymentPlanReferenceData")]
        [HttpGet]
        public HttpResponseMessage GetReferenceData(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
                {
                    HttpResponseMessage response = null;
                    DeploymentPlanVO DeploymentplanReference = _DeploymentPlanService.GetDeploymentPlanReferenceVO();
                    response = request.CreateResponse(HttpStatusCode.OK, DeploymentplanReference);

                    return response;
                });
        }

         

        /// <summary>
        /// To Get Deployment Types
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize]
        [Route("api/DeploymentPlanTypes")]
        [HttpGet]
        public HttpResponseMessage GetDeploymentPlanTypes(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<SubCategoryVO> DeploymentPlanTypes = _DeploymentPlanService.GetDeploymentPlanTypes();
                response = request.CreateResponse<List<SubCategoryVO>>(HttpStatusCode.OK, DeploymentPlanTypes);
                return response;
            });
        }

        /// <summary>
        /// To Get Planned Deployment Details For Financial Year
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize]
        [Route("api/PlannedDeployments")]
        [HttpGet]
        public HttpResponseMessage GetPlannedDeploymentDetails(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<PlannedDeploymentVO> PlannedDeployments = _DeploymentPlanService.PlannedDeploymentDetails();
                response = request.CreateResponse<List<PlannedDeploymentVO>>(HttpStatusCode.OK, PlannedDeployments);
                return response;
            });
        }

        /// <summary>
        ///   To Get Craft Name Details
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize]
        [Route("api/DeploymentPlanCraft")]
        [HttpGet]
        public HttpResponseMessage GetDeploymentCraftNames(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<PlannedDeploymentVO> crafts = _DeploymentPlanService.GetDeploymentCraftNames();
                response = request.CreateResponse<List<PlannedDeploymentVO>>(HttpStatusCode.OK, crafts);
                return response;
            });
        }
        //added by chandrima---------------------
        ///// <summary>
        ///// To Get Financial Year Data
        ///// </summary>
        ///// <param name="request"></param>
        ///// <returns></returns>
        //[Authorize]
        //[Route("api/DeploymentPlan/GetFinancialYear")]
        //[HttpGet]
        //public HttpResponseMessage GetFinancialYear(HttpRequestMessage request)
        //{
        //    return GetHttpResponse(request, () =>
        //    {
        //        HttpResponseMessage response = null;
        //     List<FinancialYearVO> FinancialYearReference = _DeploymentPlanService.GetFinancialYear();
        //     response = request.CreateResponse<List<FinancialYearVO>>(HttpStatusCode.OK, FinancialYearReference);

        //        return response;
        //    });
        //}
        /// <summary>
        /// To Add Deployment Plan Data
        /// </summary>
        /// <param name="request"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [Authorize]
        [Route("api/DeploymentPlan")]
        [HttpPost]
        public HttpResponseMessage PostDeploymentPlan(HttpRequestMessage request, DeploymentPlanVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                DeploymentPlanVO deploymentCreated = _DeploymentPlanService.AddDeploymentPlan(value);
                response = request.CreateResponse<DeploymentPlanVO>(HttpStatusCode.Created, deploymentCreated);
                return response;
            });
        }

        /// <summary>
        /// To Modify Deployment Plan Data
        /// </summary>
        /// <param name="request"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [Authorize]
        [Route("api/DeploymentPlan")]
        [HttpPut]
        public HttpResponseMessage ModifyDeploymentPlan(HttpRequestMessage request, DeploymentPlanVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                DeploymentPlanVO deploymentCreated = _DeploymentPlanService.ModifyDeploymentPlan(value);
                response = request.CreateResponse<DeploymentPlanVO>(HttpStatusCode.Created, deploymentCreated);
                return response;
            });
        }
    }
}