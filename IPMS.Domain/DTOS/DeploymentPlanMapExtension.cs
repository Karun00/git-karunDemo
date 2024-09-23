using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPMS.Domain.ValueObjects;
using IPMS.Domain.Models;
using System.Globalization;

namespace IPMS.Domain.DTOS
{
    public static class DeploymentPlanMapExtension
    {
        public static List<DeploymentPlanVO> MapToDTOForDeploy(this List<DeploymentPlan> deploymentplans)
        {
            List<DeploymentPlanVO> deploymentplanvos = new List<DeploymentPlanVO>();
            if (deploymentplans != null)
            {
            foreach (var deploymentplan in deploymentplans)
            {
                deploymentplanvos.Add(deploymentplan.MapToDTO());
            }
            }
            return deploymentplanvos;
        }  
        public static DeploymentPlanVO MapToDTO(this DeploymentPlan data)
        {
            DeploymentPlanVO VO = new DeploymentPlanVO();
            if (data != null)
            {
            VO.DeploymentPlanID = data.DeploymentPlanID;
            VO.FinancialYearID = data.FinancialYearID;
           // VO.FinancialYearName = data.SubCategory.SubCatName;
            VO.PortCode = data.PortCode;
            VO.Description = data.Description;
            VO.RecordStatus = data.RecordStatus;
            VO.CreatedBy = data.CreatedBy;
            VO.CreatedDate = data.CreatedDate;
            VO.ModifiedBy = data.ModifiedBy;
            VO.ModifiedDate = data.ModifiedDate;
            VO.PortName = data.Port.PortName;
            VO.StartDate = data.FinancialYear.StartDate;
            VO.EndDate = data.FinancialYear.EndDate;
            var Finaldate = data.FinancialYear.StartDate.ToString("MMMM", CultureInfo.InvariantCulture) + " " + data.FinancialYear.StartDate.ToString("yyyy", CultureInfo.InvariantCulture) + " - " + data.FinancialYear.EndDate.ToString("MMMM", CultureInfo.InvariantCulture) + " " + data.FinancialYear.EndDate.ToString("yyyy", CultureInfo.InvariantCulture);

        //    var dt = VO.StartDate;
        // string[] months = new string[] {"January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"};
        // var Smonth_name = months[dt.Month];
        //var Syear = dt.Year;

        //var dt2 = VO.EndDate;
        //var Emonth_name = months[dt2.Month];
        //var Eyear = dt2.Year;

        //var Finaldate = Smonth_name + " " + Syear + " - "+Emonth_name + " " + Eyear + "";
        VO.DateName = Finaldate;



            VO.DeploymentBudget = data.DeploymentBudgets.MapToDTO();
            }

            return VO;
        }

        public static DeploymentPlan MapToEntity(this DeploymentPlanVO VO)
        {
            DeploymentPlan data = new DeploymentPlan();
            if (VO != null)
            {
            data.DeploymentPlanID = VO.DeploymentPlanID;
            data.FinancialYearID = VO.FinancialYearID;
            data.PortCode = VO.PortCode;
            data.Description = VO.Description;
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
