using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;
namespace IPMS.Data.Mapping
{
    public class FloatingCraneMap : EntityTypeConfiguration<FloatingCrane>
    {
        public FloatingCraneMap()
        {
            // Primary Key
            this.HasKey(t => t.FloatingCraneID);

            // Properties
            this.Property(t => t.QualificationsCompetencies)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.EmployQualifiedTrainedPers)
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

            this.Property(t => t.RecordStatus)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("FloatingCrane");
            this.Property(t => t.FloatingCraneID).HasColumnName("FloatingCraneID");
            this.Property(t => t.LicenseRequestID).HasColumnName("LicenseRequestID");
            this.Property(t => t.QualificationsCompetencies).HasColumnName("QualificationsCompetencies");
            this.Property(t => t.EmployQualifiedTrainedPers).HasColumnName("EmployQualifiedTrainedPers");
            this.Property(t => t.HardHat).HasColumnName("HardHat");
            this.Property(t => t.SafetyShoes).HasColumnName("SafetyShoes");
            this.Property(t => t.ReflectiveJacket).HasColumnName("ReflectiveJacket");
            this.Property(t => t.SelfInflatingLifeJacket).HasColumnName("SelfInflatingLifeJacket");
            this.Property(t => t.QualifyPublicLiabilityInsu).HasColumnName("QualifyPublicLiabilityInsu");
            this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            // Relationships
            this.HasRequired(t => t.User)
                .WithMany(t => t.FloatingCranes)
                .HasForeignKey(d => d.CreatedBy);
            this.HasRequired(t => t.LicenseRequest)
                .WithMany(t => t.FloatingCranes)
                .HasForeignKey(d => d.LicenseRequestID);
            this.HasOptional(t => t.User1)
                .WithMany(t => t.FloatingCranes1)
                .HasForeignKey(d => d.ModifiedBy);

        }
    }
}
