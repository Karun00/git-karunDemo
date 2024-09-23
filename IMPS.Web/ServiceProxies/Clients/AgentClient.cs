using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;
using IPMS.ServiceProxies.Contracts;
using IPMS.Domain.Models;
using System.Threading.Tasks;
using System.Web.Mvc;
using IPMS.Web.ServiceProxies;
using IPMS.Domain.ValueObjects;

namespace IPMS.ServiceProxies.Clients
{
    public class AgentClient : UserClientBase<IAgentService>, IAgentService
    {
        public long RegisterAgent(Agent agentApplicant)
        {
            return WrapOperationWithException(() => Channel.RegisterAgent(agentApplicant));
        }

        public List<AgentVO> GetAgents(string status)
        {
            return WrapOperationWithException(() => Channel.GetAgents(status));
        }

        public Agent GetAgent(int id)
        {
            return WrapOperationWithException(() => Channel.GetAgent(id));
        }

        public List<PortCodeNameVO> GetAgentportbasedAccount(int agentid)
        {
            return WrapOperationWithException(() => Channel.GetAgentportbasedAccount(agentid));
        }

        public Agent GetzAgent(string vcn)
        {
            //int id = 0;
            //if (!string.IsNullOrEmpty(vcn))
            //{ id = Convert.ToInt32(vcn); }
            return WrapOperationWithException(() => Channel.GetzAgent(vcn));
        }


        public Agent VerifyAgent(int id)
        {
            return WrapOperationWithException(() => Channel.VerifyAgent(id));
        }

        public bool PutRejectAgent(int id)
        {
            return WrapOperationWithException(() => Channel.PutRejectAgent(id));
        }

        public bool InactiveAgent(int id)
        {
            return WrapOperationWithException(() => Channel.InactiveAgent(id));
        }

        public List<SubCategory> GetDocumentTypes()
        {
            return WrapOperationWithException(() => Channel.GetDocumentTypes());
        }

        public int CheckForTaxCombinationExistance(string incTaxNo)
        {
            return WrapOperationWithException(() => Channel.CheckForTaxCombinationExistance(incTaxNo));
        }
        public int CheckForVatCombinationExistance(string vatNo)
        {
            return WrapOperationWithException(() => Channel.CheckForVatCombinationExistance(vatNo));
        }
        public int CheckForRegCombinationExistance(string regNo)
        {
            return WrapOperationWithException(() => Channel.CheckForRegCombinationExistance(regNo));
        }

        //public long RegisterAgentAsync(Agent agentApplicant)
        //{
        //    return WrapOperationWithException(() => Channel.RegisterAgentAsync(agentApplicant));
        //}

        //public Task<List<AgentVO>> GetAgentsAsync(string status)
        //{
        //    return WrapOperationWithException(() => Channel.GetAgentsAsync(status));
        //}

        //public Task<Agent> GetAgentAsync(int id)
        //{
        //    return WrapOperationWithException(() => Channel.GetAgentAsync(id));
        //}

        //public Agent VerifyAgentAsync(int id)
        //{
        //    return WrapOperationWithException(() => Channel.VerifyAgentAsync(id));
        //}

        //public bool PutRejectAgentAsync(int id)
        //{
        //    return WrapOperationWithException(() => Channel.PutRejectAgentAsync(id));
        //}

        //public bool InactiveAgentAsync(int id)
        //{
        //    return WrapOperationWithException(() => Channel.InactiveAgentAsync(id));
        //}

        //public List<SubCategory> GetDocumentTypesAsync()
        //{
        //    return WrapOperationWithException(() => Channel.GetDocumentTypesAsync());
        //}

        //public int CheckForTaxCombinationExistanceAsync(string incTaxNo)
        //{
        //    return WrapOperationWithException(() => Channel.CheckForTaxCombinationExistanceAsync(incTaxNo));
        //}
        //public int CheckForVatCombinationExistanceAsync(string vatNo)
        //{
        //    return WrapOperationWithException(() => Channel.CheckForVatCombinationExistanceAsync(vatNo));
        //}
        //public int CheckForRegCombinationExistanceAsync(string regNo)
        //{
        //    return WrapOperationWithException(() => Channel.CheckForRegCombinationExistanceAsync(regNo));
        //}
        public void ApproveAgentRegistration(string agentId, string remarks, string taskcode)
        {
            WrapOperationWithException(() => Channel.ApproveAgentRegistration(agentId, remarks, taskcode));
        }

        //public void RequestToResubmitAgentRegistration(string agentId, string remarks, string taskcode)
        //{
        //    WrapOperationWithException(() => Channel.RequestToResubmitAgentRegistration(agentId, remarks, taskcode));
        //}

        public void VerifyAgentRegistration(string agentId, string remarks, string taskcode)
        {
            WrapOperationWithException(() => Channel.VerifyAgentRegistration(agentId, remarks, taskcode));
        }

        public void RejectAgentRegistration(string agentId, string remarks, string taskcode)
        {
            WrapOperationWithException(() => Channel.RejectAgentRegistration(agentId, remarks, taskcode));
        }

        public List<UserMasterVO> GetAllAgents()
        {
            return WrapOperationWithException(() => Channel.GetAllAgents());
        }

        // -- Added by sandeep on 12-11-2014
        public AgentVO GetAgentDetailsInVesselCallByVcn(string vcn)
        {
            return WrapOperationWithException(() => Channel.GetAgentDetailsInVesselCallByVcn(vcn));
        }
        // -- end

        public int AddAgentAccountDetails(AgentVO agentAccountDetails)
        {
            return WrapOperationWithException(() => Channel.AddAgentAccountDetails(agentAccountDetails));
        }
        public List<AgentAccountVO> GetAgentAccountDetails(int agentid)
        { return WrapOperationWithException(() => Channel.GetAgentAccountDetails(agentid)); }

    }




}