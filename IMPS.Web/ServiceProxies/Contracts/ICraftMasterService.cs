using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;


namespace IPMS.ServiceProxies.Contracts
{
    [ServiceContract]
    public interface ICraftMasterService : IDisposable
    {
        [OperationContract]
        List<CraftVO> GetCraftList();

        [OperationContract]
        CraftReferenceVO GetCraftReferences();

        [OperationContract]
        CraftVO AddCraft(CraftVO craftData);

        [OperationContract]
        CraftVO ModifyCraft(CraftVO craftData);

        //[OperationContract]
        //Task<List<CraftVO>> GetCraftlistAsync();

        //[OperationContract]
        //Task<CraftReferenceVO> GetCraftReferencesAsync();

        //[OperationContract]
        //Task<CraftVO> AddCraftAsync(CraftVO craftdata);

        //[OperationContract]
        //Task<CraftVO> ModifyCraftAsync(CraftVO craftdata);

    }
}
