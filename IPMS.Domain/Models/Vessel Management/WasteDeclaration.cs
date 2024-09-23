using System;
using System.Collections.Generic;
using Core.Repository;
using System.Runtime.Serialization;
namespace IPMS.Domain.Models
{
    [DataContract]
    public partial class WasteDeclaration : EntityBase
    {       
        [DataMember]
        public int WasteDeclarationID { get; set; }
        [DataMember]
        public string VCN { get; set; }
        [DataMember]
        public string MarpolCode { get; set; }
        [DataMember]
        public string ClassCode { get; set; }
        [DataMember]
        public int LicenseRequestID { get; set; }
        [DataMember]
        public Nullable<decimal> Quantity { get; set; }
        [DataMember]
        public string DeclarationName { get; set; }
        [DataMember]
        public string Others { get; set; }
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
        public User User { get; set; }
        [DataMember]
        public User User1 { get; set; }
        [DataMember]
        public virtual ArrivalNotification ArrivalNotification { get; set; }
        [DataMember]
        public virtual SubCategory SubCategory { get; set; }
        [DataMember]
        public virtual Marpol Marpol { get; set; }
        [DataMember]
        public virtual LicenseRequest LicenseRequest { get; set; }
    }
}
