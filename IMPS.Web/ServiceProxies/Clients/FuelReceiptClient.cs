using IPMS.Domain.ValueObjects;
using IPMS.ServiceProxies.Contracts;
using IPMS.Web.ServiceProxies;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IPMS.ServiceProxies.Clients
{
    public class FuelReceiptClient : UserClientBase<IFuelReceiptService>, IFuelReceiptService 
    {
        public List<FuelRequisitionVO> FuelReceiptDetails()
        {
            return WrapOperationWithException(() => Channel.FuelReceiptDetails());
        }
        public FuelReceiptVO GetFuelReceiptReferenceVO()
        {
            return WrapOperationWithException(() => Channel.GetFuelReceiptReferenceVO());
        }
        public FuelReceiptVO AddFuelReceipt(FuelReceiptVO data)
        {
            return WrapOperationWithException(() => Channel.AddFuelReceipt(data));
        }
        public List<FuelRequisitionVO> GetFuelReceipt(int fuelRequestionId)
        {
            return WrapOperationWithException(() => Channel.GetFuelReceipt(fuelRequestionId));
        }
        public void ApproveFuelReceipt(string fuelreceiptid, string remarks, string taskcode)
        {
            WrapOperationWithException(() => Channel.ApproveFuelReceipt(fuelreceiptid, remarks, taskcode));
        }
        public List<FuelRequisitionVO> GetFuelReceiptFuelId(int fuelReceiptId)
        {
            return WrapOperationWithException(() => Channel.GetFuelReceiptFuelId(fuelReceiptId));
        }
    }
}