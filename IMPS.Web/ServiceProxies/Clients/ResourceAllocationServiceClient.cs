using IPMS.Domain.ValueObjects;
using IPMS.ServiceProxies.Contracts;
using IPMS.Web.ServiceProxies;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace IPMS.ServiceProxies.Clients
{
    public class ResourceAllocationServiceClient : UserClientBase<IResourceAllocationService>, IResourceAllocationService
    {

        public List<ResourceAllocationVO> GetResourceAllocations(string vcn, string vesselName, string resourceName)
        {
            return WrapOperationWithException(() => Channel.GetResourceAllocations(vcn, vesselName, resourceName));
        }
        public List<ResourceAllocationVO> GetresourceAllocationdetailsByVCN(string vcn)
        {
            return WrapOperationWithException(() => Channel.GetresourceAllocationdetailsByVCN(vcn));
        }
        public List<OtherServiceRecordingVO> GetWaterDetailsList(string resourceAllocationID, string action)
        {
            return WrapOperationWithException(() => Channel.GetWaterDetailsList(resourceAllocationID,action));
        }

        public int CheckMeterNoExists(string meterno,int resourceAllocationID)
        {
            return WrapOperationWithException(() => Channel.CheckMeterNoExists(meterno, resourceAllocationID));
        }

       
        public ResourceAllocationVO GetResourceAllocationformDetails(ResourceAllocationVO resource)
        {
            return WrapOperationWithException(() => Channel.GetResourceAllocationformDetails(resource));
        }
        public ResourceAllocationReferenceDataVO GetResourceAllocationReferenceDataVO()
        {
            return WrapOperationWithException(() => Channel.GetResourceAllocationReferenceDataVO());
        }
        public List<BollardVO> GetBollardsInBerths(string berthkey)
        {
            return WrapOperationWithException(() => Channel.GetBollardsInBerths(berthkey));
        }

        public ResourceAllocationVO UpdateResourceAllocationformDetails(ResourceAllocationVO resource)
        {
            return WrapOperationWithException(() => Channel.UpdateResourceAllocationformDetails(resource));
        }
        //public OtherServiceRecordingVO SaveWaterAllocationDetails(OtherServiceRecordingVO resource)
        //{
        //    return WrapOperationWithException(() => Channel.SaveWaterAllocationDetails(resource));
        //}

        public ResourceAllocationVO SaveWaterAllocationDetails(ResourceAllocationVO resource)
        {
            return WrapOperationWithException(() => Channel.SaveWaterAllocationDetails(resource));
        }

        public ResourceAllocationVO ModifyResourceAllocationformDetails(ResourceAllocationVO resource)
        {
            return WrapOperationWithException(() => Channel.ModifyResourceAllocationformDetails(resource));
        }

        public ResourceAllocationVO GetresourceAllocationByResourceAllocId(string strResourceAllocationId)
        {
            return WrapOperationWithException(() => Channel.GetresourceAllocationByResourceAllocId(strResourceAllocationId));
        }
       
       
       
        /// <summary>
        /// Author : Sandeep Appana
        /// Date   : 28th April 2014
        /// Purpose: To verify Resource Allocation Details
        /// </summary>
        /// <param name="resource"></param>
        /// <returns></returns>
        public string VerifyResourceAllocationDetails(string operationType, string movementType, string resourceAllocationId)
        {
            return WrapOperationWithException(() => Channel.VerifyResourceAllocationDetails(operationType, movementType, resourceAllocationId));
        }

        public List<RevenuePostingVO> ServiceRecordingVcnDetailsforAutocomplete(string searchvalue)
        {
            return WrapOperationWithException(() => Channel.ServiceRecordingVcnDetailsforAutocomplete(searchvalue));
        }

        public List<VesselVO> ServiceRecordingVesselDetailsforAutocomplete(string searchvalue)
        {
            return WrapOperationWithException(() => Channel.ServiceRecordingVesselDetailsforAutocomplete(searchvalue));
        }

        public List<UserMasterVO> ServiceRecordingResourceDetailsforAutocomplete(string searchvalue)
        {
            return WrapOperationWithException(() => Channel.ServiceRecordingResourceDetailsforAutocomplete(searchvalue));
        }
        
    }
}