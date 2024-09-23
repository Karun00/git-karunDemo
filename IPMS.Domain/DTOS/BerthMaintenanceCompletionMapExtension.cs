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
    public static class BerthMaintenanceCompletionMapExtension
    {
        public static BerthMaintenanceCompletionVO MapToDto(this BerthMaintenanceCompletion data)
        {
            BerthMaintenanceCompletionVO BMCVO = new BerthMaintenanceCompletionVO();
            if (data != null)
            {
                BMCVO.BerthMaintenanceCompletionID = data.BerthMaintenanceCompletionID;
                BMCVO.BerthMaintenanceID = data.BerthMaintenanceID;
                BMCVO.CompletionDateTime = data.CompletionDateTime.ToString();
                BMCVO.observation = data.observation;
                BMCVO.RecordStatus = data.RecordStatus;
                BMCVO.CreatedBy = data.CreatedBy;
                BMCVO.CreatedDate = data.CreatedDate;
                BMCVO.ModifiedBy = data.ModifiedBy;
                BMCVO.ModifiedDate = data.ModifiedDate;
            }
            return BMCVO;
        }

        public static BerthMaintenanceCompletion MapToEntity(this BerthMaintenanceCompletionVO vo)
        {
            BerthMaintenanceCompletion data = new BerthMaintenanceCompletion();
            if (vo != null)
            {
                data.BerthMaintenanceCompletionID = vo.BerthMaintenanceCompletionID;
                data.BerthMaintenanceID = vo.BerthMaintenanceID;
                data.CompletionDateTime = DateTime.Parse(vo.CompletionDateTime, CultureInfo.InvariantCulture);
                data.observation = vo.observation;
                data.RecordStatus = vo.RecordStatus;
                data.CreatedBy = vo.CreatedBy;
                data.CreatedDate = vo.CreatedDate;
                data.ModifiedBy = vo.ModifiedBy;
                data.ModifiedDate = vo.ModifiedDate;
                data.ReferenceNo = vo.BerthMaintenanceNo;
                data.MaintenanceType = vo.MaintenanceTypeCode;
                data.BerthName = vo.MaintBerthCode;
                data.BollardsFrom = vo.FromBollard;
                data.BollardsTo = vo.ToBollard;
                data.WorkflowInstanceId = (vo.WorkflowInstanceId == 0 ? null : vo.WorkflowInstanceId);
            }

            return data;
        }
    }   
}
