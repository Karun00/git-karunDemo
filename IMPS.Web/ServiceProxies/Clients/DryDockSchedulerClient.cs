using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using IPMS.ServiceProxies.Contracts;
using IPMS.Web.ServiceProxies;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IPMS.ServiceProxies.Clients
{
    public class DryDockSchedulerClient : UserClientBase<IDryDockSchedulerService>, IDryDockSchedulerService
    {
        public List<SuppDryDockVO> GetPendingVesselForDryDock()
        {
            return WrapOperationWithException(() => Channel.GetPendingVesselForDryDock());
        }

        public List<BerthVO> GetDockList()
        {
            return WrapOperationWithException(() => Channel.GetDockList());
        }

        public SuppDryDock AddScheduleDryDock(SuppDryDockVO data)
        {
            return WrapOperationWithException(() => Channel.AddScheduleDryDock(data));
        }

        //public List<SuppDryDockVO> GetScheduledVesselForDryDock()
        //{
        //    return WrapOperationWithException(() => Channel.GetScheduledVesselForDryDock());
        //}

        public List<SuppDryDockVO> GetScheduledVesselForDryDock(string quayCode,string dockCode)
        {
            return WrapOperationWithException(() => Channel.GetScheduledVesselForDryDock(quayCode,dockCode));
        }

        public SuppDryDock UnPlannedScheduleDryDock(SuppDryDockVO data)
        {
            return WrapOperationWithException(() => Channel.UnPlannedScheduleDryDock(data));
        }

        public SuppScheduledDryDockVO ConfirmedScheduleDryDock(List<SuppScheduledDryDockVO> data)
        {
            return WrapOperationWithException(() => Channel.ConfirmedScheduleDryDock(data));
        }

    }
}