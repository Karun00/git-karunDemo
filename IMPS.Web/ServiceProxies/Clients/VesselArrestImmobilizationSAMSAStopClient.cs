using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;
using IPMS.Web.ServiceProxies.Contracts;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;

namespace IPMS.Web.ServiceProxies.Clients
{
    public class VesselArrestImmobilizationSAMSAStopClient : UserClientBase<IVesselArrestImmobilizationSAMSAStopService>, IVesselArrestImmobilizationSAMSAStopService
    {

        public List<VesselArrestImmobilizationSAMSAStopVO> GetVesselArrestImmobilizationSamsaStopList()
        {
            return WrapOperationWithException(() => Channel.GetVesselArrestImmobilizationSamsaStopList());
        }

        //public Task<List<VesselArrestImmobilizationSAMSAStopVO>> GetVesselArrestImmobilizationSamsaStopListAsync()
        //{
        //    return WrapOperationWithException(() => Channel.GetVesselArrestImmobilizationSamsaStopListAsync());
        //}

        public VesselArrestImmobilizationSAMSAStopVO AddVesselArrestImmobilizationSamsaStop(VesselArrestImmobilizationSAMSAStopVO vesselArrestImmobilizationSamsaStopData)
        {
            return WrapOperationWithException(() => Channel.AddVesselArrestImmobilizationSamsaStop(vesselArrestImmobilizationSamsaStopData));
        }

        public VesselArrestImmobilizationSAMSAStopVO ModifyVesselArrestImmobilizationSamsaStop(VesselArrestImmobilizationSAMSAStopVO vesselArrestImmobilizationSamsaStopData)
        {
            return WrapOperationWithException(() => Channel.ModifyVesselArrestImmobilizationSamsaStop(vesselArrestImmobilizationSamsaStopData));
        }

        public List<ArrivalNotificationVO> GetVcnDetails()
        {
            return WrapOperationWithException(() => Channel.GetVcnDetails());
        }

        //public Task<List<ArrivalNotificationVO>> GetVcnDetailsAsync()
        //{
        //    return WrapOperationWithException(() => Channel.GetVcnDetailsAsync());
        //}

    }
}