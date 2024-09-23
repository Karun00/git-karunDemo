using IPMS.Domain.ValueObjects;
using IPMS.ServiceProxies.Contracts;
using IPMS.Web.ServiceProxies;
using System.Collections.Generic;

namespace IPMS.ServiceProxies.Clients
{
    public class BerthPlanningConfigurationsClient : UserClientBase<IBerthPlanningConfigurationsService>, IBerthPlanningConfigurationsService
    {
        public List<BerthPlanningConfigurationsVO> BerthPlanningConfigurationsDetails()
        {
            return WrapOperationWithException(() => Channel.BerthPlanningConfigurationsDetails());
        }

        public BerthPlanningConfigurationsVO ModifyBerthPlanConfig(BerthPlanningConfigurationsVO berthplanconfigdata)
        {
            return WrapOperationWithException(() => Channel.ModifyBerthPlanConfig(berthplanconfigdata));
        }

        public BerthPlanningConfigurationsVO AddBerthPlanConfig(BerthPlanningConfigurationsVO berthplanconfigdata)
        {
            return WrapOperationWithException(() => Channel.AddBerthPlanConfig(berthplanconfigdata));
        }
    }
}