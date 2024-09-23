using IPMS.Domain.ValueObjects;
using IPMS.ServiceProxies.Contracts;
using IPMS.Web.ServiceProxies;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IPMS.ServiceProxies.Clients
{
    
    public class ResourceAllocationConfigRuleClient : UserClientBase<IResourceAllocationConfigRuleService>, IResourceAllocationConfigRuleService
    {       
     
        public List<ResourceAllocationConfigRuleVO> GetResourceAllocationConfigRuleList()
        {
            return WrapOperationWithException(() => Channel.GetResourceAllocationConfigRuleList());
        }

        public  ResourceAllocationConfigRuleVO AddResourceAllocationConfigRule(ResourceAllocationConfigRuleVO RACRdata)
         {
            return WrapOperationWithException(() => Channel.AddResourceAllocationConfigRule(RACRdata));
        }
       
        public ResourceAllocationConfigRuleVO ModifyResourceAllocationConfigRule(ResourceAllocationConfigRuleVO RACRdata)
        {
            return WrapOperationWithException(() => Channel.ModifyResourceAllocationConfigRule(RACRdata));
        }


        public RAConfigruleReferenceVO GetResourceAllocationConfigRuleReferencesVO()
        {
            return WrapOperationWithException(() => Channel.GetResourceAllocationConfigRuleReferencesVO());
        }
    }
}