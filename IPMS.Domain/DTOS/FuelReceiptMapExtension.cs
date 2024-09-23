using System;
using System.Collections.Generic;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System.Globalization;

namespace IPMS.Domain.DTOS
{
    public static class FuelReceiptMapExtension
    {

       
        public static FuelReceiptVO MapToDTO(this FuelReceipt data)
        {
            FuelReceiptVO fuelreceiptvo = new FuelReceiptVO();
            if (data != null)
            {
                fuelreceiptvo.FuelReceiptID = data.FuelReceiptID;
                fuelreceiptvo.FuelRequisitionID = data.FuelRequisitionID;
                fuelreceiptvo.SupplyingModeCode = data.SupplyingModeCode;
                fuelreceiptvo.ModeID = data.ModeID;
                fuelreceiptvo.PortCode = data.PortCode;
                fuelreceiptvo.QuayCode = data.QuayCode;
                fuelreceiptvo.BerthCode = data.BerthCode;
                fuelreceiptvo.ReceiptDate = Convert.ToDateTime(data.ReceiptDate).ToString();
                fuelreceiptvo.FuelGradeCode = data.GradeCode;
                fuelreceiptvo.StartReading = data.StartReading;
                fuelreceiptvo.FinishReading = data.FinishReading;
                fuelreceiptvo.ReceivedQty = data.ReceivedQty;
                fuelreceiptvo.ReceivedTempCelsius = data.ReceivedTempCelsius;
                fuelreceiptvo.VCF = data.VCF;
                fuelreceiptvo.Qttyat20Degree1 = data.Qttyat20Degree1;
                fuelreceiptvo.Qttyat20Degree2 = data.Qttyat20Degree2;
                fuelreceiptvo.PumpStartDateTime = Convert.ToDateTime(data.PumpStartDateTime).ToString();
                fuelreceiptvo.PumpFinishDateTime = Convert.ToDateTime(data.PumpFinishDateTime).ToString();
                fuelreceiptvo.Densityat15DegCelsius = data.Densityat15DegCelsius;
                fuelreceiptvo.Densityat20DegCelsius = data.Densityat20DegCelsius;
                fuelreceiptvo.FlashPoint = data.FlashPoint;
                fuelreceiptvo.BatchNo = data.BatchNo;
                fuelreceiptvo.KinematicViscat50DegCelsius = data.KinematicViscat50DegCelsius;
                fuelreceiptvo.WaterContent = data.WaterContent;
                fuelreceiptvo.SulphurContent = data.SulphurContent;
                fuelreceiptvo.Supplier = data.Supplier;
                fuelreceiptvo.FuelRemarks = data.Remarks;
                fuelreceiptvo.RecordStatus = data.RecordStatus;
                fuelreceiptvo.CreatedBy = data.CreatedBy;
                fuelreceiptvo.CreatedDate = data.CreatedDate;
                fuelreceiptvo.ModifiedBy = data.ModifiedBy;
                fuelreceiptvo.ModifiedDate = data.ModifiedDate;
                fuelreceiptvo.Flag = data.Flag;
                fuelreceiptvo.Other = data.Other;
                fuelreceiptvo.FuelReceiptNo = data.FuelReceiptNo;
            }
            return fuelreceiptvo;
        }
        public static FuelReceipt MapToEntity(this FuelReceiptVO vo)
        {
            FuelReceipt fuelreceipt = new FuelReceipt();
            if (vo != null)
            {
                fuelreceipt.FuelReceiptID = vo.FuelReceiptID;
                fuelreceipt.FuelRequisitionID = vo.FuelRequisitionID;
                fuelreceipt.SupplyingModeCode = vo.SupplyingModeCode;
                fuelreceipt.ModeID = vo.ModeID;
                fuelreceipt.PortCode = vo.PortCode;
                fuelreceipt.QuayCode = vo.QuayCode;
                fuelreceipt.BerthCode = vo.BerthCode;
                fuelreceipt.ReceiptDate = DateTime.Parse(Convert.ToDateTime(vo.ReceiptDate, CultureInfo.InvariantCulture).ToString(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
                fuelreceipt.GradeCode = vo.FuelGradeCode;
                fuelreceipt.StartReading = vo.StartReading;
                fuelreceipt.FinishReading = vo.FinishReading;
                fuelreceipt.ReceivedQty = vo.ReceivedQty;
                fuelreceipt.ReceivedTempCelsius = vo.ReceivedTempCelsius;
                fuelreceipt.VCF = vo.VCF;
                fuelreceipt.Qttyat20Degree1 = vo.Qttyat20Degree1;
                fuelreceipt.Qttyat20Degree2 = vo.Qttyat20Degree2;
                fuelreceipt.PumpStartDateTime = DateTime.Parse(Convert.ToDateTime(vo.PumpStartDateTime, CultureInfo.InvariantCulture).ToString(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
                fuelreceipt.PumpFinishDateTime = DateTime.Parse(Convert.ToDateTime(vo.PumpFinishDateTime, CultureInfo.InvariantCulture).ToString(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
                fuelreceipt.Densityat15DegCelsius = vo.Densityat15DegCelsius;
                fuelreceipt.Densityat20DegCelsius = vo.Densityat20DegCelsius;
                fuelreceipt.FlashPoint = vo.FlashPoint;
                fuelreceipt.BatchNo = vo.BatchNo;
                fuelreceipt.KinematicViscat50DegCelsius = vo.KinematicViscat50DegCelsius;
                fuelreceipt.WaterContent = vo.WaterContent;
                fuelreceipt.SulphurContent = vo.SulphurContent;
                fuelreceipt.Supplier = vo.Supplier;
                fuelreceipt.Remarks = vo.FuelRemarks;
                fuelreceipt.RecordStatus = vo.RecordStatus;
                fuelreceipt.CreatedBy = vo.CreatedBy;
                fuelreceipt.CreatedDate = vo.CreatedDate;
                fuelreceipt.ModifiedBy = vo.ModifiedBy;
                fuelreceipt.ModifiedDate = vo.ModifiedDate;
                fuelreceipt.Flag = vo.Flag;
                fuelreceipt.Other = vo.Other;
                fuelreceipt.FuelReceiptNo = vo.FuelReceiptNo;

                fuelreceipt.BerthName = vo.BerthName;
                fuelreceipt.CraftName = vo.CraftName;
                fuelreceipt.OilType = vo.OilType;
                fuelreceipt.Grade = vo.Grade;
                fuelreceipt.RequisitionNo = vo.FuelRequistionNo;
                fuelreceipt.RequiredQuantity = vo.Quantity;
                fuelreceipt.ReceivedQuantity = vo.ReceivedQty;
                fuelreceipt.WorkflowInstanceId = (vo.WorkflowInstanceId == 0 ? null : vo.WorkflowInstanceId);
            }

            return fuelreceipt;
        }
       
 

    }
}
