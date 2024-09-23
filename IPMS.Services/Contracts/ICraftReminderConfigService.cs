using System;
using Core.Repository.Providers.EntityFramework;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IPMS.Domain.Models;
using System.ServiceModel;
using IPMS.Domain.ValueObjects;

namespace IPMS.Services
{
    [ServiceContract]
    public interface ICraftReminderConfigService
    {
        [OperationContract]
        [FaultContract(typeof(Exception))]
        CraftReferenceVO GetCraftReminderReferences();

        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<CraftReminderConfigVO> GetCraftReminderConfigDetails(int craftId);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        CraftVO AddCraftReminderConfig(CraftReminderConfigVO data);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        CraftReminderConfigVO ModifyCraftReminderConfig(CraftReminderConfigVO data);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<CraftVO> GetCraftReminderConfigById(int craftReminderConfigId);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        void AcknowledgeCraftReminderConfig(string craftReminderConfigID, string comments, string taskcode);

    }
}
