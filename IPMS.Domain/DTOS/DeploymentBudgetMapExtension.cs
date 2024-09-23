using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPMS.Domain.ValueObjects;
using IPMS.Domain.Models;

namespace IPMS.Domain.DTOS
{
    public static class DeploymentBudgetMapExtension
    {
        public static List<PlannedDeploymentVO> MapToDTO(this IEnumerable<DeploymentBudget> deploymentbudget)
        {
            var deploymentbudgetVOList = new List<PlannedDeploymentVO>();
            if (deploymentbudget != null)
            {
                foreach (var item in deploymentbudget)
                {
                    deploymentbudgetVOList.Add(item.MapToDTO());
                }
            }
            return deploymentbudgetVOList;
        }

        public static PlannedDeploymentVO MapToDTO(this DeploymentBudget data)
        {
            PlannedDeploymentVO VO = new PlannedDeploymentVO();
            if (data != null)
            {
                VO.DeploymentBudgetID = data.DeploymentBudgetID;
                VO.DeploymentPlanID = data.DeploymentPlanID;
                VO.Budget = data.Budget;
                VO.DredgPlan = data.DredgPlan;
                VO.Jan = data.Jan;
                VO.JanCraftID = data.JanCraftID;
                VO.Feb = data.Feb;
                VO.FebCraftID = data.FebCraftID;
                VO.Mar = data.Mar;
                VO.MarCraftID = data.MarCraftID;
                VO.Apr = data.Apr;
                VO.AprCraftID = data.AprCraftID;
                VO.May = data.May;
                VO.MayCraftID = data.MayCraftID;
                VO.Jun = data.Jun;
                VO.JunCraftID = data.JunCraftID;
                VO.Jul = data.Jul;
                VO.JulCraftID = data.JulCraftID;
                VO.Aug = data.Aug;
                VO.AugCraftID = data.AugCraftID;
                VO.Sep = data.Sep;
                VO.SepCraftID = data.SepCraftID;
                VO.Oct = data.Oct;
                VO.OctCraftID = data.OctCraftID;
                VO.Nov = data.Nov;
                VO.NovCraftID = data.NovCraftID;
                VO.Dec = data.Dec;
                VO.DecCraftID = data.DecCraftID;
                VO.SubCatCode = data.DredgingType;


                VO.RecordStatus = data.RecordStatus;
                VO.CreatedBy = data.CreatedBy;
                VO.CreatedDate = data.CreatedDate;
                VO.ModifiedBy = data.ModifiedBy;
                VO.ModifiedDate = data.ModifiedDate;
            }
            return VO;
        }
        public static DeploymentBudget MapToEntity(this PlannedDeploymentVO VO)
        {
            DeploymentBudget data = new DeploymentBudget();
            if (VO != null)
            {
                data.DeploymentBudgetID = VO.DeploymentBudgetID;
                data.DeploymentPlanID = VO.DeploymentPlanID;
                data.Budget = VO.Budget;
                data.DredgPlan = VO.DredgPlan;
                data.Jan = VO.Jan;
                data.JanCraftID = VO.JanCraftID;
                data.Feb = VO.Feb;
                data.FebCraftID = VO.FebCraftID;
                data.Mar = VO.Mar;
                data.MarCraftID = VO.MarCraftID;
                data.Apr = VO.Apr;
                data.AprCraftID = VO.AprCraftID;
                data.May = VO.May;
                data.MayCraftID = VO.MayCraftID;
                data.Jun = VO.Jun;
                data.JunCraftID = VO.JunCraftID;
                data.Jul = VO.Jul;
                data.JulCraftID = VO.JulCraftID;
                data.Aug = VO.Aug;
                data.AugCraftID = VO.AugCraftID;
                data.Sep = VO.Sep;
                data.SepCraftID = VO.SepCraftID;
                data.Oct = VO.Oct;
                data.OctCraftID = VO.OctCraftID;
                data.Nov = VO.Nov;
                data.NovCraftID = VO.NovCraftID;
                data.Dec = VO.Dec;
                data.DecCraftID = VO.DecCraftID;
                data.DredgingType = VO.SubCatCode;

                data.RecordStatus = VO.RecordStatus;
                data.CreatedBy = VO.CreatedBy;
                data.CreatedDate = VO.CreatedDate;
                data.ModifiedBy = VO.ModifiedBy;
                data.ModifiedDate = VO.ModifiedDate;
            }
            return data;
        }

    }
}
