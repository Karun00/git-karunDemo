using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;
using IPMS.ServiceProxies.Contracts;
using IPMS.Domain.Models;
using System.Threading.Tasks;
using System.Web.Mvc;
using IPMS.Domain.ValueObjects;
using IPMS.Web.ServiceProxies.Contracts;
using IPMS.Web.ServiceProxies;

namespace IPMS.ServiceProxies.Clients
{
    public class VesselAgentChangeClient : UserClientBase<IVesselAgentChangeService>, IVesselAgentChangeService
    {
        /// <summary>
        /// To Get Vessel Agent reffernce data
        /// </summary>
        /// <returns></returns>
        public VesselAgentReferenceVO GetVesselAgentChangeReferncesVo(string mode)
        {
            return WrapOperationWithException(() => Channel.GetVesselAgentChangeReferncesVo(mode));
        }
        /// <summary>
        /// To get cahnge of agent request details
        /// </summary>
        /// <returns></returns>
        public List<VesselAgentChangeVO> GetVesselAgentChangeRequestDetails(string etaFrom, string etaTo)
        {
            return WrapOperationWithException(() => Channel.GetVesselAgentChangeRequestDetails(etaFrom, etaTo));
        }
        /// <summary>
        /// To view change of agent request details data from pending tasks 
        /// </summary>
        /// <param name="vcn"></param>
        /// <returns></returns>
        public List<VesselAgentChangeVO> GetzVesselAgentChangeRequestDetails(string vcn)
        {
            return WrapOperationWithException(() => Channel.GetzVesselAgentChangeRequestDetails(vcn));
        }
        /// <summary>
        /// To add change of agent request details 
        /// </summary>
        /// <param name="vesselAgentChange"></param>
        /// <returns></returns>
        public VesselAgentChangeVO AddVesselAgentChange(VesselAgentChangeVO vesselAgentChange)
        {
            return WrapOperationWithException(() => Channel.AddVesselAgentChange(vesselAgentChange));
        }
        /// <summary>
        /// To modify change of request details
        /// </summary>
        /// <param name="vesselAgentChange"></param>
        /// <returns></returns>
        public VesselAgentChangeVO ModifyVesselAgentChanges(VesselAgentChangeVO vesselAgentChange)
        {
            return WrapOperationWithException(() => Channel.ModifyVesselAgentChanges(vesselAgentChange));
        }
        /// <summary>
        /// To verify change of agent request 
        /// </summary>
        /// <param name="referenceId"></param>
        /// <param name="remarks"></param>
        /// <param name="taskCode"></param>
        public void VerifyVesselAgentChangeOfRequest(string referenceId, string remarks, string taskCode)
        {
            WrapOperationWithException(() => Channel.VerifyVesselAgentChangeOfRequest(referenceId, remarks, taskCode));
        }

        /// <summary>
        /// To approve change of agent request
        /// </summary>
        /// <param name="referenceId"></param>
        /// <param name="remarks"></param>
        /// <param name="taskCode"></param>
        public void ApproveVesselAgentChangeOfRequest(string referenceId, string remarks, string taskCode)
        {
            WrapOperationWithException(() => Channel.ApproveVesselAgentChangeOfRequest(referenceId, remarks, taskCode));
        }
        /// <summary>
        /// To reject change of agent request 
        /// </summary>
        /// <param name="referenceId"></param>
        /// <param name="remarks"></param>
        /// <param name="taskCode"></param>
        public void RejectVesselAgentChangeOfRequest(string referenceId, string remarks, string taskCode)
        {
            WrapOperationWithException(() => Channel.RejectVesselAgentChangeOfRequest(referenceId, remarks, taskCode));
        }

        /// <summary>
        /// To get approved VCN's
        /// </summary>
        /// <returns></returns>
        public List<VesselCallVO> GetVCNDetails()
        {
            return WrapOperationWithException(() => Channel.GetVCNDetails());
        }

        public List<VesselCallVO> GetVCNActiveList()
        {
            return WrapOperationWithException(() => Channel.GetVCNActiveList());
        }

        public int ValidateVCN(string VCN)
        {
            return WrapOperationWithException(() => Channel.ValidateVCN(VCN));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="vcn"></param>
        /// <param name="vesselName"></param>
        /// <param name="etaFrom"></param>
        /// <param name="etaTo"></param>
        /// <returns></returns>
        public List<VesselAgentChangeVO> GetVesselAgentChangeRequestsSearchDetail(string vcn, string vesselName, string etaFrom, string etaTo)
        {
            return WrapOperationWithException(() => Channel.GetVesselAgentChangeRequestsSearchDetail(vcn, vesselName, etaFrom, etaTo));
        }
    }
}
