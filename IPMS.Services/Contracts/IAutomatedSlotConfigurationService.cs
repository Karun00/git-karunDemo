using System;
using Core.Repository.Providers.EntityFramework;
using Core.Repository;
using IPMS.Domain.Models;
using IPMS.Data.Context;
using System.Collections.Generic;
using System.ServiceModel;
using IPMS.Domain.ValueObjects;

namespace IPMS.Services
{
    [ServiceContract]
    public interface IAutomatedSlotConfigurationService 
    {
        [OperationContract] 
        [FaultContract(typeof(Exception))]
        List<AutomatedSlotConfigurationVO> GetAutomatedSlotConfigList();

        //[OperationContract]
        //[FaultContract(typeof(Exception))]
        //List<SlotPriorityConfigurationVO> GetSlotPriorityConfigList(int slotpriorityid);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        AutomatedSlotConfigurationVO UpdateAutomatedSlotConfigDetails(AutomatedSlotConfigurationVO data);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        AutomatedSlotConfigurationVO SaveAutomatedSlotConfigDetails(AutomatedSlotConfigurationVO data);
      
        [OperationContract]
        [FaultContract(typeof(Exception))]
        AutomatedSlotConfigurationReferenceDataVO GetAutomatedSlotConfigurationReferenceVO();




      
    }
}
