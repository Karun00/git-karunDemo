using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;  

namespace IPMS.ServiceProxies.Contracts
{
    [ServiceContract]
    public interface IArrivalNotificationService : IDisposable
    {

        /// <summary>
        /// To Add ArrivalNotification Data
        /// </summary>
        /// <param name="arrivalnotificationdata"></param>
        /// <returns></returns>
        [OperationContract]
        ArrivalNotificationVO AddArrivalNotification(ArrivalNotificationVO arrivalnotificationdata);

        /// <summary>
        /// To Add ArrivalNotification Data
        /// </summary>
        /// <param name="arrivalnotificationdata"></param>
        /// <returns></returns>
        [OperationContract]
        ArrivalNotificationVO AddArrivalNotificationDraft(ArrivalNotificationVO arrivalnotificationdata);

        [OperationContract]
       string AddTimeRuleConfig(ArrivalNotificationVO arrivalnotificationdata);

        [OperationContract]
        string ArrivalDuplicateValidation(ArrivalNotificationVO arrivalnotificationdata);

        /// <summary>
        /// To Modify ArrivalNotification Data
        /// </summary>
        /// <param name="arrivalnotificationdata"></param>
        /// <returns></returns>
        [OperationContract]
        ArrivalNotificationVO ModifyArrivalNotification(ArrivalNotificationVO arrivalnotificationdata);

        /// <summary>
        /// To get Agent Details
        /// </summary>
        /// <param name="AgentID"></param>
        /// <returns></returns>
        [OperationContract]        
        Agent GetAgentDetails(int agentid);

        /// <summary>
        /// To Get vesel Details
        /// </summary>
        /// <param name="VesselID"></param>
        /// <returns></returns>
        [OperationContract]       
        Vessel GetVesselDetails(int vesselid);

        /// <summary>
        /// To Get Pilot Details
        /// </summary>
        /// <param name="PilotID"></param>
        /// <returns></returns>
        [OperationContract]      
        Pilot GetPilotDetails(int pilotid);

        /// <summary>
        /// To Get ArrivalNotification Reference data While initialization
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        ArrivalNotificationReferenceVO GetArrivalNotificationReferenceVO();

        /// <summary>
        /// To Get ArrivalNotification List Data
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<ArrivalNotificationGridVO> GetArrivalNotifications(string frmdt, string todt, string vcn, string veselid, string imdg, string isps, string imdgClear, string ispsClear, string phoClear);

        /// <summary>
        /// To Get Vesel Details
        /// </summary>
        /// <param name="vslname"></param>
        /// <returns></returns>
        [OperationContract]
        List<VesselVO> VesselDeetailsAutoComplete(string vslname);

        /// <summary>
        /// To get ArrivalNotification Details Based on VCN
        /// </summary>
        /// <param name="vcn"></param>
        /// <returns></returns>
        [OperationContract]
        List<ArrivalNotificationVO> GetArrivalNotification(string vcn);
        /// <summary>
        /// To get ArrivalNotification Details Based on ETA
        /// </summary>
        /// <param name="vcn"></param>
        /// <returns></returns>
        [OperationContract]
        List<ArrivalNotificationVO> GetArrivalNotificationSearch(string etaFrom, string etaTo);
        
        /// <summary>
        /// To Approve Arrival Notification Request
        /// </summary>
        /// <param name="VCN"></param>
        /// <param name="remarks"></param>
        /// <param name="taskcode"></param>
        [OperationContract]
        void ApproveArrivalNotification(string vcn, string remarks, string taskcode);

        /// <summary>
        /// Arrival Notification Request ISPS
        /// </summary>
        /// <param name="VCN"></param>
        /// <param name="remarks"></param>
        /// <param name="taskcode"></param>
        [OperationContract]
        void ApproveArrivalNotificationIsps(string vcn, string remarks, string taskcode);

        /// <summary>
        /// Arrival Notification Request IMDG
        /// </summary>
        /// <param name="VCN"></param>
        /// <param name="remarks"></param>
        /// <param name="taskcode"></param>
        [OperationContract]
        void ApproveArrivalNotificationImdg(string vcn, string remarks, string taskcode);

        /// <summary>
        /// Arrival Notification Request DHM
        /// </summary>
        /// <param name="VCN"></param>
        /// <param name="remarks"></param>
        /// <param name="taskcode"></param>
        [OperationContract]
        void ApproveArrivalNotificationDhm(string vcn, string remarks, string taskcode);

        /// <summary>
        /// To Update Workflow for Arrival Notification Request for Resubmition
        /// </summary>
        /// <param name="VCN"></param>
        /// <param name="remarks"></param>
        /// <param name="taskcode"></param>
        [OperationContract]
        void RequestToResubmitArrivalNotification(string vcn, string remarks, string taskcode);

