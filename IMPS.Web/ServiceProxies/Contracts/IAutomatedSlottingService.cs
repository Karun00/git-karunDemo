using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;

namespace IPMS.ServiceProxies.Contracts
{
    [ServiceContract]
    public interface IAutomatedSlottingService : IDisposable
    {
        /// <summary>
        /// To Get VesselMovementTypes
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<SubCategoryVO> GetMovementTypes();

        /// <summary>
        /// Get AutomatedSlotting  unplanned vessel
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<VesselCallMovementVO> GetUnPlannedVesselDet(DateTime slotDate);



        /// <summary>
        /// Update Slot details in VesselCallMovements
        /// </summary>
        /// <param name="slotStatus"></param>
        /// <param name="slot"></param>
        /// <param name="slotDate"></param>
        /// <param name="vesselCallmovementID"></param>
        /// <returns></returns>
        [OperationContract]
        int UpdateVesselSlotDetails(List<VesselCallMovementVO> slotDetails);
        /// <summary>
        /// Get Automated Slotting  planned vessel
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<VesselCallMovementVO> GetPlannedVesselDetails(DateTime slotDate);

        [OperationContract]
        AutomatedSlotConfigurationVO GetAutomatedConfigurationDetails(DateTime slotDate);

        [OperationContract]
        AutomatedSlotConfigurationVO GetExtendableYesNo(DateTime slotDate);

        /// <summary>
        /// Author  : Sandeep Appana  
        /// Date    : 20th February 2015
        /// Purpose : To get AutomatedSlotting Privileges 
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        bool GetPrivilegesByUserIdAndEntityCode(string entityCode);

        /// <summary>
        /// Author  : Sandeep Appana  
        /// Date    : 21st February 2015
        /// Purpose : To Update Single Vessel Slot Details  
        /// </summary>
        /// <param name="slotDetails"></param>
        /// <returns></returns>
        [OperationContract]
        bool UpdateSingleVesselSlotDetails(VesselCallMovementVO slotDetails);

        [OperationContract]
        List<string> GetActiveSlots();

        [OperationContract]
        List<AutomatedSlotBlockingVO> GetBlockedSlots(DateTime slotDate);

        [OperationContract]
        List<SubCategory> GetReasonTypes();

        

    }
}
