using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;
namespace IPMS.Data.Mapping
{
    public class BunkeringMap : EntityTypeConfiguration<Bunkering>
    {
        public BunkeringMap()
        {
            // Primary Key
            this.HasKey(t => t.BunkeringID);

            // Properties
            this.Property(t => t.ProvideBunkeringPorts)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.GenlHealthSafetyCertificate)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.EmployeesSelfInflating)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.RecordStatus)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);


            this.Property(t => t.QualifyPublicLiabilityInsu)
                .IsFixedLength()
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("Bunkering");
            this.Property(t => t.BunkeringID).HasColumnName("BunkeringID");
            this.Property(t => t.LicenseRequestID).HasColumnName("LicenseRequestID");
            this.Property(t => t.ProvideBunkeringPorts).HasColumnName("ProvideBunkeringPorts");
            this.Property(t => t.YearsProvidingBunkering).HasColumnName("YearsProvidingBunkering");
            this.Property(t => t.QualifyPublicLiabilityInsu).HasColumnName("QualifyPublicLiabilityInsu");
            this.Property(t => t.GenlHealthSafetyCertificate).HasColumnName("GenlHealthSafetyCertificate");
            this.Property(t => t.EmployeesSelfInflating).HasColumnName("EmployeesSelfInflating");
            this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            // Relationships
            this.HasRequired(t => t.User)
                .WithMany(t => t.Bunkerings)
                .HasForeignKey(d => d.CreatedBy);
            this.HasRequired(t => t.LicenseRequest)
                .WithMany(t => t.Bunkerings)
                .HasForeignKey(d => d.LicenseRequestID);
            this.HasOptional(t => t.User1)
                .WithMany(t => t.Bunkerings1)
                .HasForeignKey(d => d.ModifiedBy);

        }
    }
}
