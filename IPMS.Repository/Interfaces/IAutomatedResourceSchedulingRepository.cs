using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;

namespace IPMS.Repository
{
    public interface IAutomatedResourceSchedulingRepository
    {
        List<VesselCallMovementVO> GetPendingMovementsForAllocation(string portCode);

        List<MovementResourceAllocationVO> GetResourceAllocations(string portCode,DateTime slotDate);

        /// <summary>
        /// Author : Sandeep Appana
        /// Date   : 15th Sep 2014
        /// Purpose: To get craft details based on ServiceType
        /// </summary>
        /// <param name="resourceSlot"></param>
        /// <param name="portCode"></param>
        /// <returns></returns>
        List<IdNameVO> GetSearchCraft(ResourceSlotVO resourceSlot, string portCode);

        //List<ResourceAllocationVO> GetResourceAllocationByDate(string portcode);

        /// <summary>
        /// Author : Sandeep Appana
        /// Date   :1st Oct  2014
        /// Purpose: To get all servicetype details
        /// </summary>
        /// <returns></returns>
        List<ServiceTypeVO> GetServiceTypes();

        /// <summary>
        /// Get service request details by service request id 
        /// </summary>
        /// <param name="serviceRequestId"></param>
        /// <returns></returns>
        VesselVO GetServiceRequestDetailsById(int serviceRequestId);


        /// <summary>
        /// Author : Omprakash Kotha
        /// Date   :28th Nov 2014
        /// Purpose: To get all Resource Calendar details
        /// </summary>
        /// <returns></returns>
        List<ResourceCalendarVO> GetResourceCalendarDetails(ResourceCalendarSearchVO objResourceCalendarSearchVO, string strPortCode);

        /// <summary>
        /// Author : Omprakash Kotha
        /// Date   :28th Nov 2014
        /// Purpose: To get all Resource Calendar details
        /// </summary>
        /// <returns></returns>
        List<ResourceCalendarVO> GetCraftCalendarDetails(ResourceCalendarSearchVO objResourceCalendarSearchVO, string strPortCode);

        /// <summary>
        /// Author : Sandeep Appana
        /// Date   :13th Jan  2015
        /// Purpose: To get all Crafts Availability ServiceTypes details
        /// </summary>
        /// <returns></returns>
        List<ServiceTypeVO> GetCraftAvailabilityServiceTypes();
        
        /// <summary>
        /// Author : Sandeep Appana
        /// Date   : 24th February 2015
        /// Purpose: To get details based on resourceAllocationId
        /// </summary>
        /// <param name="resourceAllocationId"></param>
        /// <returns></returns>
        ResourceAllocationVO GetResourceAllocationById(int resourceAllocationId);

        /// <summary>
        /// Author : Sandeep Appana
        /// Date   : 19th May 2015
        /// Purpose: To verify ServiceRequestID in Vesselcallmovement 
        /// </summary>
        /// <param name="vesselCallMovement"></param>
        /// <returns></returns>
        bool VerifyMovementIsActiveByVCNAndServiceRequestId(string vcn, int serviceRequestId);

        List<AutomatedSlotBlockingVO> GetAutomatedBlockedSlots(List<ResourceSlotVO> resourceSlots, string portCode);
    }
}
