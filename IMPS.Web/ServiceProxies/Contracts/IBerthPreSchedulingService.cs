using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace IPMS.ServiceProxies.Contracts
{
    [ServiceContract]
    public interface IBerthPreSchedulingService : IDisposable
    {
        [OperationContract]
        BerthPreSchedulingReferenceVO GetBerthPreSchedulingReferenceVO();

        [OperationContract]
        List<VCMData> GetVesselCallMovements(string agentId, string eta, string etd,string vesselType, string reasonforVisit, string cargoType, string movementStatus);


        [OperationContract]
        List<SuitableBerthVO> GetSuitableBerths(string vcn, string etb, string etub, string vesselCallMovementId);

        /// <summary>
        /// To Get Reference Data For Berth Planning Table View
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        BerthPlanningTableReferenceVO GetBerthPlanningTableReferenceVO();

        /// <summary>
        ///  To Get Reference VCM List For Berth Planning Table View
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<VCMTableData> GetVesselCallMovementsTable(string quayCode, string berthCode, string vesselStatus, string eta, string toDate);

                

        [OperationContract]
        string ModifyBerthPreScheduling(BerthPreSchedulingVO data);


    }
}
