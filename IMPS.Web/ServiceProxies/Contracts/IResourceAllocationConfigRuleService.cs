using IPMS.Domain.Models;
using System.Collections.Generic;
using System.ServiceModel;
using System;
using IPMS.Domain.ValueObjects;
using System.Threading.Tasks;

namespace IPMS.ServiceProxies.Contracts
{
    [ServiceContract]
    public interface IResourceAllocationConfigRuleService : IDisposable
    {
        [OperationContract]
        List<ResourceAllocationConfigRuleVO> GetResourceAllocationConfigRuleList();

        [OperationContract]     
        ResourceAllocationConfigRuleVO AddResourceAllocationConfigRule(ResourceAllocationConfigRuleVO RACRdata);

        [OperationContract]       
        ResourceAllocationConfigRuleVO ModifyResourceAllocationConfigRule(ResourceAllocationConfigRuleVO RACRdata);
      
        [OperationContract]
        RAConfigruleReferenceVO GetResourceAllocationConfigRuleReferencesVO();
    }
}
