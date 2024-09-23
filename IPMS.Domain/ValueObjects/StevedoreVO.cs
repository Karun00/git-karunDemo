using System;

namespace IPMS.Domain.ValueObjects
{
   /// <summary>
    /// Data Transfer Object for Stevedore
   /// </summary>
    public partial class StevedoreVO
    {
        
        public int StevedoreID { get; set; }
        
        public int LicenseRequestID { get; set; }
        
        public string NASASAMember { get; set; }
        
        public string QualifiedFirstAiderPerShift { get; set; }
        
        public string QualifiedFirstAiderPerShift50 { get; set; }
        
        public string FireFightingEmployeesTrained { get; set; }
        
        public string SafetyOfficers { get; set; }
        
        public string QualifiedExperiencedRiggers { get; set; }
        
        public string VehicleOperators { get; set; }
        
        public string LiftingEquipmentInspectors { get; set; }
        
        public string Electricians { get; set; }
        
        public string FlagSignalOperators { get; set; }
        
        public string Lifting { get; set; }
        
        public string EquipmentInspectors { get; set; }
        
        public string HazardousCargoHandlers { get; set; }
        
        public string SafetyHealthEnvironmentalRep { get; set; }
        
        public string OperationalManager { get; set; }
        
        public string LiftingEquipment { get; set; }
        
        public string MotorizedEquipment { get; set; }
        
        public string ElectricalEquipment { get; set; }
        
        public string PalletsLoadSupportingDevices { get; set; }
        
        public string HandProtectors { get; set; }
        
        public string HeadProtectors { get; set; }
        
        public string EyeFaceProtection { get; set; }
        
        public string Footwear { get; set; }
        
        public string ProtectiveClothing { get; set; }
        
        public string HearingConservationEquipment { get; set; }
        
        public string RespiratoryEquipment { get; set; }
        
        public string SafetyHarnesses { get; set; }
        
        public string OHSAAppointed { get; set; }
        
        public string SafetyHealthEnvIncidents { get; set; }
        
        public string OccuHealthSafetyTraining { get; set; }
        
        public string AnnualRefresher { get; set; }
        
        public string FirstAidBoxesAvailable { get; set; }
        
        public string FacilitiesTreatInjuries { get; set; }
        
        public string EmergencyContactDetails { get; set; }
        
        public string EvacuationProceduresPracticed { get; set; }
        
        public string HousekeepingFacilities { get; set; }
        
        public string WorkerFacilities { get; set; }
        
        public string StackingStorageFacilities { get; set; }
        
        public string InspectionFacilities { get; set; }
        
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
