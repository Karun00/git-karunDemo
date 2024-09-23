using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using IPMS.ServiceProxies.Clients;
using IPMS.ServiceProxies.Contracts;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace IPMS.Web.Api
{
    public class DockingPlanController : ApiControllerBase
    {
        IDockingPlanService  _DockingPlanService;
        public DockingPlanController()
        {
            _DockingPlanService = new DockingPlanClient();
        }

        /// <summary>
        ///  To Get Docking Plan Details
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize]
        [Route("api/DockingPlan")]
        [HttpGet]
        public HttpResponseMessage GetDockingPlanDetails(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<DockingPlanVO> dockingplans = _DockingPlanService.DockingPlanDetails();
                response = request.CreateResponse<List<DockingPlanVO>>(HttpStatusCode.OK, dockingplans);
                return response;
            });
        }

        /// <summary>
        ///   To Get Vessel Details
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize]
        [Route("api/DockingPlanVessel")]
        [HttpGet]
        public HttpResponseMessage GetCraftNames(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {

                HttpResponseMessage response = null;
                string searchColumn = HttpContext.Current.Request.Params["columnName"];
                string searchValue = HttpContext.Current.Request.Params["filter[filters][0][value]"];
                List<DockingPlanVO> vessels = _DockingPlanService.GetVesselNames(searchValue, searchColumn);
                response = request.CreateResponse<List<DockingPlanVO>>(HttpStatusCode.OK, vessels);
                return response;

                //HttpResponseMessage response = null;
                //List<DockingPlanVO> vessels = _DockingPlanService.GetVesselNames();
                //response = request.CreateResponse<List<DockingPlanVO>>(HttpStatusCode.OK, vessels);
                //return response;
            });
        }

        /// <summary>
        ///  To Get Vessel Details By VesselID
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize]
        [Route("api/DockingPlanVesselInfo/{VesselID}")]
        [HttpGet]
        public HttpResponseMessage GetVesselsByID(HttpRequestMessage request, int VesselID)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                DockingPlanVO vesselinfo = _DockingPlanService.GetVesselsById(VesselID);
                response = request.CreateResponse<DockingPlanVO>(HttpStatusCode.OK, vesselinfo);
                return response;
            });
        }

        /// <summary>
        /// To Add DockingPlan Data
        /// </summary>
        /// <param name="request"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [Authorize]
        [Route("api/DockingPlan")]
        [HttpPost]
        public HttpResponseMessage PostDockingPlanData(HttpRequestMessage request, DockingPlanVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                DockingPlanVO dockplanCreated = _DockingPlanService.AddDockingPlan(value);
                response = request.CreateResponse<DockingPlanVO>(HttpStatusCode.Created, dockplanCreated);
                return response;
            });
        }


        /// <summary>
        /// To Modify DockingPlan Data
        /// </summary>
        /// <param name="request"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [Authorize]
        [Route("api/DockingPlan")]
        [HttpPut]
        public HttpResponseMessage ModifyDockingPlanData(HttpRequestMessage request, DockingPlanVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                DockingPlanVO dockplanCreated = _DockingPlanService.ModifyDockingPlan(value);
                response = request.CreateResponse<DockingPlanVO>(HttpStatusCode.Created, dockplanCreated);
                return response;
            });
        }

        /// <summary>
        /// To Docuemtns types 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize]
         [Route("api/DocumentTypes")]
        [HttpGet]
        public HttpResponseMessage GetDocumentTypes(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<SubCategory> subCategoryDocTypeDetails = _DockingPlanService.GetDocumentTypes();
                response = request.CreateResponse(HttpStatusCode.OK, subCategoryDocTypeDetails);
                return response;
            });
        }


        #region Workflow Integrated Methods
        /// <summary>
        /// To Approve DockingPlan
        /// </summary>
        /// <param name="request"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [Route("api/DockingPlan/Approve")]
        [HttpPost]
        public HttpResponseMessage ApproveDockingPlan(HttpRequestMessage request, PendingTaskVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                _DockingPlanService.ApproveDockingPlan(value.ReferenceID, value.Remarks, value.TaskCode);

                response = request.CreateResponse(HttpStatusCode.Created);
                return response;
            });
        }
        //Add by Srinivas
        /// <summary>
        /// To Cancel Grid DockingPlan
        /// </summary>
        /// <param name="request"></param>
        /// <param name="value"></param>
        /// <returns></returns>

        [Authorize]
        [Route("api/DockingPlan/GridCancel")]
        [HttpPost]
        public HttpResponseMessage Cancel(HttpRequestMessage request, DockingPlanVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                //value.CreatedBy=DateTime.Now;
                //value.ModifiedDate = DateTime.Now;

                DockingPlanVO servicerequestCreated = _DockingPlanService.Cancel(value);
                response = request.CreateResponse<DockingPlanVO>(HttpStatusCode.Created, servicerequestCreated);


                return response;
            });
        }


        /// <summary>
        /// To Reject DockingPlan
        /// </summary>
        /// <param name="request"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [Route("api/DockingPlan/Reject")]
        [HttpPost]
        public HttpResponseMessage RejectDockingPlan(HttpRequestMessage request, PendingTaskVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                _DockingPlanService.RejectDockingPlan(value.ReferenceID, value.Remarks, value.TaskCode);
                response = request.CreateResponse(HttpStatusCode.Created);
                return response;
            });
        }

        /// <summary>
        ///  To get Docking Plan based on dockingplanid
        /// </summary>
        /// <param name="request"></param>
        /// <param name="dockingplanid"></param>
        /// <returns></returns>
        [Authorize]
        [Route("api/DockingPlan/{dockingplanid}")]
        [HttpGet]
        public HttpResponseMessage GetDockingPlan(HttpRequestMessage request, int dockingplanid)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<DockingPlanVO> dockingplantype = _DockingPlanService.GetDockingPlan(dockingplanid);
                response = request.CreateResponse<List<DockingPlanVO>>(HttpStatusCode.OK, dockingplantype);
                return response;
            });
        }

        #endregion


    }
}