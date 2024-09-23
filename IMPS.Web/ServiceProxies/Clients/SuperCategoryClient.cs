using IPMS.Domain.ValueObjects;
using IPMS.ServiceProxies.Contracts;
using IPMS.Web.ServiceProxies;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IPMS.ServiceProxies.Clients
{
    public class SuperCategoryClient : UserClientBase<ISuperCategoryService>, ISuperCategoryService
    {
        public SuperCategoryVO AddSuperCategory(SuperCategoryVO supCatData)
        {
            return WrapOperationWithException(() => Channel.AddSuperCategory(supCatData));
        }

        public SuperCategoryVO ModifySuperCategory(SuperCategoryVO supCatData)
        {
            return WrapOperationWithException(() => Channel.ModifySuperCategory(supCatData));
        }

        //public Task<SuperCategoryVO> AddSuperCategoryAsync(SuperCategoryVO supcatdata)
        //{
        //    return WrapOperationWithException(() => Channel.AddSuperCategoryAsync(supcatdata));
        //}

        //public Task<SuperCategoryVO> ModifySuperCategoryAsync(SuperCategoryVO supcatdata)
        //{
        //    return WrapOperationWithException(() => Channel.ModifySuperCategoryAsync(supcatdata));
        //}

        public List<SuperCategoryVO> SuperCategoryDetails()
        {
            return WrapOperationWithException(() => Channel.SuperCategoryDetails());
        }

        //public Task<List<SuperCategoryVO>> SuperCategoryDetailsAsync()
        //{
        //    return WrapOperationWithException(() => Channel.SuperCategoryDetailsAsync());
        //}
    }
}