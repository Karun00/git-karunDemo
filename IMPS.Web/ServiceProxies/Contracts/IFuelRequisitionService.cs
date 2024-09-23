using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;

namespace IPMS.ServiceProxies.Contracts
{
    [ServiceContract]
    public interface IFuelRequisitionService : IDisposable
    {
        /// <summary>
        /// To Get Fuel Requisition Details 
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<FuelRequisitionVO> FuelRequisitionDetails();

        /// <summary>
        /// To Get Fuel Requisition Reference data While initialization
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        FuelRequisitionVO GetFuelRequisitionReferenceVO();

        /// <summary>
        /// To Get Craft Details
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<FuelRequisitionVO> GetCraftNames();

        /// <summary>
        /// To Get Crafts Information By CraftID
        /// </summary>
        /// <param name="CraftID"></param>
        /// <returns></returns>
        [OperationContract]
        FuelRequisitionVO GetCraftsByID(int CraftID);

        /// <summary>
        /// To Add Fuel Requisition Data
        /// </summary>
        /// <param name="fuelrequisitiondata"></param>
        /// <returns></returns>
        [OperationContract]
        FuelRequisitionVO AddFuelRequisition(FuelRequisitionVO data);

        /// <summary>
        /// To Modify Fuel Requisition Data
        /// </summary>
        /// <param name="fuelrequisitiondata"></param>
        /// <returns></returns>
        [OperationContract]
        FuelRequisitionVO ModifyFuelRequisition(FuelRequisitionVO data);

        /// <summary>
        ///  To Approve Fuel Requisition Request
        /// </summary>
        /// <param name="FuelRequisitionID"></param>
        /// <param name="remarks"></param>
        /// <param name="taskcode"></param>
        [OperationContract]
        void ApproveFuelRequisition(string fuelrequisitionid, string remarks, string taskcode);       

        /// <summary>
        ///  To Reject Fuel Requisition Request
        /// </summary>
        /// <param name="FuelRequisitionID"></param>
        /// <param name="remarks"></param>
        /// <param name="taskcode"></param>
        [OperationContract]
        void RejectFuelRequisition(string fuelrequisitionid, string remarks, string taskcode);

        /// <summary>
        ///  To get Fuel Requisition based on fuelrequisitionid
        /// </summary>
        /// <param name="fuelrequisitionid"></param>
        /// <returns></returns>
        [OperationContract]
        List<FuelRequisitionVO> GetFuelRequisition(int fuelrequisitionid);

    }
}
