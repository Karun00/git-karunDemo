using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Repository
{
    public interface IFuelReceiptRepository
    {
        List<FuelRequisitionVO> GetFuelReceiptDetails(string portCode);

        List<FuelRequisitionVO> GetFuelReceipt(int fuelRequestionId);

        List<FuelRequisitionVO> GetFuelReceiptDetailsById(string fuelRequestionId);

        FuelReceipt GetFuelReceiptApproveId(string fuelReceiptId);

        List<FuelRequisitionVO> GetFuelReceiptFuelId(int fuelReceiptId);

        FuelRequisitionVO GetFuelReceiptDetailsRequestById(string fuelReceiptId);
    }
}
