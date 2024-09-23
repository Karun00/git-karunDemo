using IPMS.Domain.ValueObjects;
using IPMS.ServiceProxies.Contracts;
using IPMS.Web.ServiceProxies;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IPMS.ServiceProxies.Clients
{
    public class LocationClient : UserClientBase<ILocationService>, ILocationService
    {
        public List<LocationVO> LocationDetails()
        {
            return WrapOperationWithException(() => Channel.LocationDetails());
        }
        public LocationVO AddLocation(LocationVO locationData)
        {
            return WrapOperationWithException(() => Channel.AddLocation(locationData));
        }
        public LocationVO ModifyLocation(LocationVO locationData)
        {
            return WrapOperationWithException(() => Channel.ModifyLocation(locationData));
        }
    }
}