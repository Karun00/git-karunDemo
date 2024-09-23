using System;
using System.Collections.Generic;
using System.Web;
using System.ServiceModel;
using IPMS.ServiceProxies.Contracts;
using IPMS.Domain.Models;
using System.Threading.Tasks;
using IPMS.Web.ServiceProxies;
using IPMS.Domain.ValueObjects;

namespace IPMS.ServiceProxies.Clients
{
    public class AutomatedSlottingClient : UserClientBase<IAutomatedSlottingService>, IAutomatedSlottingService
    {
        /// <summary>
        /// To Get VesselMovementTypes
        /// </summary>
        /// <returns></returns>
        public List<SubCategoryVO> GetMovementTypes()
        {
            return WrapOperationWithException(() => Channel.GetMovementTypes());
        }

        public List<VesselCallMovementVO> GetUnPlannedVesselDet(DateTime slotDate)
        {
            return WrapOperationWithException(() => Channel.GetUnPlannedVesselDet(slotDate));
        }

        public int UpdateVesselSlotDetails(List<VesselCallMovementVO> slotDetails)
        {
            return WrapOperationWithException(() => Channel.UpdateVesselSlotDetails(slotDetails));
        }

        public List<VesselCallMovementVO> GetPlannedVesselDetails(DateTime slotDate)
        {
            return WrapOperationWithException(() => Channel.GetPlannedVesselDetails(slotDate));
        }

        public AutomatedSlotConfigurationVO GetAutomatedConfigurationDetails(DateTime slotDate)
        {
            return WrapOperationWithException(() => Channel.GetAutomatedConfigurationDetails(slotDate));
        }

        public AutomatedSlotConfigurationVO GetExtendableYesNo(DateTime slotDate)
        {
            return WrapOperationWithException(() => Channel.GetExtendableYesNo(slotDate));
        }

        public bool GetPrivilegesByUserIdAndEntityCode(string entityCode)
        {
            return WrapOperationWithException(() => Channel.GetPrivilegesByUserIdAndEntityCode(entityCode));
        }

        public bool UpdateSingleVesselSlotDetails(VesselCallMovementVO slotDetails)
        {
            return WrapOperationWithException(() => Channel.UpdateSingleVesselSlotDetails(slotDetails));
        }

        public List<string> GetActiveSlots()
        {
            return WrapOperationWithException(() => Channel.GetActiveSlots());
        }
        public List<AutomatedSlotBlockingVO> GetBlockedSlots(DateTime slotDate)
        {
            return WrapOperationWithException(() => Channel.GetBlockedSlots(slotDate));
        }

        public List<SubCategory> GetReasonTypes()
        {
            return WrapOperationWithException(() => Channel.GetReasonTypes());
        }
      
    }
}