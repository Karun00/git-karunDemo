using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using IPMS.ServiceProxies.Contracts;
using IPMS.Web.ServiceProxies;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace IPMS.ServiceProxies.Clients
{
    public class ArrivalNotificationClient : UserClientBase<IArrivalNotificationService>, IArrivalNotificationService
    {

        public ArrivalNotificationVO AddArrivalNotification(ArrivalNotificationVO arrivalnotificationdata)
        {
            return WrapOperationWithException(() => Channel.AddArrivalNotification(arrivalnotificationdata));
        }

        public ArrivalNotificationVO AddArrivalNotificationDraft(ArrivalNotificationVO arrivalnotificationdata)
        {
            return WrapOperationWithException(() => Channel.AddArrivalNotificationDraft(arrivalnotificationdata));
        }
        public string AddTimeRuleConfig(ArrivalNotificationVO arrivalnotificationdata)
        {
            return WrapOperationWithException(() => Channel.AddTimeRuleConfig(arrivalnotificationdata));
        }
        public string ArrivalDuplicateValidation(ArrivalNotificationVO arrivalnotificationdata)
        {
            return WrapOperationWithException(() => Channel.ArrivalDuplicateValidation(arrivalnotificationdata));
        }   
        public ArrivalNotificationVO ModifyArrivalNotification(ArrivalNotificationVO arrivalnotificationdata)
        {
            return WrapOperationWithException(() => Channel.ModifyArrivalNotification(arrivalnotificationdata));
        }
        public Agent GetAgentDetails(int agentid)
        {
            return WrapOperationWithException(() => Channel.GetAgentDetails(agentid));
        }
        public Vessel GetVesselDetails(int vesselid)
        {
            return WrapOperationWithException(() => Channel.GetVesselDetails(vesselid));
        }
        public Pilot GetPilotDetails(int pilotid)
        {
            return WrapOperationWithException(() => Channel.GetPilotDetails(pilotid));
        }
        public ArrivalNotificationReferenceVO GetArrivalNotificationReferenceVO()
        {
            return WrapOperationWithException(() => Channel.GetArrivalNotificationReferenceVO());
        }
        public List<ArrivalNotificationGridVO> GetArrivalNotifications(string frmdt, string todt, string vcn, string veselid, string imdg, string isps, string imdgClear, string ispsClear, string phoClear)
        {
            return WrapOperationWithException(() => Channel.GetArrivalNotifications(frmdt, todt, vcn, veselid, imdg, isps,imdgClear, ispsClear, phoClear));
        }
        public List<ArrivalNotificationVO> GetArrivalNotification(string vcn)
        {
            return WrapOperationWithException(() => Channel.GetArrivalNotification(vcn));
        }
        public List<ArrivalNotificationVO> GetArrivalNotificationSearch(string etaFrom, string etaTo)
        {
            return WrapOperationWithException(() => Channel.GetArrivalNotificationSearch(etaFrom, etaTo));
        }
        public List<VesselVO> VesselDeetailsAutoComplete(string vslname)
        {
            return WrapOperationWithException(() => Channel.VesselDeetailsAutoComplete(vslname));
        }
        public void ApproveArrivalNotification(string vcn, string remarks, string taskcode)
        {
            WrapOperationWithException(() => Channel.ApproveArrivalNotification(vcn, remarks, taskcode));
        }
        public void ApproveArrivalNotificationIsps(string vcn, string remarks, string taskcode)
        {
            WrapOperationWithException(() => Channel.ApproveArrivalNotificationIsps(vcn, remarks, taskcode));
        }
        public void ApproveArrivalNotificationImdg(string vcn, string remarks, string taskcode)
        {
            WrapOperationWithException(() => Channel.ApproveArrivalNotificationImdg(vcn, remarks, taskcode));
        }
        public void RequestToResubmitArrivalNotification(string vcn, string remarks, string taskcode)
        {
            WrapOperationWithException(() => Channel.RequestToResubmitArrivalNotification(vcn, remarks, taskcode));
        }
        public void RequestToResubmitArrivalNotificationIsps(string vcn, string remarks, string taskcode)
        {
            WrapOperationWithException(() => Channel.RequestToResubmitArrivalNotificationIsps(vcn, remarks, taskcode));
        }
        public void RequestToResubmitArrivalNotificationImdg(string vcn, string remarks, string taskcode)
        {
            WrapOperationWithException(() => Channel.RequestToResubmitArrivalNotificationImdg(vcn, remarks, taskcode));
        }
        public void RequestToResubmitArrivalNotificationDhm(string vcn, string remarks, string taskcode)
        {
            WrapOperationWithException(() => Channel.RequestToResubmitArrivalNotificationDhm(vcn, remarks, taskcode));
        }
        public void ApproveArrivalNotificationPH(string vcn, string remarks, string taskcode)
        {
            WrapOperationWithException(() => Channel.ApproveArrivalNotificationPH(vcn, remarks, taskcode));
        }
        public void RequestToResubmitArrivalNotificationPH(string vcn, string remarks, string taskcode)
        {
            WrapOperationWithException(() => Channel.RequestToResubmitArrivalNotificationPH(vcn, remarks, taskcode));
        }
        public void ApproveArrivalNotificationDhm(string vcn, string remarks, string taskcode)
        {
            WrapOperationWithException(() => Channel.ApproveArrivalNotificationDhm(vcn, remarks, taskcode));
        }     

        // -- Added by sandeep on 27-8-2014

        public List<ArrivalCommodityVo> GetArrivalCommoditiesByVcn(string vcn)
        {
            return WrapOperationWithException(() => Channel.GetArrivalCommoditiesByVcn(vcn));
        }

        public ArrivalNotificationVO GetArrivalNotificationVO(string vcn)
        {
            return WrapOperationWithException(() => Channel.GetArrivalNotificationVO(vcn));
        }
        public ArrivalNotificationReferenceVO GetArrivalNotificationBirthReferenceVO(string toid)
        {
            return WrapOperationWithException(() => Channel.GetArrivalNotificationBirthReferenceVO(toid));
        }

        public ArrivalNotificationReferenceVO GetArrivalNotificationDraftReferenceVO()
        {
            return WrapOperationWithException(() => Channel.GetArrivalNotificationDraftReferenceVO());
        }
        
         
        // -- end

        public ArrivalNotificationVO CancelArrivalNotification(ArrivalNotificationVO arrivalnotificationdata)
        {
            return WrapOperationWithException(() => Channel.CancelArrivalNotification(arrivalnotificationdata));
        }

        ///mahesh
        public List<VesselVO> GetVesselNamesAN(string searchvalue, string serchcolumn)
        {
            return WrapOperationWithException(() => Channel.GetVesselNamesAN(searchvalue, serchcolumn));
        }

        public void RejectArrivalNotification(string vcn, string remarks, string taskcode)
        {
            WrapOperationWithException(() => Channel.RejectArrivalNotification(vcn, remarks, taskcode));
        }

        public void RejectArrivalNotificationByPhc(string vcn, string remarks, string taskcode)
        {
            WrapOperationWithException(() => Channel.RejectArrivalNotificationByPhc(vcn, remarks, taskcode));
        }

        public void RejectArrivalNotificationByIsps(string vcn, string remarks, string taskcode)
        {
            WrapOperationWithException(() => Channel.RejectArrivalNotificationByIsps(vcn, remarks, taskcode));
        }

        public void RejectArrivalNotificationByImdg(string vcn, string remarks, string taskcode)
        {
            WrapOperationWithException(() => Channel.RejectArrivalNotificationByImdg(vcn, remarks, taskcode));
        }

        public void RejectArrivalNotificationByDhm(string vcn, string remarks, string taskcode)
        {
            WrapOperationWithException(() => Channel.RejectArrivalNotificationByDhm(vcn, remarks, taskcode));
        }

        public string ArrivalBerthingRules1(string arrdraft, string preferedberthkey)
        {
            return WrapOperationWithException(() => Channel.ArrivalBerthingRules1(arrdraft, preferedberthkey));
        }

        public string ArrivalBerthingRules2(string arrdraft, string preferedberthkey, string cargotype)
        {
            return WrapOperationWithException(() => Channel.ArrivalBerthingRules2(arrdraft, preferedberthkey, cargotype));
        }

        public List<RevenuePostingVO> GetArrivalVcnDetailsForAutocomplete(string searchvalue)
        {
            return WrapOperationWithException(() => Channel.GetArrivalVcnDetailsForAutocomplete(searchvalue));
        }



        public List<ArrivalNotificationVO> GetArrivalNotificationForWorkFlow(string vcn, int workflowinstanceid)
        {
            return WrapOperationWithException(() => Channel.GetArrivalNotificationForWorkFlow(vcn, workflowinstanceid));
        }       

        public List<ArrvWorkflowStatusVo> GetNotificationStatus(string vcn)
        { return WrapOperationWithException(() => Channel.GetNotificationStatus(vcn)); }

         public List<AgentVO> GetAgents(string searchvalue)
        { return WrapOperationWithException(() => Channel.GetAgents(searchvalue)); }

        
         public string ArrivalNotificationVoyageValidation(int vesselid, string voyagein, string voyageout)
         {
             return WrapOperationWithException(() => Channel.ArrivalNotificationVoyageValidation(vesselid, voyagein, voyageout));
         }      

    }
}