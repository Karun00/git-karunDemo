using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;

namespace IPMS.Repository
{
    public interface IVesselArrestImmobilizationSAMSAStopRepository
    {
        VesselArrestImmobilizationSAMSAStopVO GetVesselArrestImmobilizationSamsaStopById(int vasId, string portCode);
        List<VesselArrestImmobilizationSAMSAStopVO> GetVesselArrestImmobilizationSamsaStopList(string portCode);
        int AddVesselArrestImmobilizationSamsaStop(VesselArrestImmobilizationSAMSAStopVO entity, int userID);
        void ModifyVesselArrestImmobilizationSamsaStop(VesselArrestImmobilizationSAMSAStopVO entity, int userId);
        List<ArrivalNotificationVO> GetVcnDetails(string portCode);
        VesselArrestImmobilizationSAMSAStopVO GetVesselArrestImmobilizationSamsaStopById(string vasId);
    }
}
