using System;
using System.Collections.Generic;
using Core.Repository;
using System.Runtime.Serialization;

namespace IPMS.Domain.Models
{
    public partial class FireEquipment : EntityBase
    {
        public int FireEquipmentID { get; set; }
        public int LicenseRequestID { get; set; }
        public string MemberAssociationsBureaus { get; set; }
        public string EquipmentTradersAssociation { get; set; }
        public string AutomaticSprinklerInspection { get; set; }
        public string FireDetectionInstallers { get; set; }
        public string EquipInstallationMaintenance { get; set; }
        public int YearsProvidingEquipment { get; set; }
        public string EmployeesApplQualifications { get; set; }
        public string FireMaintenanceCertificate { get; set; }
        public string SANS1475permit { get; set; }
        public string DOFTASCertificate { get; set; }
        public string GenlHealthSafetyCertificate { get; set; }
        public string FireDivisionRegistration { get; set; }
        public string EquipmentRegisterTestCerti { get; set; }
        public string HardHat { get; set; }
        public string SafetyShoes { get; set; }
        public string ReflectiveJacket { get; set; }
        public string SelfInflatingLifeJacket { get; set; }
        public string QualifyPublicLiabilityInsu { get; set; }
        public string RiskAssessmentReportDealing { get; set; }
        public string CompiledPlanReducingRisk { get; set; }
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
