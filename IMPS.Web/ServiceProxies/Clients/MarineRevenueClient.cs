using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using IPMS.ServiceProxies.Contracts;
using IPMS.Web.ServiceProxies;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IPMS.ServiceProxies.Clients
{
    public class MarineRevenueClient : UserClientBase<IMarineRevenueService>, IMarineRevenueService
    {
        public List<RevenuePostingVO> GetMarineRevenueList()
        {
            return WrapOperationWithException(() => Channel.GetMarineRevenueList());
        }

        public List<RevenuePostingVO> GetMarineRevenueDetails(string vcnSearch, string vesselName,string frmdate,string todate)
        {
            return WrapOperationWithException(() => Channel.GetMarineRevenueDetails(vcnSearch, vesselName, frmdate, todate));
        }

        public List<RevenuePostingVO> GetVcnDetails(string searchValue, string searchColumn,string param)
        {
            return WrapOperationWithException(() => Channel.GetVcnDetails(searchValue, searchColumn,param));
        }

        public RevenuePostingSectionsVO GetRevenueSectionDetails(string vcn)
        {
            return WrapOperationWithException(() => Channel.GetRevenueSectionDetails(vcn));
        }

        public List<AgentVO> GetVcnAgents(string searchValue)
        {
            return WrapOperationWithException(() => Channel.GetVcnAgents(searchValue));
        }

        public List<AgentAccountVO> GetAgentAccountDetails(int agentId)
        {
            return WrapOperationWithException(() => Channel.GetAgentAccountDetails(agentId));
        }

        public int AddMarineRevenueDetails(RevenuePostingSectionsVO revenuePostingDetails)
        {
            return WrapOperationWithException(() => Channel.AddMarineRevenueDetails(revenuePostingDetails));
        }

        public RevenuePostingSectionsVO GetRevenueSectionDetailsView(int revenuePostingId, int agentId, int accountId)
        {
            return WrapOperationWithException(() => Channel.GetRevenueSectionDetailsView(revenuePostingId, agentId, accountId));
        }
        public List<RevenuePostingVO> RevenuePostingVcnDetailsforAutocomplete(string searchvalue)
        {
            return WrapOperationWithException(() => Channel.RevenuePostingVcnDetailsforAutocomplete(searchvalue));
        }

        public List<VesselVO> RevenuePostingVesselDetailsforAutocomplete(string searchvalue)
        {
            return WrapOperationWithException(() => Channel.RevenuePostingVesselDetailsforAutocomplete(searchvalue));
        }
    }
}