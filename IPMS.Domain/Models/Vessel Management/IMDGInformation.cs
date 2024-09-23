using System;
using System.Collections.Generic;
using Core.Repository;
using System.Runtime.Serialization;
namespace IPMS.Domain.Models
{
    public partial class IMDGInformation:EntityBase
    {
        public int IMDGInformationID { get; set; }
        public string VCN { get; set; }
        public string ClassCode { get; set; }
        public string CargoCode { get; set; }
        public string UNNo { get; set; }
        public string Purpose { get; set; }
        public string Others { get; set; }
        public Nullable<decimal> Quantity { get; set; }
        public Nullable<decimal> NoofContainer { get; set; }
        public string RecordStatus { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public virtual ArrivalNotification ArrivalNotification { get; set; }
        public virtual SubCategory SubCategoryPurpus { get; set; }
        public virtual SubCategory SubCategory { get; set; }
        public virtual SubCategory SubCategory1 { get; set; }
        public virtual User User { get; set; }
        public virtual User User1 { get; set; }
    }
}
