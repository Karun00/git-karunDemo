using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Web.Mvc;
using Core.Repository.Providers.EntityFramework;


namespace IPMS.Services
{
    [ServiceContract]
    public interface IVesselAgentChangeService
    {
        /// <summary>
        /// To Get Vessel Agent reffernce data
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        VesselAgentReferenceVO GetVesselAgentChangeReferncesVo(string mode);
        /// <summary>
        /// To get cahnge of agent request details
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<VesselAgentChangeVO> GetVesselAgentChangeRequestDetails(string etaFrom, string etaTo);
        /// <summary>
        /// To view change of agent request details data from pending tasks 
        /// </summary>
        /// <param name="vcn"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<VesselAgentChangeVO> GetzVesselAgentChangeRequestDetails(string vcn);
        /// <summary>
        ///  To add change of agent request details 
        /// </summary>
        /// <param name="vesselAgentChange"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        VesselAgentChangeVO AddVesselAgentChange(VesselAgentChangeVO vesselAgentChange);
        /// <summary>
        /// To modify change of request details
        /// </summary>
        /// <param name="vesselAgentChange"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        VesselAgentChangeVO ModifyVesselAgentChanges(VesselAgentChangeVO vesselAgentChange);
        /// <summary>
        /// To verify change of agent request 
        /// </summary>
        /// <param name="referenceId"></param>
        /// <param name="remarks"></param>
        /// <param name="taskCode"></param>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        void VerifyVesselAgentChangeOfRequest(string referenceId, string remarks, string taskCode);
        /// <summary>
        /// To approve change of agent request
        /// </summary>
        /// <param name="referenceId"></param>
        /// <param name="remarks"></param>
        /// <param name="taskCode"></param>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        void ApproveVesselAgentChangeOfRequest(string referenceId, string remarks, string taskCode);
        /// <summary>
        /// To reject change of agent request 
        /// </summary>
        /// <param name="referenceId"></param>
        /// <param name="remarks"></param>
        /// <param name="taskCode"></param>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        void RejectVesselAgentChangeOfRequest(string referenceId, string remarks, string taskCode);
        /// <summary>
        /// To get approved VCN's
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<VesselCallVO> GetVCNDetails();
        //To get active vcn's
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<VesselCallVO> GetVCNActiveList();

        [OperationContract]
        [FaultContract(typeof(Exception))]
        int ValidateVCN(string VCN);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="vcn"></param>
        /// <param name="vesselName"></param>
        /// <param name="etaFrom"></param>
        /// <param name="etaTo"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<VesselAgentChangeVO> GetVesselAgentChangeRequestsSearchDetail(string vcn, string vesselName, string etaFrom, string etaTo);
    }
}

