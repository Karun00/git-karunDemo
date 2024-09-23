using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;

namespace IPMS.ServiceProxies.Contracts
{
    [ServiceContract]
    public interface IMarineRevenueService : IDisposable
    {
        [OperationContract]
        List<RevenuePostingVO> GetMarineRevenueList();

        [OperationContract]
        List<RevenuePostingVO> GetMarineRevenueDetails(string vcnSearch, string vesselName,string frmdate,string todate);

        [OperationContract]
        List<RevenuePostingVO> GetVcnDetails(string searchValue, string searchColumn,string param);

        [OperationContract]
        RevenuePostingSectionsVO GetRevenueSectionDetails(string vcn);

        [OperationContract]
        List<AgentVO> GetVcnAgents(string searchValue);

        [OperationContract]
        List<AgentAccountVO> GetAgentAccountDetails(int agentId);

        [OperationContract]
        int AddMarineRevenueDetails(RevenuePostingSectionsVO revenuePostingDetails);

        [OperationContract]
        RevenuePostingSectionsVO GetRevenueSectionDetailsView(int revenuePostingId, int agentId, int accountId);

        [OperationContract]
        List<RevenuePostingVO> RevenuePostingVcnDetailsforAutocomplete(string searchvalue);

        [OperationContract]
        List<VesselVO> RevenuePostingVesselDetailsforAutocomplete(string searchvalue);
    }
}
