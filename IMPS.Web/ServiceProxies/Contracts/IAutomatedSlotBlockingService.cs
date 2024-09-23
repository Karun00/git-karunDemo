using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace IPMS.ServiceProxies.Contracts
{
    [ServiceContract]
    public interface IAutomatedSlotBlockingService : IDisposable    
    {
        [OperationContract]
        List<AutomatedSlotBlockingVO> GetAutomatedSlotBlockings();

        [OperationContract]
        AutomatedSlotBlockingVO GetAutomatedReferenceData();

        [OperationContract]
        AutomatedSlotBlockingVO SaveAutomatedSlotBlocking(AutomatedSlotBlockingVO data);

        [OperationContract]
        AutomatedSlotBlockingVO ModifyAutomatedSlotBlocking(AutomatedSlotBlockingVO data);
    }
}