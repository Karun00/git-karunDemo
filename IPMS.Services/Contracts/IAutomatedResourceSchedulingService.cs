using System;
using Core.Repository.Providers.EntityFramework;
using Core.Repository;
using IPMS.Domain.Models;
using IPMS.Data.Context;
using System.Collections.Generic;
using System.ServiceModel;
using IPMS.Domain.ValueObjects;

namespace IPMS.Services
{
    [ServiceContract]
    public interface IAutomatedResourceSchedulingService
    {
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<VesselCallMovementVO> GetPendingMovementsForAllocation();

        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<MovementResourceAllocationVO> GetResourceAllocations(DateTime slotDate);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        //List<MovementResourceAllocationVO> ScheduleMovements(List<VesselCallMovementVO> vesselMovements);

        //-- changed by sandeep on 02-02-2015
        //List<MovementResourceAllocationVO> ScheduleMovements(VesselCallMovementVO vesselMovements);
        string ScheduleMovements(VesselCallMovementVO vesselMovements);
        //-- end

        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<MovementResourceAllocationVO> SaveResourceAllocations(MovementResourceAllocationVO vesselCallMovements);



        /// <summary>
        /// Author : Sandeep Appana
        /// Date   : 15th Sep 2014
        /// Purpose: To get craft details based on ServiceType
        /// </summary>
        /// <param name="resourceSlot"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<IdNameVO> GetSearchCraft(ResourceSlotVO resourceSlot);

        /// <summary>
        /// Author : Sandeep Appana
        /// Date   : 1st Oct  2014
        /// Purpose: To get all ServiceType details
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<ServiceTypeVO> GetServiceTypes();

        /// <summary>
        /// Author : Omprakash Kotha
        /// Date   : 25st Nov  2014
        /// Purpose: To get all Shifts details
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<ShiftVO> GetShiftDetails();


        /// <summary>
        /// Author : Omprakash Kotha
        /// Date   : 28th Nov  2014
        /// Purpose: To get all Resource Calendar details
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<ResourceCalendarVO> GetResourceCalendarDetails(ResourceCalendarSearchVO objResourceCalendarSearch);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<ResourceCalendarVO> GetCraftCalendarDetails(ResourceCalendarSearchVO objResourceCalendarSearchVO);

        /// <summary>
        /// Author : Sandeep Appana
        /// Date   :13th Jan  2015
        /// Purpose: To get all Crafts Availability ServiceTypes details
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
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
        [OperationContract]
        [FaultContract(typeof(Exception))]
        bool VerifyMovementIsActiveByVCNAndServiceRequestId(string vcn, int serviceRequestId);
    }
}
