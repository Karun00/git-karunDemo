using System;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System.Collections.Generic;

namespace IPMS.Repository
{
   public  interface IResourceAllocationConfigRuleRepository
    {
       List<ResourceAllocationConfigRule> GetresourceAllocationconfigurationruledetails(string PortID);
       List<ServiceType> GetResourceAllocationConfigRulemovementtypedetails();
       ResourceAllocationConfigRuleVO AddResourceAllocationConfigRule(ResourceAllocationConfigRuleVO RACRdata, int userid, string portcode);
       ResourceAllocationConfigRuleVO ModifyResourceAllocationConfigRule(ResourceAllocationConfigRuleVO RACRvo, int userid, string portcode);
    }
}
