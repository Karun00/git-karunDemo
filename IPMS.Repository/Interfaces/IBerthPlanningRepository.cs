using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Repository
{
    public interface IBerthPlanningRepository
    {
        List<QuayVO> GetQuaysInPort(string portCode);

        List<BerthsData> GetBerthsWithBollard(string portCode, string quayCode);

        List<QuayBerthBollardData> GetQuayBerthsBollard(string portCode);

        List<BerthPlanningVO> SaveVesselCallMovements(List<BerthPlanningVO> plannedVessels, int userId);

        List<BerthPlanningVO> GetVesselInformation(string portCode, int userId, string userType, string fromDate, string toDate);

        List<BerthMaintenanceData> GetBerthMaintenance(string portCode, int userId, string fromDate, string toDate);

        UserData GetUserDetails(string portCode, int userId);

        List<BerthPlanningConfiguration> GetBerthPlanningConfigurations(string portCode);

        Boolean CheckBerthAvailability(string portCode, string vcn, string vcmId, string quayCode, string fromBerthCode, string fromBollardMeter, string toBerthCode, string toBollardMeter, string fromTime, string toTime);

        void ShiftingServiceRequest(string vcn);

        List<BerthPlanningVO> GetVesselInformationGIS(string portCode, int userId, string userType, string fromDate, string toDate);
        //anchoredcode

        List<AnchorVesselInfoGISVO> GetAnchorVesselInformationGIS(string portCode);


        GISMapPathVo GetGisMapPath(string portCode);
        List<BerthedVessels> GetBerthedVesselDetails(string portcode);
        List<SailedVessels> GetSailedVesselDetails(string portcode);
        List<AnchoredVessels> GetAnchoredVesselDetails(string portcode);

    }
}
