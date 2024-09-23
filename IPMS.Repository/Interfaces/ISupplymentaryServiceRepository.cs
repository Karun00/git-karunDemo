using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System.Collections.Generic;

namespace IPMS.Repository
{
    public interface ISupplymentaryServiceRepository
    {
        List<SubCategoryVO> GetServiceType();
        List<SuppServiceRequestVO> GetSupplymentaryServiceRequestList(string portcode, int agentUserId, int toUserId, int empId, string frmdate, string todate, string vcnSearch, string vesselName);
        SuppServiceRequestVO GetSupplymentaryServiceRequest(string portcode, string SuppServiceRequestId);
        List<SuppServiceRequestVO> AllSuppHotWorkInspectionDetails(string portcode);
        SuppServiceRequestVO GetSuppServiceRequestByID(int suppservicerequestid);
        List<SuppServiceRequestVO> GetSupplymentaryServiceRequestListVcn(string portcode, string VCN);


        List<SuppServiceRequestVO> AllSuppDockUnDockTimeDetails(string portcode);



        /// <summary>
        /// Author : Sandeep Appana
        /// Date   : 15th Sep 2014
        /// Purpose: To get all approved water service details
        /// </summary>
        /// <param name="vcn"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        List<SuppServiceRequestVO> GetApprovedWaterService(string vcn, string date);

        List<IMDGInformationVO> GetIMDGForSupplymentaryServiceRequest(string vcn);

        VesselCallMovementVO GetEtbEtubFromVcn(string vcn);

        UserMasterVO GetUserTypesForUser(int userId, string portCode);

        /// <summary>
        /// Author : Sandeep Appana
        /// Date   : 3rd June 2014
        /// Purpose: To get VCN by search value 
        /// </summary>
        /// <param name="PortCode"></param>
        /// <param name="AgentUserID"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        List<ServiceRequestVCNDetails> GetVCNDetailsForSuppServiceRequest(string portCode, int agentUserId, string searchValue);
        List<SuppServiceRequestVO> GetSupplementaryGridDetails(string portcode, int agentUserId, int toUserId, int empId, string frmdate, string todate, string vcnSearch, string vesselName);

    }
}
