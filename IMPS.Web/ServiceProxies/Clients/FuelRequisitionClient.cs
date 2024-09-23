using IPMS.Domain.ValueObjects;
using IPMS.ServiceProxies.Contracts;
using IPMS.Web.ServiceProxies;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IPMS.ServiceProxies.Clients
{
    public class FuelRequisitionClient : UserClientBase<IFuelRequisitionService>, IFuelRequisitionService 
    {
        public List<FuelRequisitionVO> FuelRequisitionDetails()
        {
            return WrapOperationWithException(() => Channel.FuelRequisitionDetails());
        }
        public FuelRequisitionVO GetFuelRequisitionReferenceVO()
        {
            return WrapOperationWithException(() => Channel.GetFuelRequisitionReferenceVO());
        }
        public List<FuelRequisitionVO> GetCraftNames()
        {
            return WrapOperationWithException(() => Channel.GetCraftNames());
        }

        public FuelRequisitionVO GetCraftsByID(int CraftID)
        {
            return WrapOperationWithException(() => Channel.GetCraftsByID(CraftID));
        }

        public FuelRequisitionVO AddFuelRequisition(FuelRequisitionVO data)
        {
            return WrapOperationWithException(() => Channel.AddFuelRequisition(data));
        }

        public FuelRequisitionVO ModifyFuelRequisition(FuelRequisitionVO data)
        {
            return WrapOperationWithException(() => Channel.ModifyFuelRequisition(data));
        }

        public void ApproveFuelRequisition(string fuelrequisitionid, string remarks, string taskcode)
        {
            WrapOperationWithException(() => Channel.ApproveFuelRequisition(fuelrequisitionid, remarks, taskcode));
        }
      
        public void RejectFuelRequisition(string fuelrequisitionid, string remarks, string taskcode)
        {
            WrapOperationWithException(() => Channel.RejectFuelRequisition(fuelrequisitionid, remarks, taskcode));
        }
        public List<FuelRequisitionVO> GetFuelRequisition(int fuelrequisitionid)
        {
            return WrapOperationWithException(() => Channel.GetFuelRequisition(fuelrequisitionid));
        }

    }
}