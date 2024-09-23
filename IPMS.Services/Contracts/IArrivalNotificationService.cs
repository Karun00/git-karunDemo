using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ServiceModel;

 
namespace IPMS.Services
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
        [FaultContract(typeof(Exception))]
        ArrivalNotificationVO AddArrivalNotification(ArrivalNotificationVO arrivalnotificationdata);

        /// <summary>
        /// To Add ArrivalNotification Data
        /// </summary>
        /// <param name="arrivalnotificationdata"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        ArrivalNotificationVO AddArrivalNotificationDraft(ArrivalNotificationVO arrivalnotificationdata);


        [OperationContract]
        [FaultContract(typeof(Exception))]
        string AddTimeRuleConfig(ArrivalNotificationVO arrivalnotificationdata);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        string ArrivalDuplicateValidation(ArrivalNotificationVO arrivalnotificationdata);

        /// <summary>
        /// To Modify ArrivalNotification Data
        /// </summary>
        /// <param name="arrivalnotificationdata"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        ArrivalNotificationVO ModifyArrivalNotification(ArrivalNotificationVO arrivalnotificationdata);

        /// <summary>
        /// To get Agent Details
        /// </summary>
        /// <param name="AgentID"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        Agent GetAgentDetails(int agentid);


        /// <summary>
        /// To Get vesel Details
        /// </summary>
        /// <param name="VesselID"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        Vessel GetVesselDetails(int vesselid);

        /// <summary>
        /// To Get Pilot Details
        /// </summary>
        /// <param name="PilotID"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        Pilot GetPilotDetails(int pilotid);

        /// <summary>
        /// To Get ArrivalNotification Reference data While initialization
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        ArrivalNotificationReferenceVO GetArrivalNotificationReferenceVO();

        /// <summary>
        /// To Get ArrivalNotification List Data
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        IList<ArrivalNotificationGridVO> GetArrivalNotifications(string frmdt, string todt, string vcn, string veselid, string imdg, string isps, string imdgClear, string ispsClear, string phoClear);


        /// <summary>
        /// To Get ArrivalNotification List Data
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        IList<ArrivalNotificationVO> GetArrivalNotificationSearch(string etaFrom, string etaTo);


        /// <summary>
        /// To get ArrivalNotification Details Based on VCN
        /// </summary>
        /// <param name="vcn"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        IList<ArrivalNotificationVO> GetArrivalNotification(string vcn);

                /// <summary>
        /// To get ArrivalNotification notificationStatus Based on VCN
        /// </summary>
        /// <param name="vcn"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<ArrvWorkflowStatusVo> GetNotificationStatus(string vcn);
        /// <summary>
        /// To Approve Arrival Notification Request
        /// </summary>
        /// <param name="VCN"></param>
        /// <param name="remarks"></param>
        /// <param name="taskcode"></param>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        void ApproveArrivalNotification(string vcn, string remarks, string taskcode);

        /// <summary>
        /// To Update Workflow for Arrival Notification Request for Resubmition
        /// </summary>
        /// <param name="VCN"></param>
        /// <param name="remarks"></param>
        /// <param name="taskcode"></param>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        void RequestToResubmitArrivalNotification(string vcn, string remarks, string taskcode);

        /// <summary>
        /// Arrival Notification Request ISPS
        /// </summary>
        /// <param name="VCN"></param>
        /// <param name="remarks"></param>
        /// <param name="taskcode"></param>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        void ApproveArrivalNotificationIsps(string vcn, string remarks, string taskcode);

        /// <summary>
        /// To Update Workflow for Arrival Notification Request for Resubmition ISPS
        /// </summary>
        /// <param name="VCN"></param>
        /// <param name="remarks"></param>
        /// <param name="taskcode"></param> 
        [OperationContract]
        [FaultContract(typeof(Exception))]
        void RequestToResubmitArrivalNotificationIsps(string vcn, string remarks, string taskcode);

        /// <summary>
        /// Arrival Notification Request IMDG
        /// </summary>
        /// <param name="VCN"></param>
        /// <param name="remarks"></param>
        /// <param name="taskcode"></param>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        void ApproveArrivalNotificationImdg(string vcn, string remarks, string taskcode);       

        /// <summary>
        /// To Update Workflow for Arrival Notification Request for Resubmition IMDG
        /// </summary>
        /// <param name="VCN"></param>
        /// <param name="remarks"></param>
        /// <param name="taskcode"></param>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        void RequestToResubmitArrivalNotificationImdg(string vcn, string remarks, string taskcode);

        /// <summary>
        /// To Update Workflow for Arrival Notification PH
        /// </summary>
        /// <param name="VCN"></param>
        /// <param name="remarks"></param>
        /// <param name="taskcode"></param>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        void ApproveArrivalNotificationPH(string vcn, string remarks, string taskcode);

        /// <summary>
        /// To Update Workflow for Arrival Notification Request for ResubmitionPH
        /// </summary>
        /// <param name="VCN"></param>
        /// <param name="remarks"></param>
        /// <param name="taskcode"></param>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        void RequestToResubmitArrivalNotificationPH(string vcn, string remarks, string taskcode);


        /// <summary>
        /// Arrival Notification Request DHM
        /// </summary>
        /// <param name="VCN"></param>
        /// <param name="remarks"></param>
        /// <param name="taskcode"></param>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        void ApproveArrivalNotificationDhm(string vcn, string remarks, string taskcode);

        /// <summary>
        /// To Update Workflow for Arrival Notification Request for Resubmition DHM
        /// </summary>
        /// <param name="VCN"></param>
        /// <param name="remarks"></param>
        /// <param name="taskcode"></param> 
        [OperationContract]
        [FaultContract(typeof(Exception))]
        void RequestToResubmitArrivalNotificationDhm(string vcn, string remarks, string taskcode);


        /// <summary>
        /// Author   : Sandeep Appana
        /// Date     : 27-8-2014
        /// Purpose  : To Get Arrival Commodity record(s) based on vcn
        /// </summary>
        /// <param name="vcn"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<ArrivalCommodityVo> GetArrivalCommoditiesByVcn(string vcn);


        /// <summary>
        /// Author   : Omprakash kotha
        /// Date     : 13-11-2014
        /// Purpose  : To Get Arrival Commodity record(s) based on vcn
        /// </summary>
        /// <param name="vcn"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        ArrivalNotificationVO GetArrivalNotificationVO(string vcn);


        /// <summary>
        /// To get ArrivalNotification Details Based on VCN
        /// </summary>
        /// <param name="vcn"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        ArrivalNotificationReferenceVO GetArrivalNotificationBirthReferenceVO(string toid);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        ArrivalNotificationReferenceVO GetArrivalNotificationDraftReferenceVO();

        [OperationContract]
        [FaultContract(typeof(Exception))]
        ArrivalNotificationVO CancelArrivalNotification(ArrivalNotificationVO arrivalnotificationdata);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<VesselVO> GetVesselNamesAN(string searchvalue, string serchcolumn);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        void RejectArrivalNotification(string vcn, string remarks, string taskcode);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        void RejectArrivalNotificationByPhc(string vcn, string remarks, string taskcode);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        void RejectArrivalNotificationByIsps(string vcn, string remarks, string taskcode);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        void RejectArrivalNotificationByImdg(string vcn, string remarks, string taskcode);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        void RejectArrivalNotificationByDhm(string vcn, string remarks, string taskcode);       

        [OperationContract]
        [FaultContract(typeof(Exception))]
        string ArrivalBerthingRules1(string arrdraft, string preferedberthkey);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        string ArrivalBerthingRules2(string arrdraft, string preferedberthkey, string cargotype);


        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<RevenuePostingVO> GetArrivalVcnDetailsForAutocomplete(string searchvalue);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        IList<ArrivalNotificationVO> GetArrivalNotificationForWorkFlow(string vcn, int workflowinstanceid);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<AgentVO> GetAgents(string searchvalue);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        string ArrivalNotificationVoyageValidation(int vesselid, string voyagein, string voyageout);
    }
}
