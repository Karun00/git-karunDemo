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
    public interface IAutomatedSlottingService
    {
        [OperationContract]
        [FaultContract(typeof(Exception))]
        VesselCallMovementVO GetVesselCallMovementVO(string strVesselCallMovementId);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<SubCategoryVO> GetMovementTypes();

        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<VesselCallMovementVO> GetUnPlannedVesselDet(DateTime slotDate);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        int UpdateVesselSlotDetails(List<VesselCallMovementVO> slotDetails);


        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<VesselCallMovementVO> GetPlannedVesselDetails(DateTime slotDate);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        AutomatedSlotConfigurationVO GetAutomatedConfigurationDetails(DateTime slotDate);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        AutomatedSlotConfigurationVO GetExtendableYesNo(DateTime slotDate);

        /// <summary>
        /// Author  : Sandeep Appana  
        /// Date    : 20th February 2015
        /// Purpose : To get AutomatedSlotting Role Privileges by UserID 
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        bool GetPrivilegesByUserIdAndEntityCode(string entityCode);

        /// <summary>
        /// Author  : Sandeep Appana  
        /// Date    : 21st February 2015
        /// Purpose : To Update Single Vessel Slot Details  
        /// </summary>
        /// <param name="slotDetails"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        bool UpdateSingleVesselSlotDetails(VesselCallMovementVO slotDetails);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<string> GetActiveSlots();

        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<AutomatedSlotBlockingVO> GetBlockedSlots(DateTime slotDate);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<SubCategory> GetReasonTypes();



    }
}
