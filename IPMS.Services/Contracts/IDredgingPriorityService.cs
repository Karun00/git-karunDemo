using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace IPMS.Services
{
    [ServiceContract]
    public interface IDredgingPriorityService
    {
        /// <summary>
        /// To  Dredging Priority Reference Data While initialization
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<FinancialYearVO> GetMonths(int financialYearId);

        /// <summary>
        /// Get Months
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        DredgingPriorityVO GetDredgingPriorityReferenceVO();

        /// <summary>
        ///  To Get Dredging Priority Details
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<DredgingPriorityVO> DredgingPriorityDetails(int financialYearId);

        /// <summary>
        /// To Get Dredging Priority Volumes
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<DredgingPriorityVolumeVO> GetDredgingPriorityVolumes(int financialYearId);

        /// <summary>
        /// This method is used for fetch the Location data
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<DredgingPriorityVO> GetLocationTypes();


        /// <summary>
        /// This method is used for fetch the Berth data
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<DredgingPriorityVO> GetBerthTypes();

        /// <summary>
        /// To add Dredging Priority data
        /// </summary>
        /// <param name="request"></param>
        /// <param name="DredgingPrioritydata"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        DredgingPriorityVO AddDredgingPriorityDetails(DredgingPriorityVO dredgingPriorityData);

        /// <summary>
        /// To Update Dredging Priority data
        /// </summary>
        /// <param name="request"></param>
        /// <param name="dredgingPriorityData"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        DredgingPriorityVO ModifyDredgingPriorityDetails(DredgingPriorityVO dredgingPriorityData);
        /// <summary>
        ///  To Get Dredging Priority Details by dredgingpriorityid
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<DredgingPriorityVO> GetDredgingPriorityPendingView(int dredgingPriorityId);

        /// <summary>
        ///  To Approve Dredging Priority Request
        /// </summary>
        /// <param name="DredgingPriorityID"></param>
        /// <param name="remarks"></param>
        /// <param name="taskcode"></param>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        void ApproveDredgingPriority(string dredgingPriorityId, string remarks, string taskCode);

        /// <summary>
        /// To Reject Dredging Priority Request
        /// </summary>
        /// <param name="DredgingPriorityID"></param>
        /// <param name="remarks"></param>
        /// <param name="taskcode"></param>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        void RejectDredgingPriority(string dredgingPriorityId, string remarks, string taskCode);

        /// <summary>
        /// To Get Financial Year Data
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<FinancialYearVO> GetFinancialYear();

        /// <summary>
        /// Author  : Sandeep Appana
        /// Date    : 30th Dec 2014
        /// Purpose : To get List of Berth Occupation details
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<DredgingOperationVO> GetBerthOccupationList();
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<DredgingOperationVO> GetBerthOccupationById(int id);
        /// <summary>
        /// Author  : Sandeep Appana
        /// Date    : 1st Jan 2015
        /// Purpose : To get List of Dredging Volume details
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<DredgingOperationVO> GetDredgingVolumeList();
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<DredgingOperationVO> GetDredgingVolumeById(int id);
        /// <summary>
        /// Author  : Sandeep Appana
        /// Date    : 1st Jan 2015
        /// Purpose : Update Dredging Volume details
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        DredgingOperationVO UpdateDredgingVolume(DredgingOperationVO data);

        /// <summary>
        /// Author  : Sandeep Appana
        /// Date    : 2nd Jan 2015
        /// Purpose : Update Berth Occupation details
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        DredgingOperationVO UpdateBerthOccupation(DredgingOperationVO data);


        [OperationContract]
        [FaultContract(typeof(Exception))]
        void ApproveBerthOccupation(string dredgingPriorityAreaId, string remarks, string taskCode);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        void RejectBerthOccupation(string dredgingPriorityAreaId, string remarks, string taskCode);


        [OperationContract]
        [FaultContract(typeof(Exception))]
        void ApproveDredgingVolume(string dredgingPriorityAreaId, string remarks, string taskCode);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        void RejectDredgingVolume(string dredgingPriorityAreaId, string remarks, string taskCode);


        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<DredgingOperationVO> DredgingPriorityAreaDetails(int dredgingPriorityId);
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<DredgingOperationVO> DredgingPriorityAreaDetailsPending(int dredgingPriorityId);
        /// <summary>
        /// This method is used for fetch the Berth data
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<DredgingPriorityDocumentVO> GetDocument(int dredgingPriorityId);
        /// <summary>
        /// This method is used for cancel
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        DredgingOperationVO Cancel(DredgingOperationVO data);
       
    }


}
