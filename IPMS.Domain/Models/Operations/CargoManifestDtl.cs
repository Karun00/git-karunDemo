using System;
using System.Collections.Generic;
using Core.Repository;
using System.Runtime.Serialization;

namespace IPMS.Domain.Models
{
    public partial class CargoManifestDtl:EntityBase
    {
        [DataMember]
        public int CargoManifestDtlID { get; set; }
        [DataMember]
        public int CargoManifestID { get; set; }
        [DataMember]
        public string CargoTypeCode { get; set; }
        [DataMember]
        public decimal Quantity { get; set; }
        [DataMember]
        public string UOMCode { get; set; }
        [DataMember]
        public decimal OutTurn { get; set; }
        [DataMember]
        public string RecordStatus { get; set; }
        [DataMember]
        public int CreatedBy { get; set; }
        [DataMember]
        public Nullable<System.DateTime> CreatedDate { get; set; }
        [DataMember]
        public Nullable<int> ModifiedBy { get; set; }
        [DataMember]
        public Nullable<System.DateTime> ModifiedDate { get; set; }

        public virtual CargoManifest CargoManifest { get; set; }
        public virtual SubCategory SubCategory { get; set; }
        public virtual User User { get; set; }
        public virtual User User1 { get; set; }
        public virtual SubCategory SubCategory1 { get; set; }
    }
}