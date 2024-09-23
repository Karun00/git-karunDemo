using System;
using System.Collections.Generic;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System.Globalization;

namespace IPMS.Domain.DTOS
{
    public static class FuelRequisitionApprovalMapExtension
    {
        public static FuelRequisitionApprovalVO MapToDto(this FuelRequisitionApproval data)
        {
            FuelRequisitionApprovalVO fuelrequisitionapprovalvo = new FuelRequisitionApprovalVO();
            if (data != null)
            {
                fuelrequisitionapprovalvo.FuelRequisitionApprovalID = data.FuelRequisitionApprovalID;
                fuelrequisitionapprovalvo.FuelRequisitionID = data.FuelRequisitionID;
                fuelrequisitionapprovalvo.WFStatus = data.WFStatus;
                fuelrequisitionapprovalvo.ApprovedBy = data.ApprovedBy;
                fuelrequisitionapprovalvo.ApprovedDate = Convert.ToDateTime(data.ApprovedDate).ToString();
                fuelrequisitionapprovalvo.RejectComments = data.RejectComments;
                fuelrequisitionapprovalvo.RecordStatus = data.RecordStatus;
                fuelrequisitionapprovalvo.CreatedBy = data.CreatedBy;
                fuelrequisitionapprovalvo.CreatedDate = data.CreatedDate;
                fuelrequisitionapprovalvo.ModifiedBy = data.ModifiedBy;
                fuelrequisitionapprovalvo.ModifiedDate = data.ModifiedDate;
            }
            return fuelrequisitionapprovalvo;
        }
        public static FuelRequisitionApproval MapToEntity(this FuelRequisitionApprovalVO vo)
        {
            FuelRequisitionApproval fuelrequisitionapproval = new FuelRequisitionApproval();
            if (vo != null)
            {
                fuelrequisitionapproval.FuelRequisitionApprovalID = vo.FuelRequisitionApprovalID;
                fuelrequisitionapproval.FuelRequisitionID = vo.FuelRequisitionID;
                fuelrequisitionapproval.WFStatus = vo.WFStatus;
                fuelrequisitionapproval.ApprovedBy = vo.ApprovedBy;
                fuelrequisitionapproval.ApprovedDate = DateTime.Parse(Convert.ToDateTime(vo.ApprovedDate, CultureInfo.InvariantCulture).ToString(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
                fuelrequisitionapproval.RejectComments = vo.RejectComments;
                fuelrequisitionapproval.RecordStatus = vo.RecordStatus;
                fuelrequisitionapproval.CreatedBy = vo.CreatedBy;
                fuelrequisitionapproval.CreatedDate = vo.CreatedDate;
                fuelrequisitionapproval.ModifiedBy = vo.ModifiedBy;
                fuelrequisitionapproval.ModifiedDate = vo.ModifiedDate;
            }
            return fuelrequisitionapproval;
        }
    }
}
