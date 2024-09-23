using IPMS.Domain.Models;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Web.Mvc;
using IPMS.Domain.ValueObjects;


namespace IPMS.Services
{
    [ServiceContract]
    public interface IAgentService : IDisposable
    {
        /*IEnumerable<Applicant> GetApplicantsInAPort(Port port);[OperationContract][FaultContract(typeof(Exception))]
        Applicant RegisterAgent(IPMS.Domain.Models.Applicant agentApplicant);[OperationContract][FaultContract(typeof(Exception))]*/
        
        [OperationContract]
        [FaultContract(typeof(Exception))]
        decimal RegisterAgent(IPMS.Domain.Models.Agent agentApplicant);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<AgentVO> GetAgents(string status);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        Agent GetAgent(int id);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        Agent GetzAgent(string vcn);


        [OperationContract]
        [FaultContract(typeof(Exception))]
        Agent VerifyAgent(int id);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        bool PutRejectAgent(int id);

        [OperationContract]
        bool InactiveAgent(int id);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<SubCategory> GetDocumentTypes();

        [OperationContract]
        [FaultContract(typeof(Exception))]
        int CheckForTaxCombinationExistance(string incTaxNo);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        int CheckForRegCombinationExistance(string regNo);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        int CheckForVatCombinationExistance(string vatNo);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        void ApproveAgentRegistration(string agentId, string remarks, string taskcode);
        [OperationContract]
        [FaultContract(typeof(Exception))]
        void VerifyAgentRegistration(string agentId, string remarks, string taskcode);
        [OperationContract]
        [FaultContract(typeof(Exception))]
        void RejectAgentRegistration(string agentId, string remarks, string taskcode);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<PortCodeNameVO> GetAgentportbasedAccount(int agentid);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        int AddAgentAccountDetails(AgentVO agentAccountDetails);
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<AgentAccountVO> GetAgentAccountDetails(int agentid);


        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<UserMasterVO> GetAllAgents();

        // -- Added by sandeep on 12-11-2014
        [OperationContract]
        [FaultContract(typeof(Exception))]
        AgentVO GetAgentDetailsInVesselCallByVcn(string vcn);
        // -- end

    }
}
