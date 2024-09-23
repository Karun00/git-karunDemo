using System.ServiceModel;
using System;
using System.Collections.Generic;
using IPMS.Domain.ValueObjects;

namespace IPMS.Services
{
    [ServiceContract]
    public interface ISuppServiceResourceAllocService
    {
        /// <summary>
        /// Author : Sandeep Appana
        /// Date   : 15th Sep 2014
        /// Purpose: To get all approved water service details
        /// </summary>
        /// <param name="vcn"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<SuppServiceRequestVO> GetApprovedWaterService(string vcn, string date);

        /// <summary>
        /// Author : Sandeep Appana
        /// Date   : 15th Sep 2014
        /// Purpose: To get user details based on ServiceType
        /// </summary>
        /// <param name="ResourceSlot"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<IdNameVO> GetSearchResource(ResourceSlotVO ResourceSlot);


        /// <summary>
        /// Author  : Sandeep Appana
        /// Date    : 17th Sep 2014
        /// Purpose : To get list of resources based on date and portcode
        /// </summary>
        /// <param name="date"></param>
        /// <param name="portcode"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<ResourceAllocationSlotVO> GetSupplementaryResourceAllocationByDate(DateTime date, string vcn);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<ResourceSlotVO> GetSlotConfiguration(DateTime date);

        ///// <summary>
        ///// Author  : Sandeep Appana
        ///// Date    : 22nd Sep 2014
        ///// Purpose : To get list of resources based on VCN, date and portcode
        ///// </summary>
        ///// <param name="vcn"></param>
        ///// <param name="date"></param>
        ///// <param name="portcode"></param>
        ///// <returns></returns>
        //[OperationContract]
        //[FaultContract(typeof(Exception))]
        //List<ResourceAllocationVO> GetSupplementaryResourceAllocationByVCNAndDate(string vcn, string date);

        /// <summary>
        /// Author  : Sandeep Appana
        /// Date    : 22nd Sep 2014
        /// Purpose : To Post data into the service
        /// </summary>
        /// <param name="ResourceAllocationSlotVOs"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<ResourceAllocationSlotVO> PostSupplementaryResourceAllocation(List<ResourceAllocationSlotVO> ResourceAllocationSlotVOs);

        /// <summary>
        /// Author  : Sandeep Appana
        /// Date    : 29th Dec 2014
        /// Purpose : To get PortName by PortCode
        /// </summary>
        /// <param name="portcode"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        string GetPortNameByPortCode();

        /// <summary>
        /// Author  : Sandeep Appana
        /// Date    : 6th jan 2015
        /// Purpose : To get Resource Allocation Details by ResourceAllocationID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ResourceAllocationVO GetResourceAllocationByID(int resourceAllocationId);

        /// <summary>
        /// Author  : Sandeep Appana
        /// Date    : 12th Feb 2015
        /// Purpose : To get all VCN based on portcode, servicetype and date 
        /// </summary>
        /// <param name="portcode"></param>
        /// <param name="servicetype"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<String> GetVCNDetails(DateTime date);

        /// <summary>
        /// Author  : Sandeep Appana
        /// Date    : 30th Mar 2015
        /// Purpose : To Save data into the database
        /// </summary>
        /// <param name="ResourceAllocationSlotVO"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        ResourceAllocationSlotVO UpdateResourceAllocation(ResourceAllocationSlotVO resourceAllocationSlotData);

        /// <summary>
        /// Author  : Sandeep Appana
        /// Date    : 30th Mar 2015
        /// Purpose : To get all active slots
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<ResourceSlotVO> GetActiveResourceSlots();

        /// <summary>
        /// Author  : Sandeep Appana
        /// Date    : 7th July 2015
        /// Purpose : To update resource allocation data into the database
        /// </summary>
        /// <param name="resourceAllocationId"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        string UpdateResourceAllocationSlotById(string resourceAllocationId);

    }
}
