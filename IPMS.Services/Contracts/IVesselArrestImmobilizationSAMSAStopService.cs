using System;
using IPMS.Domain.Models;
using System.Collections.Generic;
using System.ServiceModel;
using IPMS.Domain.ValueObjects;

namespace IPMS.Services
{
    [ServiceContract]
    public interface IVesselArrestImmobilizationSAMSAStopService
    {
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<VesselArrestImmobilizationSAMSAStopVO> GetVesselArrestImmobilizationSamsaStopList();

        [OperationContract]
        [FaultContract(typeof(Exception))]
        VesselArrestImmobilizationSAMSAStopVO AddVesselArrestImmobilizationSamsaStop(VesselArrestImmobilizationSAMSAStopVO vesselArrestImmobilizationSamsaStopData);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        VesselArrestImmobilizationSAMSAStopVO ModifyVesselArrestImmobilizationSamsaStop(VesselArrestImmobilizationSAMSAStopVO vesselArrestImmobilizationSamsaStopData);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<ArrivalNotificationVO> GetVcnDetails();

        [OperationContract]
        [FaultContract(typeof(Exception))]
        Entity GetEntities(string entityCode);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        VesselArrestImmobilizationSAMSAStopVO GetVesselArrestImmobilizationSamsaStopById(int vasId);
    }
}
