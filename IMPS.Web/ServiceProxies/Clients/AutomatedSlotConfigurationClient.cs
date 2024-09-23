using IPMS.Domain.ValueObjects;
using IPMS.Web.ServiceProxies;
using IPMS.ServiceProxies.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IPMS.ServiceProxies.Clients
{
    public class AutomatedSlotConfigurationClient : UserClientBase<IAutomatedSlotConfigurationService>, IAutomatedSlotConfigurationService
    {
        public List<AutomatedSlotConfigurationVO> GetAutomatedSlotConfigList()
        {
            return WrapOperationWithException(() => Channel.GetAutomatedSlotConfigList());
        }

        public List<SlotPriorityConfigurationVO> GetSlotPriorityConfigList(int slotpriorityid)
        {
            return WrapOperationWithException(() => Channel.GetSlotPriorityConfigList(slotpriorityid));
        }

        public AutomatedSlotConfigurationVO UpdateAutomatedSlotConfigDetails(AutomatedSlotConfigurationVO data)
        {
            return WrapOperationWithException(() => Channel.UpdateAutomatedSlotConfigDetails(data));
        }

        public AutomatedSlotConfigurationVO SaveAutomatedSlotConfigDetails(AutomatedSlotConfigurationVO data)
        {
            return WrapOperationWithException(() => Channel.SaveAutomatedSlotConfigDetails(data));
        }
        public AutomatedSlotConfigurationReferenceDataVO GetAutomatedSlotConfigurationReferenceVO()
        {
            return WrapOperationWithException(() => Channel.GetAutomatedSlotConfigurationReferenceVO());
        }
    }
}