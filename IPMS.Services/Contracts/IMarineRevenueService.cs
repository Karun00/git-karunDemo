using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ServiceModel;


namespace IPMS.Services
{
    [ServiceContract]
    public interface IMarineRevenueService
    {
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<RevenuePostingVO> GetMarineRevenueList();

        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<RevenuePostingVO> GetMarineRevenueDetails(string vcnSearch, string vesselName,string frmdate,string todate);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<RevenuePostingVO> GetVcnDetails(string searchValue, string searchColumn, string param);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        RevenuePostingSectionsVO GetRevenueSectionDetails(string vcn);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<AgentVO> GetVcnAgents(string searchValue);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<AgentAccountVO> GetAgentAccountDetails(int agentId);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        int AddMarineRevenueDetails(RevenuePostingSectionsVO revenuePostingDetails);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        RevenuePostingSectionsVO GetRevenueSectionDetailsView(int revenuePostingId, int agentId, int accountId);


        /// <summary>
        ///  /// Srini
        /// Adv search for VCN auto complete
        /// </summary>
        /// <param name="searchvalue"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<RevenuePostingVO> RevenuePostingVcnDetailsforAutocomplete(string searchvalue);


        /// <summary>
        ///  /// Srini
        /// Adv search for Vessel auto complete
        /// </summary>
        /// <param name="searchvalue"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<VesselVO> RevenuePostingVesselDetailsforAutocomplete(string searchvalue);
    }
}
