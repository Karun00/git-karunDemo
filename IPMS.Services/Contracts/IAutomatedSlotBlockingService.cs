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
    public interface IAutomatedSlotBlockingService
    {
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<AutomatedSlotBlockingVO> GetAutomatedSlotBlockings();

        [OperationContract]
        [FaultContract(typeof(Exception))]
        AutomatedSlotBlockingVO SaveAutomatedSlotBlocking(AutomatedSlotBlockingVO data);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        AutomatedSlotBlockingVO ModifyAutomatedSlotBlocking(AutomatedSlotBlockingVO data);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        AutomatedSlotBlockingVO GetAutomatedReferenceData();
    }
}
