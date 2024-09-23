using System;
using System.Collections.Generic;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;

namespace IPMS.Repository
{
    public interface IDivingRequestRepository
    {
        List<QuayVO> GetPortQuays(string portCode);
        List<LocationVO> GetOtherLocations(string portCode);
        List<BerthVO> GetQuayBerths(string portCode, string quayCode);
        List<BollardVO> GetBerthBollards(string portCode, string quayCode, string berthCode);
        List<DivingRequestVO> GetAllDivingRequests(string portCode);
        List<DivingRequestVO> GetAllDivingTaskExecutions(string portCode);
        List<LocationVO> GetAllLocations(string portCode);
        //Added by Srini 
        List<DivingRequestVO> GetAllDivingRequestOccupation(string portCode);
        DivingRequestVO GetDivingRequestOccupationById(string portCode, int divingRequestId);
        List<DivingRequestVO> GetDivingRequestByIdView(int requestId);
        DivingRequestVO GetDivingRequestById(int requestId);

        List<DivingRequestVO> GetDivingRequestsForScroll(string portCode);

        string GenerateDRN();
        DivingRequestVO AddDivingRequest(DivingRequestVO divingRequest, int userId, string portCode);
        DivingRequestVO ModifyDivingChecklist(DivingRequestVO divingRequestVO, int userId);
        DivingRequestVO ModifyDivingTaskExecution(DivingRequestVO divingRequestVO, int userId);

        DivingRequestVO GetDivingRequestByIdForNotification(string requestId);

        DivingRequestVO GetDivingRequestDetailsOnCompletion(string requestId);
    }
}
