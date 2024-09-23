using IPMS.Domain.ValueObjects;
using IPMS.ServiceProxies.Contracts;
using IPMS.Web.ServiceProxies;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IPMS.ServiceProxies.Clients
{

    public class QuayClient : UserClientBase<IQuayService>, IQuayService
    {
        public List<QuayVO> QuayDetails()
        {
            return WrapOperationWithException(() => Channel.QuayDetails());
        }

        public QuayVO AddQuay(QuayVO quayData)
        {
            return WrapOperationWithException(() => Channel.AddQuay(quayData));
        }

        public QuayVO ModifyQuay(QuayVO quayData)
        {
            return WrapOperationWithException(() => Channel.ModifyQuay(quayData));
        }

        public List<QuayVO> GetQuaysWithBerths(string portCode)
        {
            return WrapOperationWithException(() => Channel.GetQuaysWithBerths(portCode));
        }

        public List<QuayVO> GetQuaysWithBerthsMobile()
        {
            return WrapOperationWithException(() => Channel.GetQuaysWithBerthsMobile());
        }

        public QuayVO GetQuayId(long id)
        {
            return WrapOperationWithException(() => Channel.GetQuayId(id));
        }

        public QuayVO DeleteQuay(long id)
        {
            return WrapOperationWithException(() => Channel.DeleteQuay(id));
        }

        //public Task<List<QuayVO>> QuayDetailsAsync()
        //{
        //    return WrapOperationWithException(() => Channel.QuayDetailsAsync());
        //}

        //public Task<List<QuayVO>> GetQuaysWithBerthsAsync(string portCode)
        //{
        //    return WrapOperationWithException(() => Channel.GetQuaysWithBerthsAsync(portCode));
        //}

        //public Task<QuayVO> AddQuayAsync(QuayVO quaydata)
        //{
        //    return WrapOperationWithException(() => Channel.AddQuayAsync(quaydata));
        //}

        //public Task<QuayVO> ModifyQuayAsync(QuayVO quaydata)
        //{
        //    return WrapOperationWithException(() => Channel.ModifyQuayAsync(quaydata));
        //}

        //public Task<QuayVO> GetQuayIDAsync(long id)
        //{
        //    return WrapOperationWithException(() => Channel.GetQuayIDAsync(id));
        //}

        //public Task<QuayVO> DeletequayAsync(long id)
        //{
        //    return WrapOperationWithException(() => Channel.DeletequayAsync(id));
        //}
    }
}