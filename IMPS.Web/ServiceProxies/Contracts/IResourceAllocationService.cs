using IPMS.Domain.Models;
using System.Collections.Generic;
using System.ServiceModel;
using System;
using IPMS.Domain.ValueObjects;
using System.Threading.Tasks;

namespace IPMS.ServiceProxies.Contracts
{
    [ServiceContract]
    public interface IResourceAllocationService : IDisposable
    {
        [OperationContract]
        List<ResourceAllocationVO> GetResourceAllocations(string vcn, string vesselName, string resourceName);
        [OperationContract]
        List<ResourceAllocationVO> GetresourceAllocationdetailsByVCN(string vcn);
        [OperationContract]
        List<OtherServiceRecordingVO> GetWaterDetailsList(string resourceAllocationID, string action);

        [OperationContract]
        ResourceAllocationVO GetResourceAllocationformDetails(ResourceAllocationVO resource);
        [OperationContract]
        ResourceAllocationReferenceDataVO GetResourceAllocationReferenceDataVO();
        [OperationContract]
        List<BollardVO> GetBollardsInBerths(string berthkey);
        [OperationContract]
        ResourceAllocationVO UpdateResourceAllocationformDetails(ResourceAllocationVO resource);
        [OperationContract]
        ResourceAllocationVO ModifyResourceAllocationformDetails(ResourceAllocationVO resource);
        [OperationContract]
        ResourceAllocationVO GetresourceAllocationByResourceAllocId(string strResourceAllocationId);

        [OperationContract]
        int CheckMeterNoExists(string meterno,int resourceAllocationID);

     
        [OperationContract]
        ResourceAllocationVO SaveWaterAllocationDetails(ResourceAllocationVO resource);
        /// <summary>
        /// Author : Sandeep Appana
        /// Date   : 28th April 2014
        /// Purpose: To verify Resource Allocation Details
        /// </summary>
        /// <param name="resource"></param>
        /// <returns></returns>
        [OperationContract]
        string VerifyResourceAllocationDetails(string operationType, string movementType, string resourceAllocationId);




        [OperationContract]
        List<RevenuePostingVO> ServiceRecordingVcnDetailsforAutocomplete(string searchvalue);

        [OperationContract]
        List<VesselVO> ServiceRecordingVesselDetailsforAutocomplete(string searchvalue);

        [OperationContract]
        List<UserMasterVO> ServiceRecordingResourceDetailsforAutocomplete(string searchvalue);
    }
}
