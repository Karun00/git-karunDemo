using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;
namespace IPMS.Data.Mapping
{
    public class PestControlMap : EntityTypeConfiguration<PestControl>
    {
        public PestControlMap()
        {
            // Primary Key
            this.HasKey(t => t.PestControlID);

            // Properties
            this.Property(t => t.AgricultureDeptrelevant)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.SAQACertificate)
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

            this.Property(t => t.FaceMasks)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.ProtectiveGloves)
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
            this.ToTable("PestControl");
            this.Property(t => t.PestControlID).HasColumnName("PestControlID");
            this.Property(t => t.LicenseRequestID).HasColumnName("LicenseRequestID");
            this.Property(t => t.AgricultureDeptrelevant).HasColumnName("AgricultureDeptrelevant");
            this.Property(t => t.SAQACertificate).HasColumnName("SAQACertificate");
            this.Property(t => t.EmployQualifiedTrainedPers).HasColumnName("EmployQualifiedTrainedPers");
            this.Property(t => t.HardHat).HasColumnName("HardHat");
            this.Property(t => t.SafetyShoes).HasColumnName("SafetyShoes");
            this.Property(t => t.ReflectiveJacket).HasColumnName("ReflectiveJacket");
            this.Property(t => t.SelfInflatingLifeJacket).HasColumnName("SelfInflatingLifeJacket");
            this.Property(t => t.FaceMasks).HasColumnName("FaceMasks");
            this.Property(t => t.ProtectiveGloves).HasColumnName("ProtectiveGloves");
            this.Property(t => t.QualifyPublicLiabilityInsu).HasColumnName("QualifyPublicLiabilityInsu");
            this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            // Relationships
            this.HasRequired(t => t.LicenseRequest)
                .WithMany(t => t.PestControls)
                .HasForeignKey(d => d.LicenseRequestID);
            this.HasRequired(t => t.User)
                .WithMany(t => t.PestControls)
                .HasForeignKey(d => d.CreatedBy);
            this.HasOptional(t => t.User1)
                .WithMany(t => t.PestControls1)
                .HasForeignKey(d => d.ModifiedBy);

        }
    }
}
