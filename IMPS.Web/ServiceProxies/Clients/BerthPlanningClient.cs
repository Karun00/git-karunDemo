using IPMS.Domain.ValueObjects;
using IPMS.Web.ServiceProxies.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace IPMS.Web.ServiceProxies.Clients
{
    public class BerthPlanningClient : UserClientBase<IBerthPlanningService>, IBerthPlanningService
    {
        public List<QuayVO> GetQuaysInPort()
        {

            return WrapOperationWithException(() => Channel.GetQuaysInPort());
        }

        public List<BerthsData> GetBerthsWithBollard(string quayCode)
        {
            return WrapOperationWithException(() => Channel.GetBerthsWithBollard(quayCode));
        }

        public List<QuayBerthBollardData> GetQuayBerthsBollard()
        {
            return WrapOperationWithException(() => Channel.GetQuayBerthsBollard());
        }



        public Boolean CheckBerthAvailability(string vcn, string vcmId, string quayCode, string fromBerthCode, string fromBollardMeter, string toBerthCode, string toBollardMeter, string fromTime, string toTime)
        {
            return WrapOperationWithException(() => Channel.CheckBerthAvailability(vcn, vcmId, quayCode, fromBerthCode, fromBollardMeter, toBerthCode, toBollardMeter, fromTime, toTime));


        }


        public List<BerthPlanningVO> GetVesselInformation(string fromDate, string toDate)
        {
            return WrapOperationWithException(() => Channel.GetVesselInformation(fromDate, toDate));
        }

        public List<BerthPlanningVO> SaveVesselCallMovements(List<BerthPlanningVO> plannedVessels)
        {
            return WrapOperationWithException(() => Channel.SaveVesselCallMovements(plannedVessels));
        }


        public List<BerthMaintenanceData> GetBerthMaintenance(string fromDate, string toDate)
        {
            return WrapOperationWithException(() => Channel.GetBerthMaintenance(fromDate, toDate));
        }

        public UserData GetUserDetails()
        {
            return WrapOperationWithException(() => Channel.GetUserDetails());
        }

        public List<BerthPlanningConfiguration> GetBerthPlanningConfigurations()
        {
            return WrapOperationWithException(() => Channel.GetBerthPlanningConfigurations());
        }
        public List<BerthedVessels> GetBerthedVesselDetails(string portcode)
        {
            return WrapOperationWithException(() => Channel.GetBerthedVesselDetails(portcode));
        }
        public List<SailedVessels> GetSailedVesselDetails(string portcode)
        {
            return WrapOperationWithException(() => Channel.GetSailedVesselDetails(portcode));
        }
        public List<AnchoredVessels> GetAnchoredVesselDetails(string portcode)
        {
            return WrapOperationWithException(() => Channel.GetAnchoredVesselDetails(portcode));
        }



        //public Task<List<QuayVO>> GetQuaysInPortAsync()
        //{

        //    return WrapOperationWithException(() => Channel.GetQuaysInPortAsync());
        //}

        //public Task<List<BerthsData>> GetBerthsWithBollardAsync(string quayCode)
        //{
        //    return WrapOperationWithException(() => Channel.GetBerthsWithBollardAsync(quayCode));
        //}

        //public Task<List<BerthPlanningVO>> GetVesselInformationAsync(string fromDate, string toDate)
        //{
        //    return WrapOperationWithException(() => Channel.GetVesselInformationAsync(fromDate, toDate));
        //}
        //public Task<List<BerthPlanningVO>> SaveVesselCallMovementsAsync(List<BerthPlanningVO> plannedVessels)
        //{
        //    return WrapOperationWithException(() => Channel.SaveVesselCallMovementsAsync(plannedVessels));
        //}

        public GISMapPathVo GetGisMapPath( string portcodegis)
        {
            return WrapOperationWithException(() => Channel.GetGisMapPath(portcodegis));
        }

        public List<BerthPlanningVO> GetVesselInformationGIS(string portcode,string fromDate, string toDate)
        {
            return WrapOperationWithException(() => Channel.GetVesselInformationGIS(portcode,fromDate, toDate));
        }

        //Anchored code

        public List<AnchorVesselInfoGISVO> GetAnchorVesselInformationGIS(string portcode)
        {
            return WrapOperationWithException(() => Channel.GetAnchorVesselInformationGIS(portcode));
        }
    }
}