using System;
using System.Collections.Generic;
using Core.Repository;
using System.Runtime.Serialization;

namespace IPMS.Domain.Models
{
    public partial class Bunkering : EntityBase
    {
        public int BunkeringID { get; set; }
        public int LicenseRequestID { get; set; }
        public string ProvideBunkeringPorts { get; set; }
        public int YearsProvidingBunkering { get; set; }
        public string GenlHealthSafetyCertificate { get; set; }
        public string QualifyPublicLiabilityInsu { get; set; }

        
        public string EmployeesSelfInflating { get; set; }
        public string RecordStatus { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public virtual User User { get; set; }
        public virtual LicenseRequest LicenseRequest { get; set; }
        public virtual User User1 { get; set; }
    }
}
