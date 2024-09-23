using IPMS.Domain.ValueObjects;
using IPMS.ServiceProxies.Contracts;
using IPMS.Web.ServiceProxies;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IPMS.ServiceProxies.Clients
{
    public class DeploymentPlanClient : UserClientBase<IDeploymentPlanService>, IDeploymentPlanService
    {
        public List<DeploymentPlanVO> DeploymentPlanDetails()
        {
            return WrapOperationWithException(() => Channel.DeploymentPlanDetails());
        }

        public DeploymentPlanVO GetDeploymentPlanReferenceVO()
        {
            return WrapOperationWithException(() => Channel.GetDeploymentPlanReferenceVO());
        }
        public List<SubCategoryVO> GetDeploymentPlanTypes()
        {
            return WrapOperationWithException(() => Channel.GetDeploymentPlanTypes());
        }
        public List<PlannedDeploymentVO> PlannedDeploymentDetails()
        {
            return WrapOperationWithException(() => Channel.PlannedDeploymentDetails());
        }
        public List<PlannedDeploymentVO> GetDeploymentCraftNames()
        {
            return WrapOperationWithException(() => Channel.GetDeploymentCraftNames());
        }
        public DeploymentPlanVO AddDeploymentPlan(DeploymentPlanVO deploymentdata)
        {
            return WrapOperationWithException(() => Channel.AddDeploymentPlan(deploymentdata));
        }

        public DeploymentPlanVO ModifyDeploymentPlan(DeploymentPlanVO deploymentdata)
        {
            return WrapOperationWithException(() => Channel.ModifyDeploymentPlan(deploymentdata));
        }
        //public List<FinancialYearVO> GetFinancialYear()
        //{
        //    return WrapOperationWithException(() => Channel.GetFinancialYear());
        //}

    }
}