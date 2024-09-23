using System;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System.Collections.Generic;

namespace IPMS.Repository
{
    public interface IAgentRepository
    {

        AgentDetailsVO GetAgentForUser(int userid);
        AgentDetailsVO GetAgent(int agentid);
        AgentDetailsVO GetzAgent(string vcn);
        List<UserMasterVO> GetAllAgents(string portcode);
        List<PortCodeNameVO> GetAgentportbasedAccount(int agentid, string portcode);
        List<AgentAccount> GetAgentAccountDetailsbyAgentId(int agentid, string portcode);
        List<AgentVO> GetAllAgentswithAccountno(string portcode);
        // -- Added by sandeep on 12-11-2014
        AgentVO GetAgentDetailsInVesselCallByVcn(string vcn);
        //List<AgentVO> GetAllAgentsExceptLoginAgent(string portcode, int LoginAgent);
        List<AgentVO> GetAllAgentsExceptLogOnAgent(string portcode, int loginagent, String searchvalue);

        // -- end

    }
}
