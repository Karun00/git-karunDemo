using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Repository
{
    public interface IFuelRequisitionRepository
    {
        List<FuelRequisitionVO> GetFuelRequisitionDetails(string portCode);
        

        List<FuelRequisitionVO> GetCraftNames(string portcode);

        FuelRequisitionVO GetCraftsByID(int CraftID);

        FuelRequisition GetFuelRequisitionApproveid(string fuelrequisitionid);

        FuelRequisitionVO GetFuelRequisitionRequestDetailsByID(string fuelrequisitionid);

        List<FuelRequisitionVO> GetFuelRequisition(int fuelrequisitionid);

        string GetUserNameByUserId(int userid);
    }
}
