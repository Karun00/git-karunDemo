using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;
namespace IPMS.Data.Mapping
{
    public class FireEquipmentMap : EntityTypeConfiguration<FireEquipment>
    {
        public FireEquipmentMap()
        {
            // Primary Key
            this.HasKey(t => t.FireEquipmentID);

            // Properties
            this.Property(t => t.MemberAssociationsBureaus)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.EquipmentTradersAssociation)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.AutomaticSprinklerInspection)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.FireDetectionInstallers)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.EquipInstallationMaintenance)
                .IsFixedLength()
                .HasMaxLength(1);

         

            this.Property(t => t.EmployeesApplQualifications)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.FireMaintenanceCertificate)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.SANS1475permit)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.DOFTASCertificate)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.GenlHealthSafetyCertificate)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.FireDivisionRegistration)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.EquipmentRegisterTestCerti)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.HardHat)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.SafetyShoes)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.ReflectiveJacket)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.SelfInflatingLifeJacket)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.QualifyPublicLiabilityInsu)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.RiskAssessmentReportDealing)
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
            this.ToTable("FireEquipment");
            this.Property(t => t.FireEquipmentID).HasColumnName("FireEquipmentID");
            this.Property(t => t.LicenseRequestID).HasColumnName("LicenseRequestID");
            this.Property(t => t.MemberAssociationsBureaus).HasColumnName("MemberAssociationsBureaus");
            this.Property(t => t.EquipmentTradersAssociation).HasColumnName("EquipmentTradersAssociation");
            this.Property(t => t.AutomaticSprinklerInspection).HasColumnName("AutomaticSprinklerInspection");
            this.Property(t => t.FireDetectionInstallers).HasColumnName("FireDetectionInstallers");
            this.Property(t => t.EquipInstallationMaintenance).HasColumnName("EquipInstallationMaintenance");
            this.Property(t => t.YearsProvidingEquipment).HasColumnName("YearsProvidingEquipment");
            this.Property(t => t.EmployeesApplQualifications).HasColumnName("EmployeesApplQualifications");
            this.Property(t => t.FireMaintenanceCertificate).HasColumnName("FireMaintenanceCertificate");
            this.Property(t => t.SANS1475permit).HasColumnName("SANS1475permit");
            this.Property(t => t.DOFTASCertificate).HasColumnName("DOFTASCertificate");
            this.Property(t => t.GenlHealthSafetyCertificate).HasColumnName("GenlHealthSafetyCertificate");
            this.Property(t => t.FireDivisionRegistration).HasColumnName("FireDivisionRegistration");
            this.Property(t => t.EquipmentRegisterTestCerti).HasColumnName("EquipmentRegisterTestCerti");
            this.Property(t => t.HardHat).HasColumnName("HardHat");
            this.Property(t => t.SafetyShoes).HasColumnName("SafetyShoes");
            this.Property(t => t.ReflectiveJacket).HasColumnName("ReflectiveJacket");
            this.Property(t => t.SelfInflatingLifeJacket).HasColumnName("SelfInflatingLifeJacket");
            this.Property(t => t.QualifyPublicLiabilityInsu).HasColumnName("QualifyPublicLiabilityInsu");
            this.Property(t => t.RiskAssessmentReportDealing).HasColumnName("RiskAssessmentReportDealing");
            this.Property(t => t.CompiledPlanReducingRisk).HasColumnName("CompiledPlanReducingRisk");
            this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            // Relationships
            this.HasRequired(t => t.User)
                .WithMany(t => t.FireEquipments)
                .HasForeignKey(d => d.CreatedBy);
            this.HasRequired(t => t.LicenseRequest)
                .WithMany(t => t.FireEquipments)
                .HasForeignKey(d => d.LicenseRequestID);
            this.HasOptional(t => t.User1)
                .WithMany(t => t.FireEquipments1)
                .HasForeignKey(d => d.ModifiedBy);

        }
    }
}
