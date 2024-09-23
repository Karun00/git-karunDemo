using IPMS.Domain.ValueObjects;
using IPMS.ServiceProxies.Contracts;
using IPMS.Web.ServiceProxies;
using IPMS.Web.ServiceProxies.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IPMS.ServiceProxies.Clients
{
    public class VesselSAPPostingClient : UserClientBase<IVesselSAPPostingService>, IVesselSAPPostingService
    {
        public List<VesselSAPPostingVO> GetVesselsList(string SearchColumn, string searchValue)
        {
            return WrapOperationWithException(() => Channel.GetVesselsList(SearchColumn, searchValue));
        }

        public VesselSAPPostingVO PostVesselSAP(VesselSAPPostingVO value)
        {
            return WrapOperationWithException(() => Channel.PostVesselSAP(value));
        }

        public List<SAPPostingVO> GetSAPVesselPostGrid()
        {
            return WrapOperationWithException(() => Channel.GetSAPVesselPostGrid());
        }
        
    }
}