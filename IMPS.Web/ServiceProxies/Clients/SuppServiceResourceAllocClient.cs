using IPMS.ServiceProxies.Contracts;
using System.Collections.Generic;
using IPMS.Domain.ValueObjects;
using IPMS.Web.ServiceProxies;
using System;

namespace IPMS.ServiceProxies.Clients
{
    public class SuppServiceResourceAllocClient : UserClientBase<ISuppServiceResourceAllocService>, ISuppServiceResourceAllocService
    {
        public List<SuppServiceRequestVO> GetApprovedWaterService(string vcn, string date)
        {
            return WrapOperationWithException(() => Channel.GetApprovedWaterService(vcn, date));
        }

        public List<IdNameVO> GetSearchResource(ResourceSlotVO ResourceSlot)
        {
            return WrapOperationWithException(() => Channel.GetSearchResource(ResourceSlot));
        }

        public List<ResourceAllocationSlotVO> GetSupplementaryResourceAllocationByDate(DateTime date, string vcn)
        {
            return WrapOperationWithException(() => Channel.GetSupplementaryResourceAllocationByDate(date, vcn));
        }

        //public List<ResourceAllocationVO> GetSupplementaryResourceAllocationByVCNAndDate(string vcn, string date)
        //{
        //    return WrapOperationWithException(() => Channel.GetSupplementaryResourceAllocationByVCNAndDate(vcn, date));
        //}

        public List<ResourceSlotVO> GetSlotConfiguration(DateTime date)
        {
            return WrapOperationWithException(() => Channel.GetSlotConfiguration(date));
        }

        public List<ResourceAllocationSlotVO> PostSupplementaryResourceAllocation(List<ResourceAllocationSlotVO> ResourceAllocationSlotVOs)
        {
            return WrapOperationWithException(() => Channel.PostSupplementaryResourceAllocation(ResourceAllocationSlotVOs));
        }

        public string GetPortNameByPortCode()
        {
            return WrapOperationWithException(() => Channel.GetPortNameByPortCode());
        }

        /// <summary>
        /// Author  : Sandeep Appana
        /// Date    : 12th Feb 2015
        /// Purpose : To get all VCN based on portcode, servicetype and date 
        /// </summary>
        /// <param name="portcode"></param>
        /// <param name="servicetype"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public List<String> GetVCNDetails(DateTime date)
        {
            return WrapOperationWithException(() => Channel.GetVCNDetails(date));
        }

        public ResourceAllocationSlotVO UpdateResourceAllocation(ResourceAllocationSlotVO resourceAllocationSlotData)
        {
            return WrapOperationWithException(() => Channel.UpdateResourceAllocation(resourceAllocationSlotData));
        }

        public List<ResourceSlotVO> GetActiveResourceSlots()
        {
            return WrapOperationWithException(() => Channel.GetActiveResourceSlots());
        }

        public string UpdateResourceAllocationSlotById(string resourceAllocationId)
        {
            return WrapOperationWithException(() => Channel.UpdateResourceAllocationSlotById(resourceAllocationId));
        }
    }
}