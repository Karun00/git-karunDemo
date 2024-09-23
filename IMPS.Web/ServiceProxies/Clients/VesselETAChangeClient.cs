using IPMS.Domain.ValueObjects;
using IPMS.Web.ServiceProxies.Contracts;
using System.Collections.Generic;

namespace IPMS.Web.ServiceProxies.Clients
{
    public class VesselETAChangeClient : UserClientBase<IVesselETAChangeService>, IVesselETAChangeService
    {
        public List<VesselETAChangeVO> GetArrivalVcns()
        {
            return WrapOperationWithException(() => Channel.GetArrivalVcns());
        }

        public VesselETAChangeVO GetVesselInfoByVcns(string vcn)
        {
            return WrapOperationWithException(() => Channel.GetVesselInfoByVcns(vcn));
        }

        public VesselETAChangeVO PostVesselEtaChange(VesselETAChangeVO obj)
        {
            return WrapOperationWithException(() => Channel.PostVesselEtaChange(obj));
        }

        public List<VesselETAChangeVO> ChangeEtaDetails(string vcn, string vesselName, string etaFrom, string etaTo, string agentNameSearch)
        {
            return WrapOperationWithException(() => Channel.ChangeEtaDetails(vcn, vesselName, etaFrom, etaTo, agentNameSearch));
        }

        public List<VesselETAChangeVO> ChangezEtaDetails(string vcn, int? vesselEatChangeId)
        {
            return WrapOperationWithException(() => Channel.ChangezEtaDetails(vcn, vesselEatChangeId));
        }
    }
}