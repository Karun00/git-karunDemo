using IPMS.Domain.ValueObjects;
using System.Collections.Generic;

namespace IPMS.Repository
{
    public interface ICargoManifestRepository
    {
        List<VCNData> CargoManifestDetails(string portCode);
        List<ArrivalCommoditiesList> ArrivalCommodityDetails(string VCN);
        CargoManifestVO AddCargoManifest(CargoManifestVO entity, int UserId);
        CargoManifestVO ModifyCargoManifest(CargoManifestVO entity, int UserId);
    }
}
