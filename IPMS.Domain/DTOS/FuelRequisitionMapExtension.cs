using System;
using System.Collections.Generic;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System.Globalization;

namespace IPMS.Domain.DTOS
{
    public static class FuelRequisitionMapExtension
    {
        public static List<FuelRequisitionVO> MapToDtoForFuelReceipt(this List<FuelRequisition> fuelrequests)
        {
            List<FuelRequisitionVO> fuelrequestvos = new List<FuelRequisitionVO>();
            if (fuelrequests != null)
            {
                foreach (var fuelrequest in fuelrequests)
                {
                    fuelrequestvos.Add(fuelrequest.MapToDtoForReceipts());
                }
            }
            return fuelrequestvos;
        }

        public static FuelRequisitionVO MapToDtoForReceipts(this FuelRequisition data)
        {
            FuelRequisitionVO fuelrequisitionvo = new FuelRequisitionVO();
            if (data != null)
            {
                fuelrequisitionvo.FuelRequisitionID = data.FuelRequisitionID;
                fuelrequisitionvo.PortCode = data.PortCode;
                fuelrequisitionvo.FuelRequistionNo = data.FuelRequistionNo;
                fuelrequisitionvo.CraftID = data.CraftID;
                fuelrequisitionvo.RequisitionDate = Convert.ToDateTime(data.RequisitionDate).ToString();
                fuelrequisitionvo.OilTypeCode = data.OilTypeCode;
                fuelrequisitionvo.GradeCode = data.GradeCode;
                fuelrequisitionvo.UOMCode = data.UOMCode;
                fuelrequisitionvo.Quantity = data.Quantity;
                fuelrequisitionvo.RequiredDate = Convert.ToDateTime(data.RequiredDate).ToString();
                fuelrequisitionvo.Remarks = data.Remarks;
                // fuelrequisitionvo.RecordStatus = data.RecordStatus;
                fuelrequisitionvo.CreatedBy = data.CreatedBy;
                fuelrequisitionvo.CreatedDate = data.CreatedDate;
                fuelrequisitionvo.ModifiedBy = data.ModifiedBy;
                fuelrequisitionvo.ModifiedDate = data.ModifiedDate;
                fuelrequisitionvo.CraftName = data.Craft.CraftName;
                fuelrequisitionvo.CraftCode = data.Craft.CraftCode;
                fuelrequisitionvo.CraftType = data.Craft.SubCategory3.SubCatName;
                fuelrequisitionvo.IMONo = data.Craft.IMONo;
                fuelrequisitionvo.OilType = data.SubCategory1 != null ? data.SubCategory1.SubCatName : null;
                fuelrequisitionvo.Grade = data.SubCategory != null ? data.SubCategory.SubCatName : null;
                fuelrequisitionvo.UOMName = data.SubCategory2.SubCatName;
                fuelrequisitionvo.WorkflowInstanceId = data.WorkflowInstanceId;
                fuelrequisitionvo.RequisitionStatus = data.WorkflowInstance.SubCategory != null ? data.WorkflowInstance.SubCategory.SubCatName : null;
                fuelrequisitionvo.FuelReceiptStatus = "Pending";
                fuelrequisitionvo.OwnersName = data.User != null ? data.User.FirstName : "";
                fuelrequisitionvo.FuelRequistionType = data.FuelRequistionType;
                if (data.FuelReceipts != null)
                {
                    foreach (var item in data.FuelReceipts)
                    {
                        // fuelreceiptsVoList.Add(item.MapToDTO());
                        fuelrequisitionvo.FuelReceiptID = item.FuelReceiptID;
                        fuelrequisitionvo.SupplyingModeCode = item.SupplyingModeCode;
                        fuelrequisitionvo.ModeID = item.ModeID;
                        fuelrequisitionvo.QuayCode = item.QuayCode;
                        fuelrequisitionvo.BerthCode = item.BerthCode;
                        fuelrequisitionvo.BerthName = item.Berth != null ? item.Berth.BerthName : null;
                        fuelrequisitionvo.ReceiptDate = item.ReceiptDate != null ? item.ReceiptDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture) : null;
                        fuelrequisitionvo.StartReading = Convert.ToString(item.StartReading, CultureInfo.InvariantCulture);
                        fuelrequisitionvo.FinishReading = Convert.ToString(item.FinishReading, CultureInfo.InvariantCulture);
                        fuelrequisitionvo.ReceivedQty = Convert.ToString(item.ReceivedQty, CultureInfo.InvariantCulture);
                        fuelrequisitionvo.ReceivedTempCelsius = Convert.ToString(item.ReceivedTempCelsius, CultureInfo.InvariantCulture);
                        fuelrequisitionvo.VCF = Convert.ToString(item.VCF, CultureInfo.InvariantCulture);
                        fuelrequisitionvo.Qttyat20Degree1 = Convert.ToString(item.Qttyat20Degree1, CultureInfo.InvariantCulture);
                        fuelrequisitionvo.Qttyat20Degree2 = Convert.ToString(item.Qttyat20Degree2, CultureInfo.InvariantCulture);
                        fuelrequisitionvo.PumpStartDateTime = Convert.ToString(item.PumpStartDateTime, CultureInfo.InvariantCulture);
                        fuelrequisitionvo.PumpFinishDateTime = Convert.ToString(item.PumpFinishDateTime, CultureInfo.InvariantCulture);
                        fuelrequisitionvo.Densityat15DegCelsius = Convert.ToString(item.Densityat15DegCelsius, CultureInfo.InvariantCulture);
                        fuelrequisitionvo.Densityat20DegCelsius = Convert.ToString(item.Densityat20DegCelsius, CultureInfo.InvariantCulture);
                        fuelrequisitionvo.FlashPoint = Convert.ToString(item.FlashPoint, CultureInfo.InvariantCulture);
                        fuelrequisitionvo.BatchNo = item.BatchNo;
                        fuelrequisitionvo.KinematicViscat50DegCelsius = Convert.ToString(item.KinematicViscat50DegCelsius, CultureInfo.InvariantCulture);
                        fuelrequisitionvo.WaterContent = Convert.ToString(item.WaterContent, CultureInfo.InvariantCulture);
                        fuelrequisitionvo.SulphurContent = Convert.ToString(item.SulphurContent, CultureInfo.InvariantCulture);
                        fuelrequisitionvo.Supplier = item.Supplier;
                        fuelrequisitionvo.Flag = item.Flag;
                        fuelrequisitionvo.Other = item.Other;
                        fuelrequisitionvo.FuelGradeCode = item.GradeCode;
                        fuelrequisitionvo.FuelRemarks = item.Remarks;
                        fuelrequisitionvo.FuelReceiptNo = item.FuelReceiptNo;
                        //  fuelrequisitionvo.RecordStatus = item.RecordStatus;
                        fuelrequisitionvo.FuelReceiptStatus = item.WorkflowInstance.SubCategory.SubCatName;
                        fuelrequisitionvo.WorkflowInstanceId = item.WorkflowInstance.WorkflowInstanceId;
                        fuelrequisitionvo.FuelRequistionType = data.FuelRequistionType;
                    }
                    // fuelrequisitionvo.FuelReceipts = data.FuelReceipts.MapToDTO();
                }
            }

