using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;


namespace IPMS.Web.ServiceProxies.Contracts
{
    [ServiceContract]
    public interface IVesselArrestImmobilizationSAMSAStopService : IDisposable
    {
        [OperationContract]
        List<VesselArrestImmobilizationSAMSAStopVO> GetVesselArrestImmobilizationSamsaStopList();

        //[OperationContract]
        //Task<List<VesselArrestImmobilizationSAMSAStopVO>> GetVesselArrestImmobilizationSamsaStopListAsync();

        [OperationContract]
        VesselArrestImmobilizationSAMSAStopVO AddVesselArrestImmobilizationSamsaStop(VesselArrestImmobilizationSAMSAStopVO vesselArrestImmobilizationSamsaStopData);

        [OperationContract]
        VesselArrestImmobilizationSAMSAStopVO ModifyVesselArrestImmobilizationSamsaStop(VesselArrestImmobilizationSAMSAStopVO vesselArrestImmobilizationSamsaStopData);

        [OperationContract]
        List<ArrivalNotificationVO> GetVcnDetails();

        //[OperationContract]
        //Task<List<ArrivalNotificationVO>> GetVcnDetailsAsync();
    }
}
