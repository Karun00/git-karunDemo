using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace IPMS.Services
{
    [ServiceContract]
    public interface ICraftMasterService
    {
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<CraftVO> GetCraftList();

        [OperationContract]
        [FaultContract(typeof(Exception))]
        CraftVO AddCraft(CraftVO craftData);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        CraftVO ModifyCraft(CraftVO craftData);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        CraftReferenceVO GetCraftReferences();
    }
}
