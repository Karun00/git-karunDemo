using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace IPMS.ServiceProxies.Contracts
{
    [ServiceContract]
    public interface IAgentService : IDisposable
    {

        [OperationContract]
        long RegisterAgent(Agent agentApplicant);
        [OperationContract]
        List<AgentVO> GetAgents(string status);
        [OperationContract]
        Agent GetAgent(int id);

        [OperationContract]
        Agent GetzAgent(string vcn);


        [OperationContract]
        Agent VerifyAgent(int id);
        [OperationContract]
        bool PutRejectAgent(int id);
        [OperationContract]
        bool InactiveAgent(int id);
        [OperationContract]
        List<SubCategory> GetDocumentTypes();
        [OperationContract]
        int CheckForTaxCombinationExistance(string incTaxNo);
        [OperationContract]
        int CheckForVatCombinationExistance(string vatNo);
        [OperationContract]
        int CheckForRegCombinationExistance(string regNo);
        [OperationContract]
        List<PortCodeNameVO> GetAgentportbasedAccount(int agentid);
        //[OperationContract]
        //long RegisterAgentAsync(Agent agentApplicant);
        //[OperationContract]
        //Task<List<AgentVO>> GetAgentsAsync(string status);
        //[OperationContract]
        //Task<Agent> GetAgentAsync(int id);
        //[OperationContract]
        //Agent VerifyAgentAsync(int id);
        //[OperationContract]
        //bool PutRejectAgentAsync(int id);
        //[OperationContract]
        //bool InactiveAgentAsync(int id);
        //[OperationContract]
        //List<SubCategory> GetDocumentTypesAsync();
        //[OperationContract]
        //int CheckForTaxCombinationExistanceAsync(string incTaxNo);
        //[OperationContract]
        //int CheckForVatCombinationExistanceAsync(string vatNo);
        //[OperationContract]
        //int CheckForRegCombinationExistanceAsync(string regNo);
        [OperationContract]
        void ApproveAgentRegistration(string agentId, string remarks, string taskcode);
        [OperationContract]
        void VerifyAgentRegistration(string agentId, string remarks, string taskcode);
        [OperationContract]
        void RejectAgentRegistration(string agentId, string remarks, string taskcode);
        [OperationContract]
        List<UserMasterVO> GetAllAgents();

        // -- Added by sandeep on 12-11-2014
        [OperationContract]
        AgentVO GetAgentDetailsInVesselCallByVcn(string vcn);
        // -- end
          [OperationContract]
        int AddAgentAccountDetails(AgentVO agentAccountDetails);
          [OperationContract]
          List<AgentAccountVO> GetAgentAccountDetails(int agentid);
    }
}