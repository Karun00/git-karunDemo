using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;

namespace IPMS.ServiceProxies.Contracts
{
    [ServiceContract]
    public interface IDryDockSchedulerService : IDisposable
    {
        [OperationContract]
        List<SuppDryDockVO> GetPendingVesselForDryDock();

        [OperationContract]
        List<BerthVO> GetDockList();

        [OperationContract]
        SuppDryDock AddScheduleDryDock(SuppDryDockVO data);

        //[OperationContract]
        //List<SuppDryDockVO> GetScheduledVesselForDryDock();


        [OperationContract]
        List<SuppDryDockVO> GetScheduledVesselForDryDock(string quayCode,string dockCode);

        [OperationContract]
        SuppDryDock UnPlannedScheduleDryDock(SuppDryDockVO data);

        [OperationContract]
        SuppScheduledDryDockVO ConfirmedScheduleDryDock(List<SuppScheduledDryDockVO> data);

    }
}
