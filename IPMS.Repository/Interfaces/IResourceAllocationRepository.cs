using System;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System.Collections.Generic;

namespace IPMS.Repository
{
    public interface IResourceAllocationRepository
    {
        List<ResourceAllocationVO> GetResourceAllocationDetails(string portCode, string vcn, string vesselName, string resourceName);

        List<ResourceAllocationVO> GetResourceAllocationDetailsByVCN(string portCode, string vcn);
        List<OtherServiceRecordingVO> GetWaterDetailsList(string resourceAllocationID,string action);

        ResourceAllocation GetResourceAloocationFormDetails(ResourceAllocation resourcedtls);

       

        List<ResourceAllocationVO> GetResourceAllocationByDate(string date, string portCode);

        List<ResourceAllocationVO> GetResourcesByDateAndServiceReferenceType(DateTime date, string serviceReferenceType);

        ResourceAllocationVO GetResourceAllocationFormDetails(ResourceAllocationVO resource);
    

       

        ResourceAllocationVO UpdateResourceAllocationFormDetails(ResourceAllocationVO resource, int userId, string portCode);

        ResourceAllocationVO SaveWaterAllocationDetails(ResourceAllocationVO resource, int userId, string portCode);

        int CheckMeterNoExists(string meterno, int resourceAllocationID);

        ResourceAllocationVO ModifyResourceAllocationFormDetails(ResourceAllocationVO resource, int userId, string portCode);

        ResourceAllocationVO GetResourceAllocationByResourceAllocationId(string portCode, string strResourceAllocationId);

      
        
        /// <summary>
        /// Author : Sandeep Appana
        /// Date   : 28th April 2014
        /// Purpose: To verify Resource Allocation Details
        /// </summary>
        /// <param name="resource"></param>
        /// <returns></returns>
        string VerifyResourceAllocationDetails(string operationType, string movementType, string resourceAllocationId);
        /// <summary>
        /// Srini - 
        /// Adv Search for VCN Auto complete
        /// </summary>
        /// <param name="searchValue"></param>
        /// <param name="portCode"></param>
        /// <returns></returns>
        List<RevenuePostingVO> ServiceRecordingVcnDetailsforAutocomplete(string searchValue, string portCode);
        /// <summary>
        /// Srini
        /// Adv search for Vessel auto complete
        /// </summary>
        /// <param name="PortCode"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        List<VesselVO> ServiceRecordingVesselDetailsforAutocomplete(string PortCode, string searchValue);
        /// <summary>
        /// Srini
        /// Adv search for Vessel auto complete
        /// </summary>
        /// <param name="PortCode"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        List<UserMasterVO> ServiceRecordingResourceDetailsforAutocomplete(string PortCode, string searchValue);
        

        ServiceRecordingVO GetServiceRecordingDetails(string resourceallocationid);

        ServiceRecordingVO GetServiceRecordingDetailsForMobile(int resourceallocationid);

        List<UserMasterVO> GetReourceNamesByType(string portCode, string designation);

    }
}
