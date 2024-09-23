using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;

namespace IPMS.ServiceProxies.Contracts
{
    [ServiceContract]
    public interface IAutomatedResourceSchedulingService : IDisposable
    {
        /// <summary>
        /// To Get Confirmed Service Requests
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<VesselCallMovementVO> GetPendingMovementsForAllocation();

        /// <summary>
        /// To Get Resource Allocation Slots Details
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<MovementResourceAllocationVO> GetResourceAllocations(DateTime slotDate);

        [OperationContract]
        //List<MovementResourceAllocationVO> ScheduleMovements(List<VesselCallMovementVO> vesselMovements);
        //-- changed by sandeep on 02-02-2015
        //List<MovementResourceAllocationVO> ScheduleMovements(VesselCallMovementVO vesselMovements);
        string ScheduleMovements(VesselCallMovementVO vesselMovements);

        [OperationContract]
        List<MovementResourceAllocationVO> SaveResourceAllocations(MovementResourceAllocationVO vesselCallMovements);

        /// <summary>
        /// Author : Sandeep Appana
        /// Date   : 15th Sep 2014
        /// Purpose: To get craft details based on ServiceType
        /// </summary>
        /// <param name="resourceSlot"></param>
        /// <returns></returns>
        [OperationContract]
        List<IdNameVO> GetSearchCraft(ResourceSlotVO resourceSlot);

        /// <summary>
        /// Author : Sandeep Appana
        /// Date   : 1st Oct  2014
        /// Purpose: To get all ServiceType details
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<ServiceTypeVO> GetServiceTypes();

        /// <summary>
        /// Author : Omprakash kotha
        /// Date   : 25th Nov  2014
        /// Purpose: To get all Shfts details
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<ShiftVO> GetShiftDetails();


        /// <summary>
        /// Author : Omprakash kotha
        /// Date   : 28th Nov  2014
        /// Purpose: To get all Resource Calendar details
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<ResourceCalendarVO> GetResourceCalendarDetails(ResourceCalendarSearchVO objResourceCalendarSearch);
        [OperationContract]
        List<ResourceCalendarVO> GetCraftCalendarDetails(ResourceCalendarSearchVO objResourceCalendarSearchVO);

        /// <summary>
        /// Author : Sandeep Appana
        /// Date   :13th Jan  2015
        /// Purpose: To get all Crafts Availability ServiceTypes details
        /// </summary>
        /// <returns></returns>
        [OperationContract]        
        List<ServiceTypeVO> GetCraftAvailabilityServiceTypes();

        /// <summary>
        /// Author : Sandeep Appana
        /// Date   : 19th May 2015
        /// Purpose: To verify movementtype is active by vcn and servicerequestid in Vesselcallmovement 
        /// </summary>
        /// <param name="vcn"></param>
        /// <param name="serviceRequestId"></param>
        /// <returns></returns>
        [OperationContract]
        bool VerifyMovementIsActiveByVCNAndServiceRequestId(string vcn, int serviceRequestId);
    }
}