            return fuelrequisitionvo;
        }


        public static FuelRequisitionVO MapToDto(this FuelRequisition data)
        {
            FuelRequisitionVO fuelrequisitionvo = new FuelRequisitionVO();
            if (data != null)
            {                
                fuelrequisitionvo.FuelRequisitionID = data.FuelRequisitionID;
                fuelrequisitionvo.PortCode = data.PortCode;
                fuelrequisitionvo.FuelRequistionNo = data.FuelRequistionNo;
                fuelrequisitionvo.CraftID = data.CraftID;
                fuelrequisitionvo.RequisitionDate = Convert.ToDateTime(data.RequisitionDate).ToString();
                fuelrequisitionvo.OilTypeCode = data.OilTypeCode;
                fuelrequisitionvo.GradeCode = data.GradeCode;
                fuelrequisitionvo.UOMCode = data.UOMCode;
                fuelrequisitionvo.Quantity = data.Quantity;
                fuelrequisitionvo.RequiredDate = Convert.ToDateTime(data.RequiredDate).ToString();
                fuelrequisitionvo.Remarks = data.Remarks;
                fuelrequisitionvo.RecordStatus = data.RecordStatus;
                fuelrequisitionvo.CreatedBy = data.CreatedBy;
                fuelrequisitionvo.CreatedDate = data.CreatedDate;
                fuelrequisitionvo.ModifiedBy = data.ModifiedBy;
                fuelrequisitionvo.ModifiedDate = data.ModifiedDate;
            }
            return fuelrequisitionvo;
        }
        public static FuelRequisition MapToEntity(this FuelRequisitionVO vo)
        {
            FuelRequisition fuelrequisition = new FuelRequisition();
            if (vo != null)
            {
                fuelrequisition.FuelRequisitionID = vo.FuelRequisitionID;
                fuelrequisition.PortCode = vo.PortCode;
                fuelrequisition.FuelRequistionNo = vo.FuelRequistionNo;
                fuelrequisition.CraftID = vo.CraftID;
                fuelrequisition.RequisitionDate = DateTime.Parse(Convert.ToDateTime(vo.RequisitionDate, CultureInfo.InvariantCulture).ToString(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
                fuelrequisition.OilTypeCode = (!string.IsNullOrEmpty(vo.OilTypeCode) ? vo.OilTypeCode : null);
                fuelrequisition.GradeCode = (!string.IsNullOrEmpty(vo.GradeCode) ? vo.GradeCode : null);
                fuelrequisition.UOMCode = vo.UOMCode;
                fuelrequisition.Quantity = vo.Quantity;
                fuelrequisition.RequiredDate = DateTime.Parse(Convert.ToDateTime(vo.RequiredDate, CultureInfo.InvariantCulture).ToString(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
                fuelrequisition.Remarks = vo.Remarks;
                fuelrequisition.RecordStatus = vo.RecordStatus;
                fuelrequisition.CreatedBy = vo.CreatedBy;
                fuelrequisition.CreatedDate = vo.CreatedDate;
                fuelrequisition.ModifiedBy = vo.ModifiedBy;
                fuelrequisition.ModifiedDate = vo.ModifiedDate;
                fuelrequisition.CraftName = vo.CraftName;
                fuelrequisition.OilType = (!string.IsNullOrEmpty(vo.OilType) ? vo.OilType : null);
                fuelrequisition.Grade = (!string.IsNullOrEmpty(vo.Grade) ? vo.Grade : null);
                fuelrequisition.RequisitionNo = vo.FuelRequistionNo;
                fuelrequisition.WorkflowInstanceId = (vo.WorkflowInstanceId == 0 ? null : vo.WorkflowInstanceId);
                fuelrequisition.FuelRequistionType = vo.FuelRequistionType;
            }
          
            return fuelrequisition;


        }
    }
}
