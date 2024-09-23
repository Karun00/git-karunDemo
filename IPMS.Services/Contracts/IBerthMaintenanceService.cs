using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace IPMS.Services
{
     [ServiceContract]
     public interface IBerthMaintenanceService
    {
         /// <summary>
         /// To Add Berth Maintenance Data
         /// </summary>
         /// <param name="data"></param>
         /// <returns></returns>
         [OperationContract]
         [FaultContract(typeof(Exception))]
         BerthMaintenanceVO AddBerthMaintenance(BerthMaintenanceVO data);
         
         /// <summary>
         /// To Modify Berth Maintenance Data
         /// </summary>
         /// <param name="data"></param>
         /// <returns></returns>
         [OperationContract]
         [FaultContract(typeof(Exception))]
         BerthMaintenanceVO ModifyBerthMaintenance(BerthMaintenanceVO data);

         /// <summary>
         /// To get Berth Maintenance Details
         /// </summary>
         /// <returns></returns>
         [OperationContract]
         [FaultContract(typeof(Exception))]
         List<BerthMaintenanceVO> GetBerthMaintenanceDetails();

         /// <summary>
         /// To Get Berth Maintenance Reference data While initialization
         /// </summary>
         /// <returns></returns>
         [OperationContract]
         [FaultContract(typeof(Exception))]
         BerthMaintenanceVO GetBerthMaintenanceReferenceVO();

         /// <summary>
         /// To Get Bollards based on Berth
         /// </summary>
         /// <param name="portCode"></param>
         /// <param name="quayCode"></param>
         /// <param name="berthCode"></param>
         /// <returns></returns>
         [OperationContract]
         List<BollardVO> GetBerthBollards(string portCode, string quayCode, string berthCode);

         /// <summary>
         ///  To Approve Berth Maintenance Request
         /// </summary>
         /// <param name="BerthMaintenanceID"></param>
         /// <param name="remarks"></param>
         /// <param name="taskcode"></param>
         [OperationContract]
         [FaultContract(typeof(Exception))]
         void ApproveBerthMaintenance(string berthmaintenanceid, string remarks, string taskcode);

         /// <summary>
         /// To Reject Berth Maintenance Request
         /// </summary>
         /// <param name="BerthMaintenanceID"></param>
         /// <param name="remarks"></param>
         /// <param name="taskcode"></param>
         [OperationContract]
         [FaultContract(typeof(Exception))]
         void RejectBerthMaintenance(string berthmaintenanceid, string remarks, string taskcode);

         /// <summary>
         /// To get Berth Maintenance Details based on berthmaintenanceid
         /// </summary>
         /// <param name="berthMaintenanceId"></param>
         /// <returns></returns>
         [OperationContract]
         [FaultContract(typeof(Exception))]
         List<BerthMaintenanceVO> GetBerthMaintenance(int berthMaintenanceId);


         /// <summary>
         ///  To get Workflow Remarks
         /// </summary>
         /// <returns></returns>
         [OperationContract]
         [FaultContract(typeof(Exception))]
         string GetWorkFlowRemarks(int workFlowInstanceId);
    }
}
