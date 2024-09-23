using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;

namespace IPMS.Repository
{
    public interface IAutomatedSlottingRepository
    {
        List<VesselCallMovementVO> GetUnPlannedVesselDetails(DateTime slotDate, string portCode, int userId, string userType);

        List<VesselCallMovementVO> GetPlannedVesselDetails(DateTime slotDate, string portCode, int userId, string userType);

        AutomatedSlotConfigurationVO GetAutomatedConfigurationDetails(DateTime slotDate, string portCode);

        AutomatedSlotConfigurationVO GetExtendableYesNo(string portCode, DateTime slotDate);

        int UpdateVesselSlotDetails(List<VesselCallMovementVO> slotDetails);

        string GetSlotPeriodBySlotDate(DateTime slotDateTime, string port);

        VesselCallMovementVO GetVesselCallMovementId(string valVesselCallMovementId);

        VesselCallMovementVO GetVesselCallMovementVORep(string strVesselCallMovementId);

        /// <summary>
        /// Author  : Sandeep Appana  
        /// Date    : 20th February 2015
        /// Purpose : To get AutomatedSlotting Privileges by UserID  
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="portcode"></param>
        /// <returns></returns>
        bool GetPrivilegesByUserIdAndEntityCode(int userId, string entityCode);

        List<string> GetActiveSlots(string portCode);

        List<AutomatedSlotBlockingVO> GetBlockedSlots(DateTime slotDate, string portCode);

        //string GetPreviousOverriddenSlot(int vesselCallMovementID);
        SlotOverRidingReasonsVO GetPreviousOverriddenSlot(int vesselCallMovementID);
       // string GetPreviousPlannedSlot(int vesselCallMovementID);
        VesselCallMovementVO GetPreviousPlannedSlot(int vesselCallMovementID);

        
    }
}
