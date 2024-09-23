using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace IPMS.Services
{
    [ServiceContract]
    public interface IBerthPreSchedulingService
    {
        [OperationContract]
        [FaultContract(typeof(Exception))]
        BerthPreSchedulingReferenceVO GetBerthPreSchedulingReferenceVO();

        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<VCMData> GetVesselCallMovements(string agentId, string eta, string etd, string vesselType, string reasonforVisit, string cargoType, string movementStatus);

        /// <summary>
        /// To Get Reference Data For Berth Planning Table View
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        BerthPlanningTableReferenceVO GetBerthPlanningTableReferenceVO();

        /// <summary>
        ///  To Get VCM Details For Berth Planning Table View
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<VCMTableData> GetVesselCallMovementsTable(string quayCode, string berthCode, string vesselStatus, string eta, string toDate);


        /// <summary>
        /// To Get Suitable Berths
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<SuitableBerthVO> GetSuitableBerths(string vcn, string etb, string etub, string vesselCallMovementId);


        [OperationContract]
        [FaultContract(typeof(Exception))]
        string ModifyBerthPreScheduling(BerthPreSchedulingVO data);      
        
      
        
    }
}
