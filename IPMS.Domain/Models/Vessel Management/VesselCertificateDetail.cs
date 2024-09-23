using System;
using System.Collections.Generic;
using Core.Repository;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace IPMS.Domain.Models
{
    public partial class VesselCertificateDetail : EntityBase
    {
        [Key]
        public int VACERTID { get; set; }
        public int VesselID { get; set; }
        public string CertificateName { get; set; }
        public string CertificateNo { get; set; }
        public Nullable<System.DateTime> DateOfIssue { get; set; }
        public Nullable<System.DateTime> DateOfValidity { get; set; }
        public virtual SubCategory SubCategory { get; set; }
        public virtual Vessel Vessel { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public virtual User User { get; set; }
        public virtual User User1 { get; set; }
    }
}
