using IPMS.Domain.ValueObjects;
using IPMS.ServiceProxies.Contracts;
using IPMS.Web.ServiceProxies;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IPMS.ServiceProxies.Clients
{
    public class MaterialCodeMasterClient : UserClientBase<IMaterialCodeMasterService>, IMaterialCodeMasterService
    {
        public List<MaterialCodeMasterVO> GetMaterialCodeDetails()
        {
            return WrapOperationWithException(() => Channel.GetMaterialCodeDetails());
        }
    }
}