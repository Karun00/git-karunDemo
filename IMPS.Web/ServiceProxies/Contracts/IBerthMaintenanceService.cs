using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;

namespace IPMS.ServiceProxies.Contracts
{
     [ServiceContract]
     public interface IBerthMaintenanceService : IDisposable
     {
         /// <summary>
         /// To get Berth Maintenance Details
         /// </summary>
         /// <returns></returns>
         [OperationContract]
         List<BerthMaintenanceVO> GetBerthMaintenanceDetails();       

         /// <summary>
         /// To Get Berth Maintenance Reference data While initialization
         /// </summary>
         /// <returns></returns>
         [OperationContract]
         BerthMaintenanceVO GetBerthMaintenanceReferenceVO();    

         /// <summary>
         /// To Add Berth Maintenance Data
         /// </summary>
         /// <param name="data"></param>
         /// <returns></returns>
         [OperationContract]
         BerthMaintenanceVO AddBerthMaintenance(BerthMaintenanceVO data);         

         /// <summary>
         /// To Modify Berth Maintenance Data
         /// </summary>
         /// <param name="data"></param>
         /// <returns></returns>
         [OperationContract]
         BerthMaintenanceVO ModifyBerthMaintenance(BerthMaintenanceVO data);       

         /// <summary>
         ///  To Get Bollards based on Berth
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
         void ApproveBerthMaintenance(string berthmaintenanceid, string remarks, string taskcode);      

         /// <summary>
         ///  To Reject Berth Maintenance Request
         /// </summary>
         /// <param name="BerthMaintenanceID"></param>
         /// <param name="remarks"></param>
         /// <param name="taskcode"></param>
         [OperationContract]
         void RejectBerthMaintenance(string berthmaintenanceid, string remarks, string taskcode);

         /// <summary>
         ///  To get Berth Maintenance Details based on berthmaintenanceid
         /// </summary>
         /// <param name="berthMaintenanceId"></param>
         /// <returns></returns>
         [OperationContract]
         List<BerthMaintenanceVO> GetBerthMaintenance(int berthMaintenanceId);

         /// <summary>
         /// To get Berth Maintenance Details Asynchronously
         /// </summary>
         /// <returns></returns>
         //[OperationContract]
         //Task<List<BerthMaintenanceVO>> GetBerthMaintenanceDetailsAsync();

         /// <summary>
         /// To Get Berth Maintenance Reference data While initialization Asynchronously
         /// </summary>
         /// <returns></returns>
         //[OperationContract]
         //Task<BerthMaintenanceVO> GetBerthMaintenanceReferenceVOAsync();

         /// <summary>
         /// To Add Berth Maintenance Data Asynchronously
         /// </summary>
         /// <param name="berthmaintenancedata"></param>
         /// <returns></returns>
         //[OperationContract]
         //Task<BerthMaintenanceVO> AddBerthMaintenanceAsync(BerthMaintenanceVO berthmaintenancedata);

         /// <summary>
         /// To Modify Berth Maintenance Data Asynchronously
         /// </summary>
         /// <param name="berthmaintenancedata"></param>
         /// <returns></returns>
         //[OperationContract]
         //Task<BerthMaintenanceVO> ModifyBerthMaintenanceAsync(BerthMaintenanceVO berthmaintenancedata);

         /// <summary>
         /// To get Workflow Remarks
         /// </summary>
         /// <returns></returns>
         [OperationContract]
         string GetWorkFlowRemarks(int workFlowInstanceId);
     }
}
