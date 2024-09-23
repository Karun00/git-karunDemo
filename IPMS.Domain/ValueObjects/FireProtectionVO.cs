using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace IPMS.Domain.ValueObjects
{
    /// <summary>
    /// Data Transfer Object for FireProtection
    /// </summary>
        public partial class FireProtectionVO
    {
        
        public int FireProtectionID { get; set; }
        
        public int LicenseRequestID { get; set; }
        
        public string HighRiskLicense { get; set; }
        
        public int YearsProvidingProtection { get; set; }
        
        public string EmployeesApplQualifications { get; set; }
        
        public string SAQAAccreditedBody { get; set; }
        
        public string BasicMarineFireFightingCerti { get; set; }
        
        public string Level1FirstAidCertificate { get; set; }
        
        public string BreathingApparatusCertificate { get; set; }
        
        public string GenlHealthSafetyCertificate { get; set; }
        
        public string ApprenticeshipProgramme { get; set; }
        
        public string EquipmentRegisterTestCerti { get; set; }
        
        public string HardHat { get; set; }
        
        public string SafetyShoes { get; set; }
        
        public string ReflectiveJacket { get; set; }
        
        public string SelfInflatingLifeJacket { get; set; }
        
        public string FireHelmet { get; set; }
        
        public string FireCoat { get; set; }
        
        public string QualifyPublicLiabilityInsu { get; set; }
        
        public string CompiledRiskAssessment { get; set; }
        
        public string CompiledPlanReducingRisk { get; set; }
        
        public string RecordStatus { get; set; }
        
        public int CreatedBy { get; set; }
        
        public System.DateTime CreatedDate { get; set; }
        
        public Nullable<int> ModifiedBy { get; set; }
        
        public System.DateTime ModifiedDate { get; set; }
       // public  LicenseRequestVO LicenseRequest { get; set; }
   
    }
}
