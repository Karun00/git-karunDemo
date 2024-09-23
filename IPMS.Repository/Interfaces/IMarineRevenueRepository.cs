using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System.Collections.Generic;

namespace IPMS.Repository
{
    public interface IMarineRevenueRepository
    {
        List<RevenuePostingVO> GetMarineRevenueList(string portCode);

        List<RevenuePostingVO> GetMarineRevenueDetails(string portCode, string vcnSearch, string vesselName, string frmdate, string todate);

        List<RevenuePostingVO> GetVcnDetails(string searchValue, string searchColumn, string param, string portCode);

        RevenuePostingSectionsVO GetRevenueSectionDetails(string vcn, string portCode);

        List<AgentVO> GetVcnAgents(string searchValue, string portCode);

        List<AgentAccountVO> GetAgentAccountDetails(int agentId, string portCode);

        RevenuePostingSectionsVO GetRevenueSectionDetailsView(int revenuePostingId, int agentId, int accountId, string portCode);
        /// <summary>
        /// Srini - 
        /// Adv Search for VCN Auto complete
        /// </summary>
        /// <param name="searchValue"></param>
        /// <param name="portCode"></param>
        /// <returns></returns>
        List<RevenuePostingVO> RevenuePostingVcnDetailsforAutocomplete(string searchValue, string portCode);
        /// <summary>
        /// Srini
        /// Adv search for Vessel auto complete
        /// </summary>
        /// <param name="PortCode"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        List<VesselVO> RevenuePostingVesselDetailsforAutocomplete(string PortCode, string searchValue);
        
    }
}
