using System;
using System.Collections.Generic;
using Core.Repository;
using System.Runtime.Serialization;

namespace IPMS.Domain.Models
{
    public partial class PestControl : EntityBase
    {
        public int PestControlID { get; set; }
        public int LicenseRequestID { get; set; }
        public string AgricultureDeptrelevant { get; set; }
        public string SAQACertificate { get; set; }
        public string EmployQualifiedTrainedPers { get; set; }
        public string HardHat { get; set; }
        public string SafetyShoes { get; set; }
        public string ReflectiveJacket { get; set; }
        public string SelfInflatingLifeJacket { get; set; }
        public string FaceMasks { get; set; }
        public string ProtectiveGloves { get; set; }
        public string QualifyPublicLiabilityInsu { get; set; }
        public string RecordStatus { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public virtual LicenseRequest LicenseRequest { get; set; }
        public virtual User User { get; set; }
        public virtual User User1 { get; set; }
    }
}
