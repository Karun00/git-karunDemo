using IPMS.Domain.ValueObjects;
using IPMS.ServiceProxies.Contracts;
using IPMS.Web.ServiceProxies;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IPMS.ServiceProxies.Clients
{
    public class BerthMaintenanceClient : UserClientBase<IBerthMaintenanceService>, IBerthMaintenanceService
    {
        public List<BerthMaintenanceVO> GetBerthMaintenanceDetails()
        {
            return WrapOperationWithException(() => Channel.GetBerthMaintenanceDetails());
        }
        //public Task<List<BerthMaintenanceVO>> GetBerthMaintenanceDetailsAsync()
        //{
        //    return WrapOperationWithException(() => Channel.GetBerthMaintenanceDetailsAsync());
        //}
        public BerthMaintenanceVO GetBerthMaintenanceReferenceVO()
        {
            return WrapOperationWithException(() => Channel.GetBerthMaintenanceReferenceVO());
        }
        //public Task<BerthMaintenanceVO> GetBerthMaintenanceReferenceVOAsync()
        //{
        //    return WrapOperationWithException(() => Channel.GetBerthMaintenanceReferenceVOAsync());
        //}
        public BerthMaintenanceVO AddBerthMaintenance(BerthMaintenanceVO data)
        {
            return WrapOperationWithException(() => Channel.AddBerthMaintenance(data));
        }
        //public Task<BerthMaintenanceVO> AddBerthMaintenanceAsync(BerthMaintenanceVO data)
        //{
        //    return WrapOperationWithException(() => Channel.AddBerthMaintenanceAsync(data));
        //}
        public BerthMaintenanceVO ModifyBerthMaintenance(BerthMaintenanceVO data)
        {
            return WrapOperationWithException(() => Channel.ModifyBerthMaintenance(data));
        }
        //public Task<BerthMaintenanceVO> ModifyBerthMaintenanceAsync(BerthMaintenanceVO data)
        //{
        //    return WrapOperationWithException(() => Channel.ModifyBerthMaintenanceAsync(data));
        //}
        public List<BollardVO> GetBerthBollards(string portCode, string quayCode, string berthCode)
        {
            return WrapOperationWithException(() => Channel.GetBerthBollards(portCode, quayCode, berthCode));
        }

        public void ApproveBerthMaintenance(string berthmaintenanceid, string remarks, string taskcode)
        {
            WrapOperationWithException(() => Channel.ApproveBerthMaintenance(berthmaintenanceid, remarks, taskcode));
        }
        public void RejectBerthMaintenance(string berthmaintenanceid, string remarks, string taskcode)
        {
            WrapOperationWithException(() => Channel.RejectBerthMaintenance(berthmaintenanceid, remarks, taskcode));
        }
        public List<BerthMaintenanceVO> GetBerthMaintenance(int berthMaintenanceId)
        {
            return WrapOperationWithException(() => Channel.GetBerthMaintenance(berthMaintenanceId));
        }

        public string GetWorkFlowRemarks(int workFlowInstanceId)
        {
            return WrapOperationWithException(() => Channel.GetWorkFlowRemarks(workFlowInstanceId));
        }

    }
}