using IPMS.Domain.ValueObjects;
using IPMS.ServiceProxies.Contracts;
using IPMS.Web.ServiceProxies;
using System.Collections.Generic;

namespace IPMS.ServiceProxies.Clients
{
    public class BerthPreSchedulingClient : UserClientBase<IBerthPreSchedulingService>, IBerthPreSchedulingService
    {

        public BerthPreSchedulingReferenceVO GetBerthPreSchedulingReferenceVO()
        {
            return WrapOperationWithException(() => Channel.GetBerthPreSchedulingReferenceVO());
        }


        public List<VCMData> GetVesselCallMovements(string agentId, string eta, string etd, string vesselType, string reasonforVisit, string cargoType, string movementStatus)
        {
            return WrapOperationWithException(() => Channel.GetVesselCallMovements(agentId, eta, etd, vesselType, reasonforVisit, cargoType, movementStatus));

        }

        public List<SuitableBerthVO> GetSuitableBerths(string vcn, string etb, string etub, string vesselCallMovementId)
        {
            return WrapOperationWithException(() => Channel.GetSuitableBerths(vcn, etb, etub, vesselCallMovementId));
        }

        public BerthPlanningTableReferenceVO GetBerthPlanningTableReferenceVO()
        {
            return WrapOperationWithException(() => Channel.GetBerthPlanningTableReferenceVO());
        }


        public List<VCMTableData> GetVesselCallMovementsTable(string quayCode, string berthCode, string vesselStatus, string eta, string toDate)
        {
            return WrapOperationWithException(() => Channel.GetVesselCallMovementsTable(quayCode, berthCode, vesselStatus, eta, toDate));

        }

        public string ModifyBerthPreScheduling(BerthPreSchedulingVO data)
        {
            return WrapOperationWithException(() => Channel.ModifyBerthPreScheduling(data));
        }


    }
}