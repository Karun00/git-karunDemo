using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace IPMS.ServiceProxies.Contracts
{
    [ServiceContract]
    public interface IBerthMaintenanceCompletionService : IDisposable
    {
        /// <summary>
        ///  To Add Berth Maintenance Completion Data
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [OperationContract]
        BerthMaintenanceCompletionVO AddBerthMaintenanceCompletion(BerthMaintenanceCompletionVO data);

        /// <summary>
        /// To Modify Berth Maintenance Completion Data
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [OperationContract]
        BerthMaintenanceCompletionVO ModifyBerthMaintenanceCompletion(BerthMaintenanceCompletionVO data);

        /// <summary>
        /// To get Berth Maintenance Completion Details
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<BerthMaintenanceDataVO> GetBerthMaintenanceCompletionList();

        /// <summary>
        /// To Get Berth Maintenance Ids
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<DataVO> GetBethMaintenanceIDs();

        /// <summary>
        /// To Get Berth Maintenance Details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OperationContract]
        IEnumerable<DataVO> BethMaintenanceDetails(int id);

        /// <summary>
        /// To Approve Berth Maintenance Completion
        /// </summary>
        /// <param name="BerthMaintenanceCompletionID"></param>
        /// <param name="remarks"></param>
        /// <param name="taskcode"></param>
        [OperationContract]
        void ApproveBerthMaintenanceCompletion(string berthmaintenancecompletionid, string remarks, string taskcode);

        /// <summary>
        /// To Reject Berth Maintenance Completion
        /// </summary>
        /// <param name="BerthMaintenanceCompletionID"></param>
        /// <param name="remarks"></param>
        /// <param name="taskcode"></param>
        [OperationContract]
        void RejectBerthMaintenanceCompletion(string berthmaintenancecompletionid, string remarks, string taskcode);

        /// <summary>
        ///  To Get Berth Maintenance Completion Details By ID
        /// </summary>
        /// <param name="berthMaintenanceCompletionId"></param>
        /// <returns></returns>
        [OperationContract]
        List<BerthMaintenanceDataVO> GetBerthMaintenanceCompletion(int berthMaintenanceCompletionId);

    }
}