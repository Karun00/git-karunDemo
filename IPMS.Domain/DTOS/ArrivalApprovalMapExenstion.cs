using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Domain.DTOS
{
    public static class ArrivalApprovalMapExenstion
    {
        public static ArrivalApprovalVO MapToDTO(this ArrivalApproval data)
        {
            ArrivalApprovalVO arrivalapprVo = new ArrivalApprovalVO();
            if (data != null)
            {
                arrivalapprVo.VCN = data.VCN;
                arrivalapprVo.RoleID = data.RoleID;
                arrivalapprVo.WFStatus = data.WFStatus;
                arrivalapprVo.ApprovedBy = data.ApprovedBy;
                arrivalapprVo.ApprovedDate = data.ApprovedDate;
                arrivalapprVo.RejectComments = data.RejectComments;
                arrivalapprVo.RecordStatus = data.RecordStatus;
                arrivalapprVo.CreatedBy = data.CreatedBy;
                arrivalapprVo.CreatedDate = data.CreatedDate;
                arrivalapprVo.ModifiedBy = data.ModifiedBy;
                arrivalapprVo.ModifiedDate = data.ModifiedDate;
            }
            return arrivalapprVo;
        }
        public static ArrivalApproval MapToEntity(this ArrivalApprovalVO vo)
        {
            ArrivalApproval arrivalappr = new ArrivalApproval();
            if (vo != null)
            {
                arrivalappr.VCN = vo.VCN;
                arrivalappr.RoleID = vo.RoleID;
                arrivalappr.WFStatus = vo.WFStatus;
                arrivalappr.ApprovedBy = vo.ApprovedBy;
                arrivalappr.ApprovedDate = vo.ApprovedDate;
                arrivalappr.RejectComments = vo.RejectComments;
                arrivalappr.RecordStatus = vo.RecordStatus;
                arrivalappr.CreatedBy = vo.CreatedBy;
                arrivalappr.CreatedDate = vo.CreatedDate;
                arrivalappr.ModifiedBy = vo.ModifiedBy;
                arrivalappr.ModifiedDate = vo.ModifiedDate;
            }
            return arrivalappr;
        }

        public static List<ArrivalApproval> MapToEntity(this List<ArrivalApprovalVO> vos)
        {
            List<ArrivalApproval> arrivalapprEntities = new List<ArrivalApproval>();
            if (vos != null)
            {
                foreach (var arrivalapprEntitiesvo in vos)
                {
                    arrivalapprEntities.Add(arrivalapprEntitiesvo.MapToEntity());
                }
            }
            return arrivalapprEntities;
        }

        public static List<ArrivalApprovalVO> MapToDto(this List<ArrivalApproval> arrivalApprovals)
        {
            List<ArrivalApprovalVO> ArrivalApprovalVos = new List<ArrivalApprovalVO>();
            if (arrivalApprovals != null)
            {
                foreach (var ArrivalApproval in arrivalApprovals)
                {
                    ArrivalApprovalVos.Add(ArrivalApproval.MapToDTO());

                }
            }
            return ArrivalApprovalVos;
        }

    }
}
