using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace IPMS.Services
{
    [ServiceContract]
    public interface IDivingRequestService
    {
        [OperationContract]
        List<QuayVO> GetPortQuays();

        [OperationContract]
        List<LocationVO> GetOtherLocations();

        [OperationContract]
        List<BerthVO> GetQuayBerths(string quayCode);

        [OperationContract]
        List<BollardVO> GetBerthBollards(string quayCode, string berthCode);

        [OperationContract]
        List<DivingRequestVO> GetAllDivingRequests();

        [OperationContract]
        [FaultContract(typeof(Exception))]
        DivingRequestVO AddDivingRequest(DivingRequestVO divingRequest);

        /// <summary>
        /// Author  : Sandeep Appana  
        /// Date    : 5th September 2014
        /// Purpose : To Get Diving Tas kExecution details
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<DivingRequestVO> GetAllDivingTaskExecutions();

        /// <summary>
        /// Author  : Sandeep Appana  
        /// Date    : 5th September 2014
        /// Purpose : To Modify/Update Diving Checklist Data 
        /// </summary>
        /// <param name="divingRequestvo"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        DivingRequestVO ModifyDivingCheckList(DivingRequestVO divingRequestvo);

        /// <summary>
        /// Author  : Sandeep Appana  
        /// Date    : 5th September 2014
        /// Purpose : To  Modify/Update Diving Task Execution Data
        /// </summary>
        /// <param name="divingRequestvo"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        DivingRequestVO ModifyDivingTaskExecution(DivingRequestVO divingRequestvo);

        //Added By Srin
        [OperationContract]
        List<DivingRequestVO> GetAllDivingRequestOccupation();

        [OperationContract]
        DivingRequestVO GetDivingRequestOccupationById(int divingRequestId);

        [OperationContract]
        List<DivingRequestVO> GetDivingRequestByIdView(int requestId);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        DivingRequestVO ModifyDivingRequestOccupation(DivingRequestVO divingRequestvo);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<LocationVO> GetAllLocations();

        [OperationContract]
        [FaultContract(typeof(Exception))]
        void ApproveDivingRequestOccupation(string divingRequestId, string remarks, string taskCode);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        void VerifyDivingRequestOccupation(string divingRequestId, string remarks, string taskCode);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        void RejectDivingRequestOccupation(string divingRequestId, string remarks, string taskCode);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<DivingRequestVO> GetDivingrequestsForScroll();

        [OperationContract]
        [FaultContract(typeof(Exception))]
        string GetLoggedInUserName();

        /// <summary>
        /// Author  : Sandeep Appana
        /// Date    : 11th Mar 2015
        /// Purpose : To get Diving Request Reasons details in subcategory
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<SubCategoryCodeNameVO> GetDivingRequestReasons();
    }
}
