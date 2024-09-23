using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;

namespace IPMS.ServiceProxies.Contracts
{
    [ServiceContract]
    public interface IFuelReceiptService : IDisposable
    {
        /// <summary>
        /// To Get Fuel Receipt Details 
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<FuelRequisitionVO> FuelReceiptDetails();

        /// <summary>
        /// To Get Fuel Receipt Reference data While initialization
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        FuelReceiptVO GetFuelReceiptReferenceVO();

        /// <summary>
        /// To Add Fuel Receipt Data
        /// </summary>
        /// <param name="fuelreceiptdata"></param>
        /// <returns></returns>
        [OperationContract]
        FuelReceiptVO AddFuelReceipt(FuelReceiptVO data);

        /// <summary>
        ///  To get Fuel Receipt based on fuelrequestionid
        /// </summary>
        /// <param name="fuelRequestionId"></param>
        /// <returns></returns>
        [OperationContract]
        List<FuelRequisitionVO> GetFuelReceipt(int fuelRequestionId);

        /// <summary>
        ///  To get Fuel Receipt based on fuelreceiptid
        /// </summary>
        /// <param name="fuelReceiptId"></param>
        /// <returns></returns>
        [OperationContract]
        List<FuelRequisitionVO> GetFuelReceiptFuelId(int fuelReceiptId);

        /// <summary>
        ///  To Approve Fuel Receipt Request
        /// </summary>
        /// <param name="FuelReceiptID"></param>
        /// <param name="remarks"></param>
        /// <param name="taskcode"></param>
        [OperationContract]
        void ApproveFuelReceipt(string fuelreceiptid, string remarks, string taskcode);      
    }
}
