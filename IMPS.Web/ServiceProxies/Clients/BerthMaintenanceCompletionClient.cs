using IPMS.Domain.ValueObjects;
using IPMS.ServiceProxies.Contracts;
using IPMS.Web.ServiceProxies;
using System.Collections.Generic;

namespace IPMS.ServiceProxies.Clients
{
    public class BerthMaintenanceCompletionClient : UserClientBase<IBerthMaintenanceCompletionService>, IBerthMaintenanceCompletionService
    {

        public BerthMaintenanceCompletionVO AddBerthMaintenanceCompletion(BerthMaintenanceCompletionVO data)
        {
            return WrapOperationWithException(() => Channel.AddBerthMaintenanceCompletion(data)); 
        }

        public BerthMaintenanceCompletionVO ModifyBerthMaintenanceCompletion(BerthMaintenanceCompletionVO data)
        {
            return WrapOperationWithException(() => Channel.ModifyBerthMaintenanceCompletion(data)); 
        }
        public List<BerthMaintenanceDataVO> GetBerthMaintenanceCompletionList()
        {
            return WrapOperationWithException(() => Channel.GetBerthMaintenanceCompletionList()); 
        }

        public List<DataVO> GetBethMaintenanceIDs()
        {
            return WrapOperationWithException(() => Channel.GetBethMaintenanceIDs());
        }

        public IEnumerable<DataVO> BethMaintenanceDetails(int id)
        {
            return WrapOperationWithException(() => Channel.BethMaintenanceDetails(id));
        }
        public void ApproveBerthMaintenanceCompletion(string berthmaintenancecompletionid, string remarks, string taskcode)
        {
            WrapOperationWithException(() => Channel.ApproveBerthMaintenanceCompletion(berthmaintenancecompletionid, remarks, taskcode));
        }
        public void RejectBerthMaintenanceCompletion(string berthmaintenancecompletionid, string remarks, string taskcode)
        {
            WrapOperationWithException(() => Channel.RejectBerthMaintenanceCompletion(berthmaintenancecompletionid, remarks, taskcode));
        }
        public List<BerthMaintenanceDataVO> GetBerthMaintenanceCompletion(int berthMaintenanceCompletionId)
        {
            return WrapOperationWithException(() => Channel.GetBerthMaintenanceCompletion(berthMaintenanceCompletionId));
        }
    }
}