using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System.Globalization;

namespace IPMS.Domain.DTOS
{
    public static class BerthMaintenanceMapExtension
    {
        public static List<BerthMaintenanceVO> MapToDto(this List<BerthMaintenance> berthmaintenances)
        {          
                List<BerthMaintenanceVO> berthmaintenanceVos = new List<BerthMaintenanceVO>();
                if (berthmaintenances != null)
                {
                    foreach (var berthMaintenance in berthmaintenances)
                    {
                        berthmaintenanceVos.Add(berthMaintenance.MapToDto());

                    }
                }
                return berthmaintenanceVos;            
        }

        public static BerthMaintenanceVO MapToDto(this BerthMaintenance data)
        {
            BerthMaintenanceVO VO = new BerthMaintenanceVO();
            if (data != null)
            {
                VO.BerthMaintenanceID = data.BerthMaintenanceID;
                VO.PortCode = data.PortCode;
                VO.ProjectNo = data.ProjectNo;
                VO.MaintenanceTypeCode = data.MaintenanceTypeCode;
                VO.MaintPortCode = data.MaintPortCode;
                VO.MaintQuayCode = data.MaintQuayCode;
                VO.MaintBerthCode = data.MaintBerthCode;
                VO.FromPortCode = data.FromPortCode;
                VO.FromQuayCode = data.FromQuayCode;
                VO.FromBerthCode = data.FromBerthCode;
                VO.FromBollard = data.FromBollard;
                VO.ToPortCode = data.ToPortCode;
                VO.ToQuayCode = data.ToQuayCode;
                VO.ToBerthCode = data.ToBerthCode;
                VO.ToBollard = data.ToBollard;
                VO.PeriodFrom = Convert.ToString(data.PeriodFrom, CultureInfo.InvariantCulture);
                VO.PeriodTo = Convert.ToString(data.PeriodTo, CultureInfo.InvariantCulture);
                VO.BerthKey = data.MaintPortCode + "." + data.MaintQuayCode + "." + data.MaintBerthCode;
                VO.FromBollardKey = data.FromPortCode + "." + data.FromQuayCode + "." + data.FromBerthCode + "." + data.FromBollard;
                VO.ToBollardKey = data.ToPortCode + "." + data.ToQuayCode + "." + data.ToBerthCode + "." + data.ToBollard;
                VO.MaintenanceType = data.SubCategory1.SubCatName;
                VO.BerthName = data.Berth.BerthName;
                VO.BollardsFrom = data.Bollard.BollardName;
                VO.BollardsTo = data.Bollard1.BollardName;

                if (data.WorkflowInstance != null)
                {
                    if (data.WorkflowInstance.SubCategory != null)
                    {
                        VO.Status = data.WorkflowInstance.SubCategory.SubCatName;
                    }
                }

                VO.OccupationTypeCode = data.OccupationTypeCode;
                VO.Precinct = data.Precinct;
                VO.DisciplineCode = data.DisciplineCode;
                VO.SpecialConditions = data.SpecialConditions;
                VO.Description = data.Description;
                VO.RecordStatus = data.RecordStatus;
                VO.CreatedBy = data.CreatedBy;
                VO.CreatedDate = data.CreatedDate;
                VO.ModifiedBy = data.ModifiedBy;
                VO.ModifiedDate = data.ModifiedDate;
                VO.BerthMaintenanceNo = data.BerthMaintenanceNo != null ? data.BerthMaintenanceNo : string.Empty;
                VO.WorkflowInstanceId = data.WorkflowInstanceId != null ? data.WorkflowInstanceId : null;
                VO.ReferenceNo = data.BerthMaintenanceNo;
            }
            return VO;

        }

        public static BerthMaintenance MapToEntity(this BerthMaintenanceVO vo)
        {
            BerthMaintenance data = new BerthMaintenance();
            if (vo != null)
            {
                data.BerthMaintenanceID = vo.BerthMaintenanceID;
                data.PortCode = vo.PortCode;
                data.ProjectNo = vo.ProjectNo;
                data.MaintenanceTypeCode = vo.MaintenanceTypeCode;                
                data.PeriodFrom = Convert.ToDateTime(vo.PeriodFrom, CultureInfo.InvariantCulture);
                data.PeriodTo = Convert.ToDateTime(vo.PeriodTo, CultureInfo.InvariantCulture);               

                string[] fields = vo.BerthKey.Split('.');
                string portCode = fields[0];
                string quayCode = fields[1];
                string berthCode = fields[2];
                data.MaintPortCode = portCode;
                data.MaintQuayCode = quayCode;
                data.MaintBerthCode = berthCode;

                fields = vo.FromBollardKey.Split('.');
                portCode = fields[0];
                quayCode = fields[1];
                berthCode = fields[2];
                string bollardCode = fields[3];
                data.FromPortCode = portCode;
                data.FromQuayCode = quayCode;
                data.FromBerthCode = berthCode;
                data.FromBollard = bollardCode;

                fields = vo.ToBollardKey.Split('.');
                portCode = fields[0];
                quayCode = fields[1];
                berthCode = fields[2];
                bollardCode = fields[3];
                data.ToPortCode = portCode;
                data.ToQuayCode = quayCode;
                data.ToBerthCode = berthCode;
                data.ToBollard = bollardCode;

                data.OccupationTypeCode = vo.OccupationTypeCode;
                data.Precinct = vo.Precinct;
                data.DisciplineCode = vo.DisciplineCode;
                data.SpecialConditions = vo.SpecialConditions;
                data.Description = vo.Description;
                data.RecordStatus = vo.RecordStatus;
                data.CreatedBy = vo.CreatedBy;
                data.CreatedDate = vo.CreatedDate;
                data.ModifiedBy = vo.ModifiedBy;
                data.ModifiedDate = vo.ModifiedDate;
                data.MaintenanceType = vo.MaintenanceType;
                data.BerthMaintenanceNo = vo.BerthMaintenanceNo;
                data.BerthName = vo.BerthName;
                data.BollardsFrom = vo.BollardsFrom;
                data.BollardsTo = vo.BollardsTo;
                data.ReferenceNo = vo.BerthMaintenanceNo;
                data.WorkflowInstanceId = (vo.WorkflowInstanceId == 0 ? null : vo.WorkflowInstanceId);
            }
         
            return data;
        }
    }
}
     



