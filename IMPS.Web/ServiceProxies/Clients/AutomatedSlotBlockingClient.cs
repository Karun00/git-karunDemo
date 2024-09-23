using IPMS.Domain.ValueObjects;
using IPMS.ServiceProxies.Contracts;
using IPMS.Web.ServiceProxies;
using System.Collections.Generic;


namespace IPMS.ServiceProxies.Clients
{
    public class AutomatedSlotBlockingClient : UserClientBase<IAutomatedSlotBlockingService>, IAutomatedSlotBlockingService
    {
        public List<AutomatedSlotBlockingVO> GetAutomatedSlotBlockings()
        {
            return WrapOperationWithException(() => Channel.GetAutomatedSlotBlockings());
        }

        public AutomatedSlotBlockingVO SaveAutomatedSlotBlocking(AutomatedSlotBlockingVO data)
        {
            return WrapOperationWithException(() => Channel.SaveAutomatedSlotBlocking(data));
        }

        public AutomatedSlotBlockingVO ModifyAutomatedSlotBlocking(AutomatedSlotBlockingVO data)
        {
            return WrapOperationWithException(() => Channel.ModifyAutomatedSlotBlocking(data));
        }
        public AutomatedSlotBlockingVO GetAutomatedReferenceData()
        {
            return WrapOperationWithException(() => Channel.GetAutomatedReferenceData());
        }

    }
}