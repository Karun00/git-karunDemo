using IPMS.Domain.ValueObjects;
using IPMS.ServiceProxies.Contracts;
using IPMS.Web.ServiceProxies;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IPMS.ServiceProxies.Clients
{
    public class CargoManifestClient : UserClientBase<ICargoManifestService>, ICargoManifestService
    {
        public List<VCNData> CargoManifestDetails()
        {
            return WrapOperationWithException(() => Channel.CargoManifestDetails());
        }

        public List<ArrivalCommoditiesList> ArrivalCommodityDetails(string VCN)
        {
            return WrapOperationWithException(() => Channel.ArrivalCommodityDetails(VCN));
        }

        public CargoManifestVO ModifyCargoManifest(CargoManifestVO data)
        {
            return WrapOperationWithException(() => Channel.ModifyCargoManifest(data));
        }

        //public Task<CargoManifestVO> ModifyCargoManifestAsync(CargoManifestVO data)
        //{
        //    return WrapOperationWithException(() => Channel.ModifyCargoManifestAsync(data));
        //}

        public CargoManifestVO AddCargoManifest(CargoManifestVO data)
        {
            return WrapOperationWithException(() => Channel.AddCargoManifest(data));
        }

        //public Task<CargoManifestVO> AddCargoManifAsync(CargoManifestVO data)
        //{
        //    return WrapOperationWithException(() => Channel.AddCargoManifAsync(data));
        //}     

    }
}