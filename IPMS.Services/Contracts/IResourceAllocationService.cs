using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using IPMS.Domain.ValueObjects;

namespace IPMS.Services
{
    [ServiceContract]
    public interface IResourceAllocationService
    {
        /// <summary>
        /// To get ArrivalNotification Details Based on VCN
        /// </summary>
        /// <param name="vcn"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<ResourceAllocationVO> GetResourceAllocations(string vcn, string vesselName, string resourceName);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<ResourceAllocationVO> GetresourceAllocationdetailsByVCN(string vcn);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<OtherServiceRecordingVO> GetWaterDetailsList(string resourceAllocationID, string action);
        

        [OperationContract]
        [FaultContract(typeof(Exception))]
        ResourceAllocationVO GetResourceAllocationformDetails(ResourceAllocationVO resource);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        ResourceAllocationReferenceDataVO GetResourceAllocationReferenceDataVO();

        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<BollardVO> GetBollardsInBerths(string berthkey);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        ResourceAllocationVO UpdateResourceAllocationformDetails(ResourceAllocationVO resource);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        ResourceAllocationVO SaveWaterAllocationDetails(ResourceAllocationVO resource);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        int CheckMeterNoExists(string meterno, int resourceAllocationID);
       
        [OperationContract]
        [FaultContract(typeof(Exception))]
        ResourceAllocationVO ModifyResourceAllocationformDetails(ResourceAllocationVO resource);

        /// <summary>
        /// Author  : Sandeep Appana
        /// Date    : 17th Sep 2014
        /// Purpose : To get list of water resources based on date adn port code
        /// </summary>
        /// <param name="date"></param>
        /// <param name="portcode"></param>
        /// <returns></returns>
        List<ResourceAllocationVO> GetResourceAllocationByDate(string date, string portcode);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        ResourceAllocationVO GetresourceAllocationByResourceAllocId(string strResourceAllocationId);

      

      

        

        /// <summary>
        /// Author : Sandeep Appana
        /// Date   : 28th April 2014
        /// Purpose: To verify Resource Allocation Details
        /// </summary>
        /// <param name="resource"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        string VerifyResourceAllocationDetails(string operationType, string movementType, string resourceAllocationId);
       
        /// <summary>
        ///  /// Srini
        /// Adv search for VCN auto complete
        /// </summary>
        /// <param name="searchvalue"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<RevenuePostingVO> ServiceRecordingVcnDetailsforAutocomplete(string searchvalue);


        /// <summary>
        ///  /// Srini
        /// Adv search for Vessel auto complete
        /// </summary>
        /// <param name="searchvalue"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<VesselVO> ServiceRecordingVesselDetailsforAutocomplete(string searchvalue);
        /// <summary>
        ///  /// Srini
        /// Adv search for Vessel auto complete
        /// </summary>
        /// <param name="searchvalue"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<UserMasterVO> ServiceRecordingResourceDetailsforAutocomplete(string searchvalue);

        
    }
}
