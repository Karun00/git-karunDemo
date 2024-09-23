using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ServiceModel;


namespace IPMS.Services
{
    [ServiceContract]
    public interface IDryDockSchedulerService
    {
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<SuppDryDockVO> GetPendingVesselForDryDock();

        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<BerthVO> GetDockList();

        [OperationContract]
        [FaultContract(typeof(Exception))]
        SuppDryDock AddScheduleDryDock(SuppDryDockVO data);

        //[OperationContract]
        //[FaultContract(typeof(Exception))]
        //List<SuppDryDockVO> GetScheduledVesselForDryDock();

        [OperationContract]
        [FaultContract(typeof(Exception))]
        SuppDryDock UnPlannedScheduleDryDock(SuppDryDockVO data);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        SuppScheduledDryDockVO ConfirmedScheduleDryDock(List<SuppScheduledDryDockVO> data);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<SuppDryDockVO> GetScheduledVesselForDryDock(string quayCode,string dockCode);
    }
}
