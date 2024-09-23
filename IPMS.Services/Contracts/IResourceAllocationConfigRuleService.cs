using System;
using System.Collections.Generic;
using System.ServiceModel;
using IPMS.Domain.ValueObjects;

namespace IPMS.Services
{
    [ServiceContract]
    public interface IResourceAllocationConfigRuleService
    {
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<ResourceAllocationConfigRuleVO> GetResourceAllocationConfigRuleList();

        [OperationContract]
        [FaultContract(typeof(Exception))]
        ResourceAllocationConfigRuleVO AddResourceAllocationConfigRule(ResourceAllocationConfigRuleVO RACRdata);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        ResourceAllocationConfigRuleVO ModifyResourceAllocationConfigRule(ResourceAllocationConfigRuleVO RACRdata);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        RAConfigruleReferenceVO GetResourceAllocationConfigRuleReferencesVO();
    }
}
