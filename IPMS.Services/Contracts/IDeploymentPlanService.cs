using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ServiceModel;


namespace IPMS.Services
{
    [ServiceContract]
    public interface IDeploymentPlanService
    {
        /// <summary>
        /// To Get Deployment Plan Details 
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<DeploymentPlanVO> DeploymentPlanDetails();

        /// <summary>
        /// To Get Deployment Plan Reference data While initialization
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        DeploymentPlanVO GetDeploymentPlanReferenceVO();

        /// <summary>
        /// To Get Deployment Types
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<SubCategoryVO> GetDeploymentPlanTypes();

        /// <summary>
        /// To Get Planned Deployment Details 
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<PlannedDeploymentVO> PlannedDeploymentDetails();

        /// <summary>
        /// To Get Craft Details
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<PlannedDeploymentVO> GetDeploymentCraftNames();

        /// <summary>
        /// To Add Deployment Plan Data
        /// </summary>
        /// <param name="deploymentdata"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        DeploymentPlanVO AddDeploymentPlan(DeploymentPlanVO deploymentdata);

        /// <summary>
        /// To Modify Deployment Plan Data
        /// </summary>
        /// <param name="deploymentdata"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        DeploymentPlanVO ModifyDeploymentPlan(DeploymentPlanVO deploymentdata);


        ///// <summary>
        ///// To Get Financial Year Data
        ///// </summary>
        ///// <returns></returns>
        //[OperationContract]
        //[FaultContract(typeof(Exception))]
        //List<FinancialYearVO> GetFinancialYear();
    }
}
