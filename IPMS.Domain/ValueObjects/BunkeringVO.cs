using System;

namespace IPMS.Domain.ValueObjects
{
    /// <summary>
    /// Data Transfer Object for Bunkering
    /// </summary>
    public partial class BunkeringVO
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

    }
}
