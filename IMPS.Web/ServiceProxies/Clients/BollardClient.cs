using IPMS.Domain.ValueObjects;
using IPMS.ServiceProxies.Contracts;
using IPMS.Web.ServiceProxies;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace IPMS.ServiceProxies.Clients
{
    public class BollardClient : UserClientBase<IBollardService>, IBollardService
    {
        public BollardVO AddBollard(BollardVO bollardData)
        {
            return WrapOperationWithException(() => Channel.AddBollard(bollardData));
        }

        public BollardVO ModifyBollard(BollardVO bollardData)
        {
            return WrapOperationWithException(() => Channel.ModifyBollard(bollardData));
        }

        public BollardVO GetBollardID(long id)
        {
            return WrapOperationWithException(() => Channel.GetBollardID(id));
        }

        public BollardVO DelBollard(long id)
        {
            return WrapOperationWithException(() => Channel.DelBollard(id));
        }

        public List<BollardVO> GetBollards()
        {
            return WrapOperationWithException(() => Channel.GetBollards());
        }

        public List<BollardVO> GetBollardsInBerths(string portCode, string quayCode, string berthCode)
        {
            return WrapOperationWithException(() => Channel.GetBollardsInBerths(portCode, quayCode, berthCode));
        }

        public List<BollardVO> GetBollardDetails()
        {
            return WrapOperationWithException(() => Channel.GetBollardDetails());
        }

        public List<QuayVO> GetPortQuays(string id)
        {
            return WrapOperationWithException(() => Channel.GetPortQuays(id));
        }

        public List<BerthVO> GetQuayBerths(string portCode, string quayCode)
        {
            return WrapOperationWithException(() => Channel.GetQuayBerths(portCode, quayCode));
        }

        //public Task<BollardVO> AddBollardAsync(BollardVO bollarddata)
        //{
        //    return WrapOperationWithException(() => Channel.AddBollardAsync(bollarddata));
        //}

        //public Task<BollardVO> GetBollardIDAsync(long id)
        //{
        //    return WrapOperationWithException(() => Channel.GetBollardIDAsync(id));
        //}

        //public Task<BollardVO> DelBollardAsync(long id)
        //{
        //    return WrapOperationWithException(() => Channel.DelBollardAsync(id));
        //}

        //public Task<List<BollardVO>> GetBollardsAsync()
        //{
        //    return WrapOperationWithException(() => Channel.GetBollardsAsync());
        //}
    }
}