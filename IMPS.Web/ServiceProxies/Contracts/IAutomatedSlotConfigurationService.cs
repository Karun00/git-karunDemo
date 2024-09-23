using System;
using Core.Repository.Providers.EntityFramework;
using Core.Repository;
using IPMS.Domain.Models;
using IPMS.Data.Context;
using System.Collections.Generic;
using System.ServiceModel;
using IPMS.Domain.ValueObjects;

namespace IPMS.ServiceProxies.Contracts
{
    [ServiceContract]
    public interface IAutomatedSlotConfigurationService:IDisposable
    {
        [OperationContract]
        List<AutomatedSlotConfigurationVO> GetAutomatedSlotConfigList();

        [OperationContract]
        List<SlotPriorityConfigurationVO> GetSlotPriorityConfigList(int slotpriorityid);

        [OperationContract]
        AutomatedSlotConfigurationVO UpdateAutomatedSlotConfigDetails(AutomatedSlotConfigurationVO data);

        [OperationContract]
        AutomatedSlotConfigurationVO SaveAutomatedSlotConfigDetails(AutomatedSlotConfigurationVO data);
       
        [OperationContract]    
        AutomatedSlotConfigurationReferenceDataVO GetAutomatedSlotConfigurationReferenceVO();
    }
}
