using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;

namespace IPMS.Data.Mapping
{
    public class VehiclePermitRequirementCodeMap : EntityTypeConfiguration<VehiclePermitRequirementCode>
    {
        public VehiclePermitRequirementCodeMap()
        {
            // Primary Key
            this.HasKey(t => t.PermitRequirementCodeID);

            // Properties
            this.Property(t => t.PermitRequirementCode)
                .IsRequired()
                .HasMaxLength(4);

            // Table & Column Mappings
            this.ToTable("VehiclePermitRequirementCodes");
            this.Property(t => t.PermitRequirementCodeID).HasColumnName("PermitRequirementCodeID");
            this.Property(t => t.VehiclePermitID).HasColumnName("VehiclePermitID");
            this.Property(t => t.PermitRequirementCode).HasColumnName("PermitRequirementCode");
            this.Property(t => t.PermitRequestID).HasColumnName("PermitRequestID");

            // Relationships
            this.HasRequired(t => t.PermitRequest)
                .WithMany(t => t.VehiclePermitRequirementCodes)
                .HasForeignKey(d => d.PermitRequestID);
            this.HasRequired(t => t.SubCategory)
                .WithMany(t => t.VehiclePermitRequirementCodes)
                .HasForeignKey(d => d.PermitRequirementCode);
            this.HasRequired(t => t.VehiclePermit)
                .WithMany(t => t.VehiclePermitRequirementCodes)
                .HasForeignKey(d => d.VehiclePermitID);

        }
    }
}
