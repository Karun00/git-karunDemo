using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Domain.DTOS
{
    public static class CraftOutOfCommissionMapExtension
    {
        public static CraftOutOfCommissionVO MapToDTO(this CraftOutOfCommission data)
        {
            CraftOutOfCommissionVO CCVO = new CraftOutOfCommissionVO();
            if (data != null)
            {
                CCVO.CraftOutOfCommissionID = data.CraftOutOfCommissionID;
                CCVO.CraftID = data.CraftID;
                CCVO.ExpectedDuration = data.ExpectedDuration;
                CCVO.Reason = data.Reason;
                CCVO.Remarks = data.Remarks;
                CCVO.CraftCommissionStatus = data.CraftCommissionStatus;
                CCVO.RecordStatus = data.RecordStatus;
                CCVO.CreatedBy = data.CreatedBy;
                CCVO.CreatedDate = data.CreatedDate;
                CCVO.ModifiedBy = data.ModifiedBy;
                CCVO.ModifiedDate = data.ModifiedDate;
                CCVO.OutOfCommissionDate = data.OutOfCommissionDate;
                CCVO.BackToCommissionDate = data.BackToCommissionDate;
            }
            return CCVO;
        }
        public static CraftOutOfCommission MapToEntity(this CraftOutOfCommissionVO CCVO)
        {
            CraftOutOfCommission data = new CraftOutOfCommission();
            if (CCVO != null)
            {
                data.CraftOutOfCommissionID = CCVO.CraftOutOfCommissionID;
                data.CraftID = CCVO.CraftID;
                data.ExpectedDuration = CCVO.ExpectedDuration;
                data.Reason = CCVO.Reason;
                data.Remarks = CCVO.Remarks;
                data.CraftCommissionStatus = CCVO.CraftCommissionStatus;
                data.RecordStatus = CCVO.RecordStatus;
                data.CreatedBy = CCVO.CreatedBy;
                data.CreatedDate = CCVO.CreatedDate;
                data.ModifiedBy = CCVO.ModifiedBy;
                data.ModifiedDate = CCVO.ModifiedDate;
                data.OutOfCommissionDate = CCVO.OutOfCommissionDate;
                data.BackToCommissionDate = CCVO.BackToCommissionDate;
            }
            return data;
        }
    }
}
