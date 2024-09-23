using IPMS.Domain.ValueObjects;
using IPMS.ServiceProxies.Contracts;
using IPMS.Web.ServiceProxies;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IPMS.ServiceProxies.Clients
{
    public class CraftmasterClient : UserClientBase<ICraftMasterService>, ICraftMasterService
    {
        public List<CraftVO> GetCraftList()
        {
            return WrapOperationWithException(() => Channel.GetCraftList());
        }

        public CraftVO AddCraft(CraftVO craftData)
        {
            return WrapOperationWithException(() => Channel.AddCraft(craftData));
        }

        public CraftVO ModifyCraft(CraftVO craftData)
        {
            return WrapOperationWithException(() => Channel.ModifyCraft(craftData));
        }

        public CraftReferenceVO GetCraftReferences()
        {
            return WrapOperationWithException(() => Channel.GetCraftReferences());
        }

        //public Task<List<CraftVO>> GetCraftlistAsync()
        //{
        //    return WrapOperationWithException(() => Channel.GetCraftlistAsync());
        //}

        //public Task<CraftVO> AddCraftAsync(CraftVO craftdata)
        //{
        //    return WrapOperationWithException(() => Channel.AddCraftAsync(craftdata));
        //}

        //public Task<CraftVO> ModifyCraftAsync(CraftVO craftdata)
        //{
        //    return WrapOperationWithException(() => Channel.ModifyCraftAsync(craftdata));
        //}

        //public Task<CraftReferenceVO> GetCraftReferencesAsync()
        //{
        //    return WrapOperationWithException(() => Channel.GetCraftReferencesAsync());
        //}
    }
}