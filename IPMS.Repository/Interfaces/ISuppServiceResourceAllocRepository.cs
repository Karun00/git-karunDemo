using System.Collections.Generic;
using IPMS.Domain.ValueObjects;
using System;

namespace IPMS.Repository
{
    public interface ISuppServiceResourceAllocRepository
    {
        /// <summary>
        /// Author : Sandeep Appana
        /// Date   : 15th Sep 2014
        /// Purpose: To get user details based on ServiceType
        /// </summary>
        /// <param name="ResourceSlot"></param>
        /// <param name="portcode"></param>
        /// <returns></returns>
        List<IdNameVO> GetSearchResource(ResourceSlotVO ResourceSlot, string portcode);

        /// <summary>
        /// Author  : Sandeep Appana
        /// Date    : 17th Sep 2014
        /// Purpose : To get list of resources based on date and portcode
        /// </summary>
        /// <param name="date"></param>
        /// <param name="portcode"></param>
        /// <returns></returns>
        List<ResourceAllocationSlotVO> GetSupplementaryResourceAllocationByDate(DateTime date, string portcode, string vcn,string servicetypecode);

        List<ResourceSlotVO> GetSlotConfiguration(DateTime date, string portcode);

        /// <summary>
        /// Author  : Sandeep Appana
        /// Date    : 29th Dec 2014
        /// Purpose : To get PortName by PortCode
        /// </summary>
        /// <param name="portcode"></param>
        /// <returns></returns>
        string GetPortNameByPortCode(string portcode);

        /// <summary>
        /// Author  : Sandeep Appana
        /// Date    : 6th jan 2015
        /// Purpose : To get Resource Allocation Details by ResourceAllocationID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ResourceAllocationVO GetResourceAllocationByID(int resourceAllocationId);

        string GetRoleByUser(int userid, int FloatingCraneRoleID, int WaterManRoleID);

        /// <summary>
        /// Author  : Sandeep Appana
        /// Date    : 12th Feb 2015
        /// Purpose : To get all VCN based on portcode, servicetype and date 
        /// </summary>
        /// <param name="portcode"></param>
        /// <param name="servicetype"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        List<String> GetVCNDetails(string portcode, string servicetype, DateTime date);

        /// <summary>
        /// Author  : Sandeep Appana
        /// Date    : 30th Mar 2015
        /// Purpose : To get all active slots
        /// </summary>
        /// <returns></returns>
        List<ResourceSlotVO> GetActiveResourceSlots(string portcode);

    }
}
