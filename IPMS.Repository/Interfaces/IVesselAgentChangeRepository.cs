using System;
using System.Collections.Generic;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;

namespace IPMS.Repository
{
    public interface IVesselAgentChangeRepository
    {
        List<AgentVO> GetProposedAgents(int agentId, string portcode,string mode);
        List<VesselCallVO> GetVcnDetails(string portcode, int userId);
        List<Vessel> GetVesselDetails();
        List<VesselAgentChange> GetVesselAgentChangeRequestDetails(string portCode, int agentId, int userId, string etaFrom, string etaTo);
        List<VesselAgentChange> GetzVesselAgentChangeRequestDetails(string vcn);
        VesselAgentChange GetVesselAgentcahngeNotificationById(string value);
        int GetAgentId(string portcode, int userId);
        string GetUserName(int userId);
        int ValidateVcn(string vcn);
        List<VesselAgentChange> GetVesselAgentChangeRequestsSearchDetail(string vcn, string vesselName, string etaFrom, string etaTo, int agentId, int userId, string portcode);
        List<VesselCallVO> GetVCNActiveList(string portcode, int userId);
         
    }
}
