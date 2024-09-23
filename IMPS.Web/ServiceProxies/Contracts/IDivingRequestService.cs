using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;

namespace IPMS.ServiceProxies.Contracts
{
    [ServiceContract]
    public interface IDivingRequestService : IDisposable
    {
        [OperationContract]
        List<QuayVO> GetPortQuays();

        [OperationContract]
        List<BerthVO> GetQuayBerths(string quayCode);

        [OperationContract]
        List<BollardVO> GetBerthBollards(string quayCode, string berthCode);

        [OperationContract]
        List<LocationVO> GetOtherLocations();

        [OperationContract]
        List<DivingRequestVO> GetAllDivingRequests();

        [OperationContract]
        DivingRequestVO AddDivingRequest(DivingRequestVO divingRequest);

        [OperationContract]
        List<DivingRequestVO> GetAllDivingTaskExecutions();

        [OperationContract]
        DivingRequestVO ModifyDivingCheckList(DivingRequestVO divingRequestvo);

        [OperationContract]
        DivingRequestVO ModifyDivingTaskExecution(DivingRequestVO divingRequestvo);

        [OperationContract]
        List<LocationVO> GetAllLocations();

        //  Added by Srini
        [OperationContract]
        List<DivingRequestVO> GetAllDivingRequestOccupation();

        [OperationContract]
        DivingRequestVO GetDivingRequestOccupationById(int divingRequestId);

        //ModifyDivingRequestOccupation
        [OperationContract]
        DivingRequestVO ModifyDivingRequestOccupation(DivingRequestVO divingRequestvo);

        [OperationContract]
        List<DivingRequestVO> GetDivingRequestByIdView(int requestId);

        //  Added by Srini
        [OperationContract]
        void ApproveDivingRequestOccupation(string divingRequestId, string remarks, string taskCode);

        [OperationContract]
        void VerifyDivingRequestOccupation(string divingRequestId, string remarks, string taskCode);

        [OperationContract]
        void RejectDivingRequestOccupation(string divingRequestId, string remarks, string taskCode);

        [OperationContract]
        List<DivingRequestVO> GetDivingrequestsForScroll();

        //[OperationContract]
        //List<DivingRequestVO> GetDivingrequestsForScrollAsync();

        [OperationContract]
        string GetLoggedInUserName();

        /// <summary>
        /// Author  : Sandeep Appana
        /// Date    : 11th Mar 2015
        /// Purpose : To get Diving Request Reasons details in subcategory
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<SubCategoryCodeNameVO> GetDivingRequestReasons();
    }
}
