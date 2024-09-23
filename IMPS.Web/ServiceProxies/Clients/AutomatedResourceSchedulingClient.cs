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
    public class AutomatedResourceSchedulingClient : UserClientBase<IAutomatedResourceSchedulingService>, IAutomatedResourceSchedulingService
    {
        public List<VesselCallMovementVO> GetPendingMovementsForAllocation()
        {
            return WrapOperationWithException(() => Channel.GetPendingMovementsForAllocation());
        }

        public List<MovementResourceAllocationVO> GetResourceAllocations(DateTime slotDate)
        {
            return WrapOperationWithException(() => Channel.GetResourceAllocations(slotDate));
        }

        /// <summary>
        /// Author : Sandeep Appana
        /// Date   : 15th Sep 2014
        /// Purpose: To get craft details based on ServiceType
        /// </summary>
        /// <param name="resourceSlot"></param>
        /// <returns></returns>
        public List<IdNameVO> GetSearchCraft(ResourceSlotVO resourceSlot)
        {
            return WrapOperationWithException(() => Channel.GetSearchCraft(resourceSlot));
        }

        /// <summary>
        /// Author : Sandeep Appana
        /// Date   : 1st Oct  2014
        /// Purpose: To get all ServiceType details
        /// </summary>
        /// <returns></returns>
        public List<ServiceTypeVO> GetServiceTypes()
        {
            return WrapOperationWithException(() => Channel.GetServiceTypes());
        }

        //public List<MovementResourceAllocationVO> ScheduleMovements(List<VesselCallMovementVO> vesselMovements)
        //{
        //    return WrapOperationWithException(() => Channel.ScheduleMovements(vesselMovements));
        //}

        //-- changed by sandeep on 02-02-2015
        //public List<MovementResourceAllocationVO> ScheduleMovements(VesselCallMovementVO vesselMovements)
        public string ScheduleMovements(VesselCallMovementVO vesselMovements)
        {
            return WrapOperationWithException(() => Channel.ScheduleMovements(vesselMovements));
        }

        public List<MovementResourceAllocationVO> SaveResourceAllocations(MovementResourceAllocationVO vesselCallMovements)
        {
            return WrapOperationWithException(() => Channel.SaveResourceAllocations(vesselCallMovements));
        }

        /// <summary>
        /// Author : Omprakash Kotha
        /// Date   : 25th Nov  2014
        /// Purpose: To get all Shifts details
        /// </summary>
        /// <returns></returns>
        public List<ShiftVO> GetShiftDetails()
        {
            return WrapOperationWithException(() => Channel.GetShiftDetails());
        }

        /// <summary>
        /// Author : Omprakash Kotha
        /// Date   : 28th Nov  2014
        /// Purpose: To get all Resource Calendar details
        /// </summary>
        /// <returns></returns>
        public List<ResourceCalendarVO> GetResourceCalendarDetails(ResourceCalendarSearchVO objResourceCalendarSearch)
        {
            return WrapOperationWithException(() => Channel.GetResourceCalendarDetails(objResourceCalendarSearch));
        }

        public List<ResourceCalendarVO> GetCraftCalendarDetails(ResourceCalendarSearchVO objResourceCalendarSearchVO)
        {
            return WrapOperationWithException(() => Channel.GetCraftCalendarDetails(objResourceCalendarSearchVO));
        }

        public List<ServiceTypeVO> GetCraftAvailabilityServiceTypes()
        {
            return WrapOperationWithException(() => Channel.GetCraftAvailabilityServiceTypes());
        }

        public bool VerifyMovementIsActiveByVCNAndServiceRequestId(string vcn, int serviceRequestId)
        {
            return WrapOperationWithException(() => Channel.VerifyMovementIsActiveByVCNAndServiceRequestId(vcn, serviceRequestId));
        }
    }
}