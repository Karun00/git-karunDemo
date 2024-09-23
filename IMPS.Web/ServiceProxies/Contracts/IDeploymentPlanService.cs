using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;

namespace IPMS.ServiceProxies.Contracts
{
    [ServiceContract]
    public interface IDeploymentPlanService : IDisposable
    {
        /// <summary>
        /// To Get Deployment Plan Details
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<DeploymentPlanVO> DeploymentPlanDetails();

        /// <summary>
        /// To Get Deployment Plan Reference data While initialization
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        DeploymentPlanVO GetDeploymentPlanReferenceVO();

        /// <summary>
        /// To Get  Deployment Plan Types   
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<SubCategoryVO> GetDeploymentPlanTypes();

        /// <summary>
        /// To Get Planned Deployment Details
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<PlannedDeploymentVO> PlannedDeploymentDetails();

        /// <summary>
        /// To Get Craft Details
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<PlannedDeploymentVO> GetDeploymentCraftNames();

        /// <summary>
        /// To Add Deployment Plan Data
        /// </summary>
        /// <param name="deploymentdata"></param>
        /// <returns></returns>
        [OperationContract]
        DeploymentPlanVO AddDeploymentPlan(DeploymentPlanVO deploymentdata);

        /// <summary>
        /// To Modify Deployment Plan Data
        /// </summary>
        /// <param name="deploymentdata"></param>
        /// <returns></returns>
        [OperationContract]
        DeploymentPlanVO ModifyDeploymentPlan(DeploymentPlanVO deploymentdata);
        ///// <summary>
        ///// To Get Financial Year
        ///// </summary>
        ///// <returns></returns>
        //[OperationContract]
        //List<FinancialYearVO> GetFinancialYear();
    }
}
