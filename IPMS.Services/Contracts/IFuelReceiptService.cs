using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace IPMS.Services
{
    [ServiceContract]
    public interface IFuelReceiptService
    {
        /// <summary>
        /// To Get Fuel Receipt Details
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<FuelRequisitionVO> FuelReceiptDetails();

        /// <summary>
        /// To Get Fuel Receipt  Reference data While initialization
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        FuelReceiptVO GetFuelReceiptReferenceVO();

        /// <summary>
        /// To Add Fuel Receipt Data
        /// </summary>
        /// <param name="fuelreceiptdata"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        FuelReceiptVO AddFuelReceipt(FuelReceiptVO data);

        /// <summary>
        /// To get  Fuel Receipt based on fuelrequestionid
        /// </summary>
        /// <param name="fuelRequestionId"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<FuelRequisitionVO> GetFuelReceipt(int fuelRequestionId);

        /// <summary>
        /// To get  Fuel Receipt based on fuelreceiptid
        /// </summary>
        /// <param name="fuelReceiptId"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<FuelRequisitionVO> GetFuelReceiptFuelId(int fuelReceiptId);

        /// <summary>
        ///  To Approve Fuel Receipt Request
        /// </summary>
        /// <param name="FuelReceiptID"></param>
        /// <param name="remarks"></param>
        /// <param name="taskcode"></param>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        void ApproveFuelReceipt(string fuelreceiptid, string remarks, string taskcode);

    }
}
