using System;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System.Collections.Generic;
namespace IPMS.Repository
{
    public interface IBerthPreSchedulingRepository
    {
        List<AgentData> GetAllAgents(string portCode);
        List<VCMData> GetVCMList(string portCode,int userId,string userType, string agentId, string eta, string etd, string vesselType, string reasonForVisit, string cargoType, string movementStatus);
        List<VCMTableData> GetVCMTableList(string portCode, int userId, string userType, string quayCode, string berthCode, string vesselStatus, string eta, string toDate);
        List<SuitableBerthVO> GetSuitableBerths(string vcn, string portCode, int userId, string userType, string etb, string etub, string vesselCallMovementId);
        List<BerthDataVO> GetBerthsList(string portCode);
        CompanyVO GetUserDetails(int userId);
        VesselCallMovement GetVesselCallMomentDetailsById(string vesselCallMovementId);
    
        //BerthPreSchedulingVO ModifyBerthPreScheduling(BerthPreSchedulingVO vesselcalldata,int UserID,string PortCode,string EntityCode)
    }
}
