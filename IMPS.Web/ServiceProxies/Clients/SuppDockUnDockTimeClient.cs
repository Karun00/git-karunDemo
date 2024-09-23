using System.Collections.Generic;
using System.ServiceModel;
using IPMS.ServiceProxies.Contracts;
using System.Threading.Tasks;
using IPMS.Domain.ValueObjects;
using IPMS.Web.ServiceProxies;

namespace IPMS.ServiceProxies.Clients
{
    public class SuppDockUnDockTimeClient : UserClientBase<ISuppDockUnDockTimeService>, ISuppDockUnDockTimeService
    {

        public List<SuppDryDockVO> AllSuppDockUnDockTimeDetails()
        {
            return WrapOperationWithException(() => Channel.AllSuppDockUnDockTimeDetails());
        }
        public SuppDryDockVO ModifySuppDockUnDockTime(SuppDryDockVO data)
        {
            return WrapOperationWithException(() => Channel.ModifySuppDockUnDockTime(data));
        }
    }
}