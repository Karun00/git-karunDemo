using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;


namespace IPMS.ServiceProxies.Contracts
{
    [ServiceContract]
    public interface ICraftReminderConfigService : IDisposable
    {

        [OperationContract]
        List<CraftReminderConfigVO> GetCraftReminderConfigDetails(int craftId);

        [OperationContract]
        CraftReferenceVO GetCraftReminderReferences();

        [OperationContract]
        CraftVO AddCraftReminderConfig(CraftReminderConfigVO data);

        [OperationContract]
        CraftReminderConfigVO ModifyCraftReminderConfig(CraftReminderConfigVO data);

        //[OperationContract]
        //Task<List<CraftReminderConfigVO>> GetCraftReminderConfigDetailsAsync(int craftID);

        //[OperationContract]
        //Task<CraftReferenceVO> GetCraftReminderReferencesAsync();

        //[OperationContract]
        //Task<CraftVO> AddCraftReminderConfigAsync(CraftReminderConfigVO craftConfigVO);

        //[OperationContract]
        //Task<CraftReminderConfigVO> ModifyCraftReminderConfigAsync(CraftReminderConfigVO craftConfigVO);


        [OperationContract]
        List<CraftVO> GetCraftReminderConfigById(int craftReminderConfigId);

        //[OperationContract]
        //Task<List<CraftVO>> GetCraftReminderConfigByIDAsync(int craftreminderconfigID);

        [OperationContract]
        void AcknowledgeCraftReminderConfig(string craftReminderConfigID, string comments, string taskcode);

    }
}
