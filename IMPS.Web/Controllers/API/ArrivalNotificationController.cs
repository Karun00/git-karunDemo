using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using IPMS.ServiceProxies.Clients;
using IPMS.ServiceProxies.Contracts;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;


namespace IPMS.Web.Api
{
    public class ArrivalNotificationController : ApiControllerBase
    {

        IArrivalNotificationService _arrivalnotificationservice;


        public ArrivalNotificationController()
        {
            _arrivalnotificationservice = new ArrivalNotificationClient();
        }        

        [HttpGet]
        public HttpResponseMessage GetVesselNamesAN(HttpRequestMessage request)
        {

            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                string serchcolumn = HttpContext.Current.Request.Params["columnName"];
                string searchvalue = HttpContext.Current.Request.Params["filter[filters][0][value]"];
                List<VesselVO> Vessels = _arrivalnotificationservice.GetVesselNamesAN(searchvalue, serchcolumn);
                response = request.CreateResponse<List<VesselVO>>(HttpStatusCode.OK, Vessels);
                return response;
            });
        }



        [HttpGet]
        public HttpResponseMessage GetAgentnames(HttpRequestMessage request)
        {

            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                string searchValue = HttpContext.Current.Request.Params["filter[filters][0][value]"];
                List<AgentVO> Agents = _arrivalnotificationservice.GetAgents(searchValue);
                response = request.CreateResponse<List<AgentVO>>(HttpStatusCode.OK, Agents);
                return response;
            });
        }




        [Route("api/ArrivalNotifications")]
        [HttpPost]
        public HttpResponseMessage PostArrivalNotificationData(HttpRequestMessage request, ArrivalNotificationVO arrivalldata)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                ArrivalNotificationVO add_arrivalNotification = _arrivalnotificationservice.AddArrivalNotification(arrivalldata);
                response = request.CreateResponse<ArrivalNotificationVO>(HttpStatusCode.Created, add_arrivalNotification);
                return response;
            });
        }

        [Route("api/DraftArrivalNotifications")]
        [HttpPost]
        public HttpResponseMessage PostDraftArrivalNotificationData(HttpRequestMessage request, ArrivalNotificationVO arrivalldata)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                ArrivalNotificationVO add_arrivalNotification = _arrivalnotificationservice.AddArrivalNotificationDraft(arrivalldata);
                response = request.CreateResponse<ArrivalNotificationVO>(HttpStatusCode.Created, add_arrivalNotification);
                return response;

            });
        }

        [Route("api/TimeRuleConfigData")]
        [HttpPost]
        public HttpResponseMessage PostTimeRuleConfigData(HttpRequestMessage request, ArrivalNotificationVO arrivalldata)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                string add_arrivalNotification = _arrivalnotificationservice.AddTimeRuleConfig(arrivalldata);
                response = request.CreateResponse<string>(HttpStatusCode.Created, add_arrivalNotification);
                return response;
            });
        }

        [Route("api/ArrivalDuplicateData")]
        [HttpPost]
        public HttpResponseMessage PostArrivalDuplicateValidation(HttpRequestMessage request, ArrivalNotificationVO arrivalldata)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                string add_arrivalNotification = _arrivalnotificationservice.ArrivalDuplicateValidation(arrivalldata);
                response = request.CreateResponse<string>(HttpStatusCode.Created, add_arrivalNotification);
                return response;
            });
        }

        //This method is used for Update the data.
        [Route("api/ArrivalNotifications")]
        [HttpPut]
        public HttpResponseMessage PutArrivalNotificationData(HttpRequestMessage request, ArrivalNotificationVO arrivalldata)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                ArrivalNotificationVO modify_arrivalNotification = _arrivalnotificationservice.ModifyArrivalNotification(arrivalldata);
                response = request.CreateResponse<ArrivalNotificationVO>(HttpStatusCode.Created, modify_arrivalNotification);
                return response;
            });
        }

        [Authorize]
        [Route("api/ArrivalNotificationssearchgrd/{frmdt}/{todt}/{vcnsearch}/{veselid}/{imdg}/{isps}/{imdgclear}/{ispsclear}/{phoclear}")]
        public HttpResponseMessage GetArrivalNotifications(HttpRequestMessage request, string frmdt, string todt, string vcnsearch, string veselid, string imdg, string isps, string imdgclear, string ispsclear, string phoclear)
        {
            return GetHttpResponse(request, () =>
            {               

                HttpResponseMessage response = null;

                List<ArrivalNotificationGridVO> arrivalnotifications = _arrivalnotificationservice.GetArrivalNotifications(frmdt, todt, vcnsearch, veselid, imdg, isps, imdgclear, ispsclear, phoclear);
                response = request.CreateResponse<List<ArrivalNotificationGridVO>>(HttpStatusCode.OK, arrivalnotifications);
                return response;

            });
        }        


        //This method is used for feacth the data.

        [Route("api/ArrivalNotificationss/{vcn}/{WorkflowInstanceId}")]
        [HttpGet]
        public HttpResponseMessage GetArrivalNotification(HttpRequestMessage request, string vcn, int WorkflowInstanceId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<ArrivalNotificationVO> arrivalnotifications = _arrivalnotificationservice.GetArrivalNotificationForWorkFlow(vcn, WorkflowInstanceId);
                response = request.CreateResponse<List<ArrivalNotificationVO>>(HttpStatusCode.OK, arrivalnotifications);
                return response;
            });
        }
       
        [HttpGet]
        public HttpResponseMessage GetArrivalNotificationNew(HttpRequestMessage request, string vcn)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<ArrivalNotificationVO> arrivalnotifications = _arrivalnotificationservice.GetArrivalNotification(vcn);
                response = request.CreateResponse<List<ArrivalNotificationVO>>(HttpStatusCode.OK, arrivalnotifications);
                return response;
            });
        }

        
        [HttpGet]
        public HttpResponseMessage GetNotificationStatus(HttpRequestMessage request, string vcn)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<ArrvWorkflowStatusVo> arrivalnotifications = _arrivalnotificationservice.GetNotificationStatus(vcn);
                response = request.CreateResponse<List<ArrvWorkflowStatusVo>>(HttpStatusCode.OK, arrivalnotifications);
                return response;
            });
        }

        [Route("api/ArrivalNotifications/{ETAFrom}/{ETATo}")]
        [HttpGet]
        public HttpResponseMessage GetArrivalNotificationSearch(HttpRequestMessage request, string etafrom, string etato)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<ArrivalNotificationVO> arrivalnotifications = _arrivalnotificationservice.GetArrivalNotificationSearch(etafrom, etato);
                response = request.CreateResponse<List<ArrivalNotificationVO>>(HttpStatusCode.OK, arrivalnotifications);
                return response;
            });
        }

        //This method is used for feacth the VesselDeetails for auto compleate.
        [Route("api/Vessels/{vslname}")]
        [HttpGet]
        public HttpResponseMessage GetVesselsForAutoComplete(HttpRequestMessage request, string vslname)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<VesselVO> arrivalnotifications = _arrivalnotificationservice.VesselDeetailsAutoComplete(vslname);
                response = request.CreateResponse<List<VesselVO>>(HttpStatusCode.OK, arrivalnotifications);
                return response;
            });
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize]
        [Route("api/LoadTOBirths")]
        [HttpGet]
        public HttpResponseMessage GetArrivalNotificationTOBirths(HttpRequestMessage request, string toid)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                ArrivalNotificationReferenceVO arrivalBirths = _arrivalnotificationservice.GetArrivalNotificationBirthReferenceVO(toid);
                response = request.CreateResponse<ArrivalNotificationReferenceVO>(HttpStatusCode.OK, arrivalBirths);
                return response;
            });
        }

        [Authorize]
        [Route("api/LoadDrafts")]
        [HttpGet]
        public HttpResponseMessage GetArrivalNotificationDrafts(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                ArrivalNotificationReferenceVO arrivalBirths = _arrivalnotificationservice.GetArrivalNotificationDraftReferenceVO();
                response = request.CreateResponse<ArrivalNotificationReferenceVO>(HttpStatusCode.OK, arrivalBirths);
                return response;
            });
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize]
        [Route("api/ArrivanNotificationReferenceData")]
        [HttpGet]
        public HttpResponseMessage GetArrivalNotificationReferenceData(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                ArrivalNotificationReferenceVO arrivalnotificationDetails = _arrivalnotificationservice.GetArrivalNotificationReferenceVO();
                response = request.CreateResponse(HttpStatusCode.OK, arrivalnotificationDetails);
                return response;
            });
        }
        

        #region Workflow Integrated Methods
        [Route("api/ArrivalNotifications/Approve")]
        [HttpPost]
        public HttpResponseMessage ApproveArrivalNotification(HttpRequestMessage request, PendingTaskVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                _arrivalnotificationservice.ApproveArrivalNotification(value.ReferenceID, value.Remarks, value.TaskCode);
                response = request.CreateResponse(HttpStatusCode.Created);
                return response;
            });
        }


        [Route("api/ArrivalNotifications/Isps/Approve")]
        [HttpPost]
        public HttpResponseMessage ApproveArrivalNotificationIsps(HttpRequestMessage request, PendingTaskVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                _arrivalnotificationservice.ApproveArrivalNotificationIsps(value.ReferenceID, value.Remarks, value.TaskCode);
                response = request.CreateResponse(HttpStatusCode.Created);
                return response;
            });
        }

        [Route("api/ArrivalNotifications/Imdg/Approve")]
        [HttpPost]
        public HttpResponseMessage ApproveArrivalNotificationImdg(HttpRequestMessage request, PendingTaskVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                _arrivalnotificationservice.ApproveArrivalNotificationImdg(value.ReferenceID, value.Remarks, value.TaskCode);
                response = request.CreateResponse(HttpStatusCode.Created);
                return response;
            });
        }

        [Route("api/ArrivalNotifications/RequestToResubmit")]
        [HttpPost]
        public HttpResponseMessage RequestToResubmitArrivalNotification(HttpRequestMessage request, PendingTaskVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                _arrivalnotificationservice.RequestToResubmitArrivalNotification(value.ReferenceID, value.Remarks, value.TaskCode);
                response = request.CreateResponse(HttpStatusCode.Created);
                return response;
            });
        }

        [Route("api/ArrivalNotifications/Ph/Approve")]
        [HttpPost]
        public HttpResponseMessage ApproveArrivalNotificationPH(HttpRequestMessage request, PendingTaskVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                _arrivalnotificationservice.ApproveArrivalNotificationPH(value.ReferenceID, value.Remarks, value.TaskCode);
                response = request.CreateResponse(HttpStatusCode.Created);
                return response;
            });
        }

        [Route("api/ArrivalNotifications/Ph/RequestToResubmit")]
        [HttpPost]
        public HttpResponseMessage RequestToResubmitArrivalNotificationPH(HttpRequestMessage request, PendingTaskVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                _arrivalnotificationservice.RequestToResubmitArrivalNotificationPH(value.ReferenceID, value.Remarks, value.TaskCode);
                response = request.CreateResponse(HttpStatusCode.Created);
                return response;
            });
        }

        [Route("api/ArrivalNotifications/Isps/RequestToResubmit")]
        [HttpPost]
        public HttpResponseMessage RequestToResubmitArrivalNotificationIsps(HttpRequestMessage request, PendingTaskVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                _arrivalnotificationservice.RequestToResubmitArrivalNotificationIsps(value.ReferenceID, value.Remarks, value.TaskCode);
                response = request.CreateResponse(HttpStatusCode.Created);
                return response;
            });
        }

        [Route("api/ArrivalNotifications/Imdg/RequestToResubmit")]
        [HttpPost]
        public HttpResponseMessage RequestToResubmitArrivalNotificationImdg(HttpRequestMessage request, PendingTaskVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                _arrivalnotificationservice.RequestToResubmitArrivalNotificationImdg(value.ReferenceID, value.Remarks, value.TaskCode);
                response = request.CreateResponse(HttpStatusCode.Created);
                return response;
            });
        }

        #endregion


        [Authorize]   
        [HttpGet]
        public HttpResponseMessage GetArrivalCommoditiesByVcn(HttpRequestMessage request, string vcn)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<ArrivalCommodityVo> arrivalcommodities = _arrivalnotificationservice.GetArrivalCommoditiesByVcn(vcn);
                response = request.CreateResponse<List<ArrivalCommodityVo>>(HttpStatusCode.OK, arrivalcommodities);
                return response;
            });
        }

        [Authorize]
        [Route("api/ArrivalNotificationByVCN")]
        [HttpGet]
        public HttpResponseMessage GetArrivalNotificationByVcn(HttpRequestMessage request, string vcn)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (vcn != null)
                {
                    ArrivalNotificationVO arrivalcommodities = _arrivalnotificationservice.GetArrivalNotificationVO(vcn);
                    response = request.CreateResponse<ArrivalNotificationVO>(HttpStatusCode.OK, arrivalcommodities);
                }
                return response;
            });
        }
      
        [Route("api/ArrivalVcnDetailsforAutocomplete")]
        [HttpGet]
        public HttpResponseMessage GetArrivalVcnDetailsforAutocomplete(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                string searchValue = HttpContext.Current.Request.Params["filter[filters][0][value]"];
                List<RevenuePostingVO> arrivalcommodities = _arrivalnotificationservice.GetArrivalVcnDetailsForAutocomplete(searchValue);
                response = request.CreateResponse<List<RevenuePostingVO>>(HttpStatusCode.OK, arrivalcommodities);
                return response;
            });
        }

       
        [HttpPost]
        public HttpResponseMessage Cancel(HttpRequestMessage request, ArrivalNotificationVO arrivalnotificationdata)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                ArrivalNotificationVO add_arrivalNotification = _arrivalnotificationservice.CancelArrivalNotification(arrivalnotificationdata);
                Controllers.ChatHubs.ChatHub bHub = new Controllers.ChatHubs.ChatHub();
                bHub.Show();
                response = request.CreateResponse<ArrivalNotificationVO>(HttpStatusCode.Created, add_arrivalNotification);
                return response;
            });
        }

        [Route("api/ArrivalNotifications/Reject")]
        public HttpResponseMessage RejectArrivalNotification(HttpRequestMessage request, PendingTaskVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                _arrivalnotificationservice.RejectArrivalNotification(value.ReferenceID, value.Remarks, value.TaskCode);
                Controllers.ChatHubs.ChatHub bHub = new Controllers.ChatHubs.ChatHub();
                bHub.Show();
                response = request.CreateResponse(HttpStatusCode.Created);
                return response;
            });
        }

        [Route("api/ArrivalNotifications/Isps/Reject")]
        public HttpResponseMessage RejectArrivalNotificationByIsps(HttpRequestMessage request, PendingTaskVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                _arrivalnotificationservice.RejectArrivalNotificationByIsps(value.ReferenceID, value.Remarks, value.TaskCode);
                response = request.CreateResponse(HttpStatusCode.Created);
                return response;
            });
        }

        [Route("api/ArrivalNotifications/Ph/Reject")]
        public HttpResponseMessage RejectArrivalNotificationByPhc(HttpRequestMessage request, PendingTaskVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                _arrivalnotificationservice.RejectArrivalNotificationByPhc(value.ReferenceID, value.Remarks, value.TaskCode);
                response = request.CreateResponse(HttpStatusCode.Created);
                return response;
            });
        }

        [Route("api/ArrivalNotifications/Imdg/Reject")]
        public HttpResponseMessage RejectArrivalNotificationByImdg(HttpRequestMessage request, PendingTaskVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                _arrivalnotificationservice.RejectArrivalNotificationByImdg(value.ReferenceID, value.Remarks, value.TaskCode);
                response = request.CreateResponse(HttpStatusCode.Created);
                return response;
            });
        }

        [Route("api/ArrivalNotifications/dhm/Approve")]
        [HttpPost]
        public HttpResponseMessage ApproveArrivalNotificationDhm(HttpRequestMessage request, PendingTaskVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                _arrivalnotificationservice.ApproveArrivalNotificationDhm(value.ReferenceID, value.Remarks, value.TaskCode);
                response = request.CreateResponse(HttpStatusCode.Created);
                return response;
            });
        }

        [Route("api/ArrivalNotifications/dhm/Reject")]
        public HttpResponseMessage RejectArrivalNotificationByDhm(HttpRequestMessage request, PendingTaskVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                _arrivalnotificationservice.RejectArrivalNotificationByDhm(value.ReferenceID, value.Remarks, value.TaskCode);
                response = request.CreateResponse(HttpStatusCode.Created);
                return response;
            });
        }


        [Route("api/ArrivalNotifications/dhm/RequestToResubmit")]
        [HttpPost]
        public HttpResponseMessage RequestToResubmitArrivalNotificationDhm(HttpRequestMessage request, PendingTaskVO value)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                _arrivalnotificationservice.RequestToResubmitArrivalNotificationDhm(value.ReferenceID, value.Remarks, value.TaskCode);
                response = request.CreateResponse(HttpStatusCode.Created);
                return response;
            });
        }


        [HttpGet]
        public HttpResponseMessage ArrivalBerthingRules1(HttpRequestMessage request, string arrdraft, string preferedberthkey)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                string arrivalBerthing = _arrivalnotificationservice.ArrivalBerthingRules1(arrdraft, preferedberthkey);
                response = request.CreateResponse<string>(HttpStatusCode.OK, arrivalBerthing);
                return response;
            });
        }

        [HttpGet]
        public HttpResponseMessage ArrivalBerthingRules2(HttpRequestMessage request, string arrdraft, string preferedberthkey, string cargotype)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                string arrivalBerthing = _arrivalnotificationservice.ArrivalBerthingRules2(arrdraft, preferedberthkey, cargotype);
                response = request.CreateResponse<string>(HttpStatusCode.OK, arrivalBerthing);
                return response;
            });
        }

        [Route("api/ArrivalNotificationVoyageValidation/{vesselid}/{voyagein}/{voyageout}")]
        [HttpGet]
        public HttpResponseMessage ArrivalNotificationVoyageValidation(HttpRequestMessage request, int vesselid, string voyagein, string voyageout)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                string ArrivalNotificationVoyage = _arrivalnotificationservice.ArrivalNotificationVoyageValidation(vesselid, voyagein, voyageout);
                response = request.CreateResponse<string>(HttpStatusCode.OK, ArrivalNotificationVoyage);
                return response;

            });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _arrivalnotificationservice.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}