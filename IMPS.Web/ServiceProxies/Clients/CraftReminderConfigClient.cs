using IPMS.Domain.ValueObjects;
using IPMS.ServiceProxies.Contracts;
using IPMS.Web.ServiceProxies;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IPMS.ServiceProxies.Clients
{
    public class CraftReminderConfigClient : UserClientBase<ICraftReminderConfigService>, ICraftReminderConfigService
    {
        public List<CraftReminderConfigVO> GetCraftReminderConfigDetails(int craftId)
        {
            return WrapOperationWithException(() => Channel.GetCraftReminderConfigDetails(craftId));
        }
        public CraftReferenceVO GetCraftReminderReferences()
        {
            return WrapOperationWithException(() => Channel.GetCraftReminderReferences());
        }
        public CraftVO AddCraftReminderConfig(CraftReminderConfigVO data)
        {
            return WrapOperationWithException(() => Channel.AddCraftReminderConfig(data));
        }
        public CraftReminderConfigVO ModifyCraftReminderConfig(CraftReminderConfigVO data)
        {
            return WrapOperationWithException(() => Channel.ModifyCraftReminderConfig(data));
        }

        //public Task<List<CraftReminderConfigVO>> GetCraftReminderConfigDetailsAsync(int craftID)
        //{
        //    return WrapOperationWithException(() => Channel.GetCraftReminderConfigDetailsAsync(craftID));
        //}
        //public Task<CraftReferenceVO> GetCraftReminderReferencesAsync()
        //{
        //    return WrapOperationWithException(() => Channel.GetCraftReminderReferencesAsync());
        //}
        //public Task<CraftVO> AddCraftReminderConfigAsync(CraftReminderConfigVO craftConfigVO)
        //{
        //    return WrapOperationWithException(() => Channel.AddCraftReminderConfigAsync(craftConfigVO));
        //}
        //public Task<CraftReminderConfigVO> ModifyCraftReminderConfigAsync(CraftReminderConfigVO craftConfigVO)
        //{
        //    return WrapOperationWithException(() => Channel.ModifyCraftReminderConfigAsync(craftConfigVO));
        //}

        public List<CraftVO> GetCraftReminderConfigById(int craftReminderConfigId)
        {
            return WrapOperationWithException(() => Channel.GetCraftReminderConfigById(craftReminderConfigId));
        }

        //public Task<List<CraftVO>> GetCraftReminderConfigByIDAsync(int craftreminderconfigID)
        //{
        //    return WrapOperationWithException(() => Channel.GetCraftReminderConfigByIDAsync(craftreminderconfigID));
        //}

        public void AcknowledgeCraftReminderConfig(string craftReminderConfigID, string comments, string taskcode)
        {
            WrapOperationWithException(() => Channel.AcknowledgeCraftReminderConfig(craftReminderConfigID, comments, taskcode));
        }


    }
}