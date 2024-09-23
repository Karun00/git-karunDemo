using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Domain.DTOS
{
    public static class BudgetedValuesMapExtension
    {
        public static List<BudgetedValues> MapToEntity(this List<BudgetedValuesVO> data)
        {
            List<BudgetedValues> list = new List<BudgetedValues>();
            if (data != null)
            {
                foreach (var item in data)
                {
                    list.Add(item.MapToEntity());
                }
            }
            return list;
        }

        public static List<BudgetedValuesVO> MapToDto(this List<BudgetedValues> data)
        {
            List<BudgetedValuesVO> volist = new List<BudgetedValuesVO>();
            if (data != null)
            {
                foreach (var item in data)
                {
                    volist.Add(item.MapToDto());
                }
            }
            return volist;
        }

        public static BudgetedValues MapToEntity(this BudgetedValuesVO data)
        {
            BudgetedValues budgetedValues = new BudgetedValues  
          
            {           
                BudgetedValuesID = data.BudgetedValuesID.GetValueOrDefault(),
                FinancialYearID = data.FinancialYearID.GetValueOrDefault(),
                PortCode = data.PortCode,
                VolumesContainers = data.VolumesContainers,
                VolumesRBCT = data.VolumesRBCT,
                VolumesDryBulk = data.VolumesDryBulk,
                VolumesBreakBulk = data.VolumesBreakBulk,
                MovementsContainers = data.MovementsContainers,
                MovementsRBCT = data.MovementsRBCT,
                MovementsDryBulk = data.MovementsDryBulk,
                MovementsBreakBulk = data.MovementsBreakBulk,
                STATContainers = data.STATContainers,
                STATRBCT = data.STATRBCT,
                STATDryBulk = data.STATDryBulk,
                STATBreakBulk = data.STATBreakBulk,

                //Newly Added as Per report format
                TotalArrivals = data.TotalArrivals,
                TotalGT = data.TotalGT,
                TotalBerthingDelays = data.TotalBerthingDelays,
                TotalPilotDelays = data.TotalPilotDelays,
                TotalTugAvailability = data.TotalTugAvailability,
                TotalTugDelays = data.TotalTugDelays,
                TotalTugUtilization = data.TotalTugUtilization,

                RecordStatus = data.RecordStatus,
                CreatedBy = data.CreatedBy.GetValueOrDefault(),
                CreatedDate = data.CreatedDate.GetValueOrDefault(),
                ModifiedBy = data.ModifiedBy.GetValueOrDefault(),
                ModifiedDate = data.ModifiedDate.GetValueOrDefault()
            };
            
            return budgetedValues;
        }

        public static BudgetedValuesVO MapToDto(this BudgetedValues data)
        {
            BudgetedValuesVO budgetedValuesvo = new BudgetedValuesVO
            {

                FinancialYearID = data.FinancialYearID,
                BudgetedValuesID = data.BudgetedValuesID,
                PortCode = data.PortCode,
                VolumesContainers = data.VolumesContainers,
                VolumesRBCT = data.VolumesRBCT,
                VolumesDryBulk = data.VolumesDryBulk,
                VolumesBreakBulk = data.VolumesBreakBulk,
                MovementsContainers = data.MovementsContainers,
                MovementsRBCT = data.MovementsRBCT,
                MovementsDryBulk = data.MovementsDryBulk,
                MovementsBreakBulk = data.MovementsBreakBulk,
                STATContainers = data.STATContainers,
                STATRBCT = data.STATRBCT,
                STATDryBulk = data.STATDryBulk,
                STATBreakBulk = data.STATBreakBulk,

                //Newly Added as Per report format
                TotalArrivals = data.TotalArrivals,
                TotalGT = data.TotalGT,
                TotalBerthingDelays = data.TotalBerthingDelays,
                TotalPilotDelays = data.TotalPilotDelays,
                TotalTugAvailability = data.TotalTugAvailability,
                TotalTugDelays = data.TotalTugDelays,
                TotalTugUtilization = data.TotalTugUtilization,

                RecordStatus = data.RecordStatus,
                CreatedBy = data.CreatedBy,
                CreatedDate = data.CreatedDate,
                ModifiedBy = data.ModifiedBy,
                ModifiedDate = data.ModifiedDate
            };

            return budgetedValuesvo;
        }
    }
}
