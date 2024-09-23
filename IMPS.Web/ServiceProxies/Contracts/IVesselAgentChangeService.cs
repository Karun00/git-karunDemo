using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;

namespace IPMS.ServiceProxies.Contracts
{
    [ServiceContract]
    public interface IVesselAgentChangeService : IDisposable
    {
        /// <summary>
        /// To Get Vessel Agent reffernce data
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        VesselAgentReferenceVO GetVesselAgentChangeReferncesVo(string mode);
        /// <summary>
        /// To get cahnge of agent request details
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<VesselAgentChangeVO> GetVesselAgentChangeRequestDetails(string etaFrom, string etaTo);

        /// <summary>
        /// To view change of agent request details data from pending tasks 
        /// </summary>
        /// <param name="vcn"></param>
        /// <returns></returns>
        [OperationContract]
        List<VesselAgentChangeVO> GetzVesselAgentChangeRequestDetails(string vcn);
        /// <summary>
        /// To add change of agent request details 
        /// </summary>
        /// <param name="vesselAgentChange"></param>
        /// <returns></returns>
        [OperationContract]
        VesselAgentChangeVO AddVesselAgentChange(VesselAgentChangeVO vesselAgentChange);
        /// <summary>
        /// To modify change of request details
        /// </summary>
        /// <param name="vesselAgentChange"></param>
        /// <returns></returns>
        [OperationContract]
        VesselAgentChangeVO ModifyVesselAgentChanges(VesselAgentChangeVO vesselAgentChange);
        /// <summary>
        /// To verify change of agent request 
        /// </summary>
        /// <param name="referenceId"></param>
        /// <param name="remarks"></param>
        /// <param name="taskCode"></param>
        [OperationContract]
        void VerifyVesselAgentChangeOfRequest(string referenceId, string remarks, string taskCode);
        /// <summary>
        /// To approve change of agent request
        /// </summary>
        /// <param name="referenceId"></param>
        /// <param name="remarks"></param>
        /// <param name="taskCode"></param>
        [OperationContract]
        void ApproveVesselAgentChangeOfRequest(string referenceId, string remarks, string taskCode);
        /// <summary>
        /// To reject change of agent request 
        /// </summary>
        /// <param name="referenceId"></param>
        /// <param name="remarks"></param>
        /// <param name="taskCode"></param>
        [OperationContract]
        void RejectVesselAgentChangeOfRequest(string referenceId, string remarks, string taskCode);
        /// <summary>
        /// To get approved VCN's
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<VesselCallVO> GetVCNDetails();
        //To Get Active VCN's
        [OperationContract]
        List<VesselCallVO> GetVCNActiveList();

        [OperationContract]
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
        List<VesselAgentChangeVO> GetVesselAgentChangeRequestsSearchDetail(string vcn, string vesselName, string etaFrom, string etaTo);




    }
}

