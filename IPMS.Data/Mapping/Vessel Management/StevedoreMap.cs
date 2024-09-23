using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;
namespace IPMS.Data.Mapping
{
    public class StevedoreMap : EntityTypeConfiguration<Stevedore>
    {
        public StevedoreMap()
        {
            // Primary Key
            this.HasKey(t => t.StevedoreID);

            // Properties
            this.Property(t => t.NASASAMember)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.QualifiedFirstAiderPerShift)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.QualifiedFirstAiderPerShift50)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.FireFightingEmployeesTrained)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.SafetyOfficers)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.QualifiedExperiencedRiggers)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.VehicleOperators)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.LiftingEquipmentInspectors)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.Electricians)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.FlagSignalOperators)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.Lifting)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.EquipmentInspectors)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.HazardousCargoHandlers)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.SafetyHealthEnvironmentalRep)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.OperationalManager)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.LiftingEquipment)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.MotorizedEquipment)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.ElectricalEquipment)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.PalletsLoadSupportingDevices)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.HandProtectors)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.HeadProtectors)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.EyeFaceProtection)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.Footwear)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.ProtectiveClothing)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.HearingConservationEquipment)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.RespiratoryEquipment)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.SafetyHarnesses)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.OHSAAppointed)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.SafetyHealthEnvIncidents)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.OccuHealthSafetyTraining)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.AnnualRefresher)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.FirstAidBoxesAvailable)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.FacilitiesTreatInjuries)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.EmergencyContactDetails)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.EvacuationProceduresPracticed)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.HousekeepingFacilities)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.WorkerFacilities)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.StackingStorageFacilities)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.InspectionFacilities)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.QualifyPublicLiabilityInsu)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.CompiledRiskAssessment)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.CompiledPlanReducingRisk)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.RecordStatus)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("Stevedore");
            this.Property(t => t.StevedoreID).HasColumnName("StevedoreID");
            this.Property(t => t.LicenseRequestID).HasColumnName("LicenseRequestID");
            this.Property(t => t.NASASAMember).HasColumnName("NASASAMember");
            this.Property(t => t.QualifiedFirstAiderPerShift).HasColumnName("QualifiedFirstAiderPerShift");
            this.Property(t => t.QualifiedFirstAiderPerShift50).HasColumnName("QualifiedFirstAiderPerShift50");
            this.Property(t => t.FireFightingEmployeesTrained).HasColumnName("FireFightingEmployeesTrained");
            this.Property(t => t.SafetyOfficers).HasColumnName("SafetyOfficers");
            this.Property(t => t.QualifiedExperiencedRiggers).HasColumnName("QualifiedExperiencedRiggers");
            this.Property(t => t.VehicleOperators).HasColumnName("VehicleOperators");
            this.Property(t => t.LiftingEquipmentInspectors).HasColumnName("LiftingEquipmentInspectors");
            this.Property(t => t.Electricians).HasColumnName("Electricians");
            this.Property(t => t.FlagSignalOperators).HasColumnName("FlagSignalOperators");
            this.Property(t => t.Lifting).HasColumnName("Lifting");
            this.Property(t => t.EquipmentInspectors).HasColumnName("EquipmentInspectors");
            this.Property(t => t.HazardousCargoHandlers).HasColumnName("HazardousCargoHandlers");
            this.Property(t => t.SafetyHealthEnvironmentalRep).HasColumnName("SafetyHealthEnvironmentalRep");
            this.Property(t => t.OperationalManager).HasColumnName("OperationalManager");
            this.Property(t => t.LiftingEquipment).HasColumnName("LiftingEquipment");
            this.Property(t => t.MotorizedEquipment).HasColumnName("MotorizedEquipment");
            this.Property(t => t.ElectricalEquipment).HasColumnName("ElectricalEquipment");
            this.Property(t => t.PalletsLoadSupportingDevices).HasColumnName("PalletsLoadSupportingDevices");
            this.Property(t => t.HandProtectors).HasColumnName("HandProtectors");
            this.Property(t => t.HeadProtectors).HasColumnName("HeadProtectors");
            this.Property(t => t.EyeFaceProtection).HasColumnName("EyeFaceProtection");
            this.Property(t => t.Footwear).HasColumnName("Footwear");
            this.Property(t => t.ProtectiveClothing).HasColumnName("ProtectiveClothing");
            this.Property(t => t.HearingConservationEquipment).HasColumnName("HearingConservationEquipment");
            this.Property(t => t.RespiratoryEquipment).HasColumnName("RespiratoryEquipment");
            this.Property(t => t.SafetyHarnesses).HasColumnName("SafetyHarnesses");
            this.Property(t => t.OHSAAppointed).HasColumnName("OHSAAppointed");
            this.Property(t => t.SafetyHealthEnvIncidents).HasColumnName("SafetyHealthEnvIncidents");
            this.Property(t => t.OccuHealthSafetyTraining).HasColumnName("OccuHealthSafetyTraining");
            this.Property(t => t.AnnualRefresher).HasColumnName("AnnualRefresher");
            this.Property(t => t.FirstAidBoxesAvailable).HasColumnName("FirstAidBoxesAvailable");
            this.Property(t => t.FacilitiesTreatInjuries).HasColumnName("FacilitiesTreatInjuries");
            this.Property(t => t.EmergencyContactDetails).HasColumnName("EmergencyContactDetails");
            this.Property(t => t.EvacuationProceduresPracticed).HasColumnName("EvacuationProceduresPracticed");
            this.Property(t => t.HousekeepingFacilities).HasColumnName("HousekeepingFacilities");
            this.Property(t => t.WorkerFacilities).HasColumnName("WorkerFacilities");
            this.Property(t => t.StackingStorageFacilities).HasColumnName("StackingStorageFacilities");
            this.Property(t => t.InspectionFacilities).HasColumnName("InspectionFacilities");
            this.Property(t => t.QualifyPublicLiabilityInsu).HasColumnName("QualifyPublicLiabilityInsu");
            this.Property(t => t.CompiledRiskAssessment).HasColumnName("CompiledRiskAssessment");
            this.Property(t => t.CompiledPlanReducingRisk).HasColumnName("CompiledPlanReducingRisk");
            this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            // Relationships
            this.HasRequired(t => t.LicenseRequest)
                .WithMany(t => t.Stevedores)
                .HasForeignKey(d => d.LicenseRequestID);
            this.HasRequired(t => t.User)
                .WithMany(t => t.Stevedores)
                .HasForeignKey(d => d.CreatedBy);
            this.HasOptional(t => t.User1)
                .WithMany(t => t.Stevedores1)
                .HasForeignKey(d => d.ModifiedBy);

        }
    }
}