        /// <summary>
        /// To Update Workflow for Arrival Notification Request for Resubmition ISPS
        /// </summary>
        /// <param name="VCN"></param>
        /// <param name="remarks"></param>
        /// <param name="taskcode"></param> 
        [OperationContract]
        void RequestToResubmitArrivalNotificationIsps(string vcn, string remarks, string taskcode);

        /// <summary>
        /// To Update Workflow for Arrival Notification Request for Resubmition IMDG
        /// </summary>
        /// <param name="VCN"></param>
        /// <param name="remarks"></param>
        /// <param name="taskcode"></param>
        [OperationContract]
        void RequestToResubmitArrivalNotificationImdg(string vcn, string remarks, string taskcode);

        /// <summary>
        /// To Update Workflow for Arrival Notification PH
        /// </summary>
        /// <param name="VCN"></param>
        /// <param name="remarks"></param>
        /// <param name="taskcode"></param>
        [OperationContract]
        void ApproveArrivalNotificationPH(string vcn, string remarks, string taskcode);

        /// <summary>
        /// To Update Workflow for Arrival Notification Request for Resubmition DHM
        /// </summary>
        /// <param name="VCN"></param>
        /// <param name="remarks"></param>
        /// <param name="taskcode"></param> 
        [OperationContract]
        void RequestToResubmitArrivalNotificationDhm(string vcn, string remarks, string taskcode);

        /// <summary>
        /// To Update Workflow for Arrival Notification Request for ResubmitionPH
        /// </summary>
        /// <param name="VCN"></param>
        /// <param name="remarks"></param>
        /// <param name="taskcode"></param>
         [OperationContract]
        void RequestToResubmitArrivalNotificationPH(string vcn, string remarks, string taskcode);

         [OperationContract]
         void RejectArrivalNotification(string vcn, string remarks, string taskcode);

         [OperationContract]
         void RejectArrivalNotificationByImdg(string vcn, string remarks, string taskcode);

         [OperationContract]
         void RejectArrivalNotificationByIsps(string vcn, string remarks, string taskcode);

         [OperationContract]
         void RejectArrivalNotificationByPhc(string vcn, string remarks, string taskcode);

         [OperationContract]
         void RejectArrivalNotificationByDhm(string vcn, string remarks, string taskcode);
        

        /// <summary>
        /// Author   : Sandeep Appana
        /// Date     : 27-8-2014
        /// Purpose  : To Get Arrival Commodity record(s) based on vcn
        /// </summary>
        /// <param name="vcn"></param>
        /// <returns></returns>
        [OperationContract]
         List<ArrivalCommodityVo> GetArrivalCommoditiesByVcn(string vcn);


        /// <summary>
        /// Author   : Omprakash Kotha
        /// Date     : 13-11-2014
        /// Purpose  : To Get Arrival Notification record(s) based on vcn
        /// </summary>
        /// <param name="vcn"></param>
        /// <returns></returns>
        [OperationContract]
        ArrivalNotificationVO GetArrivalNotificationVO(string vcn);

        /// <summary>
        /// Author   : Omprakash Kotha
        /// Date     : 13-11-2014
        /// Purpose  : To Get Arrival Notification record(s) based on vcn
        /// </summary>
        /// <param name="vcn"></param>
        /// <returns></returns>
        [OperationContract]
        ArrivalNotificationReferenceVO GetArrivalNotificationBirthReferenceVO(string toid);

        [OperationContract]
        ArrivalNotificationReferenceVO GetArrivalNotificationDraftReferenceVO();

        [OperationContract]
        ArrivalNotificationVO CancelArrivalNotification(ArrivalNotificationVO arrivalnotificationdata);

        /// <summary>
        /// //mahesh
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        [OperationContract]
        List<VesselVO> GetVesselNamesAN(string searchvalue, string serchcolumn);

        [OperationContract]
        string ArrivalBerthingRules1(string arrdraft, string preferedberthkey);

        [OperationContract]
        string ArrivalBerthingRules2(string arrdraft, string preferedberthkey, string cargotype);


        [OperationContract]
        List<RevenuePostingVO> GetArrivalVcnDetailsForAutocomplete(string searchvalue);


        [OperationContract]
        List<ArrivalNotificationVO> GetArrivalNotificationForWorkFlow(string vcn, int workflowinstanceid);     

        [OperationContract]
        List<ArrvWorkflowStatusVo> GetNotificationStatus(string vcn);
    
          [OperationContract]
        List<AgentVO> GetAgents(string searchvalue);     

        [OperationContract]
        string ArrivalNotificationVoyageValidation(int vesselid,string voyagein,string voyageout);
    }
}
