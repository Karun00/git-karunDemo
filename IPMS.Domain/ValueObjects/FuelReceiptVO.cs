using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace IPMS.Domain.ValueObjects
{
    [DataContract]
    public partial class FuelReceiptVO
    {
        [DataMember]
        public int FuelReceiptID { get; set; }
        [DataMember]
        public int FuelRequisitionID { get; set; }
        [DataMember]
        public string SupplyingModeCode { get; set; }
        [DataMember]
        public string ModeID { get; set; }
        [DataMember]
        public string PortCode { get; set; }
        [DataMember]
        public string QuayCode { get; set; }
        [DataMember]
        public string BerthCode { get; set; }
        [DataMember]
        public string ReceiptDate { get; set; }
        [DataMember]
        public string FuelGradeCode { get; set; }
        [DataMember]
        public long StartReading { get; set; }
        [DataMember]
        public long FinishReading { get; set; }
        [DataMember]
        public long ReceivedQty { get; set; }
        [DataMember]
        public decimal ReceivedTempCelsius { get; set; }
        [DataMember]
        public decimal VCF { get; set; }
        [DataMember]
        public decimal Qttyat20Degree1 { get; set; }
        [DataMember]
        public decimal Qttyat20Degree2 { get; set; }
        [DataMember]
        public string PumpStartDateTime { get; set; }
        [DataMember]
        public string PumpFinishDateTime { get; set; }
        [DataMember]
        public decimal Densityat15DegCelsius { get; set; }
        [DataMember]
        public decimal Densityat20DegCelsius { get; set; }
        [DataMember]
        public decimal FlashPoint { get; set; }
        [DataMember]
        public string BatchNo { get; set; }
        [DataMember]
        public decimal KinematicViscat50DegCelsius { get; set; }
        [DataMember]
        public decimal WaterContent { get; set; }
        [DataMember]
        public decimal SulphurContent { get; set; }
        [DataMember]
        public string Supplier { get; set; }
        [DataMember]
        public string FuelRemarks { get; set; }
        [DataMember]
        public string RecordStatus { get; set; }
        [DataMember]
        public int CreatedBy { get; set; }
        [DataMember]
        public System.DateTime CreatedDate { get; set; }
        [DataMember]
        public int ModifiedBy { get; set; }
        [DataMember]
        public System.DateTime ModifiedDate { get; set; }
        [DataMember]
        public string Flag { get; set; }
        [DataMember]
        public string Other { get; set; }
        [DataMember]
        public string FuelReceiptNo { get; set; }



        // Fuel Requistion

        [DataMember]
        public string FuelRequistionNo { get; set; }
        [DataMember]
        public int CraftID { get; set; }
        [DataMember]
        public string CraftCode { get; set; }
        [DataMember]
        public string CraftName { get; set; }
        [DataMember]
        public string CraftType { get; set; }
        [DataMember]
        public string IMONo { get; set; }

        [DataMember]
        public string OilTypeCode { get; set; }
        [DataMember]
        public string OilType { get; set; }     
        [DataMember]
        public decimal Quantity { get; set; }      
        [DataMember]
        public string RequisitionStatus { get; set; }
        [DataMember]
        public string UOMCode { get; set; }
        [DataMember]
        public string RequiredDate { get; set; }
        [DataMember]
        public string Grade { get; set; }
        [DataMember]
        public Nullable<int> WorkflowInstanceId { get; set; }
        [DataMember]
        public string BerthName { get; set; }
 
        



        [DataMember]
        public ICollection<SubCategoryVO> SupplyingModes { get; set; }
        [DataMember]
        public ICollection<SubCategoryVO> GradeTypes { get; set; }
        [DataMember]
        public ICollection<BerthVO> Berths { get; set; }


    }
}
