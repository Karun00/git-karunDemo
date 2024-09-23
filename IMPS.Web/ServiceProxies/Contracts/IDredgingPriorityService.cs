using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;

namespace IPMS.ServiceProxies.Contracts
{
    [ServiceContract]
    public interface IDredgingPriorityService : IDisposable
    {
        /// <summary>
        /// To Get Dredging Priority Reference data While initialization
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        DredgingPriorityVO GetDredgingPriorityReferenceVO();


        /// <summary>
        /// To Get Months
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<FinancialYearVO> GetMonths(int financialYearId);
        /// <summary>
        ///  To Get Dredging Priority Details
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<DredgingPriorityVO> DredgingPriorityDetails(int financialYearId);

        /// <summary>
        /// To GetDredging Priority Volumes
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<DredgingPriorityVolumeVO> GetDredgingPriorityVolumes(int financialYearId);

        /// <summary>
        /// This method is used for fetch the Location data
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<DredgingPriorityVO> GetLocationTypes();


        /// <summary>
        /// This method is used for fetch the Berth data
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<DredgingPriorityVO> GetBerthTypes();

        /// <summary>
        /// To add Dredging Priority data
        /// </summary>
        /// <param name="request"></param>
        /// <param name="dredgingPriorityData"></param>
        /// <returns></returns>
        [OperationContract]
        DredgingPriorityVO AddDredgingPriorityDetails(DredgingPriorityVO dredgingPriorityData);

        /// <summary>
        /// To Update Dredging Priority data
        /// </summary>
        /// <param name="request"></param>
        /// <param name="DredgingPrioritydata"></param>
        /// <returns></returns>
        [OperationContract]
        DredgingPriorityVO ModifyDredgingPriorityDetails(DredgingPriorityVO dredgingPriorityData);


        /// <summary>
        ///  To Get Dredging Priority Details by ID
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<DredgingPriorityVO> GetDredgingPriorityPendingView(int dredgingPriorityId);

        /// <summary>
        ///  To Approve  Dredging Priority Request
        /// </summary>
        /// <param name="DredgingPriorityID"></param>
        /// <param name="remarks"></param>
        /// <param name="taskcode"></param>
        [OperationContract]
        void ApproveDredgingPriority(string dredgingPriorityId, string remarks, string taskCode);

        /// <summary>
        ///  To Reject Dredging Priority Request
        /// </summary>
        /// <param name="dredgingPriorityId"></param>
        /// <param name="remarks"></param>
        /// <param name="taskCode"></param>
        [OperationContract]
        void RejectDredgingPriority(string dredgingPriorityId, string remarks, string taskCode);

        /// <summary>
        /// To Get Financial Year
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<FinancialYearVO> GetFinancialYear();

        /// <summary>
        /// Author  : Sandeep Appana
        /// Date    : 30th Dec 2014
        /// Purpose : To get List of Berth Occupation details
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<DredgingOperationVO> GetBerthOccupationList();
        [OperationContract]
        List<DredgingOperationVO> GetBerthOccupationById(int id);
        /// <summary>
        /// Author  : Sandeep Appana
        /// Date    : 1st Jan 2015
        /// Purpose : To get List of Dredging Volume details
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<DredgingOperationVO> GetDredgingVolumeList();
        [OperationContract]
        List<DredgingOperationVO> GetDredgingVolumeById(int id);
        /// <summary>
        /// Author  : Sandeep Appana
        /// Date    : 1st Jan 2015
        /// Purpose : Update Dredging Volume details
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        DredgingOperationVO UpdateDredgingVolume(DredgingOperationVO data);

        /// <summary>
        /// Author  : Sandeep Appana
        /// Date    : 2nd Jan 2015
        /// Purpose : Update Berth Occupation details
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        DredgingOperationVO UpdateBerthOccupation(DredgingOperationVO data);


        /// <summary>
        /// This method is used for Approve Berth Occupation
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        void ApproveBerthOccupation(string dredgingPriorityAreaId, string remarks, string taskCode);
        /// <summary>
        /// This method is used for Reject Berth Occupation
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        void RejectBerthOccupation(string dredgingPriorityAreaId, string remarks, string taskCode);

        /// <summary>
        /// This method is used for Approve Dredging Volume
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        void ApproveDredgingVolume(string dredgingPriorityAreaId, string remarks, string taskCode);
        /// <summary>
        /// This method is used for Reject Dredging Volume
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        void RejectDredgingVolume(string dredgingPriorityAreaId, string remarks, string taskCode);

        /// <summary>
        /// This method is used for fetch Dredging Area Details 
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<DredgingOperationVO> DredgingPriorityAreaDetails(int dredgingPriorityId);
        /// <summary>
        /// This method is used for fetch Dredging Area Details For pending Task
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<DredgingOperationVO> DredgingPriorityAreaDetailsPending(int dredgingPriorityId);
        /// <summary>
        /// This method is used for fetch Document
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<DredgingPriorityDocumentVO> GetDocument(int dredgingPriorityId);

        /// <summary>
        /// This method is used for Cancel
        /// </summary>
        /// <param name="request"></param>
        /// <param name="value"></param>
        /// <returns></returns>

        [OperationContract]
        List<DredgingOperationVO> Cancel(DredgingOperationVO data);
    }

}
