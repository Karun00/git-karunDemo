using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System.Collections.Generic;


namespace IPMS.Repository
{
    public interface IDeploymentPlanRepository
    {
        List<DeploymentPlanVO> DeploymentPlanDetails(string portCode);

        List<PlannedDeploymentVO> PlannedDeploymentDetails(string portCode);

        List<PlannedDeploymentVO> GetDeploymentCraftNames();
        List<PortVO> GetPortTypes();
        List<FinancialYearVO> GetFinancialYears();
        List<PortGeneralConfigsVO> GetCraftColors(string portCode);
    }
}
