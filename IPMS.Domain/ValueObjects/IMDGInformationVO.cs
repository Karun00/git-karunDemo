using System;

namespace IPMS.Domain.ValueObjects
{
    public class IMDGInformationVO
    {
        public int IMDGInformationID { get; set; }
        public string VCN { get; set; }
        public string ClassCode { get; set; }
        public string CargoCode { get; set; }
        public string UNNo { get; set; }
        public string Others { get; set; }
        public string Purpose { get; set; }
        public Nullable<decimal> Quantity { get; set; }
        public Nullable<decimal> NoofContainer { get; set; }
        public string RecordStatus { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public System.DateTime ModifiedDate { get; set; }
    }
}
