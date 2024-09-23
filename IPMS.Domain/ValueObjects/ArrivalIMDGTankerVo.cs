using System;
namespace IPMS.Domain.ValueObjects
{
    /// <summary>
    /// Data Transfer Object for Arrival Notification IMDG Tanker
    /// </summary>
   public  class ArrivalIMDGTankerVo
    {
        public int ArrivalIMDGTankerID { get; set; }
        public string VCN { get; set; }
        public string Purpose { get; set; }
        public string Commodity { get; set; }
        public Nullable<decimal> Quantity { get; set; }
        //public int Quantity { get; set; }
        public string FromTank { get; set; }
        public string RecordStatus { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public System.DateTime ModifiedDate { get; set; }
    }
}
