using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using IPMS.ServiceProxies.Contracts;
using IPMS.Web.ServiceProxies;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IPMS.ServiceProxies.Clients
{
    public class DockingPlanClient : UserClientBase<IDockingPlanService>, IDockingPlanService
    {
        public List<DockingPlanVO> DockingPlanDetails()
        {
            return WrapOperationWithException(() => Channel.DockingPlanDetails());
        }
        public List<DockingPlanVO> GetVesselNames(string searchValue, string searchColumn)
        {
            return WrapOperationWithException(() => Channel.GetVesselNames(searchValue, searchColumn));
        }

        public DockingPlanVO GetVesselsById(int vesselId)
        {
            return WrapOperationWithException(() => Channel.GetVesselsById(vesselId));
        }


        public DockingPlanVO AddDockingPlan(DockingPlanVO data)
        {
            return WrapOperationWithException(() => Channel.AddDockingPlan(data));
        }
        //Added by srinivas
        public DockingPlanVO Cancel(DockingPlanVO data)
        {
            return WrapOperationWithException(() => Channel.Cancel(data));
        }

        public DockingPlanVO ModifyDockingPlan(DockingPlanVO data)
        {
            return WrapOperationWithException(() => Channel.ModifyDockingPlan(data));
        }
        public void ApproveDockingPlan(string dockingplanid, string remarks, string taskcode)
        {
            WrapOperationWithException(() => Channel.ApproveDockingPlan(dockingplanid, remarks, taskcode));
        }

        public void RejectDockingPlan(string dockingplanid, string remarks, string taskcode)
        {
            WrapOperationWithException(() => Channel.RejectDockingPlan(dockingplanid, remarks, taskcode));
        }
        public List<DockingPlanVO> GetDockingPlan(int dockingPlanId)
        {
            return WrapOperationWithException(() => Channel.GetDockingPlan(dockingPlanId));
        }

        public List<SubCategory> GetDocumentTypes()
        {
            return WrapOperationWithException(() => Channel.GetDocumentTypes());
        }
    }
}