using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;
namespace IPMS.Data.Mapping
{
    public class FireProtectionMap : EntityTypeConfiguration<FireProtection>
    {
        public FireProtectionMap()
        {
            // Primary Key
            this.HasKey(t => t.FireProtectionID);

            // Properties
            this.Property(t => t.HighRiskLicense)
                .IsFixedLength()
                .HasMaxLength(1);

        

            this.Property(t => t.EmployeesApplQualifications)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.SAQAAccreditedBody)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.BasicMarineFireFightingCerti)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.Level1FirstAidCertificate)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.BreathingApparatusCertificate)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.GenlHealthSafetyCertificate)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.ApprenticeshipProgramme)
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

            this.Property(t => t.FireHelmet)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.FireCoat)
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
            this.ToTable("FireProtection");
            this.Property(t => t.FireProtectionID).HasColumnName("FireProtectionID");
            this.Property(t => t.LicenseRequestID).HasColumnName("LicenseRequestID");
            this.Property(t => t.HighRiskLicense).HasColumnName("HighRiskLicense");
            this.Property(t => t.YearsProvidingProtection).HasColumnName("YearsProvidingProtection");
            this.Property(t => t.EmployeesApplQualifications).HasColumnName("EmployeesApplQualifications");
            this.Property(t => t.SAQAAccreditedBody).HasColumnName("SAQAAccreditedBody");
            this.Property(t => t.BasicMarineFireFightingCerti).HasColumnName("BasicMarineFireFightingCerti");
            this.Property(t => t.Level1FirstAidCertificate).HasColumnName("Level1FirstAidCertificate");
            this.Property(t => t.BreathingApparatusCertificate).HasColumnName("BreathingApparatusCertificate");
            this.Property(t => t.GenlHealthSafetyCertificate).HasColumnName("GenlHealthSafetyCertificate");
            this.Property(t => t.ApprenticeshipProgramme).HasColumnName("ApprenticeshipProgramme");
            this.Property(t => t.EquipmentRegisterTestCerti).HasColumnName("EquipmentRegisterTestCerti");
            this.Property(t => t.HardHat).HasColumnName("HardHat");
            this.Property(t => t.SafetyShoes).HasColumnName("SafetyShoes");
            this.Property(t => t.ReflectiveJacket).HasColumnName("ReflectiveJacket");
            this.Property(t => t.SelfInflatingLifeJacket).HasColumnName("SelfInflatingLifeJacket");
            this.Property(t => t.FireHelmet).HasColumnName("FireHelmet");
            this.Property(t => t.FireCoat).HasColumnName("FireCoat");
            this.Property(t => t.QualifyPublicLiabilityInsu).HasColumnName("QualifyPublicLiabilityInsu");
            this.Property(t => t.CompiledRiskAssessment).HasColumnName("CompiledRiskAssessment");
            this.Property(t => t.CompiledPlanReducingRisk).HasColumnName("CompiledPlanReducingRisk");
            this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            // Relationships
            this.HasRequired(t => t.User)
                .WithMany(t => t.FireProtections)
                .HasForeignKey(d => d.CreatedBy);
            this.HasRequired(t => t.LicenseRequest)
                .WithMany(t => t.FireProtections)
                .HasForeignKey(d => d.LicenseRequestID);
            this.HasOptional(t => t.User1)
                .WithMany(t => t.FireProtections1)
                .HasForeignKey(d => d.ModifiedBy);

        }
    }
}
