using System;
using System.Collections.Generic;
using Core.Repository;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations.Schema;

namespace IPMS.Domain.Models
{
    public partial class FuelReceipt : EntityBase
    {
        public int FuelReceiptID { get; set; }
        public int FuelRequisitionID { get; set; }
        public string SupplyingModeCode { get; set; }
        public string ModeID { get; set; }
        public string Flag { get; set; }
        public string Other { get; set; }
        public string FuelReceiptNo { get; set; }
        public string PortCode { get; set; }
        public string QuayCode { get; set; }
        public string BerthCode { get; set; }
        public System.DateTime ReceiptDate { get; set; }
        public string GradeCode { get; set; }
        public long StartReading { get; set; }
        public long FinishReading { get; set; }
        public long ReceivedQty { get; set; }
        public decimal ReceivedTempCelsius { get; set; }
        public decimal VCF { get; set; }
        public decimal Qttyat20Degree1 { get; set; }
        public decimal Qttyat20Degree2 { get; set; }
        public System.DateTime PumpStartDateTime { get; set; }
        public System.DateTime PumpFinishDateTime { get; set; }
        public decimal Densityat15DegCelsius { get; set; }
        public decimal Densityat20DegCelsius { get; set; }
        public decimal FlashPoint { get; set; }
        public string BatchNo { get; set; }
        public decimal KinematicViscat50DegCelsius { get; set; }
        public decimal WaterContent { get; set; }
        public decimal SulphurContent { get; set; }
        public string Supplier { get; set; }
        public string Remarks { get; set; }
        public string RecordStatus { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public Nullable<int> WorkflowInstanceId { get; set; }
        public virtual Berth Berth { get; set; }
        public virtual User User { get; set; }
        public virtual FuelRequisition FuelRequisition { get; set; }
        public virtual SubCategory SubCategory { get; set; }
        public virtual User User1 { get; set; }
        public virtual SubCategory SubCategory1 { get; set; }
        public virtual WorkflowInstance WorkflowInstance { get; set; }


        [NotMapped]
        public string CraftName { get; set; }
        [NotMapped]
        public string OilType { get; set; }
        [NotMapped]
        public string Grade { get; set; }
        [NotMapped]
        public string RequisitionNo { get; set; }
        [NotMapped]
        public string BerthName { get; set; }
        [NotMapped]
        public decimal RequiredQuantity { get; set; }
        [NotMapped]
        public long ReceivedQuantity { get; set; }        
        
    }
}
