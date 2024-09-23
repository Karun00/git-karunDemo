using IPMS.Domain.ValueObjects;
using IPMS.ServiceProxies.Contracts;
using IPMS.Web.ServiceProxies;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IPMS.ServiceProxies.Clients
{
    public class ServiceTypeClient : UserClientBase<IServiceTypeService>, IServiceTypeService
    {
        public List<ServiceTypeVO> ServiceTypeDetails()
        {
            return WrapOperationWithException(() => Channel.ServiceTypeDetails());
        }

        public ServiceTypeVO AddServiceType(ServiceTypeVO serviceTypeData)
        {
            return WrapOperationWithException(() => Channel.AddServiceType(serviceTypeData));
        }

        public ServiceTypeVO ModifyServiceType(ServiceTypeVO serviceTypeData)
        {
            return WrapOperationWithException(() => Channel.ModifyServiceType(serviceTypeData));
        }
    }
}